using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Data;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.DataAccess
{
    public class DATimeLineTestimonial
    {
        public static List<MTimeLineTestimonial> List(MTimeLineTestimonial ent, BaseRequest baseRequest, ref int Val)
        {
            List<MTimeLineTestimonial> lisQuery = new List<MTimeLineTestimonial>();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_TimeLineTestimonial_Lis", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@ITimeLineId", SqlDbType.Int).Value = ent.TimeLineId;
                    cmd.Parameters.Add("@IName", SqlDbType.VarChar).Value = ent.Name;
                    cmd.Parameters.Add("@ILanguage", SqlDbType.VarChar).Value = baseRequest.Language;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MTimeLineTestimonial entRow = new MTimeLineTestimonial();
                            entRow.TimeLineTestId = Convert.ToInt32(reader["TimeLineTestId"]);
                            entRow.TimeLineId = Convert.ToInt32(reader["TimeLineId"]);
                            entRow.Name = Convert.ToString(reader["Name"]);
                            entRow.Testimonial = Convert.ToString(reader["Testimonial"]);
                            entRow.Photo = (Convert.ToString(reader["Photo"]).Equals("")) ? "" : Constant.S3Server + Convert.ToString(reader["Photo"]);

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

        public static List<MTimeLineTestimonial> RandomLis(BaseRequest baseRequest, ref int Val)
        {
            List<MTimeLineTestimonial> lisQuery = new List<MTimeLineTestimonial>();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_TimeLineTestimonial_RandomLis", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@ILanguage", SqlDbType.VarChar).Value = baseRequest.Language;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MTimeLineTestimonial entRow = new MTimeLineTestimonial();
                            entRow.Name = Convert.ToString(reader["Name"]);
                            entRow.Testimonial = Convert.ToString(reader["Testimonial"]);
                            entRow.Photo = Constant.S3Server + Convert.ToString(reader["Photo"]);

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
        public static List<MTimeLineTestimonial> Filter(MTimeLineTestimonial ent, ref int Val)
        {
            List<MTimeLineTestimonial> lisQuery = new List<MTimeLineTestimonial>();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_TimeLineTestimonial_Fil", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@ITimeLineid", SqlDbType.Int).Value = ent.TimeLineId;
                    cmd.Parameters.Add("@IName", SqlDbType.VarChar).Value = ent.Name;

                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MTimeLineTestimonial entRow = new MTimeLineTestimonial();
                            entRow.TimeLineTestId = Convert.ToInt32(reader["TimeLineTestId"]);
                            entRow.TimeLineId = Convert.ToInt32(reader["TimeLineId"]);
                            entRow.Name = Convert.ToString(reader["Name"]);
                            entRow.Testimonial = Convert.ToString(reader["Testimonial"]);
                            //entRow.Photo = Convert.ToString(reader["Photo"]);
                            entRow.Photo = (Convert.ToString(reader["Photo"]).Equals("")) ? "" : Constant.S3Server + Convert.ToString(reader["Photo"]);
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

        public static int Insert(MTimeLineTestimonial ent, ref int Val)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;

                    SqlCommand cmd = new SqlCommand("sp_TimeLineTestimonial_Ins", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@ITimeLineId", SqlDbType.Int).Value = ent.TimeLineId;
                    cmd.Parameters.Add("@IName", SqlDbType.VarChar).Value = ent.Name;
                    cmd.Parameters.Add("@ITestimonial", SqlDbType.VarChar).Value = ent.Testimonial;
                    cmd.Parameters.Add("@IPhoto", SqlDbType.VarChar).Value = ent.Photo;
                    cmd.Parameters.Add("@OTimeLineTestId", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    ent.TimeLineTestId = Convert.ToInt32(cmd.Parameters["@OTimeLineTestId"].Value);
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



        public static int Update(MTimeLineTestimonial ent, ref int Val)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;

                    SqlCommand cmd = new SqlCommand("sp_TimeLineTestimonial_Upd", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@ITimeLineTestId", SqlDbType.Int).Value = ent.TimeLineTestId;
                    cmd.Parameters.Add("@ITimeLineId", SqlDbType.Int).Value = ent.TimeLineId;
                    cmd.Parameters.Add("@IName", SqlDbType.VarChar).Value = ent.Name;
                    cmd.Parameters.Add("@ITestimonial", SqlDbType.VarChar).Value = ent.Testimonial;
                    cmd.Parameters.Add("@IPhoto", SqlDbType.VarChar).Value = ent.Photo;

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

        public static MTimeLineTestimonial Select(MTimeLineTestimonial ent, ref int Val)
        {
            MTimeLineTestimonial result = new MTimeLineTestimonial();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_TimeLineTestimonial_Sel", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@ITimeLineTestId", SqlDbType.Int).Value = ent.TimeLineTestId;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    Val = 1;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.TimeLineTestId = Convert.ToInt32(reader["TimeLineTestId"]);
                            result.TimeLineId = Convert.ToInt32(reader["TimeLineId"]);
                            result.Name = Convert.ToString(reader["Name"]);
                            result.Testimonial = Convert.ToString(reader["Testimonial"]);
                            result.Photo = Convert.ToString(reader["Photo"]);
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




        public static int Delete(MTimeLineTestimonial ent, ref int Val)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;

                    SqlCommand cmd = new SqlCommand("sp_TimeLineTestimonial_Del", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@ITimeLineTestId", SqlDbType.Int).Value = ent.TimeLineTestId;
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


