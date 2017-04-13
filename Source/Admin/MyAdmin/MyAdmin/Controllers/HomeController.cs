using System.Web.Mvc;
using MyAdmin.ActionFilter;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Linq;
using System;
using MyAdmin.Helper;
using MyAdmin.Models.Home;
using MongoDB.Driver;
using MongoDbSample.Models;
using MongoDB.Bson;

namespace MyAdmin.Controllers
{
    public class HomeController : BaseController
    {
        protected static IMongoClient _client;
        protected static IMongoDatabase _database;
        readonly PostService _postService;
        public HomeController()
        {
            _postService = new PostService();
        }

        [Authorize]
        public ActionResult Index()
        {
            //_client = new MongoClient();
            //_database = _client.GetDatabase("admin");

            //var document = new BsonDocument
            //    {
            //        { "address" , new BsonDocument
            //            {
            //                { "street", "2 Avenue" },
            //                { "zipcode", "10075" },
            //                { "building", "1480" },
            //                { "coord", new BsonArray { 73.9557413, 40.7720266 } }
            //            }
            //        },
            //        { "borough", "Manhattan" },
            //        { "cuisine", "Italian" },
            //        { "grades", new BsonArray
            //            {
            //                new BsonDocument
            //                {
            //                    { "date", new DateTime(2014, 10, 1, 0, 0, 0, DateTimeKind.Utc) },
            //                    { "grade", "A" },
            //                    { "score", 11 }
            //                },
            //                new BsonDocument
            //                {
            //                    { "date", new DateTime(2014, 1, 6, 0, 0, 0, DateTimeKind.Utc) },
            //                    { "grade", "B" },
            //                    { "score", 17 }
            //                }
            //            }
            //        },
            //        { "name", "Vella" },
            //        { "restaurant_id", "41704620" }
            //    };

            ////var collection = _database.GetCollection<BsonDocument>("restaurants");
            ////await collection.InsertOneAsync(document);

            //var result =_postService.Create(new Post
            //{
            //    Author = "Alopicasso",
            //    Date = DateTime.Now,
            //    Summary = "Hello world",
            //    Title = " Hello world title",
            //    Url = "mamana"
            //});

            //var posts = _postService.GetPosts();
            return View();
        }
    }
}