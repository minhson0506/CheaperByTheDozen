using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class Manufacturer
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Manufaturer Name")]
        public string Name { get; set; }

        //[Required]
        //[DisplayName("Display Order")]
        //[Range(1, 100, ErrorMessage = "Display Order must be between 1 and 100 only")]
        //public int DisplayOrder { get; set; }
    }
}

