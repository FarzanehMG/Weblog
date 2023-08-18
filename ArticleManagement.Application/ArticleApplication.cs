using ArticleManagement.Application.Contracts.Article;
using Framework.Application;

namespace ArticleManagement.Application
{
    public class ArticleApplication : IArticleApplication
    {
        public OperationResult Create(CreateArticle command)
        {
            throw new NotImplementedException();
        }

        public OperationResult Edit(EditArticle command)
        {
            throw new NotImplementedException();
        }

        public EditArticle GetDetails(long id)
        {
            throw new NotImplementedException();
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
            throw new NotImplementedException();
        }
    }
}
