namespace productApi.Model
{
    public class ProductPurchase
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public string? ProductName { get; set; }
        public int? PurchaseQuontity { get; set; }
    }
}
