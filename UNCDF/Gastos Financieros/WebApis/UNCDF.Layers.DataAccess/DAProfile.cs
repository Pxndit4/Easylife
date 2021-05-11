using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Data;
using UNCDF.Layers.Models;

namespace UNCDF.Layers.DataAccess
{
    public class DAProfile
    {
        public static List<MProfile> LisByUser(MProfile ent, ref int Val)
        {
            List<MProfile> lisQuery = new List<MProfile>();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_Profile_Lis_ByUser", con);
                    cmd.Parameters.Add("@IUserId", SqlDbType.VarChar).Value = ent.UserId;
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    Val = 1;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MProfile entRow = new MProfile();
                            entRow.ProfileId = Convert.ToInt32(reader["ProfileId"]);
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

        public static List<MProfile> Lis(MProfile ent, ref int Val)
        {
            List<MProfile> lisQuery = new List<MProfile>();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_Profile_Lis", con);
                    cmd.Parameters.Add("@IDescription", SqlDbType.VarChar).Value = ent.Description;
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    Val = 1;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MProfile entRow = new MProfile();
                            entRow.ProfileId = Convert.ToInt32(reader["ProfileId"]);
                            entRow.Description = Convert.ToString(reader["Description"]);
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

        public static int Insert(MProfile ent, ref int ProfileId)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_Profile_ins", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IDescription", SqlDbType.VarChar).Value = ent.Description;
                    cmd.Parameters.Add("@OProfileId", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    ProfileId = Convert.ToInt32(cmd.Parameters["@OProfileId"].Value);
                    con.Close();

                    return 0;
                }

                catch (Exception ex)
                {
                    return 2;

                }
            }
        }

        public static int InsertOptions(MProfileOptions ent)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ProfileOptions_Ins", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IProfileId", SqlDbType.VarChar).Value = ent.ProfileId;
                    cmd.Parameters.Add("@IOptionId", SqlDbType.VarChar).Value = ent.OptionId;

                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    return 0;
                }

                catch (Exception ex)
                {
                    return 2;

                }
            }
        }

        public static int Update(MProfile ent)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_Profile_Upd", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IDescription", SqlDbType.VarChar).Value = ent.Description;
                    cmd.Parameters.Add("@IProfileId", SqlDbType.Int).Value = ent.ProfileId;
                    cmd.Parameters.Add("@IStatus", SqlDbType.Int).Value = ent.Status;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    return 0;
                }

                catch (Exception ex)
                {
                    return 2;

                }
            }
        }

        public static int DeleteOptions(MProfile ent)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ProfileOptions_Del", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IProfileId", SqlDbType.VarChar).Value = ent.ProfileId;

                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    return 0;
                }

                catch (Exception ex)
                {
                    return 2;

                }
            }
        }

        public static MProfile Sel(MProfile ent, ref int Val)
        {
            MProfile entRow = new MProfile();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_Profile_Sel", con);
                    cmd.Parameters.Add("@IProfileId", SqlDbType.VarChar).Value = ent.ProfileId;
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    Val = 1;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            entRow.ProfileId = ent.ProfileId;
                            entRow.Description = Convert.ToString(reader["Description"]);
                            entRow.Status = Convert.ToInt32(reader["Status"]);

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
            return entRow;
        }

        public static List<MProfileOptions> SelOptions(MProfile ent, ref int Val)
        {
            List<MProfileOptions> lisQuery = new List<MProfileOptions>();

            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ProfileOptions_Sel", con);
                    cmd.Parameters.Add("@IProfileId", SqlDbType.VarChar).Value = ent.ProfileId;
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    Val = 1;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MProfileOptions entRow = new MProfileOptions();
                            entRow.ProfileId = Convert.ToInt32(reader["ProfileId"]);
                            entRow.OptionId = Convert.ToInt32(reader["OptionId"]);
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

        public static int Delete(MProfile ent, ref int Val)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;

                    SqlCommand cmd = new SqlCommand("sp_Profile_Del", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IProfileId", SqlDbType.Int).Value = ent.ProfileId;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    Val = 0;

                    return ent.UserId;
                }

                catch (Exception ex)
                {
                    Val = 2;

                    return 0;
                }
            }
        }

        public static List<MProfileUser> LisUsers(MProfile ent, ref int Val)
        {
            List<MProfileUser> lisQuery = new List<MProfileUser>();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ProfileUser_Sel", con);
                    cmd.Parameters.Add("@IProfileId", SqlDbType.VarChar).Value = ent.ProfileId;

                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    Val = 1;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MProfileUser entRow = new MProfileUser();
                            entRow.ProfileId = Convert.ToInt32(reader["ProfileId"]);
                            entRow.UserId = Convert.ToInt32(reader["UserId"]);
                            entRow.User = Convert.ToString(reader["User"]);
                            entRow.Name = Convert.ToString(reader["Name"]);
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

        public static List<MProfileUser> LisUsersUnAssigned(MProfileUser ent, ref int Val)
        {
            List<MProfileUser> lisQuery = new List<MProfileUser>();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ProfileUser_LisUnAsiggned", con);
                    cmd.Parameters.Add("@IProfileId", SqlDbType.VarChar).Value = ent.ProfileId;
                    cmd.Parameters.Add("@Iuser", SqlDbType.VarChar).Value = ent.User;
                    cmd.Parameters.Add("@IName", SqlDbType.VarChar).Value = ent.Name;
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    Val = 1;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MProfileUser entRow = new MProfileUser();
                            entRow.ProfileId = Convert.ToInt32(reader["ProfileId"]);
                            entRow.UserId = Convert.ToInt32(reader["UserId"]);
                            entRow.User = Convert.ToString(reader["User"]);
                            entRow.Name = Convert.ToString(reader["Name"]);
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

        public static int InsertUser(MProfileUser ent)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ProfileUser_Ins", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IProfileId", SqlDbType.VarChar).Value = ent.ProfileId;
                    cmd.Parameters.Add("@IUserId", SqlDbType.VarChar).Value = ent.UserId;

                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    return 0;
                }

                catch (Exception ex)
                {
                    return 2;

                }
            }
        }

        public static int DeleteUser(MProfileUser ent)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ProfileUser_Del", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IProfileId", SqlDbType.VarChar).Value = ent.ProfileId;
                    cmd.Parameters.Add("@IUserId", SqlDbType.VarChar).Value = ent.UserId;

                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    return 0;
                }

                catch (Exception ex)
                {
                    return 2;

                }
            }
        }
    }
}
