using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.DataAccess
{
    public class DAGenderTranslate
    { 
    public static int Insert(MGenderTranslate ent, ref int Val)
    {
        using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
        {
            try
            {
                Val = 1;

                SqlCommand cmd = new SqlCommand("sp_GenderTranslate_Ins", con);
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add("@IGenderId", SqlDbType.Int).Value = ent.GenderId;
                cmd.Parameters.Add("@ILanguageId", SqlDbType.Int).Value = ent.LanguageId;
                cmd.Parameters.Add("@IDescription", SqlDbType.VarChar).Value = ent.Description;
                cmd.Parameters.Add("@IValue", SqlDbType.VarChar).Value = ent.Value;
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                Val = 0;

                return ent.GenderId;
            }
            catch (Exception ex)
            {
                Val = 2;

                return 0;

            }
        }

    }

    public static int Update(MGenderTranslate ent, ref int Val)
    {
        using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
        {
            try
            {
                Val = 1;

                SqlCommand cmd = new SqlCommand("sp_GenderTranslate_Upd", con);
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add("@IGenderId", SqlDbType.Int).Value = ent.GenderId;
                cmd.Parameters.Add("@ILanguageId", SqlDbType.Int).Value = ent.LanguageId;
                cmd.Parameters.Add("@IValue", SqlDbType.VarChar).Value = ent.Value;
                cmd.Parameters.Add("@IDescription", SqlDbType.VarChar).Value = ent.Description;


                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Val = 0;

                return ent.GenderId;
            }

            catch (Exception ex)
            {
                Val = 2;

                return 0;

            }
        }

    }

    public static List<MGenderTranslate> Lis(MGenderTranslate ent, ref int Val)
    {
        List<MGenderTranslate> lisQuery = new List<MGenderTranslate>();
        using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_GenderTranslate_Lis", con);
                cmd.Parameters.Add("@IGenderId", SqlDbType.Int).Value = ent.GenderId;
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                Val = 1;

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        MGenderTranslate entRow = new MGenderTranslate();
                        entRow.GenderId = Convert.ToInt32(reader["GenderId"]);
                        entRow.LanguageId = Convert.ToInt32(reader["LanguageId"]);
                        entRow.Language = Convert.ToString(reader["Language"]);
                        entRow.Value = Convert.ToString(reader["Value"]);
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



    public static MGenderTranslate Select(MGenderTranslate ent, ref int Val)
    {
        MGenderTranslate result = new MGenderTranslate();
        using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_GenderTranslate_Sel", con);
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add("@IGenderId", SqlDbType.Int).Value = ent.GenderId;
                cmd.Parameters.Add("@ILanguageId", SqlDbType.Int).Value = ent.LanguageId;
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                Val = 1;

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.GenderId = Convert.ToInt32(reader["GenderId"]);
                        result.LanguageId = Convert.ToInt32(reader["LanguageId"]);
                        result.Language = Convert.ToString(reader["Language"]);
                        result.Value = Convert.ToString(reader["Value"]);
                        result.Description = Convert.ToString(reader["Description"]);
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

    public static int Delete(MGenderTranslate ent, ref int Val)
    {
        using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
        {
            try
            {
                Val = 1;

                SqlCommand cmd = new SqlCommand("sp_GenderTranslate_Del", con);
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add("@IGenderId", SqlDbType.Int).Value = ent.GenderId;
                cmd.Parameters.Add("@ILanguageId", SqlDbType.Int).Value = ent.LanguageId;
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Val = 0;

                return ent.GenderId;
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


