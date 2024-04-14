namespace Modules.SavingSystem {
    public interface IDataStorageService {
        public void Save<TData>(string key, TData data, string group);
        public TData Load<TData>(string key, string group);
        public void Delete(string key, string group);
        public void DeleteGroup(string group);
    }
}