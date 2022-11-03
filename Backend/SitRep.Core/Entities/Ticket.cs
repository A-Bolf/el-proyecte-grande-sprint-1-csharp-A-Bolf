using SitRep.Core.Common;
using SitRep.Core.Domain;

namespace SitRep.Core.Entities;

public class Ticket: EntityBase
{
    public string Title { get; set; }
    public string Description { get; set; }
    public StatusType Status { get; set; }
    public PriorityType Priority { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastUpdatedDate { get; set; }
    public virtual User Assignee { get; set; }
    public int CreatorID { get; set; }


    public Ticket(TicketDTO dto)
    {
        Title = dto.Title;
        Description = dto.Description;
        Status = dto.Status;
        Priority = dto.Priority;
        DueDate = dto.DueDate;
        LastUpdatedDate = dto.LastUpdatedDate;
        Assignee = dto.Assignee;
        CreatorID = dto.CreatorID;
    }

    public Ticket()
    {
        
    }
}