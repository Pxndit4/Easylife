using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.DataAccess
{
    public class DAIntroduction
    {
        public static List<MIntroduction> List(MIntroduction ent, BaseRequest baseRequest)
        {
            List<MIntroduction> lisQuery = new List<MIntroduction>();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_Introduction_Lis", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@ITitle", SqlDbType.VarChar).Value = ent.Title;
                    cmd.Parameters.Add("@IDescription", SqlDbType.VarChar).Value = ent.Description;
                    cmd.Parameters.Add("@ILanguage", SqlDbType.VarChar).Value = baseRequest.Language;
                    cmd.Parameters.Add("@IStatus", SqlDbType.Int).Value = ent.Status;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MIntroduction entRow = new MIntroduction();
                            entRow.IntroductionId = Convert.ToInt32(reader["IntroductionId"]);
                            entRow.Title = Convert.ToString(reader["Title"]);
                            entRow.Description = Convert.ToString(reader["Description"]);
                            entRow.Image = Constant.S3Server + Convert.ToString(reader["Image"]);
                            //entRow.Order = Convert.ToInt32(reader["Order"]);
                            entRow.Order = (reader["Order"] == DBNull.Value) ? null : (Nullable<int>)Convert.ToInt32(reader["Order"]);
                            entRow.Status = Convert.ToInt32(reader["Status"]);
                            lisQuery.Add(entRow);
                        }
                    }
                    con.Close();
                }

                catch (SqlException ex)
                {

                }
            }
            return lisQuery;
        }

        public static int Insert(MIntroduction ent, ref int Val)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;

                    SqlCommand cmd = new SqlCommand("sp_Introduction_Ins", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@ITitle", SqlDbType.VarChar).Value = ent.Title;
                    cmd.Parameters.Add("@IDescription", SqlDbType.VarChar).Value = ent.Description;
                    cmd.Parameters.Add("@IImage", SqlDbType.VarChar).Value = ent.Image;
                    cmd.Parameters.Add("@IOrder", SqlDbType.Int).Value = ent.Order;
                    cmd.Parameters.Add("@IStatus", SqlDbType.Int).Value = ent.Status;
                    cmd.Parameters.Add("@OIntroductionId", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    ent.IntroductionId = Convert.ToInt32(cmd.Parameters["@OIntroductionId"].Value);
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



        public static int Update(MIntroduction ent, ref int Val)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;

                    SqlCommand cmd = new SqlCommand("sp_Introduction_Upd", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IIntroductionId", SqlDbType.Int).Value = ent.IntroductionId;
                    cmd.Parameters.Add("@ITitle", SqlDbType.VarChar).Value = ent.Title;
                    cmd.Parameters.Add("@IDescription", SqlDbType.VarChar).Value = ent.Description;
                    cmd.Parameters.Add("@IImage", SqlDbType.VarChar).Value = ent.Image;
                    cmd.Parameters.Add("@IOrder", SqlDbType.Int).Value = ent.Order;
                    cmd.Parameters.Add("@IStatus", SqlDbType.Int).Value = ent.Status;

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

        public static MIntroduction Select(MIntroduction ent, ref int Val)
        {
            MIntroduction result = new MIntroduction();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_Introduction_Sel", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IIntroductionId", SqlDbType.Int).Value = ent.IntroductionId;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    Val = 1;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.IntroductionId = Convert.ToInt32(reader["IntroductionId"]);
                            result.Title = Convert.ToString(reader["Title"]);
                            result.Description = Convert.ToString(reader["Description"]);
                            //result.Order = Convert.ToInt32(reader["Order"]);
                            result.Order = (reader["Order"] == DBNull.Value) ? null : (Nullable<int>)Convert.ToInt32(reader["Order"]);
                            result.Image = Convert.ToString(reader["Image"]);
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


        public static int Delete(MIntroduction ent, ref int Val)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;

                    SqlCommand cmd = new SqlCommand("sp_Introduction_Del", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IIntroductionId", SqlDbType.Int).Value = ent.IntroductionId;
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
