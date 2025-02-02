# Gestion de Bibliothèque - API REST

L'API permet de gérer les livres, catégories, éditeurs, emprunts et utilisateurs en appliquant les principes d'une API REST (routing, validation, pagination, filtrage, tri, gestion des erreurs).


## Fonctionnalités Essentielles
- **Books** : Création, consultation, modification et suppression (uniquement si le livre n'est pas emprunté).
- **Categories** : Gestion des catégories (ajout et suppression uniquement si non associées à un livre).
- **Publishers** : Gestion des éditeurs (ajout et suppression uniquement si non associées à un livre).
- **Loans** : Enregistrement des emprunts et gestion du retour des livres.
- **Users** : Gestion des utilisateurs (création, consultation et suppression).

## Technologies Utilisées
- **ASP.NET Core**
- **Entity Framework Core avec SQLite**
- **Swagger** pour la documentation

## Exécuter l'API :
```sh
dotnet run
```
L'application démarrera et affichera l'URL (exemple : `https://localhost:7238`).

### Accéder à la documentation Swagger :
Ouvrez votre navigateur et rendez-vous sur `https://localhost:7238/swagger/index.html`.

## Structure Essentielle du Projet
```
LibraryApiProject/
├── Controllers/
├── Data/
├── Models/
├── Services/
├── Program.cs
└── LibraryApiProject.csproj
