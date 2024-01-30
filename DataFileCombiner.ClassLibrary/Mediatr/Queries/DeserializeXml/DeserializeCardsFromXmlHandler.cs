using DataFileCombiner.ClassLibrary.Models;
using DataFileCombiner.ClassLibrary.Utility;
using MediatR;

namespace DataFileCombiner.ClassLibrary.Mediatr.Queries;
public class DeserializeCardsFromXmlHandler : IRequestHandler<DeserializeCardsFromXmlQuery, Result<Cards>>
{
	public async Task<Result<Cards>> Handle(DeserializeCardsFromXmlQuery request, CancellationToken cancellationToken)
	{
		return await Task.Run(() =>
			XmlDeserializer.ProcessWholeFile<Cards>(request.PathRecord.Path)
		);
	}
}
