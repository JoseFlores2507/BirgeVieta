using System;

namespace Birge_Vieta
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("METODO DE BIRGE-VIETA");
            Console.WriteLine("Ingresa el grado del polinomio: ");
            byte grado = ReadByte();
            double[] polinomio = new double[grado+1];
            IngresarPolinomio(polinomio, grado);
            CambiarPolinomio(polinomio, grado);
            ProcesoBirge(polinomio, grado);
            Console.ReadKey();
        }

        static void ProcesoBirge(double[] a, byte grado)
        {
            Console.WriteLine("Cuantos 0's quieres de tolerancia?\n(Ejemplo: 0.0001 = 4 ceros):");
            byte error = ReadByte();
            byte num_R = 0;
            int band = 0;
            int val = 0;
            double[] P = new double[grado + 1];
            double[] P_raya = new double[grado];
            double[] raiz = new double[grado];
            double xi = -a[0] / a[1];
            band = a[1] == 0 ? -2 : band;
            if(band == 0)
            {
                do
                {
                    if (grado == 2 && (a[1] * a[1] - (4 * a[0] * a[2])) < 0)
                    {
                        band = -4; //determinante
                        break;
                    }
                    P[grado] = 1;
                    P_raya[grado - 1] = 1;
                    for (int i = grado - 1; i >= 0; i--)
                    {
                        P[i] = xi * P[i + 1] + a[i];
                    }
                    for (int i = grado - 2; i >= 0; i--)
                    {
                        P_raya[i] = xi * P_raya[i + 1] + P[i + 1];
                    }
                    double xi1 = xi + (-P[0] / P_raya[0]);
                    ImprimirBirge(a, P, P_raya, grado, xi);
                    double temp;
                    if (Math.Abs(P[0]) <= (Math.Pow(10, -(error))) || Math.Abs(P[0]) == 0)
                    {
                        if (grado == 2)
                        {
                            raiz[num_R] = xi;
                            num_R++;
                            raiz[num_R] = -P[1];
                            num_R++;
                            band = -1;
                        }
                        else
                        {
                            raiz[num_R] = xi;
                            num_R++;
                            grado--;
                            //Array.Reverse(P);
                            NuevoA(a, P, grado);
                            Console.ReadKey();
                            xi = -a[0] / a[1];
                            if (double.IsNaN(xi))
                            {
                                band = -2;
                            }
                            if (grado == 2)
                            {
                                band = (a[1] * a[1] - (4 * a[0] * a[2])) < 0 ? -4 : band; //determinante
                            }
                        }
                        val = 0;
                    }
                    else
                    {
                        xi = xi1;
                        if (double.IsNaN(xi))
                        {
                            band = -2;
                        }
                        temp = P[0];
                    }
                    val++;
                    if (val > error * error + 9)
                    {
                        band = -3;
                    }
                } while (band >= 0);
            }
            ImprimirRaiz(raiz, num_R);
            if (band <= -2)
            {
                Console.WriteLine("La ecuación no se puede resolver usando este metodo");
                switch (band)
                {
                    case -3: Console.WriteLine("(Limite de iteraciones alcanzado)");
                        break;
                    case -4: Console.WriteLine("(Las soluciones restantes no forman parte de los reales)");
                        break;
                    default:
                        break;
                }
            }
        }

        static void NuevoA(double[] a, double[] P, byte grado)
        {
            Console.WriteLine("[El polinomio disminuye de grado]\n");
            for(int i = grado; i>=0; i--)
            {
                a[i] = P[i+1];
            }
        }

        static void ImprimirBirge(double[] a, double[] p, double[] p_raya, byte grado, double xi)
        {
            Console.Write(String.Format("{0,-7:0.###}", xi) + "\t");
            for(int i = grado; i>= 0; i--)
            {
                Console.Write(String.Format("{0,-7:0.###}", a[i]) + "\t");
            }
            Console.WriteLine();
            Console.Write("+\t" + "\t");
            for(int i = grado -1; i >= 0; i--)
            {
                Console.Write(String.Format("{0,-7:0.###}", xi * p[i + 1]) + "\t");
            }
            Console.WriteLine();
            Console.Write("\t");
            for(int i = grado; i >= 0; i--)
            {
                Console.Write(String.Format("{0,-7:0.###}", p[i]) + "\t");
            }
            Console.WriteLine();
            Console.Write("+\t" + "\t");
            for(int i = grado - 2; i>= 0; i--)
            {
                Console.Write(String.Format("{0,-7:0.###}", xi * p_raya[i + 1]) + "\t");
            }
            Console.WriteLine();
            Console.Write("\t");
            for(int i = grado -1; i >= 0; i--)
            {
                Console.Write(String.Format("{0,-7:0.###}", p_raya[i]) + "\t");
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        static void ImprimirRaiz(double[] raiz, byte num)
        {
            if (num != 0)
            {
                Console.WriteLine("Las raíces de la ecuación son");
            }
            for (int i = 0; i < num; i++)
            {
                Console.WriteLine("x" + (i + 1) + "\t=\t" + (String.Format("{0,-7:0.#######}", raiz[i])));
            }
        }

        static void ImprimirPolinomio(double[] polinomio, byte grado, int x)
        {
            for (int i = grado; i >= 0; i--)
            {
                if (i == x)
                {
                    if (i == grado)
                    {
                        Console.Write("x^" + i + " ");
                    }
                    else if (i != 0)
                    {
                        Console.Write(string.Format("{0:+ [0.#####];[- 0.#####]}", polinomio[i]) + "x^" + i + " ");
                    }
                    else
                    {
                        Console.WriteLine(string.Format("{0:+ [0.#####];[- 0.#####]}", polinomio[i]));
                    }
                }
                else
                {
                    if (i == grado)
                    {
                        Console.Write("x^" + i + " ");
                    }
                    else if (i != 0)
                    {
                        Console.Write(string.Format("{0:+ 0.#####;- 0.#####}", polinomio[i]) + "x^" + i + " ");
                    }
                    else
                    {
                        Console.WriteLine(string.Format("{0:+ 0.#####;- 0.#####}", polinomio[i]));
                    }
                }
            }
        }
        static void ImprimirPolinomio(double[] polinomio, byte grado)
        {
            for (int i = grado; i >= 0; i--)
            {
                if (i == grado)
                {
                    Console.Write("x^" + i + " ");
                }
                else if (i != 0)
                {
                    Console.Write(string.Format("{0:+ 0.#####;- 0.#####}", polinomio[i]) + "x^" + i + " ");
                }
                else
                {
                    Console.WriteLine(string.Format("{0:+ 0.#####;- 0.#####}", polinomio[i]));
                }
            }
        }

        static void IngresarPolinomio(double[] polinomio, byte grado) {
            polinomio[grado] = 1;
            for(int i = grado-1; i>=0; i--)
            {
                ImprimirPolinomio(polinomio, grado, i);
                Console.WriteLine("Ingresa el valor del coeficiente entre ´[]´");
                polinomio[i]= ReadDouble();
                if (i == 0)
                {
                    ImprimirPolinomio(polinomio, grado);
                }
            }
            Console.ReadKey();
            Console.Clear();
        }

        static void CambiarPolinomio(double[] polinomio, byte grado)
        {
            byte x;
            Console.WriteLine("¿Quieres cambiar algun elemento?(y = si): ");
            char cambio = Console.ReadKey().KeyChar;
            while (cambio == 'y' || cambio == 'Y')
            {
                ImprimirCambio(polinomio, grado);
                do
                {
                    Console.WriteLine("¿Que coeficiente quieres cambiar?: ");
                    x = ReadByte();
                } while (x > grado - 1);
                ImprimirPolinomio(polinomio, grado, x);
                Console.WriteLine("Ingresa el nuevo valor del coeficiente elegido: ");
                polinomio[x] = ReadDouble();
                ImprimirPolinomio(polinomio, grado);
                Console.WriteLine("¿Quieres cambiar algun elemento?(y = si): ");
                cambio = Console.ReadKey().KeyChar;
            }
        }

        static void ImprimirCambio(double[] polinomio, byte grado)
        {
            int i;
            Console.WriteLine();
            for (i = grado; i>=0; i--)
            {
                if (i == grado)
                {
                    Console.Write("\t");
                }
                else
                {
                    Console.Write(i + "\t");
                }
            }
            Console.WriteLine();
            for (i = grado; i>= 0; i--)
            {
                if(i == 0)
                {
                    Console.WriteLine(string.Format("{0:+0.#####;-0.#####}", polinomio[i]));
                }
                else if(i == 1)
                {
                    Console.Write(string.Format("{0:+0.#####;-0.#####}", polinomio[i]) + "x" + "\t");
                }
                else
                {
                    Console.Write(string.Format("{0:+0.#####;-0.#####}", polinomio[i]) + "x^" + i + "\t");
                }
            }
        }

        static byte ReadByte()
        {
            string num;
            byte valido;
            num = Console.ReadLine();
            if (!byte.TryParse(num, out valido)
                || string.IsNullOrEmpty(num))
            {
                ClearLine(1);
            }
            while (!byte.TryParse(num, out valido)
                || string.IsNullOrEmpty(num))
            {
                num = Console.ReadLine();
                ClearLine(1);
            }
            return valido;
        }

        static double ReadDouble()
        {
            string num;
            double valido;
            num = Console.ReadLine();
            if (!double.TryParse(num, out valido)
                || string.IsNullOrEmpty(num))
            {
                ClearLine(1);
            }
            while (!double.TryParse(num, out valido)
                || string.IsNullOrEmpty(num))
            {
                num = Console.ReadLine();
                ClearLine(1);
            }
            return valido;
        }

        static void ClearLine(int n)
        {
            for (int i = 0; i < n + 1; i++)
            {
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, Console.CursorTop - 1);
            }
            Console.WriteLine("");
        }

    }
}
