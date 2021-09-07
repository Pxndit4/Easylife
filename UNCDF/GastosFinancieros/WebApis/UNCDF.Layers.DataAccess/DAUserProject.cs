using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Data;
using UNCDF.Layers.Model;


namespace UNCDF.Layers.DataAccess
{
    public class DAUserProject
    {
        public static int Insert(MUserProject ent)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_UserProject_Ins", con);
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IUserId", SqlDbType.Int).Value = ent.UserId;
                    cmd.Parameters.Add("@IProjectId", SqlDbType.Int).Value = ent.ProjectId;

                    con.Open();

                    cmd.ExecuteNonQuery();

                    con.Close();
                }
                catch (Exception ex)
                {
                    return 2;
                }
            }

            return 0;
        }

        public static List<MProject> NotAssignedList(MProject ent)
        {
            List<MProject> lisQuery = new List<MProject>();

            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("pr_UserProj_NotAssignedList", con);
                    cmd.Parameters.Add("@IUserId", SqlDbType.VarChar).Value = ent.UserId;
                    cmd.Parameters.Add("@IProjectCode", SqlDbType.VarChar).Value = ent.ProjectCode;
                    cmd.Parameters.Add("@IStarDate", SqlDbType.VarChar).Value = ent.StartDate;
                    cmd.Parameters.Add("@IEndDate", SqlDbType.VarChar).Value = ent.EndDate;
                    cmd.Parameters.Add("@ITitle", SqlDbType.VarChar).Value = ent.Title;
                    cmd.Parameters.Add("@IEffectiveStatus", SqlDbType.VarChar).Value = ent.EffectiveStatus;
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MProject entRow = new MProject();
                            entRow.ProjectId = Convert.ToInt32(reader["ProjectId"]);
                            entRow.ProjectCode = Convert.ToString(reader["ProjectCode"]);
                            entRow.Title = Convert.ToString(reader["Title"]);
                            entRow.Type = Convert.ToString(reader["Type"]);
                            entRow.Description = Convert.ToString(reader["Description"]);
                            entRow.StartDate = Convert.ToInt32(reader["StartDate"]);
                            entRow.EndDate = Convert.ToInt32(reader["EndDate"]);
                            entRow.Status = Convert.ToString(reader["Status"]);
                            entRow.Department = Convert.ToString(reader["Department"]);
                            entRow.EffectiveStatus = Convert.ToString(reader["EffectiveStatus"]);
                            entRow.StatusEffDate = Convert.ToInt32(reader["StatusEffDate"]);
                            entRow.StatusEffSeq = Convert.ToInt32(reader["StatusEffSeq"]);
                            entRow.AwardId = Convert.ToString(reader["AwardId"]);
                            entRow.AwardStatus = Convert.ToString(reader["AwardStatus"]);
                            entRow.StatusDescription = Convert.ToString(reader["StatusDescription"]);
                            entRow.IsVisible = Convert.ToBoolean(reader["IsVisible"]);
                            entRow.Donation = Convert.ToBoolean(reader["Donation"]);
                            lisQuery.Add(entRow);
                        }
                    }
                    con.Close();
                }

                catch (Exception ex)
                {
                    lisQuery = new List<MProject>();
                }
            }
            return lisQuery;
        }



        public static List<MUserProject> List(MUserProject ent, ref int Val)
        {
            List<MUserProject> lisQuery = new List<MUserProject>();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_UserProject_List", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IUser", SqlDbType.VarChar).Value = ent.User;
                    cmd.Parameters.Add("@IName", SqlDbType.VarChar).Value = ent.Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    Val = 0;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MUserProject entRow = new MUserProject();
                            entRow.UserId = Convert.ToInt32(reader["UserId"]);
                            entRow.User = Convert.ToString(reader["User"]);
                            entRow.Profile = Convert.ToString(reader["Profile"]);
                            entRow.Name = Convert.ToString(reader["Name"]);
                            lisQuery.Add(entRow);
                        }
                    }
                    con.Close();
                }

                catch (Exception ex)
                {
                    Val = 2;
                }
            }
            return lisQuery;
        }


        public static int Delete(MUserProject ent)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_UserProject_Del", con);
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IUserId", SqlDbType.Int).Value = ent.UserId;
                    cmd.Parameters.Add("@IProjectId", SqlDbType.Int).Value = ent.ProjectId;

                    con.Open();

                    cmd.ExecuteNonQuery();

                    con.Close();
                }
                catch (Exception ex)
                {
                    return 2;
                }
            }

            return 0;
        }
        public static List<MUserProject> ListAssigned(MUserProject ent, ref int Val)
        {
            List<MUserProject> lisQuery = new List<MUserProject>();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_UserProjAssigned_List", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IUserId", SqlDbType.Int).Value = ent.UserId;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    Val = 0;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MUserProject entRow = new MUserProject();
                            entRow.UserId = Convert.ToInt32(reader["UserId"]);
                            entRow.ProjectId = Convert.ToInt32(reader["ProjectId"]);
                            entRow.ProjectCode = Convert.ToString(reader["ProjectCode"]);
                            entRow.Title = Convert.ToString(reader["Title"]);
                            entRow.StartDate = Convert.ToInt32(reader["StartDate"]);
                            entRow.EndDate = Convert.ToInt32(reader["EndDate"]);
                            entRow.EffectiveStatus = Convert.ToString(reader["EffectiveStatus"]);
                            lisQuery.Add(entRow);
                        }
                    }
                    con.Close();
                }

                catch (Exception ex)
                {
                    Val = 2;
                }
            }
            return lisQuery;
        }


    }
}
