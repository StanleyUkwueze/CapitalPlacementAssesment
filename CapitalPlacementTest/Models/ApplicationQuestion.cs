namespace CapitalPlacementTest.Models
{
    public class ApplicationQuestion
    {
        public string id { get; set; } = Guid.NewGuid().ToString();
        public string Type { get; set; }
        public List<string>? Choice { get; set; }
        public string Description { get; set; }
        public int MaxChoiceAllowed { get; set; } = 0;
        public bool EnableOtherOptions { get; set; }
    }
}
