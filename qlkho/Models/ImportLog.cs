namespace qlkho.Models
{
    public class ImportLog
    {
        public int ImportLogID { get; set; }

        public int ImportID { get; set; }
        public Import? Import { get; set; }

        public int MaterialNameID { get; set; }
        public MaterialName? MaterialName { get; set; }

        public int Quantity { get; set; }

        public int UnitID { get; set; }
        public Unit? Unit { get; set; }
    }
}
