﻿using HotelListing.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelListing.Models
{

    public class CreateHotelDTO
    {
       
        [Required]
        [StringLength(maximumLength: 150, ErrorMessage = "Hotel Name Is Too Long")]
        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength: 250, ErrorMessage = "Hotel Address Is Too Long")]
        public string Address { get; set; }

        [Required]
        [Range(0,5)]
        public double Rating { get; set; }

        [Required]
        public int CoutryId { get; set; }
        
    }

    public class HotelDTO: CreateHotelDTO
    {
        public int Id { get; set; }
        public CountryDTO Country { get; set; }
    }
}






























