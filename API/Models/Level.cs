using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Level
    {
        [Required]
        public String LEVEL_Code { get; set; }
        [Required]
        public String Description { get; set; }
        public String Active { get; set; }
    }
}