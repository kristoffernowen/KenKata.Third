using KenKata.Shared.Models.Entities;
using KenKata.WebApp.Data;
using Microsoft.EntityFrameworkCore;

namespace KenKata.WebApp.Service
{

    public interface ITeamService
    {
        Task<List<TeamMemberProfileEntity>> GetAllAsync();
    }
    public class TeamService : ITeamService
    {
        private readonly SqlContext _sqlContext;

        public TeamService(SqlContext sqlContext)
        {
            _sqlContext = sqlContext;
        }

        public async Task<List<TeamMemberProfileEntity>> GetAllAsync()
        {
            return await _sqlContext.TeamMemberProfiles.ToListAsync();
        }
    }
}
