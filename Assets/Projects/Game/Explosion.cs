using System.Collections.Generic;
using Common;
using Logger;
using UnityEngine;

namespace Game {
    public class ExplosionParams {
        public float ExplosionRadius;
        public float ExplosionDuration;
        public int MaxDamage;
        public float MaxForcePower;
    }
    
    public class Explosion : MonoBehaviour {
        [SerializeField] private ColorComponent _colorComponent;
        private ExplosionParams _params;
        private readonly HashSet<Collider> _affectedTargets = new HashSet<Collider>();
        private float _timeLeft;

        public void Setup(ExplosionParams explosionParams) {
            _params = explosionParams;
        }

        private void Start() {
            _timeLeft = _params.ExplosionDuration;
            _colorComponent.Init();
        }

        private void Update() {
            var progr = 1f - _timeLeft / _params.ExplosionDuration;
            _colorComponent.UpdateColor(progr);
            var rad = _params.ExplosionRadius * progr;
            transform.localScale = Vector3.one * rad;
            if (progr >= 1f) 
                Destroy(gameObject);
            else
                _timeLeft -= Time.deltaTime;
        }
        
        private void OnTriggerEnter(Collider other) {
            if (_affectedTargets.Contains(other))
                return;
            var otherTag = other.gameObject.tag;
            switch (otherTag) {
                case "Player":
                    return;
                case "Target":
                    AffectTarget(other);        
                    break;
                case "Props":
                case "Terrain":
                    break;
                default:
                    Log.Logger.Warn("bullet hit for object with unknown tag: {0} (obj: {1}",
                        otherTag, other.gameObject.name);
                    break;
            }
            _affectedTargets.Add(other);
        }

        private void AffectTarget(Collider col) {
            var target = col.gameObject.GetComponent<Target>();
            if (target == null) {
                Log.Logger.Warn("Target script was not found on obj {0}", col.name);
                return;
            }
            var ratio = CalcDistanceRatio(col);
            var damage = Mathf.RoundToInt(ratio * _params.MaxDamage);
            Log.Logger.Info("deal damage for target {0}. Ratio: {1}, dmg {2}", 
                col.gameObject.name, ratio, damage);
            target.DealDamage(damage, DamageType.Explosion);
            if (target.Alive) {
                var power = ratio * _params.MaxForcePower;
                target.Push(transform.position, power);
            }
        }

        private float CalcDistanceRatio(Collider target) {
            var pos = transform.position;
            var targetClosestPoint = target.ClosestPointOnBounds(pos);
            var dist = Vector3.Distance(pos, targetClosestPoint);
            return 1f - dist / _params.ExplosionRadius;
        }
    }
}