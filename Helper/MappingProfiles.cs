using AutoMapper;
using Hamro_Pasal.DTO;
using Hamro_Pasal.Models;

namespace Hamro_Pasal.Helper
{
    public class MappingProfiles:Profile
    {

        public MappingProfiles()
        {
           // CreateMap<Location, LocationDTO>();   
            
            CreateMap<LocationDTO, Location>();
        }
    }
}
