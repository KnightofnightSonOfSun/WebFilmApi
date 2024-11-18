/*************************************************************************************
 *
 * File name:   AuthenticationException.cs
 * Description: 
 * 
 * Version：  V1.0
 * Creator  Eason Huang
 * Create time：  2024/11/17 16:21
 * ======================================
*************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koknight.Basic.Common.Exceptions
{
    public class AuthenticationException : Exception
    {
        public AuthenticationException() : base("Authentication failed!")
        {
        }

        public AuthenticationException(string message) : base(message)
        {
        }

        public AuthenticationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
