using NewBinusstoreAPI.Model;
using Binus.WS.Pattern.Output;

namespace NewBinusstoreAPI.Output
{
    public class MsUserOutput : OutputBase
    {
        public MsUser MsUser{ get; set; }


        public MsUserOutput()
        {
            this.MsUser = new MsUser();
        }
    }
}
