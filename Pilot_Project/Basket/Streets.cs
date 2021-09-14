using System.Data.Linq.Mapping;

namespace Pilot_Project.Basket
{
    [Table(Name = "Streets")]
    internal class Streets
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column(Name = "MinskStreets")]
        public string MinskStreets { get; set; }
    }
}
