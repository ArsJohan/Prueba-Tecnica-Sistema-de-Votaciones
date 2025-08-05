using System;
using System.Collections.Generic;

namespace VotacionesApi.Models;

public partial class Candidate
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Party { get; set; }

    public int? Votes { get; set; }

    public virtual ICollection<Vote> VotesNavigation { get; set; } = new List<Vote>();
}
