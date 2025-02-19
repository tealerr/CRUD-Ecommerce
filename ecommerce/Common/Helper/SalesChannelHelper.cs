using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZortCommon.Helpers
{
    public static class SalesChannelHelper
    {
        public static int GetExternalSalesChannelIdByName(string channel)
        {
            if (string.IsNullOrEmpty(channel)) return 0;
            var normalizeChannel = channel.ToLower().Trim();
            if (normalizeChannel == "lazada")
                return (int)SalesChannel.Lazada;

            if (normalizeChannel == "shopee")
                return (int)SalesChannel.Shopee;
            if (normalizeChannel == "crm" || normalizeChannel == "crms")
                return (int)SalesChannel.CRM;
            if (normalizeChannel == "tiktok")
                return (int)SalesChannel.TikTok;

            return 0;
        }
        public static string GetExternalSaleChannelName(int salesChannelId)
        {
            if (salesChannelId == (int)SalesChannel.Shopee)
                return "shopee";
            else if (salesChannelId == (int)SalesChannel.Lazada)
                return "lazada";
            else if (salesChannelId == (int)SalesChannel.TikTok)
                return "tiktok";
            return string.Empty;
        }
        public static SalesChannelType SalesChannelTypeIdToSalesChannelType(int salesChannelTypeId)
        {
            foreach (var type in Enum.GetValues(typeof(SalesChannelType)))
            {
                if ((int)type == salesChannelTypeId)
                    return (SalesChannelType)type;
            }

            return SalesChannelType.Ddots;
        }
    }
    public enum SalesChannel
    {
       CRM = 1,
       Lazada = 2,
       Shopee = 3,
       Line = 4,
       Storehub = 5,
       TikTok = 6
    }
    public enum SalesChannelType
    {
        Ddots = 0,
        MarketPlace = 1,
        Ecommerce = 2,
        OwnChannel = 3,
    }
}