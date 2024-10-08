﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker.Domain.Models
{
    public class ResponseModel<T> where T : class
    {
        public ResponseModel(T result, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            Result = result;
            StatusCode = statusCode;
        }

        public ResponseModel(string? error, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            Error = error;
            StatusCode = statusCode;
        }
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public string? Error { get; set; }
        public T? Result { get; set; }

    }
}
