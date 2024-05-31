using AutoMapper;
using RecordDb.API.Models.Domain;
using RecordDb.API.Models.DTO;

namespace RecordDb.API.Mappings
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Artist, ArtistDto>().ReverseMap();
            CreateMap<Artist, ArtistQueryDto>().ReverseMap();
            CreateMap<AddArtistDto, Artist>().ReverseMap();
            CreateMap<UpdateArtistDto, Artist>().ReverseMap();
            CreateMap<Record, RecordDto>().ReverseMap();
            CreateMap<Record, RecordQueryDto>().ReverseMap();
            CreateMap<AddRecordDto, Record>().ReverseMap();
            CreateMap<UpdateRecordDto, Record>().ReverseMap();
        }
    }
}
