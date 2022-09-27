﻿using ETradeStudy.Application.Abstractions.Services.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeStudy.Application.Abstractions.Services
{
    public interface IAuthService:IInternalAuthentication,IExternalAuthentication
    {
    }
}
