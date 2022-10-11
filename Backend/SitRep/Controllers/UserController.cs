using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SitRep.Core;
using SitRep.Core.Entities;
using SitRep.Core.UseCases.RegisterUser;
using SitRep.Core.UseCases.UpdatePassword;
using SitRep.Core.UseCases.UserLogin;
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
    private IConfiguration _configuration;

    public AuthController(IUserService userService, ISitRepContext sitRepContext, ILogger<AuthController> logger,IConfiguration configuration)
    {
        _logger = logger;
        _context = sitRepContext;
        _userService = userService;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(RegisterUserRequest userDto)
    {
        RegisterUserHandler registerNewUserHandler = new RegisterUserHandler(_context);
        var response = registerNewUserHandler.Handle(userDto);
        if (response.Failure)
        {
            _logger.LogError(response.Error);
            return BadRequest(response.Error);
        }
        _logger.LogInformation("User registered");
        var registerUserResponse = response.Value.ToRegisterUserResponse();
        return (Ok(registerUserResponse));
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(LoginRequest request)
    {
        var handler = new LoginHandler(_context, _configuration);
        var response = handler.Handle(request);

        if (response.Failure)
        {
            return BadRequest(response);
        }

        return Ok(response.Value);
    }

    [Authorize]
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