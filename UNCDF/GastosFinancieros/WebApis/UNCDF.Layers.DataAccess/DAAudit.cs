using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using UNCDF.Layers.Model;
using System.Data;

namespace UNCDF.Layers.DataAccess
{
    public class DAAudit
    {
        public static MAudit Insert(MAudit ent, ref int Val)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;

                    SqlCommand cmd = new SqlCommand("sp_Audit_Ins", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@ITable", SqlDbType.VarChar).Value = ent.Table;
                    cmd.Parameters.Add("@IRegisterId", SqlDbType.Int).Value = ent.RegisterId;
                    cmd.Parameters.Add("@IAction", SqlDbType.Int).Value = ent.Action;
                    cmd.Parameters.Add("@IUserId", SqlDbType.Int).Value = ent.UserId;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    Val = 0;

                    return ent;
                }
                catch (Exception ex)
                {
                    Val = 2;

                    return ent;
                }
            }

        }
    }
}
