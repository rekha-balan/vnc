using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// The key WCF namespace.
using System.ServiceModel;

namespace LiveViewListenerServiceLib
{
  public class LiveViewListenerService : ILiveView
  {
    public LiveViewListenerService()
    {
      //Console.WriteLine("The LiveViewListener awaits your messages ...");
    }
	
    public void DisplayLogEntry(string message)
    {
		Console.WriteLine(message);
      // string[] answers =  { "Future Uncertain", "Yes", "No", 
        // "Hazy", "Ask again later", "Definitely" };

      // // Return a random response.
      // Random r = new Random();
      // return answers[r.Next(answers.Length)];
    }
  }
} 
