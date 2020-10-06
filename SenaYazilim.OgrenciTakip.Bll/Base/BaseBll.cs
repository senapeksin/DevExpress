using SenaYazilim.Dal.Interfaces;
using SenaYazilim.OgrenciTakip.Bll.Functions;
using SenaYazilim.OgrenciTakip.Bll.Interfaces;
using SenaYazilim.OgrenciTakip.Common.Enums;
using SenaYazilim.OgrenciTakip.Common.Functions;
using SenaYazilim.OgrenciTakip.Common.Message;
using SenaYazilim.OgrenciTakip.Model.Entities.Base;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace SenaYazilim.OgrenciTakip.Bll.Base
{
    public class BaseBll<T,TContext>:IBaseBll where T: BaseEntity where TContext:DbContext
    {
        private readonly Control _ctrl;
        private IUnitOfWork<T> _uow;

        protected BaseBll() { }

        protected BaseBll(Control ctrl)
        {
            _ctrl = ctrl;
        }

        protected TResult BaseSingle<TResult>(Expression<Func<T,bool>> filter, Expression<Func<T,TResult>> selector)
        {
            #region Amacı
            /*İlk önce tek bir kayıt  çekeceğimiz Bll function olusturduk geriye TResult döndürecek.Bu function Repository e ulasıp Find fonksiyonundan bir tane select yapmamıza sağlayacak*/
            #endregion
            GeneralFunctions.CreateUnitOfWork<T, TContext>(ref _uow); //bir tane uow create etmiş olduk.
            return _uow.Rep.Find(filter, selector);  
            //Repository'e ulaşıp Find fonksiyonundan bir tane select yapmmıs olduk.
            //bool tipinin nedeni, sonuc true ise kayıt vardır.False ise kayıt yok.
        }

        protected IQueryable<TResult> BaseList<TResult>(Expression<Func<T,bool>> filter, Expression<Func<T,TResult>> selector)
        {
            GeneralFunctions.CreateUnitOfWork<T, TContext>(ref _uow);
            return _uow.Rep.Select(filter, selector);
        }
        protected bool BaseInsert(BaseEntity entity ,Expression<Func<T,bool>> filter)
        {
            GeneralFunctions.CreateUnitOfWork<T, TContext>(ref _uow);
            //Validation
            _uow.Rep.Insert(entity.EntityConvert<T>());//Bu şekilde Repository e entityimizi(öğrencitakipcontext te tanımlanmıs olan entitylerden bir tanesini cast ederek göndermiş olduk.)
            return _uow.Save(); //kayıt başarılı ise true,değilse false.
        }

        protected bool BaseUpdate(BaseEntity oldEntity,BaseEntity currentEntity,Expression<Func<T,bool>> filter)
        {
            GeneralFunctions.CreateUnitOfWork<T, TContext>(ref _uow);
            //Validation 
            //Sadece değişen propertilerini update etmek istiyoruz.Bu nedenle bu propertilere ulaşmak lazım.
            var degisenAlanlar = oldEntity.DegisenAlanlariGetir(currentEntity);//oldEntitydeki propertileri al  current entitiydeki proplarla karsılastır value su farklı olan alanların bana liste olarak geri getir.
            if (degisenAlanlar.Count == 0) return true;
            _uow.Rep.Update(currentEntity.EntityConvert<T>(),degisenAlanlar);
            return _uow.Save();
        }

        protected bool BaseDelete(BaseEntity entity,KartTuru kartTuru,bool mesajVer=true)
        {
            GeneralFunctions.CreateUnitOfWork<T, TContext>(ref _uow);

            if (mesajVer)
                if (Messages.SilMesaj(kartTuru.ToName()) != DialogResult.Yes ) return false;

            _uow.Rep.Delete(entity.EntityConvert<T>());
            return _uow.Save();
        }


        protected string BaseYeniKodVer(KartTuru kartTuru,Expression<Func<T,string>> filter ,Expression<Func<T,bool>> where=null)
        {
            GeneralFunctions.CreateUnitOfWork<T, TContext>(ref _uow);
            return _uow.Rep.YeniKodVer(kartTuru, filter, where);
        }



        #region Dispose
        public void Dispose()
        {

            _ctrl?.Dispose();
            _uow?.Dispose();
          
        }
        #endregion

    }
}
