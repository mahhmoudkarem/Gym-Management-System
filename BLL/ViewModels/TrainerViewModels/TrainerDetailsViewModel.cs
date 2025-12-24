using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels.TrainerViewModels
{
    public class TrainerDetailsViewModel
    {
        public string Name { get; set; } = null!;
        public string Specialties { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string DateOfBirth { get; set; } = null!;
        public int BuildingNumber { get; set; }

        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;
    }
}
