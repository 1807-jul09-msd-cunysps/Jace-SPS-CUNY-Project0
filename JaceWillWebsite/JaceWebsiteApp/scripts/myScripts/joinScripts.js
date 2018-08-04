function checkVal(element) {
    try {
        var target = document.getElementById("permTarget");
        if (element.value == "no" && target.childNodes.length == 0) {
            //var list = ["Address Line 1", "Address Line 2", "City", "State", "Country", ]
            var fieldset = document.getElementById("addressSet");
            var newSet = fieldset.cloneNode(true);
            for (var i in newSet.childNodes.length) {
                console.log(newSet.childNodes[i].value);
            }

            var newFieldset = document.createElement("FIELDSET");
            var legend = document.createElement("LEGEND");
            legend.innerHTML = "Permanent Address";

            var breakLine = document.createElement("BR");
            var breakLine1 = document.createElement("BR");
            var breakLine2 = document.createElement("BR");
            var breakLine3 = document.createElement("BR");
            var breakLine4 = document.createElement("BR");
            var breakLine5 = document.createElement("BR");
            var breakLine6 = document.createElement("BR");
            var breakLine7 = document.createElement("BR");
            var breakLine8 = document.createElement("BR");
            var breakLine9 = document.createElement("BR");
            var breakLine10 = document.createElement("BR");
            var breakLine11 = document.createElement("BR");

            var add1Label = document.createElement("LEBEL");
            add1Label.innerHTML = "Address Line 1";
            var add1Input = document.createElement("INPUT");
            var add2Label = document.createElement("LEBEL");
            add2Label.innerHTML = "Address Line 2";
            var add2Input = document.createElement("INPUT");
            
            var cityLabel = document.createElement("LEBEL");
            cityLabel.innerHTML = "City";
            var cityInput = document.createElement("INPUT");

            var stateLabel = document.createElement("LEBEL");
            stateLabel.innerHTML = "State";
            var stateInput = document.createElement("INPUT");


            var countryLabel = document.createElement("LEBEL");
            countryLabel.innerHTML = "Country";
            var countryInput = document.createElement("INPUT");

            var zipLabel = document.createElement("LEBEL");
            zipLabel.innerHTML = "Zipcode";
            var zipInput = document.createElement("INPUT");
            debugger;

            newFieldset.appendChild(legend);
            newFieldset.appendChild(add1Label);
            newFieldset.appendChild(breakLine);
            newFieldset.appendChild(add1Input);
            newFieldset.appendChild(breakLine2);
            
            newFieldset.appendChild(add2Label);
            newFieldset.appendChild(breakLine3);
            newFieldset.appendChild(add2Input);
            newFieldset.appendChild(breakLine4);

            newFieldset.appendChild(cityLabel);
            newFieldset.appendChild(breakLine5);
            newFieldset.appendChild(cityInput);
            newFieldset.appendChild(breakLine6);

            newFieldset.appendChild(stateLabel);
            newFieldset.appendChild(breakLine7);
            newFieldset.appendChild(stateInput);
            newFieldset.appendChild(breakLine8);

            newFieldset.appendChild(countryLabel);
            newFieldset.appendChild(breakLine9);
            newFieldset.appendChild(countryInput);
            newFieldset.appendChild(breakLine10);

            newFieldset.appendChild(zipLabel);
            newFieldset.appendChild(breakLine11);
            newFieldset.appendChild(zipInput);

            target.appendChild(newFieldset);
        } else if (target.childNodes.length == 1){
            target.removeChild(permTarget.childNodes[0]);
        }
    } catch (e) {
        console.log("Error: ", e);
    }   
}

function validateFLNames(elementID1, elementID2) {
    //debugger;
    let element1 = document.getElementById(elementID1);
    let element2 = document.getElementById(elementID2);
    var text = "";

    if (element1.value == element2.value && element1.value.length != 0) {
        text = "First and last names cannot be the same."
        element1.classList.add("is-invalid");
    }else if (/^[a-zA-Z]+$/.test(element1.value) || element1.value.length == 0 ) {
        text = "";
        element1.classList.remove("is-invalid");
    } else {
        element1.classList.add("is-invalid");
        text = "Please only enter non numeric values.";
    }
    document.getElementById(elementID1 + "Validate").innerHTML = text;
}

function validateSymbols(elementID) {
    debugger;
    let element1 = document.getElementById(elementID);

    if (/[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]+/.test(element1.value) || element1.value.length == 0) {
        text = "";
        element1.classList.remove("is-invalid");
    } else {
        element1.classList.add("is-invalid");
        text = "Please do not enter symbol values.";
    }
    document.getElementById(elementID + "Validate").innerHTML = text;
}

function validateNumerics(elementID) {
    var x, text;
    x = document.getElementById(elementID).value;
    if (!isNaN(x) && x != "") {
        text = "Please only enter non numeric values.";
    } else{
        text = "";
    }
    document.getElementById(elementID + "Validate").innerHTML = text;
}