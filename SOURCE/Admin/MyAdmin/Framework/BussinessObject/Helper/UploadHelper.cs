using BussinessObject.Helper.Contract;
using BussinessObject.Models;
using BussinessObject.PaymentModule.Models;
using BussinessObject.UploadService;

namespace BussinessObject.Helper
{
    public class UploadHelper : IUploadHelper
    {
        /// <summary>
        /// Gọi service 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public UploadImageResponse UploadImage(UploadModel model)
        {
            var response = new UploadImageResponse { IsSuccess = false, Message = ""};
            var uploadService = new FileUploadSvc();
            var myValue = uploadService.UploadImage(TypeImageUploadEnum.Avatar, model.Path, model.ByteArray);
            if (myValue != null)
            {
                response.IsSuccess = myValue.IsSuccess;
                response.Message = myValue.Message;
                response.ImagePath = myValue.ImagePath;
            }

            return response;
        }

        public UploadImageResponse UploadImage_V2(UploadModel model, int width)
        {
            var response = new UploadImageResponse { IsSuccess = false, Message = "" };
            var uploadService = new FileUploadSvc();
            var myValue = uploadService.UploadImage_V2(TypeImageUploadEnum.Avatar, model.Path, model.ByteArray, width);
            if (myValue != null)
            {
                response.IsSuccess = myValue.IsSuccess;
                response.Message = myValue.Message;
                response.ImagePath = myValue.ImagePath;
            }

            return response;
        }

    }
}
