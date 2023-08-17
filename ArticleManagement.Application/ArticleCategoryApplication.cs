using ArticleManagement.Application.Contracts.ArticleCategory;
using Framework.Application;

namespace ArticleManagement.Application
{
    public class ArticleCategoryApplication : IArticleCategoryApplication
    {
        public OperationResult Create(CreateArticleCategory command)
        {
            throw new NotImplementedException();
        }

        public OperationResult Edit(EditArticleCategory command)
        {
            throw new NotImplementedException();
        }

        public EditArticleCategory GetDetails(long id)
        {
            throw new NotImplementedException();
        }

        public List<ArticleCategoryViewModel> GetArticleCategories()
        {
            throw new NotImplementedException();
        }

        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel)
        {
            throw new NotImplementedException();
        }
    }
}