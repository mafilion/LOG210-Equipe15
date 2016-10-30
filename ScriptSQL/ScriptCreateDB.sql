/* Table Configuration de l'application */
IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_NAME = 'Settings'))
BEGIN
    CREATE TABLE Settings
	(
		IDSettings INT PRIMARY KEY IDENTITY(1,1),
		IDLanguage INT not null DEFAULT 1
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
		StudentPassword VARCHAR(1000) NOT NULL
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
	VALUES(2,'Utiliser un compte étudiant pour vous connecter.','Use a student account to login')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Utiliser un compte gestionaire pour vous connecter.','Utiliser un compte gestionaire pour vous connecter.')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Utiliser un compte gestionaire pour vous connecter.','Use a manager account to login') 

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(1,'Déconnexion','Déconnexion')

	INSERT INTO Resources (IDLanguage, TextName, Description)
	VALUES(2,'Déconnexion','Logoff')


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
		IDBookState INT FOREIGN KEY REFERENCES BookState(IdBookState),
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