using AutoMapper;
using Domain.Entities;
using UseCases.Services.AuthServices.DTOs;

namespace ITransitionProject
{
    public class Mapper : Profile
    {
        public Mapper() 
        {
            CreateMap<RegistrateUserDTO, User>();
            CreateMap<User, AuthorizedUserDTO>();
        }
    }
}
