using DataFileCombiner.ClassLibrary.Interfaces;
using DataFileCombiner.ClassLibrary.Models;

namespace DataFileCombiner.ClassLibrary.Services;
public class MatchingDataService : IMatchingDataService
{
	public int CheckedMatchesCount { get; private set; } = 0;
	public event EventHandler? NewMatchesFound;
	private readonly IProcessedIdRepository _repository;
	private readonly object _locker = new object();

	public MatchingDataService(IProcessedIdRepository repository)
	{
		_repository = repository;
	}

	public async Task CheckMatchesAsync()
	{
		int matchedRecorsCount = (from IdXml_cards in _repository.IdXml
								  join IdCsv_users in _repository.IdCsv on IdXml_cards.UserId equals IdCsv_users.UserId
								  select IdXml_cards).Count();

		if (matchedRecorsCount != CheckedMatchesCount)
		{
			await UpdateCount(matchedRecorsCount);
		}
	}

	public FilePathsPerExtension GetMatchedFilesPaths()
	{
		var matches = from IdXml_cards in _repository.IdXml
					  join IdCsv_users in _repository.IdCsv on IdXml_cards.UserId equals IdCsv_users.UserId
					  select new
					  {
						  Xmlpath = IdXml_cards.FilePath,
						  CsvPath = IdCsv_users.FilePath,
					  };
		return new FilePathsPerExtension(
			matches.Select(m => m.CsvPath).Distinct().ToArray(),
			matches.Select(m => m.Xmlpath).Distinct().ToArray()
			);
	}

	private async Task UpdateCount(int count)
	{
		await Task.Run(() =>
		{
			lock (_locker)
			{
				CheckedMatchesCount = count;
				OnNewMathesFound();
			}
		});
	}

	private void OnNewMathesFound()
	{
		NewMatchesFound?.Invoke(this, new EventArgs());
	}
}
