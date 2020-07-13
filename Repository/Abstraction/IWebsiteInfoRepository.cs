using BAL.Entities;
using BAL.ViewModels;
using System.Collections.Generic;

namespace Repository.Abstraction
{
    public interface IWebsiteInfoRepository : IRepository<WebsiteInfo>
    {
        void DbInitialize();
    }
}
