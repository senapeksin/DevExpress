using DevExpress.XtraEditors;
using System.Drawing;
using SenaYazilim.OgrenciTakip.UI.Win.Interfaces;
using System;
using System.ComponentModel;

namespace SenaYazilim.OgrenciTakip.UI.Win.UserControls.Controls
{
    [ToolboxItem(true)]
    public class MyCheckedComboBoxEdit:CheckedComboBoxEdit,IStatusBarKisaYol
    {
        public MyCheckedComboBoxEdit()
        {
            Properties.AppearanceFocused.BackColor = Color.LightCyan;
        }
        public override bool EnterMoveNextControl { get; set; } = true;
        public string StatusBarAciklama { get; set; }
        public string StatusBarKisayol { get; set; } = "F4:";
        public string StatusBarKisayolAciklama { get; set; }

    }
}
