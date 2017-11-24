using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiringPi;

namespace Hypercore.Poseidon
{

    public interface IValve
    {
        void TurnOn(int Valve);

        void SetMask(int Mask);

        //turn all valves off
        void TurnAllOff();
    }

    public class TestValve : IValve
    {

        public void TurnOn(int Valve)
        {
            Console.WriteLine("Valve {0} turned on", Valve);
        }

        public void SetMask(int Mask)
        {
            Console.WriteLine("Mask set: {0}", Mask);
        }

        public void TurnAllOff()
        {
            Console.WriteLine("All valves turned off");
        }
    }

    public class Valves : IValve
    {
        private const int SR_CLK_PIN = 7;
        private const int SR_NOE_PIN = 0;
        private const int SR_DAT_PIN = 2;
        private const int SR_LAT_PIN = 3; 

        public Valves()
        {
            GPIO.pinMode(SR_CLK_PIN, 1);
            GPIO.pinMode(SR_NOE_PIN, 1);
            GPIO.digitalWrite(SR_NOE_PIN, 1);
            GPIO.pinMode(SR_DAT_PIN, 1);
            GPIO.pinMode(SR_LAT_PIN, 1);

            ShiftOut(0);

            GPIO.digitalWrite(SR_NOE_PIN, 0);
        }

        //turn on a specific valve number
        public void TurnOn(int Valve)
        {
            int Mask = 1;

            for (int i = 1; i < Valve; i++)
            {
                Mask = Mask << 1;
            }

            ShiftOut(Mask);

            Console.WriteLine(String.Format("Station {0} On", Valve));
        }

        public void SetMask(int Mask)
        {
            ShiftOut(Mask);
        }

        //turn all valves off
        public void TurnAllOff()
        {
            ShiftOut(0);

            Console.WriteLine("All Stations Off");
        }

        private void ShiftOut(int data)
        {

            for (int i = 0; i < 8; i++)
            {
                /* Write bit to data port. */
                if (0 == (data & (1 << (7 - i))))
                {
                    GPIO.digitalWrite(SR_DAT_PIN, 0);
                }
                else
                {
                    GPIO.digitalWrite(SR_DAT_PIN, 1);
                }

                /* Pulse clock input to write next bit. */
                GPIO.digitalWrite(SR_CLK_PIN, 0);
                GPIO.digitalWrite(SR_CLK_PIN, 1);
            }

            /* Pulse latch to transfer data from shift registers */
            /* to storage registers. */
            GPIO.digitalWrite(SR_LAT_PIN, 0);
            GPIO.digitalWrite(SR_LAT_PIN, 1);

        }


    }
}
