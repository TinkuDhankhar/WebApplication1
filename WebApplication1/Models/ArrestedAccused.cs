namespace WebApplication1.Models
{
    public class ArrestedAccused
    {
        public Int64 Id { get; set; }
        public Int64 FIRAId { get; set; }
        public string ArrestPersonName { get; set; } = default!;
        public Int64 AccusedSrNo { get; set; }
        public string AccusedAddress { get; set; } = default!;
        public string FIRNo { get; set; } = default!;
        public string ArrestByName { get; set; } = default!;
        public string ArrestPersonRank { get; set; } = default!;
        public string OfficerRankCode { get; set; } = default!;
        public string ArrestDate { get; set; } = default!;
    }
}
