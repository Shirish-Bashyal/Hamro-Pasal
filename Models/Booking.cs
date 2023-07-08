namespace Hamro_Pasal.Models
{
    public class Booking
    {
        public int Id { get; set; }

        public bool IsBooked { get; set; }

        public bool IsSaled { get; set; }

        public UserDetails Booked_by_user { get; set; }

        //public UserAds ad_being_booked { get; set; }
    }
}
