using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMProjection1
{
    public partial class DMControls : Form
    {
        public PC[] roster = new PC[3];
        public List<NPC> allies = new List<NPC>();
        public List<NPC> enemies = new List<NPC>();

        public delegate void UpdateDel();
        public event UpdateDel UpdateEvent;
        public DMControls()
        {


            InitializeComponent();


        }

        public void UpdateLists()
        {
            roster[0].name = char1Name.Text;
            roster[0].hp = Convert.ToInt16(hp1.Text);
            roster[0].toHitBonus = Convert.ToInt16(thb1.Text);
            roster[0].InterpretAttackDice(ad1.Text);
            roster[0].attackBonus = Convert.ToInt16(ab1.Text);

            roster[1].name = char2Name.Text;
            roster[1].hp = Convert.ToInt16(hp2.Text);
            roster[1].toHitBonus = Convert.ToInt16(thb2.Text);
            roster[1].InterpretAttackDice(ad2.Text);
            roster[1].attackBonus = Convert.ToInt16(ab2.Text);

            roster[2].name = char3Name.Text;
            roster[2].hp = Convert.ToInt16(hp3.Text);
            roster[2].toHitBonus = Convert.ToInt16(thb3.Text);
            roster[2].InterpretAttackDice(ad3.Text);
            roster[2].attackBonus = Convert.ToInt16(ab3.Text);

            enemies.Clear();
            for(int i = 0; i < EnemiesFlowLayoutPanel.Controls.Count; i++)
            {
                enemies.Add(new NPC());
                enemies.Last().name = EnemiesFlowLayoutPanel.Controls[i].Controls[0].Text;
                enemies.Last().hp = Convert.ToInt16(EnemiesFlowLayoutPanel.Controls[i].Controls[2].Text);
                enemies.Last().ac = Convert.ToInt16(EnemiesFlowLayoutPanel.Controls[i].Controls[4].Text);
                enemies.Last().toHitBonus = Convert.ToInt16(EnemiesFlowLayoutPanel.Controls[i].Controls[6].Text);
                enemies.Last().InterpretAttackDice(EnemiesFlowLayoutPanel.Controls[i].Controls[8].Text);
                enemies.Last().attackBonus = Convert.ToInt16(EnemiesFlowLayoutPanel.Controls[i].Controls[10].Text);
            }

            UpdateEvent();
        }

        private void AddEnemyButton_Click(object sender, EventArgs e)
        {
            //EnemiesFlowLayoutPanel.
            //enemies.Add(NPC())

            GroupBox newbox = new GroupBox();

            TextBox nameBox = new TextBox();
            nameBox.Location = new System.Drawing.Point(6, 19);
            newbox.Controls.Add(nameBox);

            Label hpLabel = new Label();
            hpLabel.Text = "HP";
            hpLabel.Location = new System.Drawing.Point(7, 46);
            newbox.Controls.Add(hpLabel);

            TextBox hpBox = new TextBox();
            hpBox.Location = new System.Drawing.Point(76, 45);
            newbox.Controls.Add(hpBox);

            Label acLabel = new Label();
            acLabel.Text = "AC";
            acLabel.Location = new System.Drawing.Point(7, 72);
            newbox.Controls.Add(acLabel);

            TextBox acBox = new TextBox();
            acBox.Location = new System.Drawing.Point(76, 71);
            newbox.Controls.Add(acBox);

            Label thbLabel = new Label();
            thbLabel.Text = "ToHitBonus";
            thbLabel.Location = new System.Drawing.Point(7, 98);
            newbox.Controls.Add(thbLabel);

            TextBox thbBox = new TextBox();
            thbBox.Location = new System.Drawing.Point(76, 97);
            newbox.Controls.Add(thbBox);

            Label adLabel = new Label();
            adLabel.Text = "AttackDice";
            adLabel.Location = new Point(7, 124);
            newbox.Controls.Add(adLabel);

            TextBox adBox = new TextBox();
            adBox.Location = new Point(76, 123);
            newbox.Controls.Add(adBox);

            Label abLabel = new Label();
            abLabel.Text = "AttackBonus";
            abLabel.Location = new Point(7, 150);
            newbox.Controls.Add(abLabel);

            TextBox abBox = new TextBox();
            abBox.Location = new Point(76, 149);
            newbox.Controls.Add(abBox);



            EnemiesFlowLayoutPanel.Controls.Add(newbox);
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            UpdateLists();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            EnemiesFlowLayoutPanel.Controls.Clear();
            AlliesFlowLayoutPanel.Controls.Clear();
            UpdateLists();
        }
    }
}
