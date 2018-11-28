﻿
using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Management.Automation;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;
using AutoMapper;
using Microsoft.Extensions.Logging;
using RelationalGit.KnowledgeShareStrategies.Models;

namespace RelationalGit
{
    public class IdealKnowledgeShareStrategy : KnowledgeShareStrategy
    {
        public IdealKnowledgeShareStrategy(string knowledgeSaveReviewerReplacementType) : base(knowledgeSaveReviewerReplacementType)
        { }

        protected override PullRequestRecommendationResult RecommendReviewers(PullRequestContext pullRequestContext)
        {
            var oldestDevelopers = pullRequestContext.Developers.Values.Where(q=>q.FirstCommitPeriodId<=pullRequestContext.Period.Id);

            var longtermStayedDeveloper = oldestDevelopers.OrderBy(q=>q.LastCommitPeriodId-q.FirstCommitPeriodId).Last();

            return new PullRequestRecommendationResult(new string[] { longtermStayedDeveloper.NormalizedName});
        }
    }
}
