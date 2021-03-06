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

        public static int Update(MDeparment ent)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("sp_Deparment_Upd", con);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IDeparmentId", SqlDbType.VarChar).Value = ent.DeparmentId;
                cmd.Parameters.Add("@ILongitude", SqlDbType.VarChar).Value = ent.Longitude;
                cmd.Parameters.Add("@ILatitude", SqlDbType.VarChar).Value = ent.Latitude;
                cmd.Parameters.Add("@ICountryId", SqlDbType.VarChar).Value = ent.CountryId;

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
                            entRow.Region = Convert.ToString(reader["Region"]);
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

        public static MDeparment Get(MDeparment ent)
        {
            MDeparment lisQuery = new MDeparment();

            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_Deparment_Get", con);
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IDeparmentId", SqlDbType.VarChar).Value = ent.DeparmentId;
                    con.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            lisQuery.DeparmentId = Convert.ToInt32(reader["DeparmentId"]);
                            lisQuery.DeparmentCode = Convert.ToString(reader["DeparmentCode"]);
                            lisQuery.Description = Convert.ToString(reader["Description"]);
                            lisQuery.PracticeArea = Convert.ToString(reader["PracticeArea"]);
                            lisQuery.Region = Convert.ToString(reader["Region"]);
                            lisQuery.Latitude = Convert.ToString(reader["Latitude"]);
                            lisQuery.Longitude = Convert.ToString(reader["Longitude"]);
                            lisQuery.CountryId = Convert.ToInt32(reader["CountryId"]);
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
