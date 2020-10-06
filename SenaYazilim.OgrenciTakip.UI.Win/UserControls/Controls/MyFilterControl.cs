using System;
using DevExpress.XtraEditors;
using SenaYazilim.OgrenciTakip.UI.Win.Interfaces;
using System.ComponentModel;

namespace SenaYazilim.OgrenciTakip.UI.Win.UserControls.Controls
{
    [ToolboxItem(true)]
    public class MyFilterControl : FilterControl ,IStatusBarAciklama
    {
        public MyFilterControl()
        {
            ShowGroupCommandsIcon = true;
        }
        public string StatusBarAciklama { get; set; } = "Filtre Metni Giriniz.";
    }
}
