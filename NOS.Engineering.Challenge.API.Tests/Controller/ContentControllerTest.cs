using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.MVC;
using Moq;
using NOS.Engineering.Challenge.API.Tests.MockData;

namespace NOS.Engineering.Challenge.API.Tests.Controller
{
    public class ContentControllerTest : IClassFixture<TestFixture<Program>>
    {
        private Mock<IDatabase<Content?, ContentDto>> Database;
        private IContentsManager Manager;
        private ContentController Controller;

        public ContentControllerTest(TestFixture<Program> fixture)
        {
            // Mock data
            IQueryble<Content> contents = new ContentMockData().Contents;
            Content content = new ContentMockData().Content;
            //etc

            Database = new Mock<IDatabase>();

            // Setups
            Database.Setup(x => x.Create(It.IsAny<Content>())).Returns(contents);
            Database.Setup(x => x.Read(It.IsAny<int>())).Returns(content);
            //etc

            // Add context

            Manager = new ContentsManager(Database.Object);
            Controller = new ContentController(Manager, null);
        }

        [Fact]
        public async Task CreateContent_OkResult()
        {
            // Arrange
            Content toCreateContent = new ContentMockData().ContentInput;

            // Act
            IActionResult response = Controller.Create(toCreateContent);
            Database.Verify(x => x.Create(It.IsAny<ContentDto>()), Times.Once);

            // Assert
            OkObjectResult actionResult = Assert.IsType<OkObjectResult>(response);

            Assert.Equal(200, actionResult.StatusCode);
            Assert.IsType<OkObjectResult>(actionResult);

            Content responseObj = Assert.IsType<Content>(actionResult.Value);

            Assert.NotNull(responseObj);
            Assert.Equal(1, responseObj.Id);
        }

        [Fact]
        public async Task CreateContent_EmptyRequest()
        {
            // Arrange
            Content toCreateContent = null;

            // Act
            IActionResult response = Controller.Create(toCreateContent);

            // Assert
            OkObjectResult actionResult = Assert.IsType<OkObjectResult>(response);

            // TODO Handle error msg on services
            Assert.Equal("Not created", actionResult.details);
        }

        [Fact]
        [InlineData(1)]
        public async Task Read_OkResult(int contentId)
        {
            // Act
            IActionResult response = Controller.Read(contentId);
            Database.Verify(x => x.Read(It.IsAny<int>()), Times.Once);

            // Assert
            OkObjectResult actionResult = Assert.IsType<OkObjectResult>(response);

            Assert.Equal(200, actionResult.StatusCode);
            Assert.IsType<OkObjectResult>(actionResult);

            Content responseObj = Assert.IsType<Content>(actionResult.Value);
            Assert.NotNull(responseObj);
            Assert.Equal(1, (int)responseObj.Id);   
        }

        [Fact]
        [InlineData(9999999)]
        public async Task Read_NOkResult(int contentId)
        {
            // Arrange
            Database.Setup(x => x.Read(contentId)).ThrowsAsync(null);

            // Act
            IActionResult response = Controller.Read(contentId);

            // Assert
            OkObjectResult actionResult = Assert.IsType<OkObjectResult>(response);

            Assert.Equal(200, actionResult.StatusCode);
            Assert.IsType<OkObjectResult>(actionResult);

            Content responseObj = Assert.IsType<Content>(actionResult.Value);
            Assert.NotNull(responseObj);

            // TODO Handle error msg on services
            Assert.Equal("Not found", actionResult.details);
        }
    }
}
