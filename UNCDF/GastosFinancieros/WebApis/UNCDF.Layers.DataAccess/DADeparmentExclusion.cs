using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Data;
using UNCDF.Layers.Model;


namespace UNCDF.Layers.DataAccess
{
    public class DADeparmentExclusion
    {
        public static int Insert(MDeparmentExclusion ent)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("sp_DeparmentExclusion_Ins", con);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IDeparmentCode", SqlDbType.VarChar).Value = ent.DeparmentCode;

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }

            return 0;
        }
        public static int Delete(MDeparmentExclusion ent, ref int Val)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;

                    SqlCommand cmd = new SqlCommand("sp_DeparmentExclusion_Del", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IDeparmentCode", SqlDbType.VarChar).Value = ent.DeparmentCode;
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
        public static List<MDeparmentExclusion> ListDeparmentCodeExcluded( )
        {
            List<MDeparmentExclusion> lisQuery = new List<MDeparmentExclusion>();

            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_DeparmentExclusions_Lis", con);
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MDeparmentExclusion entRow = new MDeparmentExclusion();
                            entRow.DeparmentCode = Convert.ToString(reader["DeparmentCode"]);
                            lisQuery.Add(entRow);
                        }
                    }
                    con.Close();
                }

                catch (Exception ex)
                {
                    lisQuery = new List<MDeparmentExclusion>();
                }
            }
            return lisQuery;
        }


        public static List<MDeparment> FilDeparmentCodeExcluded(MDeparment ent, ref int Val)
        {
            List<MDeparment> lisQuery = new List<MDeparment>();

            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_DeparmentExclusion_Fil", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IDeparmentCode", SqlDbType.VarChar).Value = ent.DeparmentCode;
                    cmd.Parameters.Add("@IDescription", SqlDbType.VarChar).Value = ent.Description;
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
                    lisQuery = new List<MDeparment>();
                }
            }
            return lisQuery;
        }

    }
}
