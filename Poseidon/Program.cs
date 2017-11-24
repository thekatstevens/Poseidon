using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WiringPi;

namespace Hypercore.Poseidon
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                //Init WiringPi library
                int result = Init.WiringPiSetup();

                if (result == -1)
                {
                    Console.WriteLine("WiringPi init failed!");
                    return;
                }
            }
            catch (Exception e)
            {
#if !DEBUG
                Console.WriteLine("Init failed with error: {0)", e);
                return;
#endif
            }

            using (var Controller = new ValveController(new TestValve()))
            {

                if (args.Length > 0)
                {
                    //set valve state
                    switch (args[0].ToLower())
                    {

                        case "on":

                            var Valve = Int32.Parse(args[1]);

                            Console.WriteLine("Switching on valve {0}", Valve);

                            string Duration;

                            if (args.Length >= 3)
                            {
                                Duration = args[2];
                            }
                            else
                            {
                                Duration = "15m";
                            }

                            Controller.Schedule(Valve, Duration);

                            break;

                        case "off":
                            Controller.AllOff();
                            break;

                        case "test":
                            Controller.RunTest();
                            break;

                        default:
                            Console.WriteLine("Wtf even is that? Press the any key");
                            Console.ReadKey();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Pass me some args, mortal >:( - press the any key");
                    Console.ReadKey();
                }

            }

        
        }

        public void Test()
        {
            var Schedule = new Schedule();


        }
    }
}
