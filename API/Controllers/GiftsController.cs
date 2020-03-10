using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Couchbase;
using Couchbase.Core;
using Couchbase.N1QL;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class GiftsController : Controller
    {
        private readonly IBucket Bucket;

        public GiftsController()
        {
            Bucket = ClusterHelper.GetBucket("demo");
        }
        [HttpGet]
        [Route("api/getall")]
        public IActionResult GetAll()
        {
            //cria a query n1ql
            var n1ql = @"SELECT g.*, META(g).id
                        FROM demo g
                        WHERE g.type = 'WishListItem';";
            var query = QueryRequest.Create(n1ql);
            query.ScanConsistency(ScanConsistency.RequestPlus);

            var result = Bucket.Query<WishListItem>(query);
            return Ok(result.Rows);

        }
        [HttpGet]
        [Route("api/get/{id}")]
        public IActionResult Get(Guid id)
        {
            var result = Bucket.Get<WishListItem>(id.ToString());

            if (result.Success)
                return Ok(result.Value);
            else if (result.Status == Couchbase.IO.ResponseStatus.KeyNotFound)
                return NotFound(result.Exception);
            else
                return BadRequest(result.Exception);
        }
        [HttpPost]
        [Route("api/edit")]
        public IActionResult Edit([FromBody] WishListItem item)
        {
            if (!item.Id.HasValue)
                item.Id = Guid.NewGuid();

            //chave sera o id e o valor sera o wishlistitem
            Bucket.Upsert(item.Id.ToString(), new
            WishListItem{
                Name = item.Name
            });
            return Ok(item);
        }
        [HttpDelete]
        [Route("api/delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            var result = Bucket.Remove(id.ToString());

            if (result.Success)
                return Ok(id);
            else if (result.Status == Couchbase.IO.ResponseStatus.KeyNotFound)
                return NotFound(result.Exception);
            else
                return BadRequest(result.Exception);
        }
    }
}