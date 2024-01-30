using DataFileCombiner.ClassLibrary.Models;
using Newtonsoft.Json;

namespace DataFileCombiner.ClassLibrary.Utility;
public static class JsonFileWriter
{
	public static bool Write<T>(FilePath filePath, T objectToWrite)
	{
		try
		{
            string json = JsonConvert.SerializeObject(objectToWrite, Formatting.Indented);
            File.WriteAllText(filePath.Path, json); 
			return true;
		}
		catch
		{
			return false;
		}
	}
}
