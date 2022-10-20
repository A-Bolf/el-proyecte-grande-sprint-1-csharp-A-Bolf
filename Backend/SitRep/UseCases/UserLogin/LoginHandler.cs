using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using SitRep.Infrastructure.Persistence;


namespace SitRep.Core.UseCases.UserLogin;

public class LoginHandler:IRequestHandler<LoginRequest,Response<string>>
{
    private readonly ISitRepContext _context;
    private readonly IConfiguration _configuration;

    public LoginHandler(ISitRepContext context,IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;

    }
    public Response<string> Handle(LoginRequest message)
    {
        var user = _context.Users.ToList().FirstOrDefault(u => u.UserName == message.UserName);
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(message.Password);
        if (user == null)
        {
            return Response.Fail<string>("User not found");
        }

        if (!BCrypt.Net.BCrypt.Verify(message.Password,passwordHash))
        {
            return Response.Fail<string>("Incorrect Password");
        }
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.Role,"User")
        };

        var key = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token =new  JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        
        return Response.Ok(jwt);
    }
}