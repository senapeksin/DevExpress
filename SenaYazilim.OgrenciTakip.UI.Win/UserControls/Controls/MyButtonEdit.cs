using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using SenaYazilim.OgrenciTakip.UI.Win.Interfaces;
using System.Drawing;
using System;
using System.ComponentModel;

namespace SenaYazilim.OgrenciTakip.UI.Win.UserControls.Controls
{

    [ToolboxItem(true)]          //bu yaptığımız buttonedit controller ını kullanabilmek için.
    public class MyButtonEdit : ButtonEdit, IStatusBarKisaYol
    {
        public MyButtonEdit()    //Varsayılan butonedit özelliklerine yeni işlevler kazandırmak için yapılan işlemler
        {
            Properties.TextEditStyle = TextEditStyles.DisableTextEditor; //Buttoneditimizin textedit kısmına yazı yazamayız.
            Properties.AppearanceFocused.BackColor = Color.LightCyan;  //Buttonedit imize focuslandığımızda arka plan renginin değişmesini sağlar.
        }
        //Enter' a  bastığımız zaman index sırasına göre bir sonraki indexteki control e focuslanmasını sağlar.
        //Default olarak false gelir.Override etmek lazım.
        public override bool EnterMoveNextControl { get; set; } = true;  
        //Controller arasında geçişte statuslarda, kısayolda ve kısayolaçıklamasında açıklama metni çıkmasını sağlamak için.
          //Property oluşturuyoruz.
        public string StatusBarAciklama { get; set; }
        public string StatusBarKisayol { get; set; } = "F4 :";
        public string StatusBarKisayolAciklama { get; set; }

        #region Events
        #region açıklama
        // public long? Id { get; set; } 
        /** Id Property bize arka planda her şey ID ile tutuluyor bu ıdler burda tutulacak.Id değerleri değişebilir.(Artvin işaretli iken Bursa seçebiliriz. Bu ID değişmeleri için bir event e ihtiyacımız var.)**/ 
        #endregion


        private long? _id; //yerel bir değişken olduğu için _ ile tanımlıyoruz. (sadece bu classtan ulaşılabileceği için)
        [Browsable(false)]   // id değerini buttonedit in properties özelliklerinin olduğu kısımdan kaldırmak için.
        public long? Id
        {
            get { return _id; } //return yerine => işareti de kullanabiliriz.  "get => _id;" 
            set
            {
                var oldValue = _id;  //id mizin mevcut değerini atacağız. henüz value ile gelen değeri almadık .önceki değerimizi almış oluyoruz.
                var newValue = value; //yeni gelen value değeri newValue oluyor.

                if (newValue == oldValue) return;
                _id = value;
                //ıdchanged event ini tetiklemek.
                IdChanged(this, new IdChangedEventArgs(oldValue, newValue));


                EnabledChange(this,EventArgs.Empty);  //enabledchange diye bir event olusturmus olduk.

            }
        }


        //IdChanged ifademizin null değer olmması için =delegate{} diyoruz. 
        public event EventHandler<IdChangedEventArgs> IdChanged = delegate { };

        public event EventHandler EnabledChange = delegate { };
        




        #endregion
    }

    // değişen eski değer ıd ile ve yeni değer id nin ikisinide tutabilmek için bir class tanımlıyorum.
    public class IdChangedEventArgs : EventArgs
    {
        public IdChangedEventArgs(long? oldValue, long? newValue) //2 parametresi var : old value and new value (long? => null değerler de alabilir.)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }

        public long? OldValue { get; } //set yok değer ataması yapmayacağız.
        public long? NewValue { get; }

    }

}
