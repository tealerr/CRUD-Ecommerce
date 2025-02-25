namespace Common.Helper
{
    public class CodeHelper
    {
        public static readonly int SUCCESS = 1;
        public static readonly int FAIL = 0;
        public static readonly int OUT_OF_BOUND = -1;
        public static readonly string CENTER_POINT = "1";

        public static readonly int PrefixType = 1;

        public static readonly int ROLETYPEADMIN = 1;

        public static readonly int STATUS_CANCEL = 6;
        public static readonly int TRANSACTION_DELETE = 4;

        public static readonly int PICK_UP_AT_BRANCH = 1;
        public static readonly int PICK_UP_AT_HOME = 2;

        public static readonly int RECEIPT_VALID_TYPE_SPECIFIC_DATE = 1;
        public static readonly int RECEIPT_VALID_TYPE_PAST_DAY = 2;

        public static readonly int USER_RECEIPT_SCANNER_STATUS_PROGRESS = 1;
        public static readonly int USER_RECEIPT_SCANNER_STATUS_ADMIN_PROGRESS = 2;
        public static readonly int USER_RECEIPT_SCANNER_STATUS_SUCCESS = 3;
        public static readonly int USER_RECEIPT_SCANNER_STATUS_FAIL = 4;

        public static readonly int TICKET_WEBHOOK_ACTION_CREATE = 1;
        public static readonly int TICKET_WEBHOOK_ACTION_UPDATE = 2;
        public static readonly int TICKET_WEBHOOK_ACTION_RESOLVE = 3;
        public static readonly int TICKET_WEBHOOK_ACTION_CLOSE = 4;

        public static readonly int BLIND_FULL = 3;
        public static readonly int BLIND_NAME = 2;


        public static readonly string SEGMENT_IDENTITY = "identify";
        public static readonly string SEGMENT_TRACK = "track";
        public static readonly string SEGMENT_EVENT_REGISTRATION = "Registration";
        public static readonly string SEGMENT_EVENT_UPDATE = "Update";
        public static readonly string SEGMENT_EVENT_POINT_EARN = "Point Earn";
        public static readonly string SEGMENT_EVENT_POINT_REDEEM = "Point Redeem";
        public static readonly string SEGMENT_EVENT_POINT_VOID = "Point Void";
        public static readonly string SEGMENT_EVENT_POINT_RETURN = "Point Return";
        public static readonly string SEGMENT_EVENT_PURCHASE = "Purchase";
        public static readonly string SEGMENT_EVENT_PURCHASE_ITEM = "Purchase Item";
        public static readonly string SEGMENT_EVENT_PURCHASE_DELETE = "Purchase Deleted";
        public static readonly string SEGMENT_ACTION_PLACED = "placed";
        public static readonly string SEGMENT_ACTION_RETURN = "return";
        public static readonly string SEGMENT_ACTION_VOID = "void";

        public static readonly int AUTOMATION_ACTION_REDEEM_PRIVILEGE = 1;
        public static readonly int AUTOMATION_ACTION_POINT = 2;
        public static readonly int AUTOMATION_ACTION_REDEEM_MISSION = 3;
        public static readonly int AUTOMATION_ACTION_REDEEM_MISSION_WITH_WHEN_PURCHASE = 4;
        public static readonly int AUTOMATION_ACTION_MEMBER_TIER = 5;
        public static readonly int AUTOMATION_ACTION_CLEAR_POINT = 6;

        public static readonly int REDEEM_FILTER_TYPE_REDEEM_NAME = 1;
        public static readonly int REDEEM_FILTER_TYPE_REDEEM_ITEM = 2;

        public static readonly int TEMPERRORTRNASACTION_STATUS_DELETE = 2;
        public static readonly int TRNASACTION_STATUS_DELETE = 0;
        public static string BRAND_CODE = "";

        //public static readonly string TOKEN_KEY = "token_key";
        public static readonly string TOKEN_PROVIDER_CHATSHOP_SITE = "BrandChatShop";

        public static readonly int ALL_GROUP = 1;

        public static readonly int ACTIVE = 1;
        public static readonly int INACTIVE = 0;
        public static readonly int NOT_FOUND = 0;
        public static readonly int PIN_LOCK = 9;

        public static readonly int ISNOTREGISTER = 2;

        public static readonly int MISSION_MATCH_CONDITION_ALL = 2;
        public static readonly int MISSION_MATCH_CONDITION_ANY = 1;

        public static readonly int CONDITION_SOCIAL_USER = 1;
        public static readonly int CONDITION_SOCIAL_USER_BILLING = 2;
        public static readonly int CONDITION_SOCIAL_USER_SHIPPING = 3;
        public static readonly int CONDITION_SOCIAL_USER_CONTACT = 4;


        public static readonly int ISREGISTER = 1;
        public static readonly int ISBILLING = 2;
        public static readonly int ISSHIPPING = 3;
        public static readonly int ISCONTACT = 4;

        public static readonly int ADDRESS_PROVINCE = 1;
        public static readonly int ADDRESS_DISTRICT = 2;
        public static readonly int ADDRESS_SUBDISTRICT = 3;

        public static readonly int MISSION_CUSTOMER_SEGMENT_PERSONALIZE = 1;
        public static readonly int MISSION_CUSTOMER_SEGMENT_SEGMENT = 2;
        public static readonly int MISSION_CUSTOMER_SEGMENT_ALL = 3;

        public static readonly int MASTER_MISSION_DETAIL_TYPE_PRODUCT = 1;
        public static readonly int MASTER_MISSION_DETAIL_TYPE_COUPON = 2;
        public static readonly int MASTER_MISSION_DETAIL_TYPE_POINT = 3;
        public static readonly int MASTER_MISSION_DETAIL_TYPE_NOREWARD = 4;
        public static readonly int MASTER_MISSION_DETAIL_TYPE_RANDOM_REWARD = 5;

        public static readonly int PROMOTION_COUPON_DISCOUNT = 4;
        public static readonly int PROMOTION_HEADER_DISCOUNT = 3;
        public static readonly int PROMOTION_ITEM_DISCOUNT = 2;
        public static readonly int PROMOTION_SAP_DISCOUNT = 1;

        public static readonly int DISCOUNT_PERCENT = 1;
        public static readonly int DISCOUNT_BAHT = 2;

        public static readonly int ONLINE_BRANCH = 1;

        public static readonly int RUNNING_INVOICE_NO = 2;

        public static readonly int ADDRESS_SHIPPING = 1;
        public static readonly int ADDRESS_BILLING = 2;
        public static readonly int ADDRESS_CONTACT = 3;
        public static readonly int PAYMENT_TRANSFER = 1;

        public static readonly int BANK_TRANSFER = 1;
        public static readonly int DEBIT_2C2P = 2;

        public static readonly int PRIVILEGE_COUPON = 1;
        public static readonly int PRIVILEGE_STAMP_CARD = 2;
        public static readonly int PRIVILEGE_PASSPORT = 3;

        public static readonly int PUBLISH_COUPON = 1;
        public static readonly int DRAFT_COUPON = 0;

        public const int UpdateStatus = 1;
        public const int UpdateTrackingNumber = 2;
        public const int UpdateCancel = 3;

        public const int Spatype_Single = 1;
        public const int Spatype_Package = 2;
        public const int Spatype_Credit = 3;

        public static readonly int MAIN_BRANCH = 1;
        public static readonly int CENTER = 0;

        public static readonly string financeExcelPath = "/Media/Finance/";
        public static readonly string transactionExcelPath = "/Media/Transaction/";
        public static readonly string jobSheetlPath = "/Media/JobSheet/";
        public static readonly string ErrorString = "Error_String";
        public static readonly string AllBrandString = "ALL";

        public const int MaximumShardRecord = 200000;

        public static readonly string TOKEN_PROVIDER_FACEBOOK_USER_SITE = "FacebookUserSite";
        public static readonly string TOKEN_PROVIDER_USER_SITE = "UserSite";
        public static readonly string TOKEN_PROVIDER_AZURE_SITE = "AzureSite";
        public static readonly string TOKEN_PROVIDER_Branch_SITE = "BranchSite";
        public static readonly string TOKEN_PROVIDER_ADMIN_SITE = "AdminSite";
        public static readonly string SERVER_SIDE_ADMIN = "SERVER_SIDE_ADMIN";
        public static string AUHTORIZE_HEADER_NAME = "Authorization";
        public static readonly string SERVER_SIDE = "SERVER_SIDE";
        public static readonly string LANGUAGE_ID_KEY = "LANGUAGE_ID";
        public static readonly string LANGUAGE_BASE_ID = "1";
        public static readonly string USER_ID_KEY = "USER_ID";
        public static readonly string TOKEN_KEY = "TOKEN";
        public static readonly string IS_LOGIN_KEY = "IS_LOGIN";

        public static readonly int STATIC_PAGE_PRIVACY_POLICY = 1;
        public static readonly int STATIC_PAGE_TERMS_AND_CONDITION = 2;

        public static readonly string REQUIRE_EMAIL_REGISTER_CUSTOMER = "require_email_register_customer";
        public static readonly int REGISTER_TYPE_EMAIL = 1;
        public static readonly int REGISTER_TYPE_EXTERNAL = 2;
        public static readonly int REGISTER_TYPE_TELEPHONE = 3;

        public static readonly int SPA = 1;
        public static readonly int BOOTH = 2;

        public static readonly string VOID_CONVERT_TO_CASH = "1";
        public static readonly string VOID_CONVERT_TO_CREDIT = "2";
        public static readonly string VOID_REVERT_TO_PACKAGE = "3";

        public static readonly int BUY_SINGLE_UPGRADE = 20;

        public static readonly int YES = 1;
        public static readonly int GET_ALL = 1;

        public static readonly int ADJUST_POINT = 5;
        public const int POINT_INCREASE = 1;
        public const int POINT_DECREASE = 2;
        public const int POINT_ADJUST = 3;

        public static readonly int POINT_SETTING_DAY_NO_END_OF_MONTH = 0;
        public static readonly int POINT_SETTING_DAY = 1;
        public static readonly int POINT_SETTING_FIRST_BILL_OF_YEAR = 2;
        public static readonly int POINT_SETTING_END_OF_YEAR = 3;
        public static readonly int POINT_SETTING_NO_EXPIRE = 4;
        public static readonly int POINT_SETTING_DAY_FROM_TIER_START_DATE = 5;

        public static readonly int REDEEM_POINT = 2;
        public static readonly int REDEEM_POINT_EXTEND = 4;
        public static readonly int RETURN_REDEEM_POINT = 11;
        public static readonly int RETURN_REDEEM_POINT_EXTEND = 12;
        public static readonly int REDEEM_POINT_BRANCH = 22;
        public static readonly int REDEEM_POINT_EXTEND_BRANCH = 23;
        public static readonly int RETURN_REDEEM_POINT_BRANCH = 24;
        public static readonly int RETURN_REDEEM_POINT_EXTEND_BRANCH = 25;
        public const int REQUIRED_TYPE_FREE = 1;
        public const int REQUIRED_TYPE_QUANTITY = 2;
        public const int REQUIRED_TYPE_BATH = 3;

        public static readonly int MISSION_REDEEM_TYPE_SPENDING = 1;
        public static readonly int MISSION_REDEEM_TYPE_FREQUENCY = 2;
        public static readonly int MISSION_REDEEM_TYPE_POINT = 3;

        public const int MISSION_REDEEM_CONDITION_QUANTITY = 1;
        public const int MISSION_REDEEM_TPYE_CONDITION_SPENDING = 2;

        public const int WAIT_FOR_PACKAGING = 1;
        public const int PACKAGING = 2;
        public const int READY_TO_RECEIVE = 3;
        public const int COMPETE_WITHOUT_OTP = 4;
        public const int COMPETE = 5;
        public const int CANCELED = 6;
        public const int EXPIRED = 7;
        public const int SEND_SUCCESS = 8;

        public static readonly int TRUE = 1;
        public static readonly int FALSE = 0;

        public static readonly int AUTOMATION_SERVICE_BIRTHDAY = 1;
        public static readonly int AUTOMATION_SERVICE_REGISTER = 2;
        public static readonly int AUTOMATION_SERVICE_JOIN_CAMPAIGN = 3;
        public static readonly int AUTOMATION_SERVICE_UPGRADE_MEMBER_TIER = 4;
        public static readonly int AUTOMATION_SERVICE_QUARTER_PRIVILEGE = 5;
        public static readonly int AUTOMATION_SERVICE_WHEN_PURCHASE = 6;
        public static readonly int AUTOMATION_SERVICE_DAILY = 7;
        public static readonly int AUTOMATION_SERVICE_ALLOW = 8;
        public static readonly int AUTOMATION_SERVICE_RECEIVE_STAMP = 9;
        public static readonly int AUTOMATION_SERVICE_APPROVE_CAMPAIGN = 10;
        public static readonly int AUTOMATION_SERVICE_REJECT_CAMPAIGN = 11;
        public static readonly int AUTOMATION_SERVICE_WHEN_REDEEM = 12;
        public static readonly int AUTOMATION_SERVICE_WHEN_CONNECT_MARKETPLACE = 13;
        public static readonly int AUTOMATION_SERVICE_JOIN_MISSION = 14;
        public static readonly int AUTOMATION_SERVICE_WHEN_MISSION_UPDATE_PROGRESS = 15;
        public static readonly int AUTOMATION_SERVICE_WHEN_RECEIVE_MISSION_REWARD = 16;
        public static readonly int AUTOMATION_SERVICE_COMPLETE_MISSION = 17;
        public static readonly int AUTOMATION_SERVICE_MEMBER_TIER_EXPIRED = 18;
        public static readonly int AUTOMATION_SERVICE_CANCEL_REDEEM = 19;

        public static readonly int AUTOMATION_ATTRIBUTE_SET_MEMBER_TIER = 1;
        public static readonly int AUTOMATION_ATTRIBUTE_SET_RICH_MENU = 2;
        public static readonly int AUTOMATION_ATTRIBUTE_SET_MEMBER_TIER_CLEAR_SPENDING = 3;

        public static readonly int TRANSACION_COMPLETED = 1;
        public static readonly int TRANSACION_VOID = 2;
        public static readonly int TRANSACION_RETURNED = 3;
        public static readonly int TRANSACION_PARTIAL_RETURN = 4;

        public static readonly int ACTIVITY_EARN_POINT = 1;
        public static readonly int ACTIVITY_REDEEM_POINT = 2;
        public static readonly int ACTIVITY_VOID_RETURN = 3;
        public static readonly int ACTIVITY_REDEEM_POINT_EXTEND = 4;
        public static readonly int ACTIVITY_ADJUST_POINT = 5;
        public static readonly int ACTIVITY_VOID_RETURN_USED_POINT = 6;
        public static readonly int ACTIVITY_VOID_RETURN_USED_POINT_EXTEND = 7;
        public static readonly int ACTIVITY_TRANSFER_POINT = 99;
        public static readonly int ACTIVITY_RANDOM_REWARD = 33;
        public static readonly int ACTIVITY_MISSION = 34;

        public static readonly int CREATE_TRANSACTION_SPA = 1;
        public static readonly int CREATE_TRANSACTION = 10;
        public static readonly int VOID_TRANSACTION = 4;

        public static readonly int NOTE_CHANGE_ANSWER = 1;

        public static readonly int DELETE_TRANSACTION = 10;

        public static readonly int REQUEST_STATUS_OPEN = 1;
        public static readonly int REQUEST_STATUS_SUCCESS = 2;
        public static readonly int REQUEST_STATUS_CANCEL = 3;

        public static readonly string REQUEST_ACCESS_INFO = "request_access_info";
        public static readonly string REQUEST_PERSONAL_INFO = "request_personal_info";
        public static readonly string REQUEST_WITHDRAW_INFO = "request_withdraw_info";
        public static readonly string REQUEST_EDIT_INFO = "request_edit_info";
        public static readonly string REQUEST_SUSPEND_INFO = "request_suspend_info";
        public static readonly string REQUEST_DELETE_INFO = "request_delete_info";
        public static readonly string CANCEL_REQUEST = "cancel_request";
        public static readonly string HEADER_FOOTER = "header_footer";
        public static readonly string REQUEST_EMAIL_SUCCESS = "request_success";
        public static readonly string CANCEL_SUSPEND = "cancel_suspend";
        public static readonly string CANCEL_DELETE = "cancel_delete";
        public static readonly string DELETE_CONFIRM = "delete_confirm";

        public const int REQUEST_TYPE_ACCESS_INFO = 1;
        public const int REQUEST_TYPE_PERSONAL_INFO = 2;
        public const int REQUEST_TYPE_WITHDRAW_INFO = 3;
        public const int REQUEST_TYPE_EDIT_INFO = 4;
        public const int REQUEST_TYPE_SUSPEND_INFO = 5;
        public const int REQUEST_TYPE_DELETE_INFO = 6;

        public static readonly int EMAIL_HEADER = 1;
        public static readonly int EMAIL_BODY = 2;

        public static readonly int NOT_CONSENT = 0;
        public static readonly int DIRECT_CONSENT = 1;
        public static readonly int WAIT_TO_CONSENT = 2;
        public static readonly int FAIL_TO_CONSENT1 = 3;
        public static readonly int FAIL_TO_CONSENT2 = 4;
        public static readonly int FAIL_TO_CONSENT3 = 5;
        public static readonly int WITHDRAW = 6;
        public static readonly int SUSPEND = 7;
        public static readonly int DELETE = 8;

        public static readonly string REQUEST_SUCCESS = "ดำเนินการเรียบร้อย";
        public static readonly string REQUEST_IN_PROCESS = "ดำเนินการเรียบร้อยแล้ว จะส่งอีเมลล์ตอบกลับใน 30 วันทำการ";

        public static readonly int TEMPLATE_LINK_1 = 9;
        public static readonly int TEMPLATE_LINK_2 = 10;
        public static readonly int TEMPLATE_LINK_3 = 11;
        public static readonly int TEMPLATE_LINK_4 = 12;
        public static readonly int TEMPLATE_IMAGE_1 = 13;
        public static readonly int TEMPLATE_IMAGE_2 = 14;
        public static readonly int TEMPLATE_IMAGE_3 = 15;
        public static readonly int TEMPLATE_IMAGE_4 = 16;
        public static readonly int TEMPLATE_BRAND = 17;
        public static readonly int TEMPLATE_CONTRACT_EMAIL = 18;

        public static readonly string[] HEADER_DISCOUNT_CODE = { "7002", "7004" };
        public static readonly string[] SAP_DISCOUNT_CODE = { "6001", "6002", "6003", "6004", "6005", "6006", "6007", "6008" };
        public static readonly string[] ITEM_DISCOUNT_CODE = { "7003", "7001" };

        public static readonly double DEFAULT_DOUBLE = 0.00;

        public static readonly double OVER_PERCEN_VALUE = 100;
        public static readonly double NEGATIVE_VALUE = 0;

        public static readonly string PROMOTION_TYPE_NOT_FOUND = "Promotion type not found. ";
        public static readonly string PROMOTION_DISCOUNT_VALUE_TEXT = "Promotion discount value cannot be text. ";
        public static readonly string PROMOTION_DISCOUNT_NOT_FOUND = "Promotion discount not found. ";
        public static readonly string PROMOTION_DISCOUNT_BATH_NEGATIVE = "Promotion discount type bath cannot be negative. ";
        public static readonly string PROMOTION_DISCOUNT_PERCENT_NEGATIVE = "Promotion discount type percen cannot be negative. ";
        public static readonly string PROMOTION_DISCOUNT_PERCENT_OVER = "Promotion discount type percen cannot be over 100 percen. ";
        public static readonly string INVALID_REQUIRED_COLUMN = "Not all required columns are existed, this file could not be imported";
        public const string INVALID_PASSWORD = "Password must be at least 12 characters with at least one character from 0-9, A-Z, a-z, and special characters";
        public static readonly string RESET_PASSWORD_ERROR = "Password cannot be changed. Password must be at least 12 characters with at least one character from 0-9, A-Z, a-z, and special characters";
        public static readonly string IMPORT_ARTICLE_FAILED = "Cannot import this article. ";
        public static readonly string IMPORT_BRANCH_FAILED = "Cannot import this branch. ";
        public static readonly string IMPORT_PROMOTION_SUCCESS = "Successfully import promotion. ";
        public static readonly string IMPORT_PROMOTION_FAIL = "Failed to import this promotion. ";

        public static readonly int ACTIVITY_TYPE_LOGIN = 1;
        public static readonly int ACTIVITY_TYPE_LOGIN_FAIL = 2;
        public static readonly int ACTIVITY_TYPE_LOGOUT = 3;
        public static readonly int ACTIVITY_TYPE_CHANGE_PASSWORD = 4;
        public static readonly int ACTIVITY_TYPE_IMPORT_CUSTOMER = 5;
        public static readonly int ACTIVITY_TYPE_VIEW_CUSTOMER = 6;
        public static readonly int ACTIVITY_TYPE_EXPORT_CUSTOMER = 7;
        public static readonly int ACTIVITY_TYPE_EDIT_CUSTOMER = 8;
        public static readonly int ACTIVITY_TYPE_INCREASE_POINT = 9;
        public static readonly int ACTIVITY_TYPE_DECREASE_POINT = 10;
        public static readonly int ACTIVITY_TYPE_ADJUST_POINT = 11;
        public static readonly int ACTIVITY_TYPE_INCREASE_BRANCH_POINT = 12;
        public static readonly int ACTIVITY_TYPE_DECREASE_BRANCH_POINT = 13;
        public static readonly int ACTIVITY_TYPE_ADJUST_BRANCH_POINT = 14;
        public static readonly int ACTIVITY_TYPE_ADD_NOTE = 15;
        public static readonly int ACTIVITY_TYPE_EDIT_NOTE = 16;
        public static readonly int ACTIVITY_TYPE_REMOVE_NOTE = 17;
        public static readonly int ACTIVITY_TYPE_VIEW_TRANSACTION = 18;
        public static readonly int ACTIVITY_TYPE_EXPORT_TRANSACTION = 19;
        public static readonly int ACTIVITY_TYPE_IMPORT_TRANSACTION = 20;
        public static readonly int ACTIVITY_TYPE_VOID = 21;
        public static readonly int ACTIVITY_TYPE_DELETE = 22;
        public static readonly int ACTIVITY_TYPE_LIST_CUSTOMER = 23;
        public static readonly int ACTIVITY_TYPE_LIST_TRANSACTION = 24;
        public static readonly int ACTIVITY_TYPE_DELETE_CUSTOMER = 25;
        public static readonly int ACTIVITY_TYPE_EXPORT_REVENUE = 26;
        public static readonly int ACTIVITY_TYPE_EXPORT_CHANNEL = 27;
        public static readonly int ACTIVITY_TYPE_EXPORT_RFM = 28;
        public static readonly int ACTIVITY_TYPE_EXPORT_RETENTION = 29;
        public static readonly int ACTIVITY_TYPE_EXPORT_COUPON = 30;
        public static readonly int ACTIVITY_TYPE_EXPORT_REDEEM = 31;
        public static readonly int ACTIVITY_TYPE_EXPORT_PROMOTION = 32;
        public static readonly int ACTIVITY_TYPE_EXPORT_FIRST_PURCHASE = 33;
        public static readonly int ACTIVITY_TYPE_EXPORT_SUMMARY = 34;
        public static readonly int ACTIVITY_TYPE_EXPORT_PROSPECT = 35;
        public static readonly int ACTIVITY_TYPE_EXPORT_SERVICE = 36;
        public static readonly int ACTIVITY_TYPE_EXPORT_CUSTOMER_CONNECT = 37;
        public static readonly int ACTIVITY_TYPE_EXPORT_SEGMENT = 38;
        public static readonly int ACTIVITY_TYPE_DELETE_USER_COUPON = 39;
        public static readonly int ACTIVITY_TYPE_REDEEM_COUPON = 41;
        public static readonly int ACTIVITY_TYPE_EXPORT_MISSION_LIST = 42;
        public static readonly int ACTIVITY_TYPE_IMPORT_CUSTOMER_BILLING = 43;
        public static readonly int ACTIVITY_TYPE_IMPORT_CUSTOMER_SHIPPING = 44;
        public static readonly int ACTIVITY_TYPE_IMPORT_CUSTOMER_CONTACT = 45;

        public static readonly int EXTRAPOINT_ALL_TYPE = 0;
        public static readonly int EXTRAPOINT_MEMBERTIER = 1;
        public static readonly int EXTRAPOINT_SEGMENT = 2;
        public static readonly int REFERENCE_TYPE_EXTRAPOINT = 5;
        public static readonly int MENU_PRIVILEGE = 1;
        public static readonly int MENU_REDEEM = 2;
        public static readonly int MENU_NEWS = 3;

        public static readonly string COUPON_USED = "ใช้งานแล้ว";
        public static readonly string COUPON_UNUSED = "ยังไม่ได้ใช้งาน";
        public static readonly string COUPON_EXPIRED = "หมดอายุ";
        public static readonly string COLOR_RED = "#ff0000";
        public static readonly string COLOR_YELLOW = "#f9b115";
        public static readonly string COLOR_GREEN = "#00bd00";

        public static readonly string RESPONSE_OPTIONAL_FIELDS = "recipient_address,item_list,estimated_shipping_fee,order_flag,payment_method,update_time,message_to_seller," +
            "shipping_carrier,currency,create_time,pay_time,note,credit_card_number,days_to_ship,ship_by_date,escrow_tax," +
            "tracking_no,order_status,note_update_time,fm_tn,dropshipper_phone,cancel_reason,checkout_shipping_carrier,cancel_by,escrow_amount," +
            "buyer_cancel_reason,goods_to_declare,lm_tn,total_amount,service_code,actual_shipping_cost,cod,country,ordersn,dropshipper,buyer_username,discount_from_voucher_seller,buyer_user_id";
        public static readonly int REGISTER_CRM = 1;
        public static readonly int REGISTER_LINE = 2;
        public static readonly int REGISTER_SHOPEE = 3;
        public static readonly int REGISTER_LAZADA = 4;
        public static readonly int REGISTER_MOBILE_APP = 5;
        public static readonly int REGISTER_AZURE = 6;
        public static readonly int LOG_REGISTER_LINE_SHOPPING = 6;
        public static readonly int LOG_REGISTER_STOREHUB = 7;
        public static readonly int LOG_REGISTER_TIKTOK = 8;

        public static readonly int MEMBER_LEVEL_CONDITION = 1;
        public static readonly int GENDER_CONDITION = 2;
        public static readonly int LASTPURCHASE_DAY_CONDITION = 3;
        public static readonly int REGISTER_DAY_CONDITION = 4;
        public static readonly int EXPIRE_DAY_CONDITION = 5;
        public static readonly int PURCHASE_COUNT_CONDITION = 6;
        public static readonly int PRIVILEGE_CODE_CONDITION = 7;
        public static readonly int LASTPURCHASE_DATE_CONDITION = 8;
        public static readonly int REGISTER_DATE_CONDITION = 9;
        public static readonly int IMPORT_EXCEL_CONDITION = 10;
        public static readonly int STAMPCARD_CONDITION = 11;
        public static readonly int FIELD_CONDITION_CONDITION = 12;
        public static readonly int FIELD_GENDER_CONDITION_CONDITION = 13;
        public static readonly int FIELD_AGE_CONDITION_CONDITION = 14;
        public static readonly int AVERAGE_TRANSACTION_VALUE_CONDITION = 15;
        public static readonly int LASTPURCHASE_BRANCH_CONDITION = 16;
        public static readonly int PURCHASE_PRODUCT_CONDITION = 17;
        public static readonly int JOIN_CAMPAIGN_CONDITION = 18;
        public static readonly int PURCHASE_DATE_CONDITION = 19;
        public static readonly int TOTAL_SPEND_CONDITION = 20;
        public static readonly int TOTAL_LIFETIME_SPEND_CONDITION = 21;
        public static readonly int TOTAL_OUTSTANDING_CONDITION = 22;
        public static readonly int LAST_USE_SERVICE_BRANCH_CONDITION = 23;
        public static readonly int LAST_USE_SERVICE_X_DAY_AGO_CONDITION = 24;
        public static readonly int LASTPURCHASE_SERVICE_BRANCH_CONDITION = 25;
        public static readonly int LASTPURCHASE_SERVICE_X_DAY_AGOCONDITION = 26;
        public static readonly int USE_SERVICE_CONDITION = 27;
        public static readonly int PURCHASE_SERVICE_DATE_CONDITION = 28;
        public static readonly int USE_SERVICE_DATE_CONDITION = 29;
        public static readonly int TRANSACTION_AMOUNT = 30;
        public static readonly int REDEEM_ITEM = 31;
        public static readonly int TRANSACRION_DATE_FROM_CONDITION = 32;
        public static readonly int TRANSACRION_DATE_TO_CONDITION = 33;
        public static readonly int PROFILE_COMPLETE_CONDITION = 34;
        public static readonly int BIRTH_DATE_CONDITION = 35;
        public static readonly int CUSTOMER_TAG_CONDITION = 36;
        public static readonly int TRANSACTION_PURCHASE_PRODUCT_CONDITION = 37;
        public static readonly int TRANSACTION_PURCHASE_BRANCH_CONDITION = 38;
        public static readonly int TRANSACTION_PURCHASE_DATE_CONDITION = 39;
        public static readonly int FIRST_PURCHASE_DATE = 41;
        public static readonly int MEMBER_TIER_EXPIRED_DAY = 42;
        public static readonly int MEMBER_TIER_EXPIRED_DATE = 43;
        public static readonly int CONDITION_POINT = 44;
        public static readonly int CONDITION_FROM_MEMBER_LEVEL = 45;
        public static readonly int CONDITION_SEGMENT = 46;
        public static readonly int EXCEL_CUSTOMER = 1;
        public static readonly int EXCEL_REVENUE = 2;
        public static readonly int EXCEL_CHANNEL = 3;
        public static readonly int EXCEL_RETENTION = 4;
        public static readonly int EXCEL_LAPSE = 5;
        public static readonly int EXCEL_COUPON = 6;
        public static readonly int EXCEL_REDEEM = 7;
        public static readonly int EXCEL_PROMOTION = 8;
        public static readonly int EXCEL_FIRST_PURCHASE = 9;
        public static readonly int EXCEL_SUMMARY = 10;
        public static readonly int EXCEL_TRANSACTION = 11;
        public static readonly int EXCEL_SERVICE = 12;
        public static readonly int EXCEL_CONNECT = 13;
        public static readonly int EXCEL_FRIEND_GET_FRIEND = 14;
        public static readonly int EXCEL_COUPON_CODE = 15;
        public static readonly int EXCEL_FORM = 16;
        public static readonly int EXCEL_CUSTOMER_BILLING = 17;
        public static readonly int EXCEL_CUSTOMER_SHIPPING = 18;
        public static readonly int EXCEL_CUSTOMER_CONTACT = 19;
        public static readonly int FIELD_GENDER = 21;
        public static readonly int PLUGIN_PDPA = 1;
        public static readonly int PLUGIN_SEGMENT = 3;
        public static readonly int PLUGIN_TRCLOUD = 5;
        public static readonly int PLUGIN_TICKET_EMAIL = 7;
        public static readonly int PLUGIN_CACHE_DB = 8;
        public static readonly int PLUGIN_LS_CENTRAL = 9;
        public static readonly int REDEEM_TYPE_COUPON = 3;
        public static readonly int REDEEM_TYPE_PRODUCT = 1;

        public static readonly int CONDITIONDATE_TODAY = 1;
        public static readonly int CONDITIONDATE_THISMONTH = 2;
        public static readonly int CONDITIONDATE_CUSTOMDATE = 3;

        public static readonly int LIFECYCLETYPE_PROSPECT = 1;
        public static readonly int LIFECYCLETYPE_INACTIVE_PROSPECT = 2;
        public static readonly int LIFECYCLETYPE_FIRSTPURCHASE = 3;
        public static readonly int LIFECYCLETYPE_ACTIVE = 4;
        public static readonly int LIFECYCLETYPE_LAPSE = 5;
        public static readonly int LIFECYCLETYPE_INACTIVE = 6;
        public static readonly int LIFECYCLETYPE_PROSPECT_TO_INACTIVE_PROSPECT = 7;
        public static readonly int LIFECYCLETYPE_PROSPECT_TO_FIRSTPURCHASE = 8;
        public static readonly int LIFECYCLETYPE_INACTIVE_PROSPECT_TO_FIRSTPURCHASE = 9;
        public static readonly int LIFECYCLETYPE_FIRSTPURCHASE_TO_ACTIVE = 10;
        public static readonly int LIFECYCLETYPE_FIRSTPURCHASE_TO_LAPSE = 11;
        public static readonly int LIFECYCLETYPE_ACTIVE_TO_LAPSE = 12;
        public static readonly int LIFECYCLETYPE_LAPSE_TO_ACTIVE = 13;
        public static readonly int LIFECYCLETYPE_LAPSE_TO_INACTIVE = 14;
        public static readonly int LIFECYCLETYPE_INACTIVE_TO_ACTIVE = 15;

        public static readonly int LIFECYCLETYPE_REPORT_FIRSTPURCHASE = 1;
        public static readonly int LIFECYCLETYPE_REPORT_ACTIVE = 2;
        public static readonly int LIFECYCLETYPE_REPORT_LAPSE = 3;
        public static readonly int LIFECYCLETYPE_REPORT_INACTIVE = 4;

        public static readonly int RFM_REPORT_RECENCY = 1;
        public static readonly int RFM_REPORT_FREQUENCY = 2;
        public static readonly int RFM_REPORT_MONETARY = 3;
        public static readonly int RFM_REPORT_REPURCHASE = 4;
        public static readonly int EXTRAPOINT_TYPE_MULTIPLIER_POINT = 1;
        public static readonly int EXTRAPOINT_TYPE_AMOUNT_POINT = 2;

        public static readonly int FIELD_TYPE_DROPDOWN = 4;
        public static readonly int FIELD_TYPE_CHECKBOX = 2;
        public static readonly int FIELD_TYPE_MULTIPLECHOICE = 3;
        public static readonly int FIELD_TYPE_FIELD = 1;
        public static readonly int FIELD_TYPE_TEXTAREA = 12;
        public static readonly int FIELD_TYPE_BRANCHGROUP = 6;

        public static readonly int TICKET_ACTIVITY_TYPE_INTERNAL = 1;
        public static readonly int TICKET_ACTIVITY_TYPE_EXTERNAL = 2;
        public static readonly int TICKET_LOG_TYPE_COMMENT = 1;
        public static readonly int TICKET_LOG_TYPE_ACTIVITY = 2;

        public static readonly int TICKET_ACTION_TYPE_CREATE_TICKET = 1;
        public static readonly int TICKET_ACTION_TYPE_CREAT_ACTIVITY = 2;
        public static readonly int TICKET_ACTION_TYPE_EDIT_TICKET = 3;
        public static readonly int TICKET_ACTION_TYPE_EDIT_ACTIVITY = 4;
        public static readonly int TICKET_ACTION_TYPE_RESOLVED = 5;
        public static readonly int TICKET_ACTION_TYPE_CLOSED = 6;
        public static readonly int TICKET_STATUS_RESOLVED = 3;
        public static readonly int TICKET_STATUS_CLOSE = 4;
        public static readonly int TICKET_STATUS_INPROGRESS = 2;
        public static readonly int TICKET_STATUS_OPEN = 1;
        public static readonly int TICKET_FIELD_SOURCE = 2;
        public static readonly int TICKET_FIELD_TYPE = 6;
        public static readonly int TICKET_ACTION_TYPE_SENDMAIL = 7;
        public static readonly int TICKET_ACTION_TYPE_REPLYMAIL = 8;

        public static readonly int TICKET_FORM_DEFAULT = 1;

        public static readonly int SALE_CHANNEL_CRM = 1;
        public static readonly int SALE_CHANNEL_LAZADA = 2;
        public static readonly int SALE_CHANNEL_SHOPEE = 3;
        public static readonly int SALE_CHANNEL_LINE = 4;
        public static readonly int SALE_CHANNEL_STOREHUB = 5;
        public static readonly int SALE_CHANNEL_TIKTOK = 6;

        public static readonly string SETTING_ADDRESS_UNIT = "address_per_unit";
        public static readonly string SETTING_UPGRADE_WHEN_EXPIRED = "upgrade_when_expired";
        public static readonly string SETTING_SPILT_ITEM_WEBHOOK = "split_item_webhook";

        public static readonly int PRODUCT_TYPE_SIMPLE = 1;
        public static readonly int PRODUCT_TYPE_CONFIGURATION = 2;
        public static readonly int PRODUCTG_TYPE_VIRTUAL = 3;

        public static readonly int TRCLOUD_CREATE = 1;
        public static readonly int TRCLOUD_UPDATE = 2;
        public static readonly int TRCLOUD_GET = 3;
        public static readonly int TRCLOUD_DELETE = 4;
        public static readonly int TRCLOUD_SEARCH = 5;

        public static readonly int EXTERNAL_REF_TYPE_TRCLOUD = 1;

        public static readonly int IPST_TYPE_SUBJECT = 1;
        public static readonly int IPST_TYPE_EDUCATION = 2;
        public static readonly int IPST_TYPE_GRADE = 3;
        public static readonly string ZFIN = "ZFIN";
        public static readonly int DEPTH_1 = 1;
        public static readonly int DEPTH_2 = 2;
        public static readonly int DEPTH_3 = 3;
        public static readonly string AH2 = "AH138";
        public static readonly string IPST = "IPST";
        public static readonly int AH_ID_IPST = 2;
        public static readonly string PATH_ROOT_IPST = "1/138/";
        public static readonly string Migration_Completed = "MigrationCompleted";
        public static readonly string Migration_ERROR = "ErrorProcessingProductID";
        public static readonly string DEFAULT_BIRTHDAY = "1753-01-01";
        public static readonly int DEFAULT_CREATED_BY = 0;
        public static readonly int STATUS_ACTIVE = 1;
        public static readonly int STATUS_INACTIVE = 0;
        public static readonly int INVALID_AH_ID = 0;
        public static readonly int STOCK_EMPTY = 0;
        public static readonly int WelcomePopUp = 1;
        public static readonly int WelcomeScreen = 2;
        public static readonly int WelcomeDisable = 3;
        public static readonly int FONTTYPENORMAL = 1;
        public static readonly int FONTTYPEBOLD = 2;
        public static readonly string LANGUAGEID = "LANGUAGE-ID";
        public static readonly string HEAD_OFFICE_TEXT = "สำนักงานใหญ่";
        public static readonly string FOREIGNER_TEXT = "ต่างประเทศ";
        public static readonly string THAILAND_TEXT = "ประเทศไทย";
        public static readonly string TELEPHONE_COUNTRY_TH = "1";

        public static readonly int LOGIN_BRANCH = 0;
        public static readonly int SELECT_BRANCH = 1;
        public static readonly int UNUSED_TRANSACTION_ID = 0;
        public static readonly int SOURCE_BRANCH_TYPE = 1;
        public static readonly int DESTINATION_BRANCH_TYPE = 2;
        public static readonly int CREATE_TRANSFER_ID = 0;
        public static readonly int UNKNOWN_BRANCH_ID = 0;
        public static readonly int UNSPECIFIED_BRANCH_TYPE = 0;
        public static readonly int NO_COUNT = 0;
        public static readonly int LOGINTYPE_TELEPHONE = 1;
        public static readonly int LOGINTYPE_EMAIL = 2;
        public static readonly int EMAILTEMPLATE_OTP = 30;
        public static readonly int EMAILTEMPLATE_TICKET_CREATE = 31;
        public static readonly int EMAILTEMPLATE_TICKET_ASSIGN = 32;
        public static readonly int EMAILTEMPLATE_TICKET_CREATE_CUSTOMER = 33;
        public static readonly int EMAILTEMPLATE_TICKET_CREATE_CUSTOMER_BUSINESSHOUR = 34;

        public static readonly string SETTING_EXPORT_FULL_RECIEPT_LINE_LIMIT = "export_full_reciept_line_limit";
        public static readonly string SETTING_EXPORT_FULL_RECIEPT_LAST_PAGE_LINE_LIMIT = "export_full_reciept_last_page_line_limit";
        public static readonly int ROLE_SECTION_TICKET = 59;

        public static readonly int AUTOREPORT_EXPIRE_REMINDER = 3;

        public static readonly int RUNNING_ID_RQP = 2;
        public static readonly int RUNNING_ID_PO = 3;
        public static readonly int RUNNING_ID_GOODS_RECEIPT = 4;
        public static readonly int NOT_TRANSFER = 0;

        public static readonly int STORECREDIT_PAYMENT_CHANNEL = 19;
        public enum FieldProfileType
        {
            IdCard = 1,
            Prefix = 2,
            FirstNameTh = 3,
            FirstNameEn = 4,
            LastNameTh = 5,
            LastNameEn = 6,
            NickNameTh = 7,
            NickNameEn = 8,
            Birthday = 9,
            Telephone = 10,
            Email = 11,
            HomeAddress = 12,
            Town = 13,
            Alley = 14,
            Road = 15,
            Subdistrict = 16,
            District = 17,
            Province = 18,
            ZipCode = 19,
            Time = 20,
            Gender = 21,
            OptionalTelephone = 22,
            IdCardImage = 23
        }
        public enum FieldCompanyType
        {
            IdCard = 1,
            Prefix = 2,
            FirstNameTh = 3,
            FirstNameEn = 4,
            LastNameTh = 5,
            LastNameEn = 6,
            NickNameTh = 7,
            NickNameEn = 8,
            Birthday = 9,
            Telephone = 10,
            Email = 11,
            HomeAddress = 12,
            Town = 13,
            Alley = 14,
            Road = 15,
            Subdistrict = 16,
            District = 17,
            Province = 18,
            ZipCode = 19,
            Time = 20,
            Gender = 21,
            OptionalTelephone = 22,
            IdCardImage = 23,
            CompanyInformation = 27,
            CompanyType = 28,
            CompanyBranchType = 29,
        }
        public enum FieldBillingType
        {
            BillingAddressType = 1,
            IdCard = 2,
            Prefix = 3,
            FirstName = 4,
            LastName = 5,
            Email = 6,
            Telephone = 7,
            Country = 8,
            HomeAddress = 9,
            Town = 10,
            Alley = 11,
            Road = 12,
            Province = 13,
            District = 14,
            Subdistrict = 15,
            ZipCode = 16,
            Default = 17,
            Branch = 18
        }
        public enum FieldShippingType
        {
            AddressType = 1,
            Prefix = 2,
            FirstName = 3,
            LastName = 4,
            Email = 5,
            Telephone = 6,
            OptionalTelephone = 7,
            Country = 8,
            HomeAddress = 9,
            Town = 10,
            Alley = 11,
            Road = 12,
            Province = 13,
            District = 14,
            Subdistrict = 15,
            ZipCode = 16,
            ShippingType = 17,
            Other = 18,
            Default = 19
        }
        public enum FieldContactType
        {
            Prefix = 1,
            FirstName = 2,
            LastName = 3,
            ConpanyName = 4,
            Position = 5,
            Email = 6,
            Telephone = 7,
            OptionalTelephone = 8,
            Country = 9,
            HomeAddress = 10,
            Town = 11,
            Alley = 12,
            Road = 13,
            Province = 14,
            District = 15,
            Subdistrict = 16,
            ZipCode = 17,
            Default = 21
        }
        public enum InventoryActivitty
        {
            Receive = 1,
            Damage = 2,
            Adjust = 3,
            Sell = 4,
            Return_Void_Delete = 5,
            Redeem = 6,
            Transfer = 8
        }

        public enum TransactionStatus
        {
            Completed = 1,
            Void = 2,
            Returned = 3
        }

        public enum GraphType
        {
            Day = 1,
            Month = 2,
            Year = 3
        }

        public enum ReedeemAnalysisStatus
        {
            ReadyToReceive = 3,
            ReceivedWithoutOTP = 4,
            Received = 5,
            Success = 8
        }

        public enum RetentionExport
        {

            RevenueByDate = 1,
            Age = 2,
            Gender = 3,
            Product = 4,
            Retention = 5,
            Lapse = 6
        }

        public enum CouponAnalysisExport
        {
            Graph = 1,
            Coupon = 2,
            Product = 3,
            CouponDetail = 4,
            Branch = 5,
        }

        public enum RedeemAnalysisExport
        {
            Graph = 1,
            Completed = 2,
            RedeemDetail = 3,
            GraphMemberTier = 4,
            GraphBranch = 5,
            BranchTable = 6,
            CustomerTable = 7

        }

        public enum AgeGroup
        {
            below21 = 1,
            twentyOneTo25 = 2,
            twntySixTo30 = 3,
            ThirtyOneTo35 = 4,
            ThirtySixTo40 = 5,
            Above40 = 6
        }

        public enum InventoryTransferStatus
        {
            PendingPack = 7,
            PendingShipment = 1,
            PendingReceive = 2,
            Completed = 3,
            All = 0,
            Canceled = 5,
            Draft = 6
        }

        public enum BusinessInfomation
        {
            BrandName = 1,
            Address = 2,
            Subdistrict = 3,
            District = 4,
            Province = 5,
            Zipcode = 6,
            Latitude = 7,
            Longitude = 8,
            Open_date = 9,
            Open_time = 10,
            Telephone = 11,
            Tax_id = 12,
            Fax = 13,

        }
        public enum RQPActionType
        {
            Reject = 0,
            Transfer = 1,
            Production = 2,


        }
        public enum RQPStatus
        {
            Draft = 1,
            InProgress = 2,
            Completed = 3,


        }
        public enum InventoryDocumentType
        {
            ImportExcel = 1,
            InventoryTransfer = 2,
            GoodsReciept = 3,


        }
        public enum POStatus
        {
            WaitingForApproval = 1,
            WaitingForReceive = 2,
            Completed = 3,
            Cancelled = 4
        }
        public enum PoLogAction
        {
            TransferProduct = 1,
            ReceiveProduct = 2,
            Edit = 3,
            CreatePO = 4,
            Approve = 5,
            ClosePO = 6,
            Cancel = 7
        }
    }
}
