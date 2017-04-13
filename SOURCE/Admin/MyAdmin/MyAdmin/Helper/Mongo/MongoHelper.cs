using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Configuration;

namespace MyAdmin.Helpers
{
    public class MongoHelper<T> where T : class
    {
        public IMongoCollection<T> Collection { get; private set; }
        protected IMongoClient _client;
        protected IMongoDatabase _database;
        public MongoHelper(string collectionName)
        {
            var connectionString = ConfigurationManager.AppSettings["MongoDbConnectionString"];

            var mongoUrl = new MongoUrl(connectionString);

            

            _client = new MongoClient(connectionString);

            _database = _client.GetDatabase(mongoUrl.DatabaseName);
            

            //var collections = _database.ListCollections().ToList();

            //foreach (var item in collections)
            //{
            //    if (item["type"].ToString().Equals("system"))
            //        continue;
            //    _database.DropCollection(item["name"].ToString());
            //}

            //BsonSerializer.RegisterSerializer(typeof(DateTime), new MyMongoDBDateTimeSerializer());

            Collection = _database.GetCollection<T>(typeof(T).Name.ToLower());
            

        }
    }
}