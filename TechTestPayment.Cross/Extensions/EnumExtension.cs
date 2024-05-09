using System.ComponentModel;

namespace TechTestPayment.Cross.Extensions
{
    /// <summary>
    /// Utility class for enums.
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// Get description attribute text.
        /// </summary>
        /// <param name="value">this Enum</param>
        /// <returns></returns>
        public static string GetDescription(this Enum value)
        {
            return value.GetType()
                .GetField(value.ToString())
                ?.GetCustomAttributes(typeof(DescriptionAttribute), false)
                .SingleOrDefault() is not DescriptionAttribute attribute ? value.ToString() : attribute.Description;
        }

        /// <summary>
        /// Converts to int
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToInt(this Enum value)
        {
            return Convert.ToInt32(value);
        }
    }

}
