﻿using ClearSky.Infrastructure.Data;

namespace ClearSky.Web.Models
{
    public class PropertyViewModel
    {
        public PropertyViewModel(Property property)
        {
            Id = property.Id;
            Address = property.Address;
            ImageUrl = property.ImageUrl;
            ListPrice = property.ListPrice;
            Description = property.Description;
        }

        public string Id { get; set; }
        public string Address { get; set; }
        public string ImageUrl { get; set; }
        public decimal ListPrice { get; set; }
        public string Description { get; set; }
    }
}
