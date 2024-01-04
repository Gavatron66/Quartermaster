using Roster_Builder.Space_Marines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Aeldari
{
    public class CorsairVoidscarred : Datasheets
    {
        int currentIndex = 0;
        bool shadeRunner = false;
        bool soulWeaver = false;
        bool waySeeker = false;
        bool mistShield = false;
        string disciplineSelected = string.Empty;

        public CorsairVoidscarred()
        {
            DEFAULT_POINTS = 12;
            UnitSize = 5;
            Points = DEFAULT_POINTS * UnitSize;
            TemplateCode = "voidscarred";
            Weapons.Add("Shuriken Pistol");
            for(int i = 1; i < 5; i++)
            {
                Weapons.Add("Shuriken Pistol and Aeldari Power Sword");
            }
            Keywords.AddRange(new string[]
            {
                "AELDARI", "ANHRATHE", "ASURYANI", "DRUKHARI",
                "INFANTRY", "CORSAIR VOIDSCARRED"
            });
            PsykerPowers = new string[1] { string.Empty };
            Role = "Elites";
        }

        public override Datasheets CreateUnit()
        {
            return new CorsairVoidscarred();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);
            repo = f as Aeldari;

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            CheckBox cbOption3 = panel.Controls["cbOption3"] as CheckBox;
            CheckBox cbOption4 = panel.Controls["cbOption4"] as CheckBox;
            Label lblPsyker = panel.Controls["lblPsyker"] as Label;
            ComboBox cmbDiscipline = panel.Controls["cmbDiscipline"] as ComboBox;
            CheckedListBox clbPsyker = panel.Controls["clbPsyker"] as CheckedListBox;

            int currentSize = UnitSize;
            nudUnitSize.Minimum = 5;
            antiLoop = true;
            nudUnitSize.Value = nudUnitSize.Minimum;
            antiLoop = false;
            nudUnitSize.Maximum = 10;
            nudUnitSize.Value = currentSize;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Voidscarred Felarch w/ " + Weapons[0]);
            for(int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Corsair Voidscarred w/ " + Weapons[i]);
            }
            if(shadeRunner == true)
            {
                lbModelSelect.Items.Add("Shade Runner");
            }
            if (soulWeaver == true)
            {
                lbModelSelect.Items.Add("Soul Weaver");
            }
            if (waySeeker == true)
            {
                lbModelSelect.Items.Add("Way Seeker");
            }

            cmbOption1.Items.Clear();

            cbOption1.Text = "Include a Shade Runner (+20 pts)";
            if(shadeRunner == true)
            {
                cbOption1.Checked = true;
            }
            else
            {
                cbOption1.Checked = false;
            }

            cbOption2.Text = "Include a Soul Weaver (+20 pts)";
            if (soulWeaver == true)
            {
                cbOption2.Checked = true;
            }
            else
            {
                cbOption2.Checked = false;
            }

            cbOption3.Text = "Include a Way Seeker (+25 pts)";
            if (waySeeker == true)
            {
                cbOption3.Checked = true;
            }
            else
            {
                cbOption3.Checked = false;
            }

            cmbDiscipline.Items.Clear();
            cmbDiscipline.Items.AddRange(new string[]
            {
                "Fate",
                "Fortune"
            });

            List<string> psykerpowers = new List<string>();
            lblPsyker.Text = "Select one of the following:";
            clbPsyker.ClearSelected();

            if(disciplineSelected != string.Empty)
            {
                psykerpowers = repo.GetPsykerPowers(disciplineSelected);
                clbPsyker.Items.Clear();
                foreach (string power in psykerpowers)
                {
                    clbPsyker.Items.Add(power);
                }

                for (int i = 0; i < clbPsyker.Items.Count; i++)
                {
                    clbPsyker.SetItemChecked(i, false);
                }

                if (PsykerPowers[0] != string.Empty)
                {
                    clbPsyker.SetItemChecked(clbPsyker.Items.IndexOf(PsykerPowers[0]), true);
                }
            }
            cmbDiscipline.SelectedItem = disciplineSelected;

            cbOption1.Visible = true;
            cbOption2.Visible = true;
            cbOption3.Visible = true;
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            if(antiLoop)
            {
                return;
            }

            NumericUpDown nudUnitSize = panel.Controls["nudUnitSize"] as NumericUpDown;
            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;
            CheckBox cbOption3 = panel.Controls["cbOption3"] as CheckBox;
            CheckBox cbOption4 = panel.Controls["cbOption4"] as CheckBox;
            Label lblOption1 = panel.Controls["lblOption1"] as Label;
            Label lblPsyker = panel.Controls["lblPsyker"] as Label;
            Label lblPsykerList = panel.Controls["lblPsykerList"] as Label;
            ComboBox cmbDiscipline = panel.Controls["cmbDiscipline"] as ComboBox;
            CheckedListBox clbPsyker = panel.Controls["clbPsyker"] as CheckedListBox;

            switch (code)
            {
                case 11:
                    Weapons[currentIndex] = cmbOption1.SelectedItem.ToString();
                    if (currentIndex == 0)
                    {
                        lbModelSelect.Items[currentIndex] = "Voidscarred Felarch w/ " + Weapons[currentIndex];
                    }
                    else
                    {
                        lbModelSelect.Items[currentIndex] = "Corsair Voidscarred w/ " + Weapons[currentIndex];
                    }
                    break;
                case 111:
                    if (cmbDiscipline.SelectedItem.ToString() == disciplineSelected)
                    {
                        break;
                    }

                    disciplineSelected = cmbDiscipline.SelectedItem.ToString();
                    clbPsyker.Items.Clear();
                    clbPsyker.Items.AddRange(repo.GetPsykerPowers(disciplineSelected).ToArray());
                    PsykerPowers = new string[1] { string.Empty };
                    break;
                case 21:
                    if(cbOption1.Checked)
                    {
                        shadeRunner = true;
                        lbModelSelect.Items.Add("Shade Runner");
                    }
                    else
                    {
                        shadeRunner = false;
                        lbModelSelect.Items.Remove("Shade Runner");
                    }
                    break;
                case 22:
                    if (cbOption2.Checked)
                    {
                        soulWeaver = true;
                        lbModelSelect.Items.Add("Soul Weaver");
                    }
                    else
                    {
                        soulWeaver = false;
                        lbModelSelect.Items.Remove("Soul Weaver");
                    }
                    break;
                case 23:
                    if (cbOption3.Checked)
                    {
                        waySeeker = true;
                        lbModelSelect.Items.Add("Way Seeker");
                    }
                    else
                    {
                        waySeeker = false;
                        lbModelSelect.Items.Remove("Way Seeker");
                    }
                    break;
                case 24:
                    if (cbOption4.Checked && cbOption4.Text == "Faolchú (+10 pts)")
                    {
                        Weapons[currentIndex] = "Faolchú (+10 pts)";
                        lbModelSelect.Items[currentIndex] = "Corsair Voidscarred w/ Shuriken Pistol, Aeldari Power Sword and " + Weapons[currentIndex];
                        cmbOption1.SelectedItem = "Shuriken Pistol and Aeldari Power Sword";
                        cmbOption1.Enabled = false;
                    }
                    else if(cbOption4.Text == "Faolchú (+10 pts)")
                    {
                        Weapons[currentIndex] = "Shuriken Pistol and Aeldari Power Sword";
                        lbModelSelect.Items[currentIndex] = "Corsair Voidscarred w/ " + Weapons[currentIndex];
                        cmbOption1.Enabled = true;
                    }
                    else if (cbOption4.Text == "Mistshield (+5 pts)" && cbOption4.Checked)
                    {
                        mistShield = true;
                        lbModelSelect.Items[0] = "Voidscarred Felarch w/ " + Weapons[0] + " and Mistshield";
                    }
                    else if (cbOption4.Text == "Mistshield (+5 pts)")
                    {
                        mistShield = false;
                        lbModelSelect.Items[0] = "Voidscarred Felarch w/ " + Weapons[0];
                    }
                    break;
                case 30:
                    int temp = UnitSize;
                    UnitSize = int.Parse(nudUnitSize.Value.ToString());

                    if (temp < UnitSize)
                    {
                        Weapons.Add("Shuriken Pistol and Aeldari Power Sword");
                        lbModelSelect.Items.Insert(UnitSize - 1, "Corsair Voidscarred w/ " + Weapons[UnitSize - 1]);
                    }

                    if (temp > UnitSize)
                    {
                        lbModelSelect.Items.RemoveAt(temp - 1);
                        Weapons.RemoveAt(UnitSize - 1);
                    }
                    break;
                case 60:
                    if (clbPsyker.CheckedItems.Count < 1)
                    {
                        break;
                    }
                    else if (clbPsyker.CheckedItems.Count == 1)
                    {
                        PsykerPowers[0] = clbPsyker.CheckedItems[0] as string;
                    }
                    else
                    {
                        clbPsyker.SetItemChecked(clbPsyker.SelectedIndex, false);
                    }
                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;
                    string currentIndexStr = string.Empty;
                    if (currentIndex >= 0)
                    {
                        currentIndexStr = lbModelSelect.Items[currentIndex].ToString();
                    }

                    antiLoop = true;

                    if(currentIndex < 0)
                    {
                        lblOption1.Visible = false;
                        cmbOption1.Visible = false;
                        clbPsyker.Visible = false;
                        lblPsyker.Visible = false;
                        lblPsykerList.Visible = false;
                        cmbDiscipline.Visible = false;
                        cbOption4.Visible = false;
                    }
                    else if (currentIndexStr.Contains("Felarch"))
                    {
                        lblOption1.Visible = true;
                        cmbOption1.Visible = true;
                        clbPsyker.Visible = false;
                        lblPsyker.Visible = false;
                        lblPsykerList.Visible = false;
                        cmbDiscipline.Visible = false;
                        cbOption4.Visible = true;
                        cbOption4.Text = "Mistshield (+5 pts)";
                        cmbOption1.Enabled = true;
                        cbOption4.Enabled = true;

                        cmbOption1.Items.Clear();
                        cmbOption1.Items.AddRange(new string[]
                        {
                            "Neuro Disruptor (+5 pts)",
                            "Shuriken Rifle",
                            "Shuriken Pistol"
                        });
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

                        if(mistShield)
                        {
                            cbOption4.Checked = true;
                            lbModelSelect.Items[0] = "Voidscarred Felarch w/ " + Weapons[0] + " and Mistshield";
                        }
                        else
                        {
                            cbOption4.Checked = false;
                            lbModelSelect.Items[0] = "Voidscarred Felarch w/ " + Weapons[0];
                        }
                    }
                    else if (currentIndexStr.Contains("Corsair"))
                    {
                        lblOption1.Visible = true;
                        cmbOption1.Visible = true;
                        clbPsyker.Visible = false;
                        lblPsyker.Visible = false;
                        lblPsykerList.Visible = false;
                        cmbDiscipline.Visible = false;
                        cbOption4.Visible = true;
                        cbOption4.Text = "Faolchú (+10 pts)";

                        cmbOption1.Items.Clear();
                        cmbOption1.Items.AddRange(new string[]
                        {
                            "Corsair Blaster (+10 pts)",
                            "Corsair Shredder (+5 pts)",
                            "Ranger Long Rifle (+5 pts)",
                            "Shuriken Cannon (+10 pts)",
                            "Shuriken and Fusion Pistols (+10 pts)",
                            "Shuriken Rifle",
                            "Shuriken Pistol and Aeldari Power Sword",
                            "Wraithcannon (+15 pts)"
                        });
                        WeaponsCheck(Weapons[currentIndex], cmbOption1, cbOption4);
                        if (Weapons[currentIndex] == "Faolchú (+10 pts)")
                        {
                            cbOption4.Checked = true; 
                            cmbOption1.SelectedItem = "Shuriken Pistol and Aeldari Power Sword";
                            cmbOption1.Enabled = false;
                        }
                        else
                        {
                            cbOption4.Checked = false;
                            cmbOption1.Enabled = true;
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex]);
                        }
                    }
                    else if (currentIndexStr.Contains("Way Seeker"))
                    {
                        lblOption1.Visible = false;
                        cmbOption1.Visible = false;
                        clbPsyker.Visible = true;
                        lblPsyker.Visible = true;
                        lblPsykerList.Visible = true;
                        cmbDiscipline.Visible = true;
                        cbOption4.Visible = false;
                        cmbOption1.Enabled = true;
                    }

                    antiLoop = false;
                    break;
            }

            Points = DEFAULT_POINTS * UnitSize;

            if(shadeRunner)
            {
                Points += 20;
            }

            if(soulWeaver)
            {
                Points += 20;
            }

            if(waySeeker)
            {
                Points += 25;
            }

            if(mistShield)
            {
                Points += 5;
            }

            foreach (var weapon in Weapons)
            {
                if (weapon == "Corsair Shredder (+5 pts)" || weapon == "Neuro Disruptor (+5 pts)" 
                    || weapon == "Ranger Long Rifle (+5 pts)")
                {
                    Points += 5;
                }

                if (weapon == "Corsair Blaster (+10 pts)" || weapon == "Faolchú (+10 pts)" 
                    || weapon == "Shuriken and Fusion Pistols (+10 pts)" || weapon == "Shuriken Cannon (+10 pts)")
                {
                    Points += 10;
                }

                if (weapon == "Wraithcannon (+15 pts)")
                {
                    Points += 15;
                }
            }
        }

        public override string ToString()
        {
            return "Corsair Voidscarred - " + Points + "pts";
        }

        private void WeaponsCheck(string currentIndexWeapon, ComboBox cmbOption1, CheckBox cbOption4)
        {
            int[] weapons = { 0, 0, 0, 0, 0 };
            //Corsair Blaster/Shredder
            //Shuriken Cannon/Wraithcannon
            //Ranger Long Rifle
            //Fusion Pistol
            //Faolchú

            foreach(var weapon in Weapons)
            {
                if(weapon == "Corsair Blaster (+10 pts)" || weapon == "Corsair Shredder (+5 pts)")
                {
                    weapons[0] += 1;
                }

                if(weapon == "Shuriken Cannon (+10 pts)" || weapon == "Wraithcannon (+15 pts)")
                {
                    weapons[1] += 1;
                }

                if(weapon == "Ranger Long Rifle (+5 pts)")
                {
                    weapons[2] += 1;
                }

                if(weapon == "Shuriken and Fusion Pistols (+10 pts)")
                {
                    weapons[3] += 1;
                }

                if(weapon == "Faolchú (+10 pts)")
                {
                    weapons[4] += 1;
                }
            }

            if(UnitSize < 10)
            {
                cmbOption1.Items.Remove("Shuriken Cannon (+10 pts)");
                cmbOption1.Items.Remove("Wraithcannon (+15 pts)");
                cmbOption1.Items.Remove("Ranger Long Rifle (+5 pts)");
                cmbOption1.Items.Remove("Shuriken and Fusion Pistols (+10 pts)");

                if (weapons[0] == 1 && !(currentIndexWeapon == "Corsair Blaster (+10 pts)" || currentIndexWeapon == "Corsair Shredder (+5 pts)"))
                {
                    cmbOption1.Items.Remove("Corsair Blaster (+10 pts)");
                    cmbOption1.Items.Remove("Corsair Shredder (+5 pts)");
                }

                if (weapons[4] == 1 && !currentIndexWeapon.Contains("Faolchú (+10 pts)"))
                {
                    cbOption4.Enabled = false;
                }
                else
                {
                    cbOption4.Enabled = true;
                }
            }
            else
            {
                if (weapons[0] == 2 && !(currentIndexWeapon == "Corsair Blaster (+10 pts)" || currentIndexWeapon == "Corsair Shredder (+5 pts)"))
                {
                    cmbOption1.Items.Remove("Corsair Blaster (+10 pts)");
                    cmbOption1.Items.Remove("Corsair Shredder (+5 pts)");
                }

                if (weapons[1] == 1 && !(currentIndexWeapon == "Shuriken Cannon (+10 pts)" || currentIndexWeapon == "Wraithcannon (+15 pts)"))
                {
                    cmbOption1.Items.Remove("Shuriken Cannon (+10 pts)");
                    cmbOption1.Items.Remove("Wraithcannon (+15 pts)");
                }

                if (weapons[2] == 1 && !(currentIndexWeapon == "Ranger Long Rifle (+5 pts)"))
                {
                    cmbOption1.Items.Remove("Ranger Long Rifle (+5 pts)");
                }

                if (weapons[3] == 1 && !(currentIndexWeapon == "Shuriken and Fusion Pistols (+10 pts)"))
                {
                    cmbOption1.Items.Remove("Shuriken and Fusion Pistols (+10 pts)");
                }

                if (weapons[4] == 1 && !currentIndexWeapon.Contains("Faolchú (+10 pts)"))
                {
                    cbOption4.Enabled = false;
                }
                else
                {
                    cbOption4.Enabled = true;
                }
            }
        }
    }
}
