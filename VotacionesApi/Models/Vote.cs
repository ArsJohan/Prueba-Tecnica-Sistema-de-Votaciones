using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace VotacionesApi.Models;

public partial class Vote
{
    public int Id { get; set; }

    public int VoterId { get; set; }

    public int CandidateId { get; set; }

    [JsonIgnore]
    public virtual Candidate? Candidate { get; set; }
    [JsonIgnore]
    public virtual Voter? Voter { get; set; }
}
