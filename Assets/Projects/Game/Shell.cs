using System.Collections;
using UnityEngine;

namespace Game {
    public abstract class Shell : MonoBehaviour {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _maxLifetime;
        protected abstract float InitialForce { get; }
        
        private IEnumerator Start() {
            _rigidbody.AddForce(transform.forward * InitialForce, ForceMode.Impulse);
            yield return new WaitForSeconds(_maxLifetime);
            Destroy(gameObject);
        }

        protected virtual void DestroyThis() {
            Destroy(gameObject);
        }
    }
}