using AutoMapper;
using DataLayer.Tables;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer
{
    public class MappingProfile : Profile 
    {
        public MappingProfile() {
            // Add as many of these lines as you need to map your objects
            CreateMap<Account, AccountDto>();
            CreateMap<AccountDto, Account>();
            CreateMap<Warship, WarshipDto>();
            CreateMap<WarshipDto, Warship>();
        }
    }
}