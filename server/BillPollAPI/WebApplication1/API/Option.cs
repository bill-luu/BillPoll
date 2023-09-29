namespace WebApplication1.API
{
    public class Option
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Votes { get; set; }
        public Option(Models.Option option)
        {
            this.ID = option.Id;
            this.Name = option.Name;
            this.Votes = option.Votes;
        }

        public Option() { } 
    }
    public class OptionCreate
    {
        public string Name { get; set; }
        public int Votes { get; set; }
        public OptionCreate(Models.Option option)
        {
            this.Name = option.Name;
            this.Votes = option.Votes;
        }

        public OptionCreate() { }
    }
}
