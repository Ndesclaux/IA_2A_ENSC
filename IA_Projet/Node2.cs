using System;
using System.Collections.Generic;
using System.Diagnostics;
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


        public Node2(double x, double y)
        {
            _x = x;
            _y = y;
        }

        // Méthodes abstrates, donc à surcharger obligatoirement avec override dans une classe fille
        public override bool IsEqual(GenericNode N2)
        {
            Node2 N2bis = (Node2)N2;

            if (N2bis.X == X && N2bis.Y == Y)
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
           // Debug.WriteLine("----------- Calcul de G --------------");
            Node2 N2bis = (Node2)N2;

            int vitesseVent = Vent.GetVitesseVent(Y);
            int directionVent = Vent.GetDirectionVent(Y);

            double deltaX = Math.Pow((X - N2bis.X), 2);
            double deltaY = Math.Pow((Y - N2bis.Y), 2);

            double dist = Math.Sqrt(deltaX + deltaY);


            double angle = Math.Acos(Math.Sqrt(deltaY) / dist) * 180 / Math.PI;
            int directionBateau = Convert.ToInt32(Math.Round(angle));
           // Debug.WriteLine("Direction du bateau (G) : {0}°", directionBateau);

            int alpha = Convert.ToInt32(Math.Abs(directionVent - directionBateau));
            double vitesseBateau = GetBoatSpeed(vitesseVent, alpha);

           // Debug.WriteLine("----------- Calcul de G FIN --------------");

            return dist/vitesseBateau;
        }

        /// <summary>
        /// Détermine si le noeud est le noeud d'arrivé.
        /// </summary>
        /// <returns>Vrai si le noeud est terminal, Faux sinon.</returns>
        public override bool EndState()
        {
            //Attention à la comparaison entre 2 doubles !!  
            if (X == MainWindow._xEnd && Y == MainWindow._yEnd)
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

            //On se sert de dist pour incrémenter nos boucles ==> Correspond à la distance entre 2 node
            double dist = MainWindow._distNode;
            int nbVoisin = MainWindow._nbNodeVoisin;

            double xDebut = X - dist * nbVoisin;
            double xFin = X + dist * nbVoisin;

            double yDebut = Y - dist * nbVoisin;
            double yFin = Y + dist * nbVoisin;


            /*
             * Ici il faudra générer les noeuds voisin du noeud courant
             */

            for (double x = xDebut; x <= xFin; x += dist)
            {
                for (double y = yDebut; y <= yFin; y += dist)
                {
                    if ((x != X || y != Y) &&
                        (x >= 0 && y >= 0 && x <= MainWindow.GRID_SIZE && y <= MainWindow.GRID_SIZE))
                    {
                        //Debug.WriteLine("Point ajouté en ({0},{1})", x,y);
                        lsucc.Add(new Node2(x, y));
                    }
                }
            }
            return lsucc;
        }

        /// <summary>
        /// Renvoie une estimation du temps de trajet entre le noeud et l'arrivée.
        /// </summary>
        /// <returns>Renvoie le temps minimum de trajet entre les 2 points</returns>
        public override double CalculeHCost()
        {
           // Debug.WriteLine("----------- Calcul de H --------------");

            if (this.EndState())
                return 0;
            double h = 0.0;

            int vitesseVent = Vent.GetVitesseVent(Y);
            double xEnd = MainWindow._xEnd;
            double yEnd = MainWindow._yEnd;


            double deltaX = Math.Pow((X - xEnd), 2);
            double deltaY = Math.Pow((Y - yEnd), 2);

            double dist = Math.Sqrt(deltaX + deltaY);

            /*double angle = Math.Acos(Math.Sqrt(deltaY) / dist) * 180 / Math.PI;
            int directionBateau = Convert.ToInt32(Math.Round(angle));
            //Debug.WriteLine("Direction du bateau : {0}°", directionBateau);

            int directionOptiVent = (directionBateau + 45) % 360;*/
            //Debug.WriteLine("Direction du vent : {0}°", directionOptiVent);

            //int alpha = Convert.ToInt32(Math.Abs(directionOptiVent - directionBateau));
            int alpha = 45;
            double vitesseBateau = GetBoatSpeed(vitesseVent, alpha);

            h = dist / vitesseBateau;

           // Debug.WriteLine("----------- Calcul de H FIN --------------");
            return h;
        }

        public double GetBoatSpeed(int windSpeed, int alpha)
        {
            double boatSpeed = 0;

            if (alpha >= 0 && alpha <= 45)
                boatSpeed = (0.6 + (0.3 * alpha) / 45) * windSpeed;
            
            else if (alpha <= 90)
                boatSpeed = (0.9 - (0.2 * (alpha - 45)) / 45) * windSpeed;
            else if (alpha <=150)
                boatSpeed = (0.7*(1-((alpha-90)/60)))*windSpeed;

            return boatSpeed;
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
