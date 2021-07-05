using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Data;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.DataAccess
{
    public class DAProject
    {
        public static int Insert(MProject ent)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_Project_Ins", con);
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IProjectId", SqlDbType.VarChar).Value = ent.ProjectId;
                    cmd.Parameters.Add("@IDepartment", SqlDbType.VarChar).Value = ent.Department;
                    cmd.Parameters.Add("@IPProjectCode", SqlDbType.VarChar).Value = ent.ProjectCode;
                    cmd.Parameters.Add("@IDescription", SqlDbType.VarChar).Value = ent.Description;
                    cmd.Parameters.Add("@IType", SqlDbType.VarChar).Value = ent.Type;

                    cmd.Parameters.Add("@IEffectiveStatus", SqlDbType.VarChar).Value = ent.EffectiveStatus;
                    cmd.Parameters.Add("@IStatusEffDate", SqlDbType.Int).Value = ent.StatusEffDate;
                    cmd.Parameters.Add("@IStatusEffSeq", SqlDbType.Int).Value = ent.StatusEffSeq;

                    cmd.Parameters.Add("@IStatus", SqlDbType.VarChar).Value = ent.Status;
                    
                    cmd.Parameters.Add("@IStatusDescription", SqlDbType.VarChar).Value = ent.StatusDescription;


                    cmd.Parameters.Add("@IStartDate", SqlDbType.Int).Value = ent.StartDate;
                    cmd.Parameters.Add("@IEndDate", SqlDbType.Int).Value = ent.EndDate;
                    cmd.Parameters.Add("@ITitle", SqlDbType.VarChar).Value = ent.Title;
                    cmd.Parameters.Add("@IAwardId", SqlDbType.VarChar).Value = ent.AwardId;
                    cmd.Parameters.Add("@IAwardStatus", SqlDbType.VarChar).Value = ent.AwardStatus;

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

        public static MProject Get(MProject ent)
        {
            MProject result = new MProject();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_Project_Get", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IProjectId", SqlDbType.Int).Value = ent.ProjectId;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

        //            Val = 1;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            result.ProjectId = Convert.ToInt32(reader["ProjectId"]);
                            result.ProjectCode = Convert.ToString(reader["ProjectCode"]);
                            result.Title = Convert.ToString(reader["Title"]);
                            result.Type = Convert.ToString(reader["Type"]);
                            result.Description = Convert.ToString(reader["Description"]);
                            result.StartDate = Convert.ToInt32(reader["StartDate"]) ;
                            result.EndDate = Convert.ToInt32(reader["EndDate"]);
                            result.Status = Convert.ToString(reader["Status"]);
                            result.Department = Convert.ToString(reader["Department"]);
                            result.EffectiveStatus = Convert.ToString(reader["EffectiveStatus"]);
                            result.StatusEffDate = Convert.ToInt32(reader["StatusEffDate"]);
                            result.StatusEffSeq = Convert.ToInt32(reader["StatusEffSeq"]);
                            result.StatusDescription = Convert.ToString(reader["StatusDescription"]);
                            result.AwardId = Convert.ToString(reader["AwardId"]);
                            result.AwardStatus = Convert.ToString(reader["AwardStatus"]);
                            result.Image = (Convert.ToString(reader["Image"]).Equals("")) ? "" : Convert.ToString(reader["Image"]);
                            result.Video = (Convert.ToString(reader["Video"]).Equals("")) ? "" : Convert.ToString(reader["Video"]);
                            
                                
                            //                  Val = 0;
                        }
                    }
                    con.Close();
                }

                catch (Exception ex)
                {
            //        Val = 2;
                }
            }
            return result;
        }


        public static List<MProject> List(MProject ent)
        {
            List<MProject> lisQuery = new List<MProject>();

            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("pr_Project_Lis", con);
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

        public static int Update(MProject ent)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    
                    SqlCommand cmd = new SqlCommand("pr_Project_Upd", con);
                    cmd.Parameters.Add("@IProjectId", SqlDbType.Int).Value = ent.ProjectId;
                    cmd.Parameters.Add("@IImage", SqlDbType.VarChar).Value = ent.Image;
                    cmd.Parameters.Add("@IVideo", SqlDbType.VarChar).Value = ent.Video;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    return ent.ProjectId;
                }

                catch (Exception ex)
                {
                    

                    return ent.ProjectId = 0;

                }
            }

        }


    }
}
