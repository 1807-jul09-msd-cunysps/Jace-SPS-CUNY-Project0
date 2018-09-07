//Web API Endpoints and Methods
//Check User URL: http://team3webapi.azurewebsites.net/api/checkuser  SUPPORT: POST & GET
//Create User Post:  http://team3webapi.azurewebsites.net/api/userlogin

//OnLoad Variables
//variables to store returned ContactGUID and Mortgage Account Number
var contactID, contactObj, mortgageList;

//Global Variables/ Objects
var signinFields = {
    UserName: false,
    Password: false,
    MortgageNumber: false
};
var result;

var passwordValidation = {
    length: false,
    chars: false,
    symbols: false,
    numbers: false
};

//Page Load Functions======================================================================================

//on Page load call checkSessionLoad()
//window.onload = checkSessionLoad;

function loadInfo(currentPage) {
    let xmlHttp = new XMLHttpRequest();
    xmlHttp.onreadystatechange = function () {
        if (this.readyState === 4 && this.status === 200) {
            contactObj = JSON.parse(this.responseText);
            switch (currentPage) {
                case "home":
                    loadHomeInfo();
                    break;
                case "account":
                    loadAccountInfo();
                    break;
                default:
            }
        } else {
            contactObj = "An Error Has Occured";
            loadAccountInfo();
        }
    };
    xmlHttp.open("GET", "http://team3webapi.azurewebsites.net/api/user/" + contactID, true);
    xmlHttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    xmlHttp.send();
}
function loadMortgages() {
    let xmlHttp = new XMLHttpRequest();
    xmlHttp.onreadystatechange = function () {
        if (this.readyState === 4 && this.status === 200) {
            mortgageList = JSON.parse(this.responseText);

            for (var i = 0; i < mortgageLis.length; i++) {
                let element = document.createElement("DIV");
                element.classList.add("mortgage_list");
            }

        } else {
            contactObj = "An Error Has Occured";
            loadAccountInfo();
        }
    };
    xmlHttp.open("GET", "http://team3webapi.azurewebsites.net/api/mortgage/" + contactID, true);
    xmlHttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    xmlHttp.send();
}

function loadHomeInfo() {
    document.getElementById("sign_in").classList.add("no_display");
    document.getElementById("account").classList.remove("no_display");
}

function loadAccountInfo() {
    //var result = typeof contactObj;
    if (typeof contactObj === 'string') {
        document.getElementById("error").classList.remove("hidden");
        document.getElementById("error").innerHTML = contactObj;
    } else {
        document.getElementById("error").classList.add("hidden");
        document.getElementById("accountContainer").classList.remove("hidden");
        document.getElementById("hello").innerHTML = "Hello " + contactObj["firstname"];
        loadMortgages();
    }

}

//Cookie Functions==============================================================

//Session Cookie Functions======================================================
function setSession(cGUID, cUser) {
    let sesObj = {
        "GUID": cGUID.substring(1, cGUID.length - 1),
        "username": cUser
    };
    document.cookie = "user=" + JSON.stringify(sesObj) + ";" + "path=/";
    window.location.replace("/Pages/Account.html");
}

function getSession(cName) {
    cName = cName + "=";
    let cookieList = document.cookie.split(";");
    for (var i = 0; i < cookieList.length; i++) {
        let cookie = cookieList[i];
        if (cookie === "") { continue; }
        while (cookie.charAt(0) === "") {
            cookie = cookie.substring(1);
        }
        if (cookie.indexOf(cName) === 0) {
            let result = cookie.substring(cName.length, cookie.length);
            return result;
        }
    }
    return false;
}

function delSession() {
    document.cookie = 'user=; expires=Thu, 01 Jan 1970 00:00:01 GMT;';
}

function checkSessionLoad(currentPage) {
    let value = getSession("user");
    if (value !== false) {
        let userObj = JSON.parse(value);
        contactID = userObj["GUID"];
        switch (currentPage) {
            case "home":
                loadHomeInfo();
                break;
            case "account":
                break;
            default:
        }
        loadInfo(currentPage);
    }
}

function checkSession(cName) {
    let value = getSession(cName);
    if (value !== false) {
        let userObj = JSON.parse(value);
        alert(cName + ":" + userObj["username"]);
    } else {
        alert("No Value Found");
    }
}


//Sign In Functionality=========================================================

function selectformSignIn(value) {
    if (value) {
        document.getElementById("formSignIn").classList.remove("hidden");
        document.getElementById("formSelect").classList.add("fadeOutNext");
    } else {
        document.getElementById("formSignIn").classList.add("hidden");
        document.getElementById("formSelect").classList.remove("fadeOutNext");
    }
}

function formSignInSubmit() {
    let signInObj = {
        UserName: document.getElementById("signInUserName").value,
        Password: document.getElementById("signInPassword").value
    };
    var xmlHttp = new XMLHttpRequest();
    xmlHttp.open("POST", "http://team3webapi.azurewebsites.net/api/checkuser", true);
    xmlHttp.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
    xmlHttp.send(JSON.stringify(signInObj));
    xmlHttp.onreadystatechange = function () {
        if (xmlHttp.readyState === 4 && xmlHttp.status === 200) {
            setSession(this.responseText, signInObj.UserName);
        } else if (xmlHttp.readyState === 4 && xmlHttp.status === 400) {
            let result = JSON.parse(this.responseText);
            document.getElementById("signInSubmitValidate").innerHTML = result["Message"];
        }
    };
}
//Register Funcitonality=========================================================
function selectformRegister(value) {
    if (value) {
        document.getElementById("formRegister").classList.remove("hidden");
        document.getElementById("formSelect").classList.add("fadeOutNext");

    } else {
        document.getElementById("formSelect").classList.remove("fadeOutNext");
        document.getElementById("formRegister").classList.add("hidden");
    }
}

function validateUserName(element) {
    let results = false;
    let text = "";
    if (signinFields.UserName === false || signinFields.UserName !== element.value) {
        if (element.value.length !== 0) {
            if (!validateSymbolsCheck(element) && element.value.length > 4) {
                results = checkUserName(element);
                return;
            } else {
                results = false;
                text = "Please enter a User Name with no symbols and at least 5 characters";
                element.classList.add("is-invalid");
            }
        }
        signinFields["UserName"] = results;
        document.getElementById(element.id + "Validate").innerHTML = text;
    }
}

function checkUserName(element) {
    let userName = element.value;
    let xmlHttp = new XMLHttpRequest();
    xmlHttp.onreadystatechange = function () {
        if (this.readyState === 4 && this.status === 200) {
            let reqResult = this.responseText;
            reqResult = reqResult.substring(1, reqResult.length - 1);
            let text = "";
            if (reqResult === "Success") {
                reqResult = userName;
                element.classList.remove("is-invalid");
            }
            signinFields["UserName"] = reqResult;
            document.getElementById(element.id + "Validate").innerHTML = text;
        } else if (this.readyState === 4 && this.status === 400) {
            let reqResult = JSON.parse(this.responseText)["Message"];
            //reqResult = reqResult.substring(1, reqResult.length - 1);
            let text = "";
            if (reqResult === "Contact Exist!") {
                reqResult = false;
                element.classList.add("is-invalid");
                text = "This User Name is taken.";
            } 
            signinFields["UserName"] = reqResult;
            document.getElementById(element.id + "Validate").innerHTML = text;
        }
    };
    xmlHttp.open("GET", "http://team3webapi.azurewebsites.net/api/checkuser/" + userName, true);
    xmlHttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    xmlHttp.send();
}

    /*Password Requirements
     *> 8
     * > 1 Char
     * > 1 number
     * > 1 symbol

    */

function passwordFormat(element) {
    let length = validatePasswordLength(element.value);
    validateHelp("length", length);
    let chars = validatePasswordChars(element);
    validateHelp("chars", chars);
    let symbols = validateSymbolsCheck(element);
    validateHelp("symbols", symbols);
    let numbers = validateContainsNumber(element);
    validateHelp("numbers", numbers);
}

function showHelp() {
    document.getElementById("help").classList.remove("hidden");
}

function validateHelp(elementID, valid) {
    if (valid) {
        document.getElementById(elementID).classList.add("help_element_valid");
    } else {
        document.getElementById(elementID).classList.remove("help_element_valid");
    }
    passwordValidation[elementID] = valid;
}

function validatePassword(element, element2) {
    let results = false;
    let passwordResults = true;
    let text = "";

    let passwordFields = ["length", "symbols", "chars", "numbers"];
    for (var i = 0; i < passwordFields.length; i++) {
        if (passwordValidation[passwordFields[i]] === false) {
            passwordResults = false;
        }
    }
    if (element.value.length !== 0) {
        if (passwordResults) {
            if (element.value.length !== 0 && element2.value.length !== 0) {
                if (element.value === element2.value) {
                    element.classList.remove("is-invalid");
                    element2.classList.remove("is-invalid");
                    element.classList.add("is-valid");
                    element2.classList.add("is-valid");
                    text = "";
                    results = element.value;
                    document.getElementById(element2.id + "Validate").innerHTML = "";
                } else {
                    element.classList.add("is-invalid");
                    element2.classList.add("is-invalid");
                    element.classList.remove("is-valid");
                    element2.classList.remove("is-valid");
                    text = "The Passwords doesn't match.";
                    results = false;
                }
            } else {
                element.classList.remove("is-invalid");
                element2.classList.remove("is-invalid");
                element.classList.remove("is-valid");
                element2.classList.remove("is-valid");
                text = "";
                results = false;
                document.getElementById(element2.id + "Validate").innerHTML = "";
            }
        } else {
            document.getElementById("registerPassword").classList.add("is-invalid");
            results = false;
        }
    } else {
        element.classList.remove("is-invalid");
        element2.classList.remove("is-invalid");
        element.classList.remove("is-valid");
        element2.classList.remove("is-valid");            
    }
    signinFields['Password'] = results;
    document.getElementById(element.id + "Validate").innerHTML = text;
    document.getElementById("help").classList.add("hidden");
}

function validateMortgage(element) {
    let text = "";
    let results = false;
    if (validateNumbersCheck(element) && element.value.length === 12) {
        results = element.value;
        text = "";
        element.classList.remove("is-invalid");
    } else {
        results = false;
        text = "The Mortgage Number entered is invalid";
        element.classList.add("is-invalid");
    }
    signinFields["MortgageNumber"] = results;
    document.getElementById(element.id + "Validate").innerHTML = text;
}

function formRegisterSubmit() {
    let fields = ["UserName", "Password", "MortgageNumber"];

    validateUserName(document.getElementById("registerUserName"));
    validatePassword(document.getElementById("registerPassword"), document.getElementById("registerPassword2"));
    validateMortgage(document.getElementById("registerMortgageNumber"));

    let clearedValidation = true;
    for (var i = 0; i < fields.length; i++) {
        if (signinFields[fields[i]] === false) {
            clearedValidation = false;
            if (fields[i] === "Password") {
                let element = document.getElementById("registerPassword");
                element.classList.add("is-invalid");
                document.getElementById("registerPasswordValidate").innerHTML = "Password is not valid.";
            }
        }
    }
    if (clearedValidation) {
        //Ajax call
        document.getElementById("registerSubmitValidate").innerHTML = "";
        let registerObj = {};
        for (let i = 0; i < fields.length; i++) {
            registerObj[fields[i]] = signinFields[fields[i]];
        }
        var xmlHttp = new XMLHttpRequest();
        xmlHttp.open("POST", "http://team3webapi.azurewebsites.net/api/userlogin", true);
        xmlHttp.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
        xmlHttp.send(JSON.stringify(registerObj));
        xmlHttp.onreadystatechange = function () {
            if (xmlHttp.readyState === 4 && xmlHttp.status === 200) {
                let result = this.responseText;
            }
        };
        //document.getElementById("formDocuments").classList.remove("hiddenForm");
        //document.getElementById("formMortgage").classList.add("fadeOutNext");
    } else {
        document.getElementById("registerSubmitValidate").innerHTML = "There are errors on the form.";
    }
}