using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.UseCases.AddUser
{
    public interface IUserUseCase
    {
        int AddUser(User user);
        List<User> ListUser();

    }
}
