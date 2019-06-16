namespace MagicalBaba.BaseLibrary.Interfaces
{
    /// <summary>
    /// Text interceptor
    /// </summary>
    interface IInterceptor
    {
        /// <summary>
        /// Intercepts the given text and returns the new value
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        string Intercept(string text);

        /// <summary>
        /// Returns the secret text hidden between the hotkeys
        /// </summary>
        string GetSecretText();
    }
}