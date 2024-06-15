using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace WorkoutManager.Models;

public class LiftTypeViewModel
{
    public List<Lift>? Lifts { get; set; }
    public SelectList? Types { get; set; }
    public string? LiftType { get; set; }
    public string? SearchString { get; set; }
}
