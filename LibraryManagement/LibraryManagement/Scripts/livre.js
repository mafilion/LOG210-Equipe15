
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
            if (document.getElementById('livre_noISBN').value != "" || document.getElementById('livre_Titre').value != "" || document.getElementById('livre_nbPages').value != "" || document.getElementById('livre_prix').value != "" || document.getElementById('AuteurNom1').value != "")
            {
                if (confirm("Des informations d'un livre sont déjà entré. Voulez-vous quand même mettre à jour les champs?"))
                {
                    UpdateFormContent(data);
                }
            }
            else
            {
                UpdateFormContent(data);
            }
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

function UpdateFormContent(data)
{
    document.getElementById('livre_noISBN').value = data.livre.noISBN;
    document.getElementById('livre_Titre').value = data.livre.Titre;
    document.getElementById('livre_nbPages').value = data.livre.nbPages;
    document.getElementById('livre_prix').value = data.livre.prix;
    //Auteur (Dynamique)
    //document.getElementById('AuteurNom1').value = data.ListAuteur.get(0); 
}
