using BlogApi.Data;
using BlogApi.DTOs;
using BlogApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Services
{
    public class CommentService
    {

        private readonly BlogContext _context;

        public CommentService(BlogContext context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetByArticleAsync(int articleId)
        {
            try
            {
                var comments = await _context.Comments
                    .Where(c => c.ArticleId == articleId)
                    .ToListAsync();

                return comments;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la récupération des commentaires : " + ex.Message);
            }
        }

        public async Task<Comment> CreateAsync(CreateCommentDto dto) 
        { 
            try 
            { 
                var article = await _context.Articles.FindAsync(dto.ArticleId); 

                if (article == null) 
                    throw new Exception(" Article introuvable"); 
                
                if (string.IsNullOrWhiteSpace(dto.Author)) 
                    throw new Exception("L'auteur est obligatoire"); 
                
                if (string.IsNullOrWhiteSpace(dto.Content)) 
                    throw new Exception("Le contenu du commentaire est obligatoire"); 
                
                var comment = new Comment 
                { 
                    ArticleId = dto.ArticleId, 
                    Author = dto.Author, 
                    Content = dto.Content, 
                    CreatedAt = DateTime.UtcNow 
                }; 
                
                _context.Comments.Add(comment); 
                await _context.SaveChangesAsync();

                return comment; 
            } 
            catch (Exception ex) 
            { 
                throw new Exception("Erreur lors de la création du commentaire : " + ex.Message); 
            } 
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var comment = await _context.Comments.FindAsync(id);
                if (comment == null)
                    throw new Exception("Commentaire introuvable");

                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la suppression du commentaire : " + ex.Message);
            }
        }
    }
}
