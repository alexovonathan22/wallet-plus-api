using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletPlusApi.Core.Common.Responses
{
    public class BaseResponse
    {
        [JsonProperty("statusCode")]
        public string StatusCode { get; set; } // "00 - success, 01 - failed, 02 - pending"

        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("responseAt")]
        public string Time { get; set; } = $"{DateTimeOffset.Now.ToLocalTime()}";

        [JsonProperty("data")]
        public object Data { get; set; }
    }

}
