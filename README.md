# DataFilesCombiner

This app works asynchronously by checking the input folder for new data files (.xml and .csv). Currently, it is only configured for one type of report based on matching UserId properties from deserialized objects. It checks files initially and stores in memory only successfully parsed Id's and filepaths. So, at the moment of generating the report, it will only read files that have matched records.

# Usage

Copy files into the input folder (in whatever order you prefer), configured in appsettings.json. For example, with a relative to the executable file path:

```json
  "InputDirectoryPath": ".\\Input\\",
```

In case you get lost with the configuration, there is an "Open input folder" button. Once the program finds a matched record, the "Generate report" button will be enabled. Clicking the "Generate report" button will command the app to reread matching files. On success, it will open the output folder, the location of which is provided in the config file. For example, with an absolute file path:

```json
  "OutputDirectoryPath": "C:\\SuperValuableReports\\",
```

I added logging for troubleshooting the app. Reading them can provide an idea of what has gone wrong if the report failed to generate. Logs are saved to the output directory as well.