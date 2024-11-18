/*************************************************************************************
 *
 * File name:   SessionCodeUser.cs
 * Description: 
 * 
 * Version：  V1.0
 * Creator  Eason Huang
 * Create time：  2024/11/16 10:28
 * ======================================
*************************************************************************************/
using Koknight.Basic.Database.Oauth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koknight.Feature.Oauth
{
    public class SessionCodeUser
    {
        public DateTime ExpiresTime { get; set; }

        public UserInfo UserInfo { get; set; }
    }
}
