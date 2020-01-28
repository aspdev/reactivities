using System.Linq;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using API.Config;
using Microsoft.AspNetCore.Hosting;
using Raven.Client.Documents;

namespace API.DataStore
{
    public class ReactivitiesDocumentStoreHolder
    {
        public IDocumentStore Store { get; }

        public ReactivitiesDocumentStoreHolder(ReactivitiesConfiguration configuration,
        IWebHostEnvironment environment)
        {
            var certificatePassword = configuration.RavenConfiguration.CertificatePassword;
            var certificatePath = environment.ContentRootPath + configuration.RavenConfiguration.CertificatePath;
            var passwordSecureString = CreateSecureString(certificatePassword);
            var store = new DocumentStore
            {
                Urls = configuration.RavenConfiguration.Urls,
                Database = configuration.RavenConfiguration.DatabaseName,
                Certificate = new X509Certificate2(certificatePath, passwordSecureString, X509KeyStorageFlags.MachineKeySet)
            };

            Store = store.Initialize();
        }

        private SecureString CreateSecureString(string stringToMakeSecure)
        {
            var secureString = new SecureString();
            stringToMakeSecure.ToCharArray().ToList().ForEach(secureString.AppendChar);
            secureString.MakeReadOnly();

            return secureString;
        }
    }
}