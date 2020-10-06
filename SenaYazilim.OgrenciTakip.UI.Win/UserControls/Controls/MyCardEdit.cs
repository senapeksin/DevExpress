using DevExpress.Utils;
using DevExpress.XtraEditors.Mask;
using System.ComponentModel;

namespace SenaYazilim.OgrenciTakip.UI.Win.UserControls.Controls
{
    public class MyCardEdit: MyTextEdit
    {
        [ToolboxItem(true)]
        public MyCardEdit()
        {
            //kart numarası ortaya yazılmış olmasını sağlar.
            Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            //kart numarası belirli bir formattan oluşmasını istiyoruz.4 rakamdan sonra - atmasını sağlar.
            Properties.Mask.MaskType = MaskType.Regular;// regular: kendi belirlediğim sekilde olsun demek.
            Properties.Mask.EditMask =@"\d?\d\?\d?\d?-\d?\d\?\d?\d?-\d?\d?\d?\d?-\d?\d?\d?\d?"; //değerin bir rakam olduğunu , ? o rakamın boş girilebileceğini gösteriyor.
            Properties.Mask.AutoComplete = AutoCompleteType.None; //yazılmayan rakam olursa 0 olarak atama yapmasını engellemek için.
            StatusBarAciklama = "Kart No Giriniz.";
        }
    }
}
