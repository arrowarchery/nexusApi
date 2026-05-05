using NexusAPI.DTO.Achievement.Response;
using NexusAPI.DTO.Activity.Response; // Assure-toi d'avoir ce DTO de base

namespace NexusAPI.DTO.Session.Response;

public class GetSessionDto 
{
    public int Id { get; set; }
    public DateTime? DateTimeStart { get; set; }
    public DateTime? DateTimeEnd { get; set; }
    public string? Status { get; set; }
    
    // On renvoie la liste complète des activités liées
    // (Utile si tu décides d'avoir plusieurs activités par session)
    public List<GetActivityDto> Activities { get; set; } = new();

    // Optionnel : Garder ActivityName pour la compatibilité avec ton affichage actuel
    // Il pourra être mappé depuis Activities.FirstOrDefault()?.Name
    public string? ActivityName { get; set; }
    
    public List<GetAchievementDto> Achievements { get; set; } = new();
}