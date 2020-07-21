using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BAL.Entities
{
    public class Section
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SectionID { get; set; }
        [Required]
        public string SectionName { get; set; }
        public string Description { get; set; }
        public int SectionOrder { get; set; }
        public bool IsActive { get; set; }

        // Get the Categories' Collection - Navigation Property [1-M Relationship]
        public ICollection<Category> Categoriess { get; set; }
        
    }
}
