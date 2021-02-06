using System;

namespace FleaMarket.Services
{
    public class GuidFileNameGenerator : IFileNameGenerator
    {
        public string Generate(string baseString)
        {
            string guid = Guid.NewGuid().ToString();
            if (string.IsNullOrEmpty(baseString))
            {
                return guid;
            }

            return guid + "_" + baseString;
        }
    }
}
