﻿using API.Database.Models;

namespace API.Services
{
    public interface IServiceChargeService
    {
        Task AddServiceCharge(ServiceCharge serviceCharge);
    }
}
