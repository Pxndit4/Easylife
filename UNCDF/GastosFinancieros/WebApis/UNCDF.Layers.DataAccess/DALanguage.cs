using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using UNCDF.Layers.Model;
using System.Data;

namespace UNCDF.Layers.DataAccess
{
    public class DALanguage
    {
        public static List<MLanguage> Lis(ref int Val)
        {
            List<MLanguage> lisQuery = new List<MLanguage>();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_Language_Lis", con);
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MLanguage entRow = new MLanguage();
                            entRow.LanguageId = Convert.ToInt32(reader["LanguageId"]);
                            entRow.Description = Convert.ToString(reader["Description"]);
                            entRow.Flag = Constant.S3Server + Convert.ToString(reader["Flag"]);
                            entRow.Code = Convert.ToString(reader["Code"]);
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

        public static int Insert(MLanguage ent, ref int Val)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;

                    SqlCommand cmd = new SqlCommand("sp_Language_Ins", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IDescription", SqlDbType.VarChar).Value = ent.Description;
                    cmd.Parameters.Add("@IFlag", SqlDbType.VarChar).Value = ent.Flag;
                    cmd.Parameters.Add("@ICode", SqlDbType.VarChar).Value = ent.Code;
                    cmd.Parameters.Add("@IStatus", SqlDbType.Int).Value = ent.Status;
                    cmd.Parameters.Add("@OLanguageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    ent.LanguageId = Convert.ToInt32(cmd.Parameters["@OLanguageId"].Value);
                    con.Close();

                    Val = 0;

                    return ent.LanguageId;
                }
                catch (Exception ex)
                {
                    Val = 2;

                    return 0;

                }
            }

        }



        public static int Update(MLanguage ent, ref int Val)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;

                    SqlCommand cmd = new SqlCommand("sp_Language_Upd", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@ILanguageId", SqlDbType.Int).Value = ent.LanguageId;
                    cmd.Parameters.Add("@IDescription", SqlDbType.VarChar).Value = ent.Description;
                    cmd.Parameters.Add("@IFlag", SqlDbType.VarChar).Value = ent.Flag;
                    cmd.Parameters.Add("@ICode", SqlDbType.VarChar).Value = ent.Code;
                    cmd.Parameters.Add("@IStatus", SqlDbType.Int).Value = ent.Status;

                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Val = 0;

                    return ent.LanguageId;
                }

                catch (Exception ex)
                {
                    Val = 2;

                    return 0;

                }
            }

        }

        public static MLanguage Select(MLanguage ent, ref int Val)
        {
            MLanguage result = new MLanguage();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_Language_Sel", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@ILanguageId", SqlDbType.Int).Value = ent.LanguageId;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    Val = 1;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.LanguageId = Convert.ToInt32(reader["LanguageId"]);
                            result.Description = Convert.ToString(reader["Description"]);
                            result.Flag = Convert.ToString(reader["Flag"]);
                            result.Code = Convert.ToString(reader["Code"]);
                            result.Status = Convert.ToInt32(reader["Status"]);


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

        public static List<MLanguage> Filter(MLanguage ent, ref int Val)
        {
            List<MLanguage> lisQuery = new List<MLanguage>();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_Language_Fil", con);
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
                            MLanguage entRow = new MLanguage();
                            entRow.LanguageId = Convert.ToInt32(reader["LanguageId"]);
                            entRow.Description = Convert.ToString(reader["Description"]);
                            entRow.Flag = Convert.ToString(reader["Flag"]);
                            entRow.Code = Convert.ToString(reader["Code"]);
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


        public static int Delete(MLanguage ent, ref int Val)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;

                    SqlCommand cmd = new SqlCommand("sp_Language_Del", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@ILanguageId", SqlDbType.Int).Value = ent.LanguageId;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    Val = 0;

                    return ent.LanguageId;
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
