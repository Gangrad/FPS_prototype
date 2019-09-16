using Common;
using Game;
using GameUI;
using Logger;
using Logger.Unity;
using Menu;
using Options;
using UnityEngine;

namespace UnityLauncher {
    public class Launcher : MonoBehaviour {
        [SerializeField] private MenuScene _menu;
        [SerializeField] private GameUIScene _gameUI;
        [SerializeField] private OptionsScene _options;
        [SerializeField] private GameScene _game;
        [SerializeField] private GameConfig _config;

        private GameUIService _gameUIService;
        private GameService _gameService;
        
        private void Start() {
            Log.Logger = UnityLoggerUtils.GetEmptyLogger(LogLevel.Debug);
            Setup();
            GameSceneManager.CurrentScene = _menu;
        }

        private void Update() {
            GameSceneManager.CurrentScene.Tick();
        }

        private void Setup() {
            var optionsModel = new OptionsModel();
            var optionsService = new OptionsService(optionsModel);
            optionsService.Load();
            _options.Setup(optionsService);
            
            var menuService = new MenuService(_options, StartGame);
            _menu.Setup(menuService);
            
            var pause = new Pause();
            _gameUIService = new GameUIService(_options, EndGame, pause);
            _gameUI.Setup(_gameUIService);
            
            _gameService = new GameService(optionsModel, _config, pause);
            _game.Setup(_gameUI, _gameService);
            _game.OnDestroyTarget += _gameUI.OnTargetDestroyed;
        }

        private void StartGame() {
            _game.StartGame();
            _gameUI.StartGame();
            GameSceneManager.CurrentScene = _game;
        }

        private void EndGame() {
            _game.EndGame();
            _gameUI.EndGame();
            GameSceneManager.CurrentScene = _menu;
        }
    }
}