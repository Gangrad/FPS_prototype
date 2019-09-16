namespace Game {
    public class BazookaParams {
        public RocketParams RocketParams;
    }
    
    public class Bazooka : Weapon {
        private BazookaParams _params;

        public void Setup(BazookaParams bazookaParams) {
            _params = bazookaParams;
        }
        
        public override void Fire() {
            var bullet = CreateShell<Rocket>();
            bullet.Setup(_params.RocketParams);
        }
    }
}