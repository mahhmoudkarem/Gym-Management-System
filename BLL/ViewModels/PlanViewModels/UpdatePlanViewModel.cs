using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels.PlanViewModels
{
    public class UpdatePlanViewModel
    {


        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Description Is Required")]
        [StringLength(200,MinimumLength =5, ErrorMessage = "Description Must Be Between 5 And 200 Char ")]
        public string Description { get; set; } = null!;
        [Required(ErrorMessage = "DurationDay Is Required")]
        [Range(1,365,ErrorMessage = "DurationDay Must Be Between 1 And 365 Days")]

        public int DurationDay { get; set; }
        [Required(ErrorMessage = "Price Is Required")]
        [Range(0.1, 10000, ErrorMessage = "Price Must Be Between 0 And 10000")]
        public decimal Price { get; set; }
    }
}
