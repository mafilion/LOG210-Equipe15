
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
            document.getElementById('livre_noISBN').value = data.noISBN;
            document.getElementById('livre_Titre').value = data.Titre;
            document.getElementById('livre_nbPages').value = data.nbPages;
            document.getElementById('livre_prix').value = data.prix;
        }

        function errorFunc() {
            alert('error');
        }
}

function sendLivre() {
       var livre = {};
         
        livre.noISBN = document.getElementById('livre_noISBN').value
        livre.Titre = document.getElementById('livre_Titre').value;
        livre.nbPages = document.getElementById('livre_nbPages').value;
        livre.prix = document.getElementById('livre_prix').value;
        
        
        //Trouvé comment envoyé les data dynamiquement(Pour les auteurs peut être envoyé une liste ?!?!?!?
        $.ajax({
            type: "POST",
            url: '/Livres/CreateBook',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(livre),
            dataType: "json",
            success: function () {
                alert('Success');
            },
            error: function () {
                alert('Erreur');
            },
        });


}
