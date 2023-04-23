using System.ComponentModel.DataAnnotations;

namespace qlkho.Models
{
    public class ImportItem
    {
        public int MaterialNameID { get; set; }
        public MaterialName? MaterialName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Expiry {  get; set; }

        public int Quantity { get; set; }

        public int UnitID { get; set; }
        public Unit? Unit { get; set; }
    }

    public class ExportItem
    {
        public int MaterialNameID { get; set; }
        public MaterialName? MaterialName { get; set; }

        public byte Status { get; set; }

        public int Quantity { get; set; }

        public int UnitID { get; set; }
        public Unit? Unit { get; set; }
    }
}
