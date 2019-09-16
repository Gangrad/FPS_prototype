using System;
using Common;
using CsExtensions;
using Logger;
using UnityEngine;

namespace Menu {
    public class MenuService {
        private readonly IAdditiveScene _optionsScene;
        private readonly Action _startGame;

        public MenuService(IAdditiveScene optionsScene, Action startGame) {
            _optionsScene = optionsScene;
            _startGame = startGame;
        }
        
        public void StartGame() {
            Log.Logger.Info("Start");
            _startGame.SafeInvoke();
        }

        public void ShowOptions() {
            Log.Logger.Info("Show options");
            _optionsScene.Show(CloseOptions, null);
        }

        public void CloseOptions() {
            Log.Logger.Info("Close options");
            _optionsScene.Hide();
        }

        public void Quit() {
            Log.Logger.Info("Quit");
#if UNITY_EDITOR
         UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}