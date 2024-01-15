using EssmaCars.Models;
using Microsoft.EntityFrameworkCore;

namespace SweetEssma.Models
{
    public class PieRepository : IPieRepository
    {
        private readonly SweetEssmaDbContext _sweetEssmaDbContext;

        public PieRepository(SweetEssmaDbContext sweetEssmaDbContext)
        {
            _sweetEssmaDbContext = sweetEssmaDbContext;
        }

        public IEnumerable<Pie> AllPies
        {
            get
            {
                return _sweetEssmaDbContext.Pies.Include(c => c.Category);

            }
        }

        public IEnumerable<Pie> PiesOfTheWeek
        {
            get
            {
                return _sweetEssmaDbContext.Pies.Include(c => c.Category)
                    .Where(p => p.IsPieOfTheWeek);

            }
        }

        public Pie? GetPieById(int pieid)
        {
            return _sweetEssmaDbContext.Pies.FirstOrDefault(p => p.PieId == pieid);
        }

        public IEnumerable<Pie> SearchPies(string searchQuery)
        {
            return _sweetEssmaDbContext.Pies.Where(p=>p.Name.Contains(searchQuery));    
        }
    }
}
