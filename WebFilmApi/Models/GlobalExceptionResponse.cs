/*************************************************************************************
 *
 * File name:   GlobalExceptionResponse.cs
 * Description: 
 * 
 * Version：  V1.0
 * Creator  Eason Huang
 * Create time：  2024/11/16 15:03
 * ======================================
*************************************************************************************/
namespace WebFilmApi.Models
{
    public class GlobalExceptionResponse
    {
        public int StatusCode { get; set; }
        public string Message { get;set; }

        public string DetailedInformation { get; set; }
    }
}
