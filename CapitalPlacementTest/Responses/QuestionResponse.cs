namespace CapitalPlacementTest.Responses
{
    public class QuestionResponse
    {
        public string Type { get; set; }
        public List<string>? Choice { get; set; }
        public string Description { get; set; }
        public int MaxChoiceAllowed { get; set; }
        public bool EnableOtherOptions { get; set; }
    }
}
