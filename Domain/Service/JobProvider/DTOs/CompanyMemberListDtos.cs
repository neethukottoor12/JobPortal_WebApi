using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.JobProvider.DTOs
{
    public class CompanyMemberListDtos
    {
        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }

        public Enums.Role Role { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
    }
}
