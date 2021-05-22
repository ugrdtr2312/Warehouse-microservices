namespace AuthorizationService.Handlers
{
    public class Secrets
    {
        public string JwtSecret { get; }
        public string ConnectionString { get; }
        
        public Secrets(string jwtSecret, string connectionString)
        {
            JwtSecret = jwtSecret;
            ConnectionString = connectionString;
        }
    }
}