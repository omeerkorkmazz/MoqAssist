using MoqAssist.UnitTests.Business.Categories;
using MoqAssist.UnitTests.Business.Users;

namespace MoqAssist.UnitTests.Business.Products
{
    public class ProductService : IProductService
    {
        private readonly IUserService _userService;
        private readonly ICategoryService _categoryService;
        public ProductService(IUserService userService, ICategoryService categoryService)
        {
            _userService = userService;
            _categoryService = categoryService;
        }
        public bool Create(string productName, decimal price, int stock, int userId, int categoryId)
        {
            var user = _userService.GetById(userId);
            if (user == null) return false;

            var category = _categoryService.GetById(categoryId);

            if (category == null) return false;

            //Assume we created a product successfully, if conditions are verified!
            return true;
        }
    }
}