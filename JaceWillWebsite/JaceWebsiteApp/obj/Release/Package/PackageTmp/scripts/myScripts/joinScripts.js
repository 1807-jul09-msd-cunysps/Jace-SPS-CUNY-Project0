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
    var results;
    console.log(validateNumbersCheck(element));
    console.log(validateSymbolsCheck(element));
    if ((validateNumbersCheck(element) && !validateSymbolsCheck(element)) || element.value.length == 0) {
        text = "";
        element.classList.remove("is-invalid");
        results = (element.value.length != 0) ? true : false;
    } else {
        element.classList.add("is-invalid");
        text = "Only numeric values are allowed in this field.";
        results = false;
    }
    document.getElementById(element.id + "Validate").innerHTML = text;
    return results;
}
function validateChars(element) {
    //debugger;
    var results;
    console.log("numbers: " + validateCharsCheck(element));
    console.log("symbols: " + validateSymbolsCheck(element));
    if (validateCharsCheck(element) && !validateSymbolsCheck(element) || element.value.length == 0) {
        text = "";
        element.classList.remove("is-invalid");
        results = true;
    }else {
        element.classList.add("is-invalid");
        text = "Numeric values and Symbols are not allowed in this field.";
        results = false;
    }
    document.getElementById(element.id + "Validate").innerHTML = text;
    return results;
}

function validateZip(element) {
    if (validateNumbers(element)) {
        zipCodeApi(element)
    }
}

function zipCodeApi(element) {
    var url = "http://api.zippopotam.us/US/" + element.value;
    var ajaxRequest = new XMLHttpRequest;
    ajaxRequest.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            try {
                var response = this.responseText;
                console.log(response);
                if (response != "" || response != undefined) {
                    var jsonObj = JSON.parse(response);
                    document.getElementById("city").value = jsonObj.places[0]["place name"];
                    document.getElementById("state").value = jsonObj.places[0]["state"];
                    document.getElementById("country").value = jsonObj.country;
                }
            } catch (Exception) {
                console.log("ERROR AJAX Request: " + Exception);
            }
        } else if (this.readyState == 4 && this.status == 404) {
            console.log("ERROR AJAX REQUEST: Zip Code entered returned no results.");
        }
    }
    ajaxRequest.open("GET", url, true);
    ajaxRequest.send();
}

function getContacts() {
    
    var ajaxRequest = new XMLHttpRequest();
    ajaxRequest.onreadystatechange = () => {
        if (this.readyState == 4 && this.status == 200) {
            alert(JSON.parse(response.text));
        }
    }
    ajaxRequest.open("GET", "http://jacewebapi.azurewebsites.net/api/Contact", true);
    ajaxRequest.send();
}

function postContact() {
    var id = ["firstName", "lastName", "email", "age", "phone", "zip", "address1", "city", "state", "cCode", "country"];
    for (var i in id) {
        if (document.getElementById(id[i]).value == "") {
            return;
        }
    }
    let contact = {
        "FName": document.getElementById("firstName").value,
        "LName": document.getElementById("lastName").value,
        "Gender": gender,
        "Email": document.getElementById("email").value,
        "AddressID": 0,
        "ContactId": 0,
        "Age": document.getElementById("age").value,
        "PhoneNumber": document.getElementById("phone").value,
        "Address": {
            "AddressID": 0,
            "CountryID": 0,
            "ZipCode": document.getElementById("zip").value,
            "AddressST": document.getElementById("address1").value + " " + document.getElementById("address2").value,
            "City": document.getElementById("city").value,
            "State": document.getElementById("state").value,
            "Country": {
                "CountryID": 0,
                "CountryCode": document.getElementById("cCode").value,
                "CountryName": document.getElementById("country").value
            }
        }
    };
    $.ajax({
        type: "Post",
        url: "http://jacewebapi.azurewebsites.net/api/Contact",
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify(contact),
        success: function (response) {
            alert(response);
        },
        error: function (response) {
            console.log(response);
        }
    });
}
