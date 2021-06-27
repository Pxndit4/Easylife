using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;

namespace UNCDF.CMS
{
    public class WebApiUser
    {
        public List<MUser> GetUsers(MUser MUser)
        {
            List<MUser> users = new List<MUser>();
            UserRequest request = new UserRequest();
            UsersResponse response = new UsersResponse();

            request.User = MUser;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("security/api/User", "GetUsers", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<UsersResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    users = response.Users;
                }
            }

            return users;
        }

        public List<MUser> GetUsersApproved(MUser MUser)
        {
            List<MUser> users = new List<MUser>();
            UserRequest request = new UserRequest();
            UsersResponse response = new UsersResponse();

            request.User = MUser;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("security/api/User", "GetUsersApproved", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<UsersResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    users = response.Users;
                }
            }

            return users;
        }

        public List<MUser> GetUsersUnApproved(MUser MUser)
        {
            List<MUser> users = new List<MUser>();
            UserRequest request = new UserRequest();
            UsersResponse response = new UsersResponse();

            request.User = MUser;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("security/api/User", "GetUsersUnApproved", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<UsersResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    users = response.Users;
                }
            }

            return users;
        }

        public MUser GetUser(MUser MUser)
        {
            MUser user = new MUser();
            UserRequest request = new UserRequest();
            UserResponse response = new UserResponse();

            request.User = MUser;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("security/api/User", "GetUser", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<UserResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    user = response.User;
                }
            }

            return user;
        }


        public MUser LoginUser(MUser MUser)
        {
            MUser user = new MUser();
            UserRequest request = new UserRequest();
            UserResponse response = new UserResponse();

            request.User = MUser;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("security/api/User", "LoginUser", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<UserResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    user = response.User;
                }
            }

            return user;
        }

        public string InsertUser(MUser MUser)
        {
            UserRequest request = new UserRequest();
            UserResponse response = new UserResponse();
            string returnMsg = string.Empty;

            request.User = MUser;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("security/api/User", "InsertUser", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<UserResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking User api";
            }

            return returnMsg;
        }

        public string UpdatetUser(MUser MUser)
        {
            UserRequest request = new UserRequest();
            UserResponse response = new UserResponse();
            string returnMsg = string.Empty;

            request.User = MUser;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("security/api/User", "UpdatetUser", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<UserResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking User api";
            }

            return returnMsg;
        }

        public string DeletMUser(MUser MUser)
        {
            UserRequest request = new UserRequest();
            UserResponse response = new UserResponse();
            string returnMsg = string.Empty;

            request.User = MUser;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("security/api/User", "DeletMUser", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<UserResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking User api";
            }

            return returnMsg;
        }

        public string ChangePassword(MUser MUser)
        {
            UserRequest request = new UserRequest();
            UserResponse response = new UserResponse();
            string returnMsg = string.Empty;

            request.User = MUser;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("security/api/User", "ChangePassword", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<UserResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking User api";
            }

            return returnMsg;
        }


        public string DeletMUserApproved(MUser MUser)
        {
            UserRequest request = new UserRequest();
            UserResponse response = new UserResponse();
            string returnMsg = string.Empty;

            request.User = MUser;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("security/api/User", "DeletMUserApproved", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<UserResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking User api";
            }

            return returnMsg;
        }

        public string InsertUserApproved(MUser MUser)
        {
            UserRequest request = new UserRequest();
            UserResponse response = new UserResponse();
            string returnMsg = string.Empty;

            request.User = MUser;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("security/api/User", "InsertUserApproved", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<UserResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking User api";
            }

            return returnMsg;
        }
    }

    internal class UserRequest
    {
        public MUser User { get; set; }
        public string ApplicationToken { get; set; }
    }

    internal class UsersResponse
    {
        public List<MUser> Users { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }

    internal class UserResponse
    {
        public MUser User { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }
}