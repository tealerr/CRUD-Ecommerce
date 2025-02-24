namespace Common.Request
{
    public class RequestAddItemToCart
    {
        public required string UserGUID { get; set; }
        public required int ProductId { get; set; }
        public required int Quantity { get; set; }
    }
}

