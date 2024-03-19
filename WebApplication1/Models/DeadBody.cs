using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class DeadBody
    {
        public int SrNo { get; set; }
        public Int64 DBinquestNum { get; set; }
        public string RegistrationNum { get; set; } = default!;
        public Int64? ImageID { get; set; }
        public string DeathType { get; set; } = default!;
        public string DeadBodyType { get; set; } = default!;
        public string Date { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Status { get; set; } = default!;
        public string PoliceStation { get; set; } = default!;
        public IEnumerable<PoliceStation> PoliceStationList { get; set; } = default!;
        public IEnumerable<DeadBody> DeadBodyList { get; set; } = default!;
        public string PoliceStationName { get; set; } = default!;
        [DataType(DataType.Date)]
        public DateTime SearchFrom { get; set; } = DateTime.Now;
        [DataType(DataType.Date)]
        public DateTime SearchTo { get; set; } = DateTime.Now;
    }
    public class PoliceStation
    {
        public string PoliceStationName { get; set; } = default!;
    }
}
