using System.Collections.Generic;
using SitRep.Core.Entities;
using SitRep.Models;

namespace SitRep.DAL;

public interface IUserService
{
    public IEnumerable<User> GetAll();
    public User GetById(long id);
    public User Register(UserDTO userDto);

    public User GetByName(string username);
    public bool VerifyPasswordHash(UserDTO userDto);
    public void UpdatePassword(UserDTO userDto);
    public bool VerifyUserExists(UserDTO userDto);
    
    public string CreateToken(UserDTO userDto);
    public void Delete(long id);
}