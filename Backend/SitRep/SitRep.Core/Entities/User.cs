using System.ComponentModel.DataAnnotations;
using SitRep.Core.Domain;

namespace SitRep.Core.Entities;

public class User : EntityBase
{
    public string UserName { get; set; }
    [Required]
    public string PasswordHash { get; set; }


}