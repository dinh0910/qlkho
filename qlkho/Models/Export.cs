using System.ComponentModel.DataAnnotations;

namespace qlkho.Models
{
    public class Export
    {
        public int ExportID { get; set; }

        public int UserID { get; set; }
        public User? User { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateCreated { get; set; }

        public byte Reason { get; set; }

        public string? Description { get; set; } = null;
    }
}
