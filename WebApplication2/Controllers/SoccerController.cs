﻿using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;
using TestTask.src.Db;
using TestTask.src.DbVersionators;
using TestTask.src.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestTask.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SoccerController : ControllerBase
    {

        private readonly IDbVersionator<int> dbVersionator;
        private readonly IDbClient dbClient;

        // GET api/<ValuesController>/5
        [HttpGet("")]
        public async Task<Soccer> Get()
        {
            var id = HttpContext.Request.Query["id"];
            var soccer = await dbClient.GetSoccerAsync(id.ToString());
            return soccer;
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task Post([FromBody] Soccer soccer)
        {
            if (soccer.HasEmptyFieldExceptId())
            {
                return;
            }
            await dbClient.PostSoccerAsync(soccer);
            dbVersionator.СommitDatabaseСhange();
        }

        // PUT api/<ValuesController>/5
        [HttpPut("")]
        public async Task Put([FromBody] Soccer soccer)
        {
            await dbClient.PutSoccerAsync(soccer);
            dbVersionator.СommitDatabaseСhange();
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task Delete(string guid)
        {
            await dbClient.DeleteSoccerAsync(guid);
            dbVersionator.СommitDatabaseСhange();
        }

        public SoccerController(IDbVersionator<int> dbVersionator, IDbClient dbClient)
        {
            this.dbVersionator = dbVersionator;
            this.dbClient = dbClient;
        }
    }
}
