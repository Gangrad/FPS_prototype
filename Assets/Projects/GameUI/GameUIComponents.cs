using System;
using System.Collections.Generic;
using CsExtensions;
using TMPro;
using UnityEngine;

namespace GameUI {
    public class GameUISettings {
        public int InitialPoints;
        public Dictionary<KeyCode, Action> InputCallbacks;
    }
    
    [Serializable]
    public class GameUIComponents {
        [SerializeField] private TextMeshProUGUI _pointsLabel;
        [SerializeField] private GameObject _root;
        private Dictionary<KeyCode, Action> _inputCallbacks;
        
        public GameObject Root { get { return _root; } }

        public void Setup(GameUISettings settings) {
            UpdatePoints(settings.InitialPoints);
            _inputCallbacks = settings.InputCallbacks;
        }

        public void ListenInput() {
            foreach (var entry in _inputCallbacks) {
                if (Input.GetKeyDown(entry.Key))
                    entry.Value.SafeInvoke();
            }
        }

        public void UpdatePoints(int points) {
            _pointsLabel.text = points.ToString();
        }
    }
}