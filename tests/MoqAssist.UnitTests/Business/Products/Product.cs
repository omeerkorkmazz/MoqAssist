namespace MoqAssist.UnitTests.Business.Products
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        public int SellerId { get; set; }
    }
}