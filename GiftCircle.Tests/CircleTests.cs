using System;
using System.Threading.Tasks;
using GiftCircle.Models;
using GiftCircle.Persistence;
using Xunit;

namespace GiftCircle.Tests
{
    public class CircleTests
    {
        private CirclesRepository _circlesRepository;
        private Guid _newCircleId;
        private Circle _retrievedCircle;

        [Fact]
        public async Task Can_create_circles()
        {
            Given_a_circles_repository();

            await When_I_create_a_circle();

            await Then_the_circle_was_created();
        }

        private void Given_a_circles_repository()
        {
            _circlesRepository = new CirclesRepository(new DynamoDbSettings {ServiceUrl = "http://localhost:5006"});
        }

        private async Task When_I_create_a_circle()
        {
            _newCircleId = Guid.NewGuid();

            var circle = new Circle{Id = _newCircleId, UserId = "123", Name = "456"};

            await _circlesRepository.CreateCircle(circle);
        }

        private async Task Then_the_circle_was_created()
        {
            _retrievedCircle = await _circlesRepository.GetCircle(_newCircleId);

            Assert.NotNull(_retrievedCircle);
        }
    }
}
