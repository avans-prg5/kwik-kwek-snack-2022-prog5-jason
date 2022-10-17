using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KwikKwekSnack.Domain
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string LoginName { get; set; }
        [Required]
        [DefaultValue(UserRole.Customer)]
        public UserRole Role { get; set; }
    }

    public enum UserRole
    {
        Customer,
        Employee,
        Admin
    }
}
