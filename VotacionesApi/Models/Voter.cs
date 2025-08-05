using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace VotacionesApi.Models;

public partial class Voter
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool HasVoted { get; set; } = false;

    [JsonIgnore]
    public virtual ICollection<Vote> Votes { get; set; } = new List<Vote>();
}
