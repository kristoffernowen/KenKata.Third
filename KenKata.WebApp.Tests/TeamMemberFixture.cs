using System;
using System.Collections.Generic;
using System.Linq;

using KenKata.Shared.Models.Entities;

namespace KenKata.WebApp.Tests
{
    public class TeamMemberFixture
    {
        private static List<TeamMemberProfileEntity> _teamProfiles = new()
        {
            new TeamMemberProfileEntity
            {
                FirstName = "Janne",
                LastName = "Jansson",
                Id = 1,
                ProfilePhotoFileName = "här",
                Title = "Chef",
                UserId = Guid.NewGuid().ToString()
            },
            new TeamMemberProfileEntity
            {
                FirstName = "Mikey",
                LastName = "Manson",
                Id = 2,
                ProfilePhotoFileName = "där",
                Title = "Wingman",
                UserId = Guid.NewGuid().ToString()
            }
        };

        public static List<TeamMemberProfileEntity> GetAllTeamMembers()
        {
            return _teamProfiles.ToList();
        }

    }
}
