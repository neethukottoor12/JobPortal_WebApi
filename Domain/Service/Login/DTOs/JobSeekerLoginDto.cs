using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Service.Login.DTOs
{
    public class JobSeekerLoginDto
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string Phone { get; set; }

        [Required]
        [EmailAddress]
        public string Role { get; set; }
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        public string? Token { get; set; }
        public JobSeekerLoginDto(){}
    }
}
