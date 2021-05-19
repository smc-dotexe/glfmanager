﻿using GLFManager.App.Repositories.Interfaces;
using GLFManager.Models.Entities;
using GLFManager.Models.ViewModels.Account;
using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GLFManager.App.Repositories
{
    public class UserAccountRepository : IUserAccountRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserAccountRepository(ApplicationDbContext dbContext, UserManager<User> userManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
        {
            _context = dbContext;
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var retrievedUser = await _context.Users.FirstAsync(user => user.Email == email);
            return retrievedUser;
        }

        public async Task<LoginResponseViewModel> GetAccessTokenForLogin(LoginViewModel loginInput)
        {
            using (var httpClient = new HttpClient())
            {
                var authority = _configuration.GetSection("Identity").GetValue<string>("Authority");

                var tokenResponse = await httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest 
                {
                    Address = authority + "/connect/token",
                    UserName = loginInput.Email,
                    Password
                })
            }
        }
    }
}