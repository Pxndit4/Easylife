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
    public class ProjectExclusionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [Route("0/InsertProjectExclusion")]
        public ProjectExclusionResponse InsertProjectDonation([FromBody] ProjectExclusionRequest request)
        {
            ProjectExclusionResponse response = new ProjectExclusionResponse();

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

            MProjectExclusion ent = new MProjectExclusion();

            ent.ListCode = request.projectExclusion.ListCode;

            int Val = 0;

            foreach(string code in ent.ListCode)
            {
                MProjectExclusion obj = new MProjectExclusion();
                obj.ProjectCode = code;
                int respt = BProjectExclusion.Insert(obj);

            }

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorInsert, "Project Exclusion");
            }

            response.projectExclusion = ent;

            return response;
        }

        [HttpPost]
        [Route("0/ListProjectsCodeExclusions")]
        public ProjectExclusionsResponse ListProjectsCodeExclusions([FromBody] ProjectExclusionRequest request)
        {
            ProjectExclusionsResponse response = new ProjectExclusionsResponse();

            try
            {
                if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
                {
                    response.Code = "2";
                    response.Message = Messages.ApplicationTokenNoAutorize;
                    return response;
                }

               
                List<MProjectExclusion> projectsCode = BProjectExclusion.ListProjectCodeExcluded();

                response.Code = "0";
                response.Message = "Success";
                response.projectExclusions = projectsCode.ToArray();
            }
            catch (Exception ex)
            {
                response.Code = "2";
                response.Message = ex.Message;
            }

            return response;

        }

        [HttpPost]
        [Route("0/DeleteProjectCode")]
        public ProjectExclusionResponse DeleteProjectCode([FromBody] ProjectExclusionRequest request)
        {
            ProjectExclusionResponse response = new ProjectExclusionResponse();
            MProjectExclusion ent = new MProjectExclusion();

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

            ent.ProjectCode = request.projectExclusion.ProjectCode;

            int Val = 0;

            int rpt = BProjectExclusion.Delete(ent, ref Val);

            //Record the audit
            //Val = BAudit.RecordAudit("Banner", ent.BannerId, 3, baseRequest.Session.UserId);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorDelete, "Banner");
            }

            response.projectExclusion = ent;

            return response;
        }
        [HttpPost]
        [Route("0/InsertDeparmentExclusion")]
        public DeparmentExclusionResponse InsertDeparmentExclusion([FromBody] DeparmentExclusionRequest request)
        {
            DeparmentExclusionResponse response = new DeparmentExclusionResponse();

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

            MDeparmentExclusion ent = new MDeparmentExclusion();

            ent.ListCode = request.deparmentExclusion.ListCode;

            int Val = 0;

            foreach (string code in ent.ListCode)
            {
                MDeparmentExclusion obj = new MDeparmentExclusion();
                obj.DeparmentCode = code;
                int respt = BDeparmentExclusion.Insert(obj);

            }

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorInsert, "Project Exclusion");
            }

            response.deparmentExclusion = ent;

            return response;
        }

        [HttpPost]
        [Route("0/ListDeparmentCodeExclusions")]
        public DeparmentExclusionsResponse ListDeparmentCodeExclusions([FromBody] DeparmentExclusionRequest request)
        {
            DeparmentExclusionsResponse response = new DeparmentExclusionsResponse();

            try
            {
                if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
                {
                    response.Code = "2";
                    response.Message = Messages.ApplicationTokenNoAutorize;
                    return response;
                }


                List<MDeparmentExclusion> deparmentCode = BDeparmentExclusion.ListDeparmentCodeExcluded();

                response.Code = "0";
                response.Message = "Success";
                response.departementExclusions = deparmentCode.ToArray();
            }
            catch (Exception ex)
            {
                response.Code = "2";
                response.Message = ex.Message;
            }

            return response;

        }

        [HttpPost]
        [Route("0/DeleteDeparmentCode")]
        public DeparmentExclusionResponse DeleteDeparmentCode([FromBody] DeparmentExclusionRequest request)
        {
            DeparmentExclusionResponse response = new DeparmentExclusionResponse();
            MDeparmentExclusion ent = new MDeparmentExclusion();

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

            ent.DeparmentCode = request.deparmentExclusion.DeparmentCode;

            int Val = 0;

            int rpt = BDeparmentExclusion.Delete(ent, ref Val);

            //Record the audit
            //Val = BAudit.RecordAudit("Banner", ent.BannerId, 3, baseRequest.Session.UserId);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorDelete, "Banner");
            }

            response.deparmentExclusion = ent;

            return response;
        }

        [HttpPost]
        [Route("0/FilDeparmentCodeExcluded")]
        public DeparmentsResponse FilDeparmentCodeExcluded([FromBody] BaseRequest request)
        {
            DeparmentsResponse response = new DeparmentsResponse();

            try
            {
                if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
                {
                    response.Code = "2";
                    response.Message = Messages.ApplicationTokenNoAutorize;
                    return response;
                }

                List<MDeparment> deparments = BDeparmentExclusion.FilDeparmentCodeExcluded();

                response.Deparments = deparments.ToArray();
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



        [HttpPost]
        [Route("0/InsertPracticeAreaExclusion")]
        public PracticeAreaExclusionResponse InsertPracticeAreaExclusion([FromBody] PracticeAreaExclusionRequest request)
        {
            PracticeAreaExclusionResponse response = new PracticeAreaExclusionResponse();

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

            MPracticeAreaExclusion ent = new MPracticeAreaExclusion();

            ent.ListCode = request.practiceAreaExclusion.ListCode;

            int Val = 0;

            foreach (string code in ent.ListCode)
            {
                MPracticeAreaExclusion obj = new MPracticeAreaExclusion();
                obj.PracticeArea = code;
                int respt = BPracticeAreaExclusion.Insert(obj);

            }

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorInsert, "Project Exclusion");
            }

            response.practiceAreaExclusion = ent;

            return response;
        }

        [HttpPost]
        [Route("0/ListPracticeAreasExclusions")]
        public PracticeAreasResponse ListPracticeAreasExclusions([FromBody] PracticeAreaExclusionRequest request)
        {
            PracticeAreasResponse response = new PracticeAreasResponse();

            try
            {
                if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
                {
                    response.Code = "2";
                    response.Message = Messages.ApplicationTokenNoAutorize;
                    return response;
                }


                List<MPracticeAreaExclusion> list = BPracticeAreaExclusion.ListPracticeAreaCodeExcluded();

                response.Code = "0";
                response.Message = "Success";
                response.practiceAreaExclusions = list.ToArray();
            }
            catch (Exception ex)
            {
                response.Code = "2";
                response.Message = ex.Message;
            }

            return response;

        }

        [HttpPost]
        [Route("0/FilPracticeAreasExclusions")]
        public PracticeAreasResponse FilPracticeAreasExclusions([FromBody] PracticeAreaExclusionRequest request)
        {
            PracticeAreasResponse response = new PracticeAreasResponse();
            MPracticeAreaExclusion ent = new MPracticeAreaExclusion();



            try
            {
                if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
                {
                    response.Code = "2";
                    response.Message = Messages.ApplicationTokenNoAutorize;
                    return response;
                }
                int val = 0;
                ent.PracticeArea = request.practiceAreaExclusion.PracticeArea;
                List<MPracticeAreaExclusion> list = BPracticeAreaExclusion.List(ent,ref val);

                response.Code = "0";
                response.Message = "Success";
                response.practiceAreaExclusions = list.ToArray();
            }
            catch (Exception ex)
            {
                response.Code = "2";
                response.Message = ex.Message;
            }

            return response;

        }



        [HttpPost]
        [Route("0/DeletePracticeAreaExclusion")]
        public PracticeAreaExclusionResponse DeletePracticeAreaExclusion([FromBody] PracticeAreaExclusionRequest request)
        {
            PracticeAreaExclusionResponse response = new PracticeAreaExclusionResponse();
            MPracticeAreaExclusion ent = new MPracticeAreaExclusion();

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

            ent.PracticeArea = request.practiceAreaExclusion.PracticeArea;

            int Val = 0;

            int rpt = BPracticeAreaExclusion.Delete(ent, ref Val);

            //Record the audit
            //Val = BAudit.RecordAudit("Banner", ent.BannerId, 3, baseRequest.Session.UserId);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorDelete, "Practice Area");
            }

            response.practiceAreaExclusion = ent;

            return response;
        }


    }
}
