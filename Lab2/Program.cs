using System;
using System.Text;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        double[] alpha = { 2, -2, -1, 7, -5, -3, -3 };  
        double[] beta = { 0, 0.5, 6, 3, 2, 1, 2 };       
        double[] gamma = { 1, 4, 1, 4, 2, 7, 0 };        

        double[] b = { 8, 6, 3, -2, 4, 7, -0.5 }; 

        double[] l, u;

        CalculateLU(alpha, beta, gamma, out l, out u);
      
        double[] y = ForwardSubstitution(l, beta, b);

        double[] x = BackwardSubstitution(u, y);

        PrintSolution(x);
        CheckSolution(x);
    }

    static void CalculateLU(double[] alpha, double[] beta, double[] gamma, out double[] l, out double[] u)
    {
        l = new double[alpha.Length];
        u = new double[alpha.Length - 1];

        l[0] = alpha[0];
        u[0] = gamma[0] / l[0];

        for (int k = 1; k < alpha.Length; k++)
        {
            l[k] = alpha[k] - beta[k] * u[k - 1];
            if (k < alpha.Length - 1)
            {
                u[k] = gamma[k] / l[k];
            }
        }
    }

    static double[] ForwardSubstitution(double[] l, double[] beta, double[] b)
    {        
        double[] y = new double[l.Length];

        y[0] = b[0] / l[0];
        for (int i = 1; i < l.Length; i++)
        {
            y[i] = (b[i] - beta[i] * y[i - 1]) / l[i];
        }

        return y;
    }

    static double[] BackwardSubstitution(double[] u, double[] y)
    {
        int n = y.Length;
        double[] x = new double[n];

        x[n - 1] = y[n - 1];
        for (int i = n - 2; i >= 0; i--)
        {
            x[i] = y[i] - u[i] * x[i + 1];
        }

        return x;
    }

    static void PrintSolution(double[] x)
    {
        Console.WriteLine("Розв'язок системи:");
        for (int i = 0; i < x.Length; i++)
        {
            Console.WriteLine($"x[{i + 1}] = {x[i]:F4}");
        }
    }
    static void CheckSolution(double[] x)
    {
        double eq1 = 2 * x[0] + 1 * x[1];
        double eq2 = 0.5 * x[0] - 2 * x[1] + 4 * x[2];
        double eq3 = 6 * x[1] - 1 * x[2] + 1 * x[3];
        double eq4 = 3 * x[2] + 7 * x[3] + 4 * x[4];
        double eq5 = 2 * x[3] - 5 * x[4] + 2 * x[5];
        double eq6 = 1 * x[4] - 3 * x[5] + 7 * x[6];
        double eq7 = 2 * x[5] - 3 * x[6];

        double[] leftSides = { eq1, eq2, eq3, eq4, eq5, eq6, eq7};
        double[] rightSides = { 8, 6, 3, -2, 4, 7, -0.5 };

        Console.WriteLine("\nПеревірка:");
        for (int i = 0; i < 7; i++)
        {
            double diff = Math.Abs(leftSides[i] - rightSides[i]);
            Console.WriteLine($"Р-ння {i + 1}: ліва частина = {leftSides[i]}, права частина = {rightSides[i]}");
        }
    }

}
