using MongoDB.Bson;
using MongoDB.Driver;
using WebApplication1.Api.Api;
using WebApplication1.Model;

namespace WebApplication1.Api
{
    public class MongoDBApi : IMongoDBApi
    {
        public readonly IMongoCollection<User> _mongoCollection;
        public MongoDBApi() 
        {
            _mongoCollection = connectMongoDB();
        }
        public IMongoCollection<User> connectMongoDB()
        {
            // MongoDB连接字符串
            string connectionString = "mongodb://kute:123456@47.94.210.202:27017";

            // 创建MongoDB客户端
            var mongoClient = new MongoClient(connectionString);

            // 获取数据库和集合
            var database = mongoClient.GetDatabase("kutejiang");
            var collection = database.GetCollection<User>("User");

            return collection;
        }

        public List<User> GetList()
        {
            var filter = new BsonDocument();
            return  _mongoCollection.Find(_=>true).ToList();
        }

        public User? GetAsync(string id)
        {
            var result = _mongoCollection.Find(x => x.Id == id).FirstOrDefault();
            return result;
        }
        public  List<User>? GetCondition(List<BsonDocument> pipeline)
        {


            var aggregateOptions = new AggregateOptions();
            var result = _mongoCollection.Aggregate<User>(pipeline).ToList();
            return result;
        }

        public void CreateAsync(User newUser)
        {
            _mongoCollection.InsertOne(newUser);
        }

        public bool UpdateAsync(string id, User updatedUser)
        {
            var result = _mongoCollection.ReplaceOne(x => x.Id == id, updatedUser);
            return result.ModifiedCount == 0 ? false : true;
        }
        public bool RemoveAsync(string id)
        {
            var result = _mongoCollection.DeleteOne(x=>x.Id==id);
            return result.DeletedCount == 0 ? false : true;
        }
        public bool RemovCondition(FilterDefinition<User> Filt)
        {
            var result = _mongoCollection.DeleteMany(Filt);
            return result.DeletedCount == 0 ? false : true;
        }
    }
}
