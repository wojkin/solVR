namespace Utils
{
    /// <summary>
    /// A class which wraps a struct. Useful for passing a value type by reference.
    /// </summary>
    /// <typeparam name="T">The type of the struct which will be wrapped.</typeparam>
    public class Wrapper<T> where T : struct
    {
        public T Value; // the wrapped struct

        /// <summary>
        /// Initializes the wrapper.
        /// </summary>
        /// <param name="value">The initial struct value.</param>
        public Wrapper(T value)
        {
            Value = value;
        }
    }
}