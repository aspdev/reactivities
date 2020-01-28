namespace API.Config
{
    public class ReactivitiesConfiguration
    {
        public RavenConfiguration RavenConfiguration { get; set; }


    }

    public class RavenConfiguration
    {
        public string DatabaseName { get; set; }
        public string[] Urls { get; set; }
        public string CertificatePath { get; set; }
        public string CertificatePassword { get; set; }
    }
}