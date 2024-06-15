using System.ComponentModel.DataAnnotations;

namespace WorkoutManager.Models;

public class Lift
{
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required]
    [Display(Name = "Weight (kg)")]
    public decimal Weight { get; set; }

    [Required]
    public int Reps { get; set; }

    [Required]
    public string? Type { get; set; }

    [StringLength(50)]
    public string? Notes { get; set; }

    [Required]
    [Range(1, 10)]
    public int RPE { get; set; }

    [Required]
    [Display(Name = "Main Body Part")]
    public string? MainBodyPart { get; set; }
}
