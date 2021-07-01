using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.DataAccess
{
    public class DATimeLineMultimedia 
    { 
       public static List<MTimeLineMultimedia> List(MTimeLine ent, BaseRequest baseRequest, ref int Val)
    {
        List<MTimeLineMultimedia> lisQuery = new List<MTimeLineMultimedia>();
        using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_TimeLineMultimedia_List", con);
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add("@ITimeLineId", SqlDbType.Int).Value = ent.TimeLineId;
                cmd.Parameters.Add("@ILanguage", SqlDbType.VarChar).Value = baseRequest.Language;
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        MTimeLineMultimedia entRow = new MTimeLineMultimedia();
                        entRow.TimeLineMulId = Convert.ToInt32(reader["TimeLineMulId"]);
                        entRow.TimeLineId = Convert.ToInt32(reader["TimeLineId"]);
                        entRow.Type = Convert.ToInt32(reader["Type"]);
                        entRow.File = Constant.S3Server + Convert.ToString(reader["File"]);
                        entRow.Title = Convert.ToString(reader["Title"]);

                        lisQuery.Add(entRow);
                        Val = 0;
                    }
                }
                con.Close();
            }

            catch (SqlException ex)
            {
                Val = 2;
            }
        }
        return lisQuery;
    }
    public static List<MTimeLineMultimedia> Filter(MTimeLineMultimedia ent, ref int Val)
    {
        List<MTimeLineMultimedia> lisQuery = new List<MTimeLineMultimedia>();
        using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_TimeLineMultimedia_Fil", con);
                cmd.Parameters.Add("@ITimeLineid", SqlDbType.Int).Value = ent.TimeLineId;
                cmd.Parameters.Add("@ITitle", SqlDbType.VarChar).Value = ent.Title;
                cmd.Parameters.Add("@IType", SqlDbType.Int).Value = ent.Type;
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        MTimeLineMultimedia entRow = new MTimeLineMultimedia();
                        entRow.TimeLineMulId = Convert.ToInt32(reader["TimeLineMulId"]);
                        entRow.TimeLineId = Convert.ToInt32(reader["TimeLineid"]);
                        entRow.Type = Convert.ToInt32(reader["Type"]);
                        //entRow.File = Convert.ToString(reader["File"]);
                        entRow.File = Constant.S3Server + Convert.ToString(reader["File"]);
                        entRow.Title = Convert.ToString(reader["Title"]);
                        lisQuery.Add(entRow);

                        Val = 0;
                    }
                }
                con.Close();
            }

            catch (SqlException ex)
            {
                Val = 2;
            }
        }
        return lisQuery;
    }

    public static int Insert(MTimeLineMultimedia ent, ref int Val)
    {
        using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
        {
            try
            {
                Val = 1;

                SqlCommand cmd = new SqlCommand("sp_TimeLineMultimedia_Ins", con);
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add("@ITimeLineId", SqlDbType.Int).Value = ent.TimeLineId;
                cmd.Parameters.Add("@IType", SqlDbType.Int).Value = ent.Type;
                cmd.Parameters.Add("@IFile", SqlDbType.VarChar).Value = ent.File;
                cmd.Parameters.Add("@ITitle", SqlDbType.VarChar).Value = ent.Title;
                cmd.Parameters.Add("@OTimeLineMulId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();
                ent.TimeLineMulId = Convert.ToInt32(cmd.Parameters["@OTimeLineMulId"].Value);
                con.Close();

                Val = 0;

                return ent.TimeLineMulId;
            }
            catch (Exception ex)
            {
                Val = 2;

                return 0;

            }
        }

    }



    public static int Update(MTimeLineMultimedia ent, ref int Val)
    {
        using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
        {
            try
            {
                Val = 1;

                SqlCommand cmd = new SqlCommand("sp_TimeLineMultimedia_Upd", con);
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add("@ITimeLineMulId", SqlDbType.Int).Value = ent.TimeLineMulId;
                cmd.Parameters.Add("@IType", SqlDbType.Int).Value = ent.Type;
                cmd.Parameters.Add("@IFile", SqlDbType.VarChar).Value = ent.File;
                cmd.Parameters.Add("@ITitle", SqlDbType.VarChar).Value = ent.Title;

                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Val = 0;

                return ent.TimeLineMulId;
            }

            catch (Exception ex)
            {
                Val = 2;

                return 0;

            }
        }

    }

    public static MTimeLineMultimedia Select(MTimeLineMultimedia ent, ref int Val)
    {
        MTimeLineMultimedia result = new MTimeLineMultimedia();
        using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_TimeLineMultimedia_Sel", con);
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add("@ITimeLineMulId", SqlDbType.Int).Value = ent.TimeLineMulId;
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                Val = 1;

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.TimeLineMulId = Convert.ToInt32(reader["TimeLineMulId"]);
                        result.TimeLineId = Convert.ToInt32(reader["TimeLineid"]);
                        result.Type = Convert.ToInt32(reader["Type"]);
                        result.File = Convert.ToString(reader["File"]);
                        result.Title = Convert.ToString(reader["Title"]);
                        Val = 0;
                    }
                }
                con.Close();
            }

            catch (Exception ex)
            {
                Val = 2;
            }
        }
        return result;
    }




    public static int Delete(MTimeLineMultimedia ent, ref int Val)
    {
        using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
        {
            try
            {
                Val = 1;

                SqlCommand cmd = new SqlCommand("sp_TimeLineMultimedia_Del", con);
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add("@ITimeLineMulId", SqlDbType.Int).Value = ent.TimeLineMulId;
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                Val = 0;

                return ent.TimeLineMulId;
            }

            catch (Exception ex)
            {
                Val = 2;

                return 0;

            }
        }

    }

}
}
