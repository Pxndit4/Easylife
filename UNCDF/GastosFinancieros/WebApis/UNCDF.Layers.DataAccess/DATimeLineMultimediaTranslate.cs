using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Data;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.DataAccess
{
   public class DATimeLineMultimediaTranslate
    {
        public static int Insert(MTimeLineMultimediaTranslate ent, ref int Val)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;

                    SqlCommand cmd = new SqlCommand("sp_TimeLineMultimediaTranslate_Ins", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@ITimeLineMulId", SqlDbType.Int).Value = ent.TimeLineMulId;
                    cmd.Parameters.Add("@ILanguageId", SqlDbType.Int).Value = ent.LanguageId;
                    cmd.Parameters.Add("@ITitle", SqlDbType.VarChar).Value = ent.Title;

                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    Val = 0;

                    return ent.TimeLineMulId;
                }
                catch (Exception ex)
                {
                    Val = 2;

                    return 0;

                }
            }

        }

        public static int Update(MTimeLineMultimediaTranslate ent, ref int Val)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;

                    SqlCommand cmd = new SqlCommand("sp_TimeLineMultimediaTranslate_Upd", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@ITimeLineMulId", SqlDbType.Int).Value = ent.TimeLineMulId;
                    cmd.Parameters.Add("@ILanguageId", SqlDbType.Int).Value = ent.LanguageId;
                    cmd.Parameters.Add("@ITitle", SqlDbType.VarChar).Value = ent.Title;

                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Val = 0;

                    return ent.TimeLineMulId;
                }

                catch (Exception ex)
                {
                    Val = 2;

                    return 0;

                }
            }

        }

        public static List<MTimeLineMultimediaTranslate> Lis(MTimeLineMultimediaTranslate ent, ref int Val)
        {
            List<MTimeLineMultimediaTranslate> lisQuery = new List<MTimeLineMultimediaTranslate>();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_TimeLineMultimediaTranslate_Lis", con);
                    cmd.Parameters.Add("@ITimeLineMulId", SqlDbType.Int).Value = ent.TimeLineMulId;
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    Val = 1;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MTimeLineMultimediaTranslate entRow = new MTimeLineMultimediaTranslate();
                            entRow.TimeLineMulId = Convert.ToInt32(reader["TimeLineMulId"]);
                            entRow.LanguageId = Convert.ToInt32(reader["LanguageId"]);
                            entRow.Language = Convert.ToString(reader["Language"]);
                            entRow.Title = Convert.ToString(reader["Title"]);
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



        public static MTimeLineMultimediaTranslate Select(MTimeLineMultimediaTranslate ent, ref int Val)
        {
            MTimeLineMultimediaTranslate result = new MTimeLineMultimediaTranslate();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_TimeLineMultimediaTranslate_Sel", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@ITimeLineMulId", SqlDbType.Int).Value = ent.TimeLineMulId;
                    cmd.Parameters.Add("@ILanguageId", SqlDbType.Int).Value = ent.LanguageId;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    Val = 1;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.TimeLineMulId = Convert.ToInt32(reader["TimeLineMulId"]);
                            result.LanguageId = Convert.ToInt32(reader["LanguageId"]);
                            result.Language = Convert.ToString(reader["Language"]);
                            result.Title = Convert.ToString(reader["Title"]);
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

        public static int Delete(MTimeLineMultimediaTranslate ent, ref int Val)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;

                    SqlCommand cmd = new SqlCommand("sp_TimeLineMultimediaTranslate_Del", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@ITimeLineMulId", SqlDbType.Int).Value = ent.TimeLineMulId;
                    cmd.Parameters.Add("@ILanguageId", SqlDbType.Int).Value = ent.LanguageId;
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Val = 0;

                    return ent.TimeLineMulId;
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


