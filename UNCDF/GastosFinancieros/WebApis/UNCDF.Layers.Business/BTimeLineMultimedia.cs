using System;
using System.Collections.Generic;
using System.Text;
using UNCDF.Layers.DataAccess;
using UNCDF.Layers.Model;



namespace UNCDF.Layers.Business
{
public  class BTimeLineMultimedia 
{ 
   public static List<MTimeLineMultimedia> List(MTimeLine ent, BaseRequest baseRequest, ref int Val)
    {
        return DATimeLineMultimedia.List(ent, baseRequest, ref Val);
    }

    public static List<MTimeLineMultimedia> Filter(MTimeLineMultimedia ent, ref int Val)
    {
        return DATimeLineMultimedia.Filter(ent, ref Val);
    }


    public static int Insert(MTimeLineMultimedia ent, ref int Val)
    {
        return DATimeLineMultimedia.Insert(ent, ref Val);

    }

    public static int Update(MTimeLineMultimedia ent, ref int Val)
    {
        return DATimeLineMultimedia.Update(ent, ref Val);

    }

    public static MTimeLineMultimedia Select(MTimeLineMultimedia ent, ref int Val)
    {
        return DATimeLineMultimedia.Select(ent, ref Val);
    }

    public static int Delete(MTimeLineMultimedia ent, ref int Val)
    {
        return DATimeLineMultimedia.Delete(ent, ref Val);
    }
}
}