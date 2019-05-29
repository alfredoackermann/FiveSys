using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FileSys.Shared.Application.ViewModels;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;

namespace FileSys.Retaguarda.Application.Admin.ViewModel
{
    public class EmailTelaViewModel
    {
        public EmailTelaViewModel()
        {

        }

        public EmailTelaViewModel(EmailViewModel email)
        {
            ApenasUmEmail = true;
            Emails = new EmailViewModel[1] { email };
        }

        public EmailTelaViewModel(IEnumerable<EmailViewModel> emails)
        {
            ApenasUmEmail = false;
            Emails = (emails.Count() > 0 ? emails.ToArray() : null);
        }

        public bool ApenasUmEmail { get; set; }

        public EmailViewModel[] Emails { get; set; }
    }

    public class EmailViewModel : BaseViewModel
    {
        public string Endereco { get; set; }

        public static void Mapping(Profile mapper)
        {
            mapper.CreateMap<EmailViewModel, Email>();

            mapper.CreateMap<Email, EmailViewModel>();
        }
    }
}
