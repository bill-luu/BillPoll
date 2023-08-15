using Microsoft.Extensions.Options;
using System.Xml.Linq;
using WebApplication1.Models;

namespace WebApplication1.API
{
    public class Poll
    {
        public string Id { get; set; }
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

        public Poll(string id, string name, ICollection<Option> options)
        {
            Id = id;
            Name = name;
            Options = options;
        }

        public Poll() { }
    }

}
