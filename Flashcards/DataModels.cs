using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public class Flashcard
    {
        public int FlashcardId { get; set; }
        public string Front { get; set; }
        public string Back { get; set; }
        public int StackId { get; set; }
        public Stack Stack { get; set; }
    }

    public class Stack
    {
        public int StackId { get; set; }  
        public string StackName { get; set; }
        public ICollection<Flashcard> Flashcards { get; set; }
    }

    public class FlashcardDTO
    {
        public int FlashcardId { get; set; }
        public string Front { get; set; }
        public string Back { get; set; }

    }

}
