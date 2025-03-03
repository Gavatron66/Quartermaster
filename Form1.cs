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
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Roster_Builder.Space_Marines;
using Roster_Builder.Death_Guard;
using Roster_Builder.Adeptus_Custodes;
using Roster_Builder.Genestealer_Cults;
using Roster_Builder.Aeldari.Harlequins;
using Roster_Builder.Adeptus_Mechanicus;
using Roster_Builder.Tau_Empire;
using Roster_Builder.Orks;
using Roster_Builder.Grey_Knights;
using Roster_Builder.Adepta_Sororitas;
using Roster_Builder.Astra_Militarum;
using System.Drawing.Imaging;
using Roster_Builder.Imperial_Knights;
using Roster_Builder.Leagues_of_Votann;
using Roster_Builder.Chaos_Space_Marines;

namespace Roster_Builder
{
    public partial class Form1 : Form
    {
        Roster roster;
        Detachment currentDetachment;
        int currentIndex = -1;
        Faction units;
        bool isLoading = false;
        List<int> restrictedIndexes = new List<int>();

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
            MenuPanel.Location = new System.Drawing.Point(242, 25);
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
                new AdeptaSororitas(),
                new AdeptusCustodes(),
                new AdMech(),
                new Aeldari.Aeldari(),
                new AstraMilitarum(),
                new ChaosSpaceMarines(),
                new DeathGuard(),
                new Drukhari.Drukhari(),
                new GSC(),
                new GreyKnights(),
                new Harlequins(),
                new ImperialKnights(),
                new LeaguesOfVotann(),
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
                new AdeptaSororitas(),
                new AdeptusCustodes(),
                new AdMech(),
                new Aeldari.Aeldari(),
                new AstraMilitarum(),
                new ChaosSpaceMarines(),
                new DeathGuard(),
                new Drukhari.Drukhari(),
                new GSC(),
                new GreyKnights(),
                new Harlequins(),
                new ImperialKnights(),
                new LeaguesOfVotann(),
                new Necrons.Necrons(),
                new Orks.Orks(),
                new SpaceMarines(),
                new T_au(),
                new Tyranids.Tyranids(),
            });

            lbUnits.DrawItem += new DrawItemEventHandler(DrawUnitsWithRestrictions);
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
            lblErrors.Visible = true;
            lblErrors.BringToFront();
            lblEditingUnit.BringToFront(); 
            panelSubFaction.BringToFront();
            panelSubFaction.Visible = false;

            units = cmbSelectFaction.SelectedItem as Faction;
            units.SetUpForm(this);
            units.SetPoints((int)nudSelectPoints.Value);
            roster.CreateNewDetachment(cmbDetachment.SelectedItem.ToString(), units, txtName.Text);
            currentDetachment = roster.Detachments[0];
            units.roster = currentDetachment.roster;
            cmbCurrentDetachment.Items.Add(currentDetachment);
            cmbCurrentDetachment.SelectedIndex = 0;

            List<string> subFactions = units.GetSubFactions();
            foreach (var subfaction in subFactions)
            {
                cmbSubFaction.Items.Add(subfaction);
            }

            if (!isLoading)
            {
                lbRoster.Items.Add(units.subFactionName);
                updateLBRoster();
            }

            if(txtName.Text == "<Optional>")
            {
                currentDetachment.name = units.ToString() + " Detachment";
            }
            else
            {
                currentDetachment.name = txtName.Text;
            }

            units.UpdateSubFaction(true, null);
            updateLBRoster();
        }

        private void btnAddToRoster_Click(object sender, EventArgs e)
        {
            if (lbUnits.SelectedIndex >= 0 && !restrictedIndexes.Contains(lbUnits.SelectedIndex))
            {
                if (lbUnits.SelectedItem is Datasheets)
                {
                    Datasheets newUnit = lbUnits.SelectedItem as Datasheets;
                    newUnit.repo = units;
                    units.UpdateSubFaction(true, newUnit);
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

            restrictedIndexes = units.restrictedDatasheets;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (currentIndex < 0) return;
            currentDetachment.roster[currentIndex].RemoveFromFaction();
            units.UpdateSubFaction(false, currentDetachment.roster[currentIndex]);
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
            lbUnits.SelectedIndex = -1;
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
                units.SetSubFactionPanel(panelSubFaction);
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
            units.SaveSubFaction(50, panelSubFaction);
            lbRoster.Items[0] = units.subFactionName + ": " + cmbSubFaction.SelectedItem.ToString();

            lbUnits.Items.Clear();
            lbUnits.Items.AddRange(currentDetachment.currentFaction.GetDatasheets().ToArray());
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
            cmbCurrentDetachment.SelectedIndex = roster.Detachments.Count - 1;

            panelSubFaction.Visible = false;
        }

        private void cmbCurrentDetachment_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentDetachment = roster.Detachments[cmbCurrentDetachment.SelectedIndex];
            units = currentDetachment.currentFaction;

            lbUnits.Items.Clear();
            lbUnits.Items.AddRange(currentDetachment.currentFaction.GetDatasheets().ToArray());

            lbRoster.Items.Clear();
            lbRoster.Items.Add(currentDetachment.currentFaction.subFactionName + ": " + currentDetachment.currentFaction.currentSubFaction);
            lbRoster.Items.AddRange(currentDetachment.roster.ToArray());

            currentIndex = -1;
            panelSubFaction.Visible = false;
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

        private void cmbNDFaction_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbSubCustom1_SelectedIndexChanged(object sender, EventArgs e)
        {
            units.SaveSubFaction(51, panelSubFaction);
        }

        private void cmbSubCustom2_SelectedIndexChanged(object sender, EventArgs e)
        {
            units.SaveSubFaction(52, panelSubFaction);
        }

        private void clbSubCustom_SelectedIndexChanged(object sender, EventArgs e)
        {
            units.SaveSubFaction(57, panelSubFaction);
        }

        private void cmbSubCustom3_SelectedIndexChanged(object sender, EventArgs e)
        {
            units.SaveSubFaction(53, panelSubFaction);
        }

        private void cmbSubCustom4_SelectedIndexChanged(object sender, EventArgs e)
        {
            units.SaveSubFaction(54, panelSubFaction);
        }

        private void cmbSubCustom5_SelectedIndexChanged(object sender, EventArgs e)
        {
            units.SaveSubFaction(55, panelSubFaction);
        }

        private void cmbSubCustom6_SelectedIndexChanged(object sender, EventArgs e)
        {
            units.SaveSubFaction(56, panelSubFaction);
        }

        private void AdjustComboBoxWidth_DropDown(object sender, System.EventArgs e)
        {
            ComboBox senderComboBox = (ComboBox)sender;
            int width = senderComboBox.Width;
            Graphics g = senderComboBox.CreateGraphics();
            Font font = senderComboBox.Font;
            int vertScrollBarWidth =
                (senderComboBox.Items.Count > senderComboBox.MaxDropDownItems)
                ? SystemInformation.VerticalScrollBarWidth : 0;

            int newWidth;
            foreach (var s in ((ComboBox)sender).Items)
            {
                newWidth = (int)g.MeasureString(s.ToString(), font).Width
                    + vertScrollBarWidth;
                if (width < newWidth)
                {
                    width = newWidth;
                }
            }

            senderComboBox.DropDownWidth = width;
        }

        private void DrawUnitsWithRestrictions(object sender, DrawItemEventArgs e)
        {
            //if (e.Index < 0)
            //{
            //    return;
            //}

            //// Draw the background of the ListBox control for each item.
            //Brush brush = Brushes.LightSlateGray;
            //Brush defbrush = Brushes.White;
            //Brush currentbrush = Brushes.LightBlue;

            //if (restrictedIndexes.Contains(e.Index))
            //{
            //    e = new DrawItemEventArgs(e.Graphics,
            //                      e.Font,
            //                      e.Bounds,
            //                      e.Index,
            //                      e.State ^ DrawItemState.Selected,
            //                      e.ForeColor,
            //                      Color.Yellow); // Choose the color.
            //}
            //else if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            //{
            //    e = new DrawItemEventArgs(e.Graphics,
            //                      e.Font,
            //                      e.Bounds,
            //                      e.Index,
            //                      e.State ^ DrawItemState.Selected,
            //                      e.ForeColor,
            //                      Color.Yellow); // Choose the color.
            //}

            //brush.Dispose();
            //defbrush.Dispose();
            //currentbrush.Dispose();
            //// Define the default color of the brush as black.
            //Brush myBrush = Brushes.Black;

            //// Draw the current item text based on the current Font 
            //// and the custom brush settings.
            //e.Graphics.DrawString(lbUnits.Items[e.Index].ToString(),
            //    e.Font, myBrush, e.Bounds, StringFormat.GenericDefault);
            //// If the ListBox has focus, draw a focus rectangle around the selected item.
            //e.DrawFocusRectangle();

            if (e.Index < 0) return;

            // If the item is selected them change the back color.
            if(restrictedIndexes.Contains(e.Index))
            {
                e = new DrawItemEventArgs(e.Graphics,
                                          e.Font,
                                          e.Bounds,
                                          e.Index,
                                          e.State ^ DrawItemState.Grayed,
                                          e.ForeColor,
                                          Color.LightSlateGray); // Choose the color.
            }
            else if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                e = new DrawItemEventArgs(e.Graphics,
                                          e.Font,
                                          e.Bounds,
                                          e.Index,
                                          e.State ^ DrawItemState.Selected,
                                          e.ForeColor,
                                          Color.SlateBlue); // Choose the color.

            // Draw the background of the ListBox control for each item.
            e.DrawBackground();

            // Draw the current item text
            e.Graphics.DrawString(lbUnits.Items[e.Index].ToString(), e.Font, Brushes.Black, e.Bounds, StringFormat.GenericDefault);

            // If the ListBox has focus, draw a focus rectangle around the selected item.
            e.DrawFocusRectangle();
        }
    }
}