using DataFileCombiner.ClassLibrary.Models;

namespace DataFileCombiner.ClassLibrary.Interfaces;
/// <summary>
/// Sort of persistance level for Id from processed files, so app wont need to hold in memory all deserialized ojects.
/// </summary>
public interface IProcessedIdRepository
{
	IQueryable<IdFromCsv> IdCsv { get; }
	IQueryable<IdFromXml> IdXml { get; }

	Task AddRangeAsync(IEnumerable<IdFromCsv> ids);
	Task AddRangeAsync(IEnumerable<IdFromXml> ids);
	Task Reset();
}
