using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SitRep.Core.Entities;
using SitRep.DAL;
using SitRep.Models;

namespace SitRep.Controllers;

[Route("/api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<RegiterUserDTO>> Register(UserDTO userDto)
    {
        var user = _userService.Register(userDto);
        if (user == null) return BadRequest();
        RegiterUserDTO regiterUserDTO = new RegiterUserDTO

        {
            Id = user.id,
            UserName = user.UserName,
        };
        return (Ok(regiterUserDTO));
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(UserDTO userDto)
    {
        if (!_userService.VerifyUserExists(userDto))
        {
            return BadRequest("User Not Found!");
        }
        if (!_userService.VerifyPasswordHash(userDto))
        {
            return BadRequest("Wrong Password!");
        }

        var token = _userService.CreateToken(userDto);

        return Ok(token);
    }

    [HttpPost("UpdatePassword")]
    public async Task<ActionResult<User>> UpdatePassword([FromBody]UserDTO userDto)
    {
        _userService.UpdatePassword(userDto);
        return Ok(userDto.FromDto());
    }

}