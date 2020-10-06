using SenaYazilim.OgrenciTakip.Model.Entities.Base.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SenaYazilim.OgrenciTakip.Model.Entities.Base
{
    public class BaseEntity :IBaseEntity
    {
        [Column(Order =0),Key,DatabaseGenerated(DatabaseGeneratedOption.None)]//Id column u database e create ederken 0.indexe (en başa) yerleştir.Otomatik artan şekilde ilerlemesini engelledik.
        public long Id { get; set; }
        [Column(Order = 1),Required,StringLength(20)]//null geçilemez ve max 20 karakter olacağını ayarladık
        public virtual string Kod { get; set; }
    }
}
