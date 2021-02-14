using Moq;
using MoqAssist.Core.Dictionary;
using MoqAssist.UnitTests.Business.Categories;
using MoqAssist.UnitTests.Business.Users;

namespace MoqAssist.UnitTests.Tests.MockDictionary
{
    public class MockWithSetupDictionary : MoqAssistDictionary
    {
        // If you want, yo can prepare your mock objects before registering.
        // UserService and CategoryService preparations are just simple examples. You can prepare your objects however you want with Moq library!

        public override void RegisterMocks()
        {
            Register<IUserService>(prepareUserServiceMock());
            Register<ICategoryService>(prepareCategoryServiceMock());
        }

        private Mock<IUserService> prepareUserServiceMock()
        {
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(x => x.GetById(It.IsAny<int>())).Returns<User>(null);
            return userServiceMock;
        }
        private Mock<ICategoryService> prepareCategoryServiceMock()
        {
            var categoryServiceMock = new Mock<ICategoryService>();
            categoryServiceMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(new Category() { Id = 1, Name = "Computers" });
            return categoryServiceMock;
        }
    }
}