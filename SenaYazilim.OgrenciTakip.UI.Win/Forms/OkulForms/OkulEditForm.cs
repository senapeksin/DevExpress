using SenaYazilim.OgrenciTakip.Bll.General;
using SenaYazilim.OgrenciTakip.UI.Win.Forms.BaseForms;
using SenaYazilim.OgrenciTakip.Common.Enums;
using SenaYazilim.OgrenciTakip.Model.Dto;
using SenaYazilim.OgrenciTakip.UI.Win.Functions;
using SenaYazilim.OgrenciTakip.Model.Entities;
using System;
using DevExpress.XtraEditors;

namespace SenaYazilim.OgrenciTakip.UI.Win.Forms.OkulForms
{
    public partial class OkulEditForm : BaseEditForm
    {
        public OkulEditForm()
        {
            InitializeComponent();

            DataLayoutControl = myDataLayoutControl;
            Bll = new OkulBll(myDataLayoutControl);
            BaseKartTuru = KartTuru.Okul;
            EventsLoad();
        }

        protected internal override void Yukle()
        {
            OldEntity = BaseIslemTuru == IslemTuru.EntityInsert ? new OkulS() : ((OkulBll)Bll).Single(FilterFunctions.Filter<Okul>(Id));
            #region açklama
            /*İlk önce Entitymizin İnsert veya Update olması durumunu kontrol ediyoruz.Eğer insert se bizim entityden yani dto mizden birtane iinstance alsın,eğer update ise bu sefer  Bll e gitsin Single olarak bir veri çeksin.Ve filtre olarakda göndermriş olduğumuz Id si şu ıdye eşit olan kaydı çek getir demiş olduk*/
            //Yeni bir kayıt ekleniyor ise Dto den bir tane instance üretsin. Eğer yeni değilse o zaman bir tane entity databaseden çekmiş olacağız.Ve bu çektiğimiz entity ide oldEntity e atayacağız.Eğer daha sonra değişiklikler varsa onları daha sonra  currententity ile yakalayacağız.
            #endregion
            NesneyiKontrollereBagla();


            if (BaseIslemTuru != IslemTuru.EntityInsert) return;   //entityinsert olmadığı müddetce aşağıdaki kodları çalıştırma diyoruz.

            txtKod.Text = ((OkulBll)Bll).YeniKodVer();         //Artık kod üreteceğiz.
            txtOkulAdi.Focus();                                //OkulAdına focuslanmayı sağlayacağız.

        }

        protected override void NesneyiKontrollereBagla()      //Gelen entity i  burada kontrollerimize bağlayacağız.
        {
            var entity = (OkulS)OldEntity;                    //entitymizi olusturduk.Tipi OkulS oldu.


            txtKod.Text = entity.Kod;
            txtOkulAdi.Text = entity.OkulAdi;
            txtIl.Id = entity.IlId;
            txtIl.Text = entity.IlAdi;
            txtIlce.Id = entity.IlceId;
            txtIlce.Text = entity.IlceAdi;
            txtAciklama.Text = entity.Aciklama;
            tglDurum.IsOn = entity.Durum;
        }


        protected override void GuncelNesneOlustur()
        #region açıklama
        //şu anda kullanmış olduğumuz yani database e gönderilecek olan entityden bir taane instance alacağız ve burdaki kontollerimizin mevcut değerlerini ona atmış olacağız. 
        #endregion
        {
            CurrentEntity = new Okul
            {
                Id = Id,
                Kod = txtKod.Text,
                OkulAdi = txtOkulAdi.Text,
                IlId = Convert.ToInt64(txtIl.Id),
                IlceId = Convert.ToInt64(txtIlce.Id),
                Aciklama = txtAciklama.Text,
                Durum = tglDurum.IsOn
                //Bu şekilde güncel nesnemizi olusturmuş olduk.Yani Bll de yapmış olduğumuz değişiklikleri hemen yakalayabilmiş olacağız.
            };

            ButonEnabledDurumu();

        }


        protected override void SecimYap(object sender)
        {
            //seçim yaparken buttoneditlerde seçim yapmak için kullanıcaz bu yüzden kontrol koyalım birtane.
            if (!(sender is ButtonEdit)) return;  //buttonedit değilse herhangi bir işlem yapma.

            using (var sec = new SelectFunctions())
            {
                if (sender == txtIl)
                    sec.Sec(txtIl);

                else if (sender == txtIlce)
                    sec.Sec(txtIlce, txtIl);
            }
        }

        protected override void Control_EnabledChange(object sender, EventArgs e)
        {
            if (sender != txtIl) return; //il alanı değilse işlemi yapma
            txtIl.ControlEnabledChange(txtIlce);
        }






    }
}