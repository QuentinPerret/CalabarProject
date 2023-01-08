//Control the asp controller depending on the radio box
document.querySelector("form").addEventListener("submit", (e) => {
    var form = document.getElementById('formId')
    if (document.getElementById('boolAssoTrueId').checked) {
        form.action = "/Collaborateur/FinalInformation"
    }
    else {
        form.action = "/UtilisateurCommun/FinalInformation"
    }
})
