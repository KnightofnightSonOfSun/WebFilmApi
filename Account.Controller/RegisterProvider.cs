using WebFilmApi.Account.Logic;
using Autofac;
using Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFilmApi.Account.Model.Response;
using DB.Mysql.Account.Logic;
using DB.Mysql.Account.Logic.Interface;
using DB.Mysql.Account.Model.Output;

namespace WebFilmApi.Account.Controller
{
    public class RegisterProvider : IRegister
    {
        public void Register(ContainerBuilder builder)
        {
            builder.RegisterType<SearchTeacherManager>().As<ILogicManager<SearchTeacherResponse, string>>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            builder.RegisterType<SearchTeacherByNameCommand>().As<IMysqlCommand<List<TeacherInfoOutput>, string>>();
        }
    }
}
