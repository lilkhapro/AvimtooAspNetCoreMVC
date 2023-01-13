using WebAppVide.Models;

namespace WebAppVide.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Pie> PiesOfTheWeek { get; set; }
        public string Message { get; set; }

        public HomeViewModel(IEnumerable<Pie> piesOfTheWeek, string v)
        {
            this.PiesOfTheWeek = piesOfTheWeek;
            this.Message = v;
        }
    }
}
