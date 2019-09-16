using Common;
using UnityEngine;

namespace Game {
    public class CharacterController : MonoBehaviour {
        [SerializeField] private Transform _cameraSpot;
        [SerializeField] private Gun _gun;
        [SerializeField] private Bazooka _bazooka;
        private IPlayerParams _playerParams;
        private MovementComponent _movement;
        private LookComponent _look;
        private ShootComponent _shoot;

        public void Setup(IPlayerParams playerParams, GameConfig config) {
            _playerParams = playerParams;
            var rb = GetComponent<Rigidbody>();
            _movement = new MovementComponent(rb, _playerParams, config);
            var lookParams = new LookComponentParams {
                CharacterTr = transform,
                CameraTr = _cameraSpot,
                WeaponTrs = new []{_gun.transform}
            };
            _look = new LookComponent(lookParams);
            var gunParams = new GunParams {
                BulletParams = new BulletParams {
                    InitialForce = config.InitialBulletGunForce,
                    Damage = config.BulletDamage
                }
            };
            _gun.Setup(gunParams);
            var bazookaParams = new BazookaParams {
                RocketParams = new RocketParams {
                    InitialForce = config.InitialRocketForce,
                    ExplosionParams = new ExplosionParams {
                        ExplosionRadius = config.RocketExplosionRadius,
                        ExplosionDuration = config.RocketExplosionDuration,
                        MaxDamage = config.MaxRocketDamage,
                        MaxForcePower = config.MaxForcePower
                    }
                }
            };
            _bazooka.Setup(bazookaParams);
            _shoot = new ShootComponent(new Weapon[]{_gun, _bazooka});
        }

        public void AttachCamera(Camera cam) {
            var camTr = cam.transform;
            camTr.SetParent(_cameraSpot);
            camTr.localPosition = Vector3.zero;
            camTr.localRotation = Quaternion.identity;
        }

        private void Update() {
            var dt = Time.deltaTime;
            _movement.Tick(dt);
            _look.Tick();
            _shoot.Tick();
        }

        public void OnPauseState(bool pause) {
            enabled = !pause;
        }
    }
}