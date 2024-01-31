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

Inputs example:

```xml
<?xml version="1.0" encoding="UTF-8" ?>
<Cards>
    <Card UserId="1">
        <Pan>1234123412341231</Pan>
        <ExpDate>10/11/2017</ExpDate>
    </Card>
    <Card UserId="2">
        <Pan>1234123412341232</Pan>
        <ExpDate>20/11/2017</ExpDate>
    </Card>
</Cards>
```

```csv
UserId;Name;SecondName;Number
1;Anatolii;Ivanov;1_9056938119
2;Андрей;Андреев;2_9056938119
```

Output example:

```json
{
  "Records": [
    {
      "UserId": "1",
      "Pan": "1234123412341231",
      "ExpDate": "10/11/2017",
      "FirstName": "Anatolii",
      "LastName": "Ivanov",
      "Phone": "1_9056938119"
    },
    {
      "UserId": "2",
      "Pan": "1234123412341232",
      "ExpDate": "20/11/2017",
      "FirstName": "Андрей",
      "LastName": "Андреев",
      "Phone": "2_9056938119"
    },
  ]
}
```