using Microsoft.EntityFrameworkCore;
using ReadME.Database;
using ReadME.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadME.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly DataContext _dataContext;

        public UserRepo(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
            

        public bool AddUser(User user)
        {
            _dataContext.Users.AddAsync(user);
            _dataContext.SaveChanges();
            return true;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            User user =await  _dataContext.Users.FirstOrDefaultAsync(user => user.Email == email);
            return user;
        }


        public async Task<bool> GenerateOTPAsync(OTP otpObj)
        {
           await _dataContext.OTP.AddAsync(otpObj);
            _dataContext.SaveChanges();
            return true;
        }

        public async Task<OTP> GetOTPByUserId(int UserId)
        {
            OTP otp = await _dataContext.OTP.FirstOrDefaultAsync(otp => otp.UserId == UserId);
            return otp;
        }

        public async Task<User> GetUserById(int UserId)
        {
            User user = await _dataContext.Users.FirstOrDefaultAsync(user => user.Id == UserId);
            return user;
        }

        public void save()
        {
            _dataContext.SaveChanges();
        }

        public async Task<bool> DeleteInvalidOTP()
        {
            List<OTP> otps = await _dataContext.OTP.ToListAsync();
            
             otps.ForEach(otp=>
            {
              
                if(DateTime.Now.Subtract(otp.OtpTime).TotalMinutes > 5)
                {
                    _dataContext.OTP.Remove(otp);
                    _dataContext.SaveChanges();
                }
            });

            return true;
        }


        public async Task<bool> DeleteOTPByUserId(int userId)
        {
            List<OTP> otps = await _dataContext.OTP.ToListAsync();

            otps.ForEach(otp =>
            {

                if (userId==otp.UserId)
                {
                    _dataContext.OTP.Remove(otp);
                    _dataContext.SaveChanges();
                }
            });

            return true;
        }
    }
}
