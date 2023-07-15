namespace Hamro_Pasal.DTO
{
    public class AdsDetailDTO
    {
        public int Id { get; set; }
        public string AdTitle { get; set; }

        public string AdDescription { get; set; }

        public string? Price { get; set; }

        // public string PicturePath { get; set; }   // to save pictures uploaded by user about ads, need multiple pictures

        public DateTime? CreatedDate { get; set; }

        LocationDTO Location { get; set; }
      public   string AdsAddress { get; set; }
        public string AdByUser { get; set; }
        public string PhoneNumberOfSeller { get; set; }
       // public DateTime Date { get; set; }
    }
}
