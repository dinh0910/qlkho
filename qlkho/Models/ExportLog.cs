namespace qlkho.Models
{
    public class ExportLog
    {
        public int ExportLogID { get; set; }

        public int ExportID { get; set; }
        public Export? Export { get; set; }

        public int MaterialNameID { get; set; }
        public MaterialName? MaterialName { get; set; }

        public int Quantity { get; set; }

        public int UnitID { get; set; }
        public Unit? Unit {  get; set; }
    }
}
