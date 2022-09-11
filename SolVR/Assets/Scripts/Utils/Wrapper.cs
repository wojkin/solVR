namespace Utils
{
    /// <summary>
    /// A class which wraps a struct. Useful for passing a value type by reference.
    /// </summary>
    /// <typeparam name="T">The type of the struct which will be wrapped.</typeparam>
    public class Wrapper<T> where T : struct
    {
        #region Variables

        /// <summary>the wrapped struct</summary>
        public T Value;

        #endregion

        #region Custom Methods

        /// <summary>
        /// Initializes the wrapper.
        /// </summary>
        /// <param name="value">The initial struct value.</param>
        public Wrapper(T value)
        {
            Value = value;
        }

        #endregion
    }
}