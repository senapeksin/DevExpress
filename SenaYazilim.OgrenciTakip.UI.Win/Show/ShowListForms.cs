﻿using SenaYazilim.OgrenciTakip.Common.Enums;
using SenaYazilim.OgrenciTakip.Model.Entities.Base;
using SenaYazilim.OgrenciTakip.UI.Win.Forms.BaseForms;
using System;
using System.Windows.Forms;

namespace SenaYazilim.OgrenciTakip.UI.Win.Show
{
    public class ShowListForms<TForm> where TForm : BaseListForm
    {
        public static void ShowListForm(KartTuru kartTuru)
        {
            //Yetki Kontrolü Yaapılacak
            var frm = (TForm)Activator.CreateInstance(typeof(TForm));
            frm.MdiParent = Form.ActiveForm;

            frm.Yukle();
            frm.Show();
        }


        public static BaseEntity ShowDialogListForm(KartTuru kartTuru, long? seciliGelecekId, params object[] prm)
        {
            //Yetki Kontrolü 


            using (var frm = (TForm)Activator.CreateInstance(typeof(TForm), prm))   //bir tane form olusturup instance nı almış olduk
            {
                frm.SeciliGelecekId = seciliGelecekId;
                frm.Yukle();
                frm.ShowDialog();


                return frm.DialogResult == DialogResult.OK ? frm.SelectedEntity : null;
            }
        }





    }
}
