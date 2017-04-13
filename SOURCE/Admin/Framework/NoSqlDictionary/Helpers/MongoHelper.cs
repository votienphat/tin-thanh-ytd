using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace NoSqlDictionary.Helpers
{
    public class MongoHelper<T> where T : class
    {
        public IMongoCollection<T> Collection { get; private set; }
        protected IMongoClient _client;
        protected IMongoDatabase _database;
        private string collectionName;

        public MongoHelper()
        {
            var connectionString = ConfigurationManager.AppSettings["MongoDbConnectionString"];

            var mongoUrl = new MongoUrl(connectionString);



            _client = new MongoClient(connectionString);

            _database = _client.GetDatabase(mongoUrl.DatabaseName);


            var collections = _database.ListCollections().ToList();

            foreach (var item in collections)
            {
                if (item["type"].ToString().Equals("system"))
                    continue;
                _database.DropCollection(item["name"].ToString());
            }

        }

        public MongoHelper(string collectionName)
        {
            this.collectionName = collectionName;
        }
    }
}
