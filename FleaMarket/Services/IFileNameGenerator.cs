namespace FleaMarket.Services
{
    /// <summary>
    /// Service to generate the name of a file
    /// </summary>
    public interface IFileNameGenerator
    {
        /// <summary>
        /// Generate the name of a file
        /// </summary>
        /// <param name="baseString">The base for the generating name</param>
        /// <returns>The name of a file</returns>
        string Generate(string baseString);
    }
}
