using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;

namespace UNCDF.CMS
{
    public class WebApiProfile
    {
        public List<MProfile> GetProfilesByUser(MProfile MProfile)
        {
            List<MProfile> profiles = new List<MProfile>();
            ProfileRequest request = new ProfileRequest();
            ProfilesResponse response = new ProfilesResponse();

            request.Profile = MProfile;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("security/api/Profile", "GetProfilesByUser", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<ProfilesResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    profiles = response.Profiles;
                }
            }

            return profiles;
        }

        public List<MProfile> GetProfiles(MProfile MProfile)
        {
            List<MProfile> profiles = new List<MProfile>();
            ProfileRequest request = new ProfileRequest();
            ProfilesResponse response = new ProfilesResponse();

            request.Profile = MProfile;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("security/api/Profile", "GetProfiles", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<ProfilesResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    profiles = response.Profiles;
                }
            }

            return profiles;
        }

        public MProfile GetProfile(MProfile MProfile)
        {
            MProfile profile = new MProfile();
            ProfileRequest request = new ProfileRequest();
            ProfileResponse response = new ProfileResponse();

            request.Profile = MProfile;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("security/api/Profile", "GetProfile", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<ProfileResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    profile = response.Profile;
                    profile.Options = response.Options;
                }
            }

            return profile;
        }

        public string InsertProfile(MProfile MProfile)
        {
            ProfileRequest request = new ProfileRequest();
            ProfileResponse response = new ProfileResponse();

            string returnMsg = string.Empty;

            request.Profile = MProfile;
            request.Options = MProfile.Options;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("security/api/Profile", "InsertProfile", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<ProfileResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking User api";
            }

            return returnMsg;
        }

        public string UpdatMProfile(MProfile MProfile)
        {
            ProfileRequest request = new ProfileRequest();
            ProfileResponse response = new ProfileResponse();

            string returnMsg = string.Empty;

            request.Profile = MProfile;
            request.Options = MProfile.Options;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("security/api/Profile", "UpdatMProfile", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<ProfileResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking User api";
            }

            return returnMsg;
        }

        public string DeletMProfile(MProfile MProfile)
        {
            ProfileRequest request = new ProfileRequest();
            ProfileResponse response = new ProfileResponse();
            string returnMsg = string.Empty;

            request.Profile = MProfile;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("security/api/Profile", "DeletMProfile", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<ProfileResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking User api";
            }

            return returnMsg;
        }

        public List<MProfileUser> GetUsersProfile(MProfile MProfile)
        {
            List<MProfileUser> UsersProfile = new List<MProfileUser>();
            ProfileRequest request = new ProfileRequest();
            UsersProfileResponse response = new UsersProfileResponse();

            request.Profile = MProfile;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("security/api/Profile", "GetUsersProfile", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<UsersProfileResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    UsersProfile = response.UsersProfile;
                }
            }

            return UsersProfile;
        }


        public List<MProfileUser> GetUsersUnAssigned(MProfileUser MProfile)
        {
            List<MProfileUser> UsersProfile = new List<MProfileUser>();
            ProfileUserRequest request = new ProfileUserRequest();
            UsersProfileResponse response = new UsersProfileResponse();

            request.ProfileUser = MProfile;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("security/api/Profile", "GetUsersUnAssigned", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<UsersProfileResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    UsersProfile = response.UsersProfile;
                }
            }

            return UsersProfile;
        }

        public string RegisterUsersProfile(MProfileUser MProfile)
        {
            ProfileUserRequest request = new ProfileUserRequest();
            UserProfileResponse response = new UserProfileResponse();
            string returnMsg = string.Empty;

            request.ProfileUser = MProfile;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("security/api/Profile", "RegisterUsersProfile", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<UserProfileResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking User api";
            }

            return returnMsg;
        }

        public string DeleteUsersProfile(MProfileUser MProfile)
        {
            ProfileUserRequest request = new ProfileUserRequest();
            UserProfileResponse response = new UserProfileResponse();
            string returnMsg = string.Empty;

            request.ProfileUser = MProfile;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("security/api/Profile", "DeleteUsersProfile", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<UserProfileResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking User api";
            }

            return returnMsg;
        }
    }

    public class ProfileRequest
    {
        public MProfile Profile { get; set; }

        public List<MProfileOptions> Options { get; set; }

        public string ApplicationToken { get; set; }
    }

    public class ProfilesResponse
    {
        public List<MProfile> Profiles { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }

    public class ProfileResponse
    {
        public MProfile Profile { get; set; }
        public List<MProfileOptions> Options { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }

    public class UsersProfileResponse
    {
        public List<MProfileUser> UsersProfile { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }

    public class UserProfileResponse
    {
        public MProfileUser UserProfile { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }

    public class ProfileUserRequest
    {
        public MProfileUser ProfileUser { get; set; }

        public Session Session { get; set; }

        public string ApplicationToken { get; set; }
    }
}