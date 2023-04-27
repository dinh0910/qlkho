using System.ComponentModel.DataAnnotations;

namespace qlkho.Models
{
    public class Lend
    {
        public int LendID { get; set; }

        public int UserID { get; set; }
        public User? User { get; set; }

        public string? OrganizationName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateCreated { get; set; }

        public string? Description { get; set; } = null;
    }
}
