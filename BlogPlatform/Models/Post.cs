﻿using System.ComponentModel.DataAnnotations;

// Project made by 00011270
// For CC module level 6 WIUT
namespace BlogPlatform.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
