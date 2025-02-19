using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helper
{
    public class WebhookKeyHelper
    {
        public static readonly int CreateProduct = 1;
        public static readonly int UpdateProduct = 2;
        public static readonly int DeleteProduct = 3;
        public static readonly int UpdateStock = 4;
        public static readonly int CreateCategory = 5;
        public static readonly int UpdateCategory = 6;
        public static readonly int UpdateProductCategory = 9;

        public static readonly int UpdateUser = 8;
        public static readonly int DeleteCategory = 10;
        public static readonly int CreateAddress = 11;
        public static readonly int UpdateAddress = 12;
        public static readonly int DeleteAddress = 13;

    }

    public class WebhookTypeHelper
    {
        public static readonly int GlobalWebhook = 1;
        public static readonly int InventoryWebhook = 2;


    }

    public class WebhookLogHelper
    {
        public static readonly string ResponseCodeError = "internal webhook error";

    }
    
}
