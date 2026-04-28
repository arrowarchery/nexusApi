using System.ComponentModel.DataAnnotations;

namespace NexusAPI.Models;

public class Activity
{
    [Key] public int Id { get; set; }
    [Required] public string? Name { get; set; }
    [Required] public string? Description { get; set; }

    

    public List<Activity>? Activities { get; set; }
}