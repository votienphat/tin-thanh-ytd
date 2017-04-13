using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;
using MyAdmin.Helpers;

namespace MongoDbSample.Models
{
    public class PostService
    {
        private readonly MongoHelper<Post> _posts;

        public PostService()
        {
            _posts = new MongoHelper<Post>("");
        }

        public async Task Create(Post post)
        {
            post.Comments = new List<Comment>();
            await _posts.Collection.InsertOneAsync(post);
        }

        //public void Edit(Post post)
        //{
        //    _posts.Collection.UpdateMany(
        //        Query.EQ("_id", post.PostId),
        //        Update.Set("Title", post.Title)
        //            .Set("Url", post.Url)
        //            .Set("Summary", post.Summary)
        //            .Set("Details", post.Details));
        //}

        public void Delete(ObjectId postId)
        {
            var filter = Builders<Post>.Filter.Eq("_id", postId);
            _posts.Collection.DeleteOne(filter);
        }

        public IList<Post> GetPosts()
        {
            return _posts.Collection.AsQueryable().ToList();
        }

        public Post GetPost(ObjectId id)
        {
            var filter = Builders<Post>.Filter.Eq("_id", id);
            
            var post = _posts.Collection.Find(filter).Single();
            return post;
        }

        //public Post GetPost(string url)
        //{
        //    var post = _posts.Collection.Find(Query.EQ("Url", url)).SetFields(Fields.Slice("Comments", -5)).Single();
        //    post.Comments = post.Comments.OrderByDescending(c => c.Date).ToList();
        //    return post;
        //}
    }
}