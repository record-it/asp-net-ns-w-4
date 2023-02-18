using System.Text.Json.Serialization;

namespace wykład_4.Models
{
    public class BookDetails
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        [JsonIgnore]
        public int BookId { get; set;}

        public override string? ToString()
        {
            return $"Description: {Description}, Rating: {Rating}";
        }
    }
}
