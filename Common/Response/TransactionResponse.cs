using Common.Models;

namespace Common.Responses
{
    public class TransactionResponse
    {
        public int UserTransactionId { get; set; }

        public double GrandTotal { get; set; }

        public DateTime CreatedTime { get; set; }

        public List<UserTransactionProductResponse> TransactionDetails { get; set; }
    }

    public class UserTransactionProductResponse
    {
        public string ProductName { get; set; } = null!;

        public double ProductPrice { get; set; }

        public int Quantity { get; set; }

        public double Total { get; set; }
    }
}