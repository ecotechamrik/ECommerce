using BAL.Entities;

namespace Repository.Abstraction
{
    public interface IWebsiteInfoRepository : IRepository<WebsiteInfo>
    {
        void DbInitialize();
    }
}
