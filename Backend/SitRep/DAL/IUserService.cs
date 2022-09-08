using System.Collections.Generic;
using SitRep.Models;

namespace SitRep.DAL;

public interface IUserService
{
    public IEnumerable<User> GetAll();
    public IEnumerable<string> GetAllUserNames();
    public User GetById(long id);
    public void Register(UserDTO userDto);
    public bool VerifyPasswordHash(UserDTO userDto);

    public bool VerifyUserExists(UserDTO userDto);
    
    public string CreateToken(UserDTO userDto);
    public void Update(UserDTO userDto);
    public void Delete(long id);
}