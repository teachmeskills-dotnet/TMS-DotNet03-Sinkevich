using System.Collections.Generic;

namespace FindHousingProject.DAL.Entities
{
    /// <summary>
    /// Country information.
    /// </summary>
    public class Country
    {
        /// <summary>
        /// Country Id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Country name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// List of places
        /// </summary>
        public ICollection<Place> Places { get; set; }
    }
}
