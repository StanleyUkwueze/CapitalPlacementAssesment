namespace CapitalPlacementTest.Requests
{
    public class EditQuestionDto
    {
        public string Type { get; set; }
        public List<string>? Choice { get; set; }
        public string Description { get; set; }
        public int MaxChoiceAllowed { get; set; } = 0;
        public bool EnableOtherOptions { get; set; }
    }
}
