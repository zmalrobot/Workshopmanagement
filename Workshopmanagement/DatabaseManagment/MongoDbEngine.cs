using MongoDB.Driver;

namespace Workshopmanagement.DatabaseManagment
{
    public class MongoDbEngine
    {
        private static MongoClient mongoClient { get; set; }

        #region GetClient
        /// <summary>
        /// Ritorna il client di mongo, se non instanzato lo instanzia
        /// </summary>
        /// <returns></returns>
        public static MongoClient GetClient()
        {
            if (mongoClient == null)
                mongoClient = InitializeClient();

            return mongoClient;
        }
        #endregion

        #region GetDatabase
        /// <summary>
        /// Ritorna il database di mongo, se il client non è instanzato prova a instanzarlo
        /// </summary>
        /// <returns></returns>
        public static IMongoDatabase GetDatabase()
        {
            if (mongoClient == null)
                mongoClient = InitializeClient();

            return mongoClient.GetDatabase(Environment.GetEnvironmentVariable("MongoDatabase"));
        }
        #endregion

        #region DisposeClient
        /// <summary>
        /// Non sono sicuro che serva, prima o poi lo tolgo
        /// </summary>
        public static void DisposeClient()
        {
            mongoClient = null;
        }
        #endregion

        #region static InitializeClient
        /// <summary>
        /// Inizializza il clinet di mongo db
        /// </summary>
        /// <returns></returns>
        private static MongoClient InitializeClient()
        {
            var setting = new MongoClientSettings();

            #region Auth
            if (Convert.ToBoolean(Environment.GetEnvironmentVariable("MongoUseAutentication")))
            {
                var mechanism = Environment.GetEnvironmentVariable("MongoMechanism");
                var identity = new MongoInternalIdentity(Environment.GetEnvironmentVariable("MongoDatabase"), Environment.GetEnvironmentVariable("MongoUsername"));
                var evidence = new PasswordEvidence(Environment.GetEnvironmentVariable("MongoPassword"));
                setting.Credential = new MongoCredential(mechanism, identity, evidence);
            }
            #endregion

            setting.ApplicationName = Environment.GetEnvironmentVariable("ApplicationName");
            setting.AllowInsecureTls = Convert.ToBoolean(Environment.GetEnvironmentVariable("AllowInsecureTls"));
            setting.Server = new MongoServerAddress(Environment.GetEnvironmentVariable("MongoServerAddress"), Convert.ToInt32(Environment.GetEnvironmentVariable("MongoServerPort")));


            var client = new MongoClient(setting);
            return client;
        }
        #endregion
    }
}
