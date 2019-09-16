namespace SystemProtocols {
    public interface IStorageProvider {
        bool Exists(string name);
        void Save(string name, byte[] data);
        void Save(string name, string data);
        void Save(string name, string[] data);
        byte[] LoadBytes(string name);
        string LoadText(string name);
        string[] LoadLines(string name);
    }
}
