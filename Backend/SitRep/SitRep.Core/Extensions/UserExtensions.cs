using System.Security.Cryptography;
using SitRep.Core.Entities;

namespace SitRep.Models;

public static class UserExtensions
{
    public static User FromDto(this UserDTO userDto)
    {
        var user = new User();
        user.UserName = userDto.UserName;
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
        return user;
    }
}