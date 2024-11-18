/*************************************************************************************
 *
 * File name:   GlobalExceptionMiddleware.cs
 * Description: 
 * 
 * Version：  V1.0
 * Creator  Eason Huang
 * Create time：  2024/11/16 14:47
 * ======================================
*************************************************************************************/
using Koknight.Basic.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;
using WebFilmApi.Models;

namespace WebFilmApi.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception happened: {ex}");
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var exceptionResponse = new GlobalExceptionResponse();
            switch (ex)
            {
                case AuthenticationException ae: 
                    exceptionResponse = GenerateExceptionResponse((int)HttpStatusCode.Forbidden, ex); break;
                case InternalServerErrorException isee:
                    exceptionResponse = GenerateExceptionResponse((int)HttpStatusCode.InternalServerError, ex); break;
                case NotFoundException nfe:
                    exceptionResponse = GenerateExceptionResponse((int)HttpStatusCode.NotFound, ex); break;
                default: throw ex;
            }

            await context.Response.WriteAsJsonAsync(exceptionResponse);
        }

        private GlobalExceptionResponse GenerateExceptionResponse(int statusCode, Exception ex)
        {
            return new GlobalExceptionResponse
            {
                StatusCode = statusCode,
                Message = ex.Message,
                DetailedInformation = ex.StackTrace
            };
        }
    }
}
