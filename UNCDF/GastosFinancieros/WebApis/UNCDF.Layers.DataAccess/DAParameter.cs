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


        public static MParameter Sel(MParameter ent, ref int Val)
        {
            MParameter result = new MParameter();

            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_Parameters_Sel", con);
                    cmd.Parameters.Add("@IParameterId", SqlDbType.Int).Value = ent.ParameterId;
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    Val = 1;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            result.ParameterId = Convert.ToInt32(reader["ParameterId"]);
                            result.Code = Convert.ToString(reader["Code"]);
                            result.Description = Convert.ToString(reader["Description"]);
                            result.Valor1 = Convert.ToString(reader["Valor1"]);
                            result.Valor2 = Convert.ToString(reader["Valor2"]);
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

        public static int Update(MParameter ent, ref int Val)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;

                    SqlCommand cmd = new SqlCommand("sp_Parameters_Upd", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IParameterId", SqlDbType.Int).Value = ent.ParameterId;
                    cmd.Parameters.Add("@ICode", SqlDbType.VarChar).Value = ent.Code;
                    cmd.Parameters.Add("@IDescription", SqlDbType.VarChar).Value = ent.Description;
                    cmd.Parameters.Add("@IValor1", SqlDbType.VarChar).Value = ent.Valor1;
                    cmd.Parameters.Add("@IValor2", SqlDbType.VarChar).Value = ent.Valor2;
                    cmd.Parameters.Add("@IStatus", SqlDbType.Int).Value = ent.Status;

                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Val = 0;

                    return ent.ParameterId;
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
