namespace WebApplication1.Models
{
    public class Option
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public int Votes { get; set; }

        public Option(string iD, string name, int votes)
        {
            ID = iD;
            Name = name;
            Votes = votes;
        }
    }
}
