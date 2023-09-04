public class Bidder
{
    public string id { get; set; }
    public Society society { get; set; }
    public Tender tender { get; set; }
    public string dateSoumission { get; set; }
    public int status { get; set; }
    public string[] files { get; set; }
}
