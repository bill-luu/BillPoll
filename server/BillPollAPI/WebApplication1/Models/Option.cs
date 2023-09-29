namespace WebApplication1.Models
{
    public class Option
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Votes { get; set; }
        public int PollId { get; set; }
        public Poll Poll { get; set; } = null!;
        public Option(API.Option optionDTO, int pollID )
        {
            Id = optionDTO.ID;
            Name = optionDTO.Name;
            Votes = optionDTO.Votes;
            PollId = pollID;
        }

        public Option() { }
    }

}
