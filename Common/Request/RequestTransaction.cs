namespace Common.Request
{
    public class RequestSubmitTransactions
    {
        public List<RequestSubmitTransaction> Transaction { get; set; } = new();
    }

    public partial class RequestSubmitTransaction
    {
        public required string UserGUID { get; set; }

        public int ProductId { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

        public double Total { get; set; }
    }
}