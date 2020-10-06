using SenaYazilim.OgrenciTakip.UI.Win.Forms.BaseForms;
using SenaYazilim.OgrenciTakip.Bll.General;
using SenaYazilim.OgrenciTakip.Common.Enums;
using SenaYazilim.OgrenciTakip.UI.Win.Show;
using SenaYazilim.OgrenciTakip.UI.Win.Functions;
using SenaYazilim.OgrenciTakip.Model.Entities;

namespace SenaYazilim.OgrenciTakip.UI.Win.Forms.OkulForms
{
    public partial class OkulListForm : BaseListForm
    {
        public OkulListForm()
        {
            InitializeComponent();
            Bll = new OkulBll(); //artık bu Bll ile BaseListForm da işlem yapabileceğiz.
        }

            protected override void DegiskenleriDoldur()
            {
                Tablo = tablo;
                BaseKartTuru = KartTuru.Okul;
                FormShow = new ShowEditForms<OkulEditForm>();
                Navigator = longNavigator.Navigator;
            }
            protected override void Listele() //Bll katmanından çekmiş olduğumuz veriyi tablomuzun datasource üne ekleyeceğiz.
            {
                Tablo.GridControl.DataSource = ((OkulBll)Bll).List(FilterFunctions.Filter<Okul>(AktifKartlariGoster));//Listemizin içine bir tane filtre göndermiş olduk.
            }



    }
}