﻿using GLFManager.App.Repositories.Interfaces;
using GLFManager.Models.Entities;
using GLFManager.Models.ViewModels.Account;
using GLFManager.Models.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GLFManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly IUserAccountRepository _userRepository;

        public UserAccountController(SignInManager<User> signInManager, IConfiguration configuration, UserManager<User> userManager, IUserAccountRepository userRepository)
        {
            _signInManager = signInManager;
            _configuration = configuration;
            _userManager = userManager;
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseViewModel>> Login([FromBody] LoginViewModel login)
        {
            var signInResult = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, false).ConfigureAwait(false);

            if (!signInResult.Succeeded)
                return BadRequest("Invalid username/password");

            var retrievedUser = await _userRepository.GetUserByEmail(login.Email);
            var tokenStatus = _userRepository.GetLoginTokenFromIdServer(login, new UserViewModel(retrievedUser));

            return Ok(tokenStatus);
        }

    }
}