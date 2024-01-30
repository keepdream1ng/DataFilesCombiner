using DataFileCombiner.ClassLibrary.Models;

namespace DataFileCombiner.ClassLibrary.Interfaces;
public interface IMatchingDataService
{
    int CheckedMatchesCount { get; }

    event EventHandler? NewMatchesFound;

    void CheckMatches();
    IEnumerable<FilePath> GetMatchedFilesPaths();
}