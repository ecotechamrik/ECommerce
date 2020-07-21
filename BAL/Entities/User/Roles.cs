using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BAL.Entities
{
    public class Roles
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleID { get; set; }

        [Required]
        public string RoleName { get; set; }

        public int IsActive { get; set; }

        // Get the User Collection - Navigation Property [1-M Relationship]
        public ICollection<UserRoles> UserRoles { get; set; }
    }
}
