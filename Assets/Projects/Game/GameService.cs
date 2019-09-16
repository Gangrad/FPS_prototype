using Common;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace Game {
    public class GameService {
        private readonly IPlayerParams _charParams;
        private readonly GameConfig _config;
        private readonly Pause _pause;
        private CharacterController _character;

        public int InitialTargetHealth { get { return _config.InitialTargetHealth; } }

        public GameService(IPlayerParams playerParams, GameConfig config, Pause pause) {
            _pause = pause;
            _charParams = playerParams;
            _config = config;
        }
        
        public CharacterController CreateCharacter(GameObject prototype) {
            var obj = UnityObject.Instantiate(prototype, Vector3.up * 3, Quaternion.identity);
            _character = obj.GetComponent<CharacterController>();
            _character.Setup(_charParams, _config);
            _pause.OnChanged += _character.OnPauseState;
            return _character;
        }
        
        public void EndGame() {
            _pause.OnChanged -= _character.OnPauseState;
            UnityObject.Destroy(_character.gameObject);
        }
    }
}