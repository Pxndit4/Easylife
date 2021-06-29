using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Data;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.DataAccess
{
    public class DAInterface
    {
        public static List<MInterface> List(MInterface ent, BaseRequest baseRequest)
        {
            List<MInterface> lisQuery = new List<MInterface>();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_Interface_Lis", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@ILanguage", SqlDbType.VarChar).Value = baseRequest.Language;
                    cmd.Parameters.Add("@ITypeId", SqlDbType.Int).Value = ent.TypeId;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MInterface entRow = new MInterface();
                            entRow.InterfaceId = Convert.ToInt32(reader["interfaceid"]);
                            entRow.TypeId = Convert.ToInt32(reader["TypeId"]);
                            entRow.InterfaceName = Convert.ToString(reader["interfaceName"]);
                            entRow.ControlName = Convert.ToString(reader["ControlName"]);
                            entRow.Description = Convert.ToString(reader["Description"]);
                            lisQuery.Add(entRow);
                        }
                    }
                    con.Close();
                }

                catch (Exception)
                {
                }
            }
            return lisQuery;
        }


        public static int Insert(MInterface ent, ref int Val)
        {

            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;

                    SqlCommand cmd = new SqlCommand("sp_Interface_Ins", con);
                    cmd.Parameters.Add(new SqlParameter("@ITypeId", ent.TypeId));
                    cmd.Parameters.Add(new SqlParameter("@IInterfaceName", ent.InterfaceName));
                    cmd.Parameters.Add(new SqlParameter("@IDescription", ent.Description));
                    cmd.Parameters.Add(new SqlParameter("@IStatus", Convert.ToInt32(ent.Status)));
                    cmd.Parameters.Add("@OInterfaceId", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    ent.InterfaceId = Convert.ToInt32(cmd.Parameters["@OInterfaceId"].Value);
                    con.Close();

                    Val = 0;

                    return ent.InterfaceId;
                }
                catch (Exception)
                {
                    Val = 2;

                    return 0;
                }
            }
        }

        public static int Update(MInterface ent, ref int Val)
        {

            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;

                    SqlCommand cmd = new SqlCommand("sp_Interface_Upd", con);
                    cmd.Parameters.Add(new SqlParameter("@IInterfaceId", ent.InterfaceId));
                    cmd.Parameters.Add(new SqlParameter("@ITypeId", ent.TypeId));
                    cmd.Parameters.Add(new SqlParameter("@IInterfaceName", ent.InterfaceName));
                    cmd.Parameters.Add(new SqlParameter("@IDescription", ent.Description));
                    cmd.Parameters.Add(new SqlParameter("@IStatus", ent.Status));
                    cmd.CommandTimeout = 0;
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


        public static MInterface Select(MInterface ent, ref int Val)
        {
            MInterface result = new MInterface();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;
                    SqlCommand cmd = new SqlCommand("sp_Interface_Sel", con);
                    cmd.CommandTimeout = 0;

                    cmd.Parameters.Add("@IInterfaceId", SqlDbType.Int).Value = ent.InterfaceId;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            result.InterfaceId = Convert.ToInt32(reader["InterfaceId"]);
                            result.TypeId = Convert.ToInt32(reader["TypeId"]);
                            result.InterfaceName = Convert.ToString(reader["InterfaceName"]);
                            result.Description = Convert.ToString(reader["Description"]);
                            result.Status = Convert.ToString(reader["Status"]);


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
        public static int Delete(MInterface ent, ref int Val)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;

                    SqlCommand cmd = new SqlCommand("sp_Interface_Del", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IInterfaceId", SqlDbType.Int).Value = ent.InterfaceId;
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

        public static List<MInterface> Filter(MInterface ent, ref int Val)
        {
            List<MInterface> lisQuery = new List<MInterface>();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;

                    SqlCommand cmd = new SqlCommand("sp_Interface_Fil", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IInterfaceName", SqlDbType.VarChar).Value = ent.InterfaceName;
                    cmd.Parameters.Add("@ITypeId", SqlDbType.Int).Value = ent.TypeId;
                    cmd.Parameters.Add("@IStatus", SqlDbType.Int).Value = ent.Status;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MInterface entRow = new MInterface();
                            entRow.InterfaceId = Convert.ToInt32(reader["InterfaceId"]);
                            entRow.TypeId = Convert.ToInt32(reader["TypeId"]);
                            entRow.InterfaceName = Convert.ToString(reader["InterfaceName"]);
                            entRow.Description = Convert.ToString(reader["Description"]);
                            entRow.Status = Convert.ToString(reader["Status"]);
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
    }
}
