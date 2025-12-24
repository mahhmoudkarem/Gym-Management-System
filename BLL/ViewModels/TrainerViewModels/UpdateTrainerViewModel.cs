using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels.TrainerViewModels
{
    public class UpdateTrainerViewModel
    {
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        [DataType(DataType.EmailAddress)]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Email Must Be Between 5 And 100 Char")]


        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Phone Is Required")]
        [Phone(ErrorMessage = "Invalid Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Phone Must Be Valid Egypyion Phone Number")]
        public string Phone { get; set; } = null!;
        [Required(ErrorMessage = "Date Of Birth Is Required")]
        [DataType(DataType.Date)]


        public int BuildingNumber { get; set; }
        [Required(ErrorMessage = "Street Is Required")]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "Street Must Be Between 1 And 30 Char")]

        public string Street { get; set; } = null!;
        [Required(ErrorMessage = "City Is Required")]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "City Must Be Between 1 And 30 Char")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "City Contain Only Latters And Spaces")]

        public string City { get; set; } = null!;
        [Required(ErrorMessage = "Specialties Is Required")]

        public DAL.Entities.Enums.Specialties Specialties { get; set; }
    }
}
