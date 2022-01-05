using Models.EmailModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobListing.Common
{
   public interface IEmailService
    {

        Task SendMailAsync(MailRequest mailRequest);

    }
}
