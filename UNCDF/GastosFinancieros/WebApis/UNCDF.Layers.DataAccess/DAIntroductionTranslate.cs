using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.DataAccess
{
    public class DAIntroductionTranslate
    {
        public static int Insert(MIntroductionTranslate ent, ref int Val)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;

                    SqlCommand cmd = new SqlCommand("sp_IntroductionTranslate_Ins", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IIntroductionId", SqlDbType.Int).Value = ent.IntroductionId;
                    cmd.Parameters.Add("@ILanguageId", SqlDbType.Int).Value = ent.LanguageId;
                    cmd.Parameters.Add("@ITitle", SqlDbType.VarChar).Value = ent.Title;
                    cmd.Parameters.Add("@IDescription", SqlDbType.VarChar).Value = ent.Description;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    Val = 0;

                    return ent.IntroductionId;
                }
                catch (Exception ex)
                {
                    Val = 2;

                    return 0;

                }
            }

        }

        public static int Update(MIntroductionTranslate ent, ref int Val)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;

                    SqlCommand cmd = new SqlCommand("sp_IntroductionTranslate_Upd", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IIntroductionId", SqlDbType.Int).Value = ent.IntroductionId;
                    cmd.Parameters.Add("@ILanguageId", SqlDbType.Int).Value = ent.LanguageId;
                    cmd.Parameters.Add("@ITitle", SqlDbType.VarChar).Value = ent.Title;
                    cmd.Parameters.Add("@IDescription", SqlDbType.VarChar).Value = ent.Description;


                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Val = 0;

                    return ent.IntroductionId;
                }

                catch (Exception ex)
                {
                    Val = 2;

                    return 0;

                }
            }

        }

        public static List<MIntroductionTranslate> Lis(MIntroductionTranslate ent, ref int Val)
        {
            List<MIntroductionTranslate> lisQuery = new List<MIntroductionTranslate>();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_IntroductionTranslate_Lis", con);
                    cmd.Parameters.Add("@IIntroductionId", SqlDbType.Int).Value = ent.IntroductionId;
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    Val = 1;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MIntroductionTranslate entRow = new MIntroductionTranslate();
                            entRow.IntroductionId = Convert.ToInt32(reader["IntroductionId"]);
                            entRow.LanguageId = Convert.ToInt32(reader["LanguageId"]);
                            entRow.Language = Convert.ToString(reader["Language"]);
                            entRow.Title = Convert.ToString(reader["Title"]);
                            entRow.Description = Convert.ToString(reader["Description"]);
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



        public static MIntroductionTranslate Select(MIntroductionTranslate ent, ref int Val)
        {
            MIntroductionTranslate result = new MIntroductionTranslate();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_IntroductionTranslate_Sel", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IIntroductionId", SqlDbType.Int).Value = ent.IntroductionId;
                    cmd.Parameters.Add("@ILanguageId", SqlDbType.Int).Value = ent.LanguageId;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    Val = 1;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.IntroductionId = Convert.ToInt32(reader["IntroductionId"]);
                            result.LanguageId = Convert.ToInt32(reader["LanguageId"]);
                            result.Language = Convert.ToString(reader["Language"]);
                            result.Title = Convert.ToString(reader["Title"]);
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

        public static int Delete(MIntroductionTranslate ent, ref int Val)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;

                    SqlCommand cmd = new SqlCommand("sp_IntroductionTranslate_Del", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IIntroductionId", SqlDbType.Int).Value = ent.IntroductionId;
                    cmd.Parameters.Add("@ILanguageId", SqlDbType.Int).Value = ent.LanguageId;
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Val = 0;

                    return ent.IntroductionId;
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
