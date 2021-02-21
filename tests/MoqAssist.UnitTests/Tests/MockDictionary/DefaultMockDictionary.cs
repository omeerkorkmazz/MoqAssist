using Moq;
using MoqAssist.Core.Dictionary;
using MoqAssist.UnitTests.Business.Categories;
using MoqAssist.UnitTests.Business.Products;
using MoqAssist.UnitTests.Business.Users;

namespace MoqAssist.UnitTests.Tests.MockDictionary
{
    public class DefaultMockDictionary : MoqAssistDictionary
    {
        // MoqAssistDictonary is an abstract dictionary assistant to register your mock objects once while using them in unit tests
        // SIMPLE USAGE
        // 1 - Create a dictionary class inherited from MoqAssistDictonary
        // 2 - Override RegisterMocks method
        // 3 - Use Register<T> method to register your mocks objects into dictionary
        // 4 - Give an instance from your dictionary class to MoqAssist
    
        public override void RegisterMocks()
        {
            Register<IUserService>(new Mock<IUserService>());
            Register<ICategoryService>(new Mock<ICategoryService>());
            Register<IProductService>(new Mock<IProductService>());
        }
    }
}