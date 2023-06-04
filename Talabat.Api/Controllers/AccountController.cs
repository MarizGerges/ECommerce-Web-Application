using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.Api.DTOs;
using Talabat.Api.Errors;
using Talabat.Api.Extensions;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services;

namespace Talabat.Api.Controllers
{
    
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser>userManager , 
            SignInManager<AppUser> signInManager 
            ,ITokenService tokenService,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }


        [HttpPost("login")]
        public async Task <ActionResult<UserDTO>> Login(LoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return Unauthorized(new ApiResponse(401));
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));
            return Ok(new UserDTO { 
                DisplayName=user.DisplayName ,
                Email=user.Email , 
                Token= await _tokenService.CreateTokenAsync(user,_userManager)
            });
        }


        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO model)
        {
            if (CheckEmailExist(model.Email).Result.Value)
                return BadRequest(new ApiValidationErrorResponse() { Errors=new string[] {"this email is alredy in use"} });
            
                var user = new AppUser()
                {
                    DisplayName = model.DisplayName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    UserName = model.Email.Split('@')[0],

                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded) return BadRequest(new ApiResponse(400));
                return Ok(new UserDTO
                {
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                    Token = await _tokenService.CreateTokenAsync(user, _userManager)
                });
            
        }

        [HttpGet] //no name becouse it is only endpont with get
        [Authorize]
        public async Task<ActionResult<UserDTO>> GetCuttentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);
            return Ok(new UserDTO()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            });
        }

        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDTO>> GetUserAddress()
        {
            var user = await _userManager.FindUserWithAddressByEmailAsync(User);
            var address = _mapper.Map<Address, AddressDTO>(user.Address);
            return (address); 
        }

        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDTO>> UpdateUserAddress(AddressDTO updatedAdress)
        {
            var address=_mapper.Map<AddressDTO,Address>(updatedAdress);
            var user = await _userManager.FindUserWithAddressByEmailAsync(User);
            address.Id=user.Address.Id;
            user.Address=address;
            var result= await _userManager.UpdateAsync(user);
            if(!result.Succeeded)
                return BadRequest(new ApiResponse(400));
            return Ok(updatedAdress);



        }

        
        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExist(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null;
        }

    }
}
