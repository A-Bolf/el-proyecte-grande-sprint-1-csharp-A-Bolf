using System.Collections.Generic;
using SitRep.Models;

namespace SitRep.DAL;

public interface ITicketService
{
    public IEnumerable<Ticket> GetAll();
    public Ticket GetById(int id);
    public void Add(Ticket ticket);
    public void Update(Ticket ticket);
    public void Delete(int id);
}