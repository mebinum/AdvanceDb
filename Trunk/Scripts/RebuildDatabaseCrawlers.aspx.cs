using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Lucene.Net.Analysis;
using Lucene.Net.Index;
using Lucene.Net.Store;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Jobs;
using Sitecore.Search;
using Sitecore.Data.Indexing;

namespace Sitecore.SharedSource.Search.Scripts
{
    public class RebuildDatabaseCrawlers : System.Web.UI.Page
    {
        protected CheckBoxList cblIndexes;
        protected Button btnRebuild;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowIndexes();
            }
        }

        protected void RebuildBtn_Click(object sender, EventArgs args)
        {
            foreach (ListItem item in cblIndexes.Items)
            {
                if (item.Selected)
                {
                    JobOptions options = new JobOptions("RebuildSearchIndex", "index", Sitecore.Client.Site.Name, new Builder(item.Value), "Rebuild");
                    options.AfterLife = TimeSpan.FromMinutes(1.0);
                    Job job = JobManager.Start(options);
                }
            }
        }

        private IDictionary<string, Sitecore.Search.Index> GetSearchIndexes()
        {
            var _configuration = Factory.CreateObject("search/configuration", true) as SearchConfiguration;
            return _configuration.Indexes;
        }

        private void ShowIndexes()
        {
            foreach (var index in GetSearchIndexes())
            {
                cblIndexes.Items.Add(new ListItem(index.Key, index.Key));
            }
        }

        public class Builder
        {
            private string _indexName;

            public Builder(string indexName)
            {
                _indexName = indexName;
            }

            public void Rebuild()
            {
                var index = SearchManager.GetIndex(_indexName);
                if (index != null)
                {
                    index.Rebuild();
                    Optimize(false, index.Directory, index.Analyzer);
                }
            }

            protected virtual void Optimize(bool recreate, Directory directory, Analyzer analyzer)
            {
                using (new IndexLocker(directory.MakeLock("write.lock")))
                {
                    recreate |= !IndexReader.IndexExists(directory);
                    new IndexWriter(directory, analyzer, recreate).Optimize();
                }
            }
        }
    }
}
