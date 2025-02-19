using Dapper;
using MongoDB.Driver;
using MySql.Data.MySqlClient;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Common
{
    public static class ConnectionStrings
    {
        public static string? DefaultConnection { get; set; }
        public static string? ImageDomain { get; set; }
        public static string? DomainName { get; set; }
        public static string? DomainNameBrand { get; set; }
        public static string? DomainNameCMG { get; set; }
        public static string? ClientDomain { get; set; }
        public static string? MobileDomain { get; set; }
        public static string? RootPath { get; set; }
        public static string? BrandName { get; set; }
        public static string? ContentRootPath { get; set; }
        public static string? ProcessApi { get; set; }
        public static string? UrlHost { get; set; }
        public static string? ApiPath { get; set; }
        public static string? ApiKeyCMG { get; set; }
        public static string? ApiKeyBrand { get; set; }
        public static string? PdpaUsername { get; set; }
        public static string? PdpaPassword { get; set; }
        public static string? PathPdpa { get; set; }
        public static string? PdpaKey { get; set; }
        public static string? SmtpEmail { get; set; }
        public static string? SmtpPassword { get; set; }
        public static string? SmtpToEmail { get; set; }
        public static string? SendgridApikey { get; set; }
        public static string? SendgridMailSender { get; set; }
        public static string? AppId { get; set; }
        public static string? RestApiKey { get; set; }
        public static string? MailForgetPassword { get; set; }
        public static string? MailResetPassword { get; set; }
        public static string? PathImage { get; set; }
        public static string? StampKey { get; set; }
        public static string? PathWeb { get; set; }
        public static string? StampPath { get; set; }
        public static string? AutomationApi { get; set; }
        public static string? ApiKey { get; set; }
        public static string? ImportRegisterApi { get; set; }
        public static string? BearerKey { get; set; }
        public static string? SenderMailAddress { get; set; }
        public static string? SenderMailPassword { get; set; }
        public static string? SenderMailName { get; set; }
        public static string? MiMPath { get; set; }
        public static string? MiMKey { get; set; }
        public static string? AutomationPath { get; set; }
        public static string? AutomationKey { get; set; }
        public static string? wwwroot { get; set; }
        public static string? OTPSender { get; set; }
        public static string? OTPUser { get; set; }
        public static string? themeLogoDomain { get; set; }
        public static string? OTPPass { get; set; }
        public static string? Ah1Id { get; set; }
        public static string? Ah1Name { get; set; }
        public static string? MessageSecretKey { get; set; }
        public static string? LineSender { get; set; }
        public static string? MessageSender { get; set; }
        public static string? MessageSenderAdminPath { get; set; }
        public static string? AutoImportTransactionPath { get; set; }
        public static string? AutoImportCustomerPath { get; set; }
        public static string? BoothApi { get; set; }
        public static string? Ah4Id { get; set; }
        public static string? LineMesageApiPath { get; set; }
        public static string? LineMesageApiToken { get; set; }
        public static string? ImageLineAccept { get; set; }
        public static string? ImageLineReject { get; set; }
        public static string? ImageLineMessage { get; set; }
        public static string? GetRoundAutomationAPI { get; set; }
        public static string? AdminAPI { get; set; }
        public static string? LifeCycleQueue { get; set; }
        public static string? AhNo { get; set; }
        public static readonly string WebAPISecretKey = "ClarinsSpa JWT Token for Client Website";
        public static readonly string WebCMGAPISecretKey = "ClarinsSpa JWT Token for CMG Website";
        public static string? AnnonymousUserGuid { get; set; }
        public static string? AnnonymousTelephone { get; set; }
        public static string? LazadaAppKey { get; set; }
        public static string? LazadaAppSecret { get; set; }
        public static string? BrandCode { get; set; }
        public static string? MongoDatabase { get; set; }
        public static string? TransactionCodePath { get; set; }
        public static string? RedeemPath { get; set; }
        public static string? ShopeeHost { get; set; }
        public static string? ShopeePath { get; set; }
        public static string? ShopeePathV2 { get; set; }
        public static string? ShopeePartnerId { get; set; }
        public static string? ShopeePartnerKey { get; set; }
        public static string? MiddlewareMarketPlacePath { get; set; }
        public static string? MongoConnection { get; set; }
        public static string? PathReceiptScanner { get; set; }
        public static string? ReceiptScannerApikey { get; set; }
        public static string? StorehubApi { get; set; }
        public static string? BrandSecretToken { get; set; }
        public static string? AddressApi { get; set; }
        public static string? TiktokAppKey { get; set; }
        public static string? TiktokAppSecret { get; set; }
        public static string? SQLConnectionString { get; set; }
        public static string? IpstEcomBranchId { get; set; }
        public static string? IpstProductTypeCustomFieldId { get; set; }
        public static string? ImageDomainIPST { get; set; }
        public static string? AdminDomain { get; set; }
        public static string? ImageBanner { get; set; }
        public static string? ServiceDomain { get; set; }
    }
    public class DBConnection
    {
        public QueryFactory Connect()
        {
            var connection = new MySqlConnection(ConnectionStrings.DefaultConnection);
            var compiler = new MySqlCompiler();

            QueryFactory queryFactory = new QueryFactory(connection, compiler, 500);
            queryFactory.Logger = compiled =>
            {
                Console.WriteLine(compiled.ToString());
                Debug.WriteLine(compiled.ToString());
            };
            DefaultTypeMap.MatchNamesWithUnderscores = true;

            return queryFactory;
        }

        public QueryFactory ConnectSpecificDb(string DefaultConnection)
        {
            var connection = new MySqlConnection(DefaultConnection);
            var compiler = new MySqlCompiler();

            QueryFactory queryFactory = new QueryFactory(connection, compiler, 500);
            queryFactory.Logger = compiled =>
            {
                Console.WriteLine(compiled.ToString());
                Debug.WriteLine(compiled.ToString());
            };
            DefaultTypeMap.MatchNamesWithUnderscores = true;

            return queryFactory;
        }

        public MySqlConnection DapperConnection()
        {
            MySqlConnection Connection = new MySqlConnection(ConnectionStrings.DefaultConnection);
            return Connection;
        }
        public MongoClient MongoDBConnection()
        {
            MongoClientSettings settings = MongoClientSettings.FromConnectionString(ConnectionStrings.MongoConnection);
            settings.MaxConnectionIdleTime = TimeSpan.FromSeconds(30);

            MongoClient client = new MongoClient(settings);
            return client;
        }
        public QueryFactory SQLConnect()
        {
            var connection = new SqlConnection(ConnectionStrings.SQLConnectionString);
            var compiler = new SqlServerCompiler();

            QueryFactory queryFactory = new QueryFactory(connection, compiler, 500);
            queryFactory.Logger = compiled =>
            {
                //Console.WriteLine(compiled.ToString());
                //Debug.WriteLine(compiled.ToString());
            };
            DefaultTypeMap.MatchNamesWithUnderscores = true;

            return queryFactory;
        }

    }
}
