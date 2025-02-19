using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Common.Models.Zorts;
using Common.Repository;
using SendGrid.Helpers.Mail;
using SendGrid;
using Common.Repositories;
using Common.Responses;
using Common.Helper;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Spire.Doc;
using Microsoft.AspNetCore.Mvc;
using Common.Models;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Mysqlx.Crud;
using Microsoft.IdentityModel.Tokens;

namespace ZortCommon.Helpers
{
    public static class ZortHelper
    {
        static HttpClient client = new HttpClient();
        static readonly int _plugInType = PlugInTypeHelper.Zort;
        //static readonly ZortConfig _config = ZortRepository.GetConfig();
        static readonly PluginResponse _config = new PluginRepository().GetPluginById(_plugInType);
        public static Dictionary<string, string> statusToStatusIdDict = new Dictionary<string, string>(){
            {"0", "0"},
            {"1", "1"},
            {"2", "2"},
            {"3", "3"},
            {"pending", "0"},
            {"success", "1"},
            {"voided", "2"},
            {"waiting", "3"},
            {"packed", "5"},
        };
        public static Dictionary<string, string> paymentStatusToPaymentStatusIdDict = new Dictionary<string, string>(){
            {"0", "0"},
            {"1", "1"},
            {"2", "2"},
            {"3", "3"},
            {"4", "4"},
            {"pending", "0"},
            {"paid", "1"},
            {"voided", "2"},
            {"partial", "3"},
            {"overpaid", "4"},
        };
        public static string HelloWorld()
        {
            return "Hello World!!";
        }
        public class MARKETPLACESHOP
        {
            public string sellerid { get; set; }
            public string channel { get; set; }
        }

        public static async Task<List<MarketPlaceShop>> GetMarketPlaceShopByProductSkuAsync(string sku)
        {
            try
            {
                HttpRequestMessage request = SetRequest(GetMarketPlaceShopUrlBySku(sku), HttpMethod.Get);

                HttpResponseMessage response = await client.SendAsync(request);
                
                response.EnsureSuccessStatusCode();

                List<MarketPlaceShop> shops = new List<MarketPlaceShop>();
                if (response.IsSuccessStatusCode)
                {
                    shops = JsonConvert.DeserializeObject<List<MarketPlaceShop>>(await response.Content.ReadAsStringAsync());
                }

                return shops;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                return null;
            }
        }
        public class StockList
        {
            public string sku { get; set; }
            public int stock { get; set; }
            public double cost { get; set; }
        }
        public class UpdateProductStockListResponse
        {
            public string resCode { get; set; }
            public string resDesc { get; set; }
            public string resDesc2 { get; set; }
            public String resDesc3 { get; set; }
            public UpdateProductStockDetailResponse detail { get; set; }
        }
        public class UpdateProductStockDetailResponse
        {
            public int? id { get; set; }
            public int? number { get; set; }
            public String link { get; set; }
            public String link2 { get; set; }
            public String shippingchannel { get; set; }
            public String trackingno { get; set; }
            public String message { get; set; }
            public String result { get; set; }
            public StockUpdateResponse stockupdate { get; set; }
        }
        public class StockUpdateResponse
        {
            public int success { get; set; }
            public int fail { get; set; }
            public int notupdate { get; set; }
            public List<StockUpdateResultListResponse> successlist { get; set; }
            public List<StockUpdateResultListResponse> faillist { get; set; }
            public List<StockUpdateResultListResponse> notupdatelist { get; set; }
        }
        public class StockUpdateResultListResponse
        {
            public int? productid { get; set; }
            public String sku { get; set; }
            public int stock { get; set; }
            public string skutype { get; set; }
            public int cost { get; set; }
        }
        public class UpdateProductMarketPlace
        {
            public string sku { get; set; }
            public string name { get; set; }
            public string category { get; set; }
            public string sellprice { get; set; }
            public string weight { get; set; }
            public string height { get; set; }
            public string length { get; set; }
            public string width { get; set; }
            public string description { get; set; }
        }
        public class MarketPlaceShop
        {
            public string sellerid { get; set; }
            public string channel { get; set; }
            public int saleChannelId
            {
                get
                {
                    return SalesChannelHelper.GetExternalSalesChannelIdByName(channel);
                }
            }
        }
        public class PRODUCTDETAILBYMARKETPLACE
        {
            public int id { get; set; }
            public String producttype { get; set; }
            public String name { get; set; }
            public String description { get; set; }
            public String sku { get; set; }
            public String sellprice { get; set; }
            public String purchaseprice { get; set; }
            public String barcode { get; set; }
            public String stock { get; set; }
            public String availablestock { get; set; }
            public String unittext { get; set; }
            public String imagepath { get; set; }
            public String weight { get; set; }
            public String height { get; set; }
            public String length { get; set; }
            public String width { get; set; }
            public String categoryid { get; set; }
            public String category { get; set; }
            public String variationid { get; set; }
            public String variant { get; set; }
            public List<String> tag { get; set; }
            public String sharelink { get; set; }
        }
        public class FinanceMarketPlaceBase
        {
            public Decimal amount { get; set; }
            public Decimal totalproductamount { get; set; }
            public Decimal paidamount { get; set; }
            public Decimal shipping_paidby_customer { get; set; }
            public Decimal shipping_paidby_platform { get; set; }
            public Decimal shipping_actual { get; set; }
            public Decimal commission { get; set; }
            public Decimal paymentfee { get; set; }
            public Decimal otherfee { get; set; }
        }
        public class FinanceMarketPlace : FinanceMarketPlaceBase
        {
            [JsonIgnore]
            public int id { get; set; }
            public string sellerid { get; set; }
            public string channel { get; set; }
            public string name { get; set; }
            public string number { get; set; }
            public string statement { get; set; }

            public List<FinanceMarketPlaceOrder> list { get; set; }
            public int salesChannelId
            {
                get
                {
                    return SalesChannelHelper.GetExternalSalesChannelIdByName(channel);
                }
            }
            public DateTime statementDate
            {
                get
                {
                    // e.g. 10 Mar 2022-> dd MMMM yyyy -> to Date local
                    var value = name;
                    // get date -> result
                    var result = Convert.ToDateTime(value);
                    return result;
                }
            }
        }
        public class FinanceMarketPlaceOrder : FinanceMarketPlaceBase
        {
            public string ordernumber { get; set; }
            public string orderDate { get; set; }
            public DateTime orderDateByString
            {
                get
                {
                    // e.g. /Date(1632243600000)/ -> /Date(milliseconds)/ -> 1632243600000 -> DateTime -> local Date
                    var value = orderDate;
                    var millisecondsValue = value.Replace("/Date(", "").Replace(")/", "");
                    long milliseconds = long.Parse(millisecondsValue);
                    // get date result
                    DateTime result = DateTimeOffset.FromUnixTimeMilliseconds(milliseconds).ToLocalTime().DateTime;
                    return result.Date;
                }
            }
        }
        public class ZortResponse
        {
            public string resCode { get; set; }
            public String resDesc { get; set; }
            public String resDesc2 { get; set; }
            public String resDesc3 { get; set; }
            public object? detail { get; set; }
        }
        public class ZortResponseFull
        {
            public ZortResponse res { get; set; }
        }

        public class ZortTracking
        {
            public string trackingno { get; set; }
            public string trackingurl { get; set; }
            public DateTime shippingdate { get; set; }

        }
        public class ZortTrackingResponse
        {
            public string trackingno { get; set; }
            public string trackingurl { get; set; }
            public DateTime? shippingdate { get; set; }


        }
        
        public class ZortExpenseDetail
        {
            public long id { get; set; }
            public string name { get; set; }
            public string paymentname { get; set; }
            public decimal amount { get; set; }
            public string paymentdatetimeString { get; set; }
            public int paymentchannelid { get; set; }
            public bool isvat { get; set; }
            public DateTime paymentDateTime
            {
                get
                {
                    var dateTime = DateTimeOffset.Parse(this.paymentdatetimeString).DateTime;
                    return dateTime;
                }
            }
        }
        public class ZortVoucherDetail
        {
            public long id { get; set; }
            public string name { get; set; }
            public decimal amount { get; set; }
        }
        public class MarketPlaceFile
        {
            public String content { get; set; }
            public String type { get; set; }

        }
        public class ReadyToShipResponse : ZortResponse
        {
            public new ReadyToShipResponseDetail detail { get; set; }
        }
        public class ReadyToShipResponseDetail
        {
            public int id { get; set; }
            public string number { get; set; }
            public string link { get; set; }
            public string link2 { get; set; }
            public string shippingchannel { get; set; }
            public string trackingno { get; set; }
        }
        private static string GenerateStockListUpdateToJsonString(List<StockList> stocks)
        {
            return "{" +
                "\"stocks\" : [ " + string.Join(",", stocks.Select(s => GenerateStockUpdateToJsonString(s)).ToList()) + "]"
                + "}";
        }
        private static string GenerateStockUpdateToJsonString(StockList item)
        {
            return "{ "
                + "\"sku\" : \"" + item.sku + "\","
                + "\"stock\" : " + item.stock.ToString() + ","
                + "\"cost\": " + item.cost.ToString()
                + "}";
        }
        private static string GenerateProductUpdateToJsonString(UpdateProductMarketPlace item)
        {
            var result = "{ "
                // required sku
                + $"\"sku\":\"{item.sku}\"";
            if (!string.IsNullOrEmpty(item.name))
                result += $",\"name\":\"{item.name}\"";
            if (!string.IsNullOrEmpty(item.category))
                result += $",\"category\": \"{item.category}\"";
            if (!string.IsNullOrEmpty(item.sellprice))
                result += $",\"sellprice\":\"{item.sellprice}\"";
            if (!string.IsNullOrEmpty(item.weight))
                result += $",\"weight\": \"{item.weight}\"";
            if (!string.IsNullOrEmpty(item.height))
                result += $",\"height\":\"{item.height}\"";
            if (!string.IsNullOrEmpty(item.length))
                result += $",\"length\":\"{item.length}\"";
            if (!string.IsNullOrEmpty(item.width))
                result += $",\"width\":\"{item.width}\"";
            if (!string.IsNullOrEmpty(item.description))
            {
                string pattern = "(?<=\\<[^<>]*)\"(?=[^><]*\\>)";
                string output = Regex.Replace(item.description, pattern, "'");
                output = output.Replace(@"""", @"\""");

                result += $",\"description\": \"{output}\"";

            }

            result += "}";

            return result;
        }
        private static string GenerateProductImageUrlToJsonString(string sku)
        {
            return "{ "
               + "\"sku\" : \"" + sku + "\""
               + "}";
        }
        public static async Task<PRODUCTDETAILBYMARKETPLACE> GetProductDetailByMarketPlace(string sku, string channel, string sellerId)
        {
            try
            {
                HttpRequestMessage request = SetRequest(GetProductDetailByMarketPlaceUrl(sku, sellerId, channel), HttpMethod.Get);

                HttpResponseMessage response = await client.SendAsync(request);

                response.EnsureSuccessStatusCode();

                var result = new PRODUCTDETAILBYMARKETPLACE();
                if (response.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<PRODUCTDETAILBYMARKETPLACE>(await response.Content.ReadAsStringAsync());
                }

                return result ?? new PRODUCTDETAILBYMARKETPLACE(); ;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                return new PRODUCTDETAILBYMARKETPLACE(); ;
            }
        }


        public static async Task<MarketPlaceFile> GetMarketPlaceDoc(string InvoiceNO)
        {
            try
            {
                HttpRequestMessage request = SetRequest(GetMarketPlaceFile(InvoiceNO), HttpMethod.Get);

                HttpResponseMessage response = await client.SendAsync(request);

                response.EnsureSuccessStatusCode();

                var result = new MarketPlaceFile();
                if (response.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<MarketPlaceFile>(await response.Content.ReadAsStringAsync());
                }

                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                return null;
            }
        }
        public static async Task<List<FinanceMarketPlace>> GetFinanceMarketPlaceReport(string sellerId, string channel, DateTime startDate, DateTime endDate, bool isMock = false)
        {
            try
            {
                var result = new List<FinanceMarketPlace>();
                bool isSuccess = false;
                if (!isMock)
                {
                    HttpRequestMessage request = SetRequest(GetFinanceMarketPlaceReportUrl(sellerId, channel, startDate, endDate), HttpMethod.Get);

                    HttpResponseMessage response = await client.SendAsync(request);

                    response.EnsureSuccessStatusCode();

                    isSuccess = response.IsSuccessStatusCode;

                    if (isSuccess)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<List<FinanceMarketPlace>>(await response.Content.ReadAsStringAsync());
                        _ = SaveZortFinanceMarketPlaceJson(content, sellerId, channel, startDate, endDate);
                    }
                }
                else
                {
                    var fileString = File.ReadAllText($"wwwroot/Finance/zortfinance_example.json");
                    result = JsonConvert.DeserializeObject<List<FinanceMarketPlace>>(fileString) ?? new List<FinanceMarketPlace>();
                    isSuccess = true;
                    _ = SaveZortFinanceMarketPlaceJson(fileString, sellerId, channel, startDate, endDate);
                }

                if (isSuccess)
                {
                    // group statement result by statementDate
                    result = result.GroupBy(s => s.statementDate).Select(i => new FinanceMarketPlace
                    {
                        name = i.FirstOrDefault().name,
                        channel = i.FirstOrDefault().channel,
                        sellerid = i.FirstOrDefault().sellerid,
                        number = i.FirstOrDefault().number,
                        statement = i.FirstOrDefault().statement,
                        amount = i.Sum(x => x.amount),
                        totalproductamount = i.Sum(x => x.totalproductamount),
                        paidamount = i.Sum(x => x.paidamount),
                        shipping_paidby_customer = i.Sum(x => x.shipping_paidby_customer),
                        shipping_paidby_platform = i.Sum(x => x.shipping_paidby_platform),
                        shipping_actual = i.Sum(x => x.shipping_actual),
                        commission = i.Sum(x => x.commission),
                        paymentfee = i.Sum(x => x.paymentfee),
                        otherfee = i.Sum(x => x.otherfee),
                        // arrange all order in group
                        list = i.SelectMany(x => x.list).ToList() ?? new List<FinanceMarketPlaceOrder>()
                    }).OrderBy(s => s.statementDate).ToList();

                    // _ = SaveZortFinanceMarketPlaceJson(result, sellerId, channel, startDate, endDate);
                }

                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                return null;
            }
        }
        private static bool SavePdfShippingLabel(string fileName, byte[] pdfBytes)
        {
            try
            {
                var root = "wwwroot";
                var directory = root + "/" + "shippingLabel";
                if (!Directory.Exists(root))
                    Directory.CreateDirectory(root);
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                if (!fileName.ToLower().EndsWith(".pdf"))
                    fileName = fileName + ".pdf";

                File.WriteAllBytes(directory + "/" + fileName, pdfBytes);

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                return false;
            }
        }

        /// <summary>
        ///     Get Zort Order List
        /// </summary>
        /// <param name="limit">count of list</param>
        /// <param name="page">page ( calculate from limit )</param>
        /// <param name="keyword"></param>
        /// <param name="createdafter"></param>
        /// <param name="createdbefore"></param>
        /// <param name="updatedafter"></param>
        /// <param name="updatedbefore"></param>
        /// <param name="orderdateafter"></param>
        /// <param name="orderdatebefore"></param>
        /// <param name="paymentafter"></param>
        /// <param name="paymentbefore"></param>
        /// <param name="status"></param>
        /// <param name="fromamount"></param>
        /// <param name="toamount"></param>
        /// <param name="frompaymentamount"></param>
        /// <param name="topaymentamount"></param>
        /// <returns>List of Zort Order</returns>
        public static async Task<List<ZortOrder>> GetZortOrderList(int? limit = null,
            int? page = null,
            string keyword = "",
            DateTime? createdafter = null,
            DateTime? createdbefore = null,
            DateTime? updatedafter = null,
            DateTime? updatedbefore = null,
            DateTime? orderdateafter = null,
            DateTime? orderdatebefore = null,
            DateTime? paymentafter = null,
            DateTime? paymentbefore = null,
            int? status = null,
            double? fromamount = null,
            double? toamount = null,
            double? frompaymentamount = null,
            double? topaymentamount = null
            )
        {
            var plugInRepository = new PluginRepository();
            var getOrderUrl = GetOrderListUrl(limit, page, keyword, createdafter, createdbefore, updatedafter,
                updatedbefore, orderdateafter, orderdatebefore, paymentafter, paymentbefore, status,
                fromamount, toamount, frompaymentamount, topaymentamount);
            try
            {

                HttpRequestMessage request = SetRequest(getOrderUrl, HttpMethod.Get);
                var now = DateTime.Now;
                HttpResponseMessage response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var result = new ZortOrderResponse();
                string content = string.Empty;
                if (response.IsSuccessStatusCode)
                {
                    // ZortOrderResponse
                    content = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<ZortOrderResponse>(content);

                    // protect null
                    if (result == null)
                        result = new ZortOrderResponse();
                    if (result.List == null)
                        result.List = new List<ZortOrder>();
                }

                ZortRepository.InsertLog(content, response.StatusCode, result, updatedafter.GetValueOrDefault(), now);
                plugInRepository.InsertLog(new Common.Responses.PluginLogResponse()
                {
                    plugin_type_id = _plugInType,
                    response_body = content,
                    response_code = response.StatusCode.ToString(),
                    send_body = "",
                    url = getOrderUrl,
                    created_time = now
                });
                var take = limit != 0 && limit != null ? limit.GetValueOrDefault() : 500;
                var pageNo = page != 0 && page != null ? page.GetValueOrDefault() : 1;
                if(pageNo == 1)
                {
                    plugInRepository.UpdatePlugInTypeUpdateAfterTime(_plugInType, now);
                }

                if (result.Count <= (pageNo * take))
                {
                    return result.List;
                }
                else
                {
                    var totalResult = result.List;

                    var newResult = await GetZortOrderList(limit, pageNo + 1, keyword, createdafter, createdbefore, updatedafter,
                    updatedbefore, orderdateafter, orderdatebefore, paymentafter, paymentbefore, status,
                    fromamount, toamount, frompaymentamount, topaymentamount);

                    totalResult.AddRange(newResult);
                    return totalResult;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                ZortRepository.InsertLog(ex.Message, HttpStatusCode.BadGateway, new ZortOrderResponse() { List = new List<ZortOrder>(), Res = new ZortResponse(), Count = 0}, createdafter.GetValueOrDefault(), createdbefore.GetValueOrDefault());
                plugInRepository.InsertLog(new Common.Responses.PluginLogResponse()
                {
                    plugin_type_id = _plugInType,
                    response_body = ex.Message,
                    response_code = HttpStatusCode.BadGateway.ToString(),
                    send_body = "",
                    url = getOrderUrl,
                    created_time = DateTime.Now
                });

                return null;
            }
        }
        public static async Task<List<ZortExpenseDetail>> GetZortOrderExpenseDetail(int zortOrderId)
        {
            try
            {
                var getOrderExpenseUrl = GetOrderExpenseUrl(zortOrderId);

                HttpRequestMessage request = SetRequest(getOrderExpenseUrl, HttpMethod.Get);

                HttpResponseMessage response = await client.SendAsync(request);

                response.EnsureSuccessStatusCode();

                var result = new List<ZortExpenseDetail>();
                if (response.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<List<ZortExpenseDetail>>(await response.Content.ReadAsStringAsync());
                }

                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                return null;
            }
        }
        public static async Task<List<ZortVoucherDetail>> GetZortOrderVoucherDetail(int zortOrderId)
        {
            try
            {
                var getOrderVoucherUrl = GetOrderVoucherUrl(zortOrderId);

                HttpRequestMessage request = SetRequest(getOrderVoucherUrl, HttpMethod.Get);

                HttpResponseMessage response = await client.SendAsync(request);

                response.EnsureSuccessStatusCode();

                var result = new List<ZortVoucherDetail>();
                if (response.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<List<ZortVoucherDetail>>(await response.Content.ReadAsStringAsync());
                }

                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                return null;
            }
        }
        
        private static string GetMarketPlaceShopUrlBySku(string sku)
        {
            return $"{_config.url}?method=GETMARKETPLACESHOPBYPRODUCT&version=3&format=json&sku={sku}";
        }
        private static string GetUpdateProductStock(string sellerId, string channel)
        {
            return $"{_config.url}?method=UPDATEPRODUCTSTOCKLIST&version=3&format=json&warehousecode=W0001&sellerId={sellerId}&channel={channel}";
        }
        private static string GetUpdateProductMarketPlace(string sellerId, string channel)
        {
            return $"{_config.url}?method=UPDATEPRODUCTMARKETPLACE&version=3&format=json&sellerid={sellerId}&channel={channel}";
        }
        private static string GetUpdateProductImageUrlMarketPlace()
        {
            return $"{_config.url}?method=UPDATEPRODUCTIMAGEURLMARKETPLACE&version=3&format=json";
        }
        private static string GetProductDetailByMarketPlaceUrl(string sku, string sellerId, string channel)
        {
            return $"{_config.url}?method=GETPRODUCTDETAILBYMARKETPLACE&version=3&format=json&sku={sku}&channel={channel}&sellerid={sellerId}";
        }

        private static string GetMarketPlaceFile(string invoiceNO)
        {
            return $"{_config.url}?method=GETMARKETPLACESHIPMENTLABELFILE&version=3&format=json&number={invoiceNO}";
        }
        private static string GetFinanceMarketPlaceReportUrl(string sellerId, string channel, DateTime startDate, DateTime endDate)
        {
            var result = $"{_config.url}?method=GETFINANCEMARKETPLACEREPORT&version=2&format=json&sellerid={sellerId}&channel={channel}";
            // start Date
            var cultureInfo = new CultureInfo("en-US");
            result += $"&startdate={startDate.ToString("yyyy-MM-dd", cultureInfo)}";
            // end Date
            result += $"&enddate={endDate.ToString("yyyy-MM-dd", cultureInfo)}";

            return result;
        }
        private static string ReadyToShipUrl(string number, string shipment, string address)
        {
            var requestUrl = $"{_config.url}?method=READYTOSHIP&version=2&format=json&number={number}&shipment={shipment}";
            if (!string.IsNullOrEmpty(address))
                requestUrl += $"&address={address}";

            return requestUrl;
        }
        private static string PackOrderUrl(string number, string shipment, string trackingNo)
        {
            var requestUrl = $"{_config.url}?method=PACKORDER&version=3&format=json&number={number}&shipment={shipment}";
            if (!string.IsNullOrEmpty(trackingNo))
                requestUrl += $"&address={trackingNo}";

            return requestUrl;
        }
        private static string GetOrderListUrl(int? limit = null,
            int? page = null,
            string keyword = "",
            DateTime? createdafter = null,
            DateTime? createdbefore = null,
            DateTime? updatedafter = null,
            DateTime? updatedbefore = null,
            DateTime? orderdateafter = null,
            DateTime? orderdatebefore = null,
            DateTime? paymentafter = null,
            DateTime? paymentbefore = null,
            int? status = null,
            double? fromamount = null,
            double? toamount = null,
            double? frompaymentamount = null,
            double? topaymentamount = null)
        {
            var cultureInfo = new CultureInfo("en-US");
            // base request url
            
            var result = $"{_config.url}?method=GETORDERS&version=3&format=json";

            if (IsUrlV4)
                result = $"{_config.url}/Order/GetOrders?";
            // filter query
            if (limit != null) result += $"&limit={limit.Value}";
            if (page != null) result += $"&page={page.Value}";
            if (status != null) result += $"&status={status.Value}";

            if (!string.IsNullOrEmpty(keyword)) result += $"&keyword={keyword}";

            //if (createdafter != null) result += $"&createdafter={createdafter.Value.ToString("yyyy-MM-dd", cultureInfo)}";
            //if (createdbefore != null) result += $"&createdbefore={createdbefore.Value.ToString("yyyy-MM-dd", cultureInfo)}";
            if (updatedafter != null) result += $"&updatedatetimeafter={updatedafter.Value.ToString("yyyy-MM-dd HH:mm", cultureInfo)}";
            //if (updatedbefore != null) result += $"&updatedatetimebefore={updatedbefore.Value.ToString("yyyy-MM-dd HH:mm", cultureInfo)}";
            if (orderdateafter != null) result += $"&orderdateafter={orderdateafter.Value.ToString("yyyy-MM-dd", cultureInfo)}";
            if (orderdatebefore != null) result += $"&orderdatebefore={orderdatebefore.Value.ToString("yyyy-MM-dd", cultureInfo)}";
            if (paymentafter != null) result += $"&paymentafter={paymentafter.Value.ToString("yyyy-MM-dd", cultureInfo)}";
            if (paymentbefore != null) result += $"&paymentbefore={paymentbefore.Value.ToString("yyyy-MM-dd", cultureInfo)}";

            if (fromamount != null) result += $"&fromamount={fromamount.Value.ToString("N2")}";
            if (toamount != null) result += $"&toamount={toamount.Value.ToString("N2")}";
            if (frompaymentamount != null) result += $"&frompaymentamount={frompaymentamount.Value.ToString("N2")}";
            if (topaymentamount != null) result += $"&topaymentamount={topaymentamount.Value.ToString("N2")}";

            if (!string.IsNullOrEmpty(_config.order_status))
            {
                var statusIdList = ConvertStringToStatusIdList(_config.order_status, statusToStatusIdDict);
                if (statusIdList.Count > 0)
                {
                    result += $"&status={string.Join(",", statusIdList)}";
                }
            }
            if (!string.IsNullOrEmpty(_config.order_paymentstatus))
            {
                var statusIdList = ConvertStringToStatusIdList(_config.order_paymentstatus, paymentStatusToPaymentStatusIdDict);
                if (statusIdList.Count > 0)
                {
                    result += $"&paymentstatus={string.Join(",", statusIdList)}";
                }
            }


            return result;
        }
        private static string GetOrderExpenseUrl(int zortOrderId)
        {
            var result = $"{_config.url}?method=GETEXPENSEDETAIL&version=3&format=json&id={zortOrderId}";
            return result;
        }
        private static string GetOrderVoucherUrl(int zortOrderId)
        {
            var result = $"{_config.url}?method=GETVOUCHERDETAIL&version=3&format=json&id={zortOrderId}";
            return result;
        }
        private static string GetUpdateProductSalesPriceMarketPlaceUrl(string sellerId,
            string channel,
            decimal salesprice,
            DateTime startDateTime,
            DateTime endDateTime,
            int stock,
            string promotionname)
        {
            var result = $"{_config.url}?method=UPDATEPRODUCTSALESPRICEMARKETPLACE&version=3&format=json" +
                $"&sellerid={sellerId}" +
                $"&channel={channel}" +
                $"&salesprice={salesprice}" +
                $"&startdate={startDateTime.ToString("yyyy-MM-dd HH:mm")}" +
                $"&enddate={endDateTime.ToString("yyyy-MM-dd HH:mm")}" +
                $"&stock={stock}&promotionname={promotionname}";
            return result;
        }
        private static HttpRequestMessage SetRequest(string requestUri, HttpMethod method, PluginResponse config = null)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                RequestUri = new Uri(requestUri),
                Method = method,
            };
            if (config == null)
            {
                request.Headers.Add("storename", _config.storename);
                request.Headers.Add("apikey", _config.apikey);
                request.Headers.Add("apisecret", _config.apisecret);
                return request;
            }
            else
            {
                request.Headers.Add("storename", config.storename);
                request.Headers.Add("apikey", config.apikey);
                request.Headers.Add("apisecret", config.apisecret);
                return request;
            }
        }
        /// <summary>
        ///     Check input saleChannelId is shopee or lazada
        /// </summary>
        /// <param name="salesChannelId"></param>
        /// <returns>
        /// true : saleChannelId is shopee or lazada<br/>
        /// false : saleChannelId is others
        /// </returns>
        public static bool HasSellerShop(int salesChannelId)
        {
            return salesChannelId == (int)SalesChannel.Shopee || salesChannelId == (int)SalesChannel.Lazada;
        }
        public static string GetExternalSaleChannelName(int salesChannelId)
        {
            if (salesChannelId == (int)SalesChannel.Shopee)
                return "shopee";
            else if (salesChannelId == (int)SalesChannel.Lazada)
                return "lazada";
            return string.Empty;
        }
        private static string FindIframeSrcFromHtmlContent(string htmlContent)
        {
            try
            {
                int startIframeIndex = htmlContent.IndexOf("<iframe");
                int endIframeIndex = htmlContent.IndexOf("</iframe>");

                var length = endIframeIndex - startIframeIndex + 1;
                var iframString = htmlContent.Substring(startIframeIndex, length);

                var srcIframeIndex = iframString.IndexOf("http");
                var srcString = iframString.Substring(srcIframeIndex);
                var lasIndex = srcString.IndexOf("\"");
                var srcLink = srcString.Substring(0, lasIndex);
                return srcLink;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                return string.Empty;
            }
        }

        private static async Task<string> SaveZortFinanceMarketPlaceJson(List<FinanceMarketPlace> datas, string sellerId, string channel, DateTime startDateTime, DateTime endDateTime)
        {
            var jsonString = JsonConvert.SerializeObject(datas);
            var cultureInfo = new CultureInfo("en-US");
            var path = $"Finance_{sellerId}_{channel}_{startDateTime.ToString("ddMMyyyy_HHmm", cultureInfo)}-{endDateTime.ToString("ddMMyyyy_HHmm", cultureInfo)}.json";
            File.WriteAllText(path, jsonString);

            return path;
        }
        private static async Task<string> SaveZortFinanceMarketPlaceJson(string content, string sellerId, string channel, DateTime startDateTime, DateTime endDateTime)
        {
            var cultureInfo = new CultureInfo("en-US");
            var path = $"Finance_{sellerId}_{channel}_{startDateTime.ToString("ddMMyyyy_HHmm", cultureInfo)}-{endDateTime.ToString("ddMMyyyy_HHmm", cultureInfo)}.json";
            File.WriteAllText(path, content);

            return path;
        }
        public static bool IsUrlV4 {  get
            {
                return _config.url.ToLower().EndsWith("/v4");
            }
        }
        public static async Task<bool> IsZortAPIAvailable(PluginResponse config = null)
        {
            try
            {
                string requestUrl = $"{_config.url}/Contact/GETCONTACTS?page=100&limit=5&keyword=TEST";
                if (config != null)
                {
                    requestUrl = $"{config.url}/Contact/GETCONTACTS?page=100&limit=5&keyword=TEST";
                }
                HttpRequestMessage request = SetRequest(requestUrl, HttpMethod.Get, config);

                HttpResponseMessage response = await client.SendAsync(request);

                response.EnsureSuccessStatusCode();

                var result = new ZortResponseFull();
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<ZortResponseFull>(content);

                    if (result.res.resCode.Trim().Equals("200"))
                    {
                        return true;
                    }

                }

                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                return false;
            }
        }
        public static List<string> ConvertStringToStatusIdList(string statusString, Dictionary<string, string> statusIdDict)
        {
            var statusList = statusString.Split(",");
            var statusIdList = statusList.Select(s => statusIdDict.GetValueOrDefault(s.Trim().ToLower())).ToList();
            statusIdList.RemoveAll(s => string.IsNullOrEmpty(s));
            return statusIdList;
        }

        private static async Task<List<ZortOrder>>  GetZortOrderForCheckingData(ZortAuth zortAuth, DateTime startDateTime,int? limit =null, int? page = null, string status =null, string paymentStatus =null)
        {
            
          
            var getOrderUrl = GetOrderListUrlforCheckingData(limit, page, startDateTime, status, paymentStatus);
            try
            {

                HttpRequestMessage request = SetRequest(getOrderUrl, HttpMethod.Get, new PluginResponse() {apikey =zortAuth.apikey,apisecret =zortAuth.apisecret,storename = zortAuth.storename });
                var now = DateTime.Now;
                HttpResponseMessage response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var result = new ZortOrderResponse();
                string content = string.Empty;
                if (response.IsSuccessStatusCode)
                {
                    // ZortOrderResponse
                    content = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<ZortOrderResponse>(content);

                    // protect null
                    if (result == null)
                        result = new ZortOrderResponse();
                    if (result.List == null)
                        result.List = new List<ZortOrder>();
                }
                var take = limit != 0 && limit != null ? limit.GetValueOrDefault() : 500;
                var pageNo = page != 0 && page != null ? page.GetValueOrDefault() : 1;
               

                if (result.Count <= (pageNo * take))
                {
                    return result.List;
                }
                else
                {
                    var totalResult = result.List;

                    var newResult = await GetZortOrderForCheckingData(zortAuth,startDateTime, limit, pageNo +1, status, paymentStatus);

                    totalResult.AddRange(newResult);
                    return totalResult;
                }
            }
            catch (Exception ex) 
            {
                return new List<ZortOrder>();
            }
        }
        public static async Task<ZortCheckingDataModel> ZortCheckingData(ZortAuth zortAuth,DateTime startDateTime,string status,string paymentStatus)
        {

            var getResult = await GetZortOrderForCheckingData(zortAuth,startDateTime, null,null,status, paymentStatus);
            var response = new ZortCheckingDataModel();
            var listIntegrationShopRes = new List<ZortIntegrationShop>();
            var listSalesChannel = new List<ZortManualSalesChannel>();
            if (getResult.Any())
            { 
                var listIntegrationName = getResult.Where(x => !x.IntegrationShop.IsNullOrEmpty()).Select(x=>x.IntegrationName).Distinct().ToList();
                foreach (var integrationName in listIntegrationName)
                {
                    ZortIntegrationShop integration = new ZortIntegrationShop();
                    integration.Name = integrationName;
                    var orderIntegration = getResult.Where(x => x.IntegrationName == integrationName).ToList();
                    integration.Quantity = orderIntegration.Count;
                    integration.IntegrationShop = orderIntegration.Select(x=>x.IntegrationShop).Distinct().ToList();
                    listIntegrationShopRes.Add(integration);
                }
                var listManualSalesChannelName = getResult.Select(x => x.Saleschannel).Distinct().ToList();
                foreach (var salesChannelName in listManualSalesChannelName)
                {
                    ZortManualSalesChannel salesChannel = new ZortManualSalesChannel();
                    salesChannel.Name = salesChannelName;
                    var orderSalesChannel = getResult.Where(x => x.Saleschannel == salesChannelName ).ToList();
                    salesChannel.Quantity = orderSalesChannel.Count;
                    listSalesChannel.Add(salesChannel);
                }

                response.ListManualSalesChannel = listSalesChannel;
                response.ListIntregationShop = listIntegrationShopRes;
            }
            return response;
        }
        private static string GetOrderListUrlforCheckingData(int? limit = null,
    int? page = null,
    DateTime? updatedafter = null,
    string status = null,
    string paymentStatus =null)
        {
            var cultureInfo = new CultureInfo("en-US");
            // base request url
            var result = $"{_config.url}?method=GETORDERS&version=3&format=json";
            if (IsUrlV4)
                result = $"{_config.url}/Order/GetOrders?";
            // filter query
            if (limit != null) result += $"&limit={limit.Value}";
            if (page != null) result += $"&page={page.Value}";
            if (status != null) result += $"&status={status}";
            if (paymentStatus != null) result += $"&paymentstatus={paymentStatus}";
            if (updatedafter != null) result += $"&updatedatetimeafter={updatedafter.Value.ToString("yyyy-MM-dd HH:mm", cultureInfo)}";
            return result;
        }

    }
}
