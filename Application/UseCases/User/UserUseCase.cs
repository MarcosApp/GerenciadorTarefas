using Domain.Contracts.Repositories.AddUser;
using Domain.Contracts.UseCases.AddUser;
using Domain.Entities;
using System.Collections.Generic;

namespace Application.UseCases.AddUser
{
    public class UserUseCase : IUserUseCase
    {
        private readonly IUserRepository _userRepository;

        public UserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public int AddUser(User user)
        {
            return _userRepository.AddUser(user);
        }

        public List<User> ListUser()
        {
            return _userRepository.ListUser();
        }

        public User ListUser(int id)
        {
            return _userRepository.ListUser(id);
        }
    }
}
