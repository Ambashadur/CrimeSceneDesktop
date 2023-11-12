using System.Net.Http.Json;
using CS.Contracts;
using CS.Contracts.Comments;
using Microsoft.AspNetCore.WebUtilities;

namespace CS.Common.Services;

public class CommentService
{
    private const string COMMENTS_PAGE = "api/comments/page";

    public async Task<PageResult<Comment>> GetCommentsAsync(GetCommentsPageContext context) {
        var query = new Dictionary<string, string>() {
            [nameof(GetCommentsPageContext.Page)] = context.Page.ToString(),
            [nameof(GetCommentsPageContext.Count)] = context.Count.ToString(),
            [nameof(GetCommentsPageContext.UserId)] = context.UserId.ToString(),
        };

        var uri = QueryHelpers.AddQueryString(COMMENTS_PAGE, query);
        var request = new HttpRequestMessage(HttpMethod.Get, uri);

        using var response = await CSDHttpClient.Client.SendAsync(request);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<PageResult<Comment>>(options: CSDHttpClient.JsonOptions);
    }
}
