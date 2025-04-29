namespace Domain.Models.OrderModels
{
    public class Address
    {
        public Address()
        {
            
        }

        public Address(string fristName, string lastName, string street, string city, string country)
        {
            FristName = fristName;
            LastName = lastName;
            Street = street;
            City = city;
            Country = country;
        }

        public string FristName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}