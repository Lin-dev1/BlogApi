using BlogApi.DTOs;
using BlogApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {

        private readonly ArticleService _service;

        public ArticlesController(ArticleService service)
        {
            _service = service;
        }


        // GET: api/articles
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var articles =await _service.GetAllAsync();
                return Ok(articles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // GET api/articles>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var article = await _service.GetByIdAsync(id);
                return Ok(article);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("introuvable"))
                    return NotFound(new {  message = ex.Message });

                return StatusCode(500, new { error = ex.Message });
            }
        }

        // POST api/articles
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateArticleDto dto)
        {
            try
            {
                var article = await _service.CreateAsync(dto);
                return Created($"api/articles/{article.Id}", article);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("obligatoire"))
                    return BadRequest(new { message = ex.Message });
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // PUT api/articles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateArticleDto dto)
        {
            try
            {
                await _service.UpdateAsync(id, dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("introuvable"))
                    return NotFound(new { message = ex.Message });

                return StatusCode(500, new { message = ex.Message });
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("introuvalbe"))
                    return NotFound(new { message = ex.Message });

                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
