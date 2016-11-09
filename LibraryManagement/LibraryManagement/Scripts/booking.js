
function findLivre() {

    $('#book tr:gt(0)').remove()

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
            $('#book tr:last').after('<tr onClick="showBook(' + this.IDBooksCopy + ')"><td>' + this.noISBN + '</td><td>' + this.Title + '</td><td>' + this.Name + '</td><td>' + this.FirstName + ' ' + this.LastName + '</td><td>' + this.Description + '</td><td>' + this.price + ' $</td></tr>');
        });

        $("table").addClass("show");
    }

    function errorFunc() {
        alert("La recherche n'a trouvé aucun résultat correspondant.");
    }
}

function showBook(id) {

    var serviceURL = '/Booking/getBookCopy';

    $.ajax({
        type: "POST",
        url: serviceURL,
        data: '{"id":"' + id + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: successFunc,
        error: errorFunc
    });

    function successFunc(data, status) {
        var answer = confirm("Voulez-vous vraiment effectuer la cette réservation? \n\n" +
        "Titre:              " + data.Title +"\n" +
        "Auteur:             " + data.Name + "\n" +
        "Étudiant:           " + data.FirstName + " " + data.LastName + "\n" +
        "Prix:               " + data.price + "\n" +
        "État:               " + data.IDBookState + "\n");

        if (answer) {
            var serviceURL = '/Booking/CreateBooking';

            $.ajax({
                type: "POST",
                url: serviceURL,
                data: '{"id":"' + id + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: successFunc,
                error: errorFunc
            });

            return true;
        }
        else {
            return false;
        }
    }

    function errorFunc() {
        alert("La recherche n'a trouvé aucun résultat correspondant.");
    }
}
