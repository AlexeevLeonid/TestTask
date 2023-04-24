using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data.SqlClient;
using Test66bit2.src.Db;
using Test66bit2.src.DbClients.EntityClient;
using Test66bit2.src.Model;

namespace Test66bit.src.DbClients.EntityClient
{
    public class EntityDbClient : IDbClient
    {
        private readonly EntityDbContext context;

        public async Task<List<string>> GetCountriesListAsync()
        {
            return context.countries;
        }

        public async Task<List<string>> GetTeamsListAsync()
        {
            return await context.Soccers.AsNoTracking().Select(x => x.team).Distinct().ToListAsync();
        }

        public async Task<List<string>> GetSexListAsync()
        {
            return context.sex;
        }

        public async Task<List<Soccer>> GetSoccersLisAsync(IQueryCollection query)
        {
            var linq = context.Soccers.AsNoTracking();
            if (query.TryGetValue("team", out var team))
                linq = linq.Where(x => x.team == team.ToString());
            if (query.TryGetValue("sex", out var sex))
                linq = linq.Where(x => x.sex == sex.ToString());
            if (query.TryGetValue("country", out var country))
                linq = linq.Where(x => x.country == country.ToString());
            return linq.ToList();
        }

        public async Task<Soccer> GetSoccerAsync(string id)
        {
            return context.Soccers.AsNoTracking().FirstOrDefault(x => x.id == id);
        }

        public async Task PostSoccerAsync(Soccer soccer)
        {
            soccer.id = (int.Parse(context.Soccers.Max(x => x.id)) + 1).ToString();
            context.Soccers.Add(soccer);
            await context.SaveChangesAsync();
        }

        public async Task PutSoccerAsync(Soccer soccer)
        {
            var soccerInBase = context.Soccers.AsNoTracking().FirstOrDefault(x => x.id == soccer.id);
            soccerInBase = soccer;
            context.Soccers.Update(soccerInBase);
            await context.SaveChangesAsync();
        }

        public async Task DeleteSoccerAsync(string id)
        {
            context.Soccers.Remove(context.Soccers.AsNoTracking().FirstOrDefault(x => x.id == id));
            await context.SaveChangesAsync();
        }

        public EntityDbClient(IServiceScopeFactory factory)
        {
            this.context = factory.CreateScope().ServiceProvider.GetRequiredService<EntityDbContext>();
        }
    }
}
