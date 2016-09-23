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


/* Table pour Compte ﾉtudiant */
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
		StudentPassword VARCHAR(25) NOT NULL
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
	);

END

/*Table pour la coopérative*/
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
		ManagerPassword VARCHAR(25) NOT NULL
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
	VALUES(1,'Retour','Retour ・la page pr馗馘ente')

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
	VALUES(1,'Deconnexion','D馗onnexion')

	INSERT INTO Resources (IDLanguage,TextName,Description)
	VALUES(2,'Deconnexion','Log out')

	INSERT INTO Resources (IDLanguage,TextName,Description)
	VALUES(1,'Nom', 'Nom')

	INSERT INTO Resources (IDLanguage,TextName,Description)
	VALUES(2,'Nom','Last Name')

	INSERT INTO Resources (IDLanguage,TextName,Description)
	VALUES(1,'Prenom', 'Prénom')

	INSERT INTO Resources (IDLanguage,TextName,Description)
	VALUES(2,'Nom','First Name')

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



END
