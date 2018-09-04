// JavaScript source code


function AccountLoad() {

    alert(Xrm.Page.getAttribute("name").getValue());
}

function ValidatePhone() {
    var type = Xrm.Page.ui.getFormType();
    if (type === 1 || type === 2) {
        var validation = /^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]\d{3}[\s.-]\d{4}$/;
        var phoneNumber = Xrm.Page.getAttribute("telephone1");
        if (!validation.test(phoneNumber.getValue())) {
            Xrm.Page.getControl('telephone1').setNotification("Please Enter a proper number.", "errorMsg");
            Xrm.Page.ui.setFormNotification("Guidelines for filling phone number.", "WARNING", "helloMsg");
        } else {
            Xrm.Page.getControl('telephone1').clearNotification("errorMsg");

            Xrm.Page.ui.clearNotification("helloMsg");
        }
    }
}