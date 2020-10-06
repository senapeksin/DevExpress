using SenaYazilim.OgrenciTakip.Bll.General;
using SenaYazilim.OgrenciTakip.Common.Enums;
using SenaYazilim.OgrenciTakip.Model.Entities;
using SenaYazilim.OgrenciTakip.UI.Win.Forms.BaseForms;
using SenaYazilim.OgrenciTakip.UI.Win.Functions;

namespace SenaYazilim.OgrenciTakip.UI.Win.Forms.IlForms
{
    public partial class IlEditForm : BaseEditForm
    {
        public IlEditForm()
        {
            InitializeComponent();

            DataLayoutControl = myDataLayoutControl;
            Bll = new IlBll(myDataLayoutControl);
            BaseKartTuru = KartTuru.Il;
            EventsLoad();
        }


        protected internal override void Yukle()
        {
            OldEntity = BaseIslemTuru == IslemTuru.EntityInsert ? new Il() : ((IlBll)Bll).Single(FilterFunctions.Filter<Il>(Id));
            
            NesneyiKontrollereBagla();


            if (BaseIslemTuru != IslemTuru.EntityInsert) return;   //entityinsert olmadığı müddetce aşağıdaki kodları çalıştırma diyoruz.
            txtKod.Text = ((IlBll)Bll).YeniKodVer();               //Artık kod üreteceğiz.
            txtIlAdi.Focus();                               

        }

        protected override void NesneyiKontrollereBagla()         //Gelen entity i  burada kontrollerimize bağlayacağız.
        {
            var entity = (Il)OldEntity;                    


            txtKod.Text = entity.Kod;
            txtIlAdi.Text = entity.IlAdi;
            txtAciklama.Text = entity.Aciklama;
            tglDurum.IsOn = entity.Durum;
        }


        protected override void GuncelNesneOlustur()
        //şu anda kullanmış olduğumuz yani database e gönderilecek olan entityden bir taane instance alacağız ve burdaki kontollerimizin mevcut değerlerini ona atmış olacağız.
        {
            CurrentEntity = new Il
            {
                Id = Id,
                Kod = txtKod.Text,
                IlAdi = txtIlAdi.Text,
                Aciklama = txtAciklama.Text,
                Durum = tglDurum.IsOn
                //Bu şekilde güncel nesnemizi olusturmuş olduk.Yani Bll de yapmış olduğumuz değişiklikleri hemen yakalayabilmiş olacağız.
            };

            ButonEnabledDurumu();

        }



    }
}