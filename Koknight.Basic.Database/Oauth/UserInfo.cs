/*************************************************************************************
 *
 * File name:   UserInfo.cs
 * Description: 
 * 
 * Version：  V1.0
 * Creator  Eason Huang
 * Create time：  2024/11/16 10:09
 * ======================================
*************************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koknight.Basic.Database.Oauth
{
    [Table("userinfo")]
    public class UserInfo
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Account { get; set; }

        public string Name { get; set; }    

        public string Email { get; set; }   

        public string Mobile { get; set; }  

        public string Role { get; set; }
    }
}
