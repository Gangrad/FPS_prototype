using System;

namespace Common {
    public interface IAdditiveScene {
        void Show(Action close, Action toMenu);
        void Hide();
    }
}