function isValidForm() {
    var answer = confirm("Confirmation de la présence de l'étudiant \n\n" +
        "Prénom: " + document.getElementById("student_FirstName").value + "\n" +
        "Nom:    " + document.getElementById("student_LastName").value)
    if (answer) {
        return true;
    }
    else {
        return false;
    }
}