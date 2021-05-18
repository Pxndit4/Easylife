using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UNCDF.Layers.Business;
using UNCDF.Layers.Model;
using UNCDF.Utilities;

namespace UNCDF.WebApi.Security.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : Controller
    {
        private readonly IOptions<AppSettings> _appSettings;
        private readonly MAwsEmail _MAwsEmail;
        private readonly MAwsS3 _MAwsS3;

        public ProfileController(IOptions<AppSettings> appSettings, IOptions<MAwsEmail> MAwsEmail, IOptions<MAwsS3> MAwsS3)
        {
            _appSettings = appSettings;
            _MAwsEmail = MAwsEmail.Value;
            _MAwsS3 = MAwsS3.Value;
        }

        [HttpPost]
        [Route("0/GetProfiles")]
        public ProfilesResponse GetProfiles([FromBody] ProfileRequest request)
        {
            ProfilesResponse response = new ProfilesResponse();
            MProfile profile = new MProfile();
            List<MProfile> profiles = new List<MProfile>();
            BaseRequest baseRequest = new BaseRequest();

            try
            {
                /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
                if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
                {
                    response.Code = "2";
                    response.Message = Messages.ApplicationTokenNoAutorize;
                    return response;
                }
                /*************FIN DEL METODO*************/

                profile.Description = request.Profile.Description;

                int Val = 0;

                profiles = BProfile.Lis(profile, ref Val);

                if (Val.Equals(0))
                {
                    response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = Messages.Success;
                }
                else if (Val.Equals(2))
                {
                    response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = String.Format(Messages.ErrorObtainingReults, "Profiles");
                }
                else
                {
                    response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = String.Format(Messages.NotReults, "Profiles");
                }
            }
            catch (Exception ex)
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = ex.Message;
            }

            response.Profiles = profiles.ToArray();

            return response;
        }

        [HttpPost]
        [Route("0/GetProfilesByUser")]
        public ProfilesResponse GetProfilesByUser([FromBody] ProfileRequest request)
        {
            ProfilesResponse response = new ProfilesResponse();
            MProfile profile = new MProfile();
            List<MProfile> profiles = new List<MProfile>();
            BaseRequest baseRequest = new BaseRequest();

            try
            {
                /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
                if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
                {
                    response.Code = "2";
                    response.Message = Messages.ApplicationTokenNoAutorize;
                    return response;
                }
                /*************FIN DEL METODO*************/

                profile.UserId = request.Profile.UserId;

                int Val = 0;

                profiles = BProfile.LisByUser(profile, ref Val);

                if (Val.Equals(0))
                {
                    response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = Messages.Success;
                }
                else if (Val.Equals(2))
                {
                    response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = String.Format(Messages.ErrorObtainingReults, "Profiles");
                }
                else
                {
                    response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = String.Format(Messages.NotReults, "Profiles");
                }
            }
            catch (Exception ex)
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = ex.Message;
            }            

            response.Profiles = profiles.ToArray();

            return response;
        }

        [HttpPost]
        [Route("0/InsertProfile")]
        public ProfileResponse InsertProfile([FromBody] ProfileRequest request)
        {
            ProfileResponse response = new ProfileResponse();
            MProfile profile = new MProfile();
            List<MProfileOptions> options = new List<MProfileOptions>();

            try
            {
                /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
                if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
                {
                    response.Code = "2";
                    response.Message = Messages.ApplicationTokenNoAutorize;
                    return response;
                }
                /*************FIN DEL METODO*************/

                BaseRequest baseRequest = new BaseRequest();

                profile.Description = request.Profile.Description;

                foreach (MProfileOptions item in request.Options)
                {
                    options.Add(item);
                }

                int ProfileId = 0;
                int Val = BProfile.Insert(profile, options, ref ProfileId);

                profile.ProfileId = ProfileId;

                if (Val.Equals(0))
                {
                    response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = Messages.Success;
                }
                else if (Val.Equals(2))
                {
                    response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = String.Format(Messages.ErrorInsert, "Profile");
                }
            }
            catch (Exception ex)
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = ex.Message;
            }            

            response.Profile = profile;

            return response;
        }

        [HttpPost]
        [Route("0/UpdateProfile")]
        public ProfileResponse UpdateProfile([FromBody] ProfileRequest request)
        {
            ProfileResponse response = new ProfileResponse();
            MProfile profile = new MProfile();
            List<MProfileOptions> options = new List<MProfileOptions>();

            BaseRequest baseRequest = new BaseRequest();

            try
            {
                /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
                if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
                {
                    response.Code = "2";
                    response.Message = Messages.ApplicationTokenNoAutorize;
                    return response;
                }
                /*************FIN DEL METODO*************/

                profile.Description = request.Profile.Description;
                profile.ProfileId = request.Profile.ProfileId;
                profile.Status = request.Profile.Status;

                foreach (MProfileOptions item in request.Options)
                {
                    options.Add(item);
                }

                int Val = BProfile.Update(profile, options);

                if (Val.Equals(0))
                {
                    response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = Messages.Success;
                }
                else if (Val.Equals(2))
                {
                    response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = String.Format(Messages.ErrorUpdate, "Profile");
                }
            }
            catch (Exception ex)
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = ex.Message;
            }
          
            response.Profile = profile;

            return response;
        }

        [HttpPost]
        [Route("0/GetProfile")]
        public ProfileResponse GetProfile([FromBody] ProfileRequest request)
        {
            ProfileResponse response = new ProfileResponse();
            MProfile profile = new MProfile();
            List<MProfileOptions> options = new List<MProfileOptions>();

            BaseRequest baseRequest = new BaseRequest();

            try
            {
                /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
                if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
                {
                    response.Code = "2";
                    response.Message = Messages.ApplicationTokenNoAutorize;
                    return response;
                }
                /*************FIN DEL METODO*************/

                profile.ProfileId = request.Profile.ProfileId;

                int Val = 0;
                profile = BProfile.Sel(profile, ref Val);
                options = BProfile.SelOptions(profile, ref Val);

                if (Val.Equals(0))
                {
                    response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = Messages.Success;
                }
                else if (Val.Equals(2))
                {
                    response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = String.Format(Messages.ErrorSelect, "Profile");
                }

            }
            catch (Exception ex)
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = ex.Message;
            }
            
            response.Profile = profile;
            response.Options = options.ToArray();

            return response;
        }

        [HttpPost]
        [Route("0/DeleteProfile")]
        public ProfileResponse DeleteProfile([FromBody] ProfileRequest request)
        {
            ProfileResponse response = new ProfileResponse();
            BaseRequest baseRequest = new BaseRequest();
            MProfile profile = new MProfile();

            try
            {
                baseRequest.Language = request.Language;
                baseRequest.Session = request.Session;

                /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
                if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
                {
                    response.Code = "2";
                    response.Message = Messages.ApplicationTokenNoAutorize;
                    return response;
                }
                /*************FIN DEL METODO*************/                

                profile.ProfileId = request.Profile.ProfileId;

                int Val = 0;

                profile.ProfileId = BProfile.Delete(profile, ref Val);

                if (Val.Equals(0))
                {
                    response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = Messages.Success;
                }
                else if (Val.Equals(2))
                {
                    response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = String.Format(Messages.ErrorDelete, "Profile");
                }
            }
            catch (Exception ex)
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = ex.Message;
            }
            
            response.Profile = profile;

            return response;
        }

        [HttpPost]
        [Route("0/GetUsersProfile")]
        public UsersProfileResponse GetUsersProfile([FromBody] ProfileRequest request)
        {
            UsersProfileResponse response = new UsersProfileResponse();
            MProfile profile = new MProfile();
            List<MProfileUser> profiles = new List<MProfileUser>();
            BaseRequest baseRequest = new BaseRequest();

            try
            {
                /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
                if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
                {
                    response.Code = "2";
                    response.Message = Messages.ApplicationTokenNoAutorize;
                    return response;
                }
                /*************FIN DEL METODO*************/

                profile.ProfileId = request.Profile.ProfileId;

                int Val = 0;

                profiles = BProfile.LisUsers(profile, ref Val);

                if (Val.Equals(0))
                {
                    response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = Messages.Success;
                }
                else if (Val.Equals(2))
                {
                    response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = String.Format(Messages.ErrorObtainingReults, "User profiles");
                }
                else
                {
                    response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = String.Format(Messages.NotReults, "User profiles");
                }
            }
            catch (Exception ex)
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = ex.Message;
            }
            
            response.UsersProfile = profiles.ToArray();

            return response;
        }

        [HttpPost]
        [Route("0/GetUsersUnAssigned")]
        public UsersProfileResponse GetUsersUnAssigned([FromBody] ProfileUserRequest request)
        {
            UsersProfileResponse response = new UsersProfileResponse();
            MProfileUser profile = new MProfileUser();
            List<MProfileUser> profiles = new List<MProfileUser>();
            BaseRequest baseRequest = new BaseRequest();

            try
            {
                /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
                if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
                {
                    response.Code = "2";
                    response.Message = Messages.ApplicationTokenNoAutorize;
                    return response;
                }
                /*************FIN DEL METODO*************/

                profile.ProfileId = request.ProfileUser.ProfileId;
                profile.User = request.ProfileUser.User;
                profile.Name = request.ProfileUser.Name;

                int Val = 0;

                profiles = BProfile.LisUsersUnAssigned(profile, ref Val);

                if (Val.Equals(0))
                {
                    response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = Messages.Success;
                }
                else if (Val.Equals(2))
                {
                    response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = String.Format(Messages.ErrorObtainingReults, "Users UnAssigned");
                }
                else
                {
                    response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = String.Format(Messages.NotReults, "Users UnAssigned");
                }
            }
            catch (Exception ex)
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = ex.Message;
            }            

            response.UsersProfile = profiles.ToArray();

            return response;
        }

        [HttpPost]
        [Route("0/RegisterUsersProfile")]
        public UserProfileResponse RegisterUsersProfile([FromBody] ProfileUserRequest request)
        {
            UserProfileResponse response = new UserProfileResponse();
            MProfileUser profile = new MProfileUser();

            BaseRequest baseRequest = new BaseRequest();

            try
            {
                /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
                if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
                {
                    response.Code = "2";
                    response.Message = Messages.ApplicationTokenNoAutorize;
                    return response;
                }
                /*************FIN DEL METODO*************/

                profile.ProfileId = request.ProfileUser.ProfileId;
                profile.UserId = request.ProfileUser.UserId;

                int Val = 0;

                Val = BProfile.InsertUser(profile);

                if (Val.Equals(0))
                {
                    response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = Messages.Success;
                }
                else if (Val.Equals(2))
                {
                    response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = String.Format(Messages.ErrorInsert, "Users Profile");
                }
            }
            catch (Exception ex)
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = ex.Message;
            }            

            response.UserProfile = profile;

            return response;
        }

        [HttpPost]
        [Route("0/DeleteUsersProfile")]
        public UserProfileResponse DeleteUsersProfile([FromBody] ProfileUserRequest request)
        {
            UserProfileResponse response = new UserProfileResponse();
            MProfileUser profile = new MProfileUser();

            BaseRequest baseRequest = new BaseRequest();

            try
            {
                /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
                if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
                {
                    response.Code = "2";
                    response.Message = Messages.ApplicationTokenNoAutorize;
                    return response;
                }
                /*************FIN DEL METODO*************/

                profile.ProfileId = request.ProfileUser.ProfileId;
                profile.UserId = request.ProfileUser.UserId;

                int Val = 0;

                Val = BProfile.DeleteUser(profile);

                if (Val.Equals(0))
                {
                    response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = Messages.Success;
                }
                else if (Val.Equals(2))
                {
                    response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = String.Format(Messages.ErrorDelete, "Users Profile");
                }
            }
            catch (Exception ex)
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = ex.Message;
            }
           
            response.UserProfile = profile;

            return response;
        }
    }    
}
