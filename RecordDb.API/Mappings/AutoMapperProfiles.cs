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
            CreateMap<AddArtistDto, Artist>().ReverseMap();
            CreateMap<UpdateArtistDto, Artist>().ReverseMap();
        }
    }
}
