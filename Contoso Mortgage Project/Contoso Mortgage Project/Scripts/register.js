
var formFields = {
    FirstName: false, LastName: false, Email: false, SSN: false, BusinessPhone: false, MobilePhone: false, AddressLine1: false, AddressLine2: false, City: false, State: false, Country: false, ZipCode: false
};

function submitForm() {
    let fields = [
        "FirstName", "LastName", "Email", "SSN", "BusinessPhone", "MobilePhone", "AddressLine1", "AddressLine2", "City", "State", "Country", "ZipCode"
    ];
    let clearedValidation = true;
    for (var i = 0; i < fields.length; i++) {
        if (formFields[fields[i]] === false) {
            clearedValidation = false;
        }
        if (document.getElementById(fields[i]).value.length === 0) {
            clearedValidation = false;
            formFields[i] = false;
            document.getElementById(fields[i] + "Validate").innerHTML = "This field is required.";
        }
    }
    if (clearedValidation) {
        //Ajax call
        document.getElementById("SubmitValidate").innerHTML = "";
        alert("AJAX CAll");
    } else {
        document.getElementById("SubmitValidate").innerHTML = "There are errors on the form.";
    }
}
//Form Validation=================================================

//Validate All Charcters
function validateCharsCheck(element) {
    var val = /^[a-zA-Z\s]+$/;
    return val.test(element.value) ? true : false;
}

//Validate Symbols in a string
function validateSymbolsCheck(element) {
    const val = /[~`!#$%\^&*+=\-\[\]\\';,/{}|\\":<>\?\(\)_]/;
    return val.test(element.value) ? true : false;
}
//Validate Numbers in a string
function validateNumbersCheck(element) {
    const numbers = /^\d*$/;
    return (numbers.test(element.value)) ? true : false;
}

function validateSSNCheck(element) {
    const val = /^(?:\d{3}-\d{2}-\d{4}|\d{9})$/;
    return val.test(element.value)? true : false;
}

function validatePhoneCheck(element) {
    const val = /^(?:\(\d{3}\)\d{3}-\d{4}|\d{3}-\d{3}-\d{4}|\d{10})$/;
    var result = val.test(element.value);
    return val.test(element.value) ? true : false;
}

function validateChars(element) {
    var results;
    let text = "";
    if (validateCharsCheck(element) && !validateSymbolsCheck(element) || element.value.length === 0) {
        text = "";
        element.classList.remove("is-invalid");
        results = true;
    } else {
        element.classList.add("is-invalid");
        text = "Numeric values and symbols are not allowed in this field.";
        results = false;
    }
    document.getElementById(element.id + "Validate").innerHTML = text;
    formFields[element.id] = results;
}

function validateNumbers(element) {
    var results;
    console.log(validateNumbersCheck(element));
    console.log(validateSymbolsCheck(element));
    if ((validateNumbersCheck(element) && !validateSymbolsCheck(element)) || element.value.length === 0) {
        text = "";
        element.classList.remove("is-invalid");
        results = (element.value.length !== 0) ? true : false;
    } else {
        element.classList.add("is-invalid");
        text = "Only numeric values are allowed in this field.";
        results = false;
    }
    document.getElementById(element.id + "Validate").innerHTML = text;
    return results;
}

function validateSSN(element) {
    var results;
    let text = "";
    if (validateSSNCheck(element) && !validateCharsCheck(element) || element.value.length === 0) {
        text = "";
        element.classList.remove("is-invalid");
        results = true;
    } else {
        element.classList.add("is-invalid");
        text = "Please input your 9 digit SSN, '-' are optional.";
        results = false;
    }
    document.getElementById(element.id + "Validate").innerHTML = text;
    formFields[element.id] = results;
}
function validatePhone(element) {
    var results;
    let text = "";
    if (validatePhoneCheck(element) && !validateCharsCheck(element) || element.value.length === 0) {
        text = "";
        element.classList.remove("is-invalid");
        results = true;
    } else {
        element.classList.add("is-invalid");
        text = "Please input your phone number digit SSN '()' and '-' are optional.";
        results = false;
    }
    document.getElementById(element.id + "Validate").innerHTML = text;
    formFields[element.id] = results;
}