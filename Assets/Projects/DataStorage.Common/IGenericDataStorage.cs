namespace DataStorage.Common {
    public interface IIntDataStorage : IGenericDataStorage<int> {
    }

    public interface IFloatDataStorage : IGenericDataStorage<float> {
    }

    public interface IStringDataStorage : IGenericDataStorage<string> {
    }

    public interface IGenericDataStorage<T> {
        void Save(string key, T value);
        T Load(string key);
        bool Has(string key);
    }

    public static class GenericDataStorageUtils {
        public static bool TryLoad<T>(this IGenericDataStorage<T> storage, string key, out T value, T defaultValue) {
            if (storage.Has(key)) {
                value = storage.Load(key);
                return true;
            }

            value = defaultValue;
            return false;
        }
    }
}