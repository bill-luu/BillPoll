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

        public Poll(API.Poll pollDTO)
        {
            Id = pollDTO.Id;
            Name = pollDTO.Name;
            Options = new List<Option>();
            foreach (API.Option option in pollDTO.Options) {
                Options.Add(new Option(option, Id));
            }
        }

        public Poll() { }
    }
}
