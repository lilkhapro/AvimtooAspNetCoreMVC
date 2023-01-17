
using Microsoft.AspNetCore.Components;
using WebAppVide.Models;

namespace WebAppVide.Pages.App
{
    public partial class PieCard
    {
        [Parameter]
        public Pie? Pie { get; set; } = default;
    }
}
