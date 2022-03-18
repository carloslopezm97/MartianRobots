using System.ComponentModel.DataAnnotations;

namespace MartianRobots.Models.Response
{
    public class GridResponseModel
    {
        /// <summary>
        /// Grid identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// X size of the grid.
        /// </summary>
        public int XSize { get; set; }

        /// <summary>
        /// Y size of the grid.
        /// </summary>
        public int YSize { get; set; }
    }
}
