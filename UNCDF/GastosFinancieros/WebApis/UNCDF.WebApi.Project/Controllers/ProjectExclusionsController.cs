using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using UNCDF.Layers.Model;
using UNCDF.Layers.Business;
using UNCDF.Utilities;
using System.Transactions;

namespace UNCDF.WebApi.Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("_corsPolicy")]
    public class ProjectExclusionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [Route("0/InsertProjectExclusion")]
        public FundsResponse InsertProjectExclusion([FromBody] BaseRequest request)
        {
            FundsResponse response = new FundsResponse();

            try
            {
                if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
                {
                    response.Code = "2";
                    response.Message = Messages.ApplicationTokenNoAutorize;
                    return response;
                }

                List<MFund> funds = BFund.List();

                response.Funds = funds.ToArray();
                response.Code = "0";
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.Code = "2";
                response.Message = ex.Message;
            }

            return response;

        }
    }
}
