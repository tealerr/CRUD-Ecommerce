using Common.Helper;
using Common.Model;
using Common.Models;
using Common.Responses;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Request
{
    public class RegisterRequestV2
    {
        public int user_id { get; set; }
        public int registerTypeId { get; set; }
        public string socialId { get; set; }
        public string socialProvider { get; set; }
        public string country { get; set; } = "ประเทศไทย";
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string first_name_th { get; set; } = string.Empty;
        public string last_name_th { get; set; } = string.Empty;
        public string first_name_en { get; set; } = string.Empty;
        public string last_name_en { get; set; } = string.Empty;
        public string firstname_en { get; set; } = string.Empty;
        public string lastname_en { get; set; } = string.Empty;
        public string middle_name_th { get; set; } = string.Empty;
        public string middle_name_en { get; set; } = string.Empty;
        public string nickname { get; set; }
        public string nickname_en { get; set; }
        public string display_name { get; set; }
        public string telephone { get; set; } = string.Empty;
        public string optional_telephone { get; set; }
        public string email { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public string branch_id { get; set; }
        public string home_address { get; set; }
        public string alley { get; set; }
        public string town { get; set; }
        public string road { get; set; }
        public string subdistrict { get; set; }
        public string district { get; set; }
        public string province { get; set; }
        public string zip_code { get; set; }
        public string gender { get; set; }
        public string birthday { get; set; }
        public string t_datetime { get; set; }
        public string line_id { get; set; }
        public string the_one_card_member { get; set; } = string.Empty;
        public string line_ref { get; set; }
        public int accept_terms_and_conditions { get; set; }
        public int is_ba { get; set; }
        public int is_consent { get; set; }
        public int is_marketing { get; set; }
        public string birthday_day { get; set; }
        public string birthday_month { get; set; }
        public string birthday_year { get; set; }
        public int register_source_id { get; set; }
        public string prefix { get; set; }
        public string passport_no { get; set; }
        public string id_card { get; set; }
        public List<dynamic_field> custom_field { get; set; } = new List<dynamic_field>();
        public string invite_user_guid { get; set; }
        public string friend_get_friend_url_key { get; set; }
        public int is_privacy_policy { get; set; } = 1;
        public string created_time { get; set; } = DateTime.Now.ToString();
        public DateTime? expired_date { get; set; }
        public int customer_life_cycle { get; set; }
        public string external_id { get; set; }
        public int is_import { get; set; } = 0;
        public int is_complete_profile { get; set; } = 0;
        public string id_card_image { get; set; }
        public int is_company { get; set; }
        public string company_type { get; set; }
        public string company_branch_type { get; set; }
        public string company_branch { get; set; }
        public string company_branch_no { get; set; }
        public string company_information { get; set; }
        public string customer_type { get; set; }
        public int telephone_country_id { get; set; } = 1;
        public List<SocialMediaModel> social { get; set; }
        public List<DocumentModel> document { get; set; }
        public string picture_file_name { get; set; } = "";
        public string id_card_file_name { get; set; } = "";
        public string picture { get; set; } = "";
        public string ref_type { get; set; }
        public string ref_guid { get; set; }
        public string note { get; set; }
        public string member_id { get; set; }
        public int member_level_id { get; set; }
        public int price_tier { get; set; }
        public int blind_data_id { get; set; } = 1;
        public int login_type { get; set; } = CodeHelper.LOGINTYPE_TELEPHONE;
    }
    public class SocialMediaModel
    {
        public int id { get; set; }
        public string banner { get; set; }
        public string name { get; set; }
        public int social_media_group_id { get; set; }
        public int social_media_type_id { get; set; }
        public int user_shipping_address_media_id { get; set; }
        public int user_billing_address_media_id { get; set; }
        public int user_contact_address_media_id { get; set; }
        public string user_guid { get; set; }
        public string value { get; set; }

    }
    public class StateRegister
    {
        public RegisterRequestV2 user { get; set; }
        public string remoteIpAddress { get; set; }
        public string user_guid { get; set; }
        public int custom_field_count { get; set; }
        public int get_answer_require_count { get; set; }
        public int transaction_id { get; set; }
    }
    public class StateCampaignForm
    {
        public string user_guid { get; set; }
        public string remoteIpAddress { get; set; }
        public bool is_register_automation { get; set; }
        public int form_id { get; set; }
    }

    public class TransactionState
    {
        public string remoteIpAddress { get; set; }
        public string user_guid { get; set; }
        public int transaction_id { get; set; }
        public double price { get; set; }
        public bool isSpa { get; set; }
        public bool isStoreCredit { get; set; }
        public List<TransactionData.Product> productNoSerial { get; set; }
        public List<UpdateInventoryRequest> inventoryAll { get; set; }
        public string sales_user_guid { get; set; }
        public TransactionData transaction { get; set; }
        public string t_datetime { get; set; }
        public List<CalculateTransaction.ProductDetail> product_array { get; set; }
        public List<CalculateTransaction.Promotion> used_promotion { get; set; }

    }

    public class RedeemState
    {
        public TargetRequired targetRequired { get; set; }
        public string user_guid { get; set; }
        public string ipAddress { get; set; }
        public int targetRedeemorderId { get; set; }

    }
}
