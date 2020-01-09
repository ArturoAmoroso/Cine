using AutoMapper;
using Cinema.Data.Entities;
using Cinema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Data
{
    public class CineProfile: Profile
    {
        public CineProfile()
        {
            this.CreateMap<ActorEntity, Actor>()
                .ReverseMap();

            this.CreateMap<MovieEntity, Movie>()
                .ReverseMap();

            this.CreateMap<WinnerEntity, Winner>()
                .ReverseMap();


            //this.CreateMap<Camp, CampModel>()
            //  .ForMember(c => c.Venue, o => o.MapFrom(m => m.Location.VenueName))
            //  .ReverseMap();

            //this.CreateMap<Talk, TalkModel>()
            //  .ReverseMap()
            //  .ForMember(t => t.Camp, opt => opt.Ignore())
            //  .ForMember(t => t.Speaker, opt => opt.Ignore());
        }
    }
}
