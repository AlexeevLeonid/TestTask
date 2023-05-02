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
        IServiceScopeFactory factory;

        public async Task<List<string>> GetCountriesListAsync()
        {
            using (var context = factory.CreateScope().ServiceProvider.GetRequiredService<EntityDbContext>())
            {
                return context.countries;
            }
        }

        public async Task<List<string>> GetTeamsListAsync()
        {
            using (var context = factory.CreateScope().ServiceProvider.GetRequiredService<EntityDbContext>())
            {
                return await context.Soccers.AsNoTracking().Select(x => x.team).Distinct().ToListAsync();
            }
        }

        public async Task<List<string>> GetSexListAsync()
        {
            using (var context = factory.CreateScope().ServiceProvider.GetRequiredService<EntityDbContext>())
            {
                return context.sex;
            }
        }

        public async Task<List<Soccer>> GetSoccersLisAsync(IQueryCollection query)
        {
            using (var context = factory.CreateScope().ServiceProvider.GetRequiredService<EntityDbContext>())
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
        }

        public async Task<Soccer> GetSoccerAsync(string id)
        {
            using (var context = factory.CreateScope().ServiceProvider.GetRequiredService<EntityDbContext>())
            {
                return context.Soccers.AsNoTracking().FirstOrDefault(x => x.id == id);
            }
        }

        public async Task PostSoccerAsync(Soccer soccer)
        {
            using (var context = factory.CreateScope().ServiceProvider.GetRequiredService<EntityDbContext>())
            {
                soccer.id = Guid.NewGuid().ToString();
                context.Soccers.Add(soccer);
                await context.SaveChangesAsync();
            }
        }

        public async Task PutSoccerAsync(Soccer soccer)
        {
            using (var context = factory.CreateScope().ServiceProvider.GetRequiredService<EntityDbContext>())
            {
                var soccerInBase = context.Soccers.AsNoTracking().FirstOrDefault(x => x.id == soccer.id);
                soccerInBase = soccer;
                context.Soccers.Update(soccerInBase);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteSoccerAsync(string id)
        {
            using (var context = factory.CreateScope().ServiceProvider.GetRequiredService<EntityDbContext>())
            {
                context.Soccers.Remove(context.Soccers.AsNoTracking().FirstOrDefault(x => x.id == id));
                await context.SaveChangesAsync();
            }
        }

        public EntityDbClient(IServiceScopeFactory factory)
        {
            this.factory = factory;
        }
    }
}
