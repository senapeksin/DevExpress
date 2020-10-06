﻿using DevExpress.XtraEditors;
using SenaYazilim.OgrenciTakip.UI.Win.Interfaces;
using System.Drawing;
using System;
using System.ComponentModel;

namespace SenaYazilim.OgrenciTakip.UI.Win.UserControls.Controls
{

    [ToolboxItem(true)]
    public  class MyTextEdit:TextEdit,IStatusBarAciklama
    {
        public MyTextEdit()
        {
            Properties.AppearanceFocused.BackColor = Color.LightCyan;
            Properties.MaxLength = 50; 
        }
        public override bool EnterMoveNextControl { get; set; } = true;

        public string StatusBarAciklama { get; set; }
        
    }
}
