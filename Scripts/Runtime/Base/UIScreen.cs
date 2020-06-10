using UnityEngine;

namespace Fiber.UI
{
    public abstract class UIScreen: MonoBehaviour
    {
        public bool          ShownByDefault = true;
        public RectTransform Content;
        public string        Key;

        /// <summary>
        /// CALL BASE OR DO NOT OVERRIDE!!
        /// </summary>
        protected virtual void Awake()
        {
            Enabled = ShownByDefault;

            FiberCore.UI.AddScreen(Key, this);
            OnReady();
        }

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
            Enabled = !Enabled;

            if (Enabled) Show(); else Hide();
        }

        protected abstract void OnReady();
    }
}