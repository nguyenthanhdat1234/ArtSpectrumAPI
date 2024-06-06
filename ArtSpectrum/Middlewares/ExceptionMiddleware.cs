using ArtSpectrum.Commons;
using ArtSpectrum.Exceptions;
using Microsoft.Extensions.Primitives;
using System.Text.Json;

namespace ArtSpectrum.Middlewares
{
    public class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private readonly IDictionary<Type, Action<HttpContext, Exception>> _exceptionHandlers;

        public ExceptionMiddleware()
        {
            _exceptionHandlers = new Dictionary<Type, Action<HttpContext, Exception>>
        {
            //
            // Note: Handle every exception you throw here
            //

            // Built-in exceptions
            { typeof(KeyNotFoundException), HandleKeyNotFoundException },
            { typeof(InvalidDataException), HandleInvalidDataException },

            // Custom exceptions
            { typeof(ConflictException), HandleConflictException },
            { typeof(RequestValidationException), HandleRequestValidationException },
        };
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            var type = ex.GetType();
            if (_exceptionHandlers.ContainsKey(type))
            {
                _exceptionHandlers[type].Invoke(context, ex);
                return;
            }

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await WriteExceptionMessageAsync(context, ex);
        }

        private static async void HandleKeyNotFoundException(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await WriteExceptionMessageAsync(context, ex);
        }

        private static async void HandleInvalidDataException(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
            await WriteExceptionMessageAsync(context, ex);
        }

        private static async void HandleConflictException(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = StatusCodes.Status409Conflict;
            await WriteExceptionMessageAsync(context, ex);
        }

        private static async void HandleRequestValidationException(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;

            var rve = (RequestValidationException)ex;
            var data = new Dictionary<string, StringValues>();
            foreach (var vf in rve.Errors)
            {
                var propName = vf.PropertyName.ToLower();
                if (!data.ContainsKey(propName))
                {
                    data.Add(propName, vf.ErrorMessage);
                }
                else
                {
                    data[propName] = StringValues.Concat(data[propName], vf.ErrorMessage);
                }
            }

            var result = Result<Dictionary<string, StringValues>>.Fail(ex) with
            {
                Data = data
            };
            await context.Response.Body.WriteAsync(SerializeToUtf8BytesWeb(result));
        }

        private static async void HandleTokenValidationException(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await WriteExceptionMessageAsync(context, ex);
        }

        private static async void HandleUserAuthenticationException(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await WriteExceptionMessageAsync(context, ex);
        }

        private static async void HandleUnsupportedMediaTypeException(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = StatusCodes.Status415UnsupportedMediaType;
            await WriteExceptionMessageAsync(context, ex);
        }

        private static async Task WriteExceptionMessageAsync(HttpContext context, Exception ex)
        {
            await context.Response.Body.WriteAsync(SerializeToUtf8BytesWeb(Result<string>.Fail(ex)));
        }

        private static byte[] SerializeToUtf8BytesWeb<T>(T value)
        {
            return JsonSerializer.SerializeToUtf8Bytes<T>(value, new JsonSerializerOptions(JsonSerializerDefaults.Web));
        }
    }
}
