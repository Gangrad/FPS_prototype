using System;
using TMPro;
using Unity.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Options {
    public struct OptionsViewSettings {
        public float InitialSpeed;
        public Action<float> SetSpeed;
        public float InitialDamping;
        public Action<float> SetDamping;
        public Action Close;
        public Action ToMenu;
    }

    [Serializable]
    public class FloatSliderComponent {
        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI _value;

        public FloatSliderComponent SetValue(float value) {
            _slider.value = value;
            UpdateLabelValue(value);
            return this;
        }

        public FloatSliderComponent SetCallback(Action<float> onChanged) {
            Action<float> onValueChanged = value => {
                UpdateLabelValue(value);
                onChanged(value);
            };
            _slider.SetOnChangedAction(onValueChanged);
            return this;
        }

        private void UpdateLabelValue(float value) {
            _value.text = value.ToString("F2");
        }
    }
    
    [Serializable]
    public class OptionsComponents {
        [SerializeField] private FloatSliderComponent _speed;
        [SerializeField] private FloatSliderComponent _damping;
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _menuButton;

        public void Setup(OptionsViewSettings settings) {
            _speed.SetValue(settings.InitialSpeed).SetCallback(settings.SetSpeed);
            _damping.SetValue(settings.InitialDamping).SetCallback(settings.SetDamping);
            _backButton.SetOnClickAction(settings.Close);
            _menuButton.SetOnClickAction(settings.ToMenu);
            _menuButton.gameObject.SetActive(settings.ToMenu != null);
        }
    }
}