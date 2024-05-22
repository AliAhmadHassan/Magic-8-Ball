using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Magic8Ball.Application.Exceptions;
using Magic8Ball.Application.Errors;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace Magic8Ball.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        /// <summary>
        /// Método construtor da classe
        /// </summary>
        /// <param name="next">Objeto de controle das requisições</param>
        /// <param name="logger">Objeto de log</param>
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        /// <summary>
        /// Método de flow da aplicação
        /// </summary>
        /// <param name="httpContext">Contexto http</param>
        /// <returns>Indicativo de processo async</returns>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Magic8Ball.Application.Validator.ValidationException ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
            catch (Exception ex)
            {
                if (IsInternalServerError(ex))
                {
                    _logger.LogError(ex, ex.Message);
                }

                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static bool IsInternalServerError(Exception exception)
        {
            return GetStatusCode(exception) == 500;
        }

        private static int GetStatusCode(Exception exception)
        {
            return exception is BaseException
                ? ((BaseException)exception).StatusCodeException
                : (int)HttpStatusCode.InternalServerError;
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = GetStatusCode(exception);
            var problemDetails = new ProblemDetailsFields
            {
                Status = context.Response.StatusCode,
                Title = "Error",
                Type = exception.GetType().Name,
                Detail = exception.Message,
                Instance = exception?.TargetSite?.Name ?? string.Empty,
                FieldErrors = exception.Data.Contains("FieldErrors") ? (List<FieldError>)exception.Data["FieldErrors"] : null
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
        }

        private static Task HandleExceptionAsync(HttpContext context, Magic8Ball.Application.Validator.ValidationException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exception.ValidationResultModel.StatusCode;
            return context.Response.WriteAsync(JsonSerializer.Serialize(exception.ValidationResultModel));
        }
    }
}