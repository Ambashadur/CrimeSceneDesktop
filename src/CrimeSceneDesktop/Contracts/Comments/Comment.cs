namespace CS.Contracts.Comments;

public class Comment
{
    public long Id { get; set; }

    public string Scene { get; set; } = string.Empty;

    public string Text { get; set; } = string.Empty;

    public string AudioLink { get; set; } = string.Empty;

    public string PhotoLink { get; set; } = string.Empty;
}
