using System.Text.Json;

namespace schwada.upce.mhd {
    public class VehicleRepository : JsonRepository<Vehicle> {
        public VehicleRepository(string dataFilePath): base(dataFilePath) {}

        public override void Add(Vehicle Vehicle)
        {
            List<Vehicle> existingVehicles = LoadData().Vehicles.ToList();
            if (existingVehicles.Any() && existingVehicles.Any(d => d.Id == Vehicle.Id)) throw new ArgumentException($"Vehicle with ID {Vehicle.Id} already exists.");
            existingVehicles.Add(Vehicle);
            SaveData(existingVehicles);
        }

        public override void Update(Vehicle Vehicle) {
            List<Vehicle> existingVehicles = LoadData().Vehicles.ToList();
            Vehicle? existingVehicle = existingVehicles.FirstOrDefault(d => d.Id == Vehicle.Id);
            if (existingVehicle == null) throw new ArgumentException($"Vehicle with ID {Vehicle.Id} does not exist.");
            if (existingVehicle != null) {
                existingVehicles[existingVehicles.IndexOf(existingVehicle)] = Vehicle;
                SaveData(existingVehicles);
            }
        }

        public void Delete(int id) {
            List<Vehicle> existingVehicles = LoadData().Vehicles.ToList();
            Vehicle? VehicleToDelete = existingVehicles.FirstOrDefault(d => d.Id == id);
            if (VehicleToDelete == null) throw new ArgumentException($"Vehicle with ID {id} does not exist.");
            if (VehicleToDelete != null) {
                existingVehicles.Remove(VehicleToDelete);
                SaveData(existingVehicles);
            }
        }

        public Vehicle GetById(int id) {
            List<Vehicle> existingVehicles = LoadData().Vehicles.ToList();
            Vehicle? existingVehicle = existingVehicles.FirstOrDefault(d => d.Id == id);
            if (existingVehicle == null) throw new ArgumentException($"Vehicle with ID {id} does not exist.");
            return existingVehicle;
        }

        public override IEnumerable<Vehicle> GetAll() {
            return LoadData().Vehicles.ToList();
        }


        private void SaveData(List<Vehicle> Vehicles) {
            TransportationData existingData = new TransportationData();
            if (File.Exists(DataFilePath)){
                string existingJsonString = File.ReadAllText(DataFilePath);
                TransportationData? serialized = JsonSerializer.Deserialize<TransportationData>(existingJsonString);
                if(serialized == null || serialized.Vehicles == null) throw new JsonException("Object is not valid");
                existingData = serialized;
            }
            existingData.Vehicles = Vehicles;
            string modifiedJsonString = JsonSerializer.Serialize(existingData, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(DataFilePath, modifiedJsonString);
        }
    }

}