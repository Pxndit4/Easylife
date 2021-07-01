using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Data;
using UNCDF.Layers.Model;


namespace UNCDF.Layers.DataAccess
{
    public class DATimeLineTestimonialTranslate
    {
        public static int Insert(MTimeLineTestimonialTranslate ent, ref int Val)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;

                    SqlCommand cmd = new SqlCommand("sp_TimeLineTestimonialTranslate_Ins", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@ITimeLineTestId", SqlDbType.Int).Value = ent.TimeLineTestId;
                    cmd.Parameters.Add("@ILanguageId", SqlDbType.Int).Value = ent.LanguageId;
                    cmd.Parameters.Add("@ITestimonial", SqlDbType.VarChar).Value = ent.Testimonial;

                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    Val = 0;

                    return ent.TimeLineTestId;
                }
                catch (Exception ex)
                {
                    Val = 2;

                    return 0;

                }
            }

        }

        public static int Update(MTimeLineTestimonialTranslate ent, ref int Val)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;

                    SqlCommand cmd = new SqlCommand("sp_TimeLineTestimonialTranslate_Upd", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@ITimeLineTestId", SqlDbType.Int).Value = ent.TimeLineTestId;
                    cmd.Parameters.Add("@ILanguageId", SqlDbType.Int).Value = ent.LanguageId;
                    cmd.Parameters.Add("@ITestimonial", SqlDbType.VarChar).Value = ent.Testimonial;

                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Val = 0;

                    return ent.TimeLineTestId;
                }

                catch (Exception ex)
                {
                    Val = 2;

                    return 0;

                }
            }

        }

        public static List<MTimeLineTestimonialTranslate> Lis(MTimeLineTestimonialTranslate ent, ref int Val)
        {
            List<MTimeLineTestimonialTranslate> lisQuery = new List<MTimeLineTestimonialTranslate>();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_TimeLineTestimonialTranslate_Lis", con);
                    cmd.Parameters.Add("@ITimeLineTestId", SqlDbType.Int).Value = ent.TimeLineTestId;
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    Val = 1;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MTimeLineTestimonialTranslate entRow = new MTimeLineTestimonialTranslate();
                            entRow.TimeLineTestId = Convert.ToInt32(reader["TimeLineTestId"]);
                            entRow.LanguageId = Convert.ToInt32(reader["LanguageId"]);
                            entRow.Language = Convert.ToString(reader["Language"]);
                            entRow.Testimonial = Convert.ToString(reader["Testimonial"]);
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



        public static MTimeLineTestimonialTranslate Select(MTimeLineTestimonialTranslate ent, ref int Val)
        {
            MTimeLineTestimonialTranslate result = new MTimeLineTestimonialTranslate();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_TimeLineTestimonialTranslate_Sel", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@ITimeLineTestId", SqlDbType.Int).Value = ent.TimeLineTestId;
                    cmd.Parameters.Add("@ILanguageId", SqlDbType.Int).Value = ent.LanguageId;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    Val = 1;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.TimeLineTestId = Convert.ToInt32(reader["TimeLineTestId"]);
                            result.LanguageId = Convert.ToInt32(reader["LanguageId"]);
                            result.Language = Convert.ToString(reader["Language"]);
                            result.Testimonial = Convert.ToString(reader["Testimonial"]);
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

        public static int Delete(MTimeLineTestimonialTranslate ent, ref int Val)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;

                    SqlCommand cmd = new SqlCommand("sp_TimeLineTestimonialTranslate_Del", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@ITimeLineTestId", SqlDbType.Int).Value = ent.TimeLineTestId;
                    cmd.Parameters.Add("@ILanguageId", SqlDbType.Int).Value = ent.LanguageId;
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Val = 0;

                    return ent.TimeLineTestId;
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
