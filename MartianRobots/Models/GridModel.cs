using System.ComponentModel.DataAnnotations;

namespace MartianRobots.Models
{
    public class GridModel
    {
        /// <summary>
        /// X size of the grid.
        /// </summary>
        [Range(0, 50)]
        public int XSize { get; set; }

        /// <summary>
        /// Y size of the grid.
        /// </summary>
        [Range(0, 50)]
        public int YSize { get; set; }
    }
}
