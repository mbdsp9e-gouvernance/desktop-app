public class Tender
{

    public string id { get; set; }
    public string reference { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public List<Critere> critere { get; set; }
    public string dateEmission { get; set; }
    public string dateLimit { get; set; }
    public int tenderStatus { get; set; }
}
