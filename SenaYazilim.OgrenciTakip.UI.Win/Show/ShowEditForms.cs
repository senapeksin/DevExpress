using SenaYazilim.OgrenciTakip.Common.Enums;
using SenaYazilim.OgrenciTakip.UI.Win.Forms.BaseForms;
using SenaYazilim.OgrenciTakip.UI.Win.Show.Interfaces;
using System;

namespace SenaYazilim.OgrenciTakip.UI.Win.Show
{
    public class ShowEditForms<TForm> : IBaseFormShow where TForm : BaseEditForm 
    {
        public long ShowDialogEditForm(KartTuru kartTuru, long id)//,params object[] prm)
        {
            //Yetki Kontrolü

            using (var frm = (TForm)Activator.CreateInstance(typeof(TForm)))
            {
                frm.BaseIslemTuru = id > 0 ? IslemTuru.EntityUpdate : IslemTuru.EntityInsert;
                frm.Id = id;
                frm.Yukle();//formumuzu çalıstırıyoruz.
                frm.ShowDialog();
                return frm.RefreshYapilacak ? frm.Id : 0;//eğer refresh yapılacaksa (true olarak  dönmüşse) o zaman sen bu formun ID sini geri gönder.Değilse 0 olarak geri gönder.

            }
        }
        public long ShowDialogEditForm(KartTuru kartTuru, long id,params object[] prm)
        {
            //Yetki Kontrolü

            using (var frm = (TForm)Activator.CreateInstance(typeof(TForm), prm))
            {
                frm.BaseIslemTuru = id > 0 ? IslemTuru.EntityUpdate : IslemTuru.EntityInsert;
                frm.Id = id;
                frm.Yukle();//formumuzu çalıstırıyoruz.
                frm.ShowDialog();
                return frm.RefreshYapilacak ? frm.Id : 0;//eğer refresh yapılacaksa (true olarak  dönmüşse) o zaman sen bu formun ID sini geri gönder.Değilse 0 olarak geri gönder.

            }
        }







    }
}
