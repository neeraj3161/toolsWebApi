var signInButton = $("#sign-in-btn");
var loginError = $("#password-validation");

var signUpButton = $("#sign-up-btn");

var registrationModal;

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

// var registrationModel(Model) ={
//    Name : Model.Name,
//     Surname : Model.Surname,
//     Email : Model.Email,
//     Password : Model.Password
//}


signUpButton.click(function () {

    //var formData = $('form').serializeArray();
    //console.log("sign up clicked");
    //var Model = {
    //     Name:formData[1].value,
    //    Surname: formData[2].value,
    //    Email: formData[0].value,
    //    Password: formData[3].value
    //}
    

    //RegisterUser(Model);

    
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

function RegisterUser(ResgitrationModel,enteredotp)
{

    $.ajax({
        url: "/Login/UserRegistration",
        dataType: 'json',
        method: 'post',
        data: { registration: ResgitrationModel, enteredOtp: enteredotp },
        beforeSend: function () { },
        success: function (result) {
            if (result.success) {
                    toastr.success('Registration Successfull', 'Success', {
                        positionClass: 'toast-top-left'

                    });

                }

           
        },
        error: function () { },
        complete: function () { }
    })
}

function SendOtp(ResgitrationModel) {

    $.ajax({
        url: "/Login/SendOtp",
        dataType: 'json',
        method: 'post',
        data: { registration: ResgitrationModel },
        beforeSend: function () { },
        success: function (result) {
            if (result.success) {
                //toastr.success('Registration Successfull', 'Success', {
                   // positionClass: 'toast-top-left'

                //});



            }


        },
        error: function () { },
        complete: function () { }
    })
}

function viewModel() 
{
    this.pwd = ko.observable(""),
    this.isValidName = ko.observable(false),
    this.pwd.subscribe(function (newValue) {
        //alert(newValue);
    });
    this.isValidPassword = ko.observable(false),
    this.isTruePassword = ko.observable(false),
    this.isValidEmail = ko.observable(false);

    this.enteredEmailId = ko.observable("");
    this.enteredFirstName = ko.observable("");

    this.enteredOtp = ko.observable();
    signUpbtn = function ()
    {
            var formData = $('form').serializeArray();
    
            var Model = {
                 Name:formData[1].value,
                Surname: formData[2].value,
                Email: formData[0].value,
                Password: formData[3].value
        }

        registrationModal = Model;

        SendOtp(Model);


        //console.log((this.enteredEmailId()));
        //console.log(this.enteredEmailId().includes("@"));
        //this.isValidEmail(false);
        //this.validateEmail();
        //this.validateFirstName();


    }

    this.registerUser = function ()
    {
        console.log("submit button clicked");
        RegisterUser(registrationModal, this.enteredOtp());
    }


    this.validateEmail = function()
    {
        

        var emailValue = this.enteredEmailId();
        if (!emailValue.includes("@")) {
            this.isValidEmail(true);
            return false;
        }
        return true;
    }

    this.validateFirstName = function ()
    {
        const reg = new RegExp('^[0-9]+$');
        //if (this.enteredFirstName().includes(reg))
        //{
        //    this.isValidName(true);
        //    return false;
        //}
        return true;
    }
}

var vm = new viewModel();
ko.applyBindings(vm);

/*vm.isValidEmail(true);*/