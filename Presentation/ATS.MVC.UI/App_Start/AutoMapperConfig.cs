using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using ATS.Data.Model;
using ATS.Data.ViewModel;

namespace ATS.MVC.UI
{
    public class AutoMapperConfig
    {
        public static void ConfigureAutoMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<TimeSheetMaster, TimeSheetMasterViewModel>()
                    .ForMember(dest => dest.TimeSheetDetailViewModel, opt => opt.MapFrom(src => src.TimeSheetDetail));
                cfg.CreateMap<TimeSheetDetail, TimeSheetDetailViewModel>()
                    .ForMember(dest => dest.TimeSheetMaster, opt => opt.MapFrom(src => src.TimeSheetMaster))
                    .ForMember(dest => dest.TimeSheetDate, opt => opt.MapFrom(src => src.StartTime));

                cfg.CreateMap<TimeSheetMasterViewModel, TimeSheetMaster>()
                    .ForMember(dest => dest.TimeSheetDetail, opt => opt.MapFrom(src => src.TimeSheetDetailViewModel));
                cfg.CreateMap<TimeSheetDetailViewModel, TimeSheetDetail>()
                    .ForMember(dest => dest.TimeSheetMaster, opt => opt.MapFrom(src => src.TimeSheetMaster))
                    .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => new DateTime(src.TimeSheetDate.Year, src.TimeSheetDate.Month, src.TimeSheetDate.Day, src.StartTime.Hour, src.StartTime.Minute, src.StartTime.Second)))
                    .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => new DateTime(src.TimeSheetDate.Year, src.TimeSheetDate.Month, src.TimeSheetDate.Day, src.EndTime.Hour, src.EndTime.Minute, src.EndTime.Second)));
            });
        }
    }
}