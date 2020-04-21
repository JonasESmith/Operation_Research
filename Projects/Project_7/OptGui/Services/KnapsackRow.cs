namespace OptGui.Services
{
    /// <summary>
    /// Defines the <see cref="IKnapsackRow" />.
    /// </summary>
    public interface IKnapsackRow
    {
        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the Weight.
        /// </summary>
        double? Weight { get; set; }

        /// <summary>
        /// Gets or sets the Value.
        /// </summary>
        double? Value { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="KnapsackRow" />.
    /// </summary>
    public class KnapsackRow : IKnapsackRow
    {
        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Weight.
        /// </summary>
        public double? Weight { get; set; }

        /// <summary>
        /// Gets or sets the Value.
        /// </summary>
        public double? Value { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="KnapsackRow"/> class.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <param name="weight">The weight<see cref="double"/>.</param>
        /// <param name="value">The value<see cref="double"/>.</param>
        public KnapsackRow(string name, double? weight, double? value)
        {
            this.Name   = name;
            this.Weight = weight;
            this.Value  = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KnapsackRow"/> class.
        /// </summary>
        public KnapsackRow()
        {
            this.Name   = null;
            this.Weight = null;
            this.Value  = null;
        }
    }
}
