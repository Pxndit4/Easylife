using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Data;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.DataAccess
{
    public class DASubscribers
    {
        public static int Insert(MSubscribers ent, ref int Val)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;

                    SqlCommand cmd = new SqlCommand("sp_Subscribers_Ins", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IEmail", SqlDbType.VarChar).Value = ent.Email;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    Val = 0;

                    return 1;
                }

                catch (Exception ex)
                {
                    Val = 2;

                    return 0;

                }
            }
        }

        public static List<MSubscribers> List(MSubscribers ent, ref int Val)
        {
            List<MSubscribers> lisQuery = new List<MSubscribers>();

            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_Subscribers_List", con);
                    cmd.Parameters.Add("@IEmail", SqlDbType.VarChar).Value = ent.Email;
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    Val = 1;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MSubscribers entRow = new MSubscribers();
                            entRow.Id = Convert.ToInt32(reader["Id"]);
                            entRow.Email = Convert.ToString(reader["Email"]);
                            lisQuery.Add(entRow);

                            Val = 0;
                        }
                    }
                    con.Close();
                }

                catch (Exception)
                {
                    Val = 2;

                }
            }

            return lisQuery;
        }

    }
}
