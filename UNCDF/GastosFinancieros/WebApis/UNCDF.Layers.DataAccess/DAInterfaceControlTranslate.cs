using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Data;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.DataAccess
{
    public class DAInterfaceControlTranslate
    {
        public static int Insert(MInterfaceControlTranslate ent, ref int Val)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;

                    SqlCommand cmd = new SqlCommand("sp_InterfaceControlTranslate_Ins", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IInterfaceControlId", SqlDbType.Int).Value = ent.InterfaceControlId;
                    cmd.Parameters.Add("@ILanguageId", SqlDbType.Int).Value = ent.LanguageId;
                    cmd.Parameters.Add("@IDescription", SqlDbType.VarChar).Value = ent.Description;

                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    Val = 0;

                    return ent.InterfaceControlId;
                }
                catch (Exception ex)
                {
                    Val = 2;

                    return 0;

                }
            }

        }

        public static int Update(MInterfaceControlTranslate ent, ref int Val)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;

                    SqlCommand cmd = new SqlCommand("sp_InterfaceControlTranslate_Upd", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IInterfaceControlId", SqlDbType.Int).Value = ent.InterfaceControlId;
                    cmd.Parameters.Add("@ILanguageId", SqlDbType.Int).Value = ent.LanguageId;
                    cmd.Parameters.Add("@IDescription", SqlDbType.VarChar).Value = ent.Description;

                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Val = 0;

                    return ent.InterfaceControlId;
                }

                catch (Exception ex)
                {
                    Val = 2;

                    return 0;

                }
            }

        }

        public static List<MInterfaceControlTranslate> Lis(MInterfaceControlTranslate ent, ref int Val)
        {
            List<MInterfaceControlTranslate> lisQuery = new List<MInterfaceControlTranslate>();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_InterfaceControlTranslate_Lis", con);
                    cmd.Parameters.Add("@IInterfaceControlId", SqlDbType.Int).Value = ent.InterfaceControlId;
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    Val = 1;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MInterfaceControlTranslate entRow = new MInterfaceControlTranslate();
                            entRow.InterfaceControlId = Convert.ToInt32(reader["InterfaceControlId"]);
                            entRow.Description = Convert.ToString(reader["Description"]);
                            entRow.LanguageId = Convert.ToString(reader["LanguageId"]);
                            entRow.LanguageName = Convert.ToString(reader["LanguageName"]);
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



        public static MInterfaceControlTranslate Select(MInterfaceControlTranslate ent, ref int Val)
        {
            MInterfaceControlTranslate result = new MInterfaceControlTranslate();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_InterfaceControlTranslate_Sel", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IInterfaceControlId", SqlDbType.Int).Value = ent.InterfaceControlId;
                    cmd.Parameters.Add("@ILanguageId", SqlDbType.Int).Value = ent.LanguageId;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    Val = 1;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.InterfaceControlId = Convert.ToInt32(reader["InterfaceControlId"]);
                            result.LanguageId = Convert.ToString(reader["LanguageId"]);
                            result.LanguageName = Convert.ToString(reader["LanguageName"]);
                            result.Description = Convert.ToString(reader["Description"]);
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

        public static int Delete(MInterfaceControlTranslate ent, ref int Val)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;

                    SqlCommand cmd = new SqlCommand("sp_InterfaceControlTranslate_Del", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IInterfaceControlId", SqlDbType.Int).Value = ent.InterfaceControlId;
                    cmd.Parameters.Add("@ILanguageId", SqlDbType.Int).Value = ent.LanguageId;
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Val = 0;

                    //return Convert.ToInt32(ent.LanguageId);
                    return ent.InterfaceControlId;
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
