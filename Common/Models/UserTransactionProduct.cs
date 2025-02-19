using System;
using System.Collections.Generic;

namespace Common.Models
{
    public partial class UserTransactionProduct
    {
        public int Id { get; set; }

        public int UserTransactionId { get; set; }

        public int ProductId { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

        public double Total { get; set; }
    }
}


