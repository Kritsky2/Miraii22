using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Miraii.Controllers;


namespace Miraii.Models
{
    [Table("Content")]
    public class Content
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public string Image { get; set; }

        public string Category { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        
    }
}
