using UnityEngine;

namespace Fiber.UI
{
    public abstract class UIScreen: MonoBehaviour
    {
        public bool          ShownByDefault = true;
        public RectTransform Content;
        public string        Key;

        public bool Enabled
        {
            get => _enabled;
            set
            {
                Content.gameObject.SetActive(value);
                _enabled = value;
            }
        }
        private bool _enabled;

        public virtual void Show()
        {
            Enabled = true;
        }

        public virtual void Hide()
        {
            Enabled = false;
        }

        public virtual void Toggle()
        {
            Toggle(!Enabled);
        }

        public virtual void Toggle(bool value)
        {
            if (value) Show(); else Hide();
        }
    }
}