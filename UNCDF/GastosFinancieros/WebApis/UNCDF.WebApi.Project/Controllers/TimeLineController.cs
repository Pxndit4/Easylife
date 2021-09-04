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
using System.IO;

namespace UNCDF.WebApi.Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("_corsPolicy")]
    public class TimeLineController : Controller
    {
        private readonly IOptions<AppSettings> _appSettings;
        private readonly MAwsEmail _MAwsEmail;
        private readonly MAwsS3 _MAwsS3;

        public TimeLineController(IOptions<AppSettings> appSettings, IOptions<MAwsEmail> MAwsEmail, IOptions<MAwsS3> MAwsS3)
        {
            _appSettings = appSettings;
            _MAwsEmail = MAwsEmail.Value;
            _MAwsS3 = MAwsS3.Value;
        }

        [HttpPost]
        [Route("0/GetTimeLine")]
        public TimeLineResponse GetTimeLine([FromBody] TimeLineRequest request)
        {
            TimeLineResponse response = new TimeLineResponse();
            MTimeLine timeLine = new MTimeLine();

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            BaseRequest baseRequest = new BaseRequest();

            timeLine.TimeLineId = request.TimeLine.TimeLineId;

            baseRequest.Language = request.Language;
            baseRequest.Session = request.Session;

            int Val = 0;

            timeLine = BTimeLine.Sel(timeLine, baseRequest, ref Val);


            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorSelect, "TimeLine");
            }
            else
            {
                response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.NoExistsSelect, "TimeLine");
            }

            response.TimeLine = timeLine;

            return response;
        }


        [HttpPost]
        [Route("0/InsertTimeline")]
        public TimeLineResponse InsertTimeline([FromBody] TimeLineRequest request)
        {
            TimeLineResponse response = new TimeLineResponse();

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            BaseRequest baseRequest = new BaseRequest();

            baseRequest.Language = request.Language;
            baseRequest.Session = request.Session;

            MTimeLine MTimeLine = new MTimeLine();

            MTimeLine.ProjectId = request.TimeLine.ProjectId;
            MTimeLine.Date = request.TimeLine.Date;
            MTimeLine.Title = request.TimeLine.Title;
            MTimeLine.Description = request.TimeLine.Description;
            MTimeLine.Advance = request.TimeLine.Advance;
            MTimeLine.Status = 1;

            int Val = 0;

            MTimeLine.TimeLineId = BTimeLine.Insert(MTimeLine, ref Val);

            if (Val.Equals(0))
            {
                //Record the audit
                Val = BAudit.RecordAudit("TimeLine", MTimeLine.TimeLineId, 1, baseRequest.Session.UserId);

                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;

                Val = BUtilities.UnApproved(MTimeLine.TimeLineId, request.Session.UserId, _MAwsEmail);

                if (Val.Equals(0))
                {
                    response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = Messages.Success;
                }
                else if (Val.Equals(2))
                {
                    response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = String.Format(Messages.ErrorInsert, "TimeLine");
                }

            }
            else if (Val.Equals(2))
            {   
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorInsert, "TimeLine");
            }

            response.TimeLine = MTimeLine;

            return response;
        }


        [HttpPost]
        [Route("0/UpdateTimeline")]
        public TimeLineResponse UpdateTimeline([FromBody] TimeLineRequest request)
        {
            TimeLineResponse response = new TimeLineResponse();

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            BaseRequest baseRequest = new BaseRequest();

            baseRequest.Language = request.Language;
            baseRequest.Session = request.Session;

            MTimeLine MTimeLine = new MTimeLine();

            MTimeLine.TimeLineId = request.TimeLine.TimeLineId;
            MTimeLine.ProjectId = request.TimeLine.ProjectId;
            MTimeLine.Date = request.TimeLine.Date;
            MTimeLine.Title = request.TimeLine.Title;
            MTimeLine.Description = request.TimeLine.Description;
            MTimeLine.Advance = request.TimeLine.Advance;
            MTimeLine.Status = request.TimeLine.Status;


            int Val = 0;

            MTimeLine.TimeLineId = BTimeLine.Update(MTimeLine, ref Val);

            if (Val.Equals(0))
            {
                //Record the audit
                Val = BAudit.RecordAudit("TimeLine", MTimeLine.TimeLineId, 2, baseRequest.Session.UserId);

                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;

                Val = BUtilities.UnApproved(MTimeLine.TimeLineId, request.Session.UserId, _MAwsEmail);

                if (Val.Equals(0))
                {
                    response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = Messages.Success;
                }
                else if (Val.Equals(2))
                {
                    response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = String.Format(Messages.ErrorUpdate, "TimeLine");
                }
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorUpdate, "TimeLine");
            }

            response.TimeLine = MTimeLine;

            return response;
        }



        [HttpPost]
        [Route("0/DeleteTimeline")]
        public TimeLineResponse DeleteTimeline([FromBody] TimeLineRequest request)
        {
            TimeLineResponse response = new TimeLineResponse();

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            BaseRequest baseRequest = new BaseRequest();

            baseRequest.Language = request.Language;
            baseRequest.Session = request.Session;

            MTimeLine MTimeLine = new MTimeLine();

            MTimeLine.TimeLineId = request.TimeLine.TimeLineId;

            int Val = 0;

            MTimeLine.TimeLineId = BTimeLine.Delete(MTimeLine, ref Val);

            if (Val.Equals(0))
            {
                //Record the audit
                Val = BAudit.RecordAudit("TimeLine", MTimeLine.TimeLineId, 3, baseRequest.Session.UserId);

                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorDelete, "TimeLine");
            }

            response.TimeLine = MTimeLine;

            return response;
        }

        [HttpPost]
        [Route("0/SearchTimeLines")]
        public TimeLinesResponse SearchTimeLines([FromBody] TimeLineRequest request)
        {
            TimeLinesResponse response = new TimeLinesResponse();
            List<MTimeLine> timeLines = new List<MTimeLine>();
            MTimeLine MTimeLine = new MTimeLine();
            BaseRequest baseRequest = new BaseRequest();

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            MTimeLine.ProjectId = request.TimeLine.ProjectId;
            MTimeLine.Title = request.TimeLine.Title;
            MTimeLine.StartDate = request.TimeLine.StartDate;
            MTimeLine.EndDate = request.TimeLine.EndDate;

            baseRequest.Language = request.Language;
            baseRequest.Session = request.Session;

            int Val = 0;

            timeLines = BTimeLine.Filter(MTimeLine, ref Val);


            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorObtainingReults, "TimeLines");
            }
            else
            {
                response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.NotReults, "TimeLines");
            }

            response.TimeLines = timeLines.ToArray();

            return response;
        }


        [HttpPost]
        [Route("0/FilterTimeLines")]
        public TimeLinesResponse FilterTimeLines([FromBody] TimeLineRequest request)
        {
            TimeLinesResponse response = new TimeLinesResponse();
            List<MTimeLine> timeLines = new List<MTimeLine>();
            MTimeLine MTimeLine = new MTimeLine();
            BaseRequest baseRequest = new BaseRequest();

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            MTimeLine.ProjectId = request.TimeLine.ProjectId;
            MTimeLine.Title = request.TimeLine.Title;
            MTimeLine.StartDate = request.TimeLine.StartDate;
            MTimeLine.EndDate = request.TimeLine.EndDate;
            MTimeLine.Status = request.TimeLine.Status;

            baseRequest.Language = request.Language;
            baseRequest.Session = request.Session;

            int Val = 0;

            timeLines = BTimeLine.Filter(MTimeLine, ref Val);


            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorObtainingReults, "TimeLines");
            }
            else
            {
                response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.NotReults, "TimeLines");
            }

            response.TimeLines = timeLines.ToArray();

            return response;
        }
        [HttpPost]
        [Route("0/GetTimeLineUnApproved")]
        public TimeLinesResponse GetTimeLineUnApproved([FromBody] TimeLineRequest request)
        {
            TimeLinesResponse response = new TimeLinesResponse();
            List<MTimeLine> timeLines = new List<MTimeLine>();
            MTimeLine MTimeLine = new MTimeLine();
            BaseRequest baseRequest = new BaseRequest();


            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            baseRequest.Language = request.Language;
            baseRequest.Session = request.Session;

            MTimeLine.Approved = request.TimeLine.Approved;

            int Val = 0;

            timeLines = BTimeLine.ListUnApproved(MTimeLine, baseRequest, ref Val);


            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorObtainingReults, "TimeLines");
            }
            else
            {
                response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.NotReults, "TimeLines");
            }

            response.TimeLines = timeLines.ToArray();

            return response;
        }

        [HttpPost]
        [Route("0/TimeLineApproved")]
        public TimeLineResponse TimeLineApproved([FromBody] TimeLineRequest request)
        {
            TimeLineResponse response = new TimeLineResponse();

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            BaseRequest baseRequest = new BaseRequest();

            baseRequest.Language = request.Language;
            baseRequest.Session = request.Session;

            MTimeLine TimeLineBE = new MTimeLine();

            TimeLineBE.TimeLineId = request.TimeLine.TimeLineId;

            int Val = 0;

            TimeLineBE.TimeLineId = BTimeLine.Approved(TimeLineBE.TimeLineId, request.Session.UserId);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;

                if (Val.Equals(0))
                {
                    response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = Messages.Success;
                }
                else if (Val.Equals(2))
                {
                    response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = String.Format(Messages.ErrorUpdate, "TimeLine");
                }
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorUpdate, "TimeLine");
            }

            response.TimeLine = TimeLineBE;

            return response;
        }

        [HttpPost]
        [Route("0/TimeLineReject")]
        public TimeLineResponse TimeLineReject([FromBody] TimeLineRequest request)
        {
            TimeLineResponse response = new TimeLineResponse();

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            BaseRequest baseRequest = new BaseRequest();

            baseRequest.Language = request.Language;
            baseRequest.Session = request.Session;

            MTimeLine TimeLineBE = new MTimeLine();

            TimeLineBE.TimeLineId = request.TimeLine.TimeLineId;
            TimeLineBE.ReasonReject = request.TimeLine.ReasonReject;

            int Val = 0;

            Val = BTimeLine.Reject(TimeLineBE, ref Val);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;

                BUtilities.RejectMail(TimeLineBE.TimeLineId, BUser.ListProjectUser(TimeLineBE.TimeLineId, ref Val), TimeLineBE.ReasonReject, _MAwsEmail);
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorUpdate, "TimeLine");
            }

            response.TimeLine = TimeLineBE;

            return response;
        }
    }
}