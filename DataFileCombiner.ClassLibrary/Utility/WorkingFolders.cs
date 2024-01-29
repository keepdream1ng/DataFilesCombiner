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
}
