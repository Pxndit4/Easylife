using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UNCDF.Layers.Business;
using UNCDF.Layers.Model;

namespace UNCDF.WebApi.Config.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribersController : ControllerBase
    {

        [HttpPost]
        [Route("0/InsertSubscribers")]
        public SubscribersResponse InsertSubscribers([FromBody] SubscribersRequest request)
        {
            SubscribersResponse response = new SubscribersResponse();

            MSubscribers SubscribersBE = new MSubscribers();

            BaseRequest baseRequest = new BaseRequest();

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            //baseRequest.Language = request.Language;
            baseRequest.Session = request.Session;

            SubscribersBE.Email = request.Subscribers.Email;

            int Val = 0;

            BSubscribers.Insert(SubscribersBE, ref Val);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = "Success";
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorInsert, "Subscribers");
            }

            response.Subscribers = SubscribersBE;

            return response;
        }


        [HttpPost]
        [Route("0/GetSubscribers")]
        public SubscribersLisResponse GetSubscribers([FromBody] SubscribersRequest request)
        {
            SubscribersLisResponse response = new SubscribersLisResponse();
            List<MSubscribers> listsub = new List<MSubscribers>();
            MSubscribers subs = new MSubscribers();

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            subs.Email = request.Subscribers.Email;
            
            BaseRequest baseRequest = new BaseRequest();

            baseRequest.Language = request.Language;
            baseRequest.Session = request.Session;

            int Val = 0;

            listsub = BSubscribers.List(subs, ref Val);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorObtainingReults, "Parameters");
            }
            else
            {
                response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.NotReults, "Parameters");
            }

            response.SubscribersList = listsub.ToArray();

            return response;
        }

    }
}
