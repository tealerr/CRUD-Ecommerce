namespace Common.Responses
{
    public class ResponseItemsInCart
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double Total { get; set; }
        public string? ImageUrl { get; set; }
    }
}

