namespace WebApplication1.Api.Api
{
    using MongoDB.Bson;
    using MongoDB.Driver;
    using WebApplication1.Model;

    public interface IMongoDBApi
    {
        //连接
        public IMongoCollection<User> connectMongoDB ();
        //查询 全部
        public  List<User> GetList();
        // 查询单条
        public  User? GetAsync(string id);
        //根据条件查询
        public List<User>? GetCondition(List<BsonDocument> pipeline);
        //添加
        public  void CreateAsync(User newUser);
        //修改
        public bool UpdateAsync(string id, User updatedUser);
        //根据Id删除
        public bool RemoveAsync(string id);
        //根据条件删除
        public bool RemovCondition(FilterDefinition<User> Filt);
    }
}
