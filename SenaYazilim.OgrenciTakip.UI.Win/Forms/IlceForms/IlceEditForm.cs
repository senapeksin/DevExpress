using SenaYazilim.OgrenciTakip.Bll.General;
using SenaYazilim.OgrenciTakip.Common.Enums;
using SenaYazilim.OgrenciTakip.Model.Entities;
using SenaYazilim.OgrenciTakip.UI.Win.Forms.BaseForms;
using SenaYazilim.OgrenciTakip.UI.Win.Functions;

namespace SenaYazilim.OgrenciTakip.UI.Win.Forms.IlceForms
{
    public partial class IlceEditForm : BaseEditForm
    {
        #region Variables
        private readonly long _ilId;
        private readonly string _ilAdi; 
        #endregion

        public IlceEditForm(params object[] prm)
        {
            InitializeComponent();

            _ilId = (long)prm[0];
            _ilAdi = prm[1].ToString();

            DataLayoutControl = myDataLayoutControl;
            Bll = new IlceBll(myDataLayoutControl);
            BaseKartTuru = KartTuru.Ilce;
            EventsLoad();

        }



        protected internal override void Yukle()
        {
            OldEntity = BaseIslemTuru == IslemTuru.EntityInsert ? new Ilce() : ((IlceBll)Bll).Single(FilterFunctions.Filter<Ilce>(Id));
            NesneyiKontrollereBagla();
            Text = Text + $" -( {_ilAdi} )";  //İlçe Kartları -(İl Adı)


            if (BaseIslemTuru != IslemTuru.EntityInsert) return;
            txtKod.Text = ((IlceBll)Bll).YeniKodVer(x => x.IlId == _ilId);
            //Bizim göndermiş olduğumuz _ilId ye bağlı olan İlin  ilçelerini çek ve içersindeki en büyük kodu al 1 arttırıp text e ata.
            txtIlceAdi.Focus();

        }

        protected override void NesneyiKontrollereBagla()
        {
            var entity = (Ilce)OldEntity;


            txtKod.Text = entity.Kod;
            txtIlceAdi.Text = entity.IlceAdi;
            txtAciklama.Text = entity.Aciklama;
            tglDurum.IsOn = entity.Durum;
        }


        protected override void GuncelNesneOlustur()
        {
            CurrentEntity = new Ilce
            {
                Id = Id,
                Kod = txtKod.Text,
                IlceAdi = txtIlceAdi.Text,
                IlId = _ilId,
                Aciklama = txtAciklama.Text,
                Durum = tglDurum.IsOn

            };

            ButonEnabledDurumu();

        }


        protected override bool EntityInsert()
        {
            return ((IlceBll)Bll).Insert(CurrentEntity, x => x.Kod == CurrentEntity.Kod && x.IlId == _ilId);
            //Biz sana bir tane  _ilId gönderdik  sen git databasede bu ilId ile ilgili iliçeleri çek ve aynı zamanda bu ilçeler arasında sana gönderdiğimiz şu koda ait daha önce kullanılmıs bir ilçe var mı? yani bu kodu kullaanan bir ilçe var mı? demiş oluyoruz.Eğer varsa biza uyarı verecek daha önce bu kod kullanılmıstır diye.Yoksa o kodu kullanıp insert işlemini yapacak.
        }


        protected override bool EntityUpdate()
        {
            return ((IlceBll)Bll).Update(OldEntity,CurrentEntity, x => x.Kod == CurrentEntity.Kod && x.IlId == _ilId);
        }


    }
}