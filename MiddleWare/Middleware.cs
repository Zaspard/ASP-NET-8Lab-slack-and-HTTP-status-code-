using Microsoft.AspNetCore.Http;
using MiddleWare.Slack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWare
{
    public class Middleware 
    {
        private readonly RequestDelegate _next;

        public Middleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            using (var newBody = new MemoryStream())
            {
                var output = new MemoryStream();
                var oldBody = context.Response.Body;
                context.Response.Body = newBody;
                try
                {
                    await _next(context);
                    output = EditResponse(context, oldBody, JsonConvert.DeserializeObject(ReadResponse(newBody)) ?? "");
                }
                catch (JsonReaderException)
                {
                    output = EditResponse(context, oldBody, ReadResponse(newBody));
                }
                catch (InvalidOperationException)
                {
                    output = EditResponse(context, oldBody, null);
                }
                catch (Exception ex)
                {
                    output = EditResponse(context, oldBody, ex.Message, StatusCodes.Status500InternalServerError);
                    new SlackClient(ex);
                }
                finally
                {
                    await output.CopyToAsync(oldBody);
                    context.Response.Body = output;
                }
            }
        }

        private string ReadResponse(MemoryStream newBody)
        {
            newBody.Position = 0;
            return new StreamReader(newBody).ReadToEnd();
        }

        private MemoryStream EditResponse(HttpContext context, Stream originalBody, object content , int? code = null)
        {
            var json = new JObject
            {
                ["code"] = code ?? context.Response.StatusCode,
                ["content"] = content != null ? JToken.FromObject(content) : null
            };
            context.Response.StatusCode = StatusCodes.Status200OK;
            var buffer = Encoding.UTF8.GetBytes(json.ToString());
            return new MemoryStream(buffer);
        }

    }
}
