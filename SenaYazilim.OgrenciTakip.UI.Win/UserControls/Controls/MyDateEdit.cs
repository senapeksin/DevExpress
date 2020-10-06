using DevExpress.XtraEditors;
using System.Drawing;
using DevExpress.Utils;
using DevExpress.XtraEditors.Mask;
using SenaYazilim.OgrenciTakip.UI.Win.Interfaces;
using System;
using System.ComponentModel;

namespace SenaYazilim.OgrenciTakip.UI.Win.UserControls.Controls
{
    [ToolboxItem(true)]
    public class MyDateEdit :DateEdit,IStatusBarKisaYol
    {
        public MyDateEdit()
        {
            Properties.AppearanceFocused.BackColor = Color.LightCyan;
            Properties.AllowNullInput =DefaultBoolean.False;                     //tarih alanında hiçbir şekilde null değer olmamalı.
            Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center; //tarih alanı ortada olsun
            Properties.Mask.MaskType =MaskType.DateTimeAdvancingCaret;           //tarih bölümünde gün/ay/yıl maskelemesi yapılması için.   
        }
        public override bool EnterMoveNextControl { get; set; } = true;
        public string StatusBarAciklama { get; set; } 
        public string StatusBarKisayol { get; set; } = "F4 : ";
        public string StatusBarKisayolAciklama { get; set; } = "Tarih Seç";
    }
}
