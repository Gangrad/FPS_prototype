using Common;
using UnityEngine;

namespace Menu {
    public class MenuScene : MonoBehaviour, IScene {
        [SerializeField] private MenuComponents _components;
        
        public void Setup(MenuService service) {
            var settings = new MenuSettings {
                Start = service.StartGame,
                ShowOptions = service.ShowOptions,
                Quit = service.Quit
            };
            _components.Setup(settings);
        }

        void IScene.Tick() {
        }

        void IScene.Show() {
            _components.Root.SetActive(true);
        }

        void IScene.Hide() {
            _components.Root.SetActive(false);
        }
    }
}