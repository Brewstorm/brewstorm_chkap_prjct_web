using CheckAppCore.DTO;
using CheckAppCore.Models;
using CheckAppCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckAppCore.Services
{
    public class LoginService
    {
        private readonly UserRepository _userRepository;
        public LoginService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool RegisterUser(RegisterModel registerModel)
        {
            return _userRepository.RegisterUser(registerModel).Result;
        }

        public UserDataDTO GetUserData(string subject)
        {
            var user = _userRepository.GetUserFromEmailOrOauthID(subject).Result;

            if (user == null)
                return new UserDataDTO();

            return new UserDataDTO
            {
                Name = user.PersonalInfo.Name,
                Email = user.EmailAddress,
                PhotoUrl = user.PersonalInfo.SrcPhoto ?? "/images/common/ic_account_circle_black.png",
                UserType = user.UserRoles.FirstOrDefault()?.RoleID
            };
        }
    }
}
