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
END

/*Table pour les états de livres*/
IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_NAME = 'EtatLivre'))
BEGIN
	CREATE TABLE EtatLivre(
		IDEtatLivre INT PRIMARY KEY IDENTITY(1,1),
		Description VARCHAR(MAX) NOT NULL,
		PourcentagePrix float NOT NULL
	)
	/* Insertion des États */
	INSERT INTO EtatLivre(Description,PourcentagePrix) VALUES ('Comme neuf',0.75);
	INSERT INTO EtatLivre(Description,PourcentagePrix) VALUES ('Quelques pages pliés, utilisation d’un marqueur',0.50);
	INSERT INTO EtatLivre(Description,PourcentagePrix) VALUES ('Livre très utilisé, pages plié, couverture endommagés',0.25);
END


/*Table pour les auteurs des livres*/
IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_NAME = 'Auteur'))
BEGIN
	CREATE TABLE Auteur(
		IDAuteur INT PRIMARY KEY IDENTITY(1,1),
		Name VARCHAR(200) NOT NULL,
	)
END


/*Table pour les livres ajouté par les étudiants */
IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_NAME = 'Livre'))
BEGIN
	CREATE TABLE Livre(
		IDLivre INT PRIMARY KEY IDENTITY(1,1),
		noISBN VARCHAR(50),
		noEAN VARCHAR(50),
		noUPC VARCHAR(50),
		Titre varchar(300) NOT NULL,
		nbPages int NOT NULL,
		prix float NOT NULL,
		IDEtatLivre INT FOREIGN KEY REFERENCES EtatLivre(IDEtatLivre) NOT NULL
	)
	CREATE UNIQUE NONCLUSTERED INDEX idxISBN on dbo.Livre(noISBN) WHERE noISBN IS NOT NULL;
	CREATE UNIQUE NONCLUSTERED INDEX idxEAN on dbo.Livre(noEAN) WHERE noEAN IS NOT NULL;
	CREATE UNIQUE NONCLUSTERED INDEX idxUPC on dbo.Livre(noUPC) WHERE noUPC IS NOT NULL;
END

/* Table pour la liaison Livre/Auteurs */
IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_NAME = 'LivresAuteurs'))
BEGIN
	CREATE TABLE LivresAuteurs(
		IDLivresAuteurs INT PRIMARY KEY IDENTITY(1,1),
		IDAuteur INT FOREIGN KEY REFERENCES Auteur(IDAuteur) NOT NULL,
		IDLivre INT FOREIGN KEY REFERENCES Livre(IDLivre) NOT NULL
	)
END
