namespace WebAPI.Helpers
{
    public class AppSettings
    {
        public string DB_HOST { get; set; }
        public string DB_NAME { get; set; }
        public string DB_USER { get; set; }
        public string DB_PASSWORD { get; set; }
        public string SMTP_HOST { get; set; }
        public string SMTP_USER { get; set; }
        public string SMTP_PASSWORD { get; set; }

        public string JWT_TOKEN {get; set; }
        public string JWT_ISSUER {get; set; }
    }
}