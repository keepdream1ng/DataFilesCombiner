using System.Xml.Serialization;

namespace DataFileCombiner.ClassLibrary.Models;

[XmlRoot("Cards")]
public class Cards
{
    [XmlElement("Card")]
    public List<Card> CardList { get; set; }
}

public class Card
{
    [XmlAttribute("UserId")]
    public int UserId { get; set; }

    public string Pan { get; set; }
    public string ExpDate { get; set; }
}
