using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;

namespace wykład_4.Models
{
    public class Book
    {
        public Book()
        {
            Authors = new HashSet<Author>();
        }

        [HiddenInput]
        public int Id { get; set; }

        public string Title{ get; set; }

        public int EditionYear { get; set; }

        public DateTime Created { get; set; }
        [JsonPropertyName("details")]
        public BookDetails? BookDetails { get; set; }

        public int PublisherId { get; set; }

        public ISet<Author> Authors { get; set; }

    }
}
