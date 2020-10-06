using DevExpress.XtraEditors;
using System.Drawing;
using DevExpress.Utils;
using SenaYazilim.OgrenciTakip.UI.Win.Interfaces;
using System;
using System.ComponentModel;

namespace SenaYazilim.OgrenciTakip.UI.Win.UserControls.Controls
{
    [ToolboxItem(true)]
    public class MyCalcEdit : CalcEdit, IStatusBarKisaYol
    {
        public MyCalcEdit()
        {
            Properties.AppearanceFocused.BackColor = Color.LightCyan;
            Properties.AllowNullInput = DefaultBoolean.False;            //bu alan null bir değer almasın.
            Properties.EditMask = "n2";
            /**rakamları maskelememiz lazım. virgülden sonra kuruş hanesinde 2 tane sıfır olmasını sağlayan hane olacak .aralarına nokta koyacak**/
        }
        public override bool EnterMoveNextControl { get; set; } = true;
        public string StatusBarAciklama { get; set; }
        public string StatusBarKisayol { get; set; } = "F4 :";
        public string StatusBarKisayolAciklama { get; set; } = "Hesap Makinesi";

    }
}
