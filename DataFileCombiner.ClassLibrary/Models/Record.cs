using Newtonsoft.Json;

namespace DataFileCombiner.ClassLibrary.Models;

public class Record
{
    public int UserId { get; set; }
    public string Pan { get; set; }
    public string ExpDate { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
}

public class RecordsContainer
{
    [JsonProperty("Records")]
    public List<Record> Records { get; set; }
}
