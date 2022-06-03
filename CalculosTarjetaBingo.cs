using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clase9_Ejer2_POO
{
    public class CardOperations     // Genera los numeros para cada tarjeta
                                    // con 9 columnas, 1 ó 2 nros por column,5 números por row,
                                    // los números no se repiten y están ordenados en cadaa column por decena
                                    // y si si¡on dos nros en la column el menor va en row superior
    {
        public static int[,] Main()
        {
            int[,] CartonBingo = new int[3,9];
            //bool generateMatriz = true;
            int[] numros = NumberXColumn();
            GeneraNummbers(ref CartonBingo,numros);
                        
            return CartonBingo;
        }   // End Main
        
        public static int[] NumberXColumn()
        {   
            // Devuelve un vector con la cant de nros por column en un total de 15 nros
            int addition = 0;
            var evenOdd = new Random();
            int[] vectorNumbersXColumns = new int[9];
            do      // Itera hasta conseguir una combinación con 15 números
            {
                for (int i = 0; i < vectorNumbersXColumns.Length; i++)
                {
                    vectorNumbersXColumns[i] = evenOdd.Next(1, 3);   // Genera 2 ó 1 nro por column
                }
                addition = 0;
                foreach (var item in vectorNumbersXColumns)
                {
                    addition += item;       // Luego de generado el vector addition sus ítems
                }

            } while (addition != 15);       // Cuando sean 15 sale de bucle

            return vectorNumbersXColumns;
        }   // End NumberXColumn

        public static int[,] GeneraNummbers(ref int[,] CartonBingo,int[] NumberXColumn)
        {
            int contIntentos = 0;
            bool hacerNuevoCarton = false;
            // Genera los 15 numeros del carton
            do
            {             
                contIntentos++;
                Random numeros = new Random();
                int posicionMatriz = 0;         // para posición en column de matriz
                int decenaInicio = 1;           // para cálculo de las decenas
                int decenaFin = 10;             // para cálculo de las decenas
                int[] columnasAux = new int[3]; // guarda nros aleatorios de 1 column

                // Lugares en column
                Random random = new Random();
                int ubicacion = 0;              // Para colocar en 1ra,2da,3ra row
                int cargarNro = 0;              // Aux para ordenar nros de mayor a menor
                int cargarNro2 = 0;             // Aux para ordenar nros de mayor a menor

                foreach (var item in NumberXColumn)
                {
                    switch (item)
                    {
                        case 1:
                            ubicacion = random.Next(1, 3);
                            cargarNro = numeros.Next(decenaInicio, decenaFin);  // Genera nro según decena
                            switch (ubicacion)              // Decide lugar entre 3 filas (para 1 solo nro en la column)
                            {
                                case 1:                     //nro,0,0
                                    columnasAux[0] = cargarNro;
                                    columnasAux[1] = 00;
                                    columnasAux[2] = 00;
                                    break;
                                case 2:                     //0,nro,0
                                    columnasAux[0] = 00;
                                    columnasAux[1] = cargarNro;
                                    columnasAux[2] = 00;
                                    break;
                                case 3:                     //0,0,nro
                                    columnasAux[0] = 00;
                                    columnasAux[1] = 00;
                                    columnasAux[2] = cargarNro;
                                    break;
                            }
                            break;

                        case 2:   // Para 2 nros por column
                            ubicacion = random.Next(1, 3);
                            bool nrosIguales = true;
                            while (nrosIguales)     // Itera generando dos nros, hasta conseguir dos diferentes y ordenados menor y mayor                                    
                            {
                                cargarNro = numeros.Next(decenaInicio, decenaFin);  // Genera 1er nro segun decena
                                cargarNro2 = numeros.Next(decenaInicio, decenaFin); // Genera 2do nro segun decena
                                if (cargarNro < cargarNro2)     // Solo entra si están ordenados menor-mayor...
                                {
                                    nrosIguales = false;        // ...para salir del bucle
                                }
                            }
                            switch (ubicacion)// Decide lugar entre 2 lugares
                                              // y luego entre los 2 restante si el nro quedo en row 0
                            {
                                case 1:     // Ubicar 1er nro en row 1 ó 2, ergo el 2do en row 2 ó 3
                                    columnasAux[0] = cargarNro; // nro,?,?
                                    ubicacion = random.Next(1, 3);
                                    //cargarNro = numeros.Next(decenaInicio, decenaFin);
                                    if (ubicacion == 1)           // nro,nro,0
                                    {
                                        columnasAux[1] = cargarNro2;
                                        columnasAux[2] = 00;
                                    }
                                    else if (ubicacion == 2)    // nro,0,nro
                                    {
                                        columnasAux[1] = 00;
                                        columnasAux[2] = cargarNro2;
                                    }
                                    break;

                                case 2:
                                    ubicacion = random.Next(1, 2);  // 0,nro,nro
                                                                    //cargarNro = numeros.Next(decenaInicio, decenaFin);
                                    columnasAux[0] = 0;
                                    columnasAux[1] = cargarNro;
                                    //cargarNro = numeros.Next(decenaInicio, decenaFin);
                                    columnasAux[2] = cargarNro2;
                                    break;
                            }
                            break;
                    }
                    for (int filas = 0; filas < 3; filas++)     // Va guardando en "el cartón" cada una de las columnas generadas
                    {
                        CartonBingo[filas, posicionMatriz] = columnasAux[filas];
                    }
                    posicionMatriz++;               // Avanza en las columnas
                    decenaFin = decenaFin + 10;      // Prepara indicadores de decena
                    decenaInicio = decenaFin - 9;
                }
                hacerNuevoCarton = CtrlFiveInFile(CartonBingo, contIntentos);
            } while (hacerNuevoCarton);
            return CartonBingo;
        }   // End GeneraNummbers

        public static bool CtrlFiveInFile(int[,] CartonBingo, int contIntentos)
        {
            // controlar que sean 5 numeros por fila
            bool generateMatriz = false;    // True si hay que generar nueva tarjeta (hay más de 5nros en alguna fila)
            int filas = 0;                  // Para salir si tarjeta OK
            bool rowOk = true;              // Mientras la fila controlada este bien
            while (rowOk&&filas<4)          
            {         
                for (int row = 0; row < 3; row++)
                {
                    filas++;
                    int counter = 0;
                    for (int column = 0; column < 9; column++)
                    {
                        int value = CartonBingo[row, column];
                        if (value != 0) counter++;  // Suma 1 si el valor es distinto de 0(cero)
                    }
                    if (counter != 5)   // Si no cumple condicion de 5 nros en una fila
                    {
                        rowOk = false;          // La fila es incorrecta
                        generateMatriz = true;  // Debe generarse nueva tarjeta
                        break;
                    }                        
                }                
            }            
            return generateMatriz;  // True si hay que generar nueva tarjeta (hay más de 5nros en alguna fila)
        }   // End CtrlFiveInFile

    }   // End class CardOperations
}   // End namespace Clase9_Ejer2_POO