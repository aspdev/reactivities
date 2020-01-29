using System.Linq;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using Raven.Client.Documents;

namespace DataStore
{
    public class DocumentStoreHolder
    {
        public IDocumentStore Store { get; }

        public DocumentStoreHolder()
        {
            var certificatePath = Configuration.CertificatePath;
            var passwordSecureString = Configuration.CertificatePassword;
            var store = new DocumentStore
            {
                Urls = Configuration.Urls,
                Database = Configuration.DatabaseName,
                Certificate = new X509Certificate2(certificatePath, passwordSecureString, X509KeyStorageFlags.MachineKeySet)
            };

            Store = store.Initialize();
        }


    }
}