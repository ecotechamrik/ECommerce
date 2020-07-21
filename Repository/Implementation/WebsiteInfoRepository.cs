using BAL.Entities;
using DAL;
using DAL.DBInitializer;
using Microsoft.EntityFrameworkCore;
using Repository.Abstraction;

namespace Repository.Implementation
{
    public class WebsiteInfoRepository : Repository<WebsiteInfo>, IWebsiteInfoRepository
    {
        DatabaseContext Context
        {
            get
            {
                return db as DatabaseContext;
            }
        }

        public WebsiteInfoRepository(DbContext _db) : base(_db)
        {

        }

        /// <summary>
        /// Initialize DB entries at the very first time if DB is blank.
        /// </summary>
        public void DbInitialize()
        {
            WebsiteInfoDbInitializer.Initialize(Context);
        }

    }
}
