using Logger;
using UnityEngine;

namespace Game {
    public class RocketParams {
        public float InitialForce;
        public ExplosionParams ExplosionParams;
    }
    
    public class Rocket : Shell {
        [SerializeField] private Explosion _explosionPrefab; 
        private RocketParams _params;
        
        protected override float InitialForce { get { return _params.InitialForce; } }
        
        public void Setup(RocketParams rocketParams) {
            _params = rocketParams;
        }

        private void OnTriggerEnter(Collider other) {
            var otherTag = other.gameObject.tag;
            switch (otherTag) {
                case "Player":
                    return;
                case "Target":
                case "Props":
                case "Terrain":
                    break;
                default:
                    Log.Logger.Warn("bullet hit for object with unknown tag: {0} (obj: {1}",
                        otherTag, other.gameObject.name);
                    break;
            }
            DestroyThis();
        }

        protected override void DestroyThis() {
            MakeExplosion();
            base.DestroyThis();
        }

        private void MakeExplosion() {
            var explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            explosion.Setup(_params.ExplosionParams);
        }
    }
}