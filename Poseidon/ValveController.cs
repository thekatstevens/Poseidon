using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hypercore.Poseidon
{
    public class ValveController : IDisposable
    {

        private IValve Valves { get; set; }

        public ValveController()
        {
            this.Valves = new Valves();
        }

        public ValveController(IValve Valves)
        {
            this.Valves = Valves;
        }

        public void Schedule(int Valve, string Time)
        {
            Schedule(Valve, ParseTime(Time));
        }

        public void Schedule(int Valve, TimeSpan Duration)
        {
            Valves.TurnOn(Valve);

            Console.WriteLine("Sleeping for " + Duration.ToString());

            Thread.Sleep(Duration);

            Valves.TurnAllOff();
        }

        public void RunTest()
        {
            Console.WriteLine("Testing valve 1");

            Valves.TurnOn(1);

            Thread.Sleep(2000);

            Console.WriteLine("Testing valve 2");

            Valves.TurnOn(2);

            Thread.Sleep(2000);

            Valves.TurnAllOff();

            Console.WriteLine("Test program complete");
        }

        public void AllOff()
        {
            Valves.TurnAllOff();
        }

        private TimeSpan ParseTime(string Time)
        {
            if (Time.ToLower().EndsWith("s"))
            {
                return TimeSpan.FromSeconds(Double.Parse(Time.Replace("s", "")));
            }
            else if (Time.ToLower().EndsWith("m"))
            {
                return TimeSpan.FromMinutes(Double.Parse(Time.Replace("m", "")));
            }
            else if (Time.ToLower().EndsWith("m"))
            {
                return TimeSpan.FromHours(Double.Parse(Time.Replace("h", "")));
            }
            else
            {
                throw new Exception("Could not interpret timespan: " + Time);
            }
        }

        public void Dispose()
        {
            Valves.TurnAllOff();
        }
    }
}
