using System;

namespace FindHousingProject.Web.ViewModels
{
    /// <summary>
    /// Error type Model.
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// Request id.
        /// </summary>
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
