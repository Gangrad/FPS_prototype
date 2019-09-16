using System;
using System.Collections.Generic;
using SystemProtocols;
using DataHelper;
using Logger;

namespace MiniJSON {
    public class JsonData {
        public static ILogger Log = NullLogger.Instance;
        private readonly Dictionary<string, object> _data;

        public IEnumerable<string> Keys {
            get { return _data.Keys; }
        }
        
        public bool Empty { get { return _data.Count == 0; } }

        public JsonData() {
            _data = new Dictionary<string, object>();
        }

        public JsonData(Dictionary<string, object> dic) {
            _data = dic;
        }

        //--------------------------

        public bool ContainsKey(string key) {
            return _data.ContainsKey(key);
        }

        private T GetValue<T>(string key, T def, bool ignoreNull = false) {
            object obj;
            if (_data.TryGetValue(key, out obj)) {
                if (obj is T)
                    return (T) obj;
                if (obj == null) {
                    if (!ignoreNull)
                        Log.Warn("Cant convert data for key \"{0}\" to {1}, cause its null", key, typeof (T).ToString());
                }
                else
                    Log.Warn("Cant convert value \"{0}\" to {1}, data type: {2}", key, typeof (T).ToString(),
                        obj.GetType().ToString());
            }
            else
                Log.Warn("Cant find data by key {0}", key);
            return def;
        }

        public object GetObject(string key) {
            return GetValue<object>(key, null, true);
        }

        public bool GetBool(string key, bool def = false) {
            return GetValue(key, def);
        }

        public int GetInt(string key, int def = 0) {
            return (int) GetValue<long>(key, def);
        }

        public long GetLong(string key, long def = 0) {
            return GetValue(key, def);
        }

        public float GetFloat(string key, float def = 0f) {
            return (float) GetValue<double>(key, def);
        }

        public double GetDouble(string key, double def = 0d) {
            return GetValue(key, def);
        }

        public string GetString(string key, string def = "") {
            return GetValue(key, def);
        }

        public List<object> GetList(string key) {
            var res = GetValue<List<object>>(key, null, true);
            return res ?? new List<object>();
        }

        public Dictionary<string, object> GetJsonDataRaw(string key) {
            var res = GetValue<Dictionary<string, object>>(key, null, true);
            return res ?? new Dictionary<string, object>();
        }

        public JsonData GetJsonData(string key) {
            var res = GetValue<Dictionary<string, object>>(key, null, true);
            return res == null ? new JsonData() : new JsonData(res);
        }

        //----------------

        private void SetValue(string key, object value) {
            if (_data.ContainsKey(key))
                Log.Warn("Replacing value by key {0}, prev: {1} (type: {2}), new: {3}", key, _data[key],
                    _data[key].GetType().ToString(), value);
            _data[key] = value;
        }

        public void SetObject(string key, object value) {
            SetValue(key, value);
        }

        public void SetBool(string key, bool value) {
            SetValue(key, value);
        }

        public void SetInt(string key, int value) {
            SetValue(key, value);
        }

        public void SetLong(string key, long value) {
            SetValue(key, value);
        }

        public void SetFloat(string key, float value) {
            SetValue(key, value);
        }

        public void SetDouble(string key, double value) {
            SetValue(key, value);
        }

        public void SetString(string key, string value) {
            SetValue(key, value);
        }

        public void SetList(string key, List<object> value) {
            SetValue(key, value);
        }

        public void SetJsonDataRaw(string key, Dictionary<string, object> value) {
            SetValue(key, value);
        }

        public void SetJsonData(string key, JsonData value) {
            SetValue(key, value);
        }

        //-------------------------

        public override string ToString() {
            return string.Format("Json: {0}", _data.FormatJsonData());
        }

        //-------------------------

        public static JsonData LoadFromFile(string filename) {
            //try {
            //    if (File.Exists(filename)) {
            //        var text = File.ReadAllText(filename);
            //        var des = Json.Deserialize(text) as Dictionary<string, object>;
            //        if (des != null)
            //            return new JsonData(des);
            //        Log.Warn("Cant deserealize json data from file {0}", filename);
            //    }
            //    else
            //        Log.Warn("File {0} doesnt exists", filename);
            //}
            //catch (Exception e) {
            //    Log.Exception(e);
            //}
            //return new JsonData();
            return Load(filename, FileProvider.Instance);
        }

        public static JsonData Load(string text) {
            var des = Json.Deserialize(text) as Dictionary<string, object>;
            if (des != null)
                return new JsonData(des);
            return null;
        }

        public static JsonData Load(string name, IStorageProvider provider) {
            try {
                if (provider.Exists(name)) {
                    string text = provider.LoadText(name);
                    var res = Load(text);
                    if (res != null)
                        return res;
                    Log.Warn("Cant deserealize json data from file {0}", name);
                }
                else
                    Log.Warn("File {0} doesnt exists", name);
            }
            catch (Exception e) {
                Log.Exception(e);
            }
            return new JsonData();
        }

        public void SaveToFile(string filename) {
            //try {
            //    File.WriteAllText(filename, Json.Serialize(_data));
            //}
            //catch (Exception e) {
            //    Log.Exception(e);
            //}
            Save(filename, FileProvider.Instance);
        }

        public void Save(string name, IStorageProvider provider) {
            try {
                provider.Save(name, Json.Serialize(_data));
            }
            catch (Exception e) {
                Log.Exception(e);
            }
        }
    }
}
