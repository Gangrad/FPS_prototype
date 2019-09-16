using System;
using Common;
using UnityEngine;

namespace Options {
    public class OptionsScene : MonoBehaviour, IAdditiveScene {
        [SerializeField] private OptionsComponents _components;
        private OptionsService _service;

        public void Setup(OptionsService service) {
            _service = service;
        }
        
        void IAdditiveScene.Show(Action close, Action toMenu) {
            var settings = new OptionsViewSettings {
                InitialSpeed = _service.PlayerSpeed,
                SetSpeed = _service.ChangeSpeed,
                InitialDamping = _service.PlayerDamping,
                SetDamping = _service.ChangeDamping,
                Close = close,
                ToMenu = toMenu
            };
            _components.Setup(settings);
            GameSceneManager.AddAdditiveScene(this);
            gameObject.SetActive(true);
        }

        void IAdditiveScene.Hide() {
            _service.Save();
            gameObject.SetActive(false);
            GameSceneManager.RemoveAdditiveScene(this);
        }
    }
}