using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Data;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.DataAccess
{
    public class DAContinent
    {
        public static List<MContinent> List(MContinent ent, ref int Val)
        {
            List<MContinent> lisQuery = new List<MContinent>();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_Continent_Lis", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IDescription", SqlDbType.VarChar).Value = ent.Description;
                    cmd.Parameters.Add("@IStatus", SqlDbType.Int).Value = ent.Status;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    Val = 1;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MContinent entRow = new MContinent();
                            entRow.ContinentId = Convert.ToInt32(reader["ContinentId"]);
                            entRow.Description = Convert.ToString(reader["Description"]);
                            entRow.Status = Convert.ToInt32(reader["Status"]);
                            lisQuery.Add(entRow);

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
            return lisQuery;
        }
    }
}
