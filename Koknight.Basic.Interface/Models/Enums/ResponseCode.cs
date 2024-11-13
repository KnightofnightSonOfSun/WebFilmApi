/*************************************************************************************
 *
 * File name:   ResponseCode.cs
 * Description: 
 * 
 * Version：  V1.0
 * Creator  Eason Huang
 * Create time：  2024/11/13 22:48
 * ======================================
*************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koknight.Basic.Structure.Models.Enums
{
    public enum ResponseCode
    {
        Unknown = -1,
        Success = 200,
        ServerFail = 503,
    }
}
