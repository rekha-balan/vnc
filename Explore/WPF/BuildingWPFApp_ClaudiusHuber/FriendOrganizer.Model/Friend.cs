using System.ComponentModel.DataAnnotations;

namespace FriendOrganizer.Model
{
    public class Friend
    {

        #region Enums, Fields, Properties

        public int Id { get; set; }

        // Using Data Annotations to enforce constraints
        // Need to add reference to System.ComponentModel.DataAnnotations

        [Required]
        [MaxLength(50)] // Used for Strings or Byte Arrays
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        #endregion

    }
}
