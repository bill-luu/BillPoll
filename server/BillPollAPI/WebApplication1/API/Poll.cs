namespace WebApplication1.API
{
    public class Poll
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Option> Options { get; set; } = new List<Option>();

        public Poll(Models.Poll model)
        {
            this.Id = model.Id;
            this.Name = model.Name;
            foreach (Models.Option option in model.Options)
            {
                this.Options.Add(new Option(option));
            }

        }

        public Poll(int id, string name, ICollection<Option> options)
        {
            Id = id;
            Name = name;
            Options = options;
        }
        public Poll() { }
    }

    public class PollCreate
    {
        public string Name { get; set; }
        public ICollection<OptionCreate> Options { get; set; } = new List<OptionCreate>();

        public PollCreate(string name, ICollection<OptionCreate> options)
        {
            Name = name;
            Options = options;
        }
        public PollCreate() { }
    }
}
