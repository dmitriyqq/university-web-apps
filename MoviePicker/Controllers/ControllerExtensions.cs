using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MoviePicker.Controllers 
{
    public class GenericResponse
    {
        [JsonProperty("ok")]
        public bool Ok { get; set;}

        [JsonProperty("error")]
        public string Error { get; set;}

        [JsonProperty("paging")]
        public Paging Paging { get; set;}
    }

    public class GenericResponse<T> : GenericResponse
    {
        [JsonProperty("payload")]
        public T Payload { get; set;}
    }

    public static class ControllerExtensions {
        public static IActionResult OkJson<T>(this ControllerBase controller, T payload) {
            return new OkObjectResult(new GenericResponse<T> { Ok = true, Payload = payload });
        }

        public static IActionResult OkCollection<T>(this ControllerBase controller, T payload, Paging paging) {
            return new OkObjectResult(new GenericResponse<T> { Ok = true, Payload = payload, Paging = paging });
        }

        public static IActionResult OkNotFound(this ControllerBase controller, string name,string id) {
            return new NotFoundObjectResult(new GenericResponse { Ok = false, Error = $"{name} {id} not found." });
        }

        public static IActionResult OkBadRequest(this ControllerBase controller, string fieldName) {
            return new BadRequestObjectResult(new GenericResponse { Ok = false, Error = $"validation failed for {fieldName}" });
        }

        public static bool ValidatePaging(this ControllerBase controller, Paging paging)
        {
            return paging.Take > 0 && paging.Take <= 100;
        }
    }
}