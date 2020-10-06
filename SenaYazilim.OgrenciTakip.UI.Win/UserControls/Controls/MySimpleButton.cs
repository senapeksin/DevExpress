using DevExpress.XtraEditors;
using SenaYazilim.OgrenciTakip.UI.Win.Interfaces;
using System.Drawing;
using System;
using System.ComponentModel;

namespace SenaYazilim.OgrenciTakip.UI.Win.UserControls.Controls
{
    [ToolboxItem(true)]
    public class MySimpleButton:SimpleButton,IStatusBarAciklama
    {
        public MySimpleButton()
        {
            Appearance.ForeColor = Color.Maroon;    
        }
        public string StatusBarAciklama { get; set; }
    }
}
