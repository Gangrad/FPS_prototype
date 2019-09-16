using UnityEngine;

namespace Common {
    public class GameConfig : ScriptableObject {
        [Header("Movement")] public float MaxSpeed = 40;

        [Header("Weapons")] [Header("Gun")] public int InitialBulletGunForce = 100;
        public int BulletDamage = 2;
        [Header("Bazooka")] public float InitialRocketForce = 20;
        public float RocketExplosionRadius = 10;
        public float RocketExplosionDuration = .2f;
        public int MaxRocketDamage = 8;
        public float MaxForcePower = 10;

        [Header("Target")] public int InitialTargetHealth = 10;
    }
}