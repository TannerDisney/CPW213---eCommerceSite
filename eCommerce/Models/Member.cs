using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Models
{
    public class Member
    {
        /// <summary>
        /// The id of the member
        /// </summary>
        [Key]
        public int MemberId { get; set; }

        /// <summary>
        /// Legal first and last name of member
        /// </summary>
        [StringLength(60)]
        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        /// <summary>
        /// Email of the user
        /// </summary>
        [StringLength(100)]
        [Required]
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "That doesn't look like an Email Address")]
        public string EmailAddress { get; set; }

        /// <summary>
        /// The username of the member
        /// </summary>
        [StringLength(20)]
        [Required]
        [RegularExpression(@"^[\d\w]+$", 
            ErrorMessage ="Usernames can only contain A-Z, 0-9, and underscores")]
        public string Username { get; set; }

        /// <summary>
        /// The password of the member
        /// </summary>
        [StringLength(100)]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// The date of birth of the member
        /// </summary>
        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        /*
         * Make Custom Attribute in order to do a dynamic date range
        [Range(typeof(DateTime), 
            DateTime.Today.AddYears(-120).ToShortDateString(),
            DateTime.Today.ToShortDateString()]
        */
        public DateTime DateOfBirth { get; set; }
    }

    /// <summary>
    /// ViewModel for the login page
    /// </summary>
    public class LoginViewModel
    {
        [Required]
        [Display(Name ="Username or Email")]
        public string UsernameOrEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
