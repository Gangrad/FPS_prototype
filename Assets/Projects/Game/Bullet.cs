using Common;
using Logger;
using UnityEngine;

namespace Game {
    public class BulletParams {
        public float InitialForce;
        public int Damage;
    }
    
    public class Bullet : Shell {
        private BulletParams _params;

        protected override float InitialForce { get { return _params.InitialForce; } }

        public void Setup(BulletParams bulletParams) {
            _params = bulletParams;
        }

        private void OnTriggerEnter(Collider other) {
            var otherTag = other.gameObject.tag;
            switch (otherTag) {
                case "Player":
                    return;
                case "Target":
                    DealDamage(other);
                    break;
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

        private void DealDamage(Collider col) {
            var target = col.gameObject.GetComponent<Target>();
            if (target == null) {
                Log.Logger.Warn("Target script not found on obj {0}", col.gameObject.name);
                return;
            }
            target.DealDamage(_params.Damage, DamageType.Bullet);
        }
    }
}