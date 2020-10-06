using DevExpress.XtraEditors;
using System.Drawing;
using DevExpress.Utils;
using System.ComponentModel;
using SenaYazilim.OgrenciTakip.UI.Win.Interfaces;
using System;

namespace SenaYazilim.OgrenciTakip.UI.Win.UserControls.Controls
{
    [ToolboxItem(true)]
    public class MySpinEdit :SpinEdit,IStatusBarAciklama
    {
        public MySpinEdit() 
        {
            Properties.AppearanceFocused.BackColor = Color.LightCyan;
            Properties.AllowNullInput = DefaultBoolean.False;//boş geçilmesin.
            Properties.EditMask = "d"; //basamaklama yapmasın sürekli değeri artsın veya azalsın.
        }
        public override bool EnterMoveNextControl { get; set; } = true;

        public string StatusBarAciklama { get; set; }
    }
}
