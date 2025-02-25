namespace Common.Request
{
    public class RequestSubmitTransactions
    {
        public List<RequestSubmitTransaction> Transaction { get; set; } = new();
    }

    public partial class RequestSubmitTransaction
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}