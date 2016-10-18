
function findLivre() {
    var numLivre = document.getElementById('NumLivre').value;

    ///TODO Faire validation si le numéro de livre est valide avant de faire le call ajax
    var serviceURL = '/Livres/SearchBooks';

        $.ajax({
            type: "POST",
            url: serviceURL,
            data: '{"number":' + numLivre + '}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: successFunc,
            error: errorFunc
        });

        function successFunc(data, status) {
            alert("succès")
        }

        function errorFunc() {
            alert('error');
        }
}