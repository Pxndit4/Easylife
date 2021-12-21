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
    public class LogLoadController : Controller
    {
        [HttpPost]
        [Route("0/GetLogsLoad")]
        public LogLoadListResponse GetLogsLoad([FromBody] LogLoadRequest request)
        {
            LogLoadListResponse response = new LogLoadListResponse();

            try
            {
                if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
                {
                    response.Code = "2";
                    response.Message = Messages.ApplicationTokenNoAutorize;
                    return response;
                }
                int Val = 0;

                MLogLoad ent = new MLogLoad();
                ent.TypeParamId = request.LogLoad.TypeParamId;
                ent.StartDate = request.LogLoad.StartDate;
                ent.EndDate = request.LogLoad.EndDate;

                List<MLogLoad> list = BLogLoad.List(ent, ref Val);

                response.LogLoadList = list.ToArray();
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
