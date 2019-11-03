using System;
using System.Threading.Tasks;
using Xunit;

namespace GiftCircle.Tests
{
    public class CircleTests
    {
        private CirclesRepository _circlesRepository;
        private Guid _createdCircleId;
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
            _createdCircleId = await _circlesRepository.CreateCircle("123", "456");
        }

        private async Task Then_the_circle_was_created()
        {
            _retrievedCircle = await _circlesRepository.GetCircle(_createdCircleId);

            Assert.NotNull(_retrievedCircle);
        }
    }
}
