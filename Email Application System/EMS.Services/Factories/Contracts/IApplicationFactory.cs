﻿using EMS.Data.dbo_Models;
using System;

namespace EMS.Services.Factories.Contracts
{
    public interface IApplicationFactory
    {
        ApplicationDomain Create(string emailId, string userId, string EGN, string name, string phoneNum);
    }
}
