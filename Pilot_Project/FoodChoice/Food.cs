using System.Data.Linq.Mapping;

namespace Pilot_Project.FoodChoice
{
    [Table(Name = "TableWithFood")]
    public class Food
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column(Name = "FoodType")]
        public string FoodType { get; set; }
        [Column(Name = "Name")]
        public string Name { get; set; }
        [Column(Name = "Price")]
        public string Price { get; set; }
        [Column(Name = "Weight")]
        public string Weight { get; set; }
    }
}
