using SenaYazilim.OgrenciTakip.Bll.Base;
using SenaYazilim.OgrenciTakip.Common.Enums;
using SenaYazilim.OgrenciTakip.Data.Contexts;
using SenaYazilim.OgrenciTakip.Model.Entities;
using SenaYazilim.OgrenciTakip.Model.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace SenaYazilim.OgrenciTakip.Bll.General
{
    public class IlceBll: BaseBll<Ilce,OgrenciTakipContext>
    {
        public IlceBll() { }

        public IlceBll(Control ctrl) : base (ctrl) { }

        public BaseEntity Single(Expression<Func<Ilce, bool>> filter)
        {
            return BaseSingle(filter, x => x); //Ilceyi olduğu gibi geri getir diyorum.
        }

        public IEnumerable<BaseEntity> List(Expression<Func<Ilce, bool>> filter)
        {
            return BaseList(filter, x => x).OrderBy(x => x.Kod).ToList();
        }

        public bool Insert(BaseEntity entity, Expression<Func<Ilce, bool>> filter)
        {
            return BaseInsert(entity, filter);
        }

        public bool Update(BaseEntity oldEntity, BaseEntity currentEntity, Expression<Func<Ilce, bool>> filter)
        {
            return BaseUpdate(oldEntity, currentEntity, filter);
        }
        public bool Delete(BaseEntity entity)
        {
            return BaseDelete(entity, KartTuru.Ilce);
        }

        public string YeniKodVer(Expression<Func<Ilce, bool>> filter)//şu ilin ilçelerinin listesini al ve en büyüğünü getir diye bir filter söylememiz gerekiyor.
        {
            return BaseYeniKodVer(KartTuru.Ilce, x => x.Kod,filter);
        }



    }
}
