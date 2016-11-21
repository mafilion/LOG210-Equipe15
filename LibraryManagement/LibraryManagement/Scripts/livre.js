
function findLivre() {
    var numLivre = document.getElementById('BookNumber').value;

    ///TODO Faire validation si le numéro de livre est valide avant de faire le call ajax
    var serviceURL = '/Books/SearchBooks';

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
            if (document.getElementById('book_noISBN').value != "" || document.getElementById('book_Title').value != "" || document.getElementById('book_nbPages').value != "" || document.getElementById('book_price').value != "" || document.getElementById('AuthorName1').value != "")
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
    var BookAut = {
        Book: {},
        Aut: {},
        BookState: {},
        coop: {},
    }
         
        BookAut.Book.noISBN = document.getElementById('book_noISBN').value
        BookAut.Book.Title = document.getElementById('book_Title').value;
        BookAut.Book.nbPages = document.getElementById('book_nbPages').value;
        BookAut.Book.price = document.getElementById('book_price').value;
        BookAut.BookState.IDBookState = document.getElementById('IDBookState').value;
        BookAut.coop.IDCooperative = document.getElementById('IDCooperative').value;
        BookAut.Aut.Name = document.getElementById('AuthorName1').value;
 
        $.ajax({
            type: "POST",
            url: '/Books/CreateBook',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(BookAut),
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
    document.getElementById('book_noISBN').value = data.book.noISBN;
    document.getElementById('book_Title').value = data.book.Title;
    document.getElementById('book_nbPages').value = data.book.nbPages;
    document.getElementById('book_price').value = data.book.price;
    document.getElementById('AuthorName1').value = data.Aut.Name; //Temporaire 
    //for (var i = 0, l = data.ListAuteur.length; i < l; i++) {
    //    document.getElementById('AuteurNom' + (i + 1)).value = data.ListAuteur[i];
    //}
    //Auteur (Dynamique)
    //document.getElementById('AuteurNom1').value = data.ListAuteur.get(0); 
}

function CleanForm()
{
    document.getElementById('BookNumber').value = "";
    document.getElementById('book_noISBN').value = "";
    document.getElementById('book_Title').value = "";
    document.getElementById('book_nbPages').value = "";
    document.getElementById('book_price').value = "";
    document.getElementById('AuthorName1').value = "";
    document.getElementById('AuthorName2').value = "";
    document.getElementById('AuthorName3').value = "";
    document.getElementById('AuthorName4').value = "";
    document.getElementById('AuthorName5').value = "";
    document.getElementById('IDBookState').value = 1;
    document.getElementById('IDCooperative').value = 1;
}
