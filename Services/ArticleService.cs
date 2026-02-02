using BlogApi.Data;
using BlogApi.DTOs;
using BlogApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Services
{
    public class ArticleService
    {
        private readonly BlogContext _context;

        public ArticleService(BlogContext context)
        {
            _context = context;
        }

        public async Task<List<Article>> GetAllAsync()
        {
            try
            {
                return await _context.Articles
                    .Include(a => a.Comments)
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur interne lors de la récupération des articles : " + ex.Message);
            }
        }
            
        

        public async Task<Article?> GetByIdAsync(int id)
        {
            try
            {
                var article = await _context.Articles
                    .Include(a => a.Comments)
                    .FirstOrDefaultAsync(a => a.Id == id);

                if (article == null)
                    throw new Exception("Article introuvable");

                return article;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la récupération de l'aticle" + ex.Message);
            }
        }

        public async Task <Article> CreateAsync(CreateArticleDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.Title))
                    throw new Exception("Le titre est obligatoire");
                var article = new Article
                {
                    Title = dto.Title,
                    Content = dto.Content,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                _context.Articles.Add(article);
                await _context.SaveChangesAsync();

                return article;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la création de l'article :" + ex.Message);
            }
        }

        public async Task UpdateAsync(int id, UpdateArticleDto dto)
        {
            try
            {
                var article = await _context.Articles.FindAsync(id);
                if (article == null) 
                    throw new Exception("Article introuvable");

                article.Title = dto.Title;
                article.Content = dto.Content;
                article.UpdatedAt = DateTime.Now;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la mise à jour de l'article :" + ex.Message);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var article = await _context.Articles.FindAsync(id);
                if (article == null) 
                    throw new Exception("Article introuvable");

                _context.Articles.Remove(article);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la suppression de l'article" + ex.Message);
            }
        }
    }
}
