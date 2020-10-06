using SenaYazilim.OgrenciTakip.Model.Entities.Base.Interfaces;
using System;
using System.Linq;

namespace SenaYazilim.OgrenciTakip.Bll.Functions
{
    #region Acıklama
    /*2 tane function ımız olacak.Bir tanesi EntityConvert diğeri EntityListConvert .
     * Convert işlemini yaparken;2 tane Entity e ihtiyacımız var.Bunlardan birtanesi  Kaynak Entity, diğeri ise HEdef Entity.
     * Yapacağımız işlem; bir tane Kaynak Entityimiz gelecek.Bu kaynak entity nin propertları arasında dolaşacağız ve o propertileri HEdefteki Entity nin propertıları ile karşılaştıracağız.Eğer aynı ise isimlerde ise; kaynaktaki value alacağız,hedefteki propertinin value suna atayacağız.
     * 
     * 
     * 
     * 
     * 
     * 3 tane şart var :Oluusturduğumuz class ın static olması lazım,oluşturacağımız Functionların static olması lazım,aynı zamanda functionlara vermiş olduğumuz ilk değişkenin de this ile tanımlanmıs olması lazım.
     */
    #endregion
    public static class Converts
    {
        public static  TTarget EntityConvert<TTarget>(this IBaseEntity source)   //kaynak= source , hedef =target diye tanımladık.
        {
            if (source == null) return default(TTarget);//eğer kaynağımız yoksa hedefimiz null değer geriye döndürüyor.
            #region Acıklama
            /*kaynaktan alacağımız veriyi hedefe göndereceğiz.Ancak henüz ortada bir hedefimiz yok.Yani hedef entity miz yok.
                Ne yapmamız lazım? TTragetten bir tane instance üretmemiz lazım ki bir hedef entitymiz olsun.*/ 
            #endregion

            var hedef = Activator.CreateInstance<TTarget>();//Hedef entity miz olan TTarget ten bir tane instance üretmiş olduk.
            //şimdi hem kaynak hem de hedef entitylerimizin propertilerine ulaşmamız lazım.
            var kaynakProp = source.GetType().GetProperties(); //Kaynak entity mizin propertilerine ulasmıs olduk.
            var hedefProp = typeof(TTarget).GetProperties();//Generic sınıfların propertilerine ulasmak için typeof yaptık.
           //şimdi bunları karşılaştırarak hedef entity imizi olusturalım.

            foreach (var kp in kaynakProp)
            {
                var value = kp.GetValue(source); //kaynak propertinin değerine ulaşmış oluyoruz.
                var hp = hedefProp.FirstOrDefault(x => x.Name == kp.Name);//Hedef propertiye ulasmaya çalısıyoruz.Nasıl ulaşıyoruz?Diyoruz ki gelen kaynakpropertinin ismini al ve hedef propertinin arasında bunu ara.eğer burda bulabiliyorsan hp ye at  bulamazsan burası null gelmiş olacak.
                //bu şekilde hedef propertiye ulaşmıs olduk.
                if (hp != null)  //eğer hp null ise hedef propertiye value eklemiş olacağız.
                    hp.SetValue(hedef, ReferenceEquals(value, "") ? null : value);
            }

            return hedef; 
        }
    }
}
