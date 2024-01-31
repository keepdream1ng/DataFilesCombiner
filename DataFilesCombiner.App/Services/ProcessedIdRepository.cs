using DataFileCombiner.ClassLibrary.Interfaces;
using DataFileCombiner.ClassLibrary.Models;

namespace DataFilesCombiner.App.Services;
public class ProcessedIdRepository : IProcessedIdRepository
{
	public IQueryable<IdFromCsv> IdCsv => _idCsv.AsQueryable();
	private HashSet<IdFromCsv> _idCsv = [];
    private readonly object _idCsvLock = new object();

	public IQueryable<IdFromXml> IdXml => _idXml.AsQueryable();
	private HashSet<IdFromXml> _idXml = [];
    private readonly object _idXmlLock = new object();

	public async Task AddRangeAsync(IEnumerable<IdFromCsv> ids)
	{
		if (ids is null) return;
		await Task.Run(() =>
		{
			lock (_idCsvLock)
			{
				_idCsv.UnionWith(ids);
			}
		});
	}

	public async Task AddRangeAsync(IEnumerable<IdFromXml> ids)
	{
		if (ids is null) return;
		await Task.Run(() =>
		{
			lock (_idXmlLock)
			{
				_idXml.UnionWith(ids);
			}
		});
	}

	public async Task Reset()
	{
		List<Task> tasks = new();
		tasks.Add(Task.Run(() =>
		{
			lock (_idXmlLock)
			{
				_idXml.Clear();
			}
		}));

		tasks.Add(Task.Run(() =>
		{
			lock (_idCsvLock)
			{
				_idCsv.Clear();
			}
		}));
		await Task.WhenAll(tasks);
	}
}
