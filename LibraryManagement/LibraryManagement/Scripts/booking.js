
function findLivre() {
    var critere = document.getElementById('BookInfo').value;

    var serviceURL = '/Booking/SearchBooks';

        $.ajax({
            type: "POST",
            url: serviceURL,
            data: '{"value":"' + critere + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: successFunc,
            error: errorFunc
        });

        function successFunc(data, status) {

            $.each(data, function (index, value) {
                $('#book tr:last').after('<tr onClick="showBook(' + this.IDBooksCopy + ')"><td>' + this.noISBN + '</td><td>' + this.Title + '</td><td>' + this.Name + '</td><td>' + this.FirstName + ' ' + this.LastName + '</td><td>' + this.Description + '</td><td>' + this.PricePercentage + '</td></tr>');
            });

            $("table").addClass("show");
        }

        function errorFunc() {
            alert("La recherche n'a trouvé aucun résultat correspondant.");
        }
}

function isValidForm() {
    var answer = confirm("Voulez-vous vraiment enregistrer cette réservation? \n\n" +
        "Titre:             " + this.Title+ "\n" +
        "Nom:                " + this.Name + "\n")
    if (answer) {
        return true;
    }
    else {
        return false;
    }
}

function showBook(id) {
    alert(id);
}
