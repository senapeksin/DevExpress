using SenaYazilim.OgrenciTakip.Bll.General;
using SenaYazilim.OgrenciTakip.Model.Entities;
using SenaYazilim.OgrenciTakip.UI.Win.Forms.BaseForms;
using SenaYazilim.OgrenciTakip.UI.Win.Functions;
using SenaYazilim.OgrenciTakip.UI.Win.Show;
using SenaYazilim.OgrenciTakip.Common.Enums;

namespace SenaYazilim.OgrenciTakip.UI.Win.Forms.IlForms
{
    public partial class IlListForm : BaseListForm
    {
        public IlListForm()
        {
            InitializeComponent();
            Bll = new IlBll();
        }

        protected override void DegiskenleriDoldur()
        {
            Tablo = tablo;
            BaseKartTuru = KartTuru.Il;
            FormShow = new ShowEditForms<IlEditForm>();
            Navigator = longNavigator.Navigator;
        }
        protected override void Listele() //Bll katmanından çekmiş olduğumuz veriyi tablomuzun datasource üne ekleyeceğiz.
        {
            Tablo.GridControl.DataSource = ((IlBll)Bll).List(FilterFunctions.Filter<Il>(AktifKartlariGoster));//Listemizin içine bir tane filtre göndermiş olduk.
        }



    }
}