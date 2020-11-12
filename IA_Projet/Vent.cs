using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IA_Projet
{
    class Vent
    {

        public static int _direction;
        public static int _vitesse;

        public static int GetVitesseVent(double y)
        {

            string cas = MainWindow._cas;

            if (cas == "a")
            {
                _vitesse = 50;
            }
            else if (cas == "b" || cas == "c")
            {
                if (y > 150)
                    _vitesse = 50;
                else
                    _vitesse = 20;
            }

            return _vitesse;
        }

        public static int GetDirectionVent(double y)
        {
            string cas = MainWindow._cas;

            if (cas == "a")
                _direction = 30;
            else if (cas == "b")
            {
                if (y > 150)
                    _direction = 180;
                else
                    _direction = 90;
            }
            else if (cas == "c")
            {
                if (y > 150)
                    _direction = 170;
                else
                    _direction = 65;
            }

            return _direction;
        }

    }
}
