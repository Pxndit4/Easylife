using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Data;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.DataAccess
{
   public class DATimeLineTranslate
    {
        public static int Insert(MTimeLineTranslate ent, ref int Val)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;

                    SqlCommand cmd = new SqlCommand("sp_TimeLineTranslate_Ins", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@ITimeLineId", SqlDbType.Int).Value = ent.TimeLineId;
                    cmd.Parameters.Add("@ILanguageId", SqlDbType.Int).Value = ent.LanguageId;
                    cmd.Parameters.Add("@ITitle", SqlDbType.VarChar).Value = ent.Title;
                    cmd.Parameters.Add("@IDescription", SqlDbType.VarChar).Value = ent.Description;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    Val = 0;

                    return ent.TimeLineId;
                }
                catch (Exception ex)
                {
                    Val = 2;

                    return 0;

                }
            }

        }

        public static int Update(MTimeLineTranslate ent, ref int Val)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;

                    SqlCommand cmd = new SqlCommand("sp_TimeLineTranslate_Upd", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@ITimeLineId", SqlDbType.Int).Value = ent.TimeLineId;
                    cmd.Parameters.Add("@ILanguageId", SqlDbType.Int).Value = ent.LanguageId;
                    cmd.Parameters.Add("@ITitle", SqlDbType.VarChar).Value = ent.Title;
                    cmd.Parameters.Add("@IDescription", SqlDbType.VarChar).Value = ent.Description;


                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Val = 0;

                    return ent.TimeLineId;
                }

                catch (Exception ex)
                {
                    Val = 2;

                    return 0;

                }
            }

        }

        public static List<MTimeLineTranslate> Lis(MTimeLineTranslate ent, ref int Val)
        {
            List<MTimeLineTranslate> lisQuery = new List<MTimeLineTranslate>();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_TimeLineTranslate_Lis", con);
                    cmd.Parameters.Add("@ITimeLineId", SqlDbType.Int).Value = ent.TimeLineId;
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    Val = 1;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MTimeLineTranslate entRow = new MTimeLineTranslate();
                            entRow.TimeLineId = Convert.ToInt32(reader["TimeLineId"]);
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



        public static MTimeLineTranslate Select(MTimeLineTranslate ent, ref int Val)
        {
            MTimeLineTranslate result = new MTimeLineTranslate();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_TimeLineTranslate_Sel", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@ITimeLineId", SqlDbType.Int).Value = ent.TimeLineId;
                    cmd.Parameters.Add("@ILanguageId", SqlDbType.Int).Value = ent.LanguageId;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    Val = 1;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.TimeLineId = Convert.ToInt32(reader["TimeLineId"]);
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

        public static int Delete(MTimeLineTranslate ent, ref int Val)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;

                    SqlCommand cmd = new SqlCommand("sp_TimeLineTranslate_Del", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@ITimeLineId", SqlDbType.Int).Value = ent.TimeLineId;
                    cmd.Parameters.Add("@ILanguageId", SqlDbType.Int).Value = ent.LanguageId;
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Val = 0;

                    return ent.TimeLineId;
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
