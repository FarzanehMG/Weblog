using ArticleManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using Query.Contracts.Article;

namespace Query.Query
{
    public class ArticleQuery : IArticleQuery
    {
        private readonly BlogContext _context;

        public ArticleQuery(BlogContext context) => _context = context;


        /*public ArticleQuery(BlogContext context)
        {
            _context = context;
        }*/

        public ArticleQueryModel GetArticleDetails(string slug)
        {
            var article = _context.Articles
               .Include(x => x.Category)
               /*.Where(x => x.PublishDate <= DateTime.Now)*/
               .Select(x => new ArticleQueryModel
               {
                   Id = x.Id,
                   Title = x.Title,
                   CategoryName = x.Category.Name,
                   CategorySlug = x.Category.Slug,
                   Slug = x.Slug,
                   Description = x.Description,
                   Keywords = x.Keywords,
                   MetaDescription = x.MetaDescription,
                   Picture = x.Picture,
                   PictureAlt = x.PictureAlt,
                   PictureTitle = x.PictureTitle,
                   PublishDate = x.PublishDate.ToString(),
                   ShortDescription = x.ShortDescription,
               }).FirstOrDefault(x => x.Slug == slug && DateTime.Parse(x.PublishDate) <= DateTime.Now);

            /*if (!string.IsNullOrWhiteSpace(article.Keywords))
                article.KeywordList = article.Keywords.Split(",").ToList();*/

            if (!string.IsNullOrWhiteSpace(article.Keywords))
            {
                ReadOnlySpan<char> keywordsSpan = article.Keywords.AsSpan();

                var keywordList = new List<string>();
                int start = 0;
                for (int i = 0; i < keywordsSpan.Length; i++)
                {
                    if (keywordsSpan[i] == ',')
                    {
                        keywordList.Add(keywordsSpan.Slice(start, i - start).ToString());
                        start = i + 1;
                    }
                }
                // Add the last keyword after the loop
                keywordList.Add(keywordsSpan.Slice(start).ToString());

                article.KeywordList = keywordList;
            }


            return article;
        }

        public List<ArticleQueryModel> LatestArticles()
        {
            return _context.Articles
                .Include(x => x.Category)
                .Where(x => x.PublishDate <= DateTime.Now)
                .Select(x => new ArticleQueryModel
                {
                    Title = x.Title,
                    Slug = x.Slug,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    PublishDate = x.PublishDate.ToString(),
                    ShortDescription = x.ShortDescription,
                }).ToList();
        }
    }
}
