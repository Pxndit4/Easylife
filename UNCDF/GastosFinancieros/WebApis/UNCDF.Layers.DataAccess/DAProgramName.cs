using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Data;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.DataAccess
{
    public class DAProgramName
    {
        public static int Insert(MProgramName ent)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("sp_ProgramName_Ins", con);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IProjectCode", SqlDbType.VarChar).Value = ent.ProjectCode;
                cmd.Parameters.Add("@IProgramName", SqlDbType.VarChar).Value = ent.ProgramName;
                cmd.Parameters.Add("@IDonorCode", SqlDbType.VarChar).Value = ent.DonorCode;
                cmd.Parameters.Add("@IProjectDetails", SqlDbType.VarChar).Value = ent.ProjectDetails;
                cmd.Parameters.Add("@ISector", SqlDbType.VarChar).Value = ent.Sector;
                cmd.Parameters.Add("@ITaskManager", SqlDbType.VarChar).Value = ent.TaskManager;
                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }

            return 0;
        }

        public static List<MProgramName> List()
        {
            List<MProgramName> lisQuery = new List<MProgramName>();

            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ProgramName_Lis", con);
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MProgramName entRow = new MProgramName();
                            entRow.ProgramNameId = Convert.ToInt32(reader["ProgramNameId"]);
                            entRow.ProjectCode = Convert.ToString(reader["ProjectCode"]);
                            entRow.ProgramName = Convert.ToString(reader["ProgramName"]);
                            entRow.DonorCode = Convert.ToString(reader["DonorCode"]);
                            lisQuery.Add(entRow);
                        }
                    }

                    con.Close();
                }
                catch (Exception ex)
                {
                    lisQuery = null;
                }
            }

            return lisQuery;
        }
    }
}
