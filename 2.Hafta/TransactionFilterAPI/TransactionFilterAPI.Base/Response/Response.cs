﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TransactionFilterAPI.Base.Response
{
    public partial class ApiResponse
    {
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

        public ApiResponse(string message = null)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                Success = true;
            }

            else
            {
                Success = false;
                Message = message;
            }
        }

        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public partial class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Response { get; set; }

        public ApiResponse(bool isSuccess)
        {
            Success = isSuccess;
            Response = default;
            Message = isSuccess ? "Success" : "Error";
        }

        public ApiResponse(string message)
        {
            Success = false;
            Response = default;
            Message = message;
        }
    }
}
