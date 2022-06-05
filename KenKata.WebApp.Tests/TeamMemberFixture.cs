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
            },
            new TeamMemberProfileEntity
            {
                FirstName = "Likey",
                LastName = "Manson",
                Id = 3,
                ProfilePhotoFileName = "där",
                Title = "Wingman",
                UserId = Guid.NewGuid().ToString()
            },
            new TeamMemberProfileEntity
            {
                FirstName = "Tor",
                LastName = "Manson",
                Id = 4,
                ProfilePhotoFileName = "där",
                Title = "Wingman",
                UserId = Guid.NewGuid().ToString()
            },
            new TeamMemberProfileEntity
            {
                FirstName = "Oden",
                LastName = "Manson",
                Id = 5,
                ProfilePhotoFileName = "där",
                Title = "Wingman",
                UserId = Guid.NewGuid().ToString()
            },
            new TeamMemberProfileEntity
            {
                FirstName = "Sven",
                LastName = "Manson",
                Id = 6,
                ProfilePhotoFileName = "där",
                Title = "Wingman",
                UserId = Guid.NewGuid().ToString()
            },
            new TeamMemberProfileEntity
            {
                FirstName = "Britt",
                LastName = "Manson",
                Id = 7,
                ProfilePhotoFileName = "där",
                Title = "Wingman",
                UserId = Guid.NewGuid().ToString()
            },
            new TeamMemberProfileEntity
            {
                FirstName = "Stuart",
                LastName = "Manson",
                Id = 8,
                ProfilePhotoFileName = "där",
                Title = "Wingman",
                UserId = Guid.NewGuid().ToString()
            },
            new TeamMemberProfileEntity
            {
                FirstName = "Kimmy",
                LastName = "Manson",
                Id = 9,
                ProfilePhotoFileName = "där",
                Title = "Wingman",
                UserId = Guid.NewGuid().ToString()
            }

        };

        // Actually I want max 8 for the view

        public static List<TeamMemberProfileEntity> GetAllTeamMembers()
        {
            return _teamProfiles.ToList();
        }

    }
}
