namespace CapitalPlacementTest.Requests
{
    public class ApplicationDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Nationality { get; set; }
        public string CurrentResidence { get; set; }
        public string IdNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }


        public string AboutMe { get; set; }
        public string YearOfGraduation { get; set; }
        public List<string> MultipleChoiceAnswer { get; set; }
        public bool UKEmbassyRejectionStatus { get; set; }
        public int YearsOfExperience { get; set; }
        public DateTime DateMovedToUK { get; set; }
    }
}
