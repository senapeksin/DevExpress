using System;

namespace SenaYazilim.Dal.Interfaces
{
    //IUnitOfWork 'un sağladığı avantaj :yapmış olduğumuz çeşitli işlemleri tek seferde database e göndermeye yarıyor.
    //Bu interface de biz Repository e gelen talepleri database e göndereceğiz.
    //Bu yüzden IRepository e buradan ulaşabilmemiz lazım.
    public interface IUnitOfWork<T> : IDisposable where T : class
    {
        IRepository<T> Rep { get; }  // IRepository e ulaşabilmiş olacağız
        bool Save();

    }
}
