using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace oauth2testbed.Database.Tables
{
    [Table("Clients")]
    public class Client
    {
        #region ORM

        [Key]
        [Column]
        [JsonIgnore]
        public int Id { get; set; }

        [Column]
        public string Identifier { get; set; }

        [Column]
        public string Created { get; set; }

        [Column]
        public string Flow { get; set; }

        [Column]
        public string ClientId { get; set; }

        [Column]
        public string ClientSecret { get; set; }

        [Column]
        public string Username { get; set; }

        [Column]
        public string Password { get; set; }

        [Column]
        public string Scope { get; set; }

        [Column]
        public string RedirectUrls { get; set; }

        #endregion

        #region Instance functions

        /// <summary>
        /// Compile the object to be returned through the API.
        /// </summary>
        /// <param name="request">Current HTTP request.</param>
        /// <returns>Compilled object.</returns>
        public object CompileApiResponseObject(HttpRequest request)
        {
            return new
            {
                id = this.Identifier,
                created = this.Created,
                flow = this.Flow,
                authorizeUrl = $"https://{request.Host}/authorize",
                accessTokenUrl = $"https://{request.Host}/api/oauth2/authorize/{this.Flow}-flow",
                clientId = this.ClientId,
                clientSecret = this.ClientSecret,
                username = this.Username,
                password = this.Password,
                scope = this.Scope,
                redirectUrls = this.RedirectUrlsDeserialized()
            };
        }

        /// <summary>
        /// Get deserialized version of the local string.
        /// </summary>
        /// <returns>List, if any.</returns>
        public string[] RedirectUrlsDeserialized()
        {
            return this.RedirectUrls == null
                ? null
                : JsonSerializer.Deserialize<string[]>(this.RedirectUrls);
        }

        #endregion
    }
}