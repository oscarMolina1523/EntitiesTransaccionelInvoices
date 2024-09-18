using Domain.Endpoint.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net;
using System.Net.Http;
//using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using WebApi.Models;

namespace WebApi.App_Start.Handlers
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
        public override Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            context.Result = new BaseExceptionResponse(context.Exception);
            return Task.CompletedTask;
        }

        public class BaseExceptionResponse : IHttpActionResult
        {
            private readonly Exception exception;
            public BaseExceptionResponse(Exception exception)
            {
                this.exception = exception;
            }

            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                if (exception is EntityNotFoundException)
                {
                    var responseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);
                    responseMessage.Content = GetStringContent(exception, responseMessage.StatusCode);
                    return Task.FromResult(responseMessage);
                }

                var defaultResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                defaultResponse.Content = GetStringContent(exception, defaultResponse.StatusCode);
                return Task.FromResult(defaultResponse);
            }

            public static StringContent GetStringContent(Exception exception, HttpStatusCode code)
            {
                var response = new HttpResponse
                {
                    Message = exception.Message,
                    StatusCode = code,
                    Success = false,
                    Data = exception.Data
                };

                var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
                return new StringContent(JsonConvert.SerializeObject(response, settings), Encoding.UTF8, "application/json");
            }
        }
    }
}