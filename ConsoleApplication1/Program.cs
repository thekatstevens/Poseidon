using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using WiringPi;

namespace SPITest
{
    class Program
    {



        static void Main(string[] args)
        {

            //Init WiringPi library
            int result = Init.WiringPiSetup();

            if (result == -1)
            {
                Console.WriteLine("WiringPi init failed!");
                return;
            }

            ////Init WiringPi SPI library
            //result = SPI.wiringPiSPISetup(0, 32000000);
            //if (result == -1)
            //{
            //    Console.WriteLine("SPI init failed!");
            //    return;
            //}

            //Console.WriteLine("SPI init completed, using channel 0 at 32MHz for loopback testing");

            Valves Valves = new Valves();

            ShiftOut(1);

            Thread.Sleep(2000);


            Thread.Sleep(2000);

            ShiftOut(0);

            Console.WriteLine("Shift Complete");

        }

        
    }

}
