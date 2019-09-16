using System;
using Common;
using CsExtensions;
using UnityEngine;

namespace Game {
    public class Target : MonoBehaviour {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private ColorComponent _colorComponent;
        public event Action<Target, DamageType> OnDestroy;
        private int _initialHealth;
        private int _health;
        
        public bool Alive { get { return _health > 0; } }

        public void Setup(int initialHealth) {
            _initialHealth = _health = initialHealth;
            _colorComponent.Init();
        }

        public void DealDamage(int damage, DamageType type) {
            _health -= damage;
            if (!Alive) {
                OnDestroy.SafeInvoke(this, type);
                Destroy(gameObject);
            }
            else {
                var ratio = 1f - (float) (_health - 1) / (_initialHealth - 1);
                _colorComponent.UpdateColor(ratio);
            }
        }

        public void Push(Vector3 from, float power) {
            var force = (transform.position - from).normalized * power;
            _rigidbody.AddForceAtPosition(force, from, ForceMode.Impulse);
        }
    }
}