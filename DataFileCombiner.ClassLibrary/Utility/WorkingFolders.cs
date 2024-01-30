namespace DataFileCombiner.ClassLibrary.Utility;
public static class WorkingFolders
{
	public static string GetExistingFullPath(string? path)
	{
		if (path is null)
		{
			return Directory.GetCurrentDirectory();
		}
		DirectoryInfo dirInfo = new(path);
		if (!dirInfo.Exists)
		{
			dirInfo.Create();
		}
		return dirInfo.FullName;
	}
	public static bool HasReadAccess(string filePath)
	{
		try
		{
			using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
			{
				// If the file can be opened for reading, you have read access
				return true;
			}
		}
		catch (Exception)
		{
			return false;
		}
	}
}
