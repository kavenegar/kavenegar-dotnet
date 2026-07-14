namespace Kavenegar
{
    /// <summary>
    /// Configuration options for the Kavenegar API client.
    /// </summary>
    public class KavenegarOptions
    {
        /// <summary>
        /// Gets or sets the API key used for request authentication.
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// Gets or sets the base URL for the API endpoints.
        /// Defaults to "https://api.kavenegar.com".
        /// </summary>
        public string BaseUrl { get; set; } = "https://api.kavenegar.com";
    }
}
