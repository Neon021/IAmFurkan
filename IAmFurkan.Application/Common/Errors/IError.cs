using System.Net;

namespace IAmFurkan.Application.Common.Errors;
public interface IError
{
    public HttpStatusCode StatusCode { get; }
    public string ErrorMessage { get; }
}
