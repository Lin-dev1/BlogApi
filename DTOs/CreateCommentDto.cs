namespace BlogApi.DTOs
{
    public class CreateCommentDto
    {
        public int ArticleId { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
    }
}
