using System.ComponentModel.DataAnnotations;

namespace qlkho.Models
{
    public class Import
    {
        public int ImportID { get; set; }

        public int SupplierID { get; set; }
        public Supplier? Supplier { get; set; }

        public int UserID { get; set; }
        public User? User { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateCreated { get; set; }
    }
}
