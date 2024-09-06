using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public class Flashcards
    {
        int FlashcardId { get; set; }
        string Front { get; set; }
        string Back { get; set; }
        int StackId { get; set; }
    }

    public class Stack
    {
        int StackId { get; set; }  
        string StackName { get; set; }
    }

}
