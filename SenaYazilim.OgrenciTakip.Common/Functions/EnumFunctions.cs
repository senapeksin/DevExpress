using System;
using System.ComponentModel;

namespace SenaYazilim.OgrenciTakip.Common.Functions
{
    public static class EnumFunctions
    {
        private static T GetAttribute<T>(this Enum value) where T : Attribute
        {
            if (value == null) return null;
            var memberInfo = value.GetType().GetMember(value.ToString());//buraya bir enum göndereceğiz bu enum ın descriptionların attributelerine ulaşacağız.Memberınfo yani üyelerini dolaşarak bu üyelerinden decriptionlarına ulaşıcaz ve gerekli value alıp geri göndermiş olacağız.
            var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);
            return (T)attributes[0];
        }
        public static string ToName(this Enum value)
        {
            if (value == null) return null;
            var attribute = value.GetAttribute<DescriptionAttribute>();
            return attribute == null ? value.ToString() : attribute.Description;
       }
    }
}
