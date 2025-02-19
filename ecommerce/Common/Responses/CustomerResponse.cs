using Common.Model;
using Common.Models;
using Common.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Responses
{
    public class CustomerResponseModel
    {
        public List<CustomerModel> detail { get; set; }
        public int total_count { get; set; }

        public class CustomerModel
        {
            public int id { get; set; }
            public string user_guid { get; set; }
            public string nickname { get; set; } = "";
            public string first_name_en { get; set; }
            public string last_name_en { get; set; }
            public string first_name_th { get; set; }
            public string last_name_th { get; set; }
            public string middle_name_th { get; set; }
            public string middle_name_en { get; set; }
            public string birthday { get; set; }
            public int age { get; set; }
            public string age_string { get; set; }
            public string age_group { get; set; }
            public bool has_birthday_year { get; set; }
            public string gender { get; set; }
            public string email { get; set; }
            public string picture { get; set; }
            public string telephone { get; set; }
            public string optional_telephone { get; set; }
            public string line_id { get; set; }
            public DateTime created_time { get; set; }
            public string member_id { get; set; }
            public string member_level_id { get; set; }
            public string member_level_name { get; set; }
            public string member_level_icon { get; set; }
            public string point_total { get; set; }
            public string grand_total { get; set; } = "0";
            public int is_consent { get; set; }
            public string passport_no { get; set; }
            public string id_card { get; set; }
            public string prefix { get; set; }
            public string facebook { get; set; }
            public string x { get; set; }
            public int is_company { get; set; }
            public string company_type { get; set; }
            public string company_branch_type { get; set; }
            public string company_branch { get; set; }
            public string company_branch_no { get; set; }
            public string company_information { get; set; }
            public string external_id { get; set; } = string.Empty;
            public int blind_data_id { get; set; }
            public string register_date { get; set; }
        }
    }
    public class CustomerDetailResponse : CustomerResponseModel.CustomerModel
    {
        public string zip_code { get; set; }
        public string province { get; set; }
        public string home_address { get; set; }
        public string town { get; set; }
        public string country { get; set; }
        public string alley { get; set; }
        public string road { get; set; }
        public string subdistrict { get; set; }
        public string district { get; set; }
        public string total_branch_point { get; set; }
        public string id_card_image { get; set; }
        public string center_point { get; set; }
        public string month { get; set; }
        public string day { get; set; }
        public string year { get; set; }
        public string expire_date { get; set; }
        public int first_transaction_id { get; set; }
        public int last_transaction_id { get; set; }
        public string last_purchase_date { get; set; }
        public string last_purchase_branch { get; set; }
        public string last_purchase_channel { get; set; }
        public string first_purchase_date { get; set; }
        public string first_purchase_branch { get; set; }
        public string first_purchase_channel { get; set; }
        public string customer_lifecycle { get; set; }
        public string tier_start_date { get; set; }
        public string customer_type { get; set; }
        public string telephone_country_id { get; set; }
        public string telephone_country_name { get; set; }
        public string telephone_country_code { get; set; }
        public string telephone_country_dial_code { get; set; }
        public string id_card_file_name { get; set; }
        public string picture_file_name { get; set; }
        public string price_tier_name { get; set; }
        public int price_tier_id { get; set; }
        public RFMModel recently { get; set; }
        public RFMModel frequency { get; set; }
        public RFMModel monetary { get; set; }
        public RFMModel totalspend { get; set; }
        public List<MarketplaceListResponseModel> connect_channel { get; set; }
        public List<Field> user_custom_field { get; set; }
        public List<TagDetailModel> user_tag { get; set; }
        public List<DocumentModel> documents { get; set; }
        public List<DocumentSettingModel> documents_setting { get; set; }
        public List<SocialMediaModel> social { get; set; }
        public List<TicketListDetail> activity { get; set; }
        public string limit_secondary_telephone { get; set; }
        public UserSettingModel user_setting { get; set; } = new UserSettingModel();
        public List<CustomerVariableResponse> userRefDchat { get; set; }
    }
    public class UserSettingModel
    {
        public string address_per_unit { get; set; }
    }
    public class DocumentModel
    {
        public int id { get; set; }
        public int document_type_id { get; set; }
        public string document_name { get; set; }
        public string file_name { get; set; }
        public string name { get; set; }
        public string path { get; set; }
        public string user_guid { get; set; }
        public DateTime created_time { get; set; }
    }
    public class UpdateDocumentSettingModel
    {
        public List<DocumentSettingModel> document { get; set; }
        public List<int> delete { get; set; }
    }

    public class DocumentSettingModel
    {
        public int id { get; set; }
        public int is_required { get; set; }
        public string name { get; set; }
        public int is_png { get; set; }
        public int is_jpg { get; set; }
        public int is_pdf { get; set; }
        public int sort_order { get; set; }
        public int is_register { get; set; }
        public int is_not_register { get; set; }
        public int is_display { get; set; }
    }
    public class UpdateCustomerTypeModel
    {
        public List<CustomerTypeModel> customer_type { get; set; }
        public List<int> delete { get; set; }
    }
    public class CustomerTypeModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public int is_default { get; set; }
        public int sort_order { get; set; }
    }
    public class PurchaseDetailModel
    {
        public string date { get; set; }
        public string branch { get; set; }
        public string channel { get; set; }
    }
    public class RFMAllModel
    {
        public int recently_min { get; set; }
        public int recently_max { get; set; }
        public float recently_average { get; set; }
        public float recently_value { get; set; }
        public int frequency_min { get; set; }
        public int frequency_max { get; set; }
        public float frequency_average { get; set; }
        public float frequency_value { get; set; }
        public int monetary_min { get; set; }
        public int monetary_max { get; set; }
        public float monetary_average { get; set; }
        public float monetary_value { get; set; }
        public int totalspend_min { get; set; }
        public int totalspend_max { get; set; }
        public float totalspend_average { get; set; }
        public float totalspend_value { get; set; }
    }
    public class RFMModel
    {
        public int min { get; set; }
        public int max { get; set; }
        public float average { get; set; }
        public float value { get; set; }
    }

    public class CustomerExportModel : CustomerResponseModel.CustomerModel
    {
        public string name { get; set; }
        public string member_id { get; set; } = "";
        public string home_address { get; set; } = "";
        public string town { get; set; } = "";
        public string alley { get; set; } = "";
        public string road { get; set; } = "";
        public string subdistrict { get; set; } = "";
        public string district { get; set; } = "";
        public string province { get; set; } = "";
        public string zip_code { get; set; } = "";
        public int is_consent { get; set; } = 0;
        public string consent_status { get; set; }
        public int first_transaction_id { get; set; } = 0;
        public int first_branch_id { get; set; } = 0;
        public int last_transaction_id { get; set; } = 0;
        public int last_branch_id { get; set; } = 0;
        public string last_branch_name { get; set; }
        public string regiter_date
        {
            get
            {
                return regiter_date = this.created_time.ToString("dd/MM/yyyy");
            }
            set { }
        }
        public string regiter_time
        {
            get
            {
                return regiter_time = this.created_time.ToString("HH:mm:ss");
            }
            set { }
        }
        public string consent_date { get; set; }
        public string id { get; set; }
        public string point { get; set; }
        public DateTime transaction_created_time { get; set; }
        public string last_purchase_date { get; set; } = "";
        public string last_purchase_time { get; set; } = "";
        public string last_purchase_branch { get; set; } = "";
        public string first_purchase_branch { get; set; } = "";
        public string first_purchase_date { get; set; } = "";
        public string first_purchase_time { get; set; } = "";
        public string user_life_cycle { get; set; }
        public string user_guid { get; set; } = "";
        public double spend_visit { get; set; } // atv
        public int total_bill { get; set; }
        public int total_frequency { get; set; }
        public double purchase_latency { get; set; }


        public string spend_visit_string
        {
            get
            {
                return spend_visit_string = this.spend_visit.ToString();
            }
            set { }
        }
        public string total_bill_string
        {
            get
            {
                return total_bill_string = this.total_bill.ToString();
            }
            set { }
        }
        public string total_frequency_string
        {
            get
            {
                return total_frequency_string = this.total_frequency.ToString();
            }
            set { }
        }
        public string purchase_latency_string
        {
            get
            {
                return purchase_latency_string = this.purchase_latency.ToString();
            }
            set { }
        }
        public string additionalTag { get; set; }

    }
    public class AdditionalTagModel
    {
        public string user_guid { get; set; }
        public int tag_type_id { get; set; }
        public string name { get; set; }
    }
    public class AddtionalQuestionModel
    {
        public string user_guid { get; set; }
        public int register_form_field_id { get; set; }
        public string value { get; set; }
    }
    public class AddtionalQuestion
    {
        public int id { get; set; }
        public string name { get; set; }
    }
    public class CustomerTotalPoint
    {
        public string user_guid { get; set; }
        public string total_point { get; set; }
    }
    public class CustomerTransactionHistoryResponse
    {
        public List<CustomerTransactionHistory> detail { get; set; }
        public int total_count { get; set; }
        public class CustomerTransactionHistory
        {
            public string invoice_no { get; set; }
            public int transaction_id { get; set; }
            public string sale_name { get; set; }
            public string role_name { get; set; }
            public string status { get; set; }
            public string status_name { get; set; }
            public string transaction_date { get; set; }
            public string record_created_time { get; set; }
            public string grand_total { get; set; }
            public string point { get; set; }
            public int branch_id { get; set; }
            public string branch_name { get; set; }
            public int payment_channel_id { get; set; }
            public string payment_name { get; set; }
            public int source_id { get; set; }
            public string source_name { get; set; }
            public int sales_channel_id { get; set; }
            public string sales_channel_name { get; set; }
            public string branch_point { get; set; }
            public string center_point { get; set; }
        }
    }
    public class ActivityDescriptionModel
    {
        public int id { get; set; }
        public string description { get; set; }
    }
    public class CustomerPointHistoryResponse
    {
        public List<CustomerPointHistory> detail { get; set; }
        public int total_count { get; set; }
        public class CustomerPointHistory
        {
            public int id { get; set; }
            public string invoice_no { get; set; }
            public int? transaction_id { get; set; }
            public int? redeem_order_id { get; set; }
            public string redeem_item_name { get; set; } = "";
            public string sale_name { get; set; }
            public string role_name { get; set; }
            public string status { get; set; }
            public string status_name { get; set; }
            public string transaction_date { get; set; }
            public string record_created_time { get; set; }
            public string grand_total { get; set; }
            public string point { get; set; }
            public string current_point { get; set; }
            public string branch_name { get; set; }
            public string point_branch_name { get; set; }
            public int transaction_branch_id { get; set; }
            public int redeem_branch_id { get; set; }
            public string note { get; set; }
            public int activity_id { get; set; }
            public int activity_ref_id { get; set; }
            public string log_type
            {
                get
                {
                    switch (activity_id)
                    {
                        case 1:
                            return "บิลขาย";
                        case 2:
                        case 11:
                        case 12:
                        case 23:
                        case 24:
                        case 25:
                            return "แลกคะแนน";
                        case 3:
                            return "ยกเลิก";
                        case 10:
                            return "ลบรายการ";
                        case 9:
                        case 26:
                            return "คะแนนพิเศษ";
                    }
                    return "";
                }
                set { }
            }
            public bool is_centerpoint { get; set; }
        }
    }
    public class CustomerNoteResponse
    {
        public string note { get; set; }
        public string last_updated { get; set; }
        public string sale_name { get; set; }
        public string role_name { get; set; }
    }
    public class UpdateCustomerPointResponse
    {
        public int result { get; set; }
        public string message { get; set; }
        public string dev_message { get; set; }
        public int last_point { get; set; }
    }
    public class CustomerPointResponse
    {
        public int center_point { get; set; }
        public int branch_point { get; set; }
    }
    public class CustomerListResponseModel
    {
        public List<CustomerModel> detail { get; set; }
        public List<string> user_guid_list { get; set; }
        public int total_count { get; set; }

        public class CustomerModel
        {
            public string user_guid { get; set; }
            public string first_name_en { get; set; }
            public string last_name_en { get; set; }
            public string first_name_th { get; set; }
            public string last_name_th { get; set; }
            public string birthday { get; set; }
            public DateTime birthday_date { get; set; }
            public bool has_birthday_year { get; set; }
            public string gender { get; set; }
            public string email { get; set; }
            public string picture { get; set; }
            public string telephone { get; set; }
            public string line_id { get; set; }
            public DateTime created_time { get; set; }
            public string member_level_id { get; set; }
            public string member_id { get; set; }
            public string member_level_name { get; set; }
            public string member_level_icon { get; set; }
            public string point_total { get; set; }
            public string grand_total { get; set; }
            public int is_consent { get; set; }
            public int blind_data_id { get; set; }
            public string register_date { get; set; }
        }
    }
    public class CustomerLogModel
    {
        public string first_name_th { get; set; }
        public string last_name_th { get; set; }
        public string first_name_en { get; set; }
        public string last_name_en { get; set; }
        public string nickname { get; set; }
        public string email { get; set; }
        public string picture { get; set; }
        public string telephone { get; set; }
        public string member_id { get; set; }
        public string birthday { get; set; }
        public string home_address { get; set; }
        public string gender { get; set; }
        public string zip_code { get; set; }
        public string province { get; set; }
        public string town { get; set; }
        public string alley { get; set; }
        public string road { get; set; }
        public string subdistrict { get; set; }
        public string district { get; set; }
        public int is_consent { get; set; }
    }

    public class RegisterSourceListResponse
    {
        public int id { get; set; }
        public string name { get; set; }
        public int customer { get; set; }
        public int is_persist { get; set; }
        public DateTime? valid_from { get; set; }
        public DateTime? valid_to { get; set; }
        public int status { get; set; }
        public string url { get; set; }
        public string code_url { get; set; }
        public DateTime created_time { get; set; }
        public DateTime updated_time { get; set; }
    }
    public class ConsntStatusListResponse
    {
        public int id { get; set; }
        public string name { get; set; }
        public int status { get; set; }

    }
    public class CustomerLifecycleResponse
    {
        public string user_guid { get; set; }
        public DateTime created_time { get; set; }
        public DateTime user_life_cycle_change_date { get; set; }
        public int user_life_cycle_type_id { get; set; }
        public int is_update_life_cycle { get; set; }
        public string transaction_id { get; set; }
        public double grand_total { get; set; }
        public DateTime transaction_date { get; set; }

    }
    public class CustomerAllPointResponse
    {
        public int id { get; set; }
        public int point { get; set; }
        public int branch_id { get; set; }
        public int is_extension_point { get; set; }
        public string name { get; set; }
        public DateTime? expired_date { get; set; } = null;
        public string expired_date_string { get; set; }

    }
    public class LogReportLifeCycleMode
    {
        public int id { get; set; }
        public string user_guid { get; set; }
        public int report_life_cycle_type_id_from { get; set; }
        public int report_life_cycle_type_id_to { get; set; }
        public DateTime created_time { get; set; }
    }
    public class CustomerByTelResponse
    {
        public string user_guid { get; set; }
        public string telephone { get; set; }
        public string optional_telephone { get; set; }
    }

    public class CustomerLifeCycleModel
    {
        public int year { get; set; }
        public int month { get; set; }
        public int day { get; set; }
        public int report_life_cycle_type_id { get; set; }
        public int total { get; set; }
    }

    public class CustomerLifeCycleStampModel
    {
        public int user_life_cycle_type_id { get; set; }
        public int count { get; set; }
    }

    public class MemberLevelLogListResponse
    {
        public List<MemberLevelLogResponse> member_level_log_list { get; set; }
        public int total_count { get; set; }
    }
    public class MemberLevelLogResponse
    {
        public int id { get; set; }
        public string user_guid { get; set; }
        public string user_name { get; set; }
        public int from_member_level { get; set; }
        public string from_member_level_name { get; set; }
        public int to_member_level { get; set; }
        public string to_member_level_name { get; set; }
        public string note { get; set; }
        public DateTime old_start_date { get; set; }
        public DateTime old_expire_date { get; set; }
        public DateTime new_start_date { get; set; }
        public DateTime new_expire_date { get; set; }
        public DateTime created_time { get; set; }
    }
    public class StoreCreditLogListResponse
    {
        public List<StoreCreditLogResponse> store_credit_log_list { get; set; }
        public int total_count { get; set; }
    }
    public class StoreCreditLogResponse
    {
        public int id { get; set; }
        public int transaction_id { get; set; }
        public string invoiice_no { get; set; }
        public int store_credit_type_id { get; set; }
        public string store_credit_type_name { get; set; }
        public double use_store_credit { get; set; }
        public double burn_amount { get; set; }
        public double current_store_credit { get; set; }
        public string store_credit { get; set; }
        public string created_by { get; set; }
        public DateTime created_time { get; set; }
    }
}
