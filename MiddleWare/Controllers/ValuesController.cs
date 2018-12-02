using System;
using Microsoft.AspNetCore.Mvc;

namespace MiddleWare.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class Test1Controller : ControllerBase
    {
        [HttpGet]
        public ActionResult<bool> Get()
        {
            return true;
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class Test2Controller : ControllerBase
    {
        [HttpGet]
        public ActionResult<int> Get()
        {
            return 764;
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class Test3Controller : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            return StatusCode(200);
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class Test4Controller : ControllerBase
    {
        [HttpGet]
        public object Get()
        {
            var result = new
            {
                login = 3
            };

            return result;
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class Test5Controller : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "string";
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class Test6Controller : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            return BadRequest();
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class Test7Controller : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            throw new DivideByZeroException();
        }
    }


    [Route("api/[controller]")]
    [ApiController]
    public class Test8Controller : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            return StatusCode(403);
        }
    }


    [Route("api/[controller]")]
    [ApiController]
    public class Test10Controller : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            return Unauthorized();
        }
    }


    [Route("api/[controller]")]
    [ApiController]
    public class Test11Controller : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            return null;
        }
    }


}
