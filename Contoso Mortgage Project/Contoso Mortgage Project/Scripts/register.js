
var formFields = {
    FirstName: false, LastName: false, Email: false, SSN: false, BusinessPhone: false, MobilePhone: false, AddressLine1: false, AddressLine2: false, City: false, State: false, Country: false, ZipCode: false
};

function submitForm() {
    let fields = [
        "FirstName", "LastName", "City", "State", "Country", "Email", "AddressLine1", "AddressLine2", "MobilePhone", "BusinessPhone", "SSN", "ZipCode"
    ];
    validateAllFields(fields);
    let clearedValidation = true;
    for (var i = 0; i < fields.length; i++) {
        if (formFields[fields[i]] === false) {
            clearedValidation = false;
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
function validateAllFields(fields) {
    for (var i = 0; i < fields.length; i++) {
        let element = document.getElementById(fields[i]);
        if (i >= 0 && i < 5) {
            validateChars(element);
        } else if (i >= 5 && i < 8) {
            validate(element);
        } else if (i == 8 || i == 9) {
            validatePhone(element);
        } else if (i == 10) {
            validateSSN(element);
        } else {
            validateNumbers(element);
        }
    }
}


//Validate All Charcters
function validateCharsCheck(element) {
    var val = /^[a-zA-Z\s]+$/;
    let results = val.test(element.value) ? true : false;
    return results;
}

//Validate Symbols in a string
function validateSymbolsCheck(element) {
    const val = /[~`!#$%\^&*+=\-\[\]\\';,/{}|\\":<>\?\(\)_]/;
    let results = val.test(element.value) ? true : false;
    return results;
}
//Validate Numbers in a string
function validateNumbersCheck(element) {
    const val = /^\d*$/;
    let results = val.test(element.value) ? true : false;
    return results;
}

function validateSSNCheck(element) {
    const val = /^(?:\d{3}-\d{2}-\d{4}|\d{9})$/;
    let results = val.test(element.value) ? true : false;
    return results;
}

function validatePhoneCheck(element) {
    const val = /^(?:\(\d{3}\)\d{3}-\d{4}|\d{3}-\d{3}-\d{4}|\d{10})$/;
    let results = val.test(element.value) ? true : false;
    return results;
}

function validate(element) {
    let text = "";
    if (element.value.length == 0) {
        element.classList.add("is-invalid");
        text = "This field is required.";
        formFields[element.id] = false;
    } else {
        element.classList.remove("is-invalid");
        formFields[element.id] = true;
    }
    document.getElementById(element.id + "Validate").innerHTML = text;
}

function validateChars(element) {
    let results;
    let text = "";
    if (validateCharsCheck(element) && !validateSymbolsCheck(element)) {
        text = "";
        element.classList.remove("is-invalid");
        results = true;
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

function validateNumbers(element) {
    let results;
    let text = "";
    if ((validateNumbersCheck(element) && !validateSymbolsCheck(element)) && element.value.length !== 0) {
        text = "";
        element.classList.remove("is-invalid");
        results = true;
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

function validateSSN(element) {
    let results;
    let text = "";
    if (validateSSNCheck(element) && !validateCharsCheck(element)) {
        text = "";
        element.classList.remove("is-invalid");
        results = true
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
function validatePhone(element) {
    var results;
    let text = "";
    if (validatePhoneCheck(element) && !validateCharsCheck(element)) {
        text = "";
        element.classList.remove("is-invalid");
        results = (element.value.length !== 0) ? true : false;
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