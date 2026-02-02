# 📝 Blog API

API REST permettant de gérer des **articles** et des **commentaires**, développée en **ASP.NET Core**, avec une architecture séparant la logique métier et les contrôleurs HTTP.

---

## 🚀 Fonctionnalités

- CRUD complet pour les **articles**
- CRUD pour les **commentaires**
- Gestion des erreurs métier via `try/catch` (sans mélanger HTTP et logique métier)
- Documentation Swagger intégrée
- Architecture : Services + Controllers
- Base de données MySQL via Entity Framework Core

---

## 🏗️ Architecture du projet

BlogApi/
│
├── Controllers/
│   ├── ArticlesController.cs
│   └── CommentsController.cs
│
├── Services/
│   ├── ArticleService.cs
│   └── CommentService.cs
│
├── Models/
│   ├── Article.cs
│   └── Comment.cs
│
├── DTOs/
│   ├── CreateArticleDto.cs
│   ├── UpdateArticleDto.cs
│   ├── CreateCommentDto.cs
│   └── PaginatedResult.cs
│
├── Data/
│   └── BlogContext.cs
│
├── Program.cs
└── README.md


---

## 🛠️ Technologies utilisées

- **ASP.NET Core 8**
- **Entity Framework Core**
- **MySQL**
- **Swagger / OpenAPI**
- **C# 12**

---

## ⚙️ Installation

### 1. Cloner le projet

```bash
git clone https://github.com/ton-repo/blog-api.git
cd blog-api

"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;database=blog_db;User=root;Password=root;"
}

Appliquer les migrations : Add-migration Initial et update-database

Pour tester l'application avec Swagger :
Lancer l'API et saisir dans l'URL du navigateur https://localhost:7074/swagger (chnager éventuellement le port depuis votre poste)



📌 Endpoints principaux
🔹 Articles
Méthode	Endpoint	Description
GET	/api/articles	Récupère tous les articles
GET	/api/articles/{id}	Récupère un article
POST	/api/articles	Crée un article
PUT	/api/articles/{id}	Met à jour un article
DELETE	/api/articles/{id}	Supprime un article
GET	/api/articles/paged?page=1&pageSize=5	Récupère les articles paginés
🔹 Commentaires
Méthode	Endpoint	Description
GET	/api/comments/article/{articleId}	Récupère les commentaires d’un article
POST	/api/comments	Crée un commentaire
DELETE	/api/comments/{id}	Supprime un commentaire


🧠 Gestion des erreurs
L’API utilise une approche simple :

Les services lèvent des Exception avec un message métier clair

Les controllers convertissent ces erreurs en réponses HTTP (400, 404, 500)

Exemples :

"Article introuvable" → 404 Not Found

"Le titre est obligatoire" → 400 Bad Request

Erreur interne → 500 Internal Server Error


🤝 Contribution
Les contributions sont les bienvenues !
Forkez le projet, créez une branche et proposez une PR.


✨ Auteur
Développé par Linda Mehdi