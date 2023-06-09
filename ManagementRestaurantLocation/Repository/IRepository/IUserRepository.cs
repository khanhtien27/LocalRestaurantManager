﻿using ManagementRestaurantLocation.Models;
using ManagementRestaurantLocation.Models.ModelDTO;

namespace ManagementRestaurantLocation.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string username);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<LocalUser> Register(RegisterationRequestDTO registerationRequestDTO);


    }
}
