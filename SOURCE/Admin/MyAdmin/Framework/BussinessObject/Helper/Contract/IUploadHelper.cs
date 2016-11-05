using BussinessObject.Models;
using BussinessObject.PaymentModule.Models;
using EntitiesObject.Entities.UserEntities;

namespace BussinessObject.Helper.Contract
{
    public interface IUploadHelper 
    {
        UploadImageResponse UploadImage(UploadModel model);
        UploadImageResponse UploadImage_V2(UploadModel model, int width);
    }
}
