using System;
class MatrizPa
{
    private int n;
    private int[,] Mz;
    private int suma;

    public MatrizPa(int N)
    {
        this.n = N;
        this.Mz = new int[N, N];
        suma = 0;
    }

    public void CrearMatriz()
    {
        Random rd = new Random();
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (i == j || i + j == n - 1)
                {
                    Mz[i, j] = rd.Next(1, 101);
                }
                else
                {
                    Mz[i, j] = 0;
                }
            }
        }
    }

    public void CalcularSuma()
    {
        suma = 0;
        foreach (int valor in Mz)
        {
            suma += valor;
        }
    }

    public void MostrarMatriz()
    {
        Console.WriteLine("Esta es la Matriz resultante: ");
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                Console.Write(Mz[i, j] + "\t");
            }
            Console.WriteLine();
        }
        Console.WriteLine("La suma de los elementos de la matriz es: " + suma);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Ingrese el tamaño de la matriz: ");
        int N = int.Parse(Console.ReadLine());
        {
            if (N % 2 == 0)
            {
                Console.WriteLine("El número debe ser impar");
                return;
            }

            MatrizPa matriz = new MatrizPa(N);
            matriz.CrearMatriz();
            matriz.CalcularSuma();
            matriz.MostrarMatriz();
        }
    }
}