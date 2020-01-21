using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;
using ReferenceProject.Exceptions;
using Microsoft.Extensions.Hosting;

namespace ReferenceProject.Middleware
{
	/// <summary>
	/// Middleware to handle exceptions.
	/// It separates exceptions based on their type and returns different status codes and answers based on it, instead of 500 Internal Server Error code in all cases.
	/// In addition, it writes them in the log.
	/// </summary>
	/// <remarks>
	/// There is another way to do this - an exception filter.
	/// However, a middleware is a preferred way to achieve this according to the official documentation.
	/// To learn more see https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/filters?view=aspnetcore-2.1#exception-filters
	/// 
	/// See also: https://github.com/drwatson1/AspNet-Core-REST-Service/wiki#unhandled-exceptions-handling
	/// </remarks>
	public class ExceptionMiddleware
	{
		RequestDelegate Next { get; }
		ILogger Logger { get; }
		IWebHostEnvironment Environment { get; }

		public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IWebHostEnvironment environment)
		{
			Environment = environment ?? throw new ArgumentNullException(nameof(environment));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			Next = next ?? throw new ArgumentNullException(nameof(next));
		}

		public async Task InvokeAsync(HttpContext context)
		{
			var body = context.Response.Body;
			try
			{
				await Next(context);
			}
			catch (Exception ex)
			{
				// If context.Response.HasStarted == true, then we can't write to the response stream anymore. So we have to restore the body.
				// If we don't do that we get an exception.
				context.Response.Body = body;
				await HandleExceptionAsync(context, ex);
			}
		}

		async Task HandleExceptionAsync(HttpContext context, Exception ex)
		{
			int statusCode = 500;

			context.Response.ContentType = "application/json";
			context.Response.StatusCode = statusCode;

			// We can decide what the status code should return
			if (ex is KeyNotFoundException)
			{
				context.Response.StatusCode = StatusCodes.Status404NotFound;
			}
			else if (ex is DuplicateKeyException)
			{
				context.Response.StatusCode = StatusCodes.Status400BadRequest;
			}

			await context.Response.WriteAsync(
				JsonConvert.SerializeObject(
					new ErrorResponse(ex, Environment.IsDevelopment())));

			if (context.Response.StatusCode == StatusCodes.Status500InternalServerError)
			{
				Logger.LogError(ex, "Unhandled exception occurred");
			}
			else
			{
				Logger.LogDebug(ex, "Unhandled exception occurred");
			}
		}

		class ErrorResponse
		{
			public ErrorResponse(Exception ex, bool includeFullExceptionInfo)
			{
				Error = new ExceptionDescription(ex);
				if (includeFullExceptionInfo)
				{
					Error.Exception = ex;
				}
			}

			public ExceptionDescription Error { get; set; }
		}

		class ExceptionDescription
		{
			public ExceptionDescription(Exception ex)
			{
				Message = ex.Message;
			}

			public string Message { get; set; }
			public Exception Exception { get; set; }
		}
	}
}
