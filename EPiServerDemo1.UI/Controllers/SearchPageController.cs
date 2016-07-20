using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPiServer;
using EPiServer.Core;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Web.Mvc;
using EPiServerDemo1.UI.Models.Pages;
using EPiServer.Search;
using EPiServer.Web.Routing;
using EPiServer.ServiceLocation;
using System;
using System.Web;
using EPiServer.Web;
using EPiServerDemo1.UI.Models.ViewModels;
using EPiServer.Search.Queries;
using EPiServer.Search.Queries.Lucene;
using EPiServer.Security;

namespace EPiServerDemo1.UI.Controllers
{

    public class SearchPageController : PageController<SearchPage>
    {
        private readonly SearchHandler _searchHandler;
        private readonly IContentLoader _contentLoader;
        private readonly ContentSearchHandler _contentSearchHandler;
        private readonly UrlResolver _urlResolver;


        public SearchPageController()
        {
            _searchHandler = ServiceLocator.Current.GetInstance<SearchHandler>();
            _contentSearchHandler = ServiceLocator.Current.GetInstance<ContentSearchHandler>();
            _urlResolver = ServiceLocator.Current.GetInstance<UrlResolver>();
            _contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
        }

        [ValidateInput(false)]
        public ActionResult Index(SearchPage currentPage, string q)
        {
            var sm = new SearchContentModel() { };
            sm.SearchedQuery = q;
            sm.SearchPageUrl = _urlResolver.GetUrl(currentPage.ContentLink);
            if (!string.IsNullOrWhiteSpace(q))
            {
                var hits = Search(q.Trim(),
                     new[] { SiteDefinition.Current.StartPage, SiteDefinition.Current.GlobalAssetsRoot, SiteDefinition.Current.SiteAssetsRoot },
                     ControllerContext.HttpContext,
                     currentPage.LanguageID, currentPage.MaxResultsPerPage).ToList();
                sm.Hits = hits;
                sm.NumberOfHits = hits.Count();

            }
            return View(sm);
        }

        private object Search(string v, object[] p, HttpContextBase httpContext, string languageID, object itemsPerPage)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<SearchContentModel.SearchHit> Search(string searchText, IEnumerable<ContentReference> searchRoots, HttpContextBase context, string languageBranch, int ItemsPerPage)
        {
            var query = CreateQuery(searchText, searchRoots, context, languageBranch);
            var searchResults = _searchHandler.GetSearchResults(query, 1, ItemsPerPage);
            SearchContentModel.SearchHit objSearchHit = null;
            List<SearchContentModel.SearchHit> listHits = new List<SearchContentModel.SearchHit>();
            foreach (var result in searchResults.IndexResponseItems)
            {
                objSearchHit = new SearchContentModel.SearchHit();
                var content = _contentSearchHandler.GetContent<IContent>(result);

                //Page type :  specific search
                //OffersPageType offersPageType = content as OffersPageType;

                //if (offersPageType != null)
                //{
                //    string s = offersPageType.Name;
                //}

                objSearchHit.Title = content.Name;
                objSearchHit.Url = _urlResolver.GetUrl(content.ContentLink);
                //objSearchHit.Excerpt = content is OffersPageType ? ((OffersPageType)content).OfferDetails.ToString() : string.Empty;
                listHits.Add(objSearchHit);
            }
            return listHits;
        }

        private IQueryExpression CreateQuery(string searchText, IEnumerable<ContentReference> searchRoots, HttpContextBase context, string languageBranch)
        {
            //Main query which groups other queries. Each query added
            //must match in order for a page or file to be returned.
            var query = new GroupQuery(LuceneOperator.AND);

            //Add free text query to the main query
            query.QueryExpressions.Add(new FieldQuery(searchText));

            //Search for pages using the provided language
            var pageTypeQuery = new GroupQuery(LuceneOperator.AND);
            pageTypeQuery.QueryExpressions.Add(new ContentQuery<PageData>());
            pageTypeQuery.QueryExpressions.Add(new FieldQuery(languageBranch, Field.Culture));

            //Search for media without languages
            var contentTypeQuery = new GroupQuery(LuceneOperator.OR);
            contentTypeQuery.QueryExpressions.Add(new ContentQuery<MediaData>());
            contentTypeQuery.QueryExpressions.Add(pageTypeQuery);

            query.QueryExpressions.Add(contentTypeQuery);

            //Create and add query which groups type conditions using OR
            var typeQueries = new GroupQuery(LuceneOperator.OR);
            query.QueryExpressions.Add(typeQueries);

            foreach (var root in searchRoots)
            {
                var contentRootQuery = new VirtualPathQuery();
                contentRootQuery.AddContentNodes(root, _contentLoader);
                typeQueries.QueryExpressions.Add(contentRootQuery);
            }

            var accessRightsQuery = new AccessControlListQuery();
            accessRightsQuery.AddAclForUser(PrincipalInfo.Current, context);
            query.QueryExpressions.Add(accessRightsQuery);

            return query;
        }
    }
}