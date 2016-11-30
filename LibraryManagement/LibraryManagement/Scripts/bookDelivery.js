var idStudent;


function findLivre() {

    $('#book tr:gt(0)').remove()

    var critere = document.getElementById('BookInfo').value;

    var serviceURL = '/BookDelivery/SearchBooks';

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

            if (data.length != 0)
            {
                $.each(data, function (index, value) {
                    $('#book tr:last').after('<tr id="book' + index  + '"onClick="showBook(' + this.IDBooksCopy + ')"><td>' + this.noISBN + '</td><td>' + this.Title + '</td><td>' + this.AuthorName + '</td><td>' + this.FirstName + ' ' + this.LastName + '</td><td>' + this.Description + '</td><td>' + this.price + ' $</td><td>' + this.CoopName + '</td></tr>');
                });
                $("table").addClass("show");
            } else {
               alert("Aucun exemplaire ne correspond à vos critères de recherches")

            }
        }

        function errorFunc() {
            alert("La recherche n'a trouvé aucun résultat correspondant.");
        }
}

function showBook(id) {
    
    var serviceURL = '/BookDelivery/getBookCopy';

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
        $("table").removeClass("show");
        $(".researchForm").addClass("hide");
        $(".inputForm").addClass("show");
        idStudent = data.IDStudent;
        $('#noISBN').val(data.noISBN);
        $('#title').val(data.Title);
        $('#author').val(data.AuthorName);
        $('#student').val(data.FirstName + " " + data.LastName);
        $('#price').val(data.price);
        $('#IDBookState').val(data.IDBookState);
        $('#IDBookCopy').text(data.IDBooksCopy);
        $('#coop_Name').val(data.CoopName);
    }

    function errorFunc() {
        alert("La recherche n'a trouvé aucun résultat correspondant.");
    }
}

function cancel() {
    $(".researchForm").removeClass("hide");
    $(".inputForm").removeClass("show");
    $("#BookInfo").val("");
}

function deposit() {

    var BookDelivery = {}

    var IDBookCopy = $("#IDBookCopy").html();
    var IDBookState = $("#IDBookState").val();

    $.ajax({
        type: "POST",
        url: '/BookDelivery/DepositBook',
        contentType: "application/json; charset=utf-8",
        data: '{"IDBooksCopy":"' + IDBookCopy + '","IDBookState":"' + IDBookState + '"}',
        dataType: "json",
        success: function () {
            alert('Le livre a bien été déposé');
            cancel();
        },
        error: function () {
            alert('Erreur');
        },
    });

}

function finDepot() {

    if (idStudent == null) {
        alert("Vous devez avoir déposé au moins un livre avant de pouvoir finir la transaction");
    } else {
        $.ajax({
            type: "POST",
            url: '/BookDelivery/SendMail',
            contentType: "application/json; charset=utf-8",
            data: '{"IDStudent":"' + idStudent + '"}',
            dataType: "json",
            success: function () {
                alert('La confirmation par courriel a bien été envoyé');
                cancel();
            },
            error: function () {
                alert('Erreur');
            },
        });
    }
}
