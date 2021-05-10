using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using UNCDF.Layers.Models;

namespace UNCDF.Layers.DataAccess
{
    public class DUser
    {
        public static int Insert(MUser ent, ref int Val)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;

                    SqlCommand cmd = new SqlCommand("sp_User_Ins", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IType", SqlDbType.Int).Value = ent.Type;
                    cmd.Parameters.Add("@IUser", SqlDbType.VarChar).Value = ent.User;
                    cmd.Parameters.Add("@IPassword", SqlDbType.VarChar).Value = ent.Password;
                    cmd.Parameters.Add("@IName", SqlDbType.VarChar).Value = ent.Name;
                    cmd.Parameters.Add("@IStatus", SqlDbType.VarChar).Value = ent.Status;
                    cmd.Parameters.Add("@IToken", SqlDbType.VarChar).Value = ent.Token;
                    cmd.Parameters.Add("@OUserId", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    ent.UserId = Convert.ToInt32(cmd.Parameters["@OUserId"].Value);
                    con.Close();

                    Val = 0;

                    return 1;
                }

                catch (Exception)
                {
                    Val = 2;

                    return 0;

                }
            }
        }

        public static int Update(MUser ent, ref int Val)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;

                    SqlCommand cmd = new SqlCommand("sp_User_Upd", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IUserId", SqlDbType.Int).Value = ent.UserId;
                    cmd.Parameters.Add("@IUser", SqlDbType.VarChar).Value = ent.User;
                    cmd.Parameters.Add("@iStatus", SqlDbType.VarChar).Value = ent.Status;
                    cmd.Parameters.Add("@IName", SqlDbType.VarChar).Value = ent.Name;

                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    Val = 0;

                    return 1;
                }

                catch (Exception)
                {
                    Val = 2;

                    return 0;

                }
            }
        }

        public static List<MUser> Lis(MUser ent, ref int Val)
        {
            List<MUser> lisQuery = new List<MUser>();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_User_List", con);
                    cmd.Parameters.Add("@IUser", SqlDbType.VarChar).Value = ent.User;
                    cmd.Parameters.Add("@IName", SqlDbType.VarChar).Value = ent.Name;
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    Val = 1;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MUser entRow = new MUser();
                            entRow.UserId = Convert.ToInt32(reader["UserId"]);
                            entRow.Type = Convert.ToInt32(reader["Type"]);
                            entRow.User = Convert.ToString(reader["User"]);
                            entRow.Password = Convert.ToString(reader["Password"]);
                            entRow.Name = Convert.ToString(reader["Name"]);
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

        public static int ChangePassword(MUser ent, ref int Val)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;

                    SqlCommand cmd = new SqlCommand("sp_User_ChangePassword", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IUserId", SqlDbType.Int).Value = ent.UserId;
                    cmd.Parameters.Add("@IPassword", SqlDbType.VarChar).Value = ent.Password;

                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    Val = 0;

                    return 1;
                }

                catch (Exception ex)
                {
                    Val = 2;

                    return 0;

                }
            }
        }

        public static MUser Sel(MUser ent, ref int Val)
        {
            MUser result = new MUser();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_User_Sel", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IUserId", SqlDbType.Int).Value = ent.UserId;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    Val = 1;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.UserId = Convert.ToInt32(reader["UserId"]);
                            result.User = Convert.ToString(reader["User"]);
                            result.Name = Convert.ToString(reader["Name"]);
                            result.Type = Convert.ToInt32(reader["Type"]);
                            result.Status = Convert.ToInt32(reader["Status"]);

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
            return result;
        }
    }
}
