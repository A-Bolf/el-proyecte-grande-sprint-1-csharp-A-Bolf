using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SitRep.Core;
using SitRep.Core.Entities;
using SitRep.Core.UseCases.UpdatePassword;
using SitRep.DAL;
using SitRep.Models;

namespace SitRep.Controllers;

[Route("/api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<AuthController> _logger;
    private readonly ISitRepContext _context;

    public AuthController(IUserService userService, ISitRepContext sitRepContext, ILogger<AuthController> logger)
    {
        _logger = logger;
        _context = sitRepContext;
        _userService = userService;
    }
    [HttpPost("register")]
    public async Task<ActionResult<RegiterUserDTO>> Register(UserDTO userDto)
    {
        var user = _userService.Register(userDto);
        if (user == null) return BadRequest();
        RegiterUserDTO regiterUserDTO = new RegiterUserDTO

        {
            Id = user.Id,
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
    public async Task<ActionResult> UpdatePassword([FromBody]UpdatePasswordRequest request)
    {
        var handler = new UpdatePasswordHandler(_context);
        var response = handler.Handle(request);
        if (response.Failure)
        {
            _logger.LogError("Password Change Failed");

        }
        else
        {
            _logger.LogInformation("Password Change Successful");
        }

        return Ok(response);
    }

}