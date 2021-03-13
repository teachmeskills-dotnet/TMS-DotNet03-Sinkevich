using System;
using System.Collections.Generic;
using System.Text;

namespace FindHousingProject.Common.Constants
{
    /// Roles constants.
    /// </summary>
    public static class StatusConstants
    {
        /// <summary>
        /// .
        /// </summary>
        public const string Booked = "Successful";
        /// <summary>
        /// .
        /// </summary>
        public const string BookedError = "Error";
    }

    public static class StateConstants
    {
        public const byte requested = 0;
        public const byte approved = 1;
        public const byte declined = 2;
    }
}
