using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using SitRep.Core.Entities;
using SitRep.Models;

namespace SitRep.DAL;

public class UserService : IUserService
{
    private readonly SitRepContext _context;
    private readonly IConfiguration _configuration;

    public UserService(SitRepContext context,IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public IEnumerable<User> GetAll()
    {
        return _context.Users.ToList();
    }

    public User Register(UserDTO userDto)
    {
        var user = userDto.FromDto();
        if (_context.Users.Any(u => u.UserName == user.UserName))
        {
            throw new Exception("username taken");
        }
        _context.Users.AddRange(user);
        _context.SaveChanges();
        return user;
    }

    public User GetByName(string username)
    {
        return _context.Users.First(u => u.UserName == username);
    }


    public bool VerifyPasswordHash(UserDTO userDto)
    {
        var user = GetByName(userDto.UserName);
        return BCrypt.Net.BCrypt.Verify(userDto.Password,user.PasswordHash);

    }

    public bool VerifyUserExists(UserDTO userDto)
    {
        var user = _context.Users.FirstOrDefault(user => user.UserName == userDto.UserName);
        return user != null;
    }
    

    public string CreateToken(UserDTO userDto)
    {
        var user = userDto.FromDto();
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Role,"User")
        };

        var key = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token =new  JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        
        return jwt;
    }

    public void UpdatePassword(UserDTO userDto)
    {
        var user = GetByName(userDto.UserName);
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
        _context.Update(user);
        _context.SaveChanges();
    }


    public void Delete(long id)
    {
        var user = GetById(id);
        _context.Users.Remove(user);
        _context.SaveChanges();
    }

    public User GetById(long id)
    {
        return _context.Users.FirstOrDefault(user => user.Id == id);
    }
    }