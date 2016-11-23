/* Table Configuration de l'application */
IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_NAME = 'Settings'))
BEGIN
    CREATE TABLE Settings
	(
		IDSettings INT PRIMARY KEY IDENTITY(1,1),
		IDLanguage INT not null DEFAULT 1,
		SendSMS INT not null DEFAULT 0
	);

	INSERT INTO Settings (IDLanguage) VALUES (1)
END


/* Table pour Compte étudiant */
IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_NAME = 'Student'))
BEGIN
    CREATE TABLE Student
	(
		IDStudent INT PRIMARY KEY IDENTITY(1,1) ,
		FirstName VARCHAR(100) NOT NULL,
		LastName VARCHAR (100) NOT NULL,
		Email VARCHAR(150) NOT NULL,
		PhoneNumber VARCHAR(12),
		StudentPassword VARCHAR(1000) NOT NULL,
	);
END

/*Table pour la coopérative*/
IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_NAME = 'Cooperative'))
BEGIN
	CREATE TABLE Cooperative
	(
		IDCooperative INT PRIMARY KEY IDENTITY(1,1),
		Name VARCHAR(100) NOT NULL,
		NoStreet VARCHAR(10),
		Street VARCHAR(100),
		PostalCode VARCHAR(10),
		City VARCHAR(100)
	);

END

/*Table pour la Manager */
IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_NAME = 'Manager'))
BEGIN
	CREATE TABLE Manager(
		IDManager INT PRIMARY KEY IDENTITY(1,1),
		IDCooperative INT FOREIGN KEY REFERENCES Cooperative(IDCooperative),   
		FirstName VARCHAR(100) NOT NULL,
		LastName VARCHAR(100) NOT NULL,
		Email VARCHAR(150) NOT NULL,
		ManagerPassword VARCHAR(1000) NOT NULL
	)
END

/* Table pour les traductions dans l'application */
/* 
	IDLanguage 1 : Francais
    IDLanguage 2 : Anglais 
*/
IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_NAME = 'Resources'))
BEGIN
    CREATE TABLE Resources
	(
		IDResources INT PRIMARY KEY IDENTITY(1,1) ,
		IDLanguage int NOT NULL,
		TextName VARCHAR(100),
		Description VARCHAR (MAX) NOT NULL,
	);

	INSERT INTO Resources (IDLanguage,TextName,Description)
	VALUES(1,'Gestion Bibliotheque','Gestion Bibliotheque')

	INSERT INTO Resources (IDLanguage,TextName,Description)
	VALUES(2,'Gestion Bibliotheque','Library Management')

	INSERT INTO Resources (IDLanguage,TextName,Description)
	VALUES(1,'Nouveau Compte','Nouveau Compte')

	INSERT INTO Resources (IDLanguage,TextName,Description)
	VALUES(2,'Nouveau Compte','New Account')

	INSERT INTO Resources (IDLanguage,TextName,Description)
	VALUES(1,'Modifier','Modifier')

	INSERT INTO Resources (IDLanguage,TextName,Description)
	VALUES(2,'Modifier','Modify')

	INSERT INTO Resources (IDLanguage,TextName,Description)
	VALUES(1,'Supprimer','Supprimer')

	INSERT INTO Resources (IDLanguage,TextName,Description)
	VALUES(2,'Supprimer','Delete')

	INSERT INTO Resources (IDLanguage,TextName,Description)
	VALUES(1,'Retour','Retour à la page précédente')

	INSERT INTO Resources (IDLanguage,TextName,Description)
	VALUES(2,'Retour','Back to the last page')

	INSERT INTO Resources (IDLanguage,TextName,Description)
	VALUES(1,'Sauvegarder','Sauvegarder')

	INSERT INTO Resources (IDLanguage,TextName,Description)
	VALUES(2,'Sauvegarder','Save')

	INSERT INTO Resources (IDLanguage,TextName,Description)
	VALUES(1,'Connexion','Connexion')

	INSERT INTO Resources (IDLanguage,TextName,Description)
	VALUES(2,'Connexion','Log in')

	INSERT INTO Resources (IDLanguage,TextName,Description)
	VALUES(1,'Deconnexion','Déonnexion')

	INSERT INTO Resources (IDLanguage,TextName,Description)
	VALUES(2,'Deconnexion','Log out')

	INSERT INTO Resources (IDLanguage,TextName,Description)
	VALUES(1,'Nom', 'Nom')

	INSERT INTO Resources (IDLanguage,TextName,Description)
	VALUES(2,'Nom','Last Name')

	INSERT INTO Resources (IDLanguage,TextName,Description)
	VALUES(1,'Prenom', 'Prénom')

	INSERT INTO Resources (IDLanguage,TextName,Description)
	VALUES(2,'Prenom','First Name')

	INSERT INTO Resources (IDLanguage,TextName,Description)
	VALUES(1,'Courriel', 'Adresse courriel')

	INSERT INTO Resources (IDLanguage,TextName,Description)
	VALUES(2,'Courriel','Email Address')

	INSERT INTO Resources (IDLanguage,TextName,Description)
	VALUES(1,'Etudiant', 'Étudiant')

	INSERT INTO Resources (IDLanguage,TextName,Description)
	VALUES(2,'Etudiant','Student')

	INSERT INTO Resources (IDLanguage,TextName,Description)
	VALUES(1,'Telephone', 'Numero de téléphone')

	INSERT INTO Resources (IDLanguage,TextName,Description)
	VALUES(2,'Telephone','Phone Number')

	INSERT INTO Resources (IDLanguage,TextName,Description)
	VALUES(1,'Gestionnaire', 'Gestionnaire')

	INSERT INTO Resources (IDLanguage,TextName,Description)
	VALUES(2,'Gestionnaire','Manager')
	
	INSERT INTO Resources (IDLanguage,TextName,Description)
	VALUES(1,'MotDePasse','Mot de passe')
	
	INSERT INTO Resources (IDLanguage,TextName,Description)
	VALUES(2,'MotDePasse','Password')

	INSERT INTO Resources (IDLanguage,TextName,Description)
	VALUES(1,'Langues','EN')

	INSERT INTO Resources (IDLanguage,TextName,Description)
	VALUES(2,'Langues','FR')
	
	INSERT INTO Resources (IDLanguage,TextName,Description)
	VALUES(1,'Creer','Créer')

	INSERT INTO Resources (IDLanguage,TextName,Description)
	VALUES(2,'Creer','Create')

	INSERT INTO Resources (IDLanguage,TextName,Description)
	VALUES(1,'NoRue', 'Numéro de rue')

	INSERT INTO Resources (IDLanguage,TextName,Description)
	VALUES(2,'NoRue','Street number')
	
	INSERT INTO Resources (IDLanguage,TextName,Description)
	VALUES(1,'Rue', 'Rue')

	INSERT INTO Resources (IDLanguage,TextName,Description)
	VALUES(2,'Rue','Street')

	INSERT INTO Resources (IDLanguage,TextName,Description)
	VALUES(1,'Ville', 'Ville')

	INSERT INTO Resources (IDLanguage,TextName,Description)
	VALUES(2,'Ville','City')

	INSERT INTO Resources (IDLanguage,TextName,Description)
	VALUES(1,'CodePostal', 'Code Postal')

	INSERT INTO Resources (IDLanguage,TextName,Description)
	VALUES(2,'CodePostal','Postal Code')

	INSERT INTO Resources (IDLanguage,TextName,Description)
	VALUES(1,'CoopInfo', 'Information en rapport avec la coopérative')

	INSERT INTO Resources (IDLanguage,TextName,Description)
	VALUES(2,'CoopInfo','Cooperative information')
	
	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'ConfirmMotDePasse', 'Confirmation du mot de passe')
	
	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'ConfirmMotDePasse', 'Password Confirmation')
	
	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Les mots de passe ne correspondent pas', 'Les mots de passe ne correspondent pas')
	
	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Les mots de passe ne correspondent pas', 'The password are not the same')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Numéro de téléphone doit être sous le format: xxx-xxx-xxxx', 'Numéro de téléphone doit être sous le format: xxx-xxx-xxxx')
	
	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Numéro de téléphone doit être sous le format: xxx-xxx-xxxx', 'The telephone number have to follow the format: xxx-xxx-xxxx')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'L''adresse courriel n''est pas valide', 'L''adresse courriel n''est pas valide')
	
	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'L''adresse courriel n''est pas valide','The email is not in a valid format')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'L''adresse courriel est déjà utilisé', 'L''adresse courriel est déjà utilisé')
	
	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'L''adresse courriel est déjà utilisé','The email has already been used')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Tous les champs doivent contenir une valeur', 'Tous les champs doivent contenir une valeur')
	
	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Tous les champs doivent contenir une valeur','All field are required')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Ce numéro de téléphone est déjà utilisé','Ce numéro de téléphone est déjà utilisé')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Ce numéro de téléphone est déjà utilisé','This phone number is already being used')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Ajouter un livre','Ajouter un livre')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Ajouter un livre','Add a new book')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Nouveau livre','Nouveau Livre')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Nouveau livre','New Book')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Numero ISBN','Numéro ISBN')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Numero ISBN','ISBN Number')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Numero EAN','Numéro EAN(Code-barres)')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Numero EAN','EAN Number(Barcode)')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Numero UPC','Numéro UPC(Code-barres)')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Numero UPC','UPC Number(Barcode)')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Titre','Titre')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Titre','Title')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Nombre de pages','Nombre de pages')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Nombre de pages','Number of pages')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Prix neuf','Prix(Neuf)')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Prix neuf','Price(New)')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'État du livre','État du livre')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'État du livre','Book status')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Auteurs','Auteurs')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Auteurs','Author')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Auteur','Auteur')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Auteur','Author')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Rechercher Livre','Rechercher un Livre')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Rechercher Livre','Search a Book')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Rechercher','Rechercher')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Rechercher','Search')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Numero ISBN/EAN/UPC','Numéro ISBN/EAN/UPC')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Numero ISBN/EAN/UPC','ISBN/EAN/UPC Number')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Information du livre','Information du Livre')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Information du livre','Book Information')

		INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Principal','Principal')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Principal','Main')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Secondaire','Secondaire')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Secondaire','Secondary')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Demande de livraison','Demande de livraison')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Demande de livraison','Delivery Request')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Livraison','Livraison')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Livraison','Delivery')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Date réservation','Date réservation')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Date réservation','Booking Date')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Livrer','Livrer')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Livrer','Deliver')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Bonjour','Bonjour')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Bonjour','Hello')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Utiliser un compte étudiant pour vous connecter.','Utiliser un compte étudiant pour vous connecter.')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Utiliser un compte étudiant pour vous connecter.','Use a student account to log in')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Utiliser un compte gestionnaire pour vous connecter.','Utiliser un compte gestionnaire pour vous connecter.')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Utiliser un compte gestionnaire pour vous connecter.','Use a manager account to log in') 

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Déconnexion','Déconnexion')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Déconnexion','Logoff')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Détails Livraison','Détails Livraison')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Détails Livraison','Delivery Details')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Confirmation Transaction','Confirmation Transaction')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Confirmation Transaction','Trade Confirmed')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Attente de l''étudiant','Attente de l''étudiant')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Attente de l''étudiant','Waiting for the student')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Refusé','Refusé')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Refusé','Declined')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Accepté','Accepté')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Accepté','Accepted')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Livres réservés','Livres réservés')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Livres réservés','Booked books')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Bienvenue sur GestionBibliotheque','Bienvenue sur Gestion Bibliothèque!')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Bienvenue sur GestionBibliotheque','Welcome on Library Management!')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Veuillez vous connecter pour accéder aux fonctionnalités de Gestion Bibliothèque','Veuillez vous connecter pour accéder aux fonctionnalités de Gestion Bibliothèque')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Veuillez vous connecter pour accéder aux fonctionnalités de Gestion Bibliothèque','Log in to access the feature of Library Management')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Information page ajouter un livre','Il vous est possible d’ajouter des livres que vous souhaitez vendre ou échanger à votre coopérative. Pour ce faire, le numéro d’ISBN, d’UPC ou d’EAN vous sera nécessaire. Il est important de mentionner qu’il vous faudra aller porter votre livre directement à votre coopérative suite à cet ajout.')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Information page ajouter un livre','You would be able to add books that you want to sell or trade to your cooperative. To do it, you will need an ISBN , UPC or EAN number. It is important to say that you will need to carry your books to your cooperative following the addition of your books in the system.')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Information page Demande de livraison','Gestion des livraisons en cours. Suite à une réservation, l’étudiant se présentera à la coopérative pour recevoir la livraison. Seules les livraisons n’ayant pas été complétées entièrement seront disponibles (ainsi que les livres n’ayant pas été accepté ou refusés par l’étudiant).')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Information page Demande de livraison','Management of the deliveries. Following a booking, the student will show himself to the cooperative to receive his delivery. Only the deliveries who have not been completed entirely will be available.')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Remise de livre','Remise de livre')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Remise de livre','Book delivery')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Information page Remise de livre','Permet la confirmation de l’ajout des livres au système d’achat ou d’échange pour les autres étudiants. L’étudiant doit avoir effectué l’ajout du livre dans le système et être présent avec son ou ses livres à la coopérative pour effectuer la remise des livres.')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Information page Remise de livre','Allows the confirmation of the addition of books to the system of purchase or exchange for the other students. The student must have made the addition of the book in the system and be present with his books to the cooperative to make the delivery of books.')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Réserver un livre','Réserver un livre')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Réserver un livre','Reserve a book')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Information page Réserver un livre','Liste des livres disponible à l’achat. Il est important de préciser qu’une réservation de 48 heures sera attribuée à l’étudiant lors de la réservation. Suite à cela, l’étudiant à 48 heures pour aller récupérer le livre à la coopérative en question.')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Information page Réserver un livre','List of the books available for purchase. It is important to specify that a reservation of 48 hours will be attributed to the student during the reservation. Further to it, the student have 48 hours to go at his cooperative in question to get his book.')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'#Réservation','Numéro Réservation')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'#Réservation','Booking Number')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Livraison complétée','Livraison complétée')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Livraison complétée','Delivery completed')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Complétée','Complétée')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Complétée','Completed')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Incomplète','Incomplète')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Incomplète','Incomplete')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Recherche par numéro de réservation','Recherche par numéro de réservation')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Recherche par numéro de réservation','Search from booking number')
	
	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Réservation','Réservation')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Réservation','Booking')
	
	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'RechercheDepot','Recherche selon isbn/titre/étudiant')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'RechercheDepot','Research by ISBN/Title/Student')
	
	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'TitreDepot','Recherche exemplaire')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'TitreDepot','Research book copy')
	
	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'ConfirmDepot','Confirmation du dépot')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'ConfirmDepot','Confirmation of the copy deposit')
	
	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'finDepot','Fin de la remise du livre')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'finDepot','End of the copy deposit')
	
	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'DepotLivre','Remise du livre')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'DepotLivre','Book deposit')
	
	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Annulation','Annuler')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Annulation','Cancel')
	
	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'TitreMailDepot','Confirmation Dépot Livre')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'TitreMailDepot','Confirmation book deposit')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'SujetMailDepot','Les livres ont bien été déposés dans le système de la coopérative')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'SujetMailDepot','The books were successfully deposited into the system')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'TitreMailAnnulationLivraison','Annulation Réservation')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'TitreMailAnnulationLivraison','Booking Cancel')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'SujetAnnulationLivraison','Voici la liste des livres dont vous avez refusé la réception:')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'SujetAnnulationLivraison','Here is the list of the books that you refused to acquired in your delivery.')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'TotalRemboursement','Au total, l''annulation vous octroit un remboursement de:')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'TotalRemboursement','All in all, the cancellation gives you a refund of:')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Nouvelle réservation','Nouvelle réservation')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Nouvelle réservation','New booking')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Retour','Retour')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Retour','Back')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'SujetReservation','Voici les informations des votre réservation. Merci')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'SujetReservation','Here are your reservation details. Thanks ')
	
	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Création d''une réservation','Création d''une réservation')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Création d''une réservation','New booking')
	
	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Auteur, Titre, ISBN, UPC, etc','Auteur, Titre, ISBN, UPC, etc')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Auteur, Titre, ISBN, UPC, etc','Author, Title, ISBN, UPC , etc')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Vos réservation','Vos réservation:')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Vos réservation','Your booking:')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Création d''une réservation','Création d''une réservation')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Création d''une réservation','Create a reservation')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'ResevationComplete',' Votre réservation est complétée. Un transfert de l''exemplaire réservé à votre coopérative est nécessaire. \n Un courriel vous sera envoyé lorsque votre coopérative aura reçu votre exemplaire. \n Suite à la réception de votre exemplaire, un délai de 48H vous sera attribué.')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,' ResevationComplete ',' Thanks, your reservation is completed. A transfer under your cooperative is needed for the book’s copy you’ve choose. \n An email will be send to you when the cooperative will have it delivered. \n Then, you will have 48h to pick it up. Otherwise your reservation will be deleted.')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'ResevationCompleteNoPickup','Votre réservation est complétée. SVP passer le récupérer d''ici 48h.')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'ResevationCompleteNoPickup','Your reservation is compteted. It will be available for pickup for 48h.')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Livraison complétée','Livraison complétée')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Livraison complétée','Delivery completed')

		
END 

/*Table pour les états de livres*/
IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_NAME = 'BookState'))
BEGIN
	CREATE TABLE BookState(
		IDBookState INT PRIMARY KEY IDENTITY(1,1),
		Description VARCHAR(MAX) NOT NULL,
		PricePercentage float NOT NULL
	)
	/* Insertion des États */
	INSERT INTO BookState(Description,PricePercentage) VALUES ('Comme neuf',0.75);
	INSERT INTO BookState(Description,PricePercentage) VALUES ('Quelques pages pliés, utilisation d’un marqueur',0.50);
	INSERT INTO BookState(Description,PricePercentage) VALUES ('Livre très utilisé, pages plié, couverture endommagés',0.25);
END


/*Table pour les auteurs des livres*/
IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_NAME = 'Author'))
BEGIN
	CREATE TABLE Author(
		IDAuthor INT PRIMARY KEY IDENTITY(1,1),
		Name VARCHAR(200) NOT NULL,
	)
END


/*Table pour les livres ajouté par les étudiants */
IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_NAME = 'Book'))
BEGIN
	CREATE TABLE Book(
		IDBook INT PRIMARY KEY IDENTITY(1,1),
		noISBN VARCHAR(50),
		noEAN VARCHAR(50),
		noUPC VARCHAR(50),
		Title varchar(300) NOT NULL,
		nbPages int NOT NULL,
		price float NOT NULL
	)
END

/* Table pour la liaison Livre/Auteurs */
IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_NAME = 'BooksAuthors'))
BEGIN
	CREATE TABLE BooksAuthors(
		IDBooksAuthors INT PRIMARY KEY IDENTITY(1,1),
		IDAuthor INT FOREIGN KEY REFERENCES Author(IDAuthor) NOT NULL,
		IDBook INT FOREIGN KEY REFERENCES Book(IDBook) NOT NULL
	)
END

/* Table Exemplaire */
/* 1 Disponible , 0 Non-disponible(vendu) , -1 pas encore recu par l'étudiant */
IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_NAME = 'BooksCopy'))
BEGIN
	CREATE TABLE BooksCopy(
		IDBooksCopy INT PRIMARY KEY IDENTITY(1,1),
		IDStudent INT FOREIGN KEY REFERENCES Student(IDStudent) NOT NULL,
		IDBook INT FOREIGN KEY REFERENCES Book(IDBook) NOT NULL,
		IDBookState INT FOREIGN KEY REFERENCES BookState(IdBookState) NOT NULL,
		IDCooperative INT FOREIGN KEY REFERENCES Cooperative(IDCooperative) NOT NULL, 
		Available INT NOT NULL DEFAULT -1
	)
END

/*Table Réservation */
IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_NAME = 'Booking'))
BEGIN
	CREATE TABLE Booking(
		IDBooking INT PRIMARY KEY IDENTITY(1,1),
		IDStudent INT FOREIGN KEY REFERENCES Student(IDStudent) NOT NULL,
		IDManager INT FOREIGN KEY REFERENCES Manager(IDManager) NULL,
		IDCooperative INT FOREIGN KEY REFERENCES Cooperative(IDCooperative) NOT NULL, 
		BookingDate DATETIME NOT NULL,
		TradeConfirmation BIT NOT NULL
	)
END


/*Table RéservationLigne */
/*-1:Pendant la période de 48 heures, 1: Transaction Confirmé, 2: Transaction Annulé(limite dépassé ou l'étudiant ne veut plus le livre). */
IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_NAME = 'BookingLine'))
BEGIN
	CREATE TABLE BookingLine(
		IDBookingLine INT PRIMARY KEY IDENTITY(1,1),
		IDBooking INT FOREIGN KEY REFERENCES Booking(IDBooking) NOT NULL,
		IDBooksCopy INT FOREIGN KEY REFERENCES BooksCopy(IDBooksCopy) NOT NULL,
		BookingState INT NOT NULL DEFAULT -1,
	)
END

/* Table TransfertExemplaire */
IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_NAME = 'BooksCopyTransfer'))
BEGIN
	CREATE TABLE BooksCopyTransfer(
		IDBooksCopyTransfer INT PRIMARY KEY IDENTITY(1,1),
		TransferDate DateTime NULL,
		TransferConfirmation bit NOT NULL DEFAULT 0,
	)
END

/* Table LigneTransfertExemplaire */
IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_NAME = 'BooksCopyTransferLine'))
BEGIN
	CREATE TABLE BooksCopyTransferLine(
		IDBooksCopyTransferLine INT PRIMARY KEY IDENTITY(1,1),
		IDBooksCopyTransfer INT FOREIGN KEY REFERENCES BooksCopyTransfer(IDBooksCopyTransfer) NOT NULL,
		IDBooksCopy INT FOREIGN KEY REFERENCES BooksCopy(IDBooksCopy) NOT NULL,
		IDCooperativeFrom INT FOREIGN KEY REFERENCES Cooperative(IDCooperative) NOT NULL, 
		IDCooperativeTo INT FOREIGN KEY REFERENCES Cooperative(IDCooperative) NOT NULL, 
		State INT NOT NULL DEFAULT -1
	)
END