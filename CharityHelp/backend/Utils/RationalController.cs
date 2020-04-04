using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace charity.Utils
{
    [DefaultStatusCode(418)]
    public class ImATeapotObjectResult : ObjectResult
    {
        private const int DefaultStatusCode = 418;

        public ImATeapotObjectResult(object value)
            : base(value)
        {
            StatusCode = 418;
        }
    }

    [DefaultStatusCode(418)]
    public class ImATeapotResult : StatusCodeResult
    {
        private const int DefaultStatusCode = 418;

        public ImATeapotResult()
            : base(418)
        {
        }
    }


    /// <summary>
    ///     for lolz
    /// </summary>
    public class RationalController : ControllerBase
    {
        [NonAction]
        public virtual ImATeapotObjectResult ImATeapot([ActionResultObjectValue] object value)
        {
            return new ImATeapotObjectResult(value);
        }

        [NonAction]
        public virtual ImATeapotResult ImATeapot()
        {
            return new ImATeapotResult();
        }
    }
}