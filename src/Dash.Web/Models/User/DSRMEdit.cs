using System;
using System.ComponentModel.DataAnnotations;

namespace Dash.Web.Models.User
{
    public class DSRMEdit
    {
        
        [Required]
        [MaxLength(50)]
        public string Firstname { get; set; }
        [Required]
        [MaxLength(50)]
        public string Lastname { get; set; }
        [Required]
        [MaxLength(50)]
        public string Email { get; set; }
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }
        public string Password { get; set; }
    }
}