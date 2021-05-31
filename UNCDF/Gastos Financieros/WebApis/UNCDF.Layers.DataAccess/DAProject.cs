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
                    cmd.Parameters.Add("@IPProjectCode", SqlDbType.VarChar).Value = ent.ProjectCode;
                    cmd.Parameters.Add("@IDescription", SqlDbType.VarChar).Value = ent.Description;
                    cmd.Parameters.Add("@IType", SqlDbType.VarChar).Value = ent.Type;
                    cmd.Parameters.Add("@IStatus", SqlDbType.VarChar).Value = ent.Status;
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
    }
}
