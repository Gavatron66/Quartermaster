using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Astra_Militarum
{
    public class CadianCommandSquad : Datasheets
    {
        int currentIndex;
        ComboBox CMBO2;

        public CadianCommandSquad()
        {
            DEFAULT_POINTS = 15;
            UnitSize = 5;
            Points = UnitSize * DEFAULT_POINTS;
            TemplateCode = "NL2m";
            Weapons.Add("Laspistol");   //Commander
            Weapons.Add("Chainsword");
            Weapons.Add("Lasgun and Regimental Standard");      //Regimental Standard
            Weapons.Add("Lasgun");      //Medi-pack
            Weapons.Add("Laspistol");   //Master Vox
            Weapons.Add("Chainsword");  //Veteran
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ASTRA MILITARUM", "CADIAN",
                "INFANTRY", "CHARACTER", "OFFICER", "REGIMENTAL", "CADIAN COMMANDER",
                "INFANTRY", "REGIMENTAL", "COMMAND SQUAD"
            });
            Role = "HQ";
        }

        public override Datasheets CreateUnit()
        {
            return new CadianCommandSquad();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as AstraMilitarum;
            Template.LoadTemplate(TemplateCode, panel);

            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;
            CMBO2 = cmbOption2;

            panel.Controls["nudUnitSize"].Visible = false;
            panel.Controls["lblNumModels"].Visible = false;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Cadian Commander w/ " + Weapons[0] + " and " + Weapons[1]);
            lbModelSelect.Items.Add("Cadian Veteran Guardsman w/ " + Weapons[2]);
            lbModelSelect.Items.Add("Cadian Veteran Guardsman w/ " + Weapons[3] + " and Medi-pack");
            lbModelSelect.Items.Add("Cadian Veteran Guardsman w/ " + Weapons[4] + " and Master Vox");
            lbModelSelect.Items.Add("Cadian Veteran Guardsman w/ Laspistol and " + Weapons[5]);

            cmbOption2.DrawMode = DrawMode.OwnerDrawFixed;
            cmbOption2.DrawItem += new DrawItemEventHandler(TestDraw);
        }

        private void TestDraw(object sender, DrawItemEventArgs e)
        {
            // Draw the background of the ListBox control for each item.
            e.DrawBackground();
            // Define the default color of the brush as black.
            Brush myBrush = Brushes.Black;

            // Determine the color of the brush to draw each item based 
            // on the index of the item to draw.
            switch (e.Index)
            {
                case 0:
                    myBrush = Brushes.Red;
                    break;
                case 1:
                    myBrush = Brushes.Orange;
                    break;
                case 2:
                    myBrush = Brushes.Purple;
                    break;
            }

            // Draw the current item text based on the current Font 
            // and the custom brush settings.
            e.Graphics.DrawString(CMBO2.Items[e.Index].ToString(),
                e.Font, myBrush, e.Bounds, StringFormat.GenericDefault);
            // If the ListBox has focus, draw a focus rectangle around the selected item.
            e.DrawFocusRectangle();
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            if (antiLoop)
            {
                return;
            }

            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbOption2 = panel.Controls["cmbOption2"] as ComboBox;

            switch (code)
            {
                case 11:
                    switch (currentIndex)
                    {
                        case 0:
                            Weapons[0] = cmbOption1.SelectedItem.ToString();
                            break;
                        case 1:
                            Weapons[2] = cmbOption1.SelectedItem.ToString();
                            break;
                        case 2:
                            Weapons[3] = cmbOption1.SelectedItem.ToString();
                            break;
                        case 3:
                            Weapons[4] = cmbOption1.SelectedItem.ToString();
                            break;
                        case 4:
                            Weapons[5] = cmbOption1.SelectedItem.ToString();
                            break;
                    }
                    break;
                case 12:
                    if(currentIndex == 0)
                    {
                        Weapons[1] = cmbOption2.SelectedItem.ToString();
                    }
                    else if (currentIndex == 4)
                    {
                        Weapons[6] = cmbOption2.SelectedItem.ToString();
                    }
                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;
                    antiLoop = true;

                    if (currentIndex < 0)
                    {
                        cmbOption1.Visible = false;
                        cmbOption2.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        panel.Controls["lblOption2"].Visible = false;
                    }
                    else
                    {
                        cmbOption1.Visible = true;
                        panel.Controls["lblOption1"].Visible = true;
                        cmbOption2.Visible = false;
                        panel.Controls["lblOption2"].Visible = false;

                        switch(currentIndex)
                        {
                            case 0:
                                cmbOption2.Visible = true;
                                panel.Controls["lblOption2"].Visible = true;

                                cmbOption1.Items.Clear();
                                cmbOption1.Items.AddRange(new string[]
                                {
                                    "Bolt Pistol",
                                    "Laspistol",
                                    "Plasma Pistol (+5 pts)"
                                });
                                cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

                                cmbOption2.Items.Clear();
                                cmbOption2.Items.AddRange(new string[]
                                {
                                    "Chainsword",
                                    "Power Fist (+5 pts)",
                                    "Power Sword"
                                });
                                cmbOption2.SelectedIndex = cmbOption2.Items.IndexOf(Weapons[1]);

                                break;
                            case 1:
                                cmbOption1.Items.Clear();
                                cmbOption1.Items.AddRange(new string[]
                                {
                                    "Flamer",
                                    "Grenade Launcher",
                                    "Lasgun and Regimental Standard",
                                    "Meltagun",
                                    "Plasma Gun"
                                });
                                cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[2]);

                                break;
                            case 2:
                                cmbOption1.Visible = false;
                                panel.Controls["lblOption1"].Visible = false;
                                break;
                            case 3:
                                cmbOption1.Items.Clear();
                                cmbOption1.Items.AddRange(new string[]
                                {
                                    "Bolt Pistol",
                                    "Laspistol",
                                    "Plasma Pistol (+5 pts)"
                                });
                                cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[4]);

                                break;
                            case 4:
                                cmbOption1.Items.Clear();
                                cmbOption1.Items.AddRange(new string[]
                                {
                                    "Chainsword",
                                    "Flamer",
                                    "Grenade Launcher",
                                    "Meltagun",
                                    "Plasma Gun",
                                    "Power Fist (+5 pts)",
                                    "Power Sword"
                                });
                                cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[5]);

                                break;
                        }

                    }

                    antiLoop = false;
                    break;
                default: break;
            }

            antiLoop = true;
            lbModelSelect.Items[0] = "Cadian Commander w/ " + Weapons[0] + " and " + Weapons[1];
            lbModelSelect.Items[1] = "Cadian Veteran Guardsman w/ " + Weapons[2];
            lbModelSelect.Items[2] = "Cadian Veteran Guardsman w/ " + Weapons[3] + " and Medi-pack";
            lbModelSelect.Items[3] = "Cadian Veteran Guardsman w/ " + Weapons[4] + " and Master Vox";
            lbModelSelect.Items[4] = "Cadian Veteran Guardsman w/ Laspistol and " + Weapons[5];
            antiLoop = false;

            Points = DEFAULT_POINTS * UnitSize;

            if (Weapons[0] == "Plasma Pistol (+5 pts)")
            {
                Points += 5;
            }

            if (Weapons[4] == "Plasma Pistol (+5 pts)")
            {
                Points += 5;
            }

            if (Weapons[1] == "Power Fist (+5 pts)")
            {
                Points += 5;
            }

            if (Weapons[5] == "Power Fist (+5 pts)")
            {
                Points += 5;
            }
        }

        public override string ToString()
        {
            return "Cadian Command Squad - " + Points + "pts";
        }
    }
}