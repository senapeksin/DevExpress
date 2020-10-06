using DevExpress.XtraEditors;
using DevExpress.Utils;
using System.Drawing;
using SenaYazilim.OgrenciTakip.UI.Win.Interfaces;
using System;
using System.ComponentModel;

namespace SenaYazilim.OgrenciTakip.UI.Win.UserControls.Controls
{
    [ToolboxItem(true)]
    public class MyToogleSwitch:ToggleSwitch,IStatusBarAciklama
    {
        public MyToogleSwitch()
        {
            Name = "tglDurum";
            Properties.OffText = "Pasif";
            Properties.OnText = "Aktif";
            Properties.AutoHeight = false;
            Properties.AutoWidth = true;  //genişliği otomatik olarak ayarlansın.
            Properties.GlyphAlignment = HorzAlignment.Far;   //textimiz toggleswitchin solunda olsun ayarı.
            Properties.Appearance.ForeColor = Color.Maroon;

        }
        public override bool EnterMoveNextControl { get; set; } = true;
        public string StatusBarAciklama { get; set; } = "Kartın Kullanım Durumunu Seciniz.";
    }
}
