using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Data;
using UNCDF.Layers.Model;


namespace UNCDF.Layers.DataAccess
{
    public class DABanner
    {
        public static List<MBanner> RandomLis(BaseRequest baseRequest, ref int Val)
        {
            List<MBanner> lisQuery = new List<MBanner>();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_Banner_RandomLis", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@ILanguage", SqlDbType.VarChar).Value = baseRequest.Language;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    Val = 0;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MBanner entRow = new MBanner();
                            entRow.BannerId = Convert.ToInt32(reader["BannerId"]);
                            entRow.Title = Convert.ToString(reader["Title"]);
                            entRow.SubTile = Convert.ToString(reader["SubTile"]);
                            entRow.Image = Constant.S3Server + Convert.ToString(reader["Image"]);
                            lisQuery.Add(entRow);
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

        public static int Insert(MBanner ent, ref int Val)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;

                    SqlCommand cmd = new SqlCommand("sp_Banner_Ins", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@ITitle", SqlDbType.VarChar).Value = ent.Title;
                    cmd.Parameters.Add("@ISubTile", SqlDbType.VarChar).Value = ent.SubTile;
                    cmd.Parameters.Add("@IImage", SqlDbType.VarChar).Value = ent.Image;
                    cmd.Parameters.Add("@IStatus", SqlDbType.Int).Value = ent.Status;
                    cmd.Parameters.Add("@OBannerId", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    ent.BannerId = Convert.ToInt32(cmd.Parameters["@OBannerId"].Value);
                    con.Close();

                    Val = 0;

                    return ent.BannerId;
                }
                catch (Exception ex)
                {
                    Val = 2;

                    return 0;

                }
            }

        }



        public static int Update(MBanner ent, ref int Val)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;

                    SqlCommand cmd = new SqlCommand("sp_Banner_Upd", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IBannerId", SqlDbType.Int).Value = ent.BannerId;
                    cmd.Parameters.Add("@ITitle", SqlDbType.VarChar).Value = ent.Title;
                    cmd.Parameters.Add("@ISubTile", SqlDbType.VarChar).Value = ent.SubTile;
                    cmd.Parameters.Add("@IImage", SqlDbType.VarChar).Value = ent.Image;
                    cmd.Parameters.Add("@IStatus", SqlDbType.Int).Value = ent.Status;

                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Val = 0;

                    return ent.BannerId;
                }

                catch (Exception ex)
                {
                    Val = 2;

                    return 0;

                }
            }

        }

        public static MBanner Select(MBanner ent, ref int Val)
        {
            MBanner result = new MBanner();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_Banner_Sel", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IBannerId", SqlDbType.Int).Value = ent.BannerId;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    Val = 1;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.BannerId = Convert.ToInt32(reader["BannerId"]);
                            result.Title = Convert.ToString(reader["Title"]);
                            result.SubTile = Convert.ToString(reader["SubTile"]);
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

        public static List<MBanner> List(MBanner ent, ref int Val)
        {
            List<MBanner> lisQuery = new List<MBanner>();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_Banner_Lis", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@ITitle", SqlDbType.VarChar).Value = ent.Title;
                    cmd.Parameters.Add("@IStatus", SqlDbType.Int).Value = ent.Status;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    Val = 1;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MBanner entRow = new MBanner();
                            entRow.BannerId = Convert.ToInt32(reader["BannerId"]);
                            entRow.Title = Convert.ToString(reader["Title"]);
                            entRow.SubTile = Convert.ToString(reader["SubTile"]);
                            entRow.Image = Convert.ToString(reader["Image"]);
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


        public static int Delete(MBanner ent, ref int Val)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;

                    SqlCommand cmd = new SqlCommand("sp_Banner_Del", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IBannerId", SqlDbType.Int).Value = ent.BannerId;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    Val = 0;

                    return ent.BannerId;
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
