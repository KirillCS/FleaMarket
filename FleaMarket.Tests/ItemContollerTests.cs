using AutoMapper;
using FleaMarket.Interfaces.Repositories;
using FleaMarket.Interfaces.Services;
using FleaMarket.Models;
using FleaMarket.Web.Controllers;
using FleaMarket.Web.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace FleaMarket.Tests
{
    public class ItemContollerTests
    {
        private readonly ItemController controller;
        private readonly Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
        private readonly Mock<IItemRepository> itemRepositoryMock = new Mock<IItemRepository>();
        private readonly Mock<IOptions<ApplicationConfigurations>> configurationMock = new Mock<IOptions<ApplicationConfigurations>>();
        private readonly Mock<IItemService> itemServiceMock = new Mock<IItemService>();
        private readonly Mock<IMapper> mapperMock = new Mock<IMapper>();

        public ItemContollerTests()
        {
            controller = new ItemController(unitOfWorkMock.Object, configurationMock.Object, itemServiceMock.Object, mapperMock.Object);
            unitOfWorkMock.Setup(u => u.ItemRepository).Returns(itemRepositoryMock.Object);
        }

        [Fact]
        public void Get_ShouldReturnOk()
        {
            var parameters = new ItemGettingParameters();

            var result = controller.Get(parameters);

            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<GetItemResponse>(objectResult.Value);
        }
    }
}
