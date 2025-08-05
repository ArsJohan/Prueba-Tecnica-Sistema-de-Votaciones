using Microsoft.EntityFrameworkCore;
using VotacionesApi.Models;

namespace VotacionesApi.Services
{
    /// <summary>
    /// clase de servicios para manejar operaciones relacionadas con votantes.
    /// </summary>
    public class VoterServices: IVoterService
    {
        // Inyección del contexto de la base de datos.
        private readonly ElectoralContext dbcontext;
        public VoterServices(ElectoralContext context)
        {
            dbcontext = context;
        }
        public async Task<Voter> GetVoterByIdAsync(int id)
        {
            var voter = await dbcontext.Voters.FindAsync(id);
            if (voter == null)
            {
                throw new InvalidOperationException("No se encontró el votante.");
            }
            return voter;
        }
        public async Task<IEnumerable<Voter>> GetAllVotersAsync()
        {
            return await dbcontext.Voters.ToListAsync();
        }
        public async Task<Voter> CreateVoterAsync(Voter voter)
        {
            try
            {
                Voter? isVoter = await dbcontext.Voters.FirstOrDefaultAsync(v => v.Email == voter.Email);
                if (isVoter != null)
                {
                    throw new InvalidOperationException("El votante ya existe.");
                }
                dbcontext.Voters.Add(voter);
                await dbcontext.SaveChangesAsync();
                return voter;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No se pudo registrar el votante.", ex);
            }
        }
        public async Task<bool> DeleteVoterAsync(int id)
        {
            var voter = await dbcontext.Voters.FindAsync(id);
            if (voter == null)
            {
                return false;
            }
            dbcontext.Voters.Remove(voter);
            await dbcontext.SaveChangesAsync();
            return true;
        }
    }
}
