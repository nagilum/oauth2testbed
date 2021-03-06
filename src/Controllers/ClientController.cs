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
using System.Text.Json;
using System.Threading.Tasks;

namespace oauth2testbed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        /// <summary>
        /// Local logger.
        /// </summary>
        private readonly ILogger Logger;

        /// <summary>
        /// Setup logger.
        /// </summary>
        public ClientController(ILoggerFactory logger)
        {
            this.Logger = logger.CreateLogger("Api.Client");
        }

        /// <summary>
        /// Get a specific client.
        /// </summary>
        /// <param name="id">Id of client.</param>
        /// <returns>Found client.</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> Get([FromRoute] string id)
        {
            try
            {
                await using var db = new DatabaseContext();

                var client = await db.Clients
                    .FirstOrDefaultAsync(n => n.Identifier == id);

                if (client == null)
                {
                    throw new ClientNotFoundException($"Unable to find Client with identifier {id}");
                }

                return this.Ok(client.CompileApiResponseObject(this.Request));
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

        /// <summary>
        /// Create a new client.
        /// </summary>
        /// <param name="payload">Client information.</param>
        /// <returns>Created client.</returns>
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ClientPayload payload)
        {
            try
            {
                if (payload.Flow != "code" &&
                    payload.Flow != "password")
                {
                    throw new Exception("'flow' is required and only accepts the values 'code' or 'password'.");
                }

                await using var db = new DatabaseContext();

                var ident = Guid.NewGuid().ToString();
                var clientId = Tools.GenerateRandomString(16);

                while (true)
                {
                    if (!db.Clients.Any(n => n.Identifier == ident) &&
                        !db.Clients.Any(n => n.ClientId == clientId))
                    {
                        break;
                    }

                    ident = Guid.NewGuid().ToString();
                    clientId = Tools.GenerateRandomString(16);
                }

                var client = new Client
                {
                    Identifier = ident,
                    Created = DateTimeOffset.Now.ToString("o", CultureInfo.InvariantCulture),
                    Flow = payload.Flow,
                    ClientId = Tools.GenerateRandomString(16),
                    ClientSecret = Tools.GenerateRandomString(64),
                    Username = Tools.GetRandomName(),
                    Password = Tools.GenerateRandomString(32),
                    Scope = "email,personal",
                    RedirectUrls = payload.RedirectUrls != null
                        ? JsonSerializer.Serialize(payload.RedirectUrls)
                        : null
                };

                await db.Clients.AddAsync(client);
                await db.SaveChangesAsync();

                this.Logger.LogInformation($"Created new client with identifier {client.Identifier}");

                return this.Ok(client.CompileApiResponseObject(this.Request));
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

        /// <summary>
        /// Update an existing client.
        /// </summary>
        /// <param name="id">Id of client.</param>
        /// <param name="payload">Client information.</param>
        /// <returns>Success.</returns>
        [HttpPost]
        [Route("{id}")]
        public async Task<ActionResult> Update([FromRoute] string id, [FromBody] ClientPayload payload)
        {
            try
            {
                if (payload.Flow != "code" &&
                    payload.Flow != "password")
                {
                    throw new Exception("'flow' is required and only accepts the values 'code' or 'password'.");
                }

                await using var db = new DatabaseContext();

                var client = await db.Clients
                    .FirstOrDefaultAsync(n => n.Identifier == id);

                if (client == null)
                {
                    throw new ClientNotFoundException($"Unable to find Client with identifier {id}");
                }

                client.Flow = payload.Flow;
                client.RedirectUrls = payload.RedirectUrls != null
                    ? JsonSerializer.Serialize(payload.RedirectUrls)
                    : null;

                await db.SaveChangesAsync();

                this.Logger.LogInformation($"Updated client with identifier {client.Identifier}");

                return this.Ok(null);
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

        /// <summary>
        /// Delete a client.
        /// </summary>
        /// <param name="id">Id of client.</param>
        /// <returns>Success.</returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete([FromRoute] string id)
        {
            try
            {
                await using var db = new DatabaseContext();

                var client = await db.Clients
                    .FirstOrDefaultAsync(n => n.Identifier == id);

                if (client == null)
                {
                    throw new ClientNotFoundException($"Unable to find Client with identifier {id}");
                }

                db.Clients.Remove(client);

                await db.SaveChangesAsync();

                this.Logger.LogWarning($"Deleted client with identifier {client.Identifier}");

                return this.Ok(null);
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
