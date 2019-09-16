using System;
using System.Collections.Generic;
using Common;
using CsExtensions;
using UnityEngine;

#if UNITY_EDITOR
using Unity.Utils;
#endif

namespace Game {
    public class GameScene : MonoBehaviour, IScene {
        [SerializeField] private GameObject _characterPrefab;
        [SerializeField] private List<Target> _targets;
        private IScene _uiScene;
        private GameService _service;
        private CharacterController _character;
        public event Action<DamageType> OnDestroyTarget;

        public void Setup(IScene uiScene, GameService service) {
            _uiScene = uiScene;
            _service = service;
        }

        public void StartGame() {
            _character = _service.CreateCharacter(_characterPrefab);
            _character.AttachCamera(Camera.main);
            for (int i = 0, count = _targets.Count; i < count; ++i) {
                var target = _targets[i];
                target.Setup(_service.InitialTargetHealth);
                target.OnDestroy += OnTargetDestroyed;
            }
            _uiScene.Hide();
        }

        public void EndGame() {
            var mainCamera = Camera.main;
            if (mainCamera != null)
                mainCamera.transform.parent = null;
            for (int i = 0, count = _targets.Count; i < count; ++i) {
                var target = _targets[i];
                target.OnDestroy -= OnTargetDestroyed;
            }
            _service.EndGame();
        }

        void IScene.Tick() {
            _uiScene.Tick();
        }

        void IScene.Show() {
            _uiScene.Show();
        }

        void IScene.Hide() {
            _uiScene.Hide();
        }

        private void OnTargetDestroyed(Target target, DamageType damageType) {
            _targets.Remove(target);
            OnDestroyTarget.SafeInvoke(damageType);
        }

#if UNITY_EDITOR
        [Button("FindAllTargets")]
        private void FindAllTargets() {
            var allTargets = GetComponentsInChildren<Target>();
            _targets = new List<Target>(allTargets);
        }
#endif
    }
}