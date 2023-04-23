namespace qlkho.Models
{
    public class MaterialName
    {
        public int MaterialNameID { get; set; }

        public string? Name { get; set; }

        public int MaterialTypeID { get; set; }
        public MaterialType? MaterialType { get; set; }

        public int MaterialTopicID { get; set; }
        public MaterialTopic? MaterialTopic { get; set; }

        //public int ImageID { get; set; }
        //public Image? Image { get; set; }

        public int Count { get; set; }
    }
}
