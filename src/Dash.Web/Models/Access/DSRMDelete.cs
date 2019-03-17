using System;
using System.ComponentModel.DataAnnotations;

namespace Dash.Web.Models.Access
{
    public class DSRMDelete
    {
        [Required]
        public Guid Id { get; set; }
    }
}