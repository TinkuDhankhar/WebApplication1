
using System.Reflection.PortableExecutable;

namespace WebApplication1.Models
{
    public class PendingCase
    {
        public string PersonFullName { get; set; } = default!;
        public int Number { get; set; }
        public int PSCD { get; set; }
        public string PS { get; set; } = default!;
        public string FIRSRNum { get; set; } = default!;
        public string FIRRegNum { get; set; } = default!;
        public string FIRRegDate { get; set; } = default!;
        public string FIRStatusType { get; set; } = default!;
        public string District { get; set; } = default!;
        public IEnumerable<StateDistrict> DistrictList { get; set; } = default!;
        public IEnumerable<StatePoliceStation> PoliceStationList { get; set; } = default!;
        public IEnumerable<PendingCase> PendingCaseList { get; set; } = default!;
    }
    public class StateDistrict
    {
        public string District { get; set; } = default!;
    }
    public class StatePoliceStation
    {
        public string PS { get; set; } = default!;
    }
}