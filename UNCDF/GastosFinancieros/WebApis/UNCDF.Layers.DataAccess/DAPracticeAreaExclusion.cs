using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Data;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.DataAccess
{
    public class DAPracticeAreaExclusion
    {
        public static int Insert(MPracticeAreaExclusion ent)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("sp_PracticeAreaExclusion_Ins", con);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IPracticeArea", SqlDbType.VarChar).Value = ent.PracticeArea;
                

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }

            return 0;
        }
        public static int Delete(MPracticeAreaExclusion ent, ref int Val)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;

                    SqlCommand cmd = new SqlCommand("sp_PracticeAreaExclusion_Del", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IPracticeArea", SqlDbType.VarChar).Value = ent.PracticeArea;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    Val = 0;

                    return 1;//succes
                }

                catch (Exception ex)
                {
                    Val = 2;

                    return 0;

                }
            }

        }

        public static List<MPracticeAreaExclusion> List(MPracticeAreaExclusion ent, ref int Val)
        {
            List<MPracticeAreaExclusion> lisQuery = new List<MPracticeAreaExclusion>();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("pr_PracticeAreaExclusion_Lis", con);
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    Val = 1;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MPracticeAreaExclusion entRow = new MPracticeAreaExclusion();
                            entRow.PracticeArea = Convert.ToString(reader["PracticeArea"]);
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

        public static List<MPracticeAreaExclusion> ListPracticeAreaCodeExcluded()
        {
            List<MPracticeAreaExclusion> lisQuery = new List<MPracticeAreaExclusion>();

            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_PracticeAreaExclusions_Lis", con);
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MPracticeAreaExclusion entRow = new MPracticeAreaExclusion();
                            entRow.PracticeArea = Convert.ToString(reader["PracticeArea"]);
                            lisQuery.Add(entRow);
                        }
                    }
                    con.Close();
                }

                catch (Exception ex)
                {
                    lisQuery = new List<MPracticeAreaExclusion>();
                }
            }
            return lisQuery;
        }

    }
}
