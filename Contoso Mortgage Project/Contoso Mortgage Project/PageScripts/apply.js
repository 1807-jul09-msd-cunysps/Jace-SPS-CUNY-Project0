




//Global Variables
//Variable to hole all field values on page
var formFields = {
    FirstName: false,
    LastName: false,
    Email: false,
    SSN: false,
    BusinessPhone: false,
    MobilePhone: false,
    AddressLine1: false,
    AddressLine2: false,
    City: false,
    State: false,
    Country: false,
    ZipCode: false,
    MortgageName: false,
    MortgageAmount: false,
    Option: true
};
//variables to store returned ContactGUID and Mortgage Account Number
var contactID, mortgageResult;

//Validation====================================================
function validatePasswordLength(string) {
    return (string.length >= 8) ? true : false;
}
function validatePasswordChars(element) {
    var val = /[a-zA-Z\s]/;
    let results = val.test(element.value) ? true : false;
    return results;
}

//Validate All Charcters
function validateCharsCheck(element) {
    var val = /^[a-zA-Z\s]+$/;
    let results = val.test(element.value) ? true : false;
    return results;
}

//Validate Symbols
function validateSymbolsCheck(element) {
    const val = /[~`!#$%\^&*+=\-\[\]\\';,/{}|\\":<>\?\(\)_]/;
    let results = val.test(element.value) ? true : false;
    return results;
}
//Validate Numbers
function validateNumbersCheck(element) {
    const val = /^\d*$/;
    let results = val.test(element.value) ? true : false;
    return results;
}

function validateContainsNumber(element) {
    const val = /\d/;
    let results = val.test(element.value) ? true : false;
    return results;
}
//Validate SSN format xxx-xx-xxxx | xxxxxxxxx
function validateSSNCheck(element) {
    const val = /^(?:\d{3}-\d{2}-\d{4}|\d{9})$/;
    let results = val.test(element.value) ? true : false;
    return results;
}
//Validate Phone Number (xxx) xxx-xxxx | xxx-xxx-xxxx | xxxxxxxxxx
function validatePhoneCheck(element) {
    const val = /^(?:\(\d{3}\)\d{3}-\d{4}|\d{3}-\d{3}-\d{4}|\d{10})$/;
    let results = val.test(element.value) ? true : false;
    return results;
}
//Validate Any input
function validate(element) {
    let text = "";
    if (element.value.length === 0) {
        element.classList.add("is-invalid");
        text = "This field is required.";
        formFields[element.id] = false;
    } else {
        element.classList.remove("is-invalid");
        formFields[element.id] = element.value;
    }
    document.getElementById(element.id + "Validate").innerHTML = text;
}
//Validate Alphanumeric Characters 
function validateChars(element) {
    let results;
    let text = "";
    if (validateCharsCheck(element) && !validateSymbolsCheck(element)) {
        text = "";
        element.classList.remove("is-invalid");
        results = element.value;
    } else {
        element.classList.add("is-invalid");
        text = (element.value.length !== 0) ?
            "Numeric values and symbols are not allowed in this field." :
            "This field is required.";
        results = false;
    }
    document.getElementById(element.id + "Validate").innerHTML = text;
    formFields[element.id] = results;
}
//Validate Numeric Characters
function validateNumbers(element) {
    let results;
    let text = "";
    if ((validateNumbersCheck(element) && !validateSymbolsCheck(element)) && element.value.length !== 0) {
        text = "";
        element.classList.remove("is-invalid");
        results = element.value;
    } else {
        element.classList.add("is-invalid");
        text = (element.value.length !== 0) ?
            "Only numeric values are allowed in this field." :
            "This field is required.";
        results = false;
    }
    document.getElementById(element.id + "Validate").innerHTML = text;
    formFields[element.id] = results;
}
//Validate SSN Format and Numeric Characters
function validateSSN(element) {
    let results;
    let text = "";
    if (validateSSNCheck(element) && !validateCharsCheck(element)) {
        text = "";
        element.classList.remove("is-invalid");
        results = element.value;
    } else {
        element.classList.add("is-invalid");
        text = (element.value.length !== 0) ?
            "Please input your 9 digit SSN, '-' are optional." :
            "This field is required.";
        results = false;
    }
    document.getElementById(element.id + "Validate").innerHTML = text;
    formFields[element.id] = results;
}
//Validate Phone Number format and Numeric Characters
function validatePhone(element) {
    var results;
    let text = "";
    if (validatePhoneCheck(element) && !validateCharsCheck(element)) {
        text = "";
        element.classList.remove("is-invalid");
        results = element.value;
    } else {
        element.classList.add("is-invalid");
        text = (element.value.length !== 0) ?
            "Please input your phone number digit SSN '()' and '-' are optional." :
            "This field is required.";
        results = false;
    }
    document.getElementById(element.id + "Validate").innerHTML = text;
    formFields[element.id] = results;
}
//Validation====================================================


//Submit Contact ================================================
function formContactSubmit() {
    /*let fields = [
        "FirstName", "LastName","State", "Country", "Email", "SSN"
    ];*/
    let fields = ["FirstName", "LastName", "City", "State", "Country", "Email", "AddressLine1", "AddressLine2", "MobilePhone", "BusinessPhone", "SSN", "ZipCode"];
    validateAllContactFields(fields);
    let clearedValidation = true;
    for (let i = 0; i < fields.length; i++) {
        if (formFields[fields[i]] === false) {
            clearedValidation = false;
        }
    }
    if (clearedValidation) {
        //Ajax call
        document.getElementById("SubmitValidate").innerHTML = "";
        let contactObj = {};
        for (let i = 0; i < fields.length; i++) {
            contactObj[fields[i]] = formFields[fields[i]];
        }

        var xmlHttp = new XMLHttpRequest();
        xmlHttp.open("POST", "http://team3webapi.azurewebsites.net/api/user", true);
        xmlHttp.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
        xmlHttp.send(JSON.stringify(contactObj));
        xmlHttp.onreadystatechange = function () {
            if (xmlHttp.readyState === 4 && xmlHttp.status === 200) {
                contactID = this.response;
                contactID = contactID.substring(1, contactID.length - 1);

            }
        };
        document.getElementById("formMortgage").classList.remove("hiddenForm");
        document.getElementById("formContact").classList.add("fadeOutNext");

    } else {
        document.getElementById("SubmitValidate").innerHTML = "There are errors on the form.";
    }
}

function validateAllContactFields(fields) {
    for (var i = 0; i < fields.length; i++) {
        let element = document.getElementById(fields[i]);
        if (i >= 0 && i < 5) {
            validateChars(element);
        } else if (i >= 5 && i < 8) {
            validate(element);
        } else if (i === 8 || i === 9) {
            validatePhone(element);
        } else if (i === 10) {
            validateSSN(element);
        } else {
            validateNumbers(element);
        }
    }
}

//Submit Mortgage===============================================
function formMortgageSubmit() {
    /*let fields = [
        "FirstName", "LastName","State", "Country", "Email", "SSN"
    ];*/
    let fields = ["MortgageName", "MortgageAmount", "Option"];
    validateChars(document.getElementById("MortgageName"));
    validateNumbers(document.getElementById("MortgageAmount"));
    validate(document.getElementById("Option"));
    let clearedValidation = true;
    for (var i = 0; i < fields.length; i++) {
        if (formFields[fields[i]] === false) {
            clearedValidation = false;
        }
    }
    if (clearedValidation) {
        //Ajax call
        document.getElementById("SubmitValidate").innerHTML = "";
        let mortgageObj = { ContactID: contactID };
        for (let i = 0; i < fields.length; i++) {
            switch (fields[i]) {
                case "MortgageAmount":
                    mortgageObj[fields[i]] = parseFloat(formFields[fields[i]]);
                    break;
                case "Option":
                    mortgageObj[fields[i]] = parseInt(formFields[fields[i]]);
                    break;
                default:
                    mortgageObj[fields[i]] = formFields[fields[i]];
            }
        }
        var xmlHttp = new XMLHttpRequest();
        xmlHttp.open("POST", "http://team3webapi.azurewebsites.net/api/mortgage", true);
        xmlHttp.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
        xmlHttp.send(JSON.stringify(mortgageObj));
        xmlHttp.onreadystatechange = function () {
            if (xmlHttp.readyState === 4 && xmlHttp.status === 200) {
                mortgageResult = this.responseText;

            }
        };

        document.getElementById("formDocuments").classList.remove("hiddenForm");
        document.getElementById("formMortgage").classList.add("fadeOutNext");
    } else {
        document.getElementById("SubmitValidate").innerHTML = "There are errors on the form.";
    }
}
