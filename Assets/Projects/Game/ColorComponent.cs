using System;
using UnityEngine;

namespace Game {
    [Serializable]
    public class ColorComponent {
        [SerializeField] private MeshRenderer _renderer;
        [SerializeField] private Color _fromColor = Color.white;
        [SerializeField] private Color _toColor = Color.red;
        private Material _mat;

        public void Init() {
            _mat = _renderer.material = _renderer.material;
            _mat.color = _fromColor;
        }

        public void UpdateColor(float ratio) {
            _mat.color = Color.Lerp(_fromColor, _toColor, ratio);
        }
    }
}