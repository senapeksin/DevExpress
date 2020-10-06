using SenaYazilim.OgrenciTakip.Common.Enums;
using SenaYazilim.OgrenciTakip.Model.Entities;
using SenaYazilim.OgrenciTakip.UI.Win.Forms.IlceForms;
using SenaYazilim.OgrenciTakip.UI.Win.Forms.IlForms;
using SenaYazilim.OgrenciTakip.UI.Win.Show;
using SenaYazilim.OgrenciTakip.UI.Win.UserControls.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenaYazilim.OgrenciTakip.UI.Win.Functions
{
    public class SelectFunctions : IDisposable    //yani seçimişlemi yaptıktan sonra dispose olmasını istiyoruz.
    {

        #region Variables
        private MyButtonEdit _btnEdit;           //tek kullanımlı yerlerde kullanacağız.  
        private MyButtonEdit _prmEdit;           //ikili kullanımlı yerlerde kullanacağız. 
        private KartTuru _kartTuru;

        #endregion

        public void Sec(MyButtonEdit btnEdit)
        {
            _btnEdit = btnEdit;
            SecimYap();
        }

        public void Sec(MyButtonEdit btnEdit, MyButtonEdit prmEdit)
        {
            _btnEdit = btnEdit;
            _prmEdit = prmEdit;
            SecimYap();
        }


        private void SecimYap()
        {
            switch (_btnEdit.Name)
            {
                case "txtIl":
                    {
                        #region açıklama
                        /*
                         ilk önce bir tane seçim yaptırdık.Secim yaptıktan sonra geriye bir tane entity geliyor.entitynin ıd sini tıklamış olduğumuz buttonedit in ıdsine , iladını da tıklamıs olduğumuz buttoneditin edit value suna  eşitlemiş oluyoruz.
                        */
                        #endregion
                        var entity = (Il)ShowListForms<IlListForm>.ShowDialogListForm(_kartTuru, _btnEdit.Id);  //bir tane seçim yapmıs olduk.
                        if (entity != null)
                        {
                            _btnEdit.Id = entity.Id;
                            _btnEdit.EditValue = entity.IlAdi;
                        }
                    }
                    break;

                case "txtIlce":
                    {
                        var entity = (Ilce)ShowListForms<IlceListForm>.ShowDialogListForm(_kartTuru, _btnEdit.Id,_prmEdit.Id,_prmEdit.Text);  
                        //kart turu ve secili gelecek ıd dışında bir de il Id ve İl adı göndermemiz gerekiyordu.Bunun için _prmedit.ıd ile il id sini ,_prmedit.text ile ise İl adını göndermiş olduk.
                        if (entity != null)   //entity null değilse
                        {
                            _btnEdit.Id = entity.Id;
                            _btnEdit.EditValue = entity.IlceAdi;
                        }
                    }
                    break;

            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
