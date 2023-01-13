namespace WebAppVide.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly WebAppPieDbContext webAppPieDbContext;

        public CategoryRepository(WebAppPieDbContext webAppPieDbContext)
        {
            this.webAppPieDbContext = webAppPieDbContext;
        }

        public IEnumerable<Category> AllCategories
        {
            get
            {
                return webAppPieDbContext.Categories.OrderBy(p => p.CategoryName);
            }
        }
    }
}
