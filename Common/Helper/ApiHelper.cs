using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Helper
{
    public class ApiHelper
    {
        public static readonly string HEADER = "application/json";
        public static readonly string API_KEY_TRANSACTION_CODE = "CRMCODE1234!";
        public static readonly string API_KEY_REDEEM = "CEGAChAtA7Ds40p";
        public static string REDEEM_KEY = "";

        public static readonly string TRANSACTION_CODE_CHECK_CODE = "/checkcode";
        public static readonly string TRANSACTION_CODE_USE_CODE = "/usecode";
        public static readonly string REDEEM_TRANSACTIONCODE = "/transactioncode/redeemtransactioncode";
        public static readonly string REDEEM_ITEM = "/redeem/redeem_item";
    }
}
