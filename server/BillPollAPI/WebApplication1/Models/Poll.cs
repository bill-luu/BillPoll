namespace WebApplication1.Models
{
    public class Poll
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Option[] Options { get; set; }

        public Poll(string id, string name, Option[] options)
        {
            Id = id;
            Name = name;
            Options = options;
        }
    }
}
