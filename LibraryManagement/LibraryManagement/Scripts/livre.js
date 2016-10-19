
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
            alert("La recherche n'a trouvé aucun résultat correspondant.");
        }
}

function sendLivre() {
    var livreAut = {
        livre: {},
        Aut: {}
    }
         
        livreAut.livre.noISBN = document.getElementById('livre_noISBN').value
        livreAut.livre.Titre = document.getElementById('livre_Titre').value;
        livreAut.livre.nbPages = document.getElementById('livre_nbPages').value;
        livreAut.livre.prix = document.getElementById('livre_prix').value;
        livreAut.livre.IDEtatLivre = document.getElementById('IDEtatLivre').value;
        livreAut.Aut.Name = document.getElementById('AuteurNom1').value;

    //Auteur Optionnel
    /*
        if (document.getElementById('AuteurNom2').value != "")
        {
            livre.AuteurSecondaire1 = document.getElementById('AuteurNom2').value;
        }

        if (document.getElementById('AuteurNom3').value != "") {
            livre.AuteurSecondaire2 = document.getElementById('AuteurNom3').value;
        }

        if (document.getElementById('AuteurNom4').value != "") {
            livre.AuteurSecondaire3 = document.getElementById('AuteurNom4').value;
        }

        if (document.getElementById('AuteurNom5').value != "") {
            livre.AuteurSecondaire4 = document.getElementById('AuteurNom5').value;
        }
     */
 
        $.ajax({
            type: "POST",
            url: '/Livres/CreateBook',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(livreAut),
            dataType: "json",
            success: function () {
                CleanForm();
                alert('Le livre à été ajouté avec succès');
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
    document.getElementById('AuteurNom1').value = data.Aut.Name; //Temporaire 
    //for (var i = 0, l = data.ListAuteur.length; i < l; i++) {
    //    document.getElementById('AuteurNom' + (i + 1)).value = data.ListAuteur[i];
    //}
    //Auteur (Dynamique)
    //document.getElementById('AuteurNom1').value = data.ListAuteur.get(0); 
}

function CleanForm()
{
    document.getElementById('NumLivre').value = "";
    document.getElementById('livre_noISBN').value = "";
    document.getElementById('livre_Titre').value = "";
    document.getElementById('livre_nbPages').value = "";
    document.getElementById('livre_prix').value = "";
    document.getElementById('AuteurNom1').value = "";
    document.getElementById('AuteurNom2').value = "";
    document.getElementById('AuteurNom3').value = "";
    document.getElementById('AuteurNom4').value = "";
    document.getElementById('AuteurNom5').value = "";
    document.getElementById('IDEtatLivre').value = 1;
}
