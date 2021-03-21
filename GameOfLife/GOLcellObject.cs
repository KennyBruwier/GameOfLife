using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class GOLcellObject
    {
        public string Naam { get; set; }
        public bool CurrentAlive { get; set; } = false;
        virtual public char Alive { get; set; } = '☺';
        virtual public char Dead { get; set; } = ' ';
        virtual public ConsoleColor Kleur { get; set; } = ConsoleColor.Black;

        public GOLcellObject(bool aLive)
        {
            CurrentAlive = aLive;
        }
        public GOLcellObject()
        {

        }
    }

    class GOLcellPlayer : GOLcellObject
    {
        private char alive { get; set; } = '☺';
        public override char Alive { get => base.Alive; set => base.Alive = alive; } 
        public override ConsoleColor Kleur { get => base.Kleur; set => base.Kleur = value; }

        public GOLcellPlayer(bool bAlive)
        {
            CurrentAlive = bAlive;
            Kleur = ConsoleColor.Red;
        }
    }
}
