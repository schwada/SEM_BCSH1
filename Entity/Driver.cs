namespace schwada.upce.mhd {

    public class Driver
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool HasBusLicense { get; set; }
        public bool HasTrolleyLicense { get; set; }


        public Driver(int id, string name, string surname, bool hasBusLicense, bool hasTrolleyLicense)
        {
            Id = id;
            Name = name;
            Surname = surname;
            HasBusLicense = hasBusLicense;
            HasTrolleyLicense = hasTrolleyLicense;
        }
    }

}