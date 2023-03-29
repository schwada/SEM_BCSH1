namespace schwada.upce.mhd {

    public class Shift
    {
        public DateTime Date { get; set; }
        public int VehicleId { get; set; }
        public int DriverId { get; set; }

        public Shift(DateTime date, int vehicleId, int driverId)
        {
            Date = date;
            VehicleId = vehicleId;
            DriverId = driverId;
        }
    }
}