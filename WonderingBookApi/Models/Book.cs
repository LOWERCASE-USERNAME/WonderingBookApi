﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WonderingBookApi.Models
{
    public class Book
    {
        [Key]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Authors { get; set; }
        public string? Publisher { get; set; }
        public string? PublishedDate { get; set; }
        public string? Description { get; set; }
        public string? ISBN { get; set; }
        public int? PageCount { get; set; }
        public string? ImageLink { get; set; }
        // Navigation property
        [JsonIgnore]
        public virtual ICollection<Article> Articles { get; set; }
    }
}
