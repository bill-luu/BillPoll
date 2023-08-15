namespace WebApplication1.Models
{
    public class Poll
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<Option> Options { get; set; } = new List<Option>();

        public Poll(string id, string name, Option[] options)
        {
            Id = id;
            Name = name;
            Options = options;
        }

        public Poll() { }
    }
}
