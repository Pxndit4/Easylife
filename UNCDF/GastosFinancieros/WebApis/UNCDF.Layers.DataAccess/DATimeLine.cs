using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.DataAccess
{
    public class DATimeLine
    {
        public static List<MTimeLine> List(MProject ent, BaseRequest baseRequest, ref int Val, ref string Error)
        {
            List<MTimeLine> lisQuery = new List<MTimeLine>();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_TimeLine_Lis", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IProjectId", SqlDbType.Int).Value = ent.ProjectId;
                    cmd.Parameters.Add("@ILanguage", SqlDbType.VarChar).Value = baseRequest.Language;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MTimeLine entRow = new MTimeLine();
                            entRow.TimeLineId = Convert.ToInt32(reader["TimeLineId"]);
                            entRow.ProjectId = Convert.ToInt32(reader["ProjectId"]);
                            entRow.Year = Convert.ToString(reader["Year"]);
                            entRow.Month = Convert.ToString(reader["Month"]);
                            entRow.MonthName = Convert.ToString(reader["MonthName"]);
                            entRow.Day = Convert.ToString(reader["Day"]);
                            entRow.Title = Convert.ToString(reader["Title"]);
                            entRow.Description = Convert.ToString(reader["Description"]);
                            entRow.File = Constant.S3Server + Convert.ToString(reader["ImageFile"]);
                            //entRow.Advance = Convert.ToInt32(reader["Advance"]);
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

        public static MTimeLine Sel(MTimeLine ent, BaseRequest baseRequest, ref int Val)
        {
            MTimeLine result = new MTimeLine();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_TimeLine_Sel", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@ITimeLineId", SqlDbType.Int).Value = ent.TimeLineId;
                    cmd.Parameters.Add("@ILanguage", SqlDbType.VarChar).Value = baseRequest.Language;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    Val = 1;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            result.TimeLineId = Convert.ToInt32(reader["TimeLineId"]);
                            result.ProjectId = Convert.ToInt32(reader["ProjectId"]);
                            result.Year = Convert.ToString(reader["Year"]);
                            result.Month = Convert.ToString(reader["Month"]);
                            result.MonthName = Convert.ToString(reader["MonthName"]);
                            result.Day = Convert.ToString(reader["Day"]);
                            result.Title = Convert.ToString(reader["Title"]);
                            result.Description = Convert.ToString(reader["Description"]);
                            result.Date = Convert.ToDecimal(reader["Date"]);
                            result.Advance = Convert.ToInt32(reader["Advance"]);
                            result.Status = Convert.ToInt32(reader["Status"]);

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
            return result;
        }

        public static MTimeLine SelprojectTimeline(MTimeLine ent, ref int Val)
        {
            MTimeLine result = new MTimeLine();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ProjectTimeLine_Sel", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@ITimeLineId", SqlDbType.Int).Value = ent.TimeLineId;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    Val = 1;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            result.Date = Convert.ToDecimal(reader["Date"]);
                            result.Title = Convert.ToString(reader["Title"]);
                            result.Description = Convert.ToString(reader["ProjectTitle"]);

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
            return result;
        }

        public static int Insert(MTimeLine ent, ref int Val)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;

                    SqlCommand cmd = new SqlCommand("sp_TimeLine_Ins", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IProjectId", SqlDbType.Int).Value = ent.ProjectId;
                    cmd.Parameters.Add("@IDate", SqlDbType.Decimal).Value = ent.Date;
                    cmd.Parameters.Add("@ITitle", SqlDbType.VarChar).Value = ent.Title;
                    cmd.Parameters.Add("@IDescription", SqlDbType.VarChar).Value = ent.Description;
                    cmd.Parameters.Add("@IAdvance", SqlDbType.Int).Value = ent.Advance;
                    cmd.Parameters.Add("@OTimeLineId", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    ent.TimeLineId = Convert.ToInt32(cmd.Parameters["@OTimeLineId"].Value);
                    con.Close();

                    Val = 0;

                    return ent.TimeLineId;
                }
                catch (Exception ex)
                {
                    Val = 2;

                    return 0;

                }
            }

        }

        public static int Update(MTimeLine ent, ref int Val)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;

                    SqlCommand cmd = new SqlCommand("sp_TimeLine_Upd", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@ITimeLineId", SqlDbType.Int).Value = ent.TimeLineId;
                    cmd.Parameters.Add("@IDate", SqlDbType.Decimal).Value = ent.Date;
                    cmd.Parameters.Add("@ITitle", SqlDbType.VarChar).Value = ent.Title;
                    cmd.Parameters.Add("@IDescription", SqlDbType.VarChar).Value = ent.Description;
                    cmd.Parameters.Add("@IAdvance", SqlDbType.Int).Value = ent.Advance;
                    cmd.Parameters.Add("@IStatus", SqlDbType.Int).Value = ent.Status;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Val = 0;

                    return ent.TimeLineId;
                }

                catch (Exception ex)
                {
                    Val = 2;

                    return 0;

                }
            }

        }

        public static int UnApproved(MTimeLine ent)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_TimeLine_Upd_Unapproved", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@ITimeLineId", SqlDbType.Int).Value = ent.TimeLineId;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    return 0;
                }

                catch (Exception ex)
                {
                    return 2;

                }
            }

        }

        public static int Approved(MTimeLine ent)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_TimeLine_Upd_Approved", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@ITimeLineId", SqlDbType.Int).Value = ent.TimeLineId;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    return 0;
                }

                catch (Exception ex)
                {
                    return 2;

                }
            }

        }

        public static int Reject(MTimeLine ent)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_TimeLine_Reject", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@ITimeLineId", SqlDbType.Int).Value = ent.TimeLineId;
                    cmd.Parameters.Add("@IReasonReject", SqlDbType.VarChar).Value = ent.ReasonReject;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    return 0;
                }

                catch (Exception ex)
                {
                    return 2;

                }
            }

        }

        public static int Delete(MTimeLine ent, ref int Val)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;

                    SqlCommand cmd = new SqlCommand("sp_TimeLine_Del", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@ITimeLineId", SqlDbType.Int).Value = ent.TimeLineId;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    Val = 0;

                    return ent.TimeLineId;
                }

                catch (Exception ex)
                {
                    Val = 2;

                    return 0;

                }
            }

        }

        public static List<MTimeLine> Filter(MTimeLine ent, ref int Val)
        {
            List<MTimeLine> lisQuery = new List<MTimeLine>();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_TimeLine_Fil", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IProjectId", SqlDbType.Int).Value = ent.ProjectId;
                    cmd.Parameters.Add("@ITitle", SqlDbType.VarChar).Value = ent.Title;
                    cmd.Parameters.Add("@IStartDate", SqlDbType.Decimal).Value = ent.StartDate;
                    cmd.Parameters.Add("@IEndDate", SqlDbType.Decimal).Value = ent.EndDate;
                    cmd.Parameters.Add("@IStatus", SqlDbType.Int).Value = ent.Status;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MTimeLine entRow = new MTimeLine();
                            entRow.TimeLineId = Convert.ToInt32(reader["TimeLineId"]);
                            entRow.ProjectId = Convert.ToInt32(reader["ProjectId"]);
                            entRow.Date = Convert.ToDecimal(reader["Date"]);
                            entRow.Title = Convert.ToString(reader["Title"]);
                            entRow.Description = Convert.ToString(reader["Description"]);
                            entRow.Status = Convert.ToInt32(reader["Status"]);
                            //entRow.Advance = Convert.ToInt32(reader["Advance"]);
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

        public static List<MTimeLine> ListUnApproved(MTimeLine ent, BaseRequest baseRequest, ref int Val)
        {
            List<MTimeLine> lisQuery = new List<MTimeLine>();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_TimeLine_ListUnApproved", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IApproved", SqlDbType.Int).Value = ent.Approved;
                    cmd.Parameters.Add("@IUserId", SqlDbType.Int).Value = baseRequest.Session.UserId;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MTimeLine entRow = new MTimeLine();
                            entRow.TimeLineId = Convert.ToInt32(reader["TimeLineId"]);
                            entRow.ProjectId = Convert.ToInt32(reader["ProjectId"]);
                            entRow.Title = Convert.ToString(reader["Title"]);
                            entRow.TitleProject = Convert.ToString(reader["TitleProject"]);
                            entRow.ReasonReject = Convert.ToString(reader["ReasonReject"]);
                            entRow.Date = Convert.ToDecimal(reader["Date"]);
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


    }
}
