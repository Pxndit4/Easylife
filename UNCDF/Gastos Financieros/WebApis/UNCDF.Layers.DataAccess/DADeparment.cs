using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Data;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.DataAccess
{
    public class DADeparment
    {
        public static int Insert(MDeparment ent)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("sp_Deparment_Ins", con);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IDeparmentCode", SqlDbType.VarChar).Value = ent.DeparmentCode;
                cmd.Parameters.Add("@IDescription", SqlDbType.VarChar).Value = ent.Description;
                cmd.Parameters.Add("@IPracticeArea", SqlDbType.VarChar).Value = ent.PracticeArea;
                cmd.Parameters.Add("@IRegion", SqlDbType.VarChar).Value = ent.Region;

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }

            return 0;
        }

        public static List<MDeparment> List()
        {
            List<MDeparment> lisQuery = new List<MDeparment>();

            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_Deparment_Lis", con);
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MDeparment entRow = new MDeparment();
                            entRow.DeparmentId = Convert.ToInt32(reader["DeparmentId"]);
                            entRow.DeparmentCode = Convert.ToString(reader["DeparmentCode"]);
                            entRow.Description = Convert.ToString(reader["Description"]);
                            entRow.PracticeArea = Convert.ToString(reader["PracticeArea"]);
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
