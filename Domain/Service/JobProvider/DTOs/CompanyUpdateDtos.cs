using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.JobProvider.DTOs
{
    public class CompanyUpdateDtos
    {
        public Guid Id { get; set; }
        public string LegalName { get; set; } = null!;

        public string Summary { get; set; } = null!;

        

        public string Email { get; set; } = null!;

        public long Phone { get; set; }

        public string Address { get; set; } = null!;

        public string Website { get; set; } = null!;

       
    }
}

