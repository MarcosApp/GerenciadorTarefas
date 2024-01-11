﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.Repositories.AddUser
{
    public interface IUserRepository
    {
        int AddUser(User user);
        List<User> ListUser();
    }
}
