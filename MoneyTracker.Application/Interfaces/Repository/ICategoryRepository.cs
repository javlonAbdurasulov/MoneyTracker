﻿using MoneyTracker.Application.Interfaces.Base;
using MoneyTracker.Domain.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker.Application.Interfaces.Repository
{
    public interface ICategoryRepository:IGetByIdRepository<Category>
    {
    }
}
