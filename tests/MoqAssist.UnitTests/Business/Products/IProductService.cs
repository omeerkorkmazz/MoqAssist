namespace MoqAssist.UnitTests.Business.Products
{
    public interface IProductService
    {
        bool Create(string productName, decimal price, int stock, int userId, int categoryId);
    }
}