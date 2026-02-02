using BlogApi.DTOs;
using BlogApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class CommentsController : ControllerBase
    {

        private readonly CommentService _service;

        public CommentsController(CommentService service)
        {
            _service = service;
        }

        [HttpGet("articles/{articleId}/comments")]
        public async Task<IActionResult> GetByArticleId(int articleId)
        {
            try
            {
                var comments = await _service.GetByArticleAsync(articleId);
                return Ok(comments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {  message = ex.Message });
            }
        }

        [HttpPost("articles/{articleId}/comments")]
        public async Task<IActionResult> Create([FromBody] CreateCommentDto dto)
        {
            try
            {
                var comment = await _service.CreateAsync(dto);
                return Created($"api/comments/{comment.Id}", comment);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("introuvable"))
                    return NotFound(new { message = ex.Message });

                if (ex.Message.Contains("obligatoire"))
                    return BadRequest(new { message = ex.Message });

                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete("comments/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("introuvable"))
                    return NotFound(new { message = ex.Message });

                return StatusCode(500, new { message = ex.Message });
            }
        }


    }
}
