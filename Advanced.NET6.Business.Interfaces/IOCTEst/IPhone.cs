﻿using Advanced.NET6.Framework.AutofaExt.AOP;
using Autofac.Extras.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced.NET6.Business.Interfaces
{
    //[Intercept(typeof(CusotmInterceptor))]
    //[Intercept(typeof(CusotmLogInterceptor))]
    //[Intercept(typeof(CusotmCacheInterceptor))]
    public interface IPhone
    {
        void Call();

        void Text();

        void Init123456678890(IPower iPower);

        IMicrophone Microphone { get; set; }
        IHeadphone Headphone { get; set; }
        IPower Power { get; set; }
        object QueryUser(object opara);
    }
}
