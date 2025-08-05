using Microsoft.EntityFrameworkCore;
using VotacionesApi.Models;

namespace VotacionesApi.Services
{
    /// <summary>
    /// Clase de servicios para manejar operaciones relacionadas con candidatos.
    /// </summary>

    public class CandidateServices: ICandidateServices
    {
        // Inyección del contexto de la base de datos.
        private readonly ElectoralContext dbcontext;

        public CandidateServices(ElectoralContext context)
        {
            dbcontext = context;
        }

        public async Task<Candidate?> GetCandidateByIdAsync(int id)
        {
            return await dbcontext.Candidates.FindAsync(id);
        }
        public async Task<IEnumerable<Candidate>> GetAllCandidatesAsync()
        {
            return await dbcontext.Candidates.ToListAsync();
        }

        public async Task<Candidate> CreateCandidateAsync(Candidate candidate)
        {
            dbcontext.Candidates.Add(candidate);
            await dbcontext.SaveChangesAsync();
            return candidate;
        }



        public async Task<bool> DeleteCandidateAsync(int id)
        {
            var candidate = await dbcontext.Candidates.FindAsync(id);
            if (candidate == null)
            {
                return false;
            }
            dbcontext.Candidates.Remove(candidate);
            await dbcontext.SaveChangesAsync();
            return true;
        }
    }

}
