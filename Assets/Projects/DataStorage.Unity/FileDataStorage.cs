using System;
using System.IO;
using System.Text;
using Logger;
using MiniJSON;
using UnityEngine;
using Data = System.Collections.Generic.Dictionary<string, object>;

namespace DataStorage.Unity {
    public class FileDataStorage {
        public bool Save(Data data, string name) {
            try {
                var text = Json.Serialize(data);
                var bytes = Encoding.UTF8.GetBytes(text);
                File.WriteAllBytes(GetSavePath(name), bytes);
            }
            catch (Exception e) {
                Log.Logger.Exception(e);
                return false;
            }

            return true;
        }

        public Data Load(string name) {
            var path = GetSavePath(name);
            if (!File.Exists(path)) {
                return null;
            }

            try {
                var bytes = File.ReadAllBytes(path);
                var text = Encoding.UTF8.GetString(bytes);
                return (Data) Json.Deserialize(text);
            }
            catch (Exception e) {
                Log.Logger.Exception(e);
                return null;
            }
        }

        public bool DeleteSave(string name) {
            try {
                File.Delete(GetSavePath(name));
            }
            catch (Exception e) {
                Log.Logger.Exception(e);
                return false;
            }

            return true;
        }

        private static string GetSavePath(string name) {
            return Path.Combine(Application.persistentDataPath, name + ".sav");
        }
    }
}