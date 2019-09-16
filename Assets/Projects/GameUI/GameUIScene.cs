using System;
using System.Collections.Generic;
using Common;
using UnityEngine;

namespace GameUI {
    public class GameUIScene : MonoBehaviour, IScene {
        [SerializeField] private GameUIComponents _components;
        private GameUIService _service;
        
        public void Setup(GameUIService service) {
            _service = service;
            var settings = new GameUISettings {
                InitialPoints = service.Points,
                InputCallbacks = new Dictionary<KeyCode, Action> {{KeyCode.Escape, service.ToggleOptions}}
            };
            _components.Setup(settings);
            _service.PointsChanged += _components.UpdatePoints;
        }

        private void OnDestroy() {
            _service.PointsChanged -= _components.UpdatePoints;
        }

        public void StartGame() {
            _service.StartGame();
        }

        public void EndGame() {
            _service.EndGame();
        }

        void IScene.Tick() {
            _components.ListenInput();
        }

        void IScene.Show() {
            _components.Root.SetActive(true);
            Cursor.visible = false;
        }

        void IScene.Hide() {
            _components.Root.SetActive(false);
            Cursor.visible = true;
        }

        public void OnTargetDestroyed(DamageType damageType) {
            var points = damageType == DamageType.Explosion ? 2 : 1;
            _service.AddPoints(points);
        }
    }
}