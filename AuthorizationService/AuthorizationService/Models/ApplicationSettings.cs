namespace AuthorizationService.Models
{
    public class ApplicationSettings
    {
        public string JwtSecret { get; set; }
        public string ClientUrl { get; set; }
    }
}