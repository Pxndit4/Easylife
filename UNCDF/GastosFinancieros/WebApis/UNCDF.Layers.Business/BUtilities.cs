using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UNCDF.Layers.Model;
using UNCDF.Layers.DataAccess;

namespace UNCDF.Layers.Business
{
    public static class BUtilities
    {
        public static int UnApproved(int TimeLineId, int OngUserId, MAwsEmail mAwsEmail)
        {
            MTimeLine ent = new MTimeLine();
            ent.TimeLineId = TimeLineId;

            MUser userBE = new MUser();
            userBE.UserId = OngUserId;

            try
            {
                if (BProfile.ValidateUserPM(userBE).Equals(1))
                {
                    int val = BTimeLine.UnApproved(ent);

                    if (val.Equals(0))
                    {
                        ent = BTimeLine.SelprojectTimeline(ent, ref val);

                        List<MUser> lstUser = BUser.LisApproved(ref val);

                        string Message = "Dear user, the following timeline has been modified and requires your approval: <br>";
                        Message = Message + "<b>° Project: </b>" + ent.Description + "<br>";
                        Message = Message + "<b>° TimeLine: </b>" + ent.Title + "<br>";
                        Message = Message + "<b>° Fecha: </b>" + DateTime.Now.ToShortDateString() + "<br>";
                        Message = Message + "<br> Sincerely, UNITLIFE";

                        mAwsEmail.Message = Message;

                        foreach (MUser user in lstUser)
                        {
                            mAwsEmail.Subject = "UNITLIFE - Modified Timeline";
                            mAwsEmail.ToEmail = user.User;

                            BAwsSDK.SendEmailAsync(mAwsEmail);
                        }
                    }

                    return val;
                }

                return 0;
            }
            catch (Exception)
            {

                return 2;
            }

        }

        public static int RejectMail(int TimeLineId, List<MUser> Users, string Reason, MAwsEmail mAwsEmail)
        {
            try
            {
                int val = 0;
                MTimeLine ent = new MTimeLine();
                ent.TimeLineId = TimeLineId;
                ent = BTimeLine.SelprojectTimeline(ent, ref val);

                string Message = "Dear user, the following timeline has been rejected and requires your approval: <br>";
                Message = Message + "<b>° Project: </b>" + ent.Description + "<br>";
                Message = Message + "<b>° TimeLine: </b>" + ent.Title + "<br>";
                Message = Message + "<b>° Fecha: </b>" + DateTime.Now.ToShortDateString() + "<br>";
                Message = Message + "<b>° Reason: </b>" + Reason + "<br>";
                Message = Message + "<br> Sincerely, UNITLIFE";

                mAwsEmail.Message = Message;

                foreach (MUser user in Users)
                {
                    mAwsEmail.Subject = "UNITLIFE - Rejected Timeline";
                    mAwsEmail.ToEmail = user.User;

                    BAwsSDK.SendEmailAsync(mAwsEmail);
                }

                return 0;
            }
            catch (Exception)
            {

                return 2;
            }
        }


    }
}
