using System.Text.Json;

namespace schwada.upce.mhd {
    public abstract class JsonRepository<T> {
        protected readonly string DataFilePath;

        protected JsonRepository(string dataFilePath) {
            DataFilePath = dataFilePath;
        }

        protected TransportationData LoadData() {
            if (!File.Exists(DataFilePath)) return new TransportationData();
            string jsonString = File.ReadAllText(DataFilePath);
            TransportationData? data = JsonSerializer.Deserialize<TransportationData>(jsonString);
            if(data == null) data = new TransportationData();
            return data;
        }
    
        public abstract void Add(T entity);

        public abstract void Update(T entity);

        public abstract IEnumerable<T> GetAll();
    }
}