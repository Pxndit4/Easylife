using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using UNCDF.Layers.Models;

namespace UNCDF.Layers.DataAccess
{
    public class DADonor
    {
        public static int Insert(MDonor ent, BaseRequest baseRequest)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_Donor_Ins", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IFirstName", SqlDbType.VarChar).Value = ent.FirstName;
                    cmd.Parameters.Add("@ILastName", SqlDbType.VarChar).Value = ent.LastName;
                    cmd.Parameters.Add("@IEmail", SqlDbType.VarChar).Value = ent.Email;
                    cmd.Parameters.Add("@IPassword", SqlDbType.VarChar).Value = ent.Password;
                    cmd.Parameters.Add("@ICellphone", SqlDbType.VarChar).Value = ent.Cellphone;
                    cmd.Parameters.Add("@IAddress", SqlDbType.VarChar).Value = ent.Address;
                    cmd.Parameters.Add("@ICountryId", SqlDbType.Int).Value = ent.CountryId;
                    cmd.Parameters.Add("@IGender", SqlDbType.VarChar).Value = ent.Gender;
                    cmd.Parameters.Add("@IBirthday", SqlDbType.Decimal).Value = ent.Birthday;
                    cmd.Parameters.Add("@IPhoto", SqlDbType.DateTime).Value = ent.Photo;
                    cmd.Parameters.Add("@IStatus", SqlDbType.Int).Value = ent.Status;
                    cmd.Parameters.Add("@IToken", SqlDbType.VarChar).Value = ent.Token;
                    cmd.Parameters.Add("@ODonorID", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    ent.DonorId = Convert.ToInt32(cmd.Parameters["@ODonorID"].Value);
                    con.Close();

                    return 0;
                }

                catch (Exception ex)
                {
                    return 2;
                }
            }
        }

        public static int Update(MDonor ent)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_Donor_Upd", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IDonorId", SqlDbType.VarChar).Value = ent.DonorId;
                    cmd.Parameters.Add("@IFirstName", SqlDbType.VarChar).Value = ent.FirstName;
                    cmd.Parameters.Add("@ILastName", SqlDbType.VarChar).Value = ent.LastName;
                    cmd.Parameters.Add("@IEmail", SqlDbType.VarChar).Value = ent.Email;
                    cmd.Parameters.Add("@ICellphone", SqlDbType.VarChar).Value = ent.Cellphone;
                    cmd.Parameters.Add("@IAddress", SqlDbType.VarChar).Value = ent.Address;
                    cmd.Parameters.Add("@ICountryId", SqlDbType.Int).Value = ent.CountryId;
                    cmd.Parameters.Add("@IGender", SqlDbType.VarChar).Value = ent.Gender;
                    cmd.Parameters.Add("@IBirthday", SqlDbType.Decimal).Value = ent.Birthday;
                    cmd.Parameters.Add("@IPhoto", SqlDbType.VarChar).Value = ent.Photo;
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

        public static int UpdateCode(MDonor ent)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_Donor_CodeUpd", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IPassword", SqlDbType.VarChar).Value = ent.Password;
                    cmd.Parameters.Add("@IEmail", SqlDbType.VarChar).Value = ent.Email;
                    cmd.Parameters.Add("@IToken", SqlDbType.VarChar).Value = ent.Token;
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

        public static int ValidateCode(MDonor ent)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_Donor_ValidateCode", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IPassword", SqlDbType.VarChar).Value = ent.Password;
                    //cmd.Parameters.Add("@ICellphone", SqlDbType.VarChar).Value = ent.Cellphone;
                    cmd.Parameters.Add("@IEmail", SqlDbType.VarChar).Value = ent.Email;
                    cmd.Parameters.Add("@OResult", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    int Result = Convert.ToInt32(cmd.Parameters["@OResult"].Value);
                    con.Close();

                    return Result;
                }

                catch (Exception ex)
                {
                    return 2;
                }
            }
        }

        public static int ChangePassword(MDonor ent)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_Donor_ChangePassword", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IDonorId", SqlDbType.VarChar).Value = ent.DonorId;
                    cmd.Parameters.Add("@IOldPassword", SqlDbType.VarChar).Value = ent.OldPassword;
                    cmd.Parameters.Add("@IEmail", SqlDbType.VarChar).Value = ent.Email; //cmd.Parameters.Add("@ICellphone", SqlDbType.VarChar).Value = ent.Cellphone;
                    cmd.Parameters.Add("@INewPassword", SqlDbType.VarChar).Value = ent.Password;
                    cmd.Parameters.Add("@OResult", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    int Result = Convert.ToInt32(cmd.Parameters["@OResult"].Value);
                    con.Close();

                    return Result;
                }

                catch (Exception ex)
                {
                    return 2;
                }
            }
        }

        public static int Delete(MDonor ent)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_Donor_Del", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IDonorId", SqlDbType.VarChar).Value = ent.Cellphone;

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

        public static MDonor Select(MDonor ent, ref int Val)
        {
            MDonor result = new MDonor();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_Donor_Select", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@iDonorId", SqlDbType.VarChar).Value = ent.DonorId;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    Val = 1;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.DonorId = Convert.ToInt32(reader["DonorId"]);
                            result.FirstName = Convert.ToString(reader["FirstName"]);
                            result.LastName = Convert.ToString(reader["LastName"]);
                            result.Email = Convert.ToString(reader["Email"]);

                            result.Password = Convert.ToString(reader["Password"]);
                            result.Cellphone = Convert.ToString(reader["Cellphone"]);
                            result.Address = Convert.ToString(reader["Address"]);
                            result.CountryId = Convert.ToInt32(reader["CountryId"]);
                            result.Birthday = Convert.ToDecimal(reader["Birthday"]);
                            result.Photo = (Convert.ToString(reader["Photo"]).Equals("")) ? "" :  Constant.S3Server + Convert.ToString(reader["Photo"]);
                            result.Token = Convert.ToString(reader["Token"]);
                            result.Gender = Convert.ToString(reader["Gender"]);

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

        public static List<MDonor> Lis(MDonor ent, ref int Val)
        {
            List<MDonor> lisQuery = new List<MDonor>();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_Donor_Lis", con);
                    cmd.Parameters.Add("@IFirstName", SqlDbType.VarChar).Value = ent.FirstName;
                    cmd.Parameters.Add("@ILastName", SqlDbType.VarChar).Value = ent.LastName;
                    cmd.Parameters.Add("@ICountryId", SqlDbType.Int).Value = ent.CountryId;
                    cmd.Parameters.Add("@IRegistered", SqlDbType.Int).Value = ent.Registered;
                    cmd.Parameters.Add("@IStatus", SqlDbType.Int).Value = ent.Status;
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MDonor entRow = new MDonor();
                            entRow.DonorId = Convert.ToInt32(reader["DonorId"]);
                            entRow.FirstName = Convert.ToString(reader["FirstName"]);
                            entRow.LastName = Convert.ToString(reader["LastName"]);
                            entRow.Email = Convert.ToString(reader["Email"]);
                            //entRow.Password = Convert.ToString(reader["Password"]);
                            entRow.Cellphone = Convert.ToString(reader["Cellphone"]);
                            entRow.Address = Convert.ToString(reader["Address"]);
                            entRow.CountryId = Convert.ToInt32(reader["CountryId"]);
                            entRow.Country = Convert.ToString(reader["Country"]);
                            entRow.Continent = Convert.ToString(reader["Continent"]);
                            entRow.Gender = Convert.ToString(reader["Gender"]);
                            entRow.Birthday = Convert.ToDecimal(reader["Birthday"]);
                            entRow.Registered = Convert.ToInt32(reader["Registered"]);
                            entRow.Status = Convert.ToInt32(reader["Status"]);

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

        public static MDonor ValidateDonor(MDonor ent, ref int Val)
        {
            MDonor result = new MDonor();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_Donor_ValUser", con);
                    cmd.CommandTimeout = 0;
                    //cmd.Parameters.Add("@ICellphone", SqlDbType.VarChar).Value = ent.Cellphone;
                    cmd.Parameters.Add("@IEmail", SqlDbType.VarChar).Value = ent.Email;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    Val = 1;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Email = Convert.ToString(reader["Email"]);
                            result.Cellphone = Convert.ToString(reader["Cellphone"]);
                            result.CountryId = Convert.ToInt32(reader["CountryId"]);

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


        public static MDonor Login(MDonor ent, ref int Val)
        {
            MDonor result = new MDonor();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_Donor_Login", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IEmail", SqlDbType.VarChar).Value = ent.Email;
                    cmd.Parameters.Add("@IPassword", SqlDbType.VarChar).Value = ent.Password;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    Val = 1;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.DonorId = Convert.ToInt32(reader["DonorId"]);
                            result.FirstName = Convert.ToString(reader["FirstName"]);
                            result.LastName = Convert.ToString(reader["LastName"]);
                            result.Email = Convert.ToString(reader["Email"]);

                            result.Password = Convert.ToString(reader["Password"]);
                            result.Cellphone = Convert.ToString(reader["Cellphone"]);
                            result.Address = Convert.ToString(reader["Address"]);
                            result.CountryId = Convert.ToInt32(reader["CountryId"]);
                            result.Birthday = Convert.ToDecimal(reader["Birthday"]);
                            result.Photo = (Convert.ToString(reader["Photo"]).Equals("")) ? "" : Constant.S3Server + Convert.ToString(reader["Photo"]);
                            result.Token = Convert.ToString(reader["Token"]);

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

        public static int ValidateInsert(string Cellphone, string Email, ref int Registered, ref int DonorId)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_Donor_ValidateIns", con);
                    cmd.CommandTimeout = 0;
                    //cmd.Parameters.Add("@ICellphone", SqlDbType.VarChar).Value = Cellphone;
                    cmd.Parameters.Add("@IEmail", SqlDbType.VarChar).Value = Email;
                    cmd.Parameters.Add("@OResult", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@ORegistered", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@ODonorId", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    int Result = Convert.ToInt32(cmd.Parameters["@OResult"].Value);
                    Registered = Convert.ToInt32(cmd.Parameters["@ORegistered"].Value);
                    DonorId = Convert.ToInt32(cmd.Parameters["@ODonorId"].Value);
                    con.Close();

                    return Result;
                }

                catch (Exception ex)
                {
                    return 2;
                }
            }
        }
    }
}
