namespace oauth2testbed.Controllers.Payloads
{
    public class ClientPayload
    {
        /// <summary>
        /// Type of OAuth2 flow.
        /// </summary>
        public string Flow { get; set; }

        /// <summary>
        /// Redirect URLs, if any.
        /// </summary>
        public string[] RedirectUrls { get; set; }
    }
}