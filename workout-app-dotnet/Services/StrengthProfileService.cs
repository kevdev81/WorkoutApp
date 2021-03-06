﻿using System;
using System.Data;
using System.Data.SqlClient;
using workoutApp.Interfaces;
using workoutApp.Models.StrengthProfile;

namespace workoutApp.Services
{
    public class StrengthProfileService : IStrengthProfileService
    {
        private static string connString = "Server=.\\SQLEXPRESS;Database=WorkoutApp;Trusted_Connection=True;";

        public int Insert(StrengthProfileInsertRequest req)
        {
            int id = 0;
            using (SqlConnection con = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("dbo.StrengthProfile_Insert", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter outputParam = cmd.Parameters.Add("@Id", SqlDbType.Int);
                outputParam.Direction = ParameterDirection.Output;

                cmd.Parameters.AddWithValue("@UserId", req.UserId);
                cmd.Parameters.AddWithValue("@Weight", req.Weight);
                cmd.Parameters.AddWithValue("@BenchMax", req.BenchMax);
                cmd.Parameters.AddWithValue("@DeadliftMax", req.DeadliftMax);
                cmd.Parameters.AddWithValue("@SquatMax", req.SquatMax);
                cmd.Parameters.AddWithValue("@ShoulderPressMax", req.ShoulderPressMax);

                con.Open();
                cmd.ExecuteNonQuery();
                id = int.Parse(outputParam.Value.ToString());
                con.Close();
            }
            return id;
        }

        public StrengthProfileModel GetByUserId(int userId)
        {
            StrengthProfileModel strengthProfile = new StrengthProfileModel();
            using (SqlConnection con = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("dbo.StrengthProfile_SelectByUserId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", userId);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    strengthProfile.Id = (int)reader["Id"];
                    strengthProfile.UserId = userId;
                    strengthProfile.Weight = (decimal)reader["Weight"];
                    strengthProfile.BenchMax = (int)reader["BenchMax"];
                    strengthProfile.DeadliftMax = (int)reader["DeadliftMax"];
                    strengthProfile.SquatMax = (int)reader["SquatMax"];
                    strengthProfile.ShoulderPressMax = (int)reader["ShoulderPressMax"];
                    strengthProfile.DateCreated = Convert.ToDateTime(reader["DateCreated"]);
                    strengthProfile.DateModified = Convert.ToDateTime(reader["DateModified"]);
                }
                con.Close();
            }
            return strengthProfile;
        }

        //public void Update(AddressUpdateRequest address)
        //{
        //    _dataProvider.ExecuteNonQuery(
        //        "dbo.Addresses_Update",
        //        inputParamMapper: delegate (SqlParameterCollection paramCol)
        //        {
        //            paramCol.AddWithValue("@Id", address.Id);
        //            paramCol.AddWithValue("@Name", address.Name);
        //            paramCol.AddWithValue("@LineOne", address.LineOne);
        //            paramCol.AddWithValue("@LineTwo", address.LineTwo);
        //            paramCol.AddWithValue("@City", address.City);
        //            paramCol.AddWithValue("@Zip", address.Zip);
        //            paramCol.AddWithValue("@StateId", address.StateId);
        //            paramCol.AddWithValue("@CountryId", address.CountryId);
        //            paramCol.AddWithValue("@isBilling", address.IsBilling);
        //            paramCol.AddWithValue("@isProduct", address.IsProduct);
        //            paramCol.AddWithValue("@isMerchant", 0);
        //        }
        //    );
        //}

        //public void Update(int UserId)
        //{
        //    using (SqlConnection con = new SqlConnection(connString))
        //    {
        //        SqlCommand cmd = new SqlCommand("dbo.User_UpdateHasStrengthProfile", con);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.Parameters.AddWithValue("@Id", UserId);

        //        con.Open();
        //        cmd.ExecuteNonQuery();
        //        con.Close();
        //    }
        //}
    }
}