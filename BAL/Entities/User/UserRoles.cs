using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;

namespace BAL.Entities
{
    public class UserRoles
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserRoleID { get; set; }
        public int UserID { get; set; }
        public int RoleID { get; set; }

        public User User { get; set; }
        public Roles Role { get; set; }

    }
}