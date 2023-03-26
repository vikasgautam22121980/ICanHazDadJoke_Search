using System.Collections.Generic;

namespace ICanHazDadJoke_Search.Model
{
    public class ICanHazDadJoke
    {
        public int Limit { get; set; }
        public List<ICanHazDadJokeItem> Results { get; set; }
        public string Search_Term { get; set; }
        public string Status { get; set; }
        public int Total_Jokes { get; set; }
    }
}
