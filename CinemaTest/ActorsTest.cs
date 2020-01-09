using AutoMapper;
using Cinema.Data;
using Cinema.Data.Entities;
using Cinema.Exceptions;
using Cinema.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CinemaTest
{
    public class UnitTest1
    {
        [Fact]
        public async void ActorService_shouldReturnExceptionIfNotFound()
        {
            //arrange
            int actorId = 22;
            var MoqCineRepository = new Mock<ICineRepository>();
            var actorEntity = new ActorEntity()
            {
                Id = 1,
                Name = "Will",
                Lastname = "Smith",
                Age = 55
            };
            MoqCineRepository.Setup(m=>m.GetActorAsync(1,false)).Returns(Task.FromResult(actorEntity));
            var myProfile = new CineProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            var mapper = new Mapper(configuration);
            var actorService = new ActorsServices(MoqCineRepository.Object,mapper);

            //act
            await Assert.ThrowsAsync<NotFoundEx>(()=>actorService.GetActorAsync(actorId,false));
        }
    }
}
