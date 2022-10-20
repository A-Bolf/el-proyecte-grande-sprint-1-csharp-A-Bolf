using SitRep.Core.Domain;
using SitRep.Core.Entities;
using SitRep.Core.UseCases.RegisterUser;

namespace SitRep.Core.Extensions;

public static class UserExtensions
{
    public static User FromDto(this UserDTO userDto)
    {
        var user = new User();
        user.UserName = userDto.UserName;
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
        return user;
    }
    
    public static RegisterUserResponse ToRegisterUserResponse(this User user)
{
    Guard.Require(user != null, "User is required");
    return new RegisterUserResponse { Id= user.Id, UserName = user.UserName};
}
}