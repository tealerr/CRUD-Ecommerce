namespace Common.Request
{
    public class RequestAddItemToCart
    {
        public required int ProductId { get; set; }
        public required int Quantity { get; set; }
    }
}

