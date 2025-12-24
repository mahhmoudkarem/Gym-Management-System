using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels.MemberViewModels
{
    public class HealthRecordViewModel
    {
        [Required(ErrorMessage = "Hight Is Required")]
        [Range(0.1,300,ErrorMessage ="Hight Must Be Greater Than 0 And Less Than 300")]
        public decimal Hight { get; set; }
        [Required(ErrorMessage = "Wight Is Required")]
        [Range(0.1, 200, ErrorMessage = "Wight Must Be Greater Than 0 And Less Than 300")]
        public decimal Wight { get; set; }
        [Required(ErrorMessage = "Blood Type Is Required")]
        [StringLength(3,ErrorMessage = "Blood Type must be 3 Char Or Less")]
        public string BloodType { get; set; } = null!;

        public string? Note { get; set; } 
    }
}
