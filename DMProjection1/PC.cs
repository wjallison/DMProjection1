using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMProjection1
{
    public class Entity
    {

    }

    public class PC : Entity
    {
        public string name;
        public int hp;
        public int toHitBonus;
        public int attackDiceNum;
        public int attackDiceType;
        public int attackBonus;

        public PC()
        {

        }

        public PC(string _name, int _hp, int thb, int adn, int adt, int ab)
        {
            name = _name;
            hp = _hp;
            toHitBonus = thb;
            attackDiceNum = adn;
            attackDiceType = adt;
            attackBonus = ab;
        }

        public void TakeHit(int dam)
        {
            hp -= dam;
        }

        public void AddHealth(int health)
        {
            hp += health;
        }

        public void InterpretAttackDice(string input)
        {
            string[] sep = input.Split('d');
            attackDiceNum = Convert.ToInt16(sep[0]);
            attackDiceType = Convert.ToInt16(sep[1]);
        }
    }
}
