using DataFileCombiner.ClassLibrary.Models;
using System.Xml.Serialization;

namespace DataFileCombiner.ClassLibrary.Utility;
public static class XmlDeserializer
{
	public static Result<T> ProcessWholeFile<T>(string filePath)
	{
		try
		{
			XmlSerializer serializer = new XmlSerializer(typeof(T));

			using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
			{
				T? newobject = (T?)serializer.Deserialize(fileStream);
				if (newobject is null) return Result<T>.Failure();
				return Result<T>.Success(newobject);
			}
		}
		catch 
		{
			return Result<T>.Failure();
		}
	}
}
