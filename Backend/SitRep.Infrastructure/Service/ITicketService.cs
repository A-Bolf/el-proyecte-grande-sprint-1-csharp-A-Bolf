using System.Collections.Generic;
using SitRep.Core.Entities;
namespace SitRep.Infrastructure.Service;
public interface ITicketService
{
    public IEnumerable<Ticket> GetAll();
    public Ticket GetById(int id);
    public void Add(Ticket ticket);
    public void Update(Ticket ticket);
    public void Delete(int id);
    public Dictionary<StatusType,int> GetStatusCounts();
    
    public IEnumerable<Ticket> GetRecentUpdates();
}