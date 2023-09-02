namespace HotelListing.Data
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }


        //add list of hotel without adding to db
        public virtual IList<Hotel> Hotels { get; set; }
    }
}
