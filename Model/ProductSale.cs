namespace productApi.Model
{
    public class ProductSale
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public string? ProductName { get; set; }
        public int? SaleQuontity { get; set; }
    }
}
