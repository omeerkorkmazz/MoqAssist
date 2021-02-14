using Moq;
using MoqAssist.Core.Dictionary;
using MoqAssist.UnitTests.Business.Categories;
using MoqAssist.UnitTests.Business.Users;

namespace MoqAssist.UnitTests.Tests
{
    public class MockObjectDictionary : MoqAssistDictionary
    {
        public override void RegisterMockObjects()
        {
            AddToDictionary<IUserService>(new Mock<IUserService>());
            AddToDictionary<ICategoryService>(new Mock<ICategoryService>());
        }
    }
}