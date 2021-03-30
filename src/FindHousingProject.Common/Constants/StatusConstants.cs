using System;
using System.Collections.Generic;
using System.Text;

namespace FindHousingProject.Common.Constants
{
    /// </summary>
    /// Constants.
    /// </summary>
    public static class StatusConstants
    {
        /// <summary>
        /// Successfully if you can create reservation.
        /// </summary>
        public const string Booked = "Successful";

        /// <summary>
        /// Error if you can't create reservation.
        /// </summary>
        public const string BookedError = "Error";
    }

    /// <summary>
    /// This function will be created later.
    /// </summary>
    public static class StateConstants
    {
        public const byte requested = 0;
        public const byte approved = 1;
        public const byte declined = 2;
    }
}
