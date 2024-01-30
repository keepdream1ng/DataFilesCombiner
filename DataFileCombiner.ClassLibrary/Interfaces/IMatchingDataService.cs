using DataFileCombiner.ClassLibrary.Models;

namespace DataFileCombiner.ClassLibrary.Interfaces;
public interface IMatchingDataService
{
    int CheckedMatchesCount { get; }

    event EventHandler? NewMatchesFound;

    Task CheckMatchesAsync();
    FilePathsPerExtension GetMatchedFilesPaths();
}