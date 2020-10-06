using System;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using SenaYazilim.OgrenciTakip.UI.Win.Interfaces;
using DevExpress.XtraEditors.Repository;
using DevExpress.Utils;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraGrid.Views.Base;
using System.Drawing;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Registrator;
using System.ComponentModel;
//gridview olusturmayı pek anlamadım .:(

namespace SenaYazilim.OgrenciTakip.UI.Win.UserControls.Grid
{
    [ToolboxItem(true)]
    public class MyGridControl : GridControl
    {
        protected override BaseView CreateDefaultView()
        {
            var view = (GridView)CreateView("MyGridView");

            view.Appearance.ViewCaption.ForeColor = Color.Maroon;
            view.Appearance.HeaderPanel.ForeColor = Color.Maroon;
            view.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;
            view.Appearance.FooterPanel.ForeColor = Color.Maroon;
            view.Appearance.FooterPanel.Font = new Font(new FontFamily("Tahoma"),8.25f,FontStyle.Bold);

            view.OptionsMenu.EnableColumnMenu = false; //başlık menüsünü gizledik..
            view.OptionsMenu.EnableFooterMenu = false; //footer menü gizledik.
            view.OptionsMenu.EnableGroupPanelMenu = false; //gruplanıldığı zaman menü gizlemesini yaptık.

            //enter ile columnlar arası geçişi açtık.
            view.OptionsNavigation.EnterMoveNextColumn = true;

            view.OptionsPrint.AutoWidth = false;//yazıcıya bir belge göndereceksek column ayarlarının otomatik olarak değişmesini kapattık
            view.OptionsPrint.PrintFooter = false; //footer bölümleri yazıcıya gitmesini engellemek için.
            view.OptionsPrint.PrintGroupFooter = false;

            view.OptionsView.ShowViewCaption = true;
            view.OptionsView.ShowAutoFilterRow = true;
            view.OptionsView.ShowGroupPanel = false;
            view.OptionsView.ColumnAutoWidth = false;//columnlar bizim istediğimiz genişlikte kalması için.
            view.OptionsView.RowAutoHeight = true; //yazılanların aşağıya inmesini sağğladı.
            view.OptionsView.HeaderFilterButtonShowMode =FilterButtonShowMode.Button;


            //2 tane column eklemesini sağlar.
            var idColumn = new MyGridColumn();
            idColumn.Caption = "Id"; //Column başlığı
            idColumn.FieldName = "Id"; //verinin databasedeki column adı
            idColumn.OptionsColumn.AllowEdit = false;
            idColumn.OptionsColumn.ShowInCustomizationForm = false; //gözükmesini engellemiş oluyoruz.
            view.Columns.Add(idColumn);

            var kodColumn = new MyGridColumn();
            kodColumn.Caption = "Kod";
            kodColumn.FieldName = "Kod";
            kodColumn.OptionsColumn.AllowEdit = false;
            kodColumn.Visible = true;
            kodColumn.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center; 
            kodColumn.AppearanceCell.Options.UseTextOptions = true; 
            view.Columns.Add(kodColumn);

            return view;
        }
        protected override void RegisterAvailableViewsCore(InfoCollection collection)
        {
            base.RegisterAvailableViewsCore(collection);
            collection.Add(new MyGridInfoRegistrator()); 
        }
        private class MyGridInfoRegistrator : GridInfoRegistrator
        {
            public override string ViewName => "MyGridView";
            public override BaseView CreateView(GridControl grid) => new MyGridView(grid);
           
        }
    }
    public class MyGridView : GridView, IStatusBarKisaYol
    {
        #region Properties
        public string StatusBarAciklama { get; set; }
        public string StatusBarKisayol { get; set; }
        public string StatusBarKisayolAciklama { get; set; }
        #endregion
        public MyGridView() { }
        public MyGridView(GridControl ownerGrid) : base(ownerGrid) { }
        protected override void OnColumnChangedCore(GridColumn column)
        {
            base.OnColumnChangedCore(column);

            if (column.ColumnEdit == null) return;
            if (column.ColumnEdit.GetType() == typeof(RepositoryItemDateEdit))
            {
                column.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                ((RepositoryItemDateEdit)column.ColumnEdit).Mask.MaskType = MaskType.DateTimeAdvancingCaret; //otomatik olarak virgülden sonra ay kısmına geçsin
            }
        }
        protected override GridColumnCollection CreateColumnCollection()
        {
            return new MyGridColumnCollection(this);
        }
        private class MyGridColumnCollection : GridColumnCollection
        {
            public MyGridColumnCollection(ColumnView view) : base(view) { }
            protected override GridColumn CreateColumn()
            {
                var column = new MyGridColumn();
                column.OptionsColumn.AllowEdit = false;
                return column;
            }
        }
    }
    public class MyGridColumn : GridColumn, IStatusBarKisaYol
    {
        #region Properties
        public string StatusBarAciklama { get; set; }
        public string StatusBarKisayol { get; set; }
        public string StatusBarKisayolAciklama { get; set; }
        #endregion
    }
}
