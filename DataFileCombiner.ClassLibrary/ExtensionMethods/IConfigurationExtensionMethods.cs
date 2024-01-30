using DataFileCombiner.ClassLibrary.Utility;
using Microsoft.Extensions.Configuration;

namespace DataFilesCombiner.ClassLibrary.ExtensionMethods;
public static class IConfigurationExtensionMethods
{
	public static string GetInputDirectoryExistingPath(this IConfiguration configuration)
	{
		return WorkingFolders.GetExistingFullPath(configuration["InputDirectoryPath"]);
	}
	public static string GetOutputDirectoryExistingPath(this IConfiguration configuration)
	{
		return WorkingFolders.GetExistingFullPath(configuration["OutputDirectoryPath"]);
	}
}
