using System;
using Moq;
using MoqAssist.Core;
using MoqAssist.Core.Dictionary;
using MoqAssist.UnitTests.Business.Categories;
using MoqAssist.UnitTests.Business.Products;
using MoqAssist.UnitTests.Business.Users;
using MoqAssist.UnitTests.Tests.MockDictionary;
using Xunit;

namespace MoqAssist.UnitTests.Tests
{

    public class ProductServiceTests : IDisposable
    {
        private MoqAssist<ProductService> _productService { get; set; }
        private IProductService _productServiceInstance { get; set; }
        private MoqAssistDictionary _mockDictionary => DictionaryBuilder.Default;

        #region Mocks
        private Mock<IUserService> _userServiceMock { get; set; }
        private Mock<ICategoryService> _categoryServiceMock { get; set; }
        #endregion

        public ProductServiceTests()
        {
            _productService = MoqAssist<ProductService>.Construct(_mockDictionary);
            _productServiceInstance = _productService.GetConstructors()[0];

            #region Mocks
            _userServiceMock = _productService.GetMock<IUserService>();
            _categoryServiceMock = _productService.GetMock<ICategoryService>();
            #endregion
        }

        public void Dispose()
        {
            _userServiceMock = null;
            _categoryServiceMock = null;
        }

        [Theory]
        [InlineData("Test Product", 100.50, 25, 1, 3)]
        [InlineData("Test Product 2", 25, 2, 4, 9)]
        public void Create_Should_Return_True_When_Process_Successfull(string productName, decimal price, int stock, int userId, int categoryId)
        {
            _userServiceMock.Setup(x => x.GetById(userId)).Returns(new User() { Id = userId });
            _categoryServiceMock.Setup(x => x.GetById(categoryId)).Returns(new Category() { Id = categoryId });

            var response = _productServiceInstance.Create(productName, price, stock, userId, categoryId);
            Assert.True(response == true);
        }

        [Theory]
        [InlineData("Test Product", 100.50, 25, 1, 3)]
        [InlineData("Test Product 2", 25, 2, 4, 9)]
        public void Create_Should_Return_False_When_Category_Null(string productName, decimal price, int stock, int userId, int categoryId)
        {
            _userServiceMock.Setup(x => x.GetById(userId)).Returns(new User() { Id = userId });
            _categoryServiceMock.Setup(x => x.GetById(categoryId)).Returns<Category>(null);

            var response = _productServiceInstance.Create(productName, price, stock, userId, categoryId);
            Assert.True(response == false);
        }

        [Theory]
        [InlineData("Test Product", 100.50, 25, 1, 3)]
        [InlineData("Test Product 2", 25, 2, 4, 9)]
        public void Create_Should_Return_False_When_User_Null(string productName, decimal price, int stock, int userId, int categoryId)
        {
            _userServiceMock.Setup(x => x.GetById(userId)).Returns<User>(null);
            _categoryServiceMock.Setup(x => x.GetById(categoryId)).Returns(new Category() { Id = categoryId });

            var response = _productServiceInstance.Create(productName, price, stock, userId, categoryId);
            Assert.True(response == false);
        }
    }
}