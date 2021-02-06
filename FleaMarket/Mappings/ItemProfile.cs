﻿using AutoMapper;
using FleaMarket.Models;
using FleaMarket.ViewModels;
using System.Globalization;

namespace FleaMarket.Mappings
{
    class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<AddingItemViewModel, Item>()
                .ForMember(item => item.Price, opt => opt.MapFrom(vw => this.GetPriceFromString(vw.Price)))
                .ForMember(item => item.Images, opt => opt.Ignore());
        }

        private decimal? GetPriceFromString(string price)
        {
            if (price is null)
            {
                return null;
            }

            return decimal.Parse(price, new CultureInfo("en-US"));
        }
    }
}