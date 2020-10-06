using DevExpress.XtraDataLayout;
using DevExpress.XtraLayout;
using System.ComponentModel;
using System.Drawing;
using DevExpress.XtraLayout.Utils;
using System.Windows.Forms;

namespace SenaYazilim.OgrenciTakip.UI.Win.UserControls.Controls
{
    [ToolboxItem(true)]
    public class MyDataLayoutControl : DataLayoutControl
    {
        public MyDataLayoutControl()
        {
            /*normal şartlarda controller belirli bi düzen ile sıralanıyor ve tab tusuna bastığımızda sıralı bir şekilde controller arasında dolaşmış oluyoruz.
            Bizim belirlediğimiz index düzeyinde hareket etmesini istiyoruz*/
            OptionsFocus.EnableAutoTabOrder = false;//kontrol bizde olmuş oluyor.
        }
        protected override LayoutControlImplementor CreateILayoutControlImplementorCore()
        {
            return new MyLayoutControlImplementor(this);
        }
    }

    internal class MyLayoutControlImplementor : LayoutControlImplementor
    {
        public MyLayoutControlImplementor(ILayoutControlOwner owner) : base(owner)
        {

        }
        public override BaseLayoutItem CreateLayoutItem(LayoutGroup parent)
        {
            var item = base.CreateLayoutItem(parent);
            item.AppearanceItemCaption.ForeColor = Color.Maroon;
            return item;
        }
        public override LayoutGroup CreateLayoutGroup(LayoutGroup parent)
        {
            var grp = base.CreateLayoutGroup(parent);
            grp.LayoutMode = LayoutMode.Table;  //table layout olarak gelecek.

            grp.OptionsTableLayoutGroup.ColumnDefinitions[0].SizeType = SizeType.Absolute; //sabit
            grp.OptionsTableLayoutGroup.ColumnDefinitions[0].Width = 200;
            grp.OptionsTableLayoutGroup.ColumnDefinitions[1].SizeType = SizeType.Percent;//yüzde olarak ayarlayacağız diyoruz.
            grp.OptionsTableLayoutGroup.ColumnDefinitions[1].Width = 100;
            //yeni bir kolon/sütun ekleyeceğiz.Buraya toggleswitch controller inı bırakacağız.
            grp.OptionsTableLayoutGroup.ColumnDefinitions.Add(new ColumnDefinition { SizeType = SizeType.Absolute, Width = 99 });

            grp.OptionsTableLayoutGroup.RowDefinitions.Clear();  //satırları silcez.

            for (int i = 0; i < 9; i++)
            {
                grp.OptionsTableLayoutGroup.RowDefinitions.Add(new RowDefinition
                {
                    SizeType = SizeType.Absolute,
                    Height = 24
                });

                if (i + 1 != 9) continue; //10 a eşit olmadığı sürece
                grp.OptionsTableLayoutGroup.RowDefinitions.Add(new RowDefinition
                {
                    SizeType = SizeType.Percent,
                    Height = 100
                });
            }
            return grp;
        }
    }
}
