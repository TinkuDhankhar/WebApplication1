using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class FIRArrestedAccusedDetails
    {
        public IEnumerable<ArrestAddress> ArrestAddressList { get; set; } = default!;
        public IEnumerable<FIRACTSection> FIRACTSectionList { get; set; } = default!;
        public IEnumerable<ArrestPersonDetails> ArrestPersonDetailList { get; set; } = default!;
        public IEnumerable<AccusedPhyDesc> AccusedPhyDescList { get; set; } = default!;
        public IEnumerable<AccusedPhoto> AccusedPhotoList { get; set; } = default!;
        public IEnumerable<IODetails> IODetailList { get; set; } = default!;
        public IEnumerable<PersonalSearchDetails> PersonalSearchDetailList { get; set; } = default!;
        public IEnumerable<AccusePresentAddress> AccusePresentAddressList { get; set; } = default!;
        public IEnumerable<AccusedEducationalQualification> AccusedEducationalqualificationList { get; set; } = default!;
    }
    public class ArrestAddress
    {
        public Int64 Id { get; set; }
        public Int64 FIRAId { get; set; }
        public Int64 FIRRegNum { get; set; }
        public string WitnessFullName { get; set; } = default!;
        public string WitnessStatement { get; set; } = default!;
        public string WitnessAddress { get; set; } = default!;
    }
    public class FIRACTSection
    {
        public Int64 Id { get; set; }
        public Int64 FIRAId { get; set; }
        public int ActCode { get; set; }
        public string ActName { get; set; } = default!;
        public string Sec { get; set; } = default!;
    }
    public class ArrestPersonDetails
    {
        public Int64 Id { get; set; }
        public Int64 FIRAId { get; set; }
        public string FIRRegNum { get; set; } = default!;
        [DataType(DataType.Date)]
        public DateTime FIRDate { get; set; } = default!;
        public string DistrictName { get; set; } = default!;
        public string PoliceStationName { get; set; } = default!;
        public int? ArrestYear { get; set; } = default!;
        [DataType(DataType.Date)]
        public DateTime ArrestSurrenderDate { get; set; } = default!;
        [DataType(DataType.Time)]
        public DateTime ArrestSurrenderTime { get; set; } = default!;
        public string GDNumber { get; set; } = default!;
        public string ArrestDistrictName { get; set; } = default!;
        public string ArrestPolicestationName { get; set; } = default!;
        public int? SurrendInCourtCode { get; set; } = default!;
        public string CourtName { get; set; } = default!;
        public string AccusedFullName { get; set; } = default!;
        public int RelationTypeCode { get; set; } = default!;
        public string RelationNameENG { get; set; } = default!;
        public string RelationNameHINDI { get; set; } = default!;
        public string RelativeName { get; set; } = default!;
        public int ReligionCode { get; set; } = default!;
        public string AccusedReligionName { get; set; } = default!;
        public int CasteTribeCode { get; set; } = default!;
        public string AccusedCasteTribe { get; set; } = default!;
        public int CategoryCode { get; set; } = default!;
        public string AccusedCategoryName { get; set; } = default!;
        public int OccupationCode { get; set; } = default!;
        public string AccusedOccupationName { get; set; } = default!;
        public int AddTypeCode { get; set; } = default!;
        public string AddressType { get; set; } = default!;
        public string AccusedFullAddress { get; set; } = default!;
        public string AddPS { get; set; } = default!;
        public string Mobile { get; set; } = default!;
        public string Telephone { get; set; } = default!;
        public string alias1 { get; set; } = default!;
        public string alias2 { get; set; } = default!;
        public string AccusedNationality { get; set; } = default!;
        public string AccusedVoterId { get; set; } = default!;
        public string PassportPlace { get; set; } = default!;
        public DateTime? PassportDate { get; set; } = default!;
        public string InjuryDetails { get; set; } = default!;
        public string UIDNum { get; set; } = default!;
        public string PassportNum { get; set; } = default!;
        public string PanNum { get; set; } = default!;
        public string VoterId { get; set; } = default!;
        public string IntimateDate { get; set; } = default!;
        public string IntimateTime { get; set; } = default!;
        public string IntimateRelName { get; set; } = default!;
        public string IntimateRelation { get; set; } = default!;
        public string ArrestActionTaken { get; set; } = default!;
        public DateTime ArrestMemoDate { get; set; } = default!;
        public string ArrApprehReason { get; set; } = default!;
        public string ApprehenByName { get; set; } = default!;
        public string FIRCopyGivenTo { get; set; } = default!;
        public string JCWOName { get; set; } = default!;
        public string JCLDescription { get; set; } = default!;
        public int Age { get; set; } = default!;
        public string PlaceOfArrest { get; set; } = default!;
        public DateTime CreatedDate { get; set; }
    }
    public class AccusedPhyDesc
    {
        public Int64 Id { get; set; }
        public Int64 FIRAId { get; set; }
        public string ReligionName { get; set; } = default!;
        public string GenderName { get; set; } = default!;
        public string DateOfBirth { get; set; } = default!;
        public string LivingStatusCode { get; set; } = default!;
        public string EDUQualCode { get; set; } = default!;
        public string IncomeGroupCode { get; set; } = default!;
        public string AccIsDangerous { get; set; } = default!;
        public string AccPrevJumpBail { get; set; } = default!;
        public string PrevJumpBailDel { get; set; } = default!;
        public string AccGenARMED { get; set; } = default!;
        public string AccOperatesAccompl { get; set; } = default!;
        public string AccKnownCriminal { get; set; } = default!;
        public string KnownCriminalDet { get; set; } = default!;
        public string AccIsRecidivist { get; set; } = default!;
        public string AccLikelyJumpBail { get; set; } = default!;
        public string AfterBailCommitsCrime { get; set; } = default!;
        public string WantedOtherCase { get; set; } = default!;
        public string WantedCaseDetails { get; set; } = default!;
        public string BuilDType { get; set; } = default!;
        public string HeightFromCM { get; set; } = default!;
        public string ComplexionType { get; set; } = default!;
        public string TeethType { get; set; } = default!;
        public string HairType { get; set; } = default!;
        public string IsUsingWig { get; set; } = default!;
        public string HairStraightness { get; set; } = default!;
        public string HairCut { get; set; } = default!;
        public string HairLength { get; set; } = default!;
        public string HairColor { get; set; } = default!;
        public string HairDye { get; set; } = default!;
        public string HairStyle { get; set; } = default!;
        public string EyeType { get; set; } = default!;
        public string EyeColor { get; set; } = default!;
        public string EyeBlind { get; set; } = default!;
        public string EyeUsingSpecs { get; set; } = default!;
        public string EyeSpecsType { get; set; } = default!;
        public string EyeBrowThickness { get; set; } = default!;
        public string EyeBrowShape { get; set; } = default!;
        public string EyeBlinking { get; set; } = default!;
        public string EyeSquint { get; set; } = default!;
        public string BeardType { get; set; } = default!;
        public string MoustachesType { get; set; } = default!;
        public string NoseType { get; set; } = default!;
        public string FaceType { get; set; } = default!;
        public string Voice { get; set; } = default!;
        public string Habits { get; set; } = default!;
        public string DressOuterTop { get; set; } = default!;
        public string DressOuterBottom { get; set; } = default!;
        public string DressInnerTop { get; set; } = default!;
        public string DressInnerBottom { get; set; } = default!;
        public string DressAccessoryTop { get; set; } = default!;
        public string DressAccessoryBottom { get; set; } = default!;
        public string DressFootwear { get; set; } = default!;
        public string LangDialectCode { get; set; } = default!;
        public string PlaceBurnMark { get; set; } = default!;
        public string PlaceBlackMark { get; set; } = default!;
        public string PlaceLeucoDerma { get; set; } = default!;
        public string PlaceMole { get; set; } = default!;
        public string PlaceScar { get; set; } = default!;
        public string PlaceTattoo { get; set; } = default!;
        public string OtherFeatCode { get; set; } = default!;
        public string IsFPTaken { get; set; } = default!;
        public string BloodGroup { get; set; } = default!;
        public string Identification { get; set; } = default!;
        public string LegsMissing { get; set; } = default!;
        public string EarsMissing { get; set; } = default!;
        public string EarsMed { get; set; } = default!;
        public string DeafDumb { get; set; } = default!;
        public string ArmsMissing { get; set; } = default!;
        public string FingerExtra { get; set; } = default!;
        public string FingerMissing { get; set; } = default!;
        public string HunchBack { get; set; } = default!;
        public string ToeExtra { get; set; } = default!;
        public string ToeMissing { get; set; } = default!;
        public string Limping { get; set; } = default!;
        public string BowLeg { get; set; } = default!;
        public string KnockKnee { get; set; } = default!;
        public string EarLobes { get; set; } = default!;
        public string IsGoitre { get; set; } = default!;
        public DateTime CreatedDate { get; set; }
    }
    public class AccusedPhoto
    {
        public Int64 Id { get; set; }
        public Int64 FIRAId { get; set; }
        public string FileId { get; set; } = default!;
        public string Photos { get; set; } = default!;
        public string PhotoName { get; set; } = default!;
        public DateTime CreatedDate { get; set; }
    }
    public class IODetails
    {
        public Int64 Id { get; set; }
        public Int64 FIRAId { get; set; }
        public Int64 FIREGNum { get; set; }
        public string IOName { get; set; } = default!;
        public string GPFNo { get; set; } = default!;
        public string Rank { get; set; } = default!;
        public DateTime CreatedDate { get; set; }
    }
    public class PersonalSearchDetails
    {
        public Int64 Id { get; set; }
        public Int64 FIRAId { get; set; }
        public string ItemsFound { get; set; } = default!;
        public string ItemQuantity { get; set; } = default!;
        public DateTime CreatedDate { get; set; }
    }
    public class AccusePresentAddress
    {
        public Int64 Id { get; set; }
        public Int64 FIRAId { get; set; }
        public string AddressTypePresent { get; set; } = default!;
        public string AccusedPresentAddress { get; set; } = default!;
        public string AddPresentPS { get; set; } = default!;
        public DateTime CreatedDate { get; set; }
    }
    public class AccusedEducationalQualification
    {
        public Int64 Id { get; set; }
        public Int64 FIRAId { get; set; }
        public string? EducationQualificationCode { get; set; } = default!;
        public DateTime CreatedDate { get; set; }
    }
}
