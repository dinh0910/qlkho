using System.ComponentModel.DataAnnotations;

namespace qlkho.Models
{
    public class Material
    {
        public int MaterialID { get; set; }

        public int MaterialNameID { get; set; }
        public MaterialName? MaterialName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]

        public DateTime Expiry {  get; set; }

        public byte Status { get; set; }
    }
}
