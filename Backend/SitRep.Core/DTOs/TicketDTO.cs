using SitRep.Core.Entities;


public class TicketDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public StatusType Status { get; set; }
    public PriorityType Priority { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastUpdatedDate { get; set; }
    public User Assignee { get; set; }
    public int CreatorID { get; set; }
  
}