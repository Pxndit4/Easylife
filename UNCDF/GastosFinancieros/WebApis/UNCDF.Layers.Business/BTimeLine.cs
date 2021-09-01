using System;
using System.Collections.Generic;
using System.Text;
using UNCDF.Layers.DataAccess;
using UNCDF.Layers.Model;


namespace UNCDF.Layers.Business
{
    public class BTimeLine
    {        
        public static int Approved(int TimeLineId, int OngUserId)
        {
            MTimeLine ent = new MTimeLine();
            ent.TimeLineId = TimeLineId;

            try
            {

                return DATimeLine.Approved(ent);
            }
            catch (Exception)
            {

                return 2;
            }

        }

        public static int UnApproved(MTimeLine ent)
        {
            try
            {

                return DATimeLine.UnApproved(ent);
            }
            catch (Exception)
            {

                return 2;
            }
        }


        public static List<MTimeLine> List(MProject ent, BaseRequest baseRequest, ref int Val, ref string Error)
        {
            return DATimeLine.List(ent, baseRequest, ref Val, ref Error);
        }
        public static MTimeLine Sel(MTimeLine ent, BaseRequest baseRequest, ref int Val)
        {
            return DATimeLine.Sel(ent, baseRequest, ref Val);
        }


        public static int Insert(MTimeLine ent, ref int Val)
        {
            return DATimeLine.Insert(ent, ref Val);

        }

        public static int Update(MTimeLine ent, ref int Val)
        {
            return DATimeLine.Update(ent, ref Val);
        }

        public static int Delete(MTimeLine ent, ref int Val)
        {
            return DATimeLine.Delete(ent, ref Val);
        }

        public static List<MTimeLine> Filter(MTimeLine ent, ref int Val)
        {
            return DATimeLine.Filter(ent, ref Val);
        }
        public static List<MTimeLine> ListUnApproved(MTimeLine ent, BaseRequest baseRequest, ref int Val)
        {
            return DATimeLine.ListUnApproved(ent, baseRequest, ref Val);
        }

        public static int Reject(MTimeLine ent, ref int Val)
        {
            try
            {
                return DATimeLine.Reject(ent);

            }
            catch (Exception)
            {

                return 2;
            }

        }
        

        public static MTimeLine SelprojectTimeline(MTimeLine ent, ref int Val)
        {
            return DATimeLine.SelprojectTimeline(ent, ref Val);
        }
    }
}
