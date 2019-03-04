using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dash.Entity.Objects
{
    [Table("User")]
    public class DSDUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DSDUserStatusTypes Status { get; set; }
    }

    public enum DSDUserStatusTypes : short
    {
        Deactive = 0,
        Active = 1,
        Pending = 2
    }
}