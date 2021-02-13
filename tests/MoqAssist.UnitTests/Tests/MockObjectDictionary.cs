using Moq;
using MoqAssist.Core;
using MoqAssist.UnitTests.Business.Categories;
using MoqAssist.UnitTests.Business.Products;
using MoqAssist.UnitTests.Business.Users;

namespace MoqAssist.UnitTests.Tests
{
    public class MockObjectDictionary : BaseMockDictionary
    {
        public override void LoadMockObjects()
        {
            MockObjectDictionary.Add(typeof(IUserService).FullName, new Mock<IUserService>());
            MockObjectDictionary.Add(typeof(ICategoryService).FullName, new Mock<ICategoryService>());
            MockObjectDictionary.Add(typeof(IProductService).FullName, new Mock<ProductService>());
        }
    }
}