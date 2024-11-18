/*************************************************************************************
 *
 * File name:   InternalServerErrorException.cs
 * Description: 
 * 
 * Version：  V1.0
 * Creator  Eason Huang
 * Create time：  2024/11/17 16:19
 * ======================================
*************************************************************************************/
namespace Koknight.Basic.Common.Exceptions
{
    public class InternalServerErrorException : Exception
    {
        public InternalServerErrorException() : base("Internal server error!")
        {
        }

        public InternalServerErrorException(string message) : base(message)
        {
        }

        public InternalServerErrorException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
