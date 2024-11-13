/*************************************************************************************
 *
 * File name:   BaseResponse.cs
 * Description: 
 * 
 * Version：  V1.0
 * Creator  Eason Huang
 * Create time：  2024/11/13 22:44
 * ======================================
*************************************************************************************/
using Koknight.Basic.Structure.Models.Enums;

namespace Koknight.Basic.Structure.Models
{
    public class BaseResponse
    {
        public ResponseCode Code {  get; set; }

        public string Message { get; set; }
    }

    public class BaseResponse<T> : BaseResponse
    {
        public T Data { get; set; }
    }

    public static class ResponseExtend
    {
        public static BaseResponse SetSuccess(this BaseResponse response, string message = "")
        {
            BaseResponse result = new BaseResponse();
            result.Code = ResponseCode.Success;
            result.Message = message == string.Empty ? "Operate successfully" : message;
            return result;
        }
        public static BaseResponse<T> SetSuccess<T>(this BaseResponse<T> response, T data = default(T), string message = "")
        {
            response.Code = ResponseCode.Success;
            response.Message = message == string.Empty ? "Operate successfully" : message;
            response.Data = data;
            return response;
        }
        public static BaseResponse SetFail(this BaseResponse response, string message = "")
        {
            response.Code = ResponseCode.ServerFail;
            response.Message = message == string.Empty ? "Operate fail" : message;
            return response;
        }
        public static BaseResponse<T> SetFail<T>(this BaseResponse<T> response, string message = "", T data = default(T))
        {
            response.Code = ResponseCode.ServerFail;
            response.Message = message == string.Empty ? "Operate fail" : message;
            response.Data = data;
            return response;
        }

    }
}
