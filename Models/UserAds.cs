using System.ComponentModel;

namespace Hamro_Pasal.Models
{
    public class UserAds
    {
        public int Id { get; set; }
        public string AdTitle { get; set; }

        public string AdDescription { get; set; }

        public string? Price { get; set; }
        public string AdAddress { get; set; }
        public byte[]? Picture { get; set; }


        // public string PicturePath { get; set; }   // to save pictures uploaded by user about ads, need multiple pictures

        public DateTime? CreatedDate { get; set; }
        public Location Ad_Location { get; set; }   //change the location and add AdsAdress

        public UserDetails Ad_by_user { get; set; }

       // public Booking Booking_ad { get; set; }

        public Category Category_ad { get; set; }



    }
}
