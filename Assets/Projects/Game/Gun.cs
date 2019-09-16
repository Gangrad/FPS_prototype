namespace Game {
    public class GunParams {
        public BulletParams BulletParams;
    }
    
    public class Gun : Weapon {
        private GunParams _params;

        public void Setup(GunParams gunParams) {
            _params = gunParams;
        }
        
        public override void Fire() {
            var bullet = CreateShell<Bullet>();
            bullet.Setup(_params.BulletParams);
        }
    }
}