namespace qlkho.Models
{
    public class LendLog
    {
        public int LendLogID { get; set; }

        public int LendID { get; set; }
        public Lend? Lend { get; set; }

        public int MaterialNameID { get; set; }
        public MaterialName? MaterialName { get; set; }

        public int Quantity { get; set; }

        public int UnitID { get; set; }
        public Unit? Unit { get; set; }
    }
}
