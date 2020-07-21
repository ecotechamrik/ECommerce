using System;
using System.Collections.Generic;
using System.Text;

namespace BAL.ViewModels.User
{
    public class UserTokenResponse
    {
        public BAL.Entities.User user { get; set; }
        public string Token { get; set; }
    }
}
