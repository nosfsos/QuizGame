using System;

namespace Utils
{
    public static class EnumUtils
    {
        /// <summary>
        /// Returns the enum wit the same value as the string received in the parameters
        /// </summary>
        /// <typeparam name="T">The type of the enum to be used by the Parse function.</typeparam>
        /// <param name="value">The value that must be converted to enum.</param>
        /// <returns>A enum of type T with the value received in the parameters</returns>
        public static T StringToEnum<T>(string value) where T : struct
        {
            try
            {
                return (T)Enum.Parse(typeof(T), value);
            }
            catch (Exception)
            {
                //Debug.LogError(e.Message + " " + e.StackTrace);
                return default;
            }
        }
    }
}