﻿using System.Threading.Tasks;
using Chinook.Domain.Repositories;
using Xunit;

namespace Chinook.UnitTest.Repository
{
    public class ArtistRepositoryTest
    {
        private readonly IArtistRepository _repo;

        public ArtistRepositoryTest(IArtistRepository a) => _repo = a;

        [Fact]
        public async Task ArtistGetAll()
        {
            // Act
            var artists = await _repo.GetAll();

            // Assert
            Assert.True(artists.Count > 1, "The number of artists was not greater than 1");
        }
    }
}