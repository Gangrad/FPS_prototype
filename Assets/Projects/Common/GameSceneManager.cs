using System.Diagnostics;

namespace Common {
    public static class GameSceneManager {
        private static IScene _currentScene;
        public static IScene CurrentScene {
            get { return _currentScene; }
            set {
                if (_currentScene == value)
                    return;
                if (_currentScene != null)
                    _currentScene.Hide();
                _currentScene = value;
                if (_currentScene != null)
                    _currentScene.Show();
            }
        }

        private static IAdditiveScene _additiveScene;
        public static IAdditiveScene AdditiveScene {
            get { return _additiveScene; }
            private set {
                if (_additiveScene == value)
                    return;
                Debug.Assert(_currentScene == null);
                if (value != null)
                    _currentScene.Hide();
                _additiveScene = value;
                if (value == null)
                    _currentScene.Show();
            }
        }

        public static void AddAdditiveScene(IAdditiveScene scene) {
            AdditiveScene = scene;
        }

        public static void RemoveAdditiveScene(IAdditiveScene scene) {
            Debug.Assert(scene == _additiveScene);
            AdditiveScene = null;
        }
    }
}