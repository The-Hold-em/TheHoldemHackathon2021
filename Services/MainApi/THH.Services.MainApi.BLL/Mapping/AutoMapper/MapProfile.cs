using AutoMapper;

using THH.Services.MainApi.BLL.DTOs.Update;
using THH.Services.MainApi.BLL.DTOs.List;
using THH.Services.MainApi.Entities.Concrete;
using THH.Services.MainApi.BLL.DTOs.Delete;
using THH.Services.MainApi.BLL.DTOs.Create;

namespace THH.Services.MainApi.BLL.Mapping.AutoMapper;

public class MapProfile : Profile
{
    public MapProfile()
    {
        CreateMap<NodeListDto, Node>().ReverseMap();
        CreateMap<DeleteDto, Node>().ReverseMap();
        CreateMap<NodeCreateDto, Node>().ReverseMap();
        CreateMap<NodeUpdateDto, Node>().ReverseMap();

        CreateMap<PollingStationListDto, PollingStation>().ReverseMap();
        CreateMap<DeleteDto, PollingStation>().ReverseMap();
        CreateMap<PollingStationCreateDto, PollingStation>().ReverseMap();
        CreateMap<PollingStationUpdateDto, PollingStation>().ReverseMap();

        CreateMap<ElectionListDto, Election>().ReverseMap();
        CreateMap<DeleteDto, Election>().ReverseMap();
        CreateMap<ElectionCreateDto, Election>().ReverseMap();
        CreateMap<ElectionUpdateDto, Election>().ReverseMap();

        CreateMap<CandidateListDto, Candidate>().ReverseMap();
        CreateMap<DeleteDto, Candidate>().ReverseMap();
        CreateMap<CandidateCreateDto, Candidate>().ReverseMap();
        CreateMap<CandidateUpdateDto, Candidate>().ReverseMap();

        CreateMap<CityListDto, City>().ReverseMap();
        CreateMap<DistrictListDto, District>().ReverseMap();
    }
}
