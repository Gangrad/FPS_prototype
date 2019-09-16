using System;
using Unity.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Menu {
    public struct MenuSettings {
        public Action Start;
        public Action ShowOptions;
        public Action Quit;
    }
    
    [Serializable]
    public class MenuComponents {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _optionsButton;
        [SerializeField] private Button _quitButton;
        [SerializeField] private GameObject _root;

        public GameObject Root { get { return _root; } }

        public void Setup(MenuSettings settings) {
            _startButton.SetOnClickAction(settings.Start);
            _optionsButton.SetOnClickAction(settings.ShowOptions);
            _quitButton.SetOnClickAction(settings.Quit);
        }
    }
}