using System.Text.Json;

namespace schwada.upce.mhd {
    public class ShiftRepository {
        private readonly string DataFilePath;

        public ShiftRepository(string dataFilePath) {
            DataFilePath = dataFilePath;
        }

        public void Add(Shift shift) {
            List<Shift> existingShifts = LoadData().Shifts.ToList();
            Vehicle? vehicle = LoadData().Vehicles.FirstOrDefault(v => v.Id == shift.VehicleId);
            Driver? driver = LoadData().Drivers.FirstOrDefault(d => d.Id == shift.DriverId);
            if(vehicle == null || driver == null)
                throw new ArgumentException("A driver or vehicle with the specified Id does not exist");
            if (existingShifts.Any(s => s.DriverId == shift.DriverId && s.Date == shift.Date))
                throw new ArgumentException("A shift for this driver and date already exists.");
            if (vehicle.DateOfNextInspection == shift.Date || vehicle.DateOfNextInspection == shift.Date.AddDays(1))
                 throw new ArgumentException("A shift can't be scheduled for the date and the next day when the vehichle is inpected.");
            existingShifts.Add(shift);
            SaveData(existingShifts);
        }

        public void Update(Shift shift) {
            List<Shift> existingShifts = LoadData().Shifts.ToList();
            Shift? existingShift = existingShifts.FirstOrDefault(s => s.Date == shift.Date && s.DriverId == shift.DriverId);
            if (existingShift != null) {
                Vehicle? vehicle = LoadData().Vehicles.FirstOrDefault(v => v.Id == shift.VehicleId);
                Driver? driver = LoadData().Drivers.FirstOrDefault(d => d.Id == shift.DriverId);
                if(vehicle == null || driver == null)
                    throw new ArgumentException("A driver or vehicle with the specified Id does not exist");
                if (existingShifts.Any(s => s.DriverId == shift.DriverId && s.Date == shift.Date))
                    throw new ArgumentException("A shift for this driver and date already exists.");
                if (vehicle.DateOfNextInspection == shift.Date || vehicle.DateOfNextInspection == shift.Date.AddDays(1))
                    throw new ArgumentException("A shift can't be scheduled for the date and the next day when the vehichle is inpected.");
                existingShifts[existingShifts.IndexOf(existingShift)] = shift;
                SaveData(existingShifts);
            }
        }

        public void Delete(DateTime date, int driverId) {
            List<Shift> existingShifts = LoadData().Shifts.ToList();
            Shift? shiftToDelete = existingShifts.FirstOrDefault(s => s.Date == date && s.DriverId == driverId);
            if (shiftToDelete != null) {
                existingShifts.Remove(shiftToDelete);
                SaveData(existingShifts);
            }
        }

        public Shift GetByDateAndDriver(DateTime date, int driverId) {
            List<Shift> existingShifts = LoadData().Shifts.ToList();
            Shift? shift = existingShifts.FirstOrDefault(s => s.Date == date && s.DriverId == driverId);
            if(shift == null) throw new ArgumentException($"A shift with the supplied parameters does not exist");
            return shift;
        }

        public List<Shift> GetAll() {
            List<Shift> existingShifts = LoadData().Shifts.ToList();
            return existingShifts;
        }


        private TransportationData LoadData() {
            if (!File.Exists(DataFilePath)) return new TransportationData();
            string jsonString = File.ReadAllText(DataFilePath);
            TransportationData? data = JsonSerializer.Deserialize<TransportationData>(jsonString);
            if(data == null) data = new TransportationData();
            return data;
        }

        private void SaveData(List<Shift> Shifts) {
            TransportationData existingData = new TransportationData();
            if (File.Exists(DataFilePath)){
                string existingJsonString = File.ReadAllText(DataFilePath);
                var serialized = JsonSerializer.Deserialize<TransportationData>(existingJsonString);
                if(serialized == null || serialized.Shifts == null) throw new JsonException("Object is not valid");
                existingData = serialized;
            }
            existingData.Shifts = Shifts;
            string modifiedJsonString = JsonSerializer.Serialize(existingData, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(DataFilePath, modifiedJsonString);
        }
    }
}