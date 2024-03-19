namespace WebApplication1.Models
{
    public class MissingPerson
    {
        public int Id { get; set; }
        public int GenderCode { get; set; }
        public Int64 PersonRegNum { get; set; }
        public string PersonRegNo { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string PersonRegData { get; set; } = default!;
        public Int64 MissingPersonCode { get; set; }
        public string FIRNo { get; set; } = default!;
        public string District { get; set; } = default!;
        public string PoliceStation { get; set; } = default!;
    }
}