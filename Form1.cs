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
using Roster_Builder.Aeldari;
using Roster_Builder.Aeldari.Harlequins;
using Roster_Builder.Tyranids;
using Roster_Builder.Adeptus_Mechanicus;
using Roster_Builder.Tau_Empire;
using Roster_Builder.Orks;
using Roster_Builder.Grey_Knights;

namespace Roster_Builder
{
    public partial class Form1 : Form
    {
        Roster roster;
        Detachment currentDetachment;
        int currentIndex = -1;
        Faction units;
        bool isLoading = false;

        public Form1()
        {
            InitializeComponent();
            this.Size = new Size(1139, 644);
            roster = new Roster();

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
            panelNewDetach.Visible = false;

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
                new AdMech(),
                new Aeldari.Aeldari(),
                new DeathGuard(),
                new Drukhari.Drukhari(),
                new GSC(),
                new GreyKnights(),
                new Harlequins(),
                new Necrons.Necrons(),
                new Orks.Orks(),
                new SpaceMarines(),
                new T_au(),
                new Tyranids.Tyranids(),
            });
            cmbSelectFaction.Text = string.Empty;

            lbUnits.Items.Clear();
            lbRoster.Items.Clear();
            cmbSubFaction.Items.Clear();

            currentIndex = -1;

            cmbNDFaction.Items.Clear();
            cmbNDFaction.Items.AddRange(new Faction[]
            {
                new AdeptusCustodes(),
                new AdMech(),
                new Aeldari.Aeldari(),
                new DeathGuard(),
				new Drukhari.Drukhari(),
				new GSC(),
                new GreyKnights(),
                new Harlequins(),
                new Necrons.Necrons(),
				new Orks.Orks(),
				new SpaceMarines(),
				new T_au(),
				new Tyranids.Tyranids(),
            });
            #endregion
        }

        private void btnBegin_Click(object sender, EventArgs e)
        {
            if(cmbSelectFaction.SelectedIndex < 0 || nudSelectPoints.Value < 500)
            {
                return;
            }

            if(!(roster == null))
            {
                roster = new Roster();
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

            units = cmbSelectFaction.SelectedItem as Faction;
            roster.CreateNewDetachment(cmbDetachment.SelectedItem.ToString(), units, txtName.Text);
            currentDetachment = roster.Detachments[0];

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

            cmbCurrentDetachment.Items.Clear();
            cmbCurrentDetachment.Items.Add(roster.Detachments[0]);
            cmbCurrentDetachment.SelectedIndex = 0;

            if(txtName.Text == "<Optional>")
            {
                currentDetachment.name = units.ToString() + " Detachment";
            }
            else
            {
                currentDetachment.name = txtName.Text;
            }
            
        }

        private void btnAddToRoster_Click(object sender, EventArgs e)
        {
            if (lbUnits.SelectedIndex >= 0)
            {
                if (lbUnits.SelectedItem is Datasheets)
                {
                    Datasheets newUnit = lbUnits.SelectedItem as Datasheets;
                    newUnit.repo = units;
                    currentDetachment.roster.Add(newUnit.CreateUnit());
                }

                updateLBRoster();
            }
        }

        private void updateLBRoster()
        {
            object zeroItem = lbRoster.Items[0];
            lbRoster.Items.Clear();

            lbRoster.Items.Add(zeroItem);
            for (int i = 0; i < currentDetachment.roster.Count; i++)
            {
                lbRoster.Items.Add(currentDetachment.roster[i].ToString());
            }

            int pts = 0;

            //for (int i = 0; i < currentDetachment.roster.Count; i++)
            //{
            //    pts += currentDetachment.roster[i].Points;
            //}

            foreach(var d in roster.Detachments)
            {
                d.Points = 0;
                foreach(var u in d.roster)
                {
                    d.Points += u.Points;
                }
                pts += d.Points;
            }

            lblPoints.Text = pts + " / " + nudSelectPoints.Text;
            if (currentIndex >= 0)
            {
                lblEditingUnit.Text = "Currently Editing: " + currentDetachment.roster[currentIndex].ToString();
            }
            /*
            int numErrors = roster.GetErrors(pts);

            if (numErrors == 0)
            {
                lblErrors.Visible = false;
                btnSave.Enabled = true;
            }
            else
            {
                lblErrors.Visible = true;
                lblErrors.Text = "Detachment has " + numErrors + " errors";
                btnSave.Enabled = false;
            }

            toolTip1.SetToolTip(lblErrors, roster.getErrorTooltip()); */
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (currentIndex < 0) return;
            currentDetachment.roster.RemoveAt(currentIndex);
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
            currentDetachment.roster[currentIndex].SaveDatasheets(30, panel1);
            updateLBRoster();
        }

        private void cmbOption1_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentDetachment.roster[currentIndex].SaveDatasheets(11, panel1);
            updateLBRoster();
        }

        private void cmbOption2_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentDetachment.roster[currentIndex].SaveDatasheets(12, panel1);
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

            panelNewDetach.Visible = false;

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

            //roster.StratagemCheck();

            currentIndex = lbRoster.SelectedIndex - 1;

            currentDetachment.roster[currentIndex].LoadDatasheets(panel1, currentDetachment.currentFaction);

            currentDetachment.roster[currentIndex].SaveDatasheets(-1, panel1);

            if (currentIndex >= 0)
            {
                lblEditingUnit.Text = "Currently Editing: " + currentDetachment.roster[currentIndex].ToString();
            }
        }

        private void cbOption1_CheckedChanged(object sender, EventArgs e)
        {
            currentDetachment.roster[currentIndex].SaveDatasheets(21, panel1);
            updateLBRoster();
        }

        private void cbOption2_CheckedChanged(object sender, EventArgs e)
        {
            currentDetachment.roster[currentIndex].SaveDatasheets(22, panel1);
            updateLBRoster();
        }

        private void cbWarlord_CheckedChanged(object sender, EventArgs e)
        {
            if (cbWarlord.Checked)
            {
                cmbWarlord.Enabled = true;
                currentDetachment.roster[currentIndex].SaveDatasheets(25, panel1);
            }
            else
            {
                cmbWarlord.Enabled = false;
                cmbWarlord.Text = string.Empty;
                currentDetachment.roster[currentIndex].SaveDatasheets(25, panel1);
            }
            updateLBRoster();
        }

        private void cmbWarlord_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentDetachment.roster[currentIndex].SaveDatasheets(15, panel1);
            updateLBRoster();
        }

        private void cmbFactionupgrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFactionupgrade.SelectedIndex != -1)
            {
                currentDetachment.roster[currentIndex].SaveDatasheets(16, panel1);
            }
            updateLBRoster();
        }

        private void clbPsyker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (clbPsyker.SelectedIndex != -1)
            {
                currentDetachment.roster[currentIndex].SaveDatasheets(60, panel1);
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

            if(currentDetachment.currentFaction.ToString() == "Space Marines")
            {
                lbUnits.Items.Clear();
                lbUnits.Items.AddRange(currentDetachment.currentFaction.GetDatasheets().ToArray());
            }
        }

        private void cmbOption3_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentDetachment.roster[currentIndex].SaveDatasheets(13, panel1);
            updateLBRoster();
        }

        private void cmbOption4_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentDetachment.roster[currentIndex].SaveDatasheets(14, panel1);
            updateLBRoster();
        }

        private void cbLeaderOption1_CheckedChanged(object sender, EventArgs e)
        {
            currentDetachment.roster[currentIndex].SaveDatasheets(421, panel1);
            updateLBRoster();
        }

        private void cbLeaderOption2_CheckedChanged(object sender, EventArgs e)
        {
            currentDetachment.roster[currentIndex].SaveDatasheets(422, panel1);
            updateLBRoster();
        }

        private void clbPsyker_DoubleClick(object sender, EventArgs e)
        {
            if (clbPsyker.SelectedIndex != -1)
            {
                currentDetachment.roster[currentIndex].SaveDatasheets(60, panel1);
            }
        }

        private void gb_cmbOption1_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentDetachment.roster[currentIndex].SaveDatasheets(411, panel1);
            updateLBRoster();
        }

        private void nudOption1_ValueChanged(object sender, EventArgs e)
        {
            currentDetachment.roster[currentIndex].SaveDatasheets(31, panel1);
            updateLBRoster();
        }
        private void nudOption2_ValueChanged(object sender, EventArgs e)
        {
            currentDetachment.roster[currentIndex].SaveDatasheets(32, panel1);
            updateLBRoster();
        }
        private void nudOption3_ValueChanged(object sender, EventArgs e)
        {
            currentDetachment.roster[currentIndex].SaveDatasheets(33, panel1);
            updateLBRoster();
        }
        private void nudOption4_ValueChanged(object sender, EventArgs e)
        {
            currentDetachment.roster[currentIndex].SaveDatasheets(34, panel1);
            updateLBRoster();
        }

        private void gb_cmbFactionupgrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentDetachment.roster[currentIndex].SaveDatasheets(416, panel1);
            updateLBRoster();
        }

        private void lbModelSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentDetachment.roster[currentIndex].SaveDatasheets(61, panel1);
            updateLBRoster();
        }

        private void cbOption3_CheckedChanged(object sender, EventArgs e)
        {
            currentDetachment.roster[currentIndex].SaveDatasheets(23, panel1);
            updateLBRoster();
        }

        private void cmbRelic_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentDetachment.roster[currentIndex].SaveDatasheets(17, panel1);
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
                    currentDetachment.roster[0].Keywords.Add(cmbCustomSub1.SelectedItem.ToString());
                    currentDetachment.roster[0].Keywords.Add(cmbCustomSub2.SelectedItem.ToString());
                    currentDetachment.roster[0].Keywords.Add(cmbSubFaction.SelectedItem.ToString());
                }
                else
                {
                    currentDetachment.roster[0].Keywords.Add(cmbSubFaction.SelectedItem.ToString());
                }

                string savePath = sfd.FileName;
                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize<List<Datasheets>>(currentDetachment.roster, options);
                File.WriteAllText(savePath, json);
            }
            catch { }

            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.AddExtension = true;
                sfd.DefaultExt = "json";
                sfd.ShowDialog();

                string savePath = sfd.FileName;
                var options = new JsonSerializerOptions { WriteIndented = true };
                var json = JsonSerializer.Serialize<Detachment>(roster, options);
                File.WriteAllText(savePath, json);
            }
            catch { }*/
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

        //    currentDetachment.roster = newRoster;

        //    lbRoster.Items.Insert(0, units.subFactionName);

        //    if (newRoster[0].Keywords.Last() == "<Custom>")
        //    {
        //        cmbSubFaction.SelectedIndex = cmbSubFaction.Items.IndexOf(currentDetachment.roster[0].Keywords.Last());
        //        cmbCustomSub2.SelectedIndex = cmbCustomSub2.Items.IndexOf(currentDetachment.roster[0].Keywords[currentDetachment.roster[0].Keywords.Count - 2]);
        //        cmbCustomSub1.SelectedIndex = cmbCustomSub1.Items.IndexOf(currentDetachment.roster[0].Keywords[currentDetachment.roster[0].Keywords.Count - 3]);

        //        currentDetachment.roster[0].Keywords.RemoveRange(currentDetachment.roster[0].Keywords.Count - 3, 3);
        //    }
        //    else
        //    {
        //        cmbSubFaction.SelectedIndex = cmbSubFaction.Items.IndexOf(currentDetachment.roster[0].Keywords.Last());

        //        currentDetachment.roster[0].Keywords.RemoveAt(currentDetachment.roster[0].Keywords.Count - 1);
        //    }
        /*
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();

            string path = ofd.FileName;
            string json = File.ReadAllText(path);

            var newRoster = JsonSerializer.Deserialize<Detachment>(json);

            roster = new Detachment(newRoster);

            nudSelectPoints.Value = Convert.ToInt32(roster.Points.ToString());

            isLoading = true;
            btnBegin.PerformClick();

            lbRoster.Items.Insert(0, units.subFactionName);

            updateLBRoster(); */
        }

        private void nudOption5_ValueChanged(object sender, EventArgs e)
        {
            currentDetachment.roster[currentIndex].SaveDatasheets(35, panel1);
            updateLBRoster();
        }

        private void nudOption6_ValueChanged(object sender, EventArgs e)
        {
            currentDetachment.roster[currentIndex].SaveDatasheets(36, panel1);
            updateLBRoster();
        }

        private void cbStratagem1_CheckedChanged(object sender, EventArgs e)
        {
            currentDetachment.roster[currentIndex].SaveDatasheets(71, panel1);
            updateLBRoster();
        }

        private void cbStratagem2_CheckedChanged(object sender, EventArgs e)
        {
            currentDetachment.roster[currentIndex].SaveDatasheets(72, panel1);
            updateLBRoster();
        }

        private void cbStratagem3_CheckedChanged(object sender, EventArgs e)
        {
            currentDetachment.roster[currentIndex].SaveDatasheets(73, panel1);
            updateLBRoster();
        }

        private void gb_cmbOption2_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentDetachment.roster[currentIndex].SaveDatasheets(412, panel1);
            updateLBRoster();
        }

        private void nudUnitSize2_ValueChanged(object sender, EventArgs e)
        {
            currentDetachment.roster[currentIndex].SaveDatasheets(62, panel1);
            updateLBRoster();
        }

        private void cmbOption5_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentDetachment.roster[currentIndex].SaveDatasheets(18, panel1);
            updateLBRoster();
        }

        private void cmbOption6_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentDetachment.roster[currentIndex].SaveDatasheets(19, panel1);
            updateLBRoster();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SetUpForm();
        }

        private void btnDetachAdd_Click(object sender, EventArgs e)
        {
            panelNewDetach.Visible = true;
            panelSubFaction.Visible = false;

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
        }

        private void btnNewDetachment_Click(object sender, EventArgs e)
        {
            roster.CreateNewDetachment(cmbNDDetachment.Text, cmbNDFaction.SelectedItem as Faction, txtNDname.Text);
            panelNewDetach.Visible = false;
            cmbCurrentDetachment.Items.Clear();
            cmbCurrentDetachment.Items.AddRange(roster.Detachments.ToArray());
        }

        private void cmbCurrentDetachment_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentDetachment = roster.Detachments[cmbCurrentDetachment.SelectedIndex];

            lbUnits.Items.Clear();
            lbUnits.Items.AddRange(currentDetachment.currentFaction.GetDatasheets().ToArray());
            lbRoster.Items.Clear();
            lbRoster.Items.Add(currentDetachment.currentFaction.subFactionName);
            lbRoster.Items.AddRange(currentDetachment.roster.ToArray());

            currentIndex = -1;
        }

        private void btnDetachRemove_Click(object sender, EventArgs e)
        {
            if(roster.Detachments.Count < 2)
            {
                MessageBox.Show("Cannot delete the only detachment; Try editing it instead.");
                return;
            }

            string message = "Are you sure you want to delete this detachment?\n" +
                "Detachment Selected: " + currentDetachment.DetachmentName +
                "\nFaction: " + currentDetachment.currentFaction.ToString();
            string caption = "Confirm Detachment Deletion";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            result = MessageBox.Show(message, caption, buttons);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                roster.Detachments.Remove(currentDetachment);
                cmbCurrentDetachment.Items.Clear();
                cmbCurrentDetachment.Items.AddRange(roster.Detachments.ToArray());
                cmbCurrentDetachment.SelectedIndex = 0;
            }
        }

        private void cmbDiscipline_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentDetachment.roster[currentIndex].SaveDatasheets(111, panel1);
            updateLBRoster();
        }

        private void cbOption4_CheckedChanged(object sender, EventArgs e)
        {
            currentDetachment.roster[currentIndex].SaveDatasheets(24, panel1);
            updateLBRoster();
        }

        private void cbOption5_CheckedChanged(object sender, EventArgs e)
        {
            currentDetachment.roster[currentIndex].SaveDatasheets(26, panel1);
            updateLBRoster();
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }
    }
}