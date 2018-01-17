using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CyberPatriot.Models;

namespace CyberPatriot.DiscordBot.Services
{
    public interface IScoreRetrievalService
    {
        Task<CompleteScoreboardSummary> GetScoreboardAsync(ScoreboardFilterInfo filter);
        /// <summary>
        /// Gets the details for a given team, or <code>null</code> if the given team does not exist or could not be retrieved.
        /// </summary>
        /// <param name="team">The team ID to retrieve info for.</param>
        /// <returns>A task which will complete with either the team details or null if the team does not exist.</returns>
        Task<ScoreboardDetails> GetDetailsAsync(TeamId team);
        Task InitializeAsync(IServiceProvider provider);
        bool IsDynamic { get; }
        string StaticSummaryLine { get; }
        CompetitionRound Round { get; }
        ScoreFormattingOptions FormattingOptions { get; }

    }
}