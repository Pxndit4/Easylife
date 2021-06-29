using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Data;
using UNCDF.Layers.Model;


namespace UNCDF.Layers.DataAccess
{
    public class DAInterfaceControl
    {
        public static List<MInterfaceControl> List(MInterfaceControl ent, ref int Val)
        {
            List<MInterfaceControl> lisQuery = new List<MInterfaceControl>();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_InterfaceControl_Lis", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IInterfaceId", SqlDbType.VarChar).Value = ent.InterfaceId;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    Val = 1;
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MInterfaceControl entRow = new MInterfaceControl();
                            entRow.InterfaceControlId = Convert.ToInt32(reader["InterfaceControlId"]);
                            entRow.InterfaceId = Convert.ToInt32(reader["InterfaceId"]);
                            entRow.ControlName = Convert.ToString(reader["ControlName"]);
                            entRow.Description = Convert.ToString(reader["Description"]);
                            entRow.DescriptionControl = Convert.ToString(reader["DescriptionControl"]);
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

        public static int Insert(MInterfaceControl ent, ref int Val)
        {

            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;

                    SqlCommand cmd = new SqlCommand("sp_InterfaceControl_Ins", con);
                    cmd.Parameters.Add(new SqlParameter("@IInterfaceId", ent.InterfaceId));
                    cmd.Parameters.Add(new SqlParameter("@IControlName", ent.ControlName));
                    cmd.Parameters.Add(new SqlParameter("@IDescription", ent.Description));
                    cmd.Parameters.Add(new SqlParameter("@IDescriptionControl", ent.DescriptionControl));
                    cmd.Parameters.Add("@OInterfaceControlId", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    ent.InterfaceId = Convert.ToInt32(cmd.Parameters["@OInterfaceControlId"].Value);
                    con.Close();

                    Val = 0;

                    return ent.InterfaceId;
                }
                catch (Exception ex)
                {
                    Val = 2;

                    return 0;
                }
            }
        }

        public static int Update(MInterfaceControl ent, ref int Val)
        {

            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;

                    SqlCommand cmd = new SqlCommand("sp_InterfaceControl_Upd", con);
                    cmd.Parameters.Add(new SqlParameter("@IInterfaceControlId", ent.InterfaceControlId));
                    cmd.Parameters.Add(new SqlParameter("@IControlName", ent.ControlName));
                    cmd.Parameters.Add(new SqlParameter("@IDescription", ent.Description));
                    cmd.Parameters.Add(new SqlParameter("@IDescriptionControl", ent.DescriptionControl));
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Val = 0;

                    return ent.InterfaceControlId;
                }
                catch (Exception ex)
                {
                    Val = 2;

                    return 0;

                }
            }
        }


        public static MInterfaceControl Select(MInterfaceControl ent, ref int Val)
        {
            MInterfaceControl result = new MInterfaceControl();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;
                    SqlCommand cmd = new SqlCommand("sp_InterfaceControl_Sel", con);
                    cmd.CommandTimeout = 0;

                    cmd.Parameters.Add("@IInterfaceControlId", SqlDbType.Int).Value = ent.InterfaceControlId;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            result.InterfaceControlId = Convert.ToInt32(reader["InterfaceControlId"]);
                            result.InterfaceId = Convert.ToInt32(reader["InterfaceId"]);
                            result.ControlName = Convert.ToString(reader["ControlName"]);
                            result.Description = Convert.ToString(reader["Description"]);
                            result.DescriptionControl = Convert.ToString(reader["DescriptionControl"]);


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
        public static int Delete(MInterfaceControl ent, ref int Val)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;

                    SqlCommand cmd = new SqlCommand("sp_InterfaceControl_Del", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IInterfaceControlId", SqlDbType.Int).Value = ent.InterfaceControlId;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    Val = 0;

                    return ent.InterfaceId;
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
