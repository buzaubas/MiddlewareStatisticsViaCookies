using Microsoft.AspNetCore.Mvc;

namespace MiddlewareHomework
{
    public class Statistics
    {
        private readonly RequestDelegate _request;

        public Statistics(RequestDelegate request)
        {
            request = _request;
        }

        public async Task Invoke(HttpContext context)
        {
            int requestNumber = 0;

            if (context.Request.Cookies.ContainsKey("requestNumber"))
            {
                try
                {
                    requestNumber = int.Parse(context.Request.Cookies["requestNumber"]);
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
            else
            {
                context.Response.Cookies.Append("requestNumber", requestNumber.ToString());
            }

            requestNumber = requestNumber + 1;  

            try
            {
                //context.Request.Cookies["requestNumber"] = requestNumber.ToString();

                context.Response.Cookies.Append("requestNumber", requestNumber.ToString());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }

    public static class ContentMiddlewareExtension
    {
        public static IApplicationBuilder stat(this IApplicationBuilder app)
        {
            return app.UseMiddleware<Statistics>();
        }
    }
}
