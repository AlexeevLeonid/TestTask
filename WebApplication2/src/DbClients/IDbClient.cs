using TestTask.src.Model;

namespace TestTask.src.Db
{
    public interface IDbClient
    {
        public Task<List<string>> GetCountriesListAsync();
        public Task<List<string>> GetTeamsListAsync();
        public Task<List<string>> GetSexListAsync();
        public Task<List<Soccer>> GetSoccersLisAsync(IQueryCollection query);
        public Task<Soccer> GetSoccerAsync(string id);
        public Task PostSoccerAsync(Soccer soccer);
        public Task PutSoccerAsync(Soccer soccer);
        public Task DeleteSoccerAsync(string id);
    }
}
