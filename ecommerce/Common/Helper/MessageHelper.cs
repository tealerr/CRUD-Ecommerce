using Common.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Helper
{
    public class MessageHelper
    {
        public static readonly string API_KEY_UNAUTHORIZED = "API Key ไม่ถูกต้อง";
        public static readonly string INTERNAL_ERROR_MESSAGE = "เกิดข้อผิดพลาดในระบบ กรุณาลองใหม่อีกครั้ง";
        public static readonly string INVALID_AMOUNT = "กรุณาใส่จำนวนเงินมากกว่าราคาสุทธิ";
        public static readonly string INVALID_PASSWORD = "พาสเวิร์ดไม่ถูกต้อง โปรดลองอีกครั้ง";
        public static readonly string INVALID_SALES_NO = "ไม่พบ Salesperson ID นี้";
        public static readonly string INVALID_STATUS = "ไม่สามารถเปลี่ยนเป็นสถานะนี้ได้";
        public static readonly string NO_PERMISSION = "ไม่มีสิทธิ์เช้าถึง";
        public static readonly string NO_TOKEN = "ไม่พบ token";
        public static readonly string PLEASE_SELECT_PAYMENT_METHOD = "กรุณาเลือกช่องทางการชำระเงิน";
        public static readonly string SAVE_FAILED = "บันทึกไม่สำเร็จกรุณาลองใหม่อีกครั้ง";
        public static readonly string STAMP_CARD_ALREADY_REDEEM = "คุณได้รับสิทธิ์ไปแล้ว";
        public static readonly string STAMP_CARD_INVALID = "ยังสะสมสแตมป์ไม่ถึงสำหรับการแลก";
        public static readonly string SUCCESSFULLY_LOG_OUT = "ออกจากระบบสำเร็จ";
        public static readonly string TELEPHONE_NOT_FOUND = "ไม่พบเบอร์โทรศัพท์";
        public static readonly string TOKEN_UNAUTHORIZED = "ไม่มีสิทธิ์เข้าถึงข้อมูล กรุณาลองใหม่อีกครั้ง";
        public static readonly string TRANSACTION_NOT_FOUND = "ไม่พบรายการ";
        public static readonly string UPLOAD_IMAGE_FAILED = "ไม่สามารถอัพโหลดรูปได้ กรุณาลองใหม่อีกครั้ง";
        public static readonly string INTERNAL_ERROR = "เกิดข้อผิดพลาดบางประการไม่ สามารถดำเนินการได้";
        public static readonly string NOPERMISSION_ERROR = "ไม่สามารถดำเนินการต่อได้ คุณไม่มีสิทธิ์เข้าถึงการทำงานส่วนนี้";
        public static readonly string USER_NOT_FOUND = "ไม่พบ Account ในระบบ";
        public static readonly string USE_SPA = "ใช้งานสปา";
        public static readonly string VOID_SPA = "ยกเลิกการใช้งานสปา";
        public static readonly string USE_CREDIT = "ใช้งานเครดิต";
        public static readonly string BUY_SINGLE = "ซื้อ Single Treatment";
        public static readonly string BUY_PACKAGE = "ซื้อ Package";
        public static readonly string BUY_CREDIT = "เติมเครดิต";
        public static readonly string SALE_ID_NOT_CORRECT = "ไม่พบ Sale Id ในสาขา";
        public static readonly string INVALID_USED_DATETIME = "กรุณาใส่ช่วงเวลาให้อยู่ในช่วงปี ค.ศ.2000 - ปัจจุบัน";
        public static readonly string ERROR_USE_CREDIT = "error use credit";
        public static readonly string BUY_SINGLE_UPGRADE = "ซื้อ Single Treatment Upgrade";
        public static readonly string ERROR_UPGRADE_PACKAGE = "Error Upgrade Package";
        public static readonly string UPGRADE_PACKAGE_SUCCESS = "อัพเกรดสำเร็จ";
        public static readonly string USAGE_HISTORY_NOT_FOUND = "ไม่มีประวัติการใช้";
        public static readonly string CHECK_PACKAGE = "Package นี้ไม่สามารถ Upgrade ได้";
        public static readonly string BRANCH_ID_NOT_MATCH = "บิลมาจากคนละสาขา";
        public static readonly string USER_ID_NOT_MATCH = "บิลมาจากลูกค้าคนละคน";
        public static readonly string DATE_NOT_MATCH = "บิลมาจากการซื้อคนละวัน";
        public static readonly string IS_MERGE = "บิลมีการรวม Point มาก่อนแล้ว";
        public static readonly string INVOICE_NO_INCORRECT = "invoice no นี้ไม่สามารถรวมบิลได้";
        public static readonly string INVOICE_NOT_FOUND = "ไม่พบเลข invoice no";
        public static readonly string INVOICE_NO_INVALID = "เลข invoice no ซ้ำกัน";
        public static readonly string MERGE_FAIL = "รวมบิลไม่สำเร็จ";
        public static readonly string INVOICE_SPA_IS_USE = "invoice no นี้ไม่สามารถลบได้เนื่องจากมีการใช้ไปแล้ว";
        public static readonly string IMPORT_ARTICLE_FAILED = "Cannot import this article. ";
        public static readonly string BLIND_DATA = "****";

        public static readonly string ARTICLE_NOT_FOUND = "Article not found. ";
        public static readonly string ARTICLE_PRICE_NOT_FOUND = "Article price not found. ";
        public static readonly string ARTICLE_PRICE_NEGATIVE = "Article price cannot be negative. ";
        public static readonly string DATABASE_NO_PLANT_GROUP = "This plant group is not in the database. ";
        public static readonly string DATABASE_NO_PRODUCT = "This product is not in the database. ";
        public static readonly string IMPORT_BRANCH_FAILED = "Cannot import this branch. ";
        public static readonly string IMPORT_PRODUCT_FAILED = "Cannot import this product. ";
        public static readonly string IMPORT_PROMOTION_SUCCESS = "Successfully import promotion. ";
        public static readonly string IMPORT_PROMOTION_FAIL = "Failed to import this promotion. ";
        public static readonly string PLANT_GROUP_NOT_FOUND = "Plant group not found. ";
        public static readonly string PROMOTION_TYPE_NOT_FOUND = "Promotion type not found. ";
        public static readonly string UPDATE_PROMOTION_SUCCESS = "Successfully update this promotion. ";
        public static readonly string NOT_FOUND_IMPORT_INVENTORY_ERROR_MESSAGE = "Document not found.";

        public static readonly string BRANCH_NOT_FOUND_ERROR_MESSAGE = "Account นี้ไม่มี branch";
        public static readonly string INVENTORY_TRANSFER_NOT_FOUND = "ไม่พบเอกสาร หรือ ไม่สามารถเข้าถึงเอกสารดังกล่าวได้";
        public static readonly string INVENTORY_TRANSFER_NOT_SAME_BRANCH = "ไม่สามารถเลือกสาขาต้นทางและปลายทางเป็นสาขาเดียวกันได้";
        public static readonly string INVENTORY_TRANSFER_PLEASE_SELECT_PRODUCT = "โปรดเลือกสินค้า";
        public static readonly string INVENTORY_TRANSFER_MUST_BE_LOGIN_BRANCH = "สาขาต้นทาง / ปลายทางต้องเป็นสาขาปัจจุบันที่เข้าสู่ระบบ";
        public static readonly string INVENTORY_TRANSFER_ONLY_DRAFT_CHANGE = "ไม่สามารถแก้ไขได้เนื่องจากไม่ได้อยู่ในสถานะแบบร่าง";
        public static readonly string INVENTORY_TRANSFER_ONLY_CREATE_BRANCH_UPDATE = "ไม่สามารถแก้ไขได้เนื่องจากไม่ใช่สาขาที่สร้าง";
        public static readonly string INVENTORY_TRANSFER_DOCUMENT_NOT_FOUND = "ไม่พบเอกสารต้นแบบ";
        public static readonly string INVENTORY_TRANSFER_DOCUMENT_SOURCE_BRACNH_NOT_MATCH = "สาขาต้นทางไม่ตรงกับสาขาต้นทางที่สามารถเลือกได้";
        public static readonly string INVENTORY_TRANSFER_DOCUMENT_DESTINATION_BRACNH_NOT_MATCH = "สาขาปลายทางไม่ตรงกับสาขาปลายทางที่สามารถเลือกได้";
        public static readonly string INVENTORY_TRANSFER_INSUFFICIENT_STOCK = "จำนวนสินค้าไม่เพียงพอ";
        public static readonly string INVENTORY_TRANSFER_CURRENT_STOCK = "จำนวนคงเหลือ";
        public static readonly string INVENTORY_TRANSFER_PLEASE_SELECT_SERIAL = "โปรดระบุ serial สินค้าให้ครบทุกรายการ";
        public static readonly string INVENTORY_TRANSFER_SERIAL_UNAVAILABLE = "ไม่พบในสาขาต้นทาง";
        public static readonly string INVENTORY_TRANSFER_SAVE_SUCCESS = "บันทึกสำเร็จ";
        public static readonly string INVENTORY_TRANSFER_SAVE_FAIL = "บันทึกไม่สำเร็จ";
        public static readonly string INVENTORY_TRANSFER_CANNOT_CHANGE_STATUS = "สถานะไม่ถูกต้องในการดำเนินการ";
        public static readonly string INVENTORY_TRANSFER_BRANCH_NO_PERMISSION_TO_CONFIRM = "สาขาของคุณไม่มีสิทธิ์ในการยืนยัน";
        public static readonly string INVENTORY_TRANSFER_BRANCH_NO_PERMISSION_TO_CANCEL_DARFT = "เฉพาะสาขาที่สร้างที่สามารถยกเลิกได้ สาขาของคุณไม่มีสิทธิ์ในการยกเลิก";
        public static readonly string INVENTORY_TRANSFER_BRANCH_NO_PERMISSION_TO_CANCEL_SHIPMENT = "เฉพาะสาขาต้นทางที่สามารถยกเลิกได้ สาขาของคุณไม่มีสิทธิ์ในการยกเลิก";
        public static readonly string INVENTORY_TRANSFER_ONLY_PENDING_PACK_CHANGE = "ไม่สามารถแก้ไขได้เนื่องจาก ไม่ได้อยู่ในสถานะที่แก้ไขได้";
        public static readonly string SEGMENT_NOT_FOUND = "Segment not found. ";






    }
}




