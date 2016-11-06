
function isValidForm() {
    var answer = confirm("Voulez-vous vraiment enregistrer ce gestionnaire? \n\n" +
        "Prénom:             " + document.getElementById("manager_FirstName").value + "\n" +
        "Nom:                " + document.getElementById("manager_LastName").value + "\n" +
        "Courriel:           " + document.getElementById("manager_Email").value + "\n\n" +
        "Information coopérative \n\n" +
        "Nom:                " + document.getElementById("cooperative_Name").value + "\n" +
        "Numéro de rue:      " + document.getElementById("cooperative_NoStreet").value + "\n" +
        "Rue:                " + document.getElementById("cooperative_Street").value + "\n" +
        "Ville:              " + document.getElementById("cooperative_City").value + "\n" +
        "Code Postal         " + document.getElementById("cooperative_PostalCode").value + "\n\n" +
        "Si le nom de la coopérative existe déjà, alors ses infos seront mis à jour")
    if (answer) {
        return true;
    }
    else {
        return false;
    }
}