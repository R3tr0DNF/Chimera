using System.Collections;
using System.Net;
using Chimera.Models.Feedback;
using Chimera.Services.Conection;
using Chimera.Services.Feedback;

namespace Chimera.Modules
{
    internal class FeedBackTest : IModule
    {
        public string Name => "Feedback Test";

        public void Run(DualShockManager dualShock)
        {
            Console.Clear();

            if(dualShock.Stream == null)
            {
                Console.WriteLine("DualShock stream not available");
                return;
            }
            

            DualShockFeedback feedback = new DualShockFeedback(dualShock.Stream);


            while (true)
            {
                Console.Clear();

                Console.WriteLine("Feedback Test");
                Console.WriteLine();
                Console.WriteLine("1. Large Motor");
                Console.WriteLine("2. Small Motor");
                Console.WriteLine("3. Both Motors");
                Console.WriteLine("4. Stop Motors");
                Console.WriteLine("0. Exit");
                Console.WriteLine();

                Console.Write("Select option: ");

                string? input = Console.ReadLine();

                if (!int.TryParse(input, out int option))
                {
                    continue;
                }

                if (option == 0)
                {
                    break;
                }

                FeedbackState state;

                switch (option)
                {
                    case 1:
                        state = new FeedbackState
                        {
                            LargeMotor = 255,
                            SmallMotor = 0,

                            Red = 0,
                            Green = 0,
                            Blue = 255,

                            FlashOn = 0,
                            FlashOff = 0
                        };
                        break;

                    case 2:
                        state = new FeedbackState
                        {
                            LargeMotor = 0,
                            SmallMotor = 255,

                            Red = 0,
                            Green = 0,
                            Blue = 255,

                            FlashOn = 0,
                            FlashOff = 0
                        };
                        break;
                    
                    case 3:
                        state = new FeedbackState
                        {
                            LargeMotor = 255,
                            SmallMotor = 255,

                            Red = 0,
                            Green = 0,
                            Blue = 255,

                            FlashOn = 0,
                            FlashOff = 0
                        };
                        break;
                    
                    case 4:
                        state = new FeedbackState
                        {
                            LargeMotor = 0,
                            SmallMotor = 0,

                            Red = 0,
                            Green = 0,
                            Blue = 255,

                            FlashOn = 0,
                            FlashOff = 0
                        };
                        break;
                    
                    default:
                        continue;

                }

                feedback.Update(state);

                Console.WriteLine();
                Console.WriteLine("Report sent ...");
                Console.WriteLine("Press any key to continue ...");
                Console.ReadKey(true);
            }
        }
    }
}