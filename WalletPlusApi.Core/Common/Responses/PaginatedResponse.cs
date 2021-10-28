using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletPlusApi.Core.Common.Responses
{
    public class PaginatedResponse :BaseResponse
    {
        public long TotalRecords { get; set; }
        public int RecordsRetrieved { get; set; }
        public int PageSize { get; set; }
        public int NextSkipValue { get; set; }
        public int CurrentSkipValue { get; set; }
    }
}
