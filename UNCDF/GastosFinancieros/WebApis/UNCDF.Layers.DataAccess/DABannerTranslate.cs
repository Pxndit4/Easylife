using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Data;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.DataAccess
{
    public class DABannerTranslate
    {
        public static int Insert(MBannerTranslate ent, ref int Val)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;

                    SqlCommand cmd = new SqlCommand("sp_BannerTranslate_Ins", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IBannerId", SqlDbType.Int).Value = ent.BannerId;
                    cmd.Parameters.Add("@ILanguageId", SqlDbType.Int).Value = ent.LanguageId;
                    cmd.Parameters.Add("@ITitle", SqlDbType.VarChar).Value = ent.Title;
                    cmd.Parameters.Add("@ISubTile", SqlDbType.VarChar).Value = ent.SubTitle;
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

        public static int Update(MBannerTranslate ent, ref int Val)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;

                    SqlCommand cmd = new SqlCommand("sp_BannerTranslate_Upd", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IBannerId", SqlDbType.Int).Value = ent.BannerId;
                    cmd.Parameters.Add("@ILanguageId", SqlDbType.Int).Value = ent.LanguageId;
                    cmd.Parameters.Add("@ITitle", SqlDbType.VarChar).Value = ent.Title;
                    cmd.Parameters.Add("@ISubTile", SqlDbType.VarChar).Value = ent.SubTitle;


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

        public static List<MBannerTranslate> Lis(MBannerTranslate ent, ref int Val)
        {
            List<MBannerTranslate> lisQuery = new List<MBannerTranslate>();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_BannerTranslate_Lis", con);
                    cmd.Parameters.Add("@IBannerId", SqlDbType.Int).Value = ent.BannerId;
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    Val = 1;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MBannerTranslate entRow = new MBannerTranslate();
                            entRow.BannerId = Convert.ToInt32(reader["BannerId"]);
                            entRow.LanguageId = Convert.ToInt32(reader["LanguageId"]);
                            entRow.Language = Convert.ToString(reader["Language"]);
                            entRow.Title = Convert.ToString(reader["Title"]);
                            entRow.SubTitle = Convert.ToString(reader["SubTile"]);
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



        public static MBannerTranslate Select(MBannerTranslate ent, ref int Val)
        {
            MBannerTranslate result = new MBannerTranslate();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_BannerTranslate_Sel", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IBannerId", SqlDbType.Int).Value = ent.BannerId;
                    cmd.Parameters.Add("@ILanguageId", SqlDbType.Int).Value = ent.LanguageId;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    Val = 1;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.BannerId = Convert.ToInt32(reader["BannerId"]);
                            result.LanguageId = Convert.ToInt32(reader["LanguageId"]);
                            result.Language = Convert.ToString(reader["Language"]);
                            result.Title = Convert.ToString(reader["Title"]);
                            result.SubTitle = Convert.ToString(reader["SubTile"]);
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

        public static int Delete(MBannerTranslate ent, ref int Val)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;

                    SqlCommand cmd = new SqlCommand("sp_BannerTranslate_Del", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IBannerId", SqlDbType.Int).Value = ent.BannerId;
                    cmd.Parameters.Add("@ILanguageId", SqlDbType.Int).Value = ent.LanguageId;
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
