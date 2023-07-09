﻿using Hamro_Pasal.Models;

namespace Hamro_Pasal.DTO
{
    public class PostAdsDTO
    {
        public string AdTitle { get; set; }

        public string AdDescription { get; set; }

        public string? Price { get; set; }

       // public string PicturePath { get; set; }
   
       public string category { get; set; }

        public Location location { get; set; }

        public string AdsAddress { get; set; }  ///address where the item is available
    
    }

}
