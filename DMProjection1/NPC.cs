using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMProjection1
{
    public class NPC : Entity
    {
        public string name;
        public int hp;
        public int ac;
        public int toHitBonus;
        public int attackDiceNum;
        public int attackDiceType;
        public int attackBonus;

        public NPC()
        {

        }

        public NPC(string _name, int _hp, int _ac, int thb, int adn, int adt, int ab)
        {
            name = _name;
            hp = _hp;
            ac = _ac;
            toHitBonus = thb;
            attackDiceNum = adn;
            attackDiceType = adt;
            attackBonus = ab;
        }

        public void TakeHit(int dam)
        {
            hp -= dam;
        }

        public void InterpretAttackDice(string input)
        {
            string[] sep = input.Split('d');
            attackDiceNum = Convert.ToInt16(sep[0]);
            attackDiceType = Convert.ToInt16(sep[1]);
        }
    }
}
