using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Repos
{
    public class GetLinkOfLocation
    {
        public static string GetLink(object obj) => getLink(obj);

        private static string getLink(object obj)
        {
            if (obj is City _)
            {
                return ConstantStrings.UrlLinkToCities;
            }
            else if (obj is Region _)
            {
                return ConstantStrings.UrlLinkToRegions;
            }
            else if (obj is Country _)
            {
                return ConstantStrings.UrlLinkToContries;
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
