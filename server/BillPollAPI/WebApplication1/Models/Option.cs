namespace WebApplication1.Models
{
    public class Option
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public int Votes { get; set; }
        public string PollId { get; set; }
        public Poll Poll { get; set; } = null!;
        public Option(API.Option optionDTO, string pollID )
        {
            ID = optionDTO.ID;
            Name = optionDTO.Name;
            Votes = optionDTO.Votes;
            PollId = pollID;
        }

        public Option() { }
    }

}
