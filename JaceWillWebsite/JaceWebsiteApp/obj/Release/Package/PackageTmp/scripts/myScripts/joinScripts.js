var gender = "male";

function permAddress(element) {
    try {
        var target = document.getElementById("permTarget");
        if (element.value == 1 && target.childNodes.length == 0) {
            //var list = ["Address Line 1", "Address Line 2", "City", "State", "Country", ]
            var fieldset = document.getElementById("addressFS");
            var newSet = fieldset.cloneNode(true);
            console.log("newSet: ", newSet);
            newSet.childNodes[1].innerHTML = "Permanent Address"
            target.appendChild(newSet);
        } else if (target.childNodes.length == 1){
            target.removeChild(permTarget.childNodes[0]);
        }
    } catch (e) {
        console.log("Error: ", e);
    }   
}

function genderSelect(element) {
    //debugger;
    let sel, desel;
    if (element.value == "male") {
        gender = "male";
        sel = "maleBtn";
        desel = "femaleBtn";
    } else {
        gender = "female";
        desel = "maleBtn";
        sel = "femaleBtn";
    }
    var selElement = document.getElementById(sel);
    var deselElemnt = document.getElementById(desel);
    selElement.classList.add("btn-dark");
    deselElemnt.classList.remove("btn-dark")
}

function validateFLNames(elementID1, elementID2) {
    //debugger;
    let element1 = document.getElementById(elementID1);
    let element2 = document.getElementById(elementID2);
    var text = "";

    if (element1.value == element2.value && element1.value.length != 0) {
        text = "First and last names cannot be the same."
        element1.classList.add("is-invalid");
    } else if (validateCharsCheck(element1) || element1.value.length == 0 ) {
        text = "";
        element1.classList.remove("is-invalid");
    } else {
        element1.classList.add("is-invalid");
        text = "Numeric values and Symbols are not allowed in this field.";
    }
    document.getElementById(elementID1 + "Validate").innerHTML = text;
}

function validateSymbolsCheck(element) {
    var symbols = /[~`!#$%\^&*+=\-\[\]\\';,/{}|\\":<>\?\(\)_]/;
    return (symbols.test(element.value)) ? true : false;
}

function validateNumbersCheck(element) {
    var numbers = /^\d*$/;
    return (numbers.test(element.value)) ? true : false;
}

function validateCharsCheck(element) {
    var chars = /^[a-zA-Z\s]+$/;
    return (chars.test(element.value)) ? true : false;
}

function validateNumbers(element) {
    console.log(validateNumbersCheck(element));
    console.log(validateSymbolsCheck(element));
    if ((validateNumbersCheck(element) && !validateSymbolsCheck(element)) || element.value.length == 0) {
        text = "";
        element.classList.remove("is-invalid");
    } else {
        element.classList.add("is-invalid");
        text = "Only numeric values are allowed in this field.";
    }
    document.getElementById(element.id + "Validate").innerHTML = text;
}
function validateChars(element) {
    //debugger;
    console.log("numbers: " + validateCharsCheck(element));
    console.log("symbols: " + validateSymbolsCheck(element));
    if (validateCharsCheck(element) && !validateSymbolsCheck(element) || element.value.length == 0) {
        text = "";
        element.classList.remove("is-invalid");
    }else {
        element.classList.add("is-invalid");
        text = "Numeric values and Symbols are not allowed in this field.";
    }
    document.getElementById(element.id + "Validate").innerHTML = text;
}