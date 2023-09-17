using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using WebApplication1.Api;
using WebApplication1.Api.Api;
using WebApplication1.Model;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController : ControllerBase
    {

        public IMongoDBApi _mongoDBApi;
        public WeatherController(IMongoDBApi mongoDBApi)
        {
            _mongoDBApi = mongoDBApi;
        }
        [HttpGet("GetList")]
        public ActionResult<List<User>> GetList()
        {
            var user =  _mongoDBApi.GetList();

            if (user is null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpGet("Get")]
        public ActionResult<User> Get(string id)
        {
            var user =  _mongoDBApi.GetAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            return user;
        }
        [HttpGet("GetCondition")]
        public ActionResult<List<User>> GetCondition()
        {
            var pipeline = new List<BsonDocument>
            {
                BsonDocument.Parse("{ $sort: { 'age': 1 } }"), // ����age�ֶ���������
                BsonDocument.Parse("{ $skip: 0 }"), // ����ǰ0���ĵ�
                BsonDocument.Parse("{ $limit: 2 }") // ���ƽ������СΪ2
            };
            var user = _mongoDBApi.GetCondition(pipeline);

            if (user is null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost("Post")]
        public IActionResult Post(User newuser)
        {
             _mongoDBApi.CreateAsync(newuser);

            return CreatedAtAction(nameof(Get), new { id = newuser.Id }, newuser);
        }

        [HttpPost("Update")]
        public  IActionResult Update(string id, User updatedBook)
        {
            var book =  _mongoDBApi.GetAsync(id);

            if (book is null)
            {
                return NotFound();
            }

            updatedBook.Id = book.Id;

             _mongoDBApi.UpdateAsync(id, updatedBook);

            return NoContent();
        }

        [HttpDelete("Delete {id}")]
        public IActionResult Delete(string id)
        {
            var book =  _mongoDBApi.GetAsync(id);

            if (book is null)
            {
                return NotFound();
            }

            _mongoDBApi.RemoveAsync(id);

            return NoContent();
        }
        [HttpDelete("Delete")]
        public IActionResult Delete1()
        {
            var filter = Builders<User>.Filter.In("love", new BsonArray(new[] { "һ�꼶", "����" }));

            _mongoDBApi.RemovCondition(filter);

            return NoContent();
        }
    }
}