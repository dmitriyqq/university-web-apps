using Microsoft.AspNetCore.Mvc;

namespace MoviePicker.Controllers {
    public static class ControllerExtensions {
        public static IActionResult OkJson(this ControllerBase controller, object payload) {
            return new OkObjectResult(new { ok = true, payload });
        }

        public static IActionResult OkNotFound(this ControllerBase controller, string message) {
            return new NotFoundObjectResult(new { ok = false, error = message });
        }

        public static IActionResult OkBadRequest(this ControllerBase controller, string message) {
            return new BadRequestObjectResult(new { ok = false, error = message });
        }
    }
}