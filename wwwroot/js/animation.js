$(document).ready(function(){
         
    $('header ul a').hover(function(){
        if($(window).width() > 1000)
        {
            $('header ul a').css('color','#d6d6d6');
            $(this).css('color','#000');
            $('.free-nav span').css("color","#fff");
            
            $('.hover').width(0);  
            $(this).children('.hover').width($(this).children('li').width());  
        }
    })
    $('header ul a').on('mouseleave',function(){
        if($(window).width() > 1000)
        {
            
            $('header ul a').css('color','#000');
            $('header ul a div').width(0);
            $('header ul a .active').width($("header ul a .active").siblings('li').width());  
            $('header .free-nav span').css('color','#fff');
        }
    });

 

   
    $('.more button').on('click',function()
    {
       
            
        
        $(this).toggleClass('more-white');
        $(this).parent().siblings(".card-layer").toggleClass('show-more');
        $(this).parent().siblings(".more-options").toggleClass('show-more');
    });

    $('.mobile-menu').on('click',function(){
        $('.main-menu nav').css('display','block');
        $(".close-nav").css('display','block');
    });


    $('.close-nav').on('click',function(){
        $('.main-menu nav').css('display','none');
    });
    


     // Message Animation

     $(".success-message button").on('click',function(){
        $(this).parent("div").css("margin-top","-200px");
    });

    $(".error-message button").on('click',function(){
        $(this).parent("div").css("margin-top","-200px");
    });


    // Login and SignUp

    $('.signupButton').on('click',function(){
        $(this).css({'color':'#070e69','font-weight':'500'});
        $('.loginButton').css({'color':'#78777a','font-weight':'400'});
        $('.loginForm').css('display','none');
        $('.signupForm').css('display','block');
    });

    $('.loginButton').on('click',function(){
        $(this).css({'color':'#070e69','font-weight':'500'});
        $('.signupButton').css({'color':'#78777a','font-weight':'400'});
        $('.signupForm').css('display','none');
        $('.loginForm').css('display','block');
    });


    $('.logo').click(function(){
        $(this).siblings('input').attr('type','text');
        $(this).css('display','none');
        $(this).siblings('.offLogo').css('display','block');
    });
    $('.offLogo').click(function(){
        $(this).siblings('input').attr('type','password');
        $(this).css('display','none');
        $(this).siblings('.logo').css('display','block');
    });
    

    // login validation

    $("#form-login").on('submit',function()
    {
        var username=$('#username').val();
        var password=$("#password").val();
        var isUsernameValid=true;
        var isPasswordValid=true;
        
        if(username.length<4)
        {
            $('#usernameLabel').html("Username must have atleast 4 char");
            $('#usernameLabel').css('color','red');
            isUsernameValid=false;
        }
        else
        {
            $('#usernameLabel').html("Username");
            $('#usernameLabel').css('color','#494949');
            isUsernameValid=true;
        }

        if(password.length<8)
        {
            $('#passwordLabel').html("Password must have atleast 8 char");
            $('#passwordLabel').css('color','red');
            isPasswordValid=false;
            
        }
        else
        {
            $('#passwordLabel').html("Password");
            $('#passwordLabel').css('color','#494949');
            isPasswordValid=true;

        }
        return isPasswordValid && isUsernameValid;

    });

    $("#form-signup").on('submit',function()
    {
        var regUsername=$('#reg-username').val();
        var regEmail=$("#reg-email").val();
        var regPassword=$("#reg-password").val();
        var regConfpass=$("#reg-confpass").val();
        var isUsernameValid=true;
        var isEmailValid=true;
        var isPasswordValid=true;
        var isConfPassValid=true;
        
        if(regUsername.length<4)
        {
            $('#reg-usernameLabel').html("Username must have atleast 4 char");
            $('#reg-usernameLabel').css('color','red');
            isUsernameValid=false;
        }
        else
        {
            $('#reg-usernameLabel').html("Username");
            $('#reg-usernameLabel').css('color','#494949');
            isUsernameValid=true;
        }

        if(regPassword.length<8)
        {
            $('#reg-passwordLabel').html("Password must have atleast 8 char");
            $('#reg-passwordLabel').css('color','red');
            isPasswordValid=false;
            
        }
        else
        {
            $('#reg-passwordLabel').html("Password");
            $('#reg-passwordLabel').css('color','#494949');
            isPasswordValid=true;

        }

        if(regPassword!=regConfpass)
        {
            $('#reg-confpassLabel').html("Password is not matching");
            $('#reg-confpassLabel').css('color','red');
            isConfPassValid=false;
            
        }
        else
        {
            $('#reg-confpassLabel').html("Confirm Password");
            $('#reg-confpassLabel').css('color','#494949');
            isConfPassValid=true;

        }

        
        if(!regEmail.match(/^[^\s@]+@[^\s@]+$/))
        {
            $('#reg-emailLabel').html("Invalid email address");
            $('#reg-emailLabel').css('color','red');
            isEmailValid=false;
            
        }
        else
        {
            $('#reg-emailLabel').html("E-mail Address");
            $('#reg-emailLabel').css('color','#494949');
            isEmailValid=true;

        }

        


        return isUsernameValid && isEmailValid && isPasswordValid &&  isConfPassValid;

    });



    $("#form-forgotpassword").on('submit',function()
    {
     
        var fpEmail=$("#fp-input").val();
        var isFpEmailValid=true;
        
        if(!fpEmail.match(/^[^\s@]+@[^\s@]+$/))
        {
            $('#fp-emailLabel').html("Invalid email address");
            $('#fp-emailLabel').css('color','red');
            isFpEmailValid=false;
            
        }
        else
        {
            $('#fp-emailLabel').html("E-mail Address");
            $('#fp-emailLabel').css('color','#494949');
            isFpEmailValid=true;

        }

       
        return isFpEmailValid;

    });




    // OTP Verification
    

    $("#otp-1").on('input',function(e)
    {
       $(this).val("");
       OTPValidation();
       $(this).val(e.originalEvent.data);
        if($(this).val().length==1)
        {
            $("#otp-2").focus();
            OTPValidation();
        }
       
    });

    $("#otp-2").on('input',function(e)
    {
        $(this).val("");
        OTPValidation();
        $(this).val(e.originalEvent.data);
        if($(this).val().length==1)
        {
            $("#otp-3").focus();
            OTPValidation();
        }
    });

    $("#otp-3").on('input',function(e)
    {
        $(this).val("");
        OTPValidation();
        $(this).val(e.originalEvent.data);
        if($(this).val().length==1)
        {
            $("#otp-4").focus();
            OTPValidation();
        }
    });

    $("#otp-4").on('input',function(e)
    {
        $(this).val("");
        OTPValidation();
        $(this).val(e.originalEvent.data);
        if($(this).val().length==1)
        {
            $("#otp-5").focus();
            OTPValidation();
        }
    });

    $("#otp-5").on('input',function(e)
    {
        $(this).val("");
        OTPValidation();
        $(this).val(e.originalEvent.data);
        if($(this).val().length==1)
        {
            $("#otp-6").focus();
            OTPValidation();
        }
    });

    $("#otp-6").on('input',function(e)
    {
        $(this).val("");
        OTPValidation();
        $(this).val(e.originalEvent.data);
        if($(this).val().length==1)
        {
            $("#otp-6").blur();
            
            OTPValidation();
        }
    });


    function OTPValidation()
    {
        if($("#otp-1").val().length>0 && $("#otp-2").val().length>0 && $("#otp-3").val().length>0 && 
        $("#otp-4").val().length>0 && $("#otp-5").val().length>0 && $("#otp-6").val().length>0)
        {
           
           
            $(".otp-verify-btn").removeAttr("disabled");
            $(".otp-verify-btn").css("background","#36128a");
            $(".otp-verify-btn").css("cursor","pointer");
        }
        else
        {
            $(".otp-verify-btn").attr("disabled","true");
            $(".otp-verify-btn").css("background","#36128a4b");
            $(".otp-verify-btn").css("cursor","not-allowed");
        }
    }

    // Timer

    var currentVal=25;
    var timer25=setInterval(timer, 1000);

    function timer()
    {
        if(currentVal==0)
        {
            $(".otp-resend p").css("display","none");
            $(".otp-resend a").css("display","block");
            timer25.clearInterval;
           
        }
        currentVal--;
        $("#timer25").html(currentVal);
    }

    var min=4;
    var sec=59;

    var timer5min=setInterval(timerPro,1000);

    function timerPro()
    {
        if(sec==0)
        {
            if(min==0)
            {
                $(".main-timer").html("<i class='fa fa-ban' style='font-size:24px;color:red;'></i>");
                $(".otp input").attr('disabled','true');
                $(".otp input").css('border','none');
                $(".otp input").css('background','#cecece');
                timer5min.clearInterval;
            }
            min--;
            if(min==0)
            {
                $("#min, #sec").css('color','red');
            }
            $("#min").html(min);
            sec=60;
        }
        sec--;
        $("#sec").html(sec);
    }



    // Change Password
    
    $("#form-changepass").on('submit',function()
    {
       
        var regPassword=$("#reg-password").val();
        var regConfpass=$("#reg-confpass").val();
      
        var isPasswordValid=true;
        var isConfPassValid=true;
        
     

        if(regPassword.length<8)
        {
            $('#reg-passwordLabel').html("Password must have atleast 8 char");
            $('#reg-passwordLabel').css('color','red');
            isPasswordValid=false;
            
        }
        else
        {
            $('#reg-passwordLabel').html("Password");
            $('#reg-passwordLabel').css('color','#494949');
            isPasswordValid=true;

        }

        if(regPassword!=regConfpass)
        {
            $('#reg-confpassLabel').html("Password is not matching");
            $('#reg-confpassLabel').css('color','red');
            isConfPassValid=false;
            
        }
        else
        {
            $('#reg-confpassLabel').html("Confirm Password");
            $('#reg-confpassLabel').css('color','#494949');
            isConfPassValid=true;

        }

        
    

        


        return isPasswordValid &&  isConfPassValid;

    });

   

})