namespace FleaMarket
{
    /// <summary> 
    /// Contains values of the collection "ApplicationConfigurations" of the appsettings file.
    /// </summary>
    public class ApplicationConfigurations
    {
        /// <summary>
        /// The path to the folder with images
        /// </summary>
        public string ImagesFolder { get; set; }

        /// <summary>
        /// The name of the placeholder image
        /// </summary>
        public string ImagePlaceholderPath { get; set; }

        /// <summary>
        /// The symbol of the using currency in project
        /// </summary>
        public string CurrencySymbol { get; set; }
    }
}
