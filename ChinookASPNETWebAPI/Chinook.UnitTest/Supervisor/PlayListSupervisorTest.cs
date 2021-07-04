﻿using System.Linq;
using System.Threading.Tasks;
using Chinook.Domain.Supervisor;
using Xunit;

namespace Chinook.UnitTest.Supervisor
{
    public class PlayListSupervisorTest
    {
        private readonly IChinookSupervisor _super;

        public PlayListSupervisorTest(IChinookSupervisor s) => _super = s;

        [Fact]
        public async Task PlayListGetAll()
        {
            // Act
            var playLists = (await _super.GetAllPlaylist()).ToList();

            // Assert
            Assert.True(playLists.Count > 1, "The number of play lists was not greater than 1");
        }
    }
}