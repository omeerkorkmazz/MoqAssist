namespace MoqAssist.UnitTests.Business.Categories
{
    public class CategoryService : ICategoryService
    {
        public CategoryService() { }
        public Category GetById(int id)
        {
            if (id < 1) return null;
            return new Category() { Id = id, Name = "Computers" };
        }
    }
}