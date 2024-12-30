using System.Collections.Generic;

using NewBinusstoreAPI.Model;

using Binus.WS.Pattern.Output;

namespace NewBinusstoreAPI.Output
{
    public class ListMsUserOutput : OutputBase
    {
        public List<MsUser> ListMsUser { get; set; }

        public ListMsUserOutput()
        {
            this.ListMsUser = new List<MsUser>();
        }
    }
}
