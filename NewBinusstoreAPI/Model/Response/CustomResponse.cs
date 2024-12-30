using System;


namespace NewBinusstoreAPI.Model
{
    public class CustomResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public string Callback { get; set; }
    }
}
