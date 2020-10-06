using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using SenaYazilim.OgrenciTakip.Bll.Interfaces;
using SenaYazilim.OgrenciTakip.Common.Enums;
using SenaYazilim.OgrenciTakip.Common.Message;
using SenaYazilim.OgrenciTakip.Model.Entities.Base;
using SenaYazilim.OgrenciTakip.UI.Win.Functions;
using SenaYazilim.OgrenciTakip.UI.Win.UserControls.Controls;
using System;
using System.Windows.Forms;

namespace SenaYazilim.OgrenciTakip.UI.Win.Forms.BaseForms
{
    public partial class BaseEditForm : RibbonForm
    {
        protected internal IslemTuru BaseIslemTuru;
        protected internal long Id;
        protected internal bool RefreshYapilacak;
        protected MyDataLayoutControl DataLayoutControl;
        protected MyDataLayoutControl[] DataLayoutControls;
        protected IBaseBll Bll;
        protected KartTuru BaseKartTuru;
        protected BaseEntity OldEntity;//entitynin eski değişmeden önceki halini elde etcez.
        protected BaseEntity CurrentEntity;
        protected bool IsLoaded;
        protected bool KayitSonrasiFormuKapat = true;

        public BaseEditForm()
        {
            InitializeComponent();
        }

        protected void EventsLoad()
        {
            //ButtonEvents
            foreach (BarItem button in ribbonControl.Items)
                button.ItemClick += Button_ItemClick;

            //FormEvents
            Load += BaseEditForm_Load;


            void ControlEvents(Control control)
            {
                control.KeyDown += Control_KeyDown;


                switch (control)
                {
                    case MyButtonEdit edt:
                        edt.IdChanged += Control_IdChanged;
                        edt.EnabledChange += Control_EnabledChange;
                        edt.ButtonClick += Control_ButtonClick;
                        edt.DoubleClick += Control_DoubleClick;
                        break;
                    case BaseEdit edt:
                        edt.EditValueChanged += Control_EditValueChanged;
                        break;
                    
                }
            }

            if (DataLayoutControls == null)
            {
                if (DataLayoutControl == null) return;
                foreach (Control ctrl in DataLayoutControl.Controls) //datalayoutconrol ile beraber gelen controllerin içersinde dolas.Ve şu function ı çalıstır ve o kontrolü buraya at.
                    ControlEvents(ctrl);
            }
            else  //birden fazla layout gelmesi durumunda
                foreach (var layout in DataLayoutControls)   //datalayoutlar içerisinde dolaş.
                    foreach (Control ctrl in layout.Controls)
                        ControlEvents(ctrl);

        }

        protected virtual void Control_EnabledChange(object sender, EventArgs e) { }
      

        private void Control_EditValueChanged(object sender, EventArgs e)
        {
            if (!IsLoaded) return;  //form yüklenmeden  tam IdChanged olayı gerçekleşiyorsa işlemi yapma.
            GuncelNesneOlustur();  //editteki value değiştiği zaman bu eventi tetiklemiş olsun.
        }

        private void Control_DoubleClick(object sender, EventArgs e)
        {
            SecimYap(sender);
        }

        private void Control_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            SecimYap(sender);
        }

        private void Control_IdChanged(object sender, IdChangedEventArgs e)
        {
            if (!IsLoaded) return;  //form yüklenmeden  tam IdChanged olayı gerçekleşiyorsa işlemi yapma.
            GuncelNesneOlustur();
        }

        private void Control_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
            if(sender is MyButtonEdit edt)
                switch (e.KeyCode)
                {
                    case Keys.Delete when e.Control && e.Shift: //contol shift+delete kombinasyonuna basıldığı zaman şu işlemi yap;
                        edt.Id =null;
                        edt.EditValue = null;
                        break;

                    case Keys.F4:
                    case Keys.Down when e.Modifiers == Keys.Alt:  //alt aşağı tusuna basıldığı zaman açılmasını istiyorum
                        SecimYap(edt);
                        break;
                }
        }

        private void BaseEditForm_Load(object sender, EventArgs e) //Formumuzun yüklenmesi aşamasında yapılacak olan işlemler
        {
            IsLoaded = true;
            GuncelNesneOlustur();
            //SablonYukle();
            //ButonGizleGoster();
            Id = BaseIslemTuru.IdOlustur(OldEntity);


           

            //Güncelleme Yapılacak

        }

        private void Button_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.Item == btnYeni)
            {
                //Yetki Kontrolü 
                BaseIslemTuru = IslemTuru.EntityInsert;
                Yukle();
            }
            else if (e.Item == btnKaydet)
                Kaydet(false);
            else if (e.Item == btnGerial)
                GeriAl();
            else if (e.Item == btnSil)
            {
                //Yetki Kontrolü
                EntityDelete();
            }
            else if (e.Item == btnCikis)
                Close();
        }

        protected virtual void SecimYap(object sender) { }

        private void EntityDelete()
        {
            throw new NotImplementedException();
        }

        private void GeriAl()
        {
            throw new NotImplementedException();
        }

        private bool Kaydet(bool kapanis)
        {
            bool KayitIslemi()
            {
                Cursor.Current = Cursors.WaitCursor;

                switch (BaseIslemTuru)
                {
                    case IslemTuru.EntityInsert:
                        if (EntityInsert())
                            return KayitSonrasiIslemler();
                        break;
                    case IslemTuru.EntityUpdate:
                        if (EntityUpdate())
                            return KayitSonrasiIslemler();
                        break;
                }


               bool KayitSonrasiIslemler()
                {
                    OldEntity = CurrentEntity;
                    RefreshYapilacak = true;
                    ButonEnabledDurumu();

                    if (KayitSonrasiFormuKapat)
                        Close();
                    else
                        BaseIslemTuru = BaseIslemTuru == IslemTuru.EntityInsert ? IslemTuru.EntityUpdate : BaseIslemTuru;

                    return true;
                }

                return false;

            }


            var result = kapanis ? Messages.KapanisMesaj() : Messages.KayitMesaj();


            switch (result)
            {
                case DialogResult.Yes:
                    return KayitIslemi();

                case DialogResult.No:
                    if (kapanis)
                        btnKaydet.Enabled = false;
                    return true;

                case DialogResult.Cancel:
                    return true;
            }

            return false;

        }

       

        protected virtual bool EntityInsert()
        {
            return ((IBaseGenelBll)Bll).Insert(CurrentEntity);
        }

        protected virtual bool EntityUpdate()
        {
            return ((IBaseGenelBll)Bll).Update(OldEntity, CurrentEntity);
        }


        protected internal virtual void Yukle() { }


        protected virtual void NesneyiKontrollereBagla() { }


        protected virtual void GuncelNesneOlustur() { }

        protected internal virtual void ButonEnabledDurumu() 
        {
            if (!IsLoaded) return;//form yüklenmemişse herhangi bir işlem yapma
            GeneralFunctions.ButtonEnabledDurumu(btnYeni, btnKaydet, btnGerial, btnSil, OldEntity, CurrentEntity);
        }


    }
}