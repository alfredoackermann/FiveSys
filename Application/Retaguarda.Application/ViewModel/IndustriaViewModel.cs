using AutoMapper;
using FileSys.Shared.Application.ViewModels;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileSys.Retaguarda.Application.Admin.ViewModel
{
    public class IndustriaViewModel : BaseViewModel
    {
        public string Descricao { get; set; }
        public string RamoId { get; set; }
        public IDictionary<string, string> Ramos { get; set; }

        public static void Mapping(Profile Mapper)
        {
            Mapper.CreateMap<IndustriaViewModel, Industria>();
            Mapper.CreateMap<Industria, IndustriaViewModel>();
        }
    }
}
