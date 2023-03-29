namespace schwada.upce.mhd {

    public class Vehicle
    {
        public int Id { get; set; }
        public string RegistrationNumber { get; set; }
        public VehicleType Type { get; set; }
        public DateTime DateOfNextInspection { get; set; }

        public Vehicle(int id, string registrationNumber, VehicleType type, DateTime dateOfNextInspection)
        {
            Id = id;
            RegistrationNumber = registrationNumber;
            Type = type;
            DateOfNextInspection = dateOfNextInspection;
        }
    }

    public enum VehicleType
    {
        Bus,
        Trolley
    }
}