using CsvHelper;
using CsvHelper.Configuration;
using DataFileCombiner.ClassLibrary.Models;
using System.Globalization;

namespace DataFileCombiner.ClassLibrary.Utility;
public static class CsvDeserializer
{
	public static Result<IEnumerable<T>> ProcessAllRecords<T>(string filePath, string delimiter = ";")
	{
		try
		{
			var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
			{
				Delimiter = delimiter,
				HasHeaderRecord = true,
			};

			using (var reader = new System.IO.StreamReader(filePath, System.Text.Encoding.UTF8))
			using (var csv = new CsvReader(reader, configuration))
			{
				List<T> records = csv.GetRecords<T>().ToList();
				if (records.Count == 0) return Result<IEnumerable<T>>.Failure();
				return Result<IEnumerable<T>>.Success(records);
			}
		}
		catch
		{
			return Result<IEnumerable<T>>.Failure();
		}
	}
}
