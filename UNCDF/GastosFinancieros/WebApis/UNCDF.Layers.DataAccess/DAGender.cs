using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.DataAccess
{
    public class DAGender
    {
        public static List<MGender> Lis(MGender ent, ref int Val, string Language)
        {
            List<MGender> lisQuery = new List<MGender>();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_Gender_Lis", con);
                    cmd.Parameters.Add("@IDescription", SqlDbType.VarChar).Value = ent.Description;
                    cmd.Parameters.Add("@ILanguage", SqlDbType.VarChar).Value = Language;
                    cmd.Parameters.Add("@IStatus", SqlDbType.Int).Value = ent.Status;
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MGender entRow = new MGender();
                            entRow.GenderId = Convert.ToInt32(reader["GenderId"]);
                            entRow.Description = Convert.ToString(reader["Description"]);
                            entRow.Value = Convert.ToString(reader["Value"]);
                            entRow.Status = Convert.ToInt32(reader["Status"]);
                            lisQuery.Add(entRow);

                            Val = 0;
                        }
                    }
                    con.Close();
                }

                catch (SqlException ex)
                {
                    Val = 2;
                }
            }
            return lisQuery;
        }


        public static int Insert(MGender ent, ref int Val)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;

                    SqlCommand cmd = new SqlCommand("sp_Gender_Ins", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IDescription", SqlDbType.VarChar).Value = ent.Description;
                    cmd.Parameters.Add("@IValue", SqlDbType.VarChar).Value = ent.Value;
                    cmd.Parameters.Add("@IStatus", SqlDbType.VarChar).Value = ent.Status;
                    cmd.Parameters.Add("@OGenderId", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    ent.GenderId = Convert.ToInt32(cmd.Parameters["@OGenderId"].Value);
                    con.Close();

                    Val = 0;

                    return ent.GenderId;
                }
                catch (Exception ex)
                {
                    Val = 2;

                    return 0;

                }
            }

        }



        public static int Update(MGender ent, ref int Val)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;

                    SqlCommand cmd = new SqlCommand("sp_Gender_Upd", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IGenderId", SqlDbType.Int).Value = ent.GenderId;
                    cmd.Parameters.Add("@IDescription", SqlDbType.VarChar).Value = ent.Description;
                    cmd.Parameters.Add("@IValue", SqlDbType.VarChar).Value = ent.Value;
                    cmd.Parameters.Add("@IStatus", SqlDbType.Int).Value = ent.Status;

                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Val = 0;

                    return ent.GenderId;
                }

                catch (Exception ex)
                {
                    Val = 2;

                    return 0;

                }
            }

        }

        public static MGender Select(MGender ent, ref int Val)
        {
            MGender result = new MGender();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_Gender_Sel", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IGenderId", SqlDbType.Int).Value = ent.GenderId;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    Val = 1;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.GenderId = Convert.ToInt32(reader["GenderId"]);
                            result.Description = Convert.ToString(reader["Description"]);
                            result.Value = Convert.ToString(reader["Value"]);
                            result.Status = Convert.ToInt32(reader["Status"]); ;
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
            return result;
        }




        public static int Delete(MGender ent, ref int Val)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;

                    SqlCommand cmd = new SqlCommand("sp_Gender_Del", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IGenderId", SqlDbType.Int).Value = ent.GenderId;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    Val = 0;

                    return ent.GenderId;
                }

                catch (Exception ex)
                {
                    Val = 2;

                    return 0;

                }
            }

        }

    }
}
