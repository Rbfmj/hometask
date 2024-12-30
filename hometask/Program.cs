using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAutopilotTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Monte Carlo Simulation for Car Auto-pilot");
            Random r = new Random();
            int n = 10000;

            double maxPermissibleError = 5.0;

            List<double> errors = new List<double>();
            int exceedingErrorCount = 0;

            for (int i = 0; i < n; i++)
            {
                double error = Simulate(r);
                errors.Add(error);
                if (error > maxPermissibleError)
                {
                    exceedingErrorCount++;
                }
            }

            double rmse = Math.Sqrt(errors.Select(e => e * e).Sum() / n);


            Console.WriteLine($"Simulations: {n}");
            Console.WriteLine($"Maximum Permissible Error: {maxPermissibleError:F2}");
            Console.WriteLine($"Errors Exceeding Threshold: {exceedingErrorCount}");
            Console.WriteLine($"Proportion Exceeding Threshold: {(double)exceedingErrorCount / n:P2}");
            Console.WriteLine($"RMSE: {rmse:F4}");
        }
        static double Simulate(Random r)
        {
            //Target steering angle (-10 to +10 degrees)
            double targetSteering = r.NextDouble() * 20 - 10;

            //System behavior: adds bias (-1 to +1) and noise (-2 to +2)
            double systemBias = r.NextDouble() * 2 - 1;
            double randomNoise = r.NextDouble() * 4 - 2;
            double actualSteering = targetSteering + systemBias + randomNoise;

            return Math.Abs(targetSteering - actualSteering);
        }
    }
}
