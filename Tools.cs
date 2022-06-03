using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clase9_Ejer2_POO
{
    public class Tools
    {
        //public string legend = "";

        public string PresentationPoster(string legend)
        {
            // Genera cartel adaptandose al texto recibido
            int longer = legend.Length;
            string tittle = "";
            string space = " ";
            string horizBar = new string('─',longer+8);
            string vertBar = "│";
            string supIzq = "┌";
            string supDer = "┐";
            string infIzq = "└";
            string infDer = "┘";
            string margenSpace = new string(' ',4);
            string prefix = vertBar + margenSpace;
            string suffix = margenSpace+vertBar;

            tittle = supIzq + horizBar + supDer +"\n";
            tittle += prefix + legend + suffix + "\n";
            tittle += infIzq + horizBar + infDer + "\n";
            
            return tittle;
        }   //  End PresentationPoster

        public bool ctrlNumber(string fact)
        {
            bool inNumber = Int32.TryParse(fact, out _);
            return inNumber;
        }   // End CtrlNumber

        public bool OnlyYesNo(string response)
        {
            bool responseYes = false;
            CtrlString(response);
            
            if (response.ToUpper() == "S")
            {
                responseYes = true;          // La respuesta es SI
            }
            else if (response.ToUpper() == "N")
            {
                responseYes = false;          // La respuesta es NO

            }
            return responseYes;
        }   // End OnlyYesNo

        public bool CtrlString(string response)
        {
            bool isstring = false;
            for (int i = 0; i < response.Length; i++)
            {
                isstring = char.IsLetter(response[i]);
            }            
            return isstring;
        }   // End CtrlString
    }
}
