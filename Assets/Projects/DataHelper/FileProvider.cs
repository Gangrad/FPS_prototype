using System.IO;
using SystemProtocols;

namespace DataHelper {
    public class FileProvider : IStorageProvider {
        public static FileProvider Instance = new FileProvider();

        protected virtual string GetPath(string name) {
            return name;
        }

        public bool Exists(string name) {
            return File.Exists(GetPath(name));
        }

        public void Save(string name, byte[] data) {
            string path = GetPath(name);
            File.WriteAllBytes(path, data);
        }

        public void Save(string name, string data) {
            string path = GetPath(name);
            File.WriteAllText(path, data);
        }

        public void Save(string name, string[] data) {
            string path = GetPath(name);
            File.WriteAllLines(path, data);
        }

        public byte[] LoadBytes(string name) {
            string path = GetPath(name);
            return File.Exists(path) ? File.ReadAllBytes(path) : null;
        }

        public string LoadText(string name) {
            string path = GetPath(name);
            return File.Exists(path) ? File.ReadAllText(path) : null;
        }

        public string[] LoadLines(string name) {
            string path = GetPath(name);
            return File.Exists(path) ? File.ReadAllLines(path) : null;
        }
    }
}
