using BusinessObject.WebModule.Models.Config;

namespace Phystones.Models.Config
{
    public class GeneralConfigModel
    {
        public ContactConfigModel Contact { get; set; }

        public SEOConfigModel SEO { get; set; }

        public WebsiteConfigModel Website { get; set; }
    }
}