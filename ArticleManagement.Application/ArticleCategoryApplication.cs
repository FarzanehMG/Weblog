﻿using ArticleManagement.Application.Contracts.ArticleCategory;
using ArticleManagement.Domain.ArticleCategoryAgg;
using Framework.Application;

namespace ArticleManagement.Application
{
    public class ArticleCategoryApplication : IArticleCategoryApplication
    {
        private readonly IArticleCategoryRepository _articleCategoryRepository;
        private readonly IFileUploader _fileUploader;

        public ArticleCategoryApplication(IArticleCategoryRepository articleCategoryRepository, IFileUploader fileUploader)
        {
            _articleCategoryRepository = articleCategoryRepository;
            _fileUploader = fileUploader;
        }


        public OperationResult Create(CreateArticleCategory command)
        {
            var operation = new OperationResult();

            if (_articleCategoryRepository.Exists(x=>x.Name == command.Name))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }

            var slug = command.Slug.Slugify();
            var pictureName = _fileUploader.Upload(command.Picture, slug);

            var articleCategory = new ArticleCategory(command.Name, pictureName, command.PictureAlt,
                command.PictureTitle, command.Description, command.Slug, command.Keywords, command.MetaDescription);

            _articleCategoryRepository.Create(articleCategory);
            _articleCategoryRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Edit(EditArticleCategory command)
        {
            var operation = new OperationResult();

            var articleCategory = _articleCategoryRepository.Get(command.Id);

            if (articleCategory == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            if (_articleCategoryRepository.Exists(x=>x.Name == command.Name && x.Id != command.Id))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }

            var slug = command.Slug.Slugify();
            var pictureName = _fileUploader.Upload(command.Picture, slug);


            articleCategory.Edit(command.Name, pictureName, command.PictureAlt,command.PictureTitle,
                command.Description,command.Slug,command.Keywords,command.MetaDescription);

            _articleCategoryRepository.SaveChanges();
            return operation.Succedded();

        }

        public EditArticleCategory GetDetails(long id)
        {
            return _articleCategoryRepository.GetDetails(id);
        }

        public List<ArticleCategoryViewModel> GetArticleCategories()
        {
            return _articleCategoryRepository.GetArticleCategories();
        }

        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel)
        {
            return _articleCategoryRepository.Search(searchModel);
        }
    }
}