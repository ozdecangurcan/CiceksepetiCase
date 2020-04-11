using System.Net;

namespace Ciceksepeti.Dto.ApiResponse
{
    public class ApiResponse
    {
        public HttpStatusCode ResultCode { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }
        public bool IsSuccess { get; set; }

        public ApiResponse(string message = null, HttpStatusCode? resultCode = null, dynamic data = null, bool isSuccess = false)
        {
            ResultCode = resultCode ?? HttpStatusCode.SeeOther;
            Data = data;
            Message = message;
            IsSuccess = isSuccess;
        }

        public static ApiResponse ReturnAsSuccess(object data = null, string message = null)
        {
            return new ApiResponse(message ?? "İşlem başarılı", HttpStatusCode.OK, data, true);
        }
        public static ApiResponse ReturnAsFail(object data = null, string message = null)
        {
            return new ApiResponse(message ?? "İşlem başarısız", HttpStatusCode.BadRequest, data, false);
        }
        public static ApiResponse ReturnAsNotFound(object data = null, string message = null)
        {
            return new ApiResponse(message ?? "Kayıt bulunamadı", HttpStatusCode.NotFound, data, false);
        }
        public static ApiResponse ReturnAsUnauthorized(object data = null, string message = null)
        {
            return new ApiResponse(message ?? "Bu işlem için yetkiniz bulunmamaktadır", HttpStatusCode.NoContent, data, false);
        }
        public static ApiResponse ReturnAsInformation(object data = null, string message = null)
        {
            return new ApiResponse(message, HttpStatusCode.NoContent, data, false);
        }
    }
}
