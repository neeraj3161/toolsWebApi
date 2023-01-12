var signInButton = $("#sign-in-btn");
var loginError = $("#password-validation");

function userDataModel(userData) {
    Id = userData.Id,
    email= userData.emailId,
    password=userData.pwd
}

loginError.hide();

signInButton.click(function () {
    var formData = $('form').serializeArray();
    console.log(formData);
    
    loginUser(formData);
});

function loginUser(formData) {
    $.ajax({
        url: "/Login/login",
        dataType: 'json',
        method:'post',
        data: {
            emailId: formData[0].value,
            pwd: formData[1].value
        },
        beforeSend: function () { },
        success: function (result)
        {
            if (result.success)
            {
                if (result.isValidUser) {
                    toastr.success('Sign in successfull', 'Success', {
                        positionClass: 'toast-top-left'

                    }); 

                } else
                {
                    toastr.error('Please check email and password', 'Error', {
                        positionClass: 'toast-top-left'

                    });  

                    loginError.show();

                    


                }

            }
            console.log(result);
        },
        error: function () { },
        complete: function () { }
    })
}