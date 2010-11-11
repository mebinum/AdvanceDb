using Sitecore.Data.Fields;
using Sitecore.Search.Crawlers.FieldCrawlers;

namespace Sitecore.SharedSource.Search.FieldCrawlers
{
   public class DefaultFieldCrawler : FieldCrawlerBase
   {
      public DefaultFieldCrawler(Field field)
         : base(field)
      {
      }
   }
}
