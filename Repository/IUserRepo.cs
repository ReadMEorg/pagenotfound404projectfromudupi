using ReadME.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadME.Repository
{
    public interface IUserRepo
    {
        public bool AddUser(User user);

        public Task<User> GetUserByEmail(String email);

        public Task<bool> GenerateOTPAsync(OTP otpObj);

        public Task<OTP> GetOTPByUserId(int UserId);

        public Task<User> GetUserById(int UserId);

        public void save();

        public Task<bool> DeleteInvalidOTP();

        public Task<bool> DeleteOTPByUserId(int UserId);
    }
}
