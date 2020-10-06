﻿using SenaYazilim.OgrenciTakip.Bll.General;
using SenaYazilim.OgrenciTakip.Common.Enums;
using SenaYazilim.OgrenciTakip.UI.Win.Forms.BaseForms;
using SenaYazilim.OgrenciTakip.UI.Win.Show;

namespace SenaYazilim.OgrenciTakip.UI.Win.Forms.IlceForms
{
    public partial class IlceListForm : BaseListForm
    {

        #region Variables
        private readonly long _ilId;
        private readonly string _ilAdi;

        #endregion

        public IlceListForm(params object[] prm)
        {
            InitializeComponent();
            Bll = new IlceBll();

            _ilId =(long) prm[0];
            _ilAdi = prm[1].ToString();
        }
        protected override void DegiskenleriDoldur()
        {
            Tablo = tablo;
            BaseKartTuru = KartTuru.Ilce;
            Navigator = longNavigator.Navigator;
            Text = Text + $" -( {_ilAdi} )";  //İlçe Kartları -(İl Adı)

        }
        protected override void Listele() 
        {
            Tablo.GridControl.DataSource = ((IlceBll)Bll).List(x=>x.Durum==AktifKartlariGoster&&x.IlId==_ilId);//hangi ili göndermişsek o ile bağlı aktif kartları  getir demiş olduk
        }

        protected override void ShowEditForm(long id)
        {
            var result = new ShowEditForms<IlceEditForm>().ShowDialogEditForm(KartTuru.Ilce,id,_ilId,_ilAdi);
            //işlem yapılacak 
        }

    }
}