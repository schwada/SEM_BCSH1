namespace schwada.upce.mhd {

    class Program {

        static void Main(string[] args) {
            var loadedfile = "transportation_data.json";
            var drivers = new DriverRepository(loadedfile);
            var vehicles = new VehicleRepository(loadedfile);
            var shifts = new ShiftRepository(loadedfile);
            vehicles.Add(new Vehicle(555, "HAAHA", VehicleType.Bus, new DateTime(2024, 3, 1)));
            // drivers.Add(new Driver(866, "daniel", "schwam", false, true));
            shifts.Add(new Shift(new DateTime(2023,3,5), 555, 866));

            Console.WriteLine("Data has been written to transportation_data.json");
        }
    }
}
