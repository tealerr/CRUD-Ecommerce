using Common.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Responses
{
    public class BaseResponse
    {
        public int Result { get; set; }
        public object Detail { get; set; }
        public string Message { get; set; }

        public BaseResponse Success(object detail = null, string message = "Success!")
        {
            return new BaseResponse()
            {
                Result = CodeHelper.SUCCESS,
                Detail = detail,
                Message = message
            };
        }

        public BaseResponse Fail(object? detail = null, string message = "")
        {
            return new BaseResponse()
            {
                Result = CodeHelper.FAIL,
                Detail = detail,
                Message = message
            };
        }

        public BaseResponse FailToken(object detail, string message = "")
        {
            return new BaseResponse()
            {
                Result = CodeHelper.FAIL,
                Detail = detail,
                Message = message
            };
        }

        public BaseResponse FailInternalError()
        {
            return new BaseResponse()
            {
                Result = CodeHelper.FAIL,
                Detail = null,
                Message = MessageHelper.INTERNAL_ERROR_MESSAGE
            };
        }

        public BaseResponse Fail(object message)
        {
            throw new NotImplementedException();
        }

    }

    public class SegmentResponse
    {
        public bool success { get; set; }
    }

}
