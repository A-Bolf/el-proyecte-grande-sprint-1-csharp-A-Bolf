using System.Collections.Generic;
using SitRep.Core.Entities;
using SitRep.DAL;
using SitRep.Models;

namespace SitRep.DAL;

public class TicketService : ITicketService
{ 
    private readonly SitRepContext _context;
    
    public TicketService(SitRepContext context)
    {
        _context = context;
    }

    public Dictionary<StatusType, int> GetStatusCounts()
    { 
        var Tickets= _context.Tickets;
        return new Dictionary<StatusType, int>
        {
            { StatusType.OPEN, Tickets.Count(t => t.Status == StatusType.OPEN) },
            { StatusType.IN_PROGRESS, Tickets.Count(t => t.Status == StatusType.IN_PROGRESS) },
            { StatusType.RESOLVED, Tickets.Count(t => t.Status == StatusType.RESOLVED) },
            { StatusType.CLOSED, Tickets.Count(t => t.Status == StatusType.CLOSED) }
        };
    }

    public IEnumerable<Ticket> GetRecentUpdates()
    {
        //return last 3 tickets updated
        return _context.Tickets.OrderByDescending(t => t.LastUpdatedDate).Take(3);

    }

    public IEnumerable<Ticket> GetAll()
    {
        return _context.Tickets.ToList();
    }

    public Ticket GetById(int id)
    {
        return _context.Tickets.FirstOrDefault(ticket => ticket.Id == id);
    }

    public void Add(Ticket ticket)
    {
        _context.Add(ticket);
        _context.SaveChanges();
    }

    public void Update(Ticket ticket)
    {
        _context.Update(ticket);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var ticket = GetById(id);
        ticket.Assignees = null;
        _context.Tickets.Remove(ticket);
        _context.SaveChanges();
    }
}