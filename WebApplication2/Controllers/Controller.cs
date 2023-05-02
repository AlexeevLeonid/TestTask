using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Test66bit2.src.Db;
using Test66bit2.src.DbVersionators;
using Test66bit2.src.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Test66bit2.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class Controller : ControllerBase
    {

        private readonly IDbClient dbClient;

        [HttpGet("countries")]
        public async Task<List<string>> GetCountries()
        {
            return await dbClient.GetCountriesListAsync();
        }

        [HttpGet("sex")]
        public async Task<List<string>> GetSex()
        {
            return await dbClient.GetSexListAsync();
        }

        [HttpGet("teams")]
        public async Task<List<string>> GetTeams()
        {
            return await dbClient.GetTeamsListAsync();
        }

        [HttpGet("soccers")]
        public async Task<List<Soccer>> GetSoccers()
        {
            var query = HttpContext.Request.Query;
            return await dbClient.GetSoccersLisAsync(query);
        }

        public Controller(IDbVersionator<int> dbVersionator, IDbClient dbClient)
        {
            this.dbClient = dbClient;
        }
    }
}
