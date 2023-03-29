namespace schwada.upce.mhd {

    public class TransportationData
    {
        public List<Driver> Drivers { get; set; }
        public List<Vehicle> Vehicles { get; set; }
        public List<Shift> Shifts { get; set; }

        public TransportationData(){
            this.Drivers = new List<Driver>();
            this.Vehicles = new List<Vehicle>();
            this.Shifts = new List<Shift>();
        }
    }
}