using UnityEngine;

namespace Game {
    public class ShootComponent {
        private readonly Weapon[] _weapons;
        private int _activeWeaponId;

        private Weapon ActiveWeapon { get { return _weapons[_activeWeaponId]; } }

        public ShootComponent(Weapon[] weapons) {
            _weapons = weapons;
            _activeWeaponId = 0;
            for (int i = 0, count = _weapons.Length; i < count; ++i)
                _weapons[i].gameObject.SetActive(false);
            ActiveWeapon.gameObject.SetActive(true);
        }

        public void Tick() {
            if (Input.GetKeyDown(KeyCode.Tab)) {
                ChangeWeapon();
                return;
            }
            if (Input.GetMouseButtonDown(0))
                ActiveWeapon.Fire();
        }

        private void ChangeWeapon() {
            ActiveWeapon.gameObject.SetActive(false);
            _activeWeaponId = (_activeWeaponId + 1) % _weapons.Length;
            ActiveWeapon.gameObject.SetActive(true);
        }
    }
}