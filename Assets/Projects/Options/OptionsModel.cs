using System;
using Common;
using Logger;
using Json = System.Collections.Generic.Dictionary<string, object>;

namespace Options {
    public class OptionsModel : IPlayerParams {
        private float _playerAcceleration = 20;
        private float _damping = 30f;

        public float Acceleration { get { return _playerAcceleration; } set { _playerAcceleration = value; } }

        public float Damping { get { return _damping; } set { _damping = value; } }

        public Json ToJson() {
            return new Json {
                {"Acceleration", _playerAcceleration},
                {"Damping", _damping}
            };
        }

        public void UpdateFromJson(Json json) {
            LoadFloatValue(json, "Acceleration", ref _playerAcceleration);
            LoadFloatValue(json, "Damping", ref _damping);
        }

        private bool LoadFloatValue(Json json, string key, ref float value) {
            object boxedValue;
            json.TryGetValue(key, out boxedValue);
            if (boxedValue != null) {
                try {
                    value = (float)Convert.ToDouble(boxedValue);
                }
                catch (Exception e) {
                    Log.Logger.Exception(e);
                    return false;
                }
                
                return true;
            }

            return false;
        }
    }
}