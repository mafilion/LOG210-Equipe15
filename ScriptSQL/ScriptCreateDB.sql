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
		PhoneNumber VARCHAR(12) NOT NULL,
		StudentPassword VARCHAR(50) NOT NULL
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
		ManagerPassword VARCHAR(50) NOT NULL
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
	
	
END
