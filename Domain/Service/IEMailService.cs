using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service
{
    public interface IEMailService
    {

        Task SendEmailAsync(MailRequest mailRequest);
    }
}
