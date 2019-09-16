using UnityEngine;

namespace Game {
    public abstract class Weapon : MonoBehaviour {
        [SerializeField] private Transform _firePoint;
        [SerializeField] private Shell _shellPrefab;

        protected T CreateShell<T>() where T : Shell {
            var shell = Instantiate(_shellPrefab);
            var tr = shell.transform;
            tr.position = _firePoint.position;
            tr.rotation = _firePoint.rotation;
            return shell as T;
        }

        public abstract void Fire();
    }
}