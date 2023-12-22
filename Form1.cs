using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Roster_Builder.Death_Guard;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Roster_Builder.Necrons;
using Roster_Builder.Adeptus_Custodes;
using Roster_Builder.Genestealer_Cults;
using Roster_Builder.Space_Marines;

namespace Roster_Builder
{
    public partial class Form1 : Form
    {
        Roster roster;
        int currentIndex = -1;
        Faction units;
        bool isLoading = false;

        public Form1()
        {
            InitializeComponent();
            this.Size = new Size(1139, 644);

            SetUpForm();
        }

        private void SetUpForm()
        {
            #region Setting up the Form
            lbRoster.Visible = false;
            lbUnits.Visible = false;
            panel1.Size = new System.Drawing.Size(632, 570);
            panel1.Location = new System.Drawing.Point(242, 96);
            panel1.Visible = false;
            lblEditingUnit.Text = string.Empty;
            lblPoints.Text = string.Empty;
            lblCurrentPoints.Visible = false;
            btnAddToRoster.Visible = false;
            btnRemove.Visible = false;
            btnSave.Visible = false;
            MenuPanel.Visible = true;
            MenuPanel.Location = new System.Drawing.Point(242, 35);
            MenuPanel.BringToFront();
            lblErrors.Visible = false;
            panelSubFaction.Location = new System.Drawing.Point(242, 96);

            for (int i = 0; i < panel1.Controls.Count; i++)
            {
                panel1.Controls[i].Visible = false;
            }

            for (int i = 0; i < gbUnitLeader.Controls.Count; i++)
            {
                gbUnitLeader.Controls[i].Visible = false;
            }

            cmbSelectFaction.Items.Clear();
            cmbSelectFaction.Items.AddRange(new Faction[]
            {
                new AdeptusCustodes(),
                new DeathGuard(),
                new GSC(),
                new Necrons.Necrons(),
                new SpaceMarines(),
            });
            cmbSelectFaction.Text = string.Empty;

            lbUnits.Items.Clear();
            lbRoster.Items.Clear();
            cmbSubFaction.Items.Clear();

            currentIndex = -1;
            #endregion
        }

        private void btnBegin_Click(object sender, EventArgs e)
        {
            if(cmbSelectFaction.SelectedIndex < 0 || nudSelectPoints.Value < 500)
            {
                return;
            }

            lbRoster.Visible = true;
            lbUnits.Visible = true;
            panel1.Visible = true;
            MenuPanel.Visible = false;
            btnRemove.Visible = true;
            btnRemove.BringToFront();
            btnAddToRoster.Visible = true;
            btnAddToRoster.BringToFront();
            lblCurrentPoints.Visible = true;
            btnSave.Visible = true;
            btnSave.BringToFront();
            gbCustomSubfaction.Visible = false;
            lblErrors.Visible = true;
            lblErrors.BringToFront();
            lblEditingUnit.BringToFront(); 
            panelSubFaction.BringToFront();

            if (!isLoading)
            {
                units = cmbSelectFaction.SelectedItem as Faction;
                roster = new Roster(Convert.ToInt32(nudSelectPoints.Value), units);
            }
            else
            {
                units = roster.currentFaction;
            }


            List<Datasheets> datasheets = units.GetDatasheets();

            lblSubfaction.Text = "Select a " + units.subFactionName + " :";

            panel1.Controls["lblFactionUpgrade"].Text = units.factionUpgradeName;

            List<string> subFactions = units.GetSubFactions();
            foreach (var subfaction in subFactions)
            {
                cmbSubFaction.Items.Add(subfaction);
            }

            foreach (Datasheets item in datasheets)
            {
                lbUnits.Items.Add(item);
            }

            if (!isLoading)
            {
                lbRoster.Items.Add(units.subFactionName);
                updateLBRoster();
            }

            if (!isLoading)
            {
                cmbCustomSub1.Items.Clear();
                cmbCustomSub2.Items.Clear();
            }

            if (cmbSubFaction.Items.Contains("<Custom>"))
            {
                List<string> customList1 = units.GetCustomSubfactionList1();
                List<string> customList2 = units.GetCustomSubfactionList2();

                foreach (var custom1 in customList1)
                {
                    cmbCustomSub1.Items.Add(custom1);
                }
                foreach (var custom2 in customList2)
                {
                    cmbCustomSub2.Items.Add(custom2);
                }
            }

            cbStratagem1.Text = units.StratagemList[0];
            cbStratagem2.Text = units.StratagemList[1];
        }

        private void btnAddToRoster_Click(object sender, EventArgs e)
        {
            if (lbUnits.SelectedIndex >= 0)
            {
                if (lbUnits.SelectedItem is Datasheets)
                {
                    Datasheets newUnit = lbUnits.SelectedItem as Datasheets;
                    newUnit.repo = units;
                    roster.roster.Add(newUnit.CreateUnit());
                }

                updateLBRoster();
            }
        }

        private void updateLBRoster()
        {
            object zeroItem = lbRoster.Items[0];
            lbRoster.Items.Clear();

            lbRoster.Items.Add(zeroItem);
            for (int i = 0; i < roster.roster.Count; i++)
            {
                lbRoster.Items.Add(roster.roster[i].ToString());
            }

            int pts = 0;

            for (int i = 0; i < roster.roster.Count; i++)
            {
                pts += roster.roster[i].Points;
            }

            lblPoints.Text = pts + " / " + nudSelectPoints.Text;
            if (currentIndex >= 0)
            {
                lblEditingUnit.Text = "Currently Editing: " + roster.roster[currentIndex].ToString();
            }

            int numErrors = roster.GetErrors(pts);

            if (numErrors == 0)
            {
                lblErrors.Visible = false;
                btnSave.Enabled = true;
            }
            else
            {
                lblErrors.Visible = true;
                lblErrors.Text = "Roster has " + numErrors + " errors";
                btnSave.Enabled = false;
            }

            toolTip1.SetToolTip(lblErrors, roster.getErrorTooltip());
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (currentIndex < 0) return;
            roster.roster.RemoveAt(currentIndex);
            currentIndex = -1;

            updateLBRoster();
            lblEditingUnit.Text = string.Empty;

            foreach (Control control in panel1.Controls)
            {
                control.Visible = false;
                control.Enabled = true;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < panel1.Controls.Count; i++)
            {
                panel1.Controls[i].Visible = false;
            }

            lbRoster.SelectedIndex = -1;
        }

        private void nudUnitSize_ValueChanged(object sender, EventArgs e)
        {
            roster.roster[currentIndex].SaveDatasheets(30, panel1);
            updateLBRoster();
        }

        private void cmbOption1_SelectedIndexChanged(object sender, EventArgs e)
        {
            roster.roster[currentIndex].SaveDatasheets(11, panel1);
            updateLBRoster();
        }

        private void cmbOption2_SelectedIndexChanged(object sender, EventArgs e)
        {
            roster.roster[currentIndex].SaveDatasheets(12, panel1);
            updateLBRoster();
        }

        private void lbRoster_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Control control in panel1.Controls)
            {
                control.Visible = false;
                control.Enabled = true;
            }

            foreach (Control control in gbUnitLeader.Controls)
            {
                control.Visible = false;
                control.Enabled = true;
            }

            if (lbRoster.SelectedIndex == 0)
            {
                panelSubFaction.Visible = true;
                lblEditingUnit.Text = string.Empty;
                currentIndex = -10;
                return;
            }
            else
            {
                panelSubFaction.Visible = false;
            }

            if (lbRoster.SelectedIndex == -1 || lbRoster.SelectedIndex == currentIndex + 1)
            {
                if (currentIndex == -10) { panelSubFaction.Visible = true; return; }
                if (lbRoster.SelectedIndex == -1) { return; }
            }

            roster.StratagemCheck();

            currentIndex = lbRoster.SelectedIndex - 1;

            roster.roster[currentIndex].LoadDatasheets(panel1, roster.currentFaction);

            roster.roster[currentIndex].SaveDatasheets(-1, panel1);

            if (currentIndex >= 0)
            {
                lblEditingUnit.Text = "Currently Editing: " + roster.roster[currentIndex].ToString();
            }
        }

        private void cbOption1_CheckedChanged(object sender, EventArgs e)
        {
            roster.roster[currentIndex].SaveDatasheets(21, panel1);
            updateLBRoster();
        }

        private void cbOption2_CheckedChanged(object sender, EventArgs e)
        {
            roster.roster[currentIndex].SaveDatasheets(22, panel1);
            updateLBRoster();
        }

        private void cbWarlord_CheckedChanged(object sender, EventArgs e)
        {
            if (cbWarlord.Checked)
            {
                cmbWarlord.Enabled = true;
                roster.roster[currentIndex].SaveDatasheets(25, panel1);
            }
            else
            {
                cmbWarlord.Enabled = false;
                cmbWarlord.Text = string.Empty;
                roster.roster[currentIndex].SaveDatasheets(25, panel1);
            }
            updateLBRoster();
        }

        private void cmbWarlord_SelectedIndexChanged(object sender, EventArgs e)
        {
            roster.roster[currentIndex].SaveDatasheets(15, panel1);
            updateLBRoster();
        }

        private void cmbFactionupgrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFactionupgrade.SelectedIndex != -1)
            {
                roster.roster[currentIndex].SaveDatasheets(16, panel1);
            }
            updateLBRoster();
        }

        private void clbPsyker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (clbPsyker.SelectedIndex != -1)
            {
                roster.roster[currentIndex].SaveDatasheets(60, panel1);
            }
        }

        private void cmbSubFaction_SelectedIndexChanged(object sender, EventArgs e)
        {
            units.currentSubFaction = cmbSubFaction.SelectedItem.ToString();
            lbRoster.Items.RemoveAt(0);
            lbRoster.Items.Insert(0, units.subFactionName + ": " + units.currentSubFaction);

            if (cmbSubFaction.SelectedItem as string == "<Custom>")
            {
                gbCustomSubfaction.Visible = true;
            }
            else
            {
                gbCustomSubfaction.Visible = false;
            }
        }

        private void cmbOption3_SelectedIndexChanged(object sender, EventArgs e)
        {
            roster.roster[currentIndex].SaveDatasheets(13, panel1);
            updateLBRoster();
        }

        private void cmbOption4_SelectedIndexChanged(object sender, EventArgs e)
        {
            roster.roster[currentIndex].SaveDatasheets(14, panel1);
            updateLBRoster();
        }

        private void cbLeaderOption1_CheckedChanged(object sender, EventArgs e)
        {
            roster.roster[currentIndex].SaveDatasheets(421, panel1);
            updateLBRoster();
        }

        private void cbLeaderOption2_CheckedChanged(object sender, EventArgs e)
        {
            roster.roster[currentIndex].SaveDatasheets(422, panel1);
            updateLBRoster();
        }

        private void clbPsyker_DoubleClick(object sender, EventArgs e)
        {
            if (clbPsyker.SelectedIndex != -1)
            {
                roster.roster[currentIndex].SaveDatasheets(60, panel1);
            }
        }

        private void gb_cmbOption1_SelectedIndexChanged(object sender, EventArgs e)
        {
            roster.roster[currentIndex].SaveDatasheets(411, panel1);
            updateLBRoster();
        }

        private void nudOption1_ValueChanged(object sender, EventArgs e)
        {
            roster.roster[currentIndex].SaveDatasheets(31, panel1);
            updateLBRoster();
        }
        private void nudOption2_ValueChanged(object sender, EventArgs e)
        {
            roster.roster[currentIndex].SaveDatasheets(32, panel1);
            updateLBRoster();
        }
        private void nudOption3_ValueChanged(object sender, EventArgs e)
        {
            roster.roster[currentIndex].SaveDatasheets(33, panel1);
            updateLBRoster();
        }
        private void nudOption4_ValueChanged(object sender, EventArgs e)
        {
            roster.roster[currentIndex].SaveDatasheets(34, panel1);
            updateLBRoster();
        }

        private void gb_cmbFactionupgrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            roster.roster[currentIndex].SaveDatasheets(416, panel1);
            updateLBRoster();
        }

        private void lbModelSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            roster.roster[currentIndex].SaveDatasheets(61, panel1);
            updateLBRoster();
        }

        private void cbOption3_CheckedChanged(object sender, EventArgs e)
        {
            roster.roster[currentIndex].SaveDatasheets(23, panel1);
            updateLBRoster();
        }

        private void cmbRelic_SelectedIndexChanged(object sender, EventArgs e)
        {
            roster.roster[currentIndex].SaveDatasheets(17, panel1);
            updateLBRoster();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {/*
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.AddExtension = true;
                sfd.DefaultExt = "json";
                sfd.ShowDialog();

                if (cmbSubFaction.SelectedItem.ToString() == "<Custom>")
                {
                    roster.roster[0].Keywords.Add(cmbCustomSub1.SelectedItem.ToString());
                    roster.roster[0].Keywords.Add(cmbCustomSub2.SelectedItem.ToString());
                    roster.roster[0].Keywords.Add(cmbSubFaction.SelectedItem.ToString());
                }
                else
                {
                    roster.roster[0].Keywords.Add(cmbSubFaction.SelectedItem.ToString());
                }

                string savePath = sfd.FileName;
                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize<List<Datasheets>>(roster.roster, options);
                File.WriteAllText(savePath, json);
            }
            catch { }*/

            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.AddExtension = true;
                sfd.DefaultExt = "json";
                sfd.ShowDialog();

                string savePath = sfd.FileName;
                var options = new JsonSerializerOptions { WriteIndented = true };
                var json = JsonSerializer.Serialize<Roster>(roster, options);
                File.WriteAllText(savePath, json);
            }
            catch { }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
        //    OpenFileDialog ofd = new OpenFileDialog();
        //    ofd.ShowDialog();

        //    string path = ofd.FileName;
        //    string json = File.ReadAllText(path);

        //    var newRoster = JsonSerializer.Deserialize<List<Datasheets>>(json);

        //    foreach (Faction faction in cmbSelectFaction.Items)
        //    {
        //        List<Datasheets> datasheets = faction.GetDatasheets();
        //        Type[] types = new Type[datasheets.Count];

        //        for (int i = 0; i < datasheets.Count; i++)
        //        {
        //            types[i] = datasheets[i].GetType();
        //        }

        //        if (types.Contains(newRoster[0].GetType()))
        //        {
        //            isLoading = true;
        //            units = faction;
        //        }
        //    }

        //    btnBegin.PerformClick();

        //    roster.roster = newRoster;

        //    lbRoster.Items.Insert(0, units.subFactionName);

        //    if (newRoster[0].Keywords.Last() == "<Custom>")
        //    {
        //        cmbSubFaction.SelectedIndex = cmbSubFaction.Items.IndexOf(roster.roster[0].Keywords.Last());
        //        cmbCustomSub2.SelectedIndex = cmbCustomSub2.Items.IndexOf(roster.roster[0].Keywords[roster.roster[0].Keywords.Count - 2]);
        //        cmbCustomSub1.SelectedIndex = cmbCustomSub1.Items.IndexOf(roster.roster[0].Keywords[roster.roster[0].Keywords.Count - 3]);

        //        roster.roster[0].Keywords.RemoveRange(roster.roster[0].Keywords.Count - 3, 3);
        //    }
        //    else
        //    {
        //        cmbSubFaction.SelectedIndex = cmbSubFaction.Items.IndexOf(roster.roster[0].Keywords.Last());

        //        roster.roster[0].Keywords.RemoveAt(roster.roster[0].Keywords.Count - 1);
        //    }

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();

            string path = ofd.FileName;
            string json = File.ReadAllText(path);

            var newRoster = JsonSerializer.Deserialize<Roster>(json);

            roster = new Roster(newRoster);

            nudSelectPoints.Value = Convert.ToInt32(roster.Points.ToString());

            isLoading = true;
            btnBegin.PerformClick();

            lbRoster.Items.Insert(0, units.subFactionName);

            updateLBRoster();
        }

        private void nudOption5_ValueChanged(object sender, EventArgs e)
        {
            roster.roster[currentIndex].SaveDatasheets(35, panel1);
            updateLBRoster();
        }

        private void nudOption6_ValueChanged(object sender, EventArgs e)
        {
            roster.roster[currentIndex].SaveDatasheets(36, panel1);
            updateLBRoster();
        }

        private void cbStratagem1_CheckedChanged(object sender, EventArgs e)
        {
            roster.roster[currentIndex].SaveDatasheets(71, panel1);
            updateLBRoster();
        }

        private void cbStratagem2_CheckedChanged(object sender, EventArgs e)
        {
            roster.roster[currentIndex].SaveDatasheets(72, panel1);
            updateLBRoster();
        }

        private void cbStratagem3_CheckedChanged(object sender, EventArgs e)
        {
            roster.roster[currentIndex].SaveDatasheets(73, panel1);
            updateLBRoster();
        }

        private void gb_cmbOption2_SelectedIndexChanged(object sender, EventArgs e)
        {
            roster.roster[currentIndex].SaveDatasheets(412, panel1);
            updateLBRoster();
        }

        private void nudUnitSize2_ValueChanged(object sender, EventArgs e)
        {
            roster.roster[currentIndex].SaveDatasheets(62, panel1);
            updateLBRoster();
        }

        private void cmbOption5_SelectedIndexChanged(object sender, EventArgs e)
        {
            roster.roster[currentIndex].SaveDatasheets(18, panel1);
            updateLBRoster();
        }

        private void cmbOption6_SelectedIndexChanged(object sender, EventArgs e)
        {
            roster.roster[currentIndex].SaveDatasheets(19, panel1);
            updateLBRoster();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SetUpForm();
        }
    }
}