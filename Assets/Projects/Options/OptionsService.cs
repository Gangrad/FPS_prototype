using DataStorage.Unity;

namespace Options {
    public class OptionsService {
        private readonly FileDataStorage _dataStorage = new FileDataStorage();
        private readonly OptionsModel _model;
        
        public float PlayerSpeed { get { return _model.Acceleration; } }
        public float PlayerDamping { get { return _model.Damping; } }

        public OptionsService(OptionsModel model) {
            _model = model;
        }

        public void ChangeSpeed(float value) {
            _model.Acceleration = value;
        }

        public void ChangeDamping(float value) {
            _model.Damping = value;
        }

        public void Load() {
            var jsonData = _dataStorage.Load("game");
            if (jsonData != null)
                _model.UpdateFromJson(jsonData);
        }

        public void Save() {
            var jsonData = _model.ToJson();
            _dataStorage.Save(jsonData, "game");
        }
    }
}