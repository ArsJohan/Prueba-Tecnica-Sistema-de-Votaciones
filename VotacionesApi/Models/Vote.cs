using System;
using System.Collections.Generic;

namespace VotacionesApi.Models;

public partial class Vote
{
    public int Id { get; set; }

    public int? VoterId { get; set; }

    public int? CandidateId { get; set; }

    public virtual Candidate? Candidate { get; set; }

    public virtual Voter? Voter { get; set; }
}
