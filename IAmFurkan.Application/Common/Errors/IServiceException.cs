﻿using System.Net;

namespace IAmFurkan.Application.Common.Errors;
public interface IServiceException
{
    public HttpStatusCode StatusCode { get; }
    public string ErrorMessage { get;}
}
