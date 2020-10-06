using DevExpress.XtraBars;
using SenaYazilim.OgrenciTakip.UI.Win.Show.Interfaces;
using SenaYazilim.OgrenciTakip.Common.Enums;
using DevExpress.XtraGrid.Views.Grid;
using SenaYazilim.OgrenciTakip.UI.Win.Functions;
using System;
using System.Windows.Forms;
using SenaYazilim.OgrenciTakip.Model.Entities.Base;
using SenaYazilim.OgrenciTakip.Bll.Interfaces;
using DevExpress.XtraEditors;

namespace SenaYazilim.OgrenciTakip.UI.Win.Forms.BaseForms
{
    public partial class BaseListForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        protected IBaseFormShow FormShow;
        protected KartTuru BaseKartTuru;
        protected internal GridView Tablo;
        protected bool AktifKartlariGoster = true;
        protected internal bool MultiSelect;
        protected internal BaseEntity SelectedEntity;
        protected IBaseBll Bll; //IbaseBll interfaceini kullanarak buraya bll göndercem.
        protected ControlNavigator Navigator;
        protected internal long? SeciliGelecekId;

        public BaseListForm()
        {
            InitializeComponent();
        }

        private void EventsLoad()
        {
            #region baskabirkullanım
            //foreach (var item in ribbonControl.Items)
            //{
            //    switch (item)
            //    {
            //        case BarItem button:
            //            button.ItemClick += Button_ItemClick;
            //            break;
            //    }
            //} 
            #endregion
            //Button Events 
            foreach (BarItem button in ribbonControl.Items)
                button.ItemClick += Button_ItemClick;

            //Table Events
            Tablo.DoubleClick += Tablo_DoubleClick;
            Tablo.KeyDown += Tablo_KeyDown;


            //Form Events
            Shown += BaseListForm_Shown;
        }

        private void BaseListForm_Shown(object sender, EventArgs e)
        {
            Tablo.Focus();  //açılan formun içindeki tabloya focuslanmasını istiyoruz.
          //  ButonGizleGoster();
          //  SutunGizleGoster();

            if (IsMdiChild || SeciliGelecekId == null) return;
            Tablo.RowFocus("Id",SeciliGelecekId);
        }


        private void SutunGizleGoster()
        {
            throw new NotImplementedException();
        }

        private void ButonGizleGoster()
        {
            throw new NotImplementedException();
        }

        protected internal void Yukle()
        {
            DegiskenleriDoldur();
            EventsLoad();

            Tablo.OptionsSelection.MultiSelect = MultiSelect;
            Navigator.NavigatableControl = Tablo.GridControl;// artık buraya göndereceğimiz navigator hangisi ise aynı zamanda şu grid controlün navigator ısın demiş oluyoruz.


            Cursor.Current = Cursors.WaitCursor;
            Listele();
            Cursor.Current = DefaultCursor;


            //Güncellenecek!
        }


        protected virtual void DegiskenleriDoldur() { }
        

        protected virtual void ShowEditForm(long id)
        {
            var result = FormShow.ShowDialogEditForm(BaseKartTuru,id);
            ShowEditFormDefault(result);
        }

        
        protected void ShowEditFormDefault(long id)
        {
            #region açıklama
            //Kaydetme işlemini tamamladıktan sonra edit formu kapansın list forma odaklanıyor listformdaki veriler tekrar refresh edilip hangi kaydı eklemişsek o kayda focuslanmasını  istiyoruz.
            #endregion
            if (id <= 0) return;
            AktifKartlariGoster = true;
            FormCaptionAyarla();
            Tablo.RowFocus("Id",id);
        }


        private void EntityDelete()
        {
            throw new NotImplementedException();
        }
        private void SelectEntity()
        {
            if (MultiSelect)
            {
                //güncellencek.
            }
            else
                SelectedEntity = Tablo.GetRow<BaseEntity>();

            DialogResult = DialogResult.OK;//seçim yaptığımızı belirtmek için
            Close();
        }
        protected virtual void Listele() { }
        
        private void FiltreSec()
        {
            throw new NotImplementedException();
        }
        private void Yazdir()
        {
            throw new NotImplementedException();
        }
        private void FormCaptionAyarla()
        {
            if(btnAktifPasifKartlar==null)
            {
                Listele();
                return;
            }

            if(AktifKartlariGoster)
            {
                btnAktifPasifKartlar.Caption = "Pasif Kartlar";
                Tablo.ViewCaption = Text;
            }
            else
            {
                btnAktifPasifKartlar.Caption = "Aktif Kartlar";
                Tablo.ViewCaption = Text + " - Pasif Kartlar";
            }

            Listele();
        }

        private void IslemTuruSec()
        {
            if (!IsMdiChild)
            {
                //Güncellenecek!
                SelectEntity();
            }
            else
                btnDüzelt.PerformClick(); //eğer mdichil değilsen btndüzelt butonuna basılmış gibi yap.
        }



        private void Button_ItemClick(object sender, ItemClickEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor; //cursor un durumunu bekle olarak değiştir.


            if(e.Item==btnGonder) //btnGonder e tıklanılmış ise;
            {
                var link = (BarSubItemLink)e.Item.Links[0];
                link.Focus();
                link.OpenMenu();
                link.Item.ItemLinks[0].Focus();
            }
            else if(e.Item==btnStandartExcelDosyasi)
            {

            }
            else if(e.Item==btnFormatliExcelDosyasi)
            {

            }
            else if(e.Item==btnFormatsizExcelDosyasi)
            {

            }
            else if(e.Item==btnWordDosyasi)
            {

            }
            else if(e.Item==btnPdfDosyasi)
            {

            }
            else if(e.Item==btnTxtDosyasi)
            {

            }
            else if(e.Item==btnYeni)
            {
                //Yetki Kontrolü TODO!
                ShowEditForm(-1);
            }
            else if(e.Item==btnDüzelt)
            {
                ShowEditForm(Tablo.GetRowId());
            }
            else if (e.Item == btnSil)
            {
                //Yetki Kontrolü
                EntityDelete();
            }
            else if(e.Item==btnSec)
            {
                SelectEntity();
            }
            else if(e.Item==btnYinele)
            {
                Listele();
            }
            else if (e.Item == btnFiltrele)
            {
                FiltreSec();
            }
            else if (e.Item == btnKolonlar)
            {
                if (Tablo.CustomizationForm == null)
                    Tablo.ShowCustomization();
                else
                    Tablo.HideCustomization();
            }
            else if (e.Item == btnYazdir)
            {
                Yazdir();
            }
            else if (e.Item == btnCikis)
            {
                Close();
            }
            else if (e.Item == btnAktifPasifKartlar)
            {
                AktifKartlariGoster = !AktifKartlariGoster;
                FormCaptionAyarla();
            }

            Cursor.Current = DefaultCursor;
        }



        private void Tablo_DoubleClick(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            IslemTuruSec();
            Cursor.Current = DefaultCursor;
        }

       
        private void Tablo_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    IslemTuruSec();
                    break;
                case Keys.Escape:
                    Close();
                    break;
            }
        }




    }
}