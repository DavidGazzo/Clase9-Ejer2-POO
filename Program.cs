/* REQUERIMIENTOS:
Con los conocimientos vistos hasta ahora en clase realizar un programa que haga lo siguiente:
Generar un programa que cree un cartón de bingo aleatorio y lo muestre por pantalla
1)    Cartón de 3 filas por 9 columnas
2)    El cartón debe tener 15 números y 12 espacios en blanco
3)    Cada fila debe tener 5 números
4)    Cada columna debe tener 1 o 2 números
5)    Ningún número puede repetirse
6)    La primer columna contiene los números del 1 al 9, la segunda del 10 al 19, la tercera del 20 al 29,
      así sucesivamente hasta la última columna la cual contiene del 80 al 90
7)    Mostrar el carton por pantalla

DISEÑO:     Generar un vector con cant de nros que iran en cada columna, iterar hasta conseguir 15 en total.
            Luego generar nros por columna(decena), guardar en vector aux, ordenar mayor y menor si son dos.
            Guardar en matriz cartonBingo, ubicarlos (1ra,2da,3ra fila) aleatoriamente.
            Imprimir cartón
 */
// NOTA: Todos los métodos que utilicen el objeto 'Console' se encuentra en ésta clase BingoCardGenerator.
// En las clases Tools y CardOpertions no se utilizan objetos 'Console'. Ergo podrán ser usadas en aplicaciones gráficas


namespace Clase9_Ejer2_POO
{
    class BingoCardGenerator
    {
        static void Main (string[] args)
        {
            var myTools = new Tools();
            var nuevaTarjeta = new CardOperations();
            int[,] BingoCard = new int[3, 9]; // Matriz de 3 filas por 9 columnas Cartón de Bingo
            string fact = "";
            bool isNumber = false;

            // Desde aca comienza todo el proceso
            do {
                Console.Clear();
                do  // Presento programa y Solicito cantidad de tarjetas que se generarán
                {                    
                    Console.WriteLine(myTools.PresentationPoster("        Generador de Bingo        ")); // Muestra cartel inicio
                    Console.Write("¿Cuántas tarjetas de bingo desea generar?: ");
                    fact = Console.ReadLine();
                    isNumber = myTools.ctrlNumber(fact);    // Controla que el dato ingresado sea un caracter válido
                    if ((isNumber == false) || (int.Parse(fact) == 0))  // No se permiten ni letres, ni 0(cero) ni alfanumérico
                    {
                        Console.WriteLine("Debe ingresar un número mayor que 0(cero).\n Presione ENTER para intentar nuevamente...");
                        Console.ReadKey();
                        Console.Clear();
                    }
                } while ((isNumber != true) || (int.Parse(fact) == 0));

                int cantCards = int.Parse(fact);

                do // Genero números de la matriz BingoCard e imprimo tarjetas
                {
                    BingoCard = CardOperations.Main();  // Genero y controlo matriz de la nueva tarjeta
                    PrintCard(BingoCard);               // Imprimo tarjeta en pantalla
                    cantCards--;                        // Se descuenta tarjeta generada/impresa hasta cumplir con las tarjetas solicitadas
                } while (cantCards != 0);

                Console.Write("\n¿Desea generar mas tarjetas de bingo? S/N : ");                

            } while (myTools.OnlyYesNo(Console.ReadLine()));
        } // End Main

        static void PrintCard(int[,] BingoCard)
        {
            // Generar y Mostrar cartón de bingo

            Random color = new Random();        // Para color aleatorio de tarjetas
            int nroColor = color.Next(1, 7);    // Guarda valor para seleccionar en switch de color
            Console.WriteLine("\t\t\t╔══╦══╦══╦══╦══╦══╦══╦══╦══╗");
            for (int fila = 0; fila < 3; fila++)
            {
                for (int columna = 0; columna < 9; columna++)
                {
                    int valorNro = BingoCard[fila, columna];
                    if (valorNro < 10)                      // Imprime por filas
                    {
                        if (valorNro == 00)
                        {   // Si es nro 0(cero) imprime cuadro sombreado
                            if (columna == 0)
                            {
                                Console.Write("\t\t\t║");
                            }
                            else
                            {
                                Console.Write("║");
                            }

                            switch (nroColor)   // SWITCH DE COLOR
                            {   // Colores de los cuadros sombreados (espacios sin números)
                                case 1:
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    break;
                                case 2:
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    break;
                                case 3:
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    break;
                                case 4:
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    break;
                                case 5:
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    break;
                                case 6:
                                    Console.ForegroundColor = ConsoleColor.Magenta;
                                    break;
                            }
                            Console.Write("██");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.Write($"\t\t\t║0{valorNro}");     // Si es nro de una cifra agrega un 0(cero)
                        }
                    }
                    else
                    {
                        Console.Write($"║{BingoCard[fila, columna]}");    // Si es nro de 2 cifras imprime directamente
                    }
                }
                Console.WriteLine("║"); // Barra vertical final para cada fila
                if (fila < 2)
                {
                    Console.WriteLine("\t\t\t╠══╬══╬══╬══╬══╬══╬══╬══╬══╣");
                }
                else if (fila == 2)
                {
                    Console.WriteLine("\t\t\t╚══╩══╩══╩══╩══╩══╩══╩══╩══╝");
                }
            }
        }// End PrintCard

    }// End class BingoGenerator
} // End namespace
