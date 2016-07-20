﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPiServerDemo1.UI.Models.ViewModels
{
    public class SearchContentModel
    {
        public SearchContentModel()
        {

        }

        public bool SearchServiceDisabled { get; set; }
        public string SearchedQuery { get; set; }
        public int NumberOfHits { get; set; }
        public IEnumerable<SearchHit> Hits { get; set; }
        public string SearchPageUrl { get; set; }
        public class SearchHit
        {
            public string Title { get; set; }
            public string Url { get; set; }
            public string Excerpt { get; set; }

        }
    }
}