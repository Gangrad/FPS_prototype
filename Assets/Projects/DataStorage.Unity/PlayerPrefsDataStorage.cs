using DataStorage.Common;
using UnityEngine;

namespace DataStorage.Unity {
    public class PlayerPrefsDataStorage : IIntDataStorage, IFloatDataStorage, IStringDataStorage {
        //-----------------------int
        
        void IGenericDataStorage<int>.Save(string key, int value) {
            PlayerPrefs.SetInt(key, value);
        }

        int IGenericDataStorage<int>.Load(string key) {
            return PlayerPrefs.GetInt(key);
        }
        
        //----------------------float

        void IGenericDataStorage<float>.Save(string key, float value) {
            PlayerPrefs.SetFloat(key, value);
        }

        float IGenericDataStorage<float>.Load(string key) {
            return PlayerPrefs.GetFloat(key);
        }
        
        //------------------------string

        void IGenericDataStorage<string>.Save(string key, string value) {
            PlayerPrefs.SetString(key, value);
        }

        string IGenericDataStorage<string>.Load(string key) {
            return PlayerPrefs.GetString(key);
        }
        
        //------------------------common

        public bool Has(string key) {
            return PlayerPrefs.HasKey(key);
        }
    }
}