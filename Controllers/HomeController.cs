using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReadME.Models;
using ReadME.Repository;

namespace ReadME.Controllers
{
    public class HomeController : Controller
    {

        private readonly IUserRepo _iuserRepo;
        public HomeController(IUserRepo userRepo)
        {
            _iuserRepo = userRepo;
        }

        [Route("/")]
        public IActionResult Index()
        {
            ViewBag.style="homeStyle.css";
            ViewBag.search=true;
            ViewBag.message = false;
            return View("Views/Home/HomeView/Home.cshtml");
        }

        [Route("/login")]
        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.style="loginStyle.css";
            ViewBag.search=false;
            if (TempData["success"] == null && TempData["error"] == null)
            {
                ViewBag.message = false;
            }
            else
            {
                ViewBag.message = true;
                ViewBag.success = TempData["success"] == null ? null : TempData["success"];
                ViewBag.error = TempData["error"] == null ? null : TempData["error"];
            }
            return View("Views/Home/HomeView/Login.cshtml");
        }


        [Route("/signup")]
        [HttpPost]
        public async Task<IActionResult> SignUp(AuthData authdata)
        {
            User signUser =await _iuserRepo.GetUserByEmail(authdata.Email);
            if (signUser != null)
            {
                if(!signUser.isVerified)
                {
                    return await this.VerificationAsync(signUser.Email);
                }
                else
                {
                
                    TempData["error"] = "Email already exists";
                    return Redirect("/login");
                }
            }

            User user = new User();
            user.Name = authdata.Username;
            user.Email = authdata.Email;
            user.Password = authdata.Password;
            user.isVerified = false;
            user.Earnings = 0;
            bool status = _iuserRepo.AddUser(user);

            if(status)
            {
                return await this.VerificationAsync(authdata.Email);
            }
            else
            {
                TempData["error"] = "Something went wrong";
                return Redirect("/login");
            }
        }


        [Route("/login")]
        [HttpPost]
        public async Task<IActionResult> Login(AuthData authdata)
        {
            User user =await _iuserRepo.GetUserByEmail(authdata.Email);
            if (user != null)
            {
                if (!user.isVerified)
                {
                    return await this.VerificationAsync(user.Email);
                }
                else
                {
                    if (user.Password == authdata.Password)
                    {
                        return Redirect("/");
                    }
                    else
                    {
                        TempData["error"] = "Invalid Password";
                        return Redirect("/login");
                    }
                }
            }

            TempData["error"] = "User does not exist";
            return Redirect("/login");


        }


       
        private async Task<IActionResult> VerificationAsync(string Email)
        {
            User user = await _iuserRepo.GetUserByEmail(Email);

            await _iuserRepo.DeleteOTPByUserId(user.Id);



            if (user != null)
            {
                Random random = new Random();
                long otp = random.Next(100000, 1000000);

                OTP otpObj = new OTP();
                otpObj.UserId = user.Id;
                otpObj.Otp = otp;
                otpObj.OtpTime = DateTime.Now;
                await _iuserRepo.GenerateOTPAsync(otpObj);
                try
                {
                    bool otpSent = SendOTPMail(Email, user.Name, otp.ToString());

                    if (otpSent)
                    {
                        ViewBag.style = "verificationStyle.css";
                        ViewBag.search = false;
                        ViewBag.message = true;
                        ViewBag.userId = user.Id;
                        ViewBag.success = "OTP sent to your E-mail";
                        return View("Views/Home/HomeView/Verification.cshtml");
                    }
                    else
                    {

                        TempData["error"] = "Something went wrong";
                        return Redirect("/login");
                    }
                }
                catch
                {
                    TempData["error"] = "Unable to send OTP";
                    return Redirect("/login");
                }

            }
            else
            {
                return Redirect("/login");
            }
        }

        [Route("/verify/{UserId}")]
        [HttpPost]
        public async Task<IActionResult> VerifyOTP(OTPData otpdata,int UserId)
        {

            await _iuserRepo.DeleteInvalidOTP();




            String userOTP = otpdata.dig1.ToString() + otpdata.dig2.ToString()+ otpdata.dig3.ToString() + otpdata.dig4.ToString()+ otpdata.dig5.ToString() + otpdata.dig6.ToString();
            Console.WriteLine(userOTP);
            OTP actualOtpObj = await _iuserRepo.GetOTPByUserId(UserId);
            String actualOTP = actualOtpObj.Otp.ToString();
            Console.WriteLine(actualOTP);
            

            if(userOTP==actualOTP)
            {
                ViewBag.message = true;
                User user=await _iuserRepo.GetUserById(UserId);
                user.isVerified = true;
                _iuserRepo.save();
;                TempData["success"] = "Verified! Please Login";
                return Redirect("/login");
            }
            else
            {
                ViewBag.style = "verificationStyle.css";
                ViewBag.search = false;
                ViewBag.message = true;
                ViewBag.userId = UserId;
                ViewBag.error = "Invalid OTP";
                return View("Views/Home/HomeView/Verification.cshtml");
            }
        }


        [HttpGet]
        [Route("/resend/{userId}")]
        public async Task<IActionResult> Resend(int userId)
        {
            User user = await _iuserRepo.GetUserById(userId);
            return await VerificationAsync(user.Email);
        }


        [HttpGet]
        [Route("/forgot")]
        public IActionResult Forgot()
        {
            ViewBag.search = false;
            ViewBag.message = false;
            ViewBag.style = "verificationStyle.css";

            return View("Views/Home/HomeView/Forgot.cshtml");
        }



        [HttpPost]
        [Route("/forgot")]
        public async Task<IActionResult> Forgot(ForgotData forgotData)
        {
            return await FVerification(forgotData.Email);
        }





        public async Task<IActionResult> FVerification(string femail)
        {
            User user = await _iuserRepo.GetUserByEmail(femail);

            await _iuserRepo.DeleteOTPByUserId(user.Id);


            if (user != null)
            {
                Random random = new Random();
                long otp = random.Next(100000, 1000000);

                OTP otpObj = new OTP();
                otpObj.UserId = user.Id;
                otpObj.Otp = otp;
                otpObj.OtpTime = DateTime.Now;
                await _iuserRepo.GenerateOTPAsync(otpObj);

                try
                {


                    bool otpSent =  SendOTPMail(femail, user.Name, otp.ToString());
                    if (otpSent)
                    {
                        ViewBag.style = "verificationStyle.css";
                        ViewBag.search = false;
                        ViewBag.message = true;
                        ViewBag.userId = user.Id;
                        ViewBag.success = "OTP sent to your E-mail";
                        return View("Views/Home/HomeView/FVerification.cshtml");
                    }
                    else
                    {

                        TempData["error"] = "Something went wrong";
                        return Redirect("/login");
                    }
                }
                catch
                {
                  
                    TempData["error"] = "Unable to send OTP";
                    return Redirect("/login");
                
                }

            }
            else
            {
                ViewBag.search = false;

                ViewBag.message = true;
                ViewBag.error = "User Does not exist";
                ViewBag.style = "verificationStyle.css";
                return View("Views/Home/HomeView/Forgot.cshtml");
            }
        }

      


        [Route("/fverify/{UserId}")]
        [HttpPost]
        public async Task<IActionResult> FVerifyOTP(OTPData otpdata, int UserId)
        {

            await _iuserRepo.DeleteInvalidOTP();


            String userOTP = otpdata.dig1.ToString() + otpdata.dig2.ToString() + otpdata.dig3.ToString() + otpdata.dig4.ToString() + otpdata.dig5.ToString() + otpdata.dig6.ToString();
            Console.WriteLine(userOTP);
            OTP actualOtpObj = await _iuserRepo.GetOTPByUserId(UserId);
            String actualOTP = actualOtpObj.Otp.ToString();
            Console.WriteLine(actualOTP);


            if (userOTP == actualOTP)
            {
         
                ViewBag.search = false;
                ViewBag.message = true;
                ViewBag.userId = UserId;
                ViewBag.success = "Please Change Password";
                ViewBag.style = "ChangePassStyle.css";
                return View("Views/Home/HomeView/ChangePass.cshtml");
            }
            else
            {
                ViewBag.style = "verificationStyle.css";
                ViewBag.search = false;
                ViewBag.message = true;
                ViewBag.userId = UserId;
                ViewBag.error = "Invalid OTP";
                return View("Views/Home/HomeView/FVerification.cshtml");
            }
        }


        [HttpGet]
        [Route("/fresend/{userId}")]
        public async Task<IActionResult> FResend(int userId)
        {
            User user = await _iuserRepo.GetUserById(userId);
            return await FVerification(user.Email);
        }


        [HttpPost]
        [Route("/change/{userId}")]
        public async Task<IActionResult> ChangePassword(ChangeData changeData, int userId)
        {
            try
            {
                User user = await _iuserRepo.GetUserById(userId);
                user.Password = changeData.Password;
                _iuserRepo.save();
                TempData["success"] = "Password Changed";
                return Redirect("/login");
            }
            catch
            {
                ViewBag.search = false;
                ViewBag.message = true;
                ViewBag.userId = userId;
                ViewBag.error = "Something went wrong";
                ViewBag.style = "ChangePassStyle.css";
                return View("Views/Home/HomeView/ChangePass.cshtml");
            }
        }


        public bool SendOTPMail(string to, string name, string otp)
        {
            string body = "<!DOCTYPE html><html lang='en'><head><meta charset='UTF-8'><meta http-equiv='X-UA-Compatible' content='IE=edge'><meta name='viewport' content='width=device-width, initial-scale=1.0'><title>OTP</title><link rel='stylesheet' href='https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css'><style> *{ margin:0px;padding:0px; box-sizing: border-box; font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; }.heading{text-align: center;background:#5708be;padding:15px;color:#fff;} h1 i{color:#cecece;} p i{color:#4c04d3; } b{color:#7300d1;} .content{padding:20px;}</style></head><body><div class='heading'><h1><span><i class='fa fa-dice-d6'></i> </span>Read.me</h1></div><div class='content'><p>Hi "+name+"</p><br><p>Please use this OTP <b>"+otp+"</b> for your Verification</p><br><p>Thanks and Regards</p><p><i class='fa fa-dice-d6'></i> Read.me</p></div></body></html>";
            string subject = "Verify ReadME OTP";
            return  Email.Send(to, subject, body);

        
        }



    }
}