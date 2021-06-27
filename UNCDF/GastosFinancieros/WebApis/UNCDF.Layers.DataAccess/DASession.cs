using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace UNCDF.Layers.DataAccess
{
    public class DASession
    {
        public static int ValidateSession(int Type, string Token, int UserId)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ValidateSession", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IType", SqlDbType.VarChar).Value = Type;
                    cmd.Parameters.Add("@IToken", SqlDbType.VarChar).Value = Token;
                    cmd.Parameters.Add("@IUserId", SqlDbType.VarChar).Value = UserId;
                    cmd.Parameters.Add("@OVAL", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    int Result = Convert.ToInt32(cmd.Parameters["@OVAL"].Value);
                    con.Close();

                    return Result;
                }

                catch (SqlException ex)
                {
                    return 2;
                }
            }

        }
    }
}
