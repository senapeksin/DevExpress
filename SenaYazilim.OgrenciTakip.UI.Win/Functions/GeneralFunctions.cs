using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Grid;
using SenaYazilim.OgrenciTakip.Common.Enums;
using SenaYazilim.OgrenciTakip.Common.Message;
using SenaYazilim.OgrenciTakip.Model.Entities.Base;
using SenaYazilim.OgrenciTakip.UI.Win.UserControls.Controls;
using System;
using System.Windows.Forms;

namespace SenaYazilim.OgrenciTakip.UI.Win.Functions
{
    public static class GeneralFunctions
    {
        public static long GetRowId(this GridView tablo)
        {
            if (tablo.FocusedRowHandle > -1) return (long)tablo.GetFocusedRowCellValue("Id");
            //eğer focuslanan satır 0 ve yukarısı bir satırsa,indexine sahip ise; gel o satırın ID columnundaki değerini al long a cast et ve geri gönder.
            Messages.KartSecmemeUyariMesaji();
            return -1;
        }

        public static T GetRow<T>(this GridView tablo, bool mesajVer = true)
        {
            #region açıklama
            /*Eğer sectiğimiz satır 0dan büyük bir değer ise o zaman focuslandığım satırı  geriye gönder.Eğer değilse yani -1 ise (bos satır ise ) Kart secmediğini söylemek amaclı hata mesajını gönder.Ve geriye T nin default değeri ne ise onu döndürecek.*/ 
            #endregion
            if (tablo.FocusedRowHandle > -1) return (T)tablo.GetRow(tablo.FocusedRowHandle);

            if (mesajVer)
                Messages.KartSecmemeUyariMesaji();
            return default(T);

        }
        private static VeriDegisimYeri VeriDegisimYeriGetir<T>(T oldEntity, T currentEntity)
        {
            foreach (var prop in currentEntity.GetType().GetProperties())
            {
                if (prop.PropertyType.Namespace == "System.Collections.Generic") continue;
                var oldValue = prop.GetValue(oldEntity) ?? string.Empty;//prop un oldentitydeki değerini almıs olduk 
                //Şunu demek istedik:Eğer prop.GetVAlue(oldEntity) le gelen değer null ise o zaman null değilde string.Empty olarak bu oldvalue değerini al.
                var currentValue = prop.GetValue(currentEntity) ?? string.Empty;

                if (prop.PropertyType == typeof(byte[]))
                {
                    if (string.IsNullOrEmpty(oldValue.ToString()))
                        oldValue = new byte[] { 0 };
                    if (string.IsNullOrEmpty(currentValue.ToString()))
                        currentValue = new byte[] { 0 }; //default değeri olarak 0 verdik.
                    //şimdi bu iki alanı karşılayacağız.
                    if (((byte[])oldValue).Length != ((byte[])currentValue).Length)
                        return VeriDegisimYeri.Alan;
                }
                else if (!currentValue.Equals(oldValue))
                    return VeriDegisimYeri.Alan;
            }
            return VeriDegisimYeri.VeriDegisimiYok;
        }




        public static void ButtonEnabledDurumu<T>(BarButtonItem btnYeni, BarButtonItem btnKaydet, BarButtonItem btnGeriAl, BarButtonItem btnSil, T oldEntity, T currentEntity)
        {
            //veri değiişiminin nerde olduğunu yakalmamız lazım.
            var veriDegisimYeri = VeriDegisimYeriGetir(oldEntity, currentEntity);
            var butonEnabledDurumu = veriDegisimYeri == VeriDegisimYeri.Alan; //eğer verideğişimyeri alana eşitse butonenableddurumu true olacak.



            btnKaydet.Enabled = butonEnabledDurumu;//butonenableddurumu true ise yani veri değişmişse o zaman kaydet butonunun aktif olması lazım.
            btnGeriAl.Enabled = butonEnabledDurumu;
            btnYeni.Enabled = !butonEnabledDurumu;
            btnSil.Enabled = !butonEnabledDurumu;
        }

        public static long IdOlustur(this IslemTuru islemTuru, BaseEntity selectedEntity)
        {
            string SifirEkle(string deger)
            {
                if (deger.Length == 1)
                    return "0" + deger;
                return deger;
            }

            string UcBasamakYap(string deger)
            {
                switch (deger.Length)
                {
                    case 1:
                        return "00" + deger;
                    case 2:
                        return "0" + deger;
                }

                return deger;
            }

            string Id()
            {
                var yil = DateTime.Now.Date.Year.ToString();
                var ay = SifirEkle(DateTime.Now.Date.Month.ToString());
                var gun = SifirEkle(DateTime.Now.Date.Day.ToString());
                var saat = SifirEkle(DateTime.Now.Hour.ToString());
                var dakika = SifirEkle(DateTime.Now.Minute.ToString());
                var saniye = SifirEkle(DateTime.Now.Second.ToString());
                var milisaniye = UcBasamakYap(DateTime.Now.Millisecond.ToString());
                var random = SifirEkle(new Random().Next(0, 99).ToString());


                return yil + ay + gun + saat + dakika + saniye + milisaniye + random;
            }

            var id = Id();    //unutmusum
            return islemTuru == IslemTuru.EntityUpdate ? selectedEntity.Id : long.Parse(Id());
            #region açıklama
            //eğer işlem türü update ise(güncellenen bir değer ise) geriye gelen selectedEntity e Id sini gönder. Eğer değilse  olusturmus olduğumuz Id yi long a dönüştürüp geriye göndereceğiz. 
            #endregion
        }

        public static void ControlEnabledChange(this MyButtonEdit baseEdit, Control prmEdit)
        {
            switch (prmEdit)
            {
                case MyButtonEdit edt:  //eğer gelen prmedit olarak gelen control bir buttonedit ise  su işlemleri yap:
                    edt.Enabled = baseEdit.Id.HasValue&&baseEdit.Id>0;//bu parametre edt in enable durumunu baseEdit in Idsinde  herhamgi bi değer varsa true,yoksa false yap.
                    edt.Id = null;
                    edt.EditValue = null;
                    break;
                    #region açıklama
                    /*2 tane control göndereceğiz.baseedit olarak İl i, prmedit olarak İlçeyi göndermiş oluyoruz yani.
            Eğer ilcenin enable durumu İlin ıd değerinde eğer bir değer varsa o zaman ilçenin enable durumunu aç.Yoksa enable durumunu false yap ve içerisini boşalt.*/
        #endregion
            }
        }

        public static void RowFocus(this GridView tablo, string aranacakKolon, object aranacakDeger)
        {
            #region açıklama
            /*Amacımız;açılan listede göndereceğimiz Id nin hangi Ilde olduğunu bularak oraya focuslanmasını sağlamak. */
            #endregion
            var rowHandle = 0;

            for (int i = 0; i < tablo.RowCount; i++)
            {
                var bulunanDeger = tablo.GetRowCellValue(i, aranacakKolon);

                if (aranacakDeger.Equals(bulunanDeger))
                    rowHandle = i;
            }
            tablo.FocusedRowHandle = rowHandle;

        }
    }
}
