using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IA_Projet
{
    class Node2 : GenericNode
    {
        private double _x;
        private double _y;


        //public int numero;

        // Méthodes abstrates, donc à surcharger obligatoirement avec override dans une classe fille
        public override bool IsEqual(GenericNode N2)
        {
            Node2 N2bis = (Node2)N2;

            if (N2bis.X == _x && N2bis.Y == _y)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Calcule le coût de réel de déplacement.
        /// Doit renvoyer la vitesse du bateau en fonction de sa direction et de la direction du vent.
        /// </summary>
        /// <param name="N2"></param>
        /// <returns>La vitesse du bateau</returns>
        public override double GetArcCost(GenericNode N2)
        {
            Node2 N2bis = (Node2)N2;
            //return FormAstarGrapheStatique.matrice[numero, N2bis.numero];
            return 0;
        }

        /// <summary>
        /// Détermine si le noeud est le noeud d'arrivé.
        /// </summary>
        /// <returns>Vrai si le noeud est terminal, Faux sinon.</returns>
        public override bool EndState()
        {
         //Attention à la comparaison entre 2 doubles !!  
           if (_x == MainWindow._xEnd && _y == MainWindow._yEnd)
               return true;
           else
               return false;

        }

        /// <summary>
        /// Renvoie la liste des successeurs
        /// </summary>
        /// <returns></returns>
        public override List<GenericNode> GetListSucc()
        {
            List<GenericNode> lsucc = new List<GenericNode>();

            /*
             * Ici il faudra générer les noeuds voisin du noeud courant
             */

            // TODO

            /*for (int i = 0; i < FormAstarGrapheStatique.nbnodes; i++)
            {
                if (FormAstarGrapheStatique.matrice[numero, i] != -1)
                {
                    Node2 newnode2 = new Node2();
                    newnode2.numero = i;
                    lsucc.Add(newnode2);
                }
            }*/
            return lsucc;
        }

        /// <summary>
        /// Renvoie une estimation de la distance entre le noeud et l'arrivée.
        /// </summary>
        /// <returns>La distance à vol d'oiseau entre le point et le point final</returns>
        public override double CalculeHCost()
        {
            double deltaXPow = Math.Pow(_x - MainWindow._xEnd, 2);
            double deltaYPow = Math.Pow(_y - MainWindow._yEnd, 2);

            return (Math.Sqrt(deltaXPow+deltaYPow));
        }

        public override string ToString()
        {
            return "";
        }

        public double X
        {
            get => _x;
            set => _x = value;
        }

        public double Y
        {
            get => _y;
            set => _y = value;
        }
    }
}
