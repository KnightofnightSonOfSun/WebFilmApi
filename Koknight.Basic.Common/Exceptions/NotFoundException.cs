/*************************************************************************************
 *
 * File name:   NotFoundException.cs
 * Description: 
 * 
 * Version：  V1.0
 * Creator  Eason Huang
 * Create time：  2024/11/17 16:17
 * ======================================
*************************************************************************************/
namespace Koknight.Basic.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException() : base("Information not found!")
        { 
        }

        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        { 
        }
    }
}
