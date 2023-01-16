using Microsoft.EntityFrameworkCore;

namespace WebAppVide.Models
{
    public class PieRepository : IPieRepository
    {
        private readonly WebAppPieDbContext webAppPieDbContext;

        public PieRepository(WebAppPieDbContext webAppPieDbContext)
        {
            this.webAppPieDbContext = webAppPieDbContext;
        }

        public IEnumerable<Pie> AllPies {
                
            get
            {
                return webAppPieDbContext.Pies.Include(c => c.Category);
            }
        }

        public IEnumerable<Pie> PiesOfTheWeek
        {
            get
            {
                return webAppPieDbContext.Pies.Include(c => c.Category).Where(
                    p => p.IsPieOfTheWeek);
            }
        } 

        public Pie? GetPieById(int pieId)
        {
            return webAppPieDbContext.Pies.FirstOrDefault(p => p.PieId == pieId);   
        }

        public IEnumerable<Pie> SearchPies(string searchQuery)
        {
            return webAppPieDbContext.Pies.Where(p => p.Name.Contains(searchQuery));
        }
    }
} 
