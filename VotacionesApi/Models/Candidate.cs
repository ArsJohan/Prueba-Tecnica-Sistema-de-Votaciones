using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace VotacionesApi.Models;

public partial class Candidate
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Party { get; set; }

    public int? Votes { get; set; }

    [JsonIgnore]
    public virtual ICollection<Vote> VotesNavigation { get; set; } = new List<Vote>();
}
