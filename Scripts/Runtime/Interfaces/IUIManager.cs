using Fiber.UI;

namespace Fiber.Core
{
    public interface IUIManager
    {
        /// <summary>
        /// Register a new window
        /// </summary>
        /// <param name="key">Screen key</param>
        /// <param name="screen">Screen</param>
        void AddScreen(string key, UIScreen screen);

        /// <summary>
        /// Check if UIManager contains screen
        /// </summary>
        /// <param name="key">Screen key</param>
        /// <returns>Result</returns>
        bool ContainsScreen(string key);

        /// <summary>
        /// Get base screen controller
        /// </summary>
        /// <param name="key">Screen key</param>
        /// <returns>Screen</returns>
        UIScreen GetScreen(string key);

        /// <summary>
        /// Get screen and try to cast to specific type
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">Screen key</param>
        /// <returns>Casted screen</returns>
        T GetScreen<T>(string key) where T : UIScreen;
        T GetScreen<T>();

        /// <summary>
        /// Register new screen instead of existing one
        /// </summary>
        /// <param name="key">Target screen key</param>
        /// <param name="screen">New screen</param>
        void ReplaceScreen(string key, UIScreen screen);
    }
}