using System;
using Common;
using CsExtensions;
using UnityEngine;

namespace GameUI {
    public class GameUIService {
        private readonly IAdditiveScene _optionsScene;
        private readonly Action _endGame;
        private readonly Pause _pause;
        private bool _optionsOpened;
        
        public int Points { get; private set; }

        public event Action<int> PointsChanged;

        public GameUIService(IAdditiveScene optionsScene, Action endGame, Pause pause) {
            _optionsScene = optionsScene;
            _endGame = endGame;
            _pause = pause;
        }
        
        public void ToggleOptions() {
            if (_optionsOpened)
                _optionsScene.Hide();
            else
                _optionsScene.Show(ToggleOptions, _endGame);
            _optionsOpened = !_optionsOpened;
            _pause.Active = _optionsOpened;
            Cursor.visible = _optionsOpened;
        }

        public void StartGame() {
            Points = 0;
            PointsChanged.SafeInvoke(Points);
        }

        public void EndGame() {
            if (_optionsOpened)
                ToggleOptions();
        }

        public void AddPoints(int value) {
            Points += value;
            PointsChanged.SafeInvoke(Points);
        }
    }
}