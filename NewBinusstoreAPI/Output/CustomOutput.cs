using System.Collections.Generic;

using NewBinusstoreAPI.Model;

using Binus.WS.Pattern.Output;

namespace NewBinusstoreAPI.Output
{
    public class CustomOutput : OutputBase
    {
        public object Data { get; set; }
        public string Callback { get; set; }

        public CustomOutput()
        {
            this.Data = null;
            this.Callback = "";
        }
    }
}
