using System.Text.Json;

namespace schwada.upce.mhd {
    public class DriverRepository : JsonRepository<Driver>
    {
        public DriverRepository(string dataFilePath): base(dataFilePath) {}

        public override void Add(Driver driver)
        {
            if(!driver.HasBusLicense && !driver.HasTrolleyLicense) throw new ArgumentException($"Driver has to have a license");
            List<Driver> existingDrivers = LoadData().Drivers.ToList();
            if (existingDrivers.Any() && existingDrivers.Any(d => d.Id == driver.Id)) throw new ArgumentException($"Driver with ID {driver.Id} already exists.");
            existingDrivers.Add(driver);
            SaveData(existingDrivers);
        }

        public override void Update(Driver driver) {
            if(!driver.HasBusLicense && !driver.HasTrolleyLicense) throw new ArgumentException($"Driver has to have a license");
            List<Driver> existingDrivers = LoadData().Drivers.ToList();
            Driver? existingDriver = existingDrivers.FirstOrDefault(d => d.Id == driver.Id);
            if (existingDriver == null) throw new ArgumentException($"Driver with ID {driver.Id} does not exist.");
            if (existingDriver != null) {
                existingDrivers[existingDrivers.IndexOf(existingDriver)] = driver;
                SaveData(existingDrivers);
            }
        }

        public void Delete(int id) {
            List<Driver> existingDrivers = LoadData().Drivers.ToList();
            Driver? driverToDelete = existingDrivers.FirstOrDefault(d => d.Id == id);
            if (driverToDelete == null) throw new ArgumentException($"Driver with ID {id} does not exist.");
            if (driverToDelete != null) {
                existingDrivers.Remove(driverToDelete);
                SaveData(existingDrivers);
            }
        }

        public Driver GetById(int id) {
            List<Driver> existingDrivers = LoadData().Drivers.ToList();
            Driver? existingDriver = existingDrivers.FirstOrDefault(d => d.Id == id);
            if (existingDriver == null) throw new ArgumentException($"Driver with ID {id} does not exist.");
            return existingDriver;
        }

        public override IEnumerable<Driver> GetAll() {
            return LoadData().Drivers.ToList();
        }

        private void SaveData(List<Driver> drivers) {
            TransportationData existingData = new TransportationData();
            if (File.Exists(DataFilePath)){
                string existingJsonString = File.ReadAllText(DataFilePath);
                TransportationData? serialized = JsonSerializer.Deserialize<TransportationData>(existingJsonString);
                if(serialized == null || serialized.Drivers == null) throw new JsonException("Object is not valid");
                existingData = serialized;
            }
            existingData.Drivers = drivers;
            string modifiedJsonString = JsonSerializer.Serialize(existingData, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(DataFilePath, modifiedJsonString);
        }
    }
}