using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.DataAccess
{
    public class DAParameter
    {
        public static List<MParameter> List(MParameter ent, ref int Val)
        {
            List<MParameter> lisQuery = new List<MParameter>();

            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_Parameters_List", con);
                    cmd.Parameters.Add("@ICode", SqlDbType.VarChar).Value = ent.Code;
                    cmd.Parameters.Add("@IDescription", SqlDbType.VarChar).Value = ent.Description;
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    Val = 1;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MParameter entRow = new MParameter();
                            entRow.ParameterId = Convert.ToInt32(reader["ParameterId"]);
                            entRow.Code = Convert.ToString(reader["Code"]);
                            entRow.Description = Convert.ToString(reader["Description"]);
                            entRow.Valor1 = Convert.ToString(reader["Valor1"]);
                            entRow.Valor2 = Convert.ToString(reader["Valor2"]);
                            lisQuery.Add(entRow);

                            Val = 0;
                        }
                    }
                    con.Close();
                }

                catch (Exception)
                {
                    Val = 2;

                }
            }

            return lisQuery;
        }
    }
}
