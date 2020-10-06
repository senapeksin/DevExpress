using System.ComponentModel;

namespace SenaYazilim.OgrenciTakip.Common.Enums
{
    //Tüm formlarda kullanacağımız kartların isimlerini burada kaydedeceğiz. "enum" yapısı sayıları anlamlı şekilde isimlendirerek kullanabilmeye izin verir.
    public enum KartTuru : byte
    {
        [Description("Okul Kartı")]
        Okul =1 ,
        [Description("İl Kartı")]
        Il =2,
        [Description("İlçe Kartı")]
        Ilce =3
    }
}
