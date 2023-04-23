namespace qlkho.Models
{
    public class MaterialLog
    {
        public int MaterialLogID { get; set; }

        public int MaterialID { get; set; }
        public Material? Material { get; set; }

        public Boolean Stored { get; set; }

        public Boolean TakeAway { get; set; }

        public Boolean TookAway { get; set; }

        public Boolean Returned { get; set; }

        public int UserID { get; set; }
        public User? User { get; set; }

        public string? Description { get; set; }
    }
}
