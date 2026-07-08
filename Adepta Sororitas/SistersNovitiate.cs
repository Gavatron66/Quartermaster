using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Adepta_Sororitas
{
    public class SistersNovitiate : Datasheets
    {
        int currentIndex;
        bool isLoading = false;
        int specialW;

        public SistersNovitiate()
        {
            DEFAULT_POINTS = 75;
            UnitSize = 10;
            Points = DEFAULT_POINTS;
            TemplateCode = "NL1m";
            Weapons.Add("Boltgun and Bolt Pistol");
            for (int i = 1; i < UnitSize; i++)
            {
                Weapons.Add("Autogun");
            }
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "ADEPTUS MINISTORUM", "ADEPTA SORORITAS", "<ORDER>",
                "INFANTRY", "CORE", "SISTERS NOVITIATE"
            });
            Role = "Troops";
        }

        public override Datasheets CreateUnit()
        {
            return new SistersNovitiate();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as AdeptaSororitas;
            Template.LoadTemplate(TemplateCode, panel);

            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;

            panel.Controls["nudUnitSize"].Visible = false;
            panel.Controls["lblNumModels"].Visible = false;
            panel.Controls["lblModelPoints"].Visible = false;

            lbModelSelect.Items.Clear();
            lbModelSelect.Items.Add("Novitiate Superior w/ " + Weapons[0]);
            for (int i = 1; i < UnitSize; i++)
            {
                lbModelSelect.Items.Add("Sister Novitiate w/ " + Weapons[i]);
            }

            cbStratagem5.Text = repo.StratagemList[2];
            cbStratagem5.Location = new System.Drawing.Point(panel.Controls["lblOption1"].Location.X, panel.Controls["cmbOption1"].Location.Y + 32);
            panel.Controls["lblRelic"].Location = new System.Drawing.Point(cbStratagem5.Location.X, cbStratagem5.Location.Y + 30);
            cmbRelic.Location = new System.Drawing.Point(cbStratagem5.Location.X, cbStratagem5.Location.Y + 50);
            panel.Controls["lblRelic"].Visible = false;
            cmbRelic.Visible = false;

            cmbRelic.Items.Clear();
            cmbRelic.Items.AddRange(f.GetRelics(this.Keywords).ToArray());

            antiLoop = true;
            if (Stratagem.Contains(cbStratagem5.Text))
            {
                cbStratagem5.Checked = true;
                cbStratagem5.Enabled = true;

                panel.Controls["lblRelic"].Visible = true;
                cmbRelic.Visible = true;

                if (Relic == "(None)")
                {
                    cmbRelic.SelectedIndex = 0;
                }
                else
                {
                    if (Relic != null && cmbRelic.Items.Contains(Relic))
                    {
                        cmbRelic.SelectedIndex = cmbRelic.Items.IndexOf(Relic);
                    }
                    else
                    {
                        cmbRelic.SelectedIndex = 0;
                    }
                }
            }
            else
            {
                cbStratagem5.Checked = false;
                cmbRelic.SelectedIndex = 0;
            }

            panel.Controls["lblRelic"].Visible = false;
            cmbRelic.Visible = false;
            antiLoop = false;
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            if (isLoading)
            {
                return;
            }

            ListBox lbModelSelect = panel.Controls["lbModelSelect"] as ListBox;
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            CheckBox cbStratagem5 = panel.Controls["cbStratagem5"] as CheckBox;

            switch (code)
            {
                case 11:
                    if (!restrictedIndexes.Contains(cmbOption1.SelectedIndex))
                    {
                        Weapons[currentIndex] = cmbOption1.SelectedItem.ToString();
                        if (currentIndex == 0)
                        {
                            lbModelSelect.Items[currentIndex] = "Novitiate Superior w/ " + Weapons[currentIndex];
                        }
                        else
                        {
                            lbModelSelect.Items[currentIndex] = "Sister Novitiate w/ " + Weapons[currentIndex];
                        }
                    }
                    else
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex]);
                    }

                    break;
                case 17:
                    Relic = cmbRelic.SelectedItem.ToString();
                    if(Relic == "Redemption")
                    {
                        cmbOption1.SelectedIndex = 2;
                        cmbOption1.Enabled = false;
                    }
                    else
                    {
                        cmbOption1.Enabled = true;
                    }
                    break;
                case 61:
                    currentIndex = lbModelSelect.SelectedIndex;

                    if (currentIndex < 0)
                    {
                        cmbOption1.Visible = false;
                        panel.Controls["lblOption1"].Visible = false;
                        cbStratagem5.Visible = false;
                        panel.Controls["lblRelic"].Visible = false;
                        cmbRelic.Visible = false;
                        break;
                    }
                    else
                    {
                        isLoading = true;
                        cmbOption1.Visible = true;
                        panel.Controls["lblOption1"].Visible = true;

                        if (currentIndex == 0)
                        {
                            cbStratagem5.Visible = true;

                            if (Stratagem.Contains(cbStratagem5.Text))
                            {
                                panel.Controls["lblRelic"].Visible = true;
                                cmbRelic.Visible = true;
                            }

                            cmbOption1.Items.Clear();
                            cmbOption1.Items.AddRange(new string[]
                            {
                                "Boltgun and Bolt Pistol",
                                "Bolt Pistol and Power Sword",
                                "Plasma Pistol and Power Sword (+10 pts)"
                            });
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

                            if(Relic == "Redemption")
                            {
                                cmbOption1.Enabled = false;
                            }
                        }
                        else
                        {
                            cbStratagem5.Visible = false;
                            panel.Controls["lblRelic"].Visible = false;
                            cmbRelic.Visible = false;
                            cmbOption1.Enabled = true;

                            cmbOption1.Items.Clear();
                            cmbOption1.Items.AddRange(new string[]
                            {
                                "Autogun",
                                "Ministorum Flamer (+5 pts)",
                                "Novitiate Melee Weapon",
                                "Sacred Banner (+5 pts)",
                                "Simulacrum Imperialis (+5 pts)"
                            });
                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

                            restrictedIndexes.Clear();
                            if (Weapons.Contains("Sacred Banner (+5 pts)") && Weapons[currentIndex] != "Sacred Banner (+5 pts)")
                            {
                                restrictedIndexes.Add(3);
                            }

                            if (Weapons.Contains("Simulacrum Imperialis (+5 pts)") && Weapons[currentIndex] != "Simulacrum Imperialis (+5 pts)")
                            {
                                restrictedIndexes.Add(4);
                            }

                            if (specialW == 2 && Weapons[currentIndex] != "Ministorum Flamer (+5 pts)")
                            {
                                restrictedIndexes.Add(1);
                            }

                            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[currentIndex]);
                            this.DrawItemWithRestrictions(restrictedIndexes, cmbOption1);
                        }
                        isLoading = false;
                        break;
                    }
                case 75:
                    if (cbStratagem5.Checked)
                    {
                        Stratagem.Add(cbStratagem5.Text);
                        panel.Controls["lblRelic"].Visible = true;
                        cmbRelic.Visible = true;
                    }
                    else
                    {
                        if (Stratagem.Contains(cbStratagem5.Text))
                        {
                            Stratagem.Remove(cbStratagem5.Text);
                        }
                        cmbRelic.Visible = false;
                        panel.Controls["lblRelic"].Visible = false;
                        cmbRelic.SelectedIndex = 0;
                    }
                    break;
            }

            Points = DEFAULT_POINTS;

            specialW = 0;
            foreach (var weapon in Weapons)
            {
                if(weapon == "Ministorum Flamer (+5 pts)")
                {
                    specialW++;
                    Points += 5;
                }
                else if(weapon == "Plasma Pistol and Power Sword (+10 pts)")
                {
                    Points += 10;
                }
                else if(weapon == "Sacred Banner (+5 pts)")
                {
                    Points += 5;
                }
                else if(weapon == "Simulacrum Imperialis (+5 pts)")
                {
                    Points += 5;
                }
            }
        }

        public override string ToString()
        {
            return "Sisters Novitiate - " + Points + "pts";
        }
    }
}
