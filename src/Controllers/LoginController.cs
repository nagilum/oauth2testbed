using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using oauth2testbed.Controllers.Exceptions;
using oauth2testbed.Controllers.Payloads;
using oauth2testbed.Core;
using oauth2testbed.Database;
using oauth2testbed.Database.Tables;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace oauth2testbed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        /// <summary>
        /// Local logger.
        /// </summary>
        private readonly ILogger Logger;

        /// <summary>
        /// Setup logger.
        /// </summary>
        public LoginController(ILoggerFactory logger)
        {
            this.Logger = logger.CreateLogger("Api.Login");
        }

        /// <summary>
        /// Attempt to log a user in.
        /// </summary>
        /// <param name="payload">User credentials.</param>
        /// <returns>Success.</returns>
        [HttpPost]
        public async Task<ActionResult> Login([FromBody] LoginPayload payload)
        {
            try
            {
                if (payload.ClientId == null)
                {
                    throw new Exception("'clientId' is required.");
                }

                await using var db = new DatabaseContext();

                var client = await db.Clients
                    .FirstOrDefaultAsync(n => n.ClientId == payload.ClientId);

                if (client == null)
                {
                    throw new ClientNotFoundException($"Unable to find Client with id {payload.ClientId}");
                }

                var redirectUrls = client.RedirectUrlsDeserialized();

                if (redirectUrls.Length > 0 &&
                    !redirectUrls.Contains(payload.RedirectUrl))
                {
                    throw new Exception(
                        $"'redirectUrl' is required and specified one '{payload.RedirectUrl}' is incorrect.");
                }

                if (client.Username != payload.Username ||
                    client.Password != payload.Password)
                {
                    throw new LoginFailedException("Username and/or password missmatch.");
                }

                var attempt = new LoginAttempt
                {
                    ClientDbId = client.Id,
                    Created = DateTimeOffset.Now.ToString("o", CultureInfo.InvariantCulture),
                    AuthCode = Tools.GenerateRandomString(64)
                };

                await db.LoginAttempts.AddAsync(attempt);
                await db.SaveChangesAsync();

                this.Logger.LogInformation($"Successful login attempt for client with identifier {client.Identifier}");

                return this.Ok(new
                {
                    code = attempt.AuthCode
                });
            }
            catch (LoginFailedException ex)
            {
                this.Logger.LogCritical(ex, ex.Message);

                return this.Unauthorized(null);
            }
            catch (ClientNotFoundException ex)
            {
                this.Logger.LogCritical(ex, ex.Message);

                return this.NotFound(null);
            }
            catch (Exception ex)
            {
                this.Logger.LogCritical(ex, ex.Message);

                return this.BadRequest(new
                {
                    message = ex.Message
                });
            }
        }
    }
}