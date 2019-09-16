using System;
using CsExtensions;
using UnityEngine;

namespace Common {
    public class Pause {
        private bool _state;
        public event Action<bool> OnChanged;

        public bool Active {
            get { return _state; }
            set {
                if (_state == value)
                    return;
                _state = value;
                Time.timeScale = _state ? 0f : 1f;
                OnChanged.SafeInvoke(_state);
            }
        }
    }
}