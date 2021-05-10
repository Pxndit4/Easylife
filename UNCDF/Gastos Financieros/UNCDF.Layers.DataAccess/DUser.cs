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
    }
}
