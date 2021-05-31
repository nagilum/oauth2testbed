namespace oauth2testbed.Controllers.Payloads
{
    public class LoginPayload
    {
        /// <summary>
        /// OAuth2 client id.
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Specified redirect URL.
        /// </summary>
        public string RedirectUrl { get; set; }

        /// <summary>
        /// Attempted username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Attempted password.
        /// </summary>
        public string Password { get; set; }
    }
}