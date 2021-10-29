using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletPlusApi.Core.Constants;

namespace WalletPlusApi.Core.Common.Responses
{
    public static class Response
    {
        public static PaginatedResponse OkObj(object obj, long totalRecords, int skip, int take, int recordsRetrieved,
            string message = "success")
        {

            return new PaginatedResponse()
            {
                StatusCode = ResponseStatus.Success.Code,
                Data = obj,
                Status = ResponseStatus.Success.Message,
                RecordsRetrieved = recordsRetrieved,
                TotalRecords = totalRecords,
                NextSkipValue = skip,
                PageSize = take,
                CurrentSkipValue = skip - take
            };
        }

        
        public static BaseResponse Ok(object obj, string message = "success")
        {

            return new BaseResponse()
            {
                StatusCode = ResponseStatus.Success.Code,
                Data = obj,
                Status = message,
            };
        }

        public static BaseResponse Created(object obj, string message = "success")
        {

            return new BaseResponse()
            {
                StatusCode = ResponseStatus.Success.Code,
                Status = message,
                Data = obj
            };
        }

        public static BaseResponse BadRequest(object obj = null, string message = "failed")
        {

            return new BaseResponse()
            {
                StatusCode = ResponseStatus.Fail.Code,
                Data = obj,
                Status = message,
            };
        }
        public static PaginatedResponse BadRequestObj(object obj = null, string message = "failed")
        {

            return new PaginatedResponse()
            {
                StatusCode = ResponseStatus.Fail.Code,
                Data = obj,
                Status = message,
            };
        }

        public static BaseResponse NotFound(object data)
        {

            return new BaseResponse()
            {
                StatusCode = ResponseStatus.Fail.Code,
                Data = data,
                Status = ResponseStatus.Fail.Message,
            };
        }
    }
}
