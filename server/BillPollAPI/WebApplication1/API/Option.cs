using WebApplication1.Models;

namespace WebApplication1.API
{
    public class Option
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public int Votes { get; set; }
        public Option(Models.Option option)
        {
            this.ID = option.ID;
            this.Name = option.Name;
            this.Votes = option.Votes;
        }

        public Option() { } 
    }

}
