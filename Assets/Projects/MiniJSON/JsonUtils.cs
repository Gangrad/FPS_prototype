using System.Collections.Generic;
using CsExtensions;

namespace MiniJSON {
    using Data = Dictionary<string, object>;
    public static class JsonUtils {
        public static Data GetJsonData(this Data json, string key) {
            object tmp;
            return json.TryGetValue(key, out tmp) && tmp is Data ? (Data) tmp : null;
        }
        
        public static string GetStringValue(this Data json, string key, string defaultValue = "") {
            object tmp;
            return json.TryGetValue(key, out tmp) && tmp is string ? (string) tmp : defaultValue;
        }
        
        public static int GetIntValue(this Data json, string key, int defaultValue = 0) {
            object tmp;
            if (json.TryGetValue(key, out tmp)) {
                if (tmp is long)
                    return (int) (long) tmp;
                if (tmp is int)
                    return (int) tmp;
            }
            return defaultValue;
        }

        public static string FormatJsonData(this Data json) {
            return json.JoinToString(pair => {
//                var dataValue = pair.Value as Data;
//                var value = dataValue == null ? pair.Value : string.Format("{{{0}}}", dataValue.FormatJsonData());
//                return string.Format("{0} -> {1}", pair.Key, value);
                return string.Format("{0} -> {1}", pair.Key, pair.Value);
            }, "; ");
        }

        public static bool IsNullOrEmpty(this JsonData jsonData) {
            return jsonData == null || jsonData.Empty;
        }
    }
}