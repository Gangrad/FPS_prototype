using UnityEngine;

namespace Game {
    public class LookComponentParams {
        public Transform CharacterTr;
        public Transform CameraTr;
        public Transform[] WeaponTrs;
    }
    
    public class LookComponent {
        private readonly LookComponentParams _params;

        private Quaternion _weaponsRot;
        
        public LookComponent(LookComponentParams lookComponentParams) {
            _params = lookComponentParams;
            _weaponsRot = Quaternion.Euler(90, 0, 0);
        }
        
        public void Tick() {
            var yRot = Input.GetAxis("Mouse X");
            var rot = _params.CharacterTr.localRotation * Quaternion.Euler(0, yRot, 0);
            _params.CharacterTr.localRotation = rot;

            var xRot = Input.GetAxis("Mouse Y");
            rot = _params.CameraTr.localRotation * Quaternion.Euler(-xRot, 0, 0);
            _params.CameraTr.localRotation = rot;

            _weaponsRot *= Quaternion.Euler(-xRot / 2, 0, 0);
            foreach (var weaponTr in _params.WeaponTrs)
                weaponTr.localRotation = _weaponsRot;
        }
    }
}