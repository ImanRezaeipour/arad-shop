using AutoMapper;
using PersianDate;
using Shopping.DomainModel.Aggregates.Marketers.Aggregates;
using Shopping.DomainModel.Aggregates.Marketers.Entities;
using Shopping.DomainModel.Aggregates.Marketers.ValueObjects;
using Shopping.QueryModel.QueryModels.Marketers;

namespace Shopping.QueryService.Implements.Marketers
{
    public class MarketerProfile : Profile
    {
        public MarketerProfile()
        {
            CreateMap<Marketer, IMarketerFullInfoDto>()
                .ForMember(dest => dest.Documents, opt => opt.MapFrom(src => src.Documents.Split(',')))
                .ForMember(dest=>dest.BirthDate,opt=>opt.MapFrom(src=>src.BirthDate.ToFa("yyyy/MM/dd")));
            CreateMap<MarketerAddress, IMarketerAddressDto>();
            CreateMap<MarketerReagent, IMarketerReagentDto>();
            CreateMap<MarketerSalary, IMarketerSalaryDto>();
            CreateMap<MarketerComment, IMarketerCommentDto>();

            CreateMap<Marketer, IMarketerDto>()
                .ForMember(dest => dest.SubsetShopCount, opt => opt.Ignore());
        }
    }
}