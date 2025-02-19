using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static ZortCommon.Helpers.ZortHelper;

namespace ZortCommon.Helpers
{
    public class ZortOrderResponse
    {
        public ZortResponse Res { get; set; }
        public List<ZortOrder> List { get; set; }
        public int Count { get; set; }
    }
    public class ZortOrder
    {
        public string Shippingaddress { get; set; }
        public string Shippingphone { get; set; }
        public string Shippingemail { get; set; }
        public string Shippingpostcode { get; set; }
        public string Shippingprovince { get; set; }
        public string Shippingdistrict { get; set; }
        public string Shippingsubdistrict { get; set; }
        public string Trackingno { get; set; }
        public DateTime? Orderdate { get; set; }
        public string OrderdateString { get; set; }
        public string Paymentmethod { get; set; }
        public string Shippingname { get; set; }
        public float? Paymentamount { get; set; }
        public string Description { get; set; }
        public string Discount { get; set; }
        public float? Discountamount { get; set; }
        public float? Voucheramount { get; set; }
        public float? Platformdiscount { get; set; }
        public float? Sellerdiscount { get; set; }
        public string Saleschannel { get; set; }
        public List<ZortOrderList> List { get; set; }
        public float? Vatpercent { get; set; }
        public List<ZortPayment> Payments { get; set; }
        public string Createdby { get; set; }
        public string Createusername { get; set; }
        public string Createuserid { get; set; }
        public DateTime? Paymentdate { get; set; }
        public string ShippingdateString { get; set; }
        public DateTime? Shippingdate { get; set; }
        public float? Shippingamount { get; set; }
        public int? Id { get; set; }
        public int? Ordertype { get; set; }
        public string Number { get; set; }
        public string Customerid { get; set; }
        public string Customername { get; set; }
        public string Customercode { get; set; }
        public string Customeridnumber { get; set; }
        public string Customeremail { get; set; }
        public string Customerphone { get; set; }
        public string Customeraddress { get; set; }
        public string Customerbranchname { get; set; }
        public string Customerbranchno { get; set; }
        public string Cacebookname { get; set; }
        public string Facebookid { get; set; }
        public string Reference { get; set; }
        public string Warehousecode { get; set; }
        public string Status { get; set; }
        public string Paymentstatus { get; set; }
        public string Marketplacename { get; set; }
        public string Marketplaceshippingstatus { get; set; }
        public string Marketplacepayment { get; set; }
        public float? Amount { get; set; }
        public float? Vatamount { get; set; }
        public float? Shippingvat { get; set; }
        public string Shippingchannel { get; set; }
        public string Sharelink { get; set; }
        public bool? IsCOD { get; set; }
        public object Tag { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string CreateDateTimeString { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public string UpdateDateTimeString { get; set; }
        public string ExpireDate { get; set; }
        public string ExpireDateString { get; set; }
        public object TrackingList { get; set; }
        /// <summary>
        /// Seller Channel ( Shopee, Lazada )
        /// </summary>
        public string IntegrationName { get; set; }
        /// <summary>
        /// Seller Shop Id
        /// </summary>
        public string IntegrationShop { get; set; }
        public string IntegrationCustomer { get; set; }
        public string IntegrationCustomerId { get; set; }
        public decimal totalproductamount { get; set; }
    }
    public class ZortOrderList
    {
        public int? Id { get; set; }
        public string Productid { get; set; }
        public string Sku { get; set; }
        public string Name { get; set; }
        public int? Number { get; set; }
        public string Unittext { get; set; }
        public float? Pricepernumber { get; set; }
        public string Discount { get; set; }
        public float? Discountamount { get; set; }
        public float? Totalprice { get; set; }
        public int? Producttype { get; set; }
        public int? bundleid { get; set; }
        public int? bundleitemid { get; set; }
        public int? bundlenumber { get; set; }
        public string bundleCode { get; set; }
        public string bundleName { get; set; }
        public string integrationItemId { get; set; }
        public string integrationVariantId { get; set; }
        public object[] Serialnolist { get; set; }
        public int? Skutype { get; set; }
    }
    public class ZortPayment
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public float? Amount { get; set; }
        public DateTime? Paymentdatetime { get; set; }
        public string PaymentdatetimeString { get; set; }
        public string Paymentmethodid { get; set; }
        public string Link { get; set; }
    }
}