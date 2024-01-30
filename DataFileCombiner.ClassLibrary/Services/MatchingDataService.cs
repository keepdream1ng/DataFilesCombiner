using DataFileCombiner.ClassLibrary.Interfaces;
using DataFileCombiner.ClassLibrary.Models;

namespace DataFileCombiner.ClassLibrary.Services;
public class MatchingDataService : IMatchingDataService
{
	public int CheckedMatchesCount { get; private set; } = 0;
	public event EventHandler? NewMatchesFound;
	private readonly IProcessedIdRepository _repository;

	public MatchingDataService(IProcessedIdRepository repository)
	{
		_repository = repository;
	}

	public void CheckMatches()
	{
		int matchedRecorsCount = (from IdXml_cards in _repository.IdXml
								  join IdCsv_users in _repository.IdCsv on IdXml_cards.UserId equals IdCsv_users.UserId
								  select IdXml_cards).Count();

		if (matchedRecorsCount != CheckedMatchesCount)
		{
			CheckedMatchesCount = matchedRecorsCount;
			OnNewMathesFound();
		}
	}

	public IEnumerable<FilePath> GetMatchedFilesPaths()
	{
		throw new NotImplementedException();
	}

	private void OnNewMathesFound()
	{
		NewMatchesFound?.Invoke(this, new EventArgs());
	}
}
