using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Death_Guard
{
    public class Template
    {
        public Template() { }

        public void LoadTemplate(string code, Panel panel)
        {
            Form templateForm = new Testing_Form();
            GroupBox groupbox = new GroupBox();

            switch (code)
            {
                //Characters
                #region case "1m2k_pc"
                case "1m2k_pc":
                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption1"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["cmbOption1"].Visible = true;
                    panel.Controls["cmbOption1"].Location = new System.Drawing.Point(311,25);

                    panel.Controls["cbOption1"].Visible = true;
                    panel.Controls["cbOption1"].Location = new System.Drawing.Point(311, 60);

                    panel.Controls["cbOption2"].Visible = true;
                    panel.Controls["cbOption2"].Location = new System.Drawing.Point(311, 91);

                    panel.Controls["cbWarlord"].Visible = true;
                    panel.Controls["cbWarlord"].Location = new System.Drawing.Point(298, 130);

                    panel.Controls["lblWarlord"].Visible = true;
                    panel.Controls["lblWarlord"].Location = new System.Drawing.Point(294, 157);

                    panel.Controls["cmbWarlord"].Visible = true;
                    panel.Controls["cmbWarlord"].Location = new System.Drawing.Point(296, 180);

                    panel.Controls["lblPsyker"].Visible = true;
                    panel.Controls["lblPsyker"].Location = new System.Drawing.Point(86, 126);

                    panel.Controls["clbPsyker"].Visible = true;
                    panel.Controls["clbPsyker"].Location = new System.Drawing.Point(85, 149);

                    panel.Controls["lblPsykerList"].Location = new System.Drawing.Point(83, 307);

                    panel.Controls["cmbDiscipline"].Location = new System.Drawing.Point(85, 330);

                    panel.Controls["lblFactionupgrade"].Location = new System.Drawing.Point(290, 266);

                    panel.Controls["cmbFactionupgrade"].Location = new System.Drawing.Point(294, 289);

                    panel.Controls["lblRelic"].Visible = true;
                    panel.Controls["lblRelic"].Location = new System.Drawing.Point(294, 211);

                    panel.Controls["cmbRelic"].Visible = true;
                    panel.Controls["cmbRelic"].Location = new System.Drawing.Point(294, 235);

                    panel.Controls["cbStratagem1"].Visible = true;
                    panel.Controls["cbStratagem1"].Location = new System.Drawing.Point(298, 323);

                    panel.Controls["cbStratagem2"].Visible = true;
                    panel.Controls["cbStratagem2"].Location = new System.Drawing.Point(298, 353);

                    break;
                #endregion
                #region case "2m_c"
                case "2m_c":
                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption1"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["cmbOption1"].Visible = true;
                    panel.Controls["cmbOption1"].Location = new System.Drawing.Point(311, 25);

                    panel.Controls["lblOption2"].Visible = true;
                    panel.Controls["lblOption2"].Location = new System.Drawing.Point(86, 63);

                    panel.Controls["cmbOption2"].Visible = true;
                    panel.Controls["cmbOption2"].Location = new System.Drawing.Point(311, 59);

                    panel.Controls["cbWarlord"].Visible = true;
                    panel.Controls["cbWarlord"].Location = new System.Drawing.Point(100, 105);

                    panel.Controls["lblWarlord"].Visible = true;
                    panel.Controls["lblWarlord"].Location = new System.Drawing.Point(96, 132);

                    panel.Controls["cmbWarlord"].Visible = true;
                    panel.Controls["cmbWarlord"].Location = new System.Drawing.Point(98, 155);

                    panel.Controls["lblFactionupgrade"].Location = new System.Drawing.Point(293, 160);

                    panel.Controls["cmbFactionupgrade"].Location = new System.Drawing.Point(297, 184);

                    panel.Controls["lblRelic"].Visible = true;
                    panel.Controls["lblRelic"].Location = new System.Drawing.Point(293, 106);

                    panel.Controls["cmbRelic"].Visible = true;
                    panel.Controls["cmbRelic"].Location = new System.Drawing.Point(297, 129);

                    panel.Controls["cbStratagem1"].Visible = true;
                    panel.Controls["cbStratagem1"].Location = new System.Drawing.Point(298, 218);

                    panel.Controls["cbStratagem2"].Visible = true;
                    panel.Controls["cbStratagem2"].Location = new System.Drawing.Point(298, 248);
                    break;
                #endregion
                #region case "c"
                case "c":
                    panel.Controls["cbWarlord"].Visible = true;
                    panel.Controls["cbWarlord"].Location = new System.Drawing.Point(92, 25);

                    panel.Controls["lblWarlord"].Visible = true;
                    panel.Controls["lblWarlord"].Location = new System.Drawing.Point(88, 52);

                    panel.Controls["cmbWarlord"].Visible = true;
                    panel.Controls["cmbWarlord"].Location = new System.Drawing.Point(90, 75);  

                    panel.Controls["lblRelic"].Visible = true;
                    panel.Controls["lblRelic"].Location = new System.Drawing.Point(294, 28); //298, 82

                    panel.Controls["cmbRelic"].Visible = true;
                    panel.Controls["cmbRelic"].Location = new System.Drawing.Point(298, 51); //298, 106

                    panel.Controls["lblFactionupgrade"].Location = new System.Drawing.Point(298, 82);

                    panel.Controls["cmbFactionupgrade"].Location = new System.Drawing.Point(298, 106);

                    panel.Controls["cbStratagem1"].Visible = true;
                    panel.Controls["cbStratagem1"].Location = new System.Drawing.Point(298, 140);

                    panel.Controls["cbStratagem2"].Visible = true;
                    panel.Controls["cbStratagem2"].Location = new System.Drawing.Point(298, 170);

                    break;
                #endregion
                #region case "1m_c"
                case "1m_c":
                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption1"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["cmbOption1"].Visible = true;
                    panel.Controls["cmbOption1"].Location = new System.Drawing.Point(311, 25);

                    panel.Controls["cbWarlord"].Visible = true;
                    panel.Controls["cbWarlord"].Location = new System.Drawing.Point(92, 64);

                    panel.Controls["lblWarlord"].Visible = true;
                    panel.Controls["lblWarlord"].Location = new System.Drawing.Point(88, 91);

                    panel.Controls["cmbWarlord"].Visible = true;
                    panel.Controls["cmbWarlord"].Location = new System.Drawing.Point(90, 114);

                    panel.Controls["lblRelic"].Visible = true;
                    panel.Controls["lblRelic"].Location = new System.Drawing.Point(294, 67); //294, 67

                    panel.Controls["cmbRelic"].Visible = true;
                    panel.Controls["cmbRelic"].Location = new System.Drawing.Point(298, 90); //298,90

                    panel.Controls["lblFactionupgrade"].Location = new System.Drawing.Point(298, 121);

                    panel.Controls["cmbFactionupgrade"].Location = new System.Drawing.Point(298, 145);

                    panel.Controls["cbStratagem1"].Visible = true;
                    panel.Controls["cbStratagem1"].Location = new System.Drawing.Point(298, 179);

                    panel.Controls["cbStratagem2"].Visible = true;
                    panel.Controls["cbStratagem2"].Location = new System.Drawing.Point(298, 209);
                    break;
                #endregion
                #region case "2m_pc"
                case "2m_pc":
                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption1"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["cmbOption1"].Visible = true;
                    panel.Controls["cmbOption1"].Location = new System.Drawing.Point(311, 25);

                    panel.Controls["lblOption2"].Visible = true;
                    panel.Controls["lblOption2"].Location = new System.Drawing.Point(86, 63);

                    panel.Controls["cmbOption2"].Visible = true;
                    panel.Controls["cmbOption2"].Location = new System.Drawing.Point(311, 59);

                    panel.Controls["cbWarlord"].Visible = true;
                    panel.Controls["cbWarlord"].Location = new System.Drawing.Point(298, 111);

                    panel.Controls["lblWarlord"].Visible = true;
                    panel.Controls["lblWarlord"].Location = new System.Drawing.Point(294, 138);

                    panel.Controls["cmbWarlord"].Visible = true;
                    panel.Controls["cmbWarlord"].Location = new System.Drawing.Point(294, 161);

                    panel.Controls["lblPsyker"].Visible = true;
                    panel.Controls["lblPsyker"].Location = new System.Drawing.Point(86, 107);

                    panel.Controls["clbPsyker"].Visible = true;
                    panel.Controls["clbPsyker"].Location = new System.Drawing.Point(85, 130);

                    panel.Controls["lblPsykerList"].Visible = true;
                    panel.Controls["lblPsykerList"].Location = new System.Drawing.Point(83, 288);

                    panel.Controls["cmbDiscipline"].Visible = true;
                    panel.Controls["cmbDiscipline"].Location = new System.Drawing.Point(85, 311);

                    panel.Controls["lblFactionupgrade"].Location = new System.Drawing.Point(290, 247);

                    panel.Controls["cmbFactionupgrade"].Location = new System.Drawing.Point(294, 270);

                    panel.Controls["lblRelic"].Visible = true;
                    panel.Controls["lblRelic"].Location = new System.Drawing.Point(294, 192);

                    panel.Controls["cmbRelic"].Visible = true;
                    panel.Controls["cmbRelic"].Location = new System.Drawing.Point(294, 216);

                    panel.Controls["cbStratagem1"].Visible = true;
                    panel.Controls["cbStratagem1"].Location = new System.Drawing.Point(294, 304);

                    panel.Controls["cbStratagem2"].Visible = true;
                    panel.Controls["cbStratagem2"].Location = new System.Drawing.Point(294, 334);
                    break;
                #endregion
                #region case "1k_pc"
                case "1k_pc":
                    panel.Controls["cbOption1"].Visible = true;
                    panel.Controls["cbOption1"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["cbWarlord"].Visible = true;
                    panel.Controls["cbWarlord"].Location = new System.Drawing.Point(298, 56);

                    panel.Controls["lblWarlord"].Visible = true;
                    panel.Controls["lblWarlord"].Location = new System.Drawing.Point(294, 83);

                    panel.Controls["cmbWarlord"].Visible = true;
                    panel.Controls["cmbWarlord"].Location = new System.Drawing.Point(294, 106);

                    panel.Controls["lblPsyker"].Visible = true;
                    panel.Controls["lblPsyker"].Location = new System.Drawing.Point(86, 52);

                    panel.Controls["clbPsyker"].Visible = true;
                    panel.Controls["clbPsyker"].Location = new System.Drawing.Point(85, 75);

                    panel.Controls["lblFactionupgrade"].Location = new System.Drawing.Point(290, 192);

                    panel.Controls["cmbFactionupgrade"].Location = new System.Drawing.Point(294, 215);

                    panel.Controls["lblRelic"].Visible = true;
                    panel.Controls["lblRelic"].Location = new System.Drawing.Point(294, 137);

                    panel.Controls["cmbRelic"].Visible = true;
                    panel.Controls["cmbRelic"].Location = new System.Drawing.Point(294, 161);

                    panel.Controls["cbStratagem1"].Visible = true;
                    panel.Controls["cbStratagem1"].Location = new System.Drawing.Point(294, 249);

                    panel.Controls["cbStratagem2"].Visible = true;
                    panel.Controls["cbStratagem2"].Location = new System.Drawing.Point(294, 279);
                    break;
                #endregion
                #region case "ncp"
                case "ncp":
                    panel.Controls["cbWarlord"].Visible = true;
                    panel.Controls["cbWarlord"].Location = new System.Drawing.Point(298, 33);

                    panel.Controls["lblWarlord"].Visible = true;
                    panel.Controls["lblWarlord"].Location = new System.Drawing.Point(294, 60);

                    panel.Controls["cmbWarlord"].Visible = true;
                    panel.Controls["cmbWarlord"].Location = new System.Drawing.Point(294, 83);

                    panel.Controls["lblPsyker"].Visible = true;
                    panel.Controls["lblPsyker"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["clbPsyker"].Visible = true;
                    panel.Controls["clbPsyker"].Location = new System.Drawing.Point(85, 52);

                    panel.Controls["lblPsykerList"].Location = new System.Drawing.Point(83, 210);

                    panel.Controls["cmbDiscipline"].Location = new System.Drawing.Point(85, 233);
                    break;
                #endregion
                #region case "nc"
                case "nc":
                    panel.Controls["cbWarlord"].Visible = true;
                    panel.Controls["cbWarlord"].Location = new System.Drawing.Point(281, 29);

                    panel.Controls["lblWarlord"].Visible = true;
                    panel.Controls["lblWarlord"].Location = new System.Drawing.Point(279, 55);

                    panel.Controls["cmbWarlord"].Visible = true;
                    panel.Controls["cmbWarlord"].Location = new System.Drawing.Point(281, 78);
                    break;
                #endregion
                #region case "1m1k_c"
                case "1m1k_c":
                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption1"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["cmbOption1"].Visible = true;
                    panel.Controls["cmbOption1"].Location = new System.Drawing.Point(311, 25);

                    panel.Controls["cbOption1"].Visible = true;
                    panel.Controls["cbOption1"].Location = new System.Drawing.Point(311, 60);

                    panel.Controls["cbWarlord"].Visible = true;
                    panel.Controls["cbWarlord"].Location = new System.Drawing.Point(87, 87);

                    panel.Controls["lblWarlord"].Visible = true;
                    panel.Controls["lblWarlord"].Location = new System.Drawing.Point(83, 114);

                    panel.Controls["cmbWarlord"].Visible = true;
                    panel.Controls["cmbWarlord"].Location = new System.Drawing.Point(85, 137);

                    panel.Controls["lblRelic"].Visible = true;
                    panel.Controls["lblRelic"].Location = new System.Drawing.Point(293, 91); //294, 67

                    panel.Controls["cmbRelic"].Visible = true;
                    panel.Controls["cmbRelic"].Location = new System.Drawing.Point(293, 113); //298,90

                    panel.Controls["lblFactionupgrade"].Location = new System.Drawing.Point(293, 145);

                    panel.Controls["cmbFactionupgrade"].Location = new System.Drawing.Point(293, 168);

                    panel.Controls["cbStratagem1"].Visible = true;
                    panel.Controls["cbStratagem1"].Location = new System.Drawing.Point(294, 202);

                    panel.Controls["cbStratagem2"].Visible = true;
                    panel.Controls["cbStratagem2"].Location = new System.Drawing.Point(294, 232);
                    break;
                #endregion
                #region case "2m1k_c"
                case "2m1k_c":
                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption1"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["cmbOption1"].Visible = true;
                    panel.Controls["cmbOption1"].Location = new System.Drawing.Point(311, 25);

                    panel.Controls["lblOption2"].Visible = true;
                    panel.Controls["lblOption2"].Location = new System.Drawing.Point(86, 63);

                    panel.Controls["cmbOption2"].Visible = true;
                    panel.Controls["cmbOption2"].Location = new System.Drawing.Point(311, 59);

                    panel.Controls["cbOption1"].Visible = true;
                    panel.Controls["cbOption1"].Location = new System.Drawing.Point(311, 93);

                    panel.Controls["cbWarlord"].Visible = true;
                    panel.Controls["cbWarlord"].Location = new System.Drawing.Point(87, 116);

                    panel.Controls["lblWarlord"].Visible = true;
                    panel.Controls["lblWarlord"].Location = new System.Drawing.Point(83, 143);

                    panel.Controls["cmbWarlord"].Visible = true;
                    panel.Controls["cmbWarlord"].Location = new System.Drawing.Point(85, 166);

                    panel.Controls["lblRelic"].Visible = true;
                    panel.Controls["lblRelic"].Location = new System.Drawing.Point(293, 120); //294, 67

                    panel.Controls["cmbRelic"].Visible = true;
                    panel.Controls["cmbRelic"].Location = new System.Drawing.Point(293, 142); //298,90

                    panel.Controls["lblFactionupgrade"].Location = new System.Drawing.Point(293, 174);

                    panel.Controls["cmbFactionupgrade"].Location = new System.Drawing.Point(293, 197);

                    panel.Controls["cbStratagem1"].Visible = true;
                    panel.Controls["cbStratagem1"].Location = new System.Drawing.Point(294, 231);

                    panel.Controls["cbStratagem2"].Visible = true;
                    panel.Controls["cbStratagem2"].Location = new System.Drawing.Point(294, 261);

                    break;
                #endregion
                #region case "1k_c"
                case "1k_c":
                    panel.Controls["cbOption1"].Visible = true;
                    panel.Controls["cbOption1"].Location = new System.Drawing.Point(90, 29);

                    panel.Controls["cbWarlord"].Visible = true;
                    panel.Controls["cbWarlord"].Location = new System.Drawing.Point(92, 64);

                    panel.Controls["lblWarlord"].Visible = true;
                    panel.Controls["lblWarlord"].Location = new System.Drawing.Point(88, 91);

                    panel.Controls["cmbWarlord"].Visible = true;
                    panel.Controls["cmbWarlord"].Location = new System.Drawing.Point(90, 114);

                    panel.Controls["lblRelic"].Visible = true;
                    panel.Controls["lblRelic"].Location = new System.Drawing.Point(294, 67); //294, 67

                    panel.Controls["cmbRelic"].Visible = true;
                    panel.Controls["cmbRelic"].Location = new System.Drawing.Point(298, 90); //298,90

                    panel.Controls["lblFactionupgrade"].Location = new System.Drawing.Point(298, 121);

                    panel.Controls["cmbFactionupgrade"].Location = new System.Drawing.Point(298, 145);

                    panel.Controls["cbStratagem1"].Visible = true;
                    panel.Controls["cbStratagem1"].Location = new System.Drawing.Point(296, 179);

                    panel.Controls["cbStratagem2"].Visible = true;
                    panel.Controls["cbStratagem2"].Location = new System.Drawing.Point(296, 209);
                    break;
                #endregion
                #region case "pc"
                case "pc":
                    panel.Controls["cbWarlord"].Visible = true;
                    panel.Controls["cbWarlord"].Location = new System.Drawing.Point(298, 33);

                    panel.Controls["lblWarlord"].Visible = true;
                    panel.Controls["lblWarlord"].Location = new System.Drawing.Point(294, 60);

                    panel.Controls["cmbWarlord"].Visible = true;
                    panel.Controls["cmbWarlord"].Location = new System.Drawing.Point(296, 83);

                    panel.Controls["lblPsyker"].Visible = true;
                    panel.Controls["lblPsyker"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["clbPsyker"].Visible = true;
                    panel.Controls["clbPsyker"].Location = new System.Drawing.Point(85, 52);

                    panel.Controls["lblPsykerList"].Location = new System.Drawing.Point(83, 210);

                    panel.Controls["cmbDiscipline"].Location = new System.Drawing.Point(85, 233);

                    panel.Controls["lblFactionupgrade"].Location = new System.Drawing.Point(290, 169);

                    panel.Controls["cmbFactionupgrade"].Location = new System.Drawing.Point(294, 192);

                    panel.Controls["lblRelic"].Visible = true;
                    panel.Controls["lblRelic"].Location = new System.Drawing.Point(294, 114);

                    panel.Controls["cmbRelic"].Visible = true;
                    panel.Controls["cmbRelic"].Location = new System.Drawing.Point(294, 138);

                    panel.Controls["cbStratagem1"].Visible = true;
                    panel.Controls["cbStratagem1"].Location = new System.Drawing.Point(298, 226);

                    panel.Controls["cbStratagem2"].Visible = true;
                    panel.Controls["cbStratagem2"].Location = new System.Drawing.Point(298, 256);

                    break;
                #endregion
                #region case "2m1k_pc"
                case "2m1k_pc":
                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption1"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["cmbOption1"].Visible = true;
                    panel.Controls["cmbOption1"].Location = new System.Drawing.Point(311, 25);

                    panel.Controls["lblOption2"].Visible = true;
                    panel.Controls["lblOption2"].Location = new System.Drawing.Point(86, 63);

                    panel.Controls["cmbOption2"].Visible = true;
                    panel.Controls["cmbOption2"].Location = new System.Drawing.Point(311, 59);

                    panel.Controls["cbOption1"].Visible = true;
                    panel.Controls["cbOption1"].Location = new System.Drawing.Point(311, 93);

                    panel.Controls["lblPsyker"].Visible = true;
                    panel.Controls["lblPsyker"].Location = new System.Drawing.Point(86, 126);

                    panel.Controls["clbPsyker"].Visible = true;
                    panel.Controls["clbPsyker"].Location = new System.Drawing.Point(85, 149);

                    panel.Controls["lblPsykerList"].Location = new System.Drawing.Point(83, 307);

                    panel.Controls["cmbDiscipline"].Location = new System.Drawing.Point(85, 330);

                    panel.Controls["cbWarlord"].Visible = true;
                    panel.Controls["cbWarlord"].Location = new System.Drawing.Point(298, 130);

                    panel.Controls["lblWarlord"].Visible = true;
                    panel.Controls["lblWarlord"].Location = new System.Drawing.Point(294, 157);

                    panel.Controls["cmbWarlord"].Visible = true;
                    panel.Controls["cmbWarlord"].Location = new System.Drawing.Point(296, 180);

                    panel.Controls["lblRelic"].Visible = true;
                    panel.Controls["lblRelic"].Location = new System.Drawing.Point(296, 211); //294, 67

                    panel.Controls["cmbRelic"].Visible = true;
                    panel.Controls["cmbRelic"].Location = new System.Drawing.Point(296, 235); //298,90

                    panel.Controls["lblFactionupgrade"].Location = new System.Drawing.Point(290, 266);

                    panel.Controls["cmbFactionupgrade"].Location = new System.Drawing.Point(294, 289);

                    panel.Controls["cbStratagem1"].Visible = true;
                    panel.Controls["cbStratagem1"].Location = new System.Drawing.Point(298, 323);

                    panel.Controls["cbStratagem2"].Visible = true;
                    panel.Controls["cbStratagem2"].Location = new System.Drawing.Point(298, 353);

                    break;
                #endregion
                #region case "1m_pc"
                case "1m_pc":
                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption1"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["cmbOption1"].Visible = true;
                    panel.Controls["cmbOption1"].Location = new System.Drawing.Point(311, 25);

                    panel.Controls["cbWarlord"].Visible = true;
                    panel.Controls["cbWarlord"].Location = new System.Drawing.Point(298, 76);

                    panel.Controls["lblWarlord"].Visible = true;
                    panel.Controls["lblWarlord"].Location = new System.Drawing.Point(294, 103);

                    panel.Controls["cmbWarlord"].Visible = true;
                    panel.Controls["cmbWarlord"].Location = new System.Drawing.Point(294, 126);

                    panel.Controls["lblPsyker"].Visible = true;
                    panel.Controls["lblPsyker"].Location = new System.Drawing.Point(86, 72);

                    panel.Controls["clbPsyker"].Visible = true;
                    panel.Controls["clbPsyker"].Location = new System.Drawing.Point(85, 95);

                    panel.Controls["lblPsykerList"].Location = new System.Drawing.Point(83, 253);

                    panel.Controls["cmbDiscipline"].Location = new System.Drawing.Point(85, 276);

                    panel.Controls["lblFactionupgrade"].Location = new System.Drawing.Point(290, 212);

                    panel.Controls["cmbFactionupgrade"].Location = new System.Drawing.Point(294, 235);

                    panel.Controls["lblRelic"].Visible = true;
                    panel.Controls["lblRelic"].Location = new System.Drawing.Point(294, 157);

                    panel.Controls["cmbRelic"].Visible = true;
                    panel.Controls["cmbRelic"].Location = new System.Drawing.Point(294, 181);

                    panel.Controls["cbStratagem1"].Visible = true;
                    panel.Controls["cbStratagem1"].Location = new System.Drawing.Point(294, 269);

                    panel.Controls["cbStratagem2"].Visible = true;
                    panel.Controls["cbStratagem2"].Location = new System.Drawing.Point(294, 299);
                    break;
                #endregion
                #region case "1m1k_pc"
                case "1m1k_pc":
                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption1"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["cmbOption1"].Visible = true;
                    panel.Controls["cmbOption1"].Location = new System.Drawing.Point(311, 25);

                    panel.Controls["cbOption1"].Visible = true;
                    panel.Controls["cbOption1"].Location = new System.Drawing.Point(311, 64);

                    panel.Controls["lblPsyker"].Visible = true;
                    panel.Controls["lblPsyker"].Location = new System.Drawing.Point(86, 96);

                    panel.Controls["clbPsyker"].Visible = true;
                    panel.Controls["clbPsyker"].Location = new System.Drawing.Point(85, 119);

                    panel.Controls["lblPsykerList"].Location = new System.Drawing.Point(83, 277);

                    panel.Controls["cmbDiscipline"].Location = new System.Drawing.Point(85, 300);

                    panel.Controls["cbWarlord"].Visible = true;
                    panel.Controls["cbWarlord"].Location = new System.Drawing.Point(298, 100);

                    panel.Controls["lblWarlord"].Visible = true;
                    panel.Controls["lblWarlord"].Location = new System.Drawing.Point(294, 127);

                    panel.Controls["cmbWarlord"].Visible = true;
                    panel.Controls["cmbWarlord"].Location = new System.Drawing.Point(296, 150);

                    panel.Controls["lblRelic"].Visible = true;
                    panel.Controls["lblRelic"].Location = new System.Drawing.Point(296, 181); //294, 67

                    panel.Controls["cmbRelic"].Visible = true;
                    panel.Controls["cmbRelic"].Location = new System.Drawing.Point(296, 205); //298,90

                    panel.Controls["lblFactionupgrade"].Location = new System.Drawing.Point(290, 236);

                    panel.Controls["cmbFactionupgrade"].Location = new System.Drawing.Point(294, 259);

                    panel.Controls["cbStratagem1"].Visible = true;
                    panel.Controls["cbStratagem1"].Location = new System.Drawing.Point(298, 293);

                    panel.Controls["cbStratagem2"].Visible = true;
                    panel.Controls["cbStratagem2"].Location = new System.Drawing.Point(298, 323);

                    break;
                #endregion
                #region case "2m2k_c"
                case "2m2k_c":
                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption1"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["cmbOption1"].Visible = true;
                    panel.Controls["cmbOption1"].Location = new System.Drawing.Point(311, 25);

                    panel.Controls["lblOption2"].Visible = true;
                    panel.Controls["lblOption2"].Location = new System.Drawing.Point(86, 63);

                    panel.Controls["cmbOption2"].Visible = true;
                    panel.Controls["cmbOption2"].Location = new System.Drawing.Point(311, 59);

                    panel.Controls["cbOption1"].Visible = true;
                    panel.Controls["cbOption1"].Location = new System.Drawing.Point(311, 93);

                    panel.Controls["cbOption2"].Visible = true;
                    panel.Controls["cbOption2"].Location = new System.Drawing.Point(311, 123);

                    panel.Controls["cbWarlord"].Visible = true;
                    panel.Controls["cbWarlord"].Location = new System.Drawing.Point(87, 146);

                    panel.Controls["lblWarlord"].Visible = true;
                    panel.Controls["lblWarlord"].Location = new System.Drawing.Point(83, 173);

                    panel.Controls["cmbWarlord"].Visible = true;
                    panel.Controls["cmbWarlord"].Location = new System.Drawing.Point(85, 196);

                    panel.Controls["lblRelic"].Visible = true;
                    panel.Controls["lblRelic"].Location = new System.Drawing.Point(293, 150); //294, 67

                    panel.Controls["cmbRelic"].Visible = true;
                    panel.Controls["cmbRelic"].Location = new System.Drawing.Point(293, 172); //298,90

                    panel.Controls["lblFactionupgrade"].Location = new System.Drawing.Point(293, 204);

                    panel.Controls["cmbFactionupgrade"].Location = new System.Drawing.Point(293, 227);

                    panel.Controls["cbStratagem1"].Visible = true;
                    panel.Controls["cbStratagem1"].Location = new System.Drawing.Point(294, 261);

                    panel.Controls["cbStratagem2"].Visible = true;
                    panel.Controls["cbStratagem2"].Location = new System.Drawing.Point(294, 291);

                    break;
                #endregion
                #region case "2m3k_c"
                case "2m3k_c":
                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption1"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["cmbOption1"].Visible = true;
                    panel.Controls["cmbOption1"].Location = new System.Drawing.Point(311, 25);

                    panel.Controls["lblOption2"].Visible = true;
                    panel.Controls["lblOption2"].Location = new System.Drawing.Point(86, 63);

                    panel.Controls["cmbOption2"].Visible = true;
                    panel.Controls["cmbOption2"].Location = new System.Drawing.Point(311, 59);

                    panel.Controls["cbOption1"].Visible = true;
                    panel.Controls["cbOption1"].Location = new System.Drawing.Point(311, 93);

                    panel.Controls["cbOption2"].Visible = true;
                    panel.Controls["cbOption2"].Location = new System.Drawing.Point(311, 123);

                    panel.Controls["cbOption3"].Visible = true;
                    panel.Controls["cbOption3"].Location = new System.Drawing.Point(311, 153);

                    panel.Controls["cbWarlord"].Visible = true;
                    panel.Controls["cbWarlord"].Location = new System.Drawing.Point(87, 176);

                    panel.Controls["lblWarlord"].Visible = true;
                    panel.Controls["lblWarlord"].Location = new System.Drawing.Point(83, 203);

                    panel.Controls["cmbWarlord"].Visible = true;
                    panel.Controls["cmbWarlord"].Location = new System.Drawing.Point(85, 226);

                    panel.Controls["lblRelic"].Visible = true;
                    panel.Controls["lblRelic"].Location = new System.Drawing.Point(293, 180); //294, 67

                    panel.Controls["cmbRelic"].Visible = true;
                    panel.Controls["cmbRelic"].Location = new System.Drawing.Point(293, 202); //298,90

                    panel.Controls["lblFactionupgrade"].Location = new System.Drawing.Point(293, 234);

                    panel.Controls["cmbFactionupgrade"].Location = new System.Drawing.Point(293, 257);

                    panel.Controls["cbStratagem1"].Visible = true;
                    panel.Controls["cbStratagem1"].Location = new System.Drawing.Point(294, 291);

                    panel.Controls["cbStratagem2"].Visible = true;
                    panel.Controls["cbStratagem2"].Location = new System.Drawing.Point(294, 321);

                    break;
                #endregion
                #region case "2m2k_pc"
                case "2m2k_pc":
                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption1"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["cmbOption1"].Visible = true;
                    panel.Controls["cmbOption1"].Location = new System.Drawing.Point(311, 25);

                    panel.Controls["lblOption2"].Visible = true;
                    panel.Controls["lblOption2"].Location = new System.Drawing.Point(86, 63);

                    panel.Controls["cmbOption2"].Visible = true;
                    panel.Controls["cmbOption2"].Location = new System.Drawing.Point(311, 59);

                    panel.Controls["cbOption1"].Visible = true;
                    panel.Controls["cbOption1"].Location = new System.Drawing.Point(311, 93);

                    panel.Controls["cbOption2"].Visible = true;
                    panel.Controls["cbOption2"].Location = new System.Drawing.Point(311, 122);

                    panel.Controls["lblPsyker"].Visible = true;
                    panel.Controls["lblPsyker"].Location = new System.Drawing.Point(86, 146);

                    panel.Controls["clbPsyker"].Visible = true;
                    panel.Controls["clbPsyker"].Location = new System.Drawing.Point(85, 169);

                    panel.Controls["lblPsykerList"].Location = new System.Drawing.Point(83, 327);

                    panel.Controls["cmbDiscipline"].Location = new System.Drawing.Point(85, 350);

                    panel.Controls["cbWarlord"].Visible = true;
                    panel.Controls["cbWarlord"].Location = new System.Drawing.Point(298, 150);

                    panel.Controls["lblWarlord"].Visible = true;
                    panel.Controls["lblWarlord"].Location = new System.Drawing.Point(294, 177);

                    panel.Controls["cmbWarlord"].Visible = true;
                    panel.Controls["cmbWarlord"].Location = new System.Drawing.Point(296, 200);

                    panel.Controls["lblRelic"].Visible = true;
                    panel.Controls["lblRelic"].Location = new System.Drawing.Point(296, 231); //294, 67

                    panel.Controls["cmbRelic"].Visible = true;
                    panel.Controls["cmbRelic"].Location = new System.Drawing.Point(296, 255); //298,90

                    panel.Controls["lblFactionupgrade"].Location = new System.Drawing.Point(290, 266);

                    panel.Controls["cmbFactionupgrade"].Location = new System.Drawing.Point(294, 289);

                    panel.Controls["cbStratagem1"].Visible = true;
                    panel.Controls["cbStratagem1"].Location = new System.Drawing.Point(298, 343);

                    panel.Controls["cbStratagem2"].Visible = true;
                    panel.Controls["cbStratagem2"].Location = new System.Drawing.Point(298, 373);

                    break;
                #endregion
                #region case "1m2k_c"
                case "1m2k_c":
                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption1"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["cmbOption1"].Visible = true;
                    panel.Controls["cmbOption1"].Location = new System.Drawing.Point(311, 25);

                    panel.Controls["cbOption1"].Visible = true;
                    panel.Controls["cbOption1"].Location = new System.Drawing.Point(311, 60);

                    panel.Controls["cbOption2"].Visible = true;
                    panel.Controls["cbOption2"].Location = new System.Drawing.Point(311, 91);

                    panel.Controls["cbWarlord"].Visible = true;
                    panel.Controls["cbWarlord"].Location = new System.Drawing.Point(298, 130);

                    panel.Controls["lblWarlord"].Visible = true;
                    panel.Controls["lblWarlord"].Location = new System.Drawing.Point(294, 157);

                    panel.Controls["cmbWarlord"].Visible = true;
                    panel.Controls["cmbWarlord"].Location = new System.Drawing.Point(296, 180);

                    panel.Controls["lblFactionupgrade"].Location = new System.Drawing.Point(290, 266);

                    panel.Controls["cmbFactionupgrade"].Location = new System.Drawing.Point(294, 289);

                    panel.Controls["lblRelic"].Visible = true;
                    panel.Controls["lblRelic"].Location = new System.Drawing.Point(294, 211);

                    panel.Controls["cmbRelic"].Visible = true;
                    panel.Controls["cmbRelic"].Location = new System.Drawing.Point(294, 235);

                    panel.Controls["cbStratagem1"].Visible = true;
                    panel.Controls["cbStratagem1"].Location = new System.Drawing.Point(298, 323);

                    panel.Controls["cbStratagem2"].Visible = true;
                    panel.Controls["cbStratagem2"].Location = new System.Drawing.Point(298, 353);

                    break;
                #endregion

                //Normal Units
                #region case "1m"
                case "1m":
                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption1"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["cmbOption1"].Visible = true;
                    panel.Controls["cmbOption1"].Location = new System.Drawing.Point(311, 25);

                    panel.Controls["lblFactionupgrade"].Location = new System.Drawing.Point(293, 59);

                    panel.Controls["cmbFactionupgrade"].Location = new System.Drawing.Point(293, 85);
                    break;
                #endregion
                #region case "2m"
                case "2m":
                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption1"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["cmbOption1"].Visible = true;
                    panel.Controls["cmbOption1"].Location = new System.Drawing.Point(311, 25);

                    panel.Controls["lblOption2"].Visible = true;
                    panel.Controls["lblOption2"].Location = new System.Drawing.Point(86, 63);

                    panel.Controls["cmbOption2"].Visible = true;
                    panel.Controls["cmbOption2"].Location = new System.Drawing.Point(311, 59);

                    break;
                #endregion
                #region case "1m1k"
                case "1m1k":
                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption1"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["cmbOption1"].Visible = true;
                    panel.Controls["cmbOption1"].Location = new System.Drawing.Point(311, 25);

                    panel.Controls["cbOption1"].Visible = true;
                    panel.Controls["cbOption1"].Location = new System.Drawing.Point(311, 60);

                    panel.Controls["lblFactionupgrade"].Location = new System.Drawing.Point(310, 87);

                    panel.Controls["cmbFactionupgrade"].Location = new System.Drawing.Point(310, 111);

                    break;
                #endregion
                #region case "2m1k"
                case "2m1k":
                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption1"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["cmbOption1"].Visible = true;
                    panel.Controls["cmbOption1"].Location = new System.Drawing.Point(311, 25);

                    panel.Controls["lblOption2"].Visible = true;
                    panel.Controls["lblOption2"].Location = new System.Drawing.Point(86, 63);

                    panel.Controls["cmbOption2"].Visible = true;
                    panel.Controls["cmbOption2"].Location = new System.Drawing.Point(311, 59);

                    panel.Controls["cbOption1"].Visible = true;
                    panel.Controls["cbOption1"].Location = new System.Drawing.Point(311, 93);

                    break;
                #endregion
                #region case "3m"
                case "3m":
                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption1"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["cmbOption1"].Visible = true;
                    panel.Controls["cmbOption1"].Location = new System.Drawing.Point(311, 25);

                    panel.Controls["lblOption2"].Visible = true;
                    panel.Controls["lblOption2"].Location = new System.Drawing.Point(86, 63);

                    panel.Controls["cmbOption2"].Visible = true;
                    panel.Controls["cmbOption2"].Location = new System.Drawing.Point(311, 59);

                    panel.Controls["lblOption3"].Visible = true;
                    panel.Controls["lblOption3"].Location = new System.Drawing.Point(86, 97);

                    panel.Controls["cmbOption3"].Visible = true;
                    panel.Controls["cmbOption3"].Location = new System.Drawing.Point(311, 93);

                    break;
                #endregion
                #region case "2k"
                case "2k":
                    panel.Controls["cbOption1"].Visible = true;
                    panel.Controls["cbOption1"].Location = new System.Drawing.Point(311, 60);

                    panel.Controls["cbOption2"].Visible = true;
                    panel.Controls["cbOption2"].Location = new System.Drawing.Point(311, 90);

                    break;
                #endregion
                #region case "1k"
                case "1k":
                    panel.Controls["cbOption1"].Visible = true;
                    panel.Controls["cbOption1"].Location = new System.Drawing.Point(311, 60);

                    panel.Controls["lblFactionupgrade"].Location = new System.Drawing.Point(98, 29);

                    panel.Controls["cmbFactionupgrade"].Location = new System.Drawing.Point(98, 53);

                    break;
                #endregion
                #region case "3m1k1N"
                case "3m1k1N":
                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption1"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["cmbOption1"].Visible = true;
                    panel.Controls["cmbOption1"].Location = new System.Drawing.Point(311, 25);

                    panel.Controls["lblOption2"].Visible = true;
                    panel.Controls["lblOption2"].Location = new System.Drawing.Point(86, 63);

                    panel.Controls["cmbOption2"].Visible = true;
                    panel.Controls["cmbOption2"].Location = new System.Drawing.Point(311, 59);

                    panel.Controls["lblOption3"].Visible = true;
                    panel.Controls["lblOption3"].Location = new System.Drawing.Point(86, 97);

                    panel.Controls["cmbOption3"].Visible = true;
                    panel.Controls["cmbOption3"].Location = new System.Drawing.Point(311, 93);

                    panel.Controls["lblnud1"].Visible = true;
                    panel.Controls["lblnud1"].Location = new System.Drawing.Point(154, 129);

                    panel.Controls["nudOption1"].Visible = true;
                    panel.Controls["nudOption1"].Location = new System.Drawing.Point(311, 127);

                    panel.Controls["cbOption1"].Visible = true;
                    panel.Controls["cbOption1"].Location = new System.Drawing.Point(311, 159);

                    break;
                #endregion
                #region case "3m1k"
                case "3m1k":
                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption1"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["cmbOption1"].Visible = true;
                    panel.Controls["cmbOption1"].Location = new System.Drawing.Point(311, 25);

                    panel.Controls["lblOption2"].Visible = true;
                    panel.Controls["lblOption2"].Location = new System.Drawing.Point(86, 63);

                    panel.Controls["cmbOption2"].Visible = true;
                    panel.Controls["cmbOption2"].Location = new System.Drawing.Point(311, 59);

                    panel.Controls["lblOption3"].Visible = true;
                    panel.Controls["lblOption3"].Location = new System.Drawing.Point(86, 97);

                    panel.Controls["cmbOption3"].Visible = true;
                    panel.Controls["cmbOption3"].Location = new System.Drawing.Point(311, 93);

                    panel.Controls["cbOption1"].Visible = true;
                    panel.Controls["cbOption1"].Location = new System.Drawing.Point(311, 127);

                    break;
                #endregion
                #region case "1m2k"
                case "1m2k":
                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption1"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["cmbOption1"].Visible = true;
                    panel.Controls["cmbOption1"].Location = new System.Drawing.Point(311, 25);

                    panel.Controls["cbOption1"].Visible = true;
                    panel.Controls["cbOption1"].Location = new System.Drawing.Point(311, 60);

                    panel.Controls["cbOption2"].Visible = true;
                    panel.Controls["cbOption2"].Location = new System.Drawing.Point(311, 90);

                    panel.Controls["lblFactionupgrade"].Location = new System.Drawing.Point(307, 117);

                    panel.Controls["cmbFactionupgrade"].Location = new System.Drawing.Point(307, 141);

                    break;
                #endregion
                #region case "1m3k"
                case "1m3k":
                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption1"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["cmbOption1"].Visible = true;
                    panel.Controls["cmbOption1"].Location = new System.Drawing.Point(311, 25);

                    panel.Controls["cbOption1"].Visible = true;
                    panel.Controls["cbOption1"].Location = new System.Drawing.Point(311, 60);

                    panel.Controls["cbOption2"].Visible = true;
                    panel.Controls["cbOption2"].Location = new System.Drawing.Point(311, 90);

                    panel.Controls["cbOption3"].Visible = true;
                    panel.Controls["cbOption3"].Location = new System.Drawing.Point(311, 120);

                    panel.Controls["lblFactionupgrade"].Location = new System.Drawing.Point(307, 145);

                    panel.Controls["cmbFactionupgrade"].Location = new System.Drawing.Point(307, 169);

                    break;
                #endregion
                #region case "3k"
                case "3k":
                    panel.Controls["cbOption1"].Visible = true;
                    panel.Controls["cbOption1"].Location = new System.Drawing.Point(311, 60);

                    panel.Controls["cbOption2"].Visible = true;
                    panel.Controls["cbOption2"].Location = new System.Drawing.Point(311, 90);

                    panel.Controls["cbOption3"].Visible = true;
                    panel.Controls["cbOption3"].Location = new System.Drawing.Point(311, 120);

                    break;
                #endregion
                #region case "6m1k"
                case "6m1k":
                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption1"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["cmbOption1"].Visible = true;
                    panel.Controls["cmbOption1"].Location = new System.Drawing.Point(311, 25);

                    panel.Controls["lblOption2"].Visible = true;
                    panel.Controls["lblOption2"].Location = new System.Drawing.Point(86, 63);

                    panel.Controls["cmbOption2"].Visible = true;
                    panel.Controls["cmbOption2"].Location = new System.Drawing.Point(311, 59);

                    panel.Controls["lblOption3"].Visible = true;
                    panel.Controls["lblOption3"].Location = new System.Drawing.Point(86, 97);

                    panel.Controls["cmbOption3"].Visible = true;
                    panel.Controls["cmbOption3"].Location = new System.Drawing.Point(311, 93);

                    panel.Controls["lblOption4"].Visible = true;
                    panel.Controls["lblOption4"].Location = new System.Drawing.Point(86, 131);

                    panel.Controls["cmbOption4"].Visible = true;
                    panel.Controls["cmbOption4"].Location = new System.Drawing.Point(311, 127);

                    panel.Controls["lblOption5"].Visible = true;
                    panel.Controls["lblOption5"].Location = new System.Drawing.Point(86, 165);

                    panel.Controls["cmbOption5"].Visible = true;
                    panel.Controls["cmbOption5"].Location = new System.Drawing.Point(311, 161);

                    panel.Controls["lblOption6"].Visible = true;
                    panel.Controls["lblOption6"].Location = new System.Drawing.Point(86, 199);

                    panel.Controls["cmbOption6"].Visible = true;
                    panel.Controls["cmbOption6"].Location = new System.Drawing.Point(311, 195);

                    panel.Controls["cbOption1"].Visible = true;
                    panel.Controls["cbOption1"].Location = new System.Drawing.Point(311, 229);

                    break;
                #endregion
                #region case "4m1k"
                case "4m1k":
                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption1"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["cmbOption1"].Visible = true;
                    panel.Controls["cmbOption1"].Location = new System.Drawing.Point(311, 25);

                    panel.Controls["lblOption2"].Visible = true;
                    panel.Controls["lblOption2"].Location = new System.Drawing.Point(86, 63);

                    panel.Controls["cmbOption2"].Visible = true;
                    panel.Controls["cmbOption2"].Location = new System.Drawing.Point(311, 59);

                    panel.Controls["lblOption3"].Visible = true;
                    panel.Controls["lblOption3"].Location = new System.Drawing.Point(86, 97);

                    panel.Controls["cmbOption3"].Visible = true;
                    panel.Controls["cmbOption3"].Location = new System.Drawing.Point(311, 93);

                    panel.Controls["lblOption4"].Visible = true;
                    panel.Controls["lblOption4"].Location = new System.Drawing.Point(86, 131);

                    panel.Controls["cmbOption4"].Visible = true;
                    panel.Controls["cmbOption4"].Location = new System.Drawing.Point(311, 127);

                    panel.Controls["cbOption1"].Visible = true;
                    panel.Controls["cbOption1"].Location = new System.Drawing.Point(311, 161);

                    break;
                #endregion
                #region case "2m4k"
                case "2m4k":
                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption1"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["cmbOption1"].Visible = true;
                    panel.Controls["cmbOption1"].Location = new System.Drawing.Point(311, 25);

                    panel.Controls["lblOption2"].Visible = true;
                    panel.Controls["lblOption2"].Location = new System.Drawing.Point(86, 63);

                    panel.Controls["cmbOption2"].Visible = true;
                    panel.Controls["cmbOption2"].Location = new System.Drawing.Point(311, 59);

                    panel.Controls["cbOption1"].Visible = true;
                    panel.Controls["cbOption1"].Location = new System.Drawing.Point(311, 93);

                    panel.Controls["cbOption2"].Visible = true;
                    panel.Controls["cbOption2"].Location = new System.Drawing.Point(311, 123);

                    panel.Controls["cbOption3"].Visible = true;
                    panel.Controls["cbOption3"].Location = new System.Drawing.Point(311, 153);

                    panel.Controls["cbOption4"].Visible = true;
                    panel.Controls["cbOption4"].Location = new System.Drawing.Point(311, 183);

                    break;
                #endregion
                #region case "1m4k"
                case "1m4k":
                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption1"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["cmbOption1"].Visible = true;
                    panel.Controls["cmbOption1"].Location = new System.Drawing.Point(311, 25);

                    panel.Controls["cbOption1"].Visible = true;
                    panel.Controls["cbOption1"].Location = new System.Drawing.Point(311, 60);

                    panel.Controls["cbOption2"].Visible = true;
                    panel.Controls["cbOption2"].Location = new System.Drawing.Point(311, 90);

                    panel.Controls["cbOption3"].Visible = true;
                    panel.Controls["cbOption3"].Location = new System.Drawing.Point(311, 120);

                    panel.Controls["cbOption4"].Visible = true;
                    panel.Controls["cbOption4"].Location = new System.Drawing.Point(311, 150);

                    break;
                #endregion
                #region case "4m"
                case "4m":
                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption1"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["cmbOption1"].Visible = true;
                    panel.Controls["cmbOption1"].Location = new System.Drawing.Point(311, 25);

                    panel.Controls["lblOption2"].Visible = true;
                    panel.Controls["lblOption2"].Location = new System.Drawing.Point(86, 63);

                    panel.Controls["cmbOption2"].Visible = true;
                    panel.Controls["cmbOption2"].Location = new System.Drawing.Point(311, 59);

                    panel.Controls["lblOption3"].Visible = true;
                    panel.Controls["lblOption3"].Location = new System.Drawing.Point(86, 97);

                    panel.Controls["cmbOption3"].Visible = true;
                    panel.Controls["cmbOption3"].Location = new System.Drawing.Point(311, 93);

                    panel.Controls["lblOption4"].Visible = true;
                    panel.Controls["lblOption4"].Location = new System.Drawing.Point(86, 131);

                    panel.Controls["cmbOption4"].Visible = true;
                    panel.Controls["cmbOption4"].Location = new System.Drawing.Point(311, 127);

                    break;
                #endregion

                //Units with variable Unit Size
                #region case "N"
                case "N":
                    panel.Controls["lblNumModels"].Visible = true;
                    panel.Controls["lblNumModels"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["nudUnitSize"].Visible = true;
                    panel.Controls["nudUnitSize"].Location = new System.Drawing.Point(243, 27);

                    panel.Controls["lblFactionupgrade"].Location = new System.Drawing.Point(239, 59);

                    panel.Controls["cmbFactionupgrade"].Location = new System.Drawing.Point(243, 82);
                    break;
                #endregion
                #region case "NS(2k)"
                case "NS(2k)":
                    panel.Controls["lblNumModels"].Visible = true;
                    panel.Controls["lblNumModels"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["nudUnitSize"].Visible = true;
                    panel.Controls["nudUnitSize"].Location = new System.Drawing.Point(243, 27);

                    panel.Controls["gbUnitLeader"].Visible = true;
                    panel.Controls["gbUnitLeader"].Location = new System.Drawing.Point(90, 59);
                    panel.Controls["gbUnitLeader"].Size = new System.Drawing.Size(359, 108);

                    groupbox = panel.Controls["gbUnitLeader"] as GroupBox;

                    groupbox.Controls["cbLeaderOption1"].Visible = true;
                    groupbox.Controls["cbLeaderOption1"].Location = new System.Drawing.Point(25, 37);

                    groupbox.Controls["cbLeaderOption2"].Visible = true;
                    groupbox.Controls["cbLeaderOption2"].Location = new System.Drawing.Point(25, 63);

                    panel.Controls["lblFactionupgrade"].Location = new System.Drawing.Point(86, 170);

                    panel.Controls["cmbFactionupgrade"].Location = new System.Drawing.Point(90, 193);
                    break;
                #endregion
                #region case "3N"
                case "3N":
                    panel.Controls["lblNumModels"].Visible = true;
                    panel.Controls["lblNumModels"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["nudUnitSize"].Visible = true;
                    panel.Controls["nudUnitSize"].Location = new System.Drawing.Point(243, 27);

                    panel.Controls["lblnud1"].Visible = true;
                    panel.Controls["lblnud1"].Location = new System.Drawing.Point(126, 61);

                    panel.Controls["nudOption1"].Visible = true;
                    panel.Controls["nudOption1"].Location = new System.Drawing.Point(283, 59);

                    panel.Controls["lblnud2"].Visible = true;
                    panel.Controls["lblnud2"].Location = new System.Drawing.Point(126, 93);

                    panel.Controls["nudOption2"].Visible = true;
                    panel.Controls["nudOption2"].Location = new System.Drawing.Point(283, 91);
                    break;
                #endregion
                #region case "N1m"
                case "N1m":
                    panel.Controls["lblNumModels"].Visible = true;
                    panel.Controls["lblNumModels"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["nudUnitSize"].Visible = true;
                    panel.Controls["nudUnitSize"].Location = new System.Drawing.Point(243, 27);

                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption1"].Location = new System.Drawing.Point(86, 63);

                    panel.Controls["cmbOption1"].Visible = true;
                    panel.Controls["cmbOption1"].Location = new System.Drawing.Point(311, 59);

                    panel.Controls["lblFactionupgrade"].Location = new System.Drawing.Point(293, 93);

                    panel.Controls["cmbFactionupgrade"].Location = new System.Drawing.Point(293, 116);
                    break;
                #endregion
                #region case "4N"
                case "4N":
                    panel.Controls["lblNumModels"].Visible = true;
                    panel.Controls["lblNumModels"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["nudUnitSize"].Visible = true;
                    panel.Controls["nudUnitSize"].Location = new System.Drawing.Point(243, 27);

                    panel.Controls["lblnud1"].Visible = true;
                    panel.Controls["lblnud1"].Location = new System.Drawing.Point(126, 61);

                    panel.Controls["nudOption1"].Visible = true;
                    panel.Controls["nudOption1"].Location = new System.Drawing.Point(283, 59);

                    panel.Controls["lblnud2"].Visible = true;
                    panel.Controls["lblnud2"].Location = new System.Drawing.Point(126, 93);

                    panel.Controls["nudOption2"].Visible = true;
                    panel.Controls["nudOption2"].Location = new System.Drawing.Point(283, 91);

                    panel.Controls["lblnud3"].Visible = true;
                    panel.Controls["lblnud3"].Location = new System.Drawing.Point(126, 125);

                    panel.Controls["nudOption3"].Visible = true;
                    panel.Controls["nudOption3"].Location = new System.Drawing.Point(283, 123);
                    break;
                #endregion
                #region case "5N"
                case "5N":
                    panel.Controls["lblNumModels"].Visible = true;
                    panel.Controls["lblNumModels"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["nudUnitSize"].Visible = true;
                    panel.Controls["nudUnitSize"].Location = new System.Drawing.Point(243, 27);

                    panel.Controls["lblnud1"].Visible = true;
                    panel.Controls["lblnud1"].Location = new System.Drawing.Point(126, 61);

                    panel.Controls["nudOption1"].Visible = true;
                    panel.Controls["nudOption1"].Location = new System.Drawing.Point(283, 59);

                    panel.Controls["lblnud2"].Visible = true;
                    panel.Controls["lblnud2"].Location = new System.Drawing.Point(126, 93);

                    panel.Controls["nudOption2"].Visible = true;
                    panel.Controls["nudOption2"].Location = new System.Drawing.Point(283, 91);

                    panel.Controls["lblnud3"].Visible = true;
                    panel.Controls["lblnud3"].Location = new System.Drawing.Point(126, 125);

                    panel.Controls["nudOption3"].Visible = true;
                    panel.Controls["nudOption3"].Location = new System.Drawing.Point(283, 123);

                    panel.Controls["lblnud4"].Visible = true;
                    panel.Controls["lblnud4"].Location = new System.Drawing.Point(126, 157);

                    panel.Controls["nudOption4"].Visible = true;
                    panel.Controls["nudOption4"].Location = new System.Drawing.Point(283, 155);
                    break;
                #endregion
                #region case "N1kS(1m)"
                case "N1kS(1m)":
                    panel.Controls["lblNumModels"].Visible = true;
                    panel.Controls["lblNumModels"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["nudUnitSize"].Visible = true;
                    panel.Controls["nudUnitSize"].Location = new System.Drawing.Point(243, 27);

                    panel.Controls["cbOption1"].Visible = true;
                    panel.Controls["cbOption1"].Location = new System.Drawing.Point(90, 66);

                    panel.Controls["gbUnitLeader"].Visible = true;
                    panel.Controls["gbUnitLeader"].Location = new System.Drawing.Point(90, 96);
                    panel.Controls["gbUnitLeader"].Size = new System.Drawing.Size(426, 77);

                    groupbox = panel.Controls["gbUnitLeader"] as GroupBox;

                    groupbox.Controls["gb_lblOption1"].Visible = true;
                    groupbox.Controls["gb_lblOption1"].Location = new System.Drawing.Point(6, 34);

                    groupbox.Controls["gb_cmbOption1"].Visible = true;
                    groupbox.Controls["gb_cmbOption1"].Location = new System.Drawing.Point(231, 31);

                    panel.Controls["lblFactionupgrade"].Location = new System.Drawing.Point(86, 170);

                    panel.Controls["cmbFactionupgrade"].Location = new System.Drawing.Point(90, 193);
                    break;
                #endregion
                #region case "3N1kS(2m)"
                case "3N1kS(2m)":
                    panel.Controls["lblNumModels"].Visible = true;
                    panel.Controls["lblNumModels"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["nudUnitSize"].Visible = true;
                    panel.Controls["nudUnitSize"].Location = new System.Drawing.Point(243, 27);

                    panel.Controls["lblnud1"].Visible = true;
                    panel.Controls["lblnud1"].Location = new System.Drawing.Point(106, 61);

                    panel.Controls["nudOption1"].Visible = true;
                    panel.Controls["nudOption1"].Location = new System.Drawing.Point(283, 59);

                    panel.Controls["lblnud2"].Visible = true;
                    panel.Controls["lblnud2"].Location = new System.Drawing.Point(106, 93);

                    panel.Controls["nudOption2"].Visible = true;
                    panel.Controls["nudOption2"].Location = new System.Drawing.Point(283, 91);

                    panel.Controls["cbOption1"].Visible = true;
                    panel.Controls["cbOption1"].Location = new System.Drawing.Point(130, 116);

                    groupbox = panel.Controls["gbUnitLeader"] as GroupBox;

                    groupbox.Visible = true;
                    groupbox.Location = new System.Drawing.Point(90, 146);
                    groupbox.Size = new System.Drawing.Size(359, 144);

                    groupbox.Controls["gb_lblOption1"].Visible = true;
                    groupbox.Controls["gb_lblOption1"].Location = new System.Drawing.Point(6, 22);

                    groupbox.Controls["gb_cmbOption1"].Visible = true;
                    groupbox.Controls["gb_cmbOption1"].Location = new System.Drawing.Point(77, 45);

                    groupbox.Controls["gb_lblOption2"].Visible = true;
                    groupbox.Controls["gb_lblOption2"].Location = new System.Drawing.Point(6, 76);

                    groupbox.Controls["gb_cmbOption2"].Visible = true;
                    groupbox.Controls["gb_cmbOption2"].Location = new System.Drawing.Point(77, 99);

                    panel.Controls["lblFactionupgrade"].Location = new System.Drawing.Point(86, 293);

                    panel.Controls["cmbFactionupgrade"].Location = new System.Drawing.Point(90, 316);

                    break;
                #endregion
                #region case "NS(1m)"
                case "NS(1m)":
                    panel.Controls["lblNumModels"].Visible = true;
                    panel.Controls["lblNumModels"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["nudUnitSize"].Visible = true;
                    panel.Controls["nudUnitSize"].Location = new System.Drawing.Point(243, 27);

                    panel.Controls["gbUnitLeader"].Visible = true;
                    panel.Controls["gbUnitLeader"].Location = new System.Drawing.Point(90, 59);
                    panel.Controls["gbUnitLeader"].Size = new System.Drawing.Size(426, 77);

                    groupbox = panel.Controls["gbUnitLeader"] as GroupBox;

                    groupbox.Controls["gb_lblOption1"].Visible = true;
                    groupbox.Controls["gb_lblOption1"].Location = new System.Drawing.Point(6, 34);

                    groupbox.Controls["gb_cmbOption1"].Visible = true;
                    groupbox.Controls["gb_cmbOption1"].Location = new System.Drawing.Point(231, 31);

                    panel.Controls["lblFactionupgrade"].Location = new System.Drawing.Point(86, 170);

                    panel.Controls["cmbFactionupgrade"].Location = new System.Drawing.Point(90, 193);
                    break;
                #endregion
                #region case "NS(2m)"
                case "NS(2m)":
                    panel.Controls["lblNumModels"].Visible = true;
                    panel.Controls["lblNumModels"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["nudUnitSize"].Visible = true;
                    panel.Controls["nudUnitSize"].Location = new System.Drawing.Point(243, 27);

                    panel.Controls["gbUnitLeader"].Visible = true;
                    panel.Controls["gbUnitLeader"].Location = new System.Drawing.Point(90, 59);
                    panel.Controls["gbUnitLeader"].Size = new System.Drawing.Size(426, 112);

                    groupbox = panel.Controls["gbUnitLeader"] as GroupBox;

                    groupbox.Controls["gb_lblOption1"].Visible = true;
                    groupbox.Controls["gb_lblOption1"].Location = new System.Drawing.Point(6, 34);

                    groupbox.Controls["gb_cmbOption1"].Visible = true;
                    groupbox.Controls["gb_cmbOption1"].Location = new System.Drawing.Point(231, 31);

                    groupbox.Controls["gb_lblOption2"].Visible = true;
                    groupbox.Controls["gb_lblOption2"].Location = new System.Drawing.Point(6, 68);

                    groupbox.Controls["gb_cmbOption2"].Visible = true;
                    groupbox.Controls["gb_cmbOption2"].Location = new System.Drawing.Point(231, 65);

                    panel.Controls["lblFactionupgrade"].Location = new System.Drawing.Point(86, 170);

                    panel.Controls["cmbFactionupgrade"].Location = new System.Drawing.Point(90, 193);
                    break;
                #endregion
                #region case "2N1m"
                case "2N1m":
                    panel.Controls["lblNumModels"].Visible = true;
                    panel.Controls["lblNumModels"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["nudUnitSize"].Visible = true;
                    panel.Controls["nudUnitSize"].Location = new System.Drawing.Point(243, 27);

                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption1"].Location = new System.Drawing.Point(86, 63);

                    panel.Controls["cmbOption1"].Visible = true;
                    panel.Controls["cmbOption1"].Location = new System.Drawing.Point(311, 59);

                    panel.Controls["lblnud1"].Visible = true;
                    panel.Controls["lblnud1"].Location = new System.Drawing.Point(154, 95);

                    panel.Controls["nudOption1"].Visible = true;
                    panel.Controls["nudOption1"].Location = new System.Drawing.Point(311, 93);
                    break;
                #endregion
                #region case "2N1mS(2m)"
                case "2N1mS(2m)":
                    panel.Controls["lblNumModels"].Visible = true;
                    panel.Controls["lblNumModels"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["nudUnitSize"].Visible = true;
                    panel.Controls["nudUnitSize"].Location = new System.Drawing.Point(243, 27);

                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption1"].Location = new System.Drawing.Point(88, 63);

                    panel.Controls["cmbOption1"].Visible = true;
                    panel.Controls["cmbOption1"].Location = new System.Drawing.Point(313, 59);

                    panel.Controls["lblnud1"].Visible = true;
                    panel.Controls["lblnud1"].Location = new System.Drawing.Point(156, 95);

                    panel.Controls["nudOption1"].Visible = true;
                    panel.Controls["nudOption1"].Location = new System.Drawing.Point(313, 93);

                    panel.Controls["gbUnitLeader"].Visible = true;
                    panel.Controls["gbUnitLeader"].Location = new System.Drawing.Point(83, 125);
                    panel.Controls["gbUnitLeader"].Size = new System.Drawing.Size(426, 112);

                    groupbox = panel.Controls["gbUnitLeader"] as GroupBox;

                    groupbox.Controls["gb_lblOption1"].Visible = true;
                    groupbox.Controls["gb_lblOption1"].Location = new System.Drawing.Point(6, 34);

                    groupbox.Controls["gb_cmbOption1"].Visible = true;
                    groupbox.Controls["gb_cmbOption1"].Location = new System.Drawing.Point(231, 31);

                    groupbox.Controls["gb_lblOption2"].Visible = true;
                    groupbox.Controls["gb_lblOption2"].Location = new System.Drawing.Point(6, 68);

                    groupbox.Controls["gb_cmbOption2"].Visible = true;
                    groupbox.Controls["gb_cmbOption2"].Location = new System.Drawing.Point(231, 65);

                    panel.Controls["lblFactionupgrade"].Location = new System.Drawing.Point(86, 170);

                    panel.Controls["cmbFactionupgrade"].Location = new System.Drawing.Point(90, 193);
                    break;
                #endregion
                #region case "N1k"
                case "N1k":
                    panel.Controls["lblNumModels"].Visible = true;
                    panel.Controls["lblNumModels"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["nudUnitSize"].Visible = true;
                    panel.Controls["nudUnitSize"].Location = new System.Drawing.Point(243, 27);

                    panel.Controls["cbOption1"].Visible = true;
                    panel.Controls["cbOption1"].Location = new System.Drawing.Point(243, 59);

                    panel.Controls["lblFactionupgrade"].Location = new System.Drawing.Point(293, 93);

                    panel.Controls["cmbFactionupgrade"].Location = new System.Drawing.Point(293, 116);
                    break;
                #endregion
                #region case "N1m2k"
                case "N1m2k":
                    panel.Controls["lblNumModels"].Visible = true;
                    panel.Controls["lblNumModels"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["nudUnitSize"].Visible = true;
                    panel.Controls["nudUnitSize"].Location = new System.Drawing.Point(243, 27);

                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption1"].Location = new System.Drawing.Point(86, 63);

                    panel.Controls["cmbOption1"].Visible = true;
                    panel.Controls["cmbOption1"].Location = new System.Drawing.Point(311, 59);

                    panel.Controls["cbOption1"].Visible = true;
                    panel.Controls["cbOption1"].Location = new System.Drawing.Point(311, 93);

                    panel.Controls["cbOption2"].Visible = true;
                    panel.Controls["cbOption2"].Location = new System.Drawing.Point(311, 124);
                    break;
                #endregion
                #region case "N1m1k"
                case "N1m1k":
                    panel.Controls["lblNumModels"].Visible = true;
                    panel.Controls["lblNumModels"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["nudUnitSize"].Visible = true;
                    panel.Controls["nudUnitSize"].Location = new System.Drawing.Point(243, 27);

                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption1"].Location = new System.Drawing.Point(86, 63);

                    panel.Controls["cmbOption1"].Visible = true;
                    panel.Controls["cmbOption1"].Location = new System.Drawing.Point(311, 59);

                    panel.Controls["cbOption1"].Visible = true;
                    panel.Controls["cbOption1"].Location = new System.Drawing.Point(311, 93);

                    panel.Controls["lblFactionupgrade"].Location = new System.Drawing.Point(293, 123);

                    panel.Controls["cmbFactionupgrade"].Location = new System.Drawing.Point(293, 146);
                    break;
                #endregion
                #region case "N1mS(1m)"
                case "N1mS(1m)":
                    panel.Controls["lblNumModels"].Visible = true;
                    panel.Controls["lblNumModels"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["nudUnitSize"].Visible = true;
                    panel.Controls["nudUnitSize"].Location = new System.Drawing.Point(243, 27);

                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption1"].Location = new System.Drawing.Point(86, 63);

                    panel.Controls["cmbOption1"].Visible = true;
                    panel.Controls["cmbOption1"].Location = new System.Drawing.Point(311, 59);

                    panel.Controls["gbUnitLeader"].Visible = true;
                    panel.Controls["gbUnitLeader"].Location = new System.Drawing.Point(90, 96);
                    panel.Controls["gbUnitLeader"].Size = new System.Drawing.Size(426, 77);

                    groupbox = panel.Controls["gbUnitLeader"] as GroupBox;

                    groupbox.Controls["gb_lblOption1"].Visible = true;
                    groupbox.Controls["gb_lblOption1"].Location = new System.Drawing.Point(6, 34);

                    groupbox.Controls["gb_cmbOption1"].Visible = true;
                    groupbox.Controls["gb_cmbOption1"].Location = new System.Drawing.Point(231, 31);

                    panel.Controls["lblFactionupgrade"].Location = new System.Drawing.Point(86, 170);

                    panel.Controls["cmbFactionupgrade"].Location = new System.Drawing.Point(90, 193);
                    break;
                #endregion
                #region case "2N2m"
                case "2N2m":
                    panel.Controls["lblNumModels"].Visible = true;
                    panel.Controls["lblNumModels"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["nudUnitSize"].Visible = true;
                    panel.Controls["nudUnitSize"].Location = new System.Drawing.Point(243, 27);

                    panel.Controls["lblnud1"].Visible = true;
                    panel.Controls["lblnud1"].Location = new System.Drawing.Point(88, 63);

                    panel.Controls["nudOption1"].Visible = true;
                    panel.Controls["nudOption1"].Location = new System.Drawing.Point(313, 59);

                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption1"].Location = new System.Drawing.Point(86, 95);

                    panel.Controls["cmbOption1"].Visible = true;
                    panel.Controls["cmbOption1"].Location = new System.Drawing.Point(313, 93);

                    panel.Controls["lblOption2"].Visible = true;
                    panel.Controls["lblOption2"].Location = new System.Drawing.Point(86, 128);

                    panel.Controls["cmbOption2"].Visible = true;
                    panel.Controls["cmbOption2"].Location = new System.Drawing.Point(311, 125);
                    break;
                #endregion
                #region case "3N_p"
                case "3N_p":
                    panel.Controls["lblNumModels"].Visible = true;
                    panel.Controls["lblNumModels"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["nudUnitSize"].Visible = true;
                    panel.Controls["nudUnitSize"].Location = new System.Drawing.Point(243, 27);

                    panel.Controls["lblnud1"].Visible = true;
                    panel.Controls["lblnud1"].Location = new System.Drawing.Point(154, 55);

                    panel.Controls["nudOption1"].Visible = true;
                    panel.Controls["nudOption1"].Location = new System.Drawing.Point(311, 53);

                    panel.Controls["lblnud2"].Visible = true;
                    panel.Controls["lblnud2"].Location = new System.Drawing.Point(154, 85);

                    panel.Controls["nudOption2"].Visible = true;
                    panel.Controls["nudOption2"].Location = new System.Drawing.Point(311, 83);

                    panel.Controls["lblPsyker"].Visible = true;
                    panel.Controls["lblPsyker"].Location = new System.Drawing.Point(86, 112);

                    panel.Controls["clbPsyker"].Visible = true;
                    panel.Controls["clbPsyker"].Location = new System.Drawing.Point(85, 135);

                    panel.Controls["lblPsykerList"].Location = new System.Drawing.Point(83, 303);

                    panel.Controls["cmbDiscipline"].Location = new System.Drawing.Point(85, 316);

                    panel.Controls["cbWarlord"].Visible = true;
                    panel.Controls["cbWarlord"].Location = new System.Drawing.Point(298, 116);

                    panel.Controls["lblWarlord"].Visible = true;
                    panel.Controls["lblWarlord"].Location = new System.Drawing.Point(294, 143);

                    panel.Controls["cmbWarlord"].Visible = true;
                    panel.Controls["cmbWarlord"].Location = new System.Drawing.Point(294, 166);

                    panel.Controls["lblRelic"].Visible = true;
                    panel.Controls["lblRelic"].Location = new System.Drawing.Point(294, 197);

                    panel.Controls["cmbRelic"].Visible = true;
                    panel.Controls["cmbRelic"].Location = new System.Drawing.Point(294, 221);
                    break;
                #endregion
                #region case "N2m1k"
                case "N2m1k":
                    panel.Controls["lblNumModels"].Visible = true;
                    panel.Controls["lblNumModels"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["nudUnitSize"].Visible = true;
                    panel.Controls["nudUnitSize"].Location = new System.Drawing.Point(243, 27);

                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption1"].Location = new System.Drawing.Point(86, 63);

                    panel.Controls["cmbOption1"].Visible = true;
                    panel.Controls["cmbOption1"].Location = new System.Drawing.Point(311, 59);

                    panel.Controls["lblOption2"].Visible = true;
                    panel.Controls["lblOption2"].Location = new System.Drawing.Point(86, 93);

                    panel.Controls["cmbOption2"].Visible = true;
                    panel.Controls["cmbOption2"].Location = new System.Drawing.Point(311, 89);

                    panel.Controls["cbOption1"].Visible = true;
                    panel.Controls["cbOption1"].Location = new System.Drawing.Point(311, 123);

                    panel.Controls["lblFactionupgrade"].Location = new System.Drawing.Point(293, 153);

                    panel.Controls["cmbFactionupgrade"].Location = new System.Drawing.Point(293, 176);
                    break;
                #endregion
                #region case "N2k"
                case "N2k":
                    panel.Controls["lblNumModels"].Visible = true;
                    panel.Controls["lblNumModels"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["nudUnitSize"].Visible = true;
                    panel.Controls["nudUnitSize"].Location = new System.Drawing.Point(243, 27);

                    panel.Controls["cbOption1"].Visible = true;
                    panel.Controls["cbOption1"].Location = new System.Drawing.Point(243, 59);

                    panel.Controls["cbOption2"].Visible = true;
                    panel.Controls["cbOption2"].Location = new System.Drawing.Point(243, 89);

                    panel.Controls["lblFactionupgrade"].Location = new System.Drawing.Point(293, 93);

                    panel.Controls["cmbFactionupgrade"].Location = new System.Drawing.Point(293, 116);
                    break;
                #endregion
                #region case "N_p"
                case "N_p":
                    panel.Controls["lblNumModels"].Visible = true;
                    panel.Controls["lblNumModels"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["nudUnitSize"].Visible = true;
                    panel.Controls["nudUnitSize"].Location = new System.Drawing.Point(243, 27);

                    panel.Controls["lblPsyker"].Visible = true;
                    panel.Controls["lblPsyker"].Location = new System.Drawing.Point(86, 59);

                    panel.Controls["clbPsyker"].Visible = true;
                    panel.Controls["clbPsyker"].Location = new System.Drawing.Point(85, 82);

                    panel.Controls["lblPsykerList"].Location = new System.Drawing.Point(83, 260);

                    panel.Controls["cmbDiscipline"].Location = new System.Drawing.Point(85, 273);

                    panel.Controls["lblFactionupgrade"].Location = new System.Drawing.Point(293, 59);

                    panel.Controls["cmbFactionupgrade"].Location = new System.Drawing.Point(293, 82);
                    break;
                #endregion
                #region case "3N3k"
                case "3N3k":
                    panel.Controls["lblNumModels"].Visible = true;
                    panel.Controls["lblNumModels"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["nudUnitSize"].Visible = true;
                    panel.Controls["nudUnitSize"].Location = new System.Drawing.Point(243, 27);

                    panel.Controls["lblnud1"].Visible = true;
                    panel.Controls["lblnud1"].Location = new System.Drawing.Point(126, 61);

                    panel.Controls["nudOption1"].Visible = true;
                    panel.Controls["nudOption1"].Location = new System.Drawing.Point(283, 59);

                    panel.Controls["lblnud2"].Visible = true;
                    panel.Controls["lblnud2"].Location = new System.Drawing.Point(126, 93);

                    panel.Controls["nudOption2"].Visible = true;
                    panel.Controls["nudOption2"].Location = new System.Drawing.Point(283, 91);

                    panel.Controls["cbOption1"].Visible = true;
                    panel.Controls["cbOption1"].Location = new System.Drawing.Point(311, 121);

                    panel.Controls["cbOption2"].Visible = true;
                    panel.Controls["cbOption2"].Location = new System.Drawing.Point(311, 151);

                    panel.Controls["cbOption3"].Visible = true;
                    panel.Controls["cbOption3"].Location = new System.Drawing.Point(311, 181);

                    break;
                #endregion

                //Units using a Listbox
                #region case "NL2m1k"
                case "NL2m1k":
                    panel.Controls["lblNumModels"].Visible = true;
                    panel.Controls["lblNumModels"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["nudUnitSize"].Visible = true;
                    panel.Controls["nudUnitSize"].Location = new System.Drawing.Point(243, 27);

                    panel.Controls["lbModelSelect"].Visible = true;
                    panel.Controls["lbModelSelect"].Location = new System.Drawing.Point(39, 77);
                    panel.Controls["lbModelSelect"].Size = new System.Drawing.Size(194, 344);

                    panel.Controls["lblOption1"].Location = new System.Drawing.Point(239, 127);

                    panel.Controls["cmbOption1"].Location = new System.Drawing.Point(282, 150);

                    panel.Controls["lblOption2"].Location = new System.Drawing.Point(239, 184);

                    panel.Controls["cmbOption2"].Location = new System.Drawing.Point(282, 207);

                    panel.Controls["cbOption1"].Location = new System.Drawing.Point(243, 241);

                    panel.Controls["lblFactionUpgrade"].Location = new System.Drawing.Point(239, 268);

                    panel.Controls["cmbFactionUpgrade"].Location = new System.Drawing.Point(243, 291);
                    break;
                #endregion
                #region case "NL2m3k"
                case "NL2m3k":
                    panel.Controls["lblNumModels"].Visible = true;
                    panel.Controls["lblNumModels"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["nudUnitSize"].Visible = true;
                    panel.Controls["nudUnitSize"].Location = new System.Drawing.Point(243, 27);

                    panel.Controls["lbModelSelect"].Visible = true;
                    panel.Controls["lbModelSelect"].Location = new System.Drawing.Point(39, 97);
                    panel.Controls["lbModelSelect"].Size = new System.Drawing.Size(194, 344);

                    panel.Controls["lblOption1"].Location = new System.Drawing.Point(239, 97);

                    panel.Controls["cmbOption1"].Location = new System.Drawing.Point(282, 120);

                    panel.Controls["lblOption2"].Location = new System.Drawing.Point(239, 151);

                    panel.Controls["cmbOption2"].Location = new System.Drawing.Point(282, 174);

                    panel.Controls["cbOption1"].Location = new System.Drawing.Point(243, 153);

                    panel.Controls["cbOption2"].Location = new System.Drawing.Point(243, 183);

                    panel.Controls["cbOption3"].Location = new System.Drawing.Point(243, 208);

                    panel.Controls["lblFactionUpgrade"].Location = new System.Drawing.Point(239, 235);

                    panel.Controls["cmbFactionUpgrade"].Location = new System.Drawing.Point(243, 258);
                    break;
                #endregion
                #region case "NL3k"
                case "NL3k":
                    panel.Controls["lblNumModels"].Visible = true;
                    panel.Controls["lblNumModels"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["nudUnitSize"].Visible = true;
                    panel.Controls["nudUnitSize"].Location = new System.Drawing.Point(243, 27);

                    panel.Controls["lbModelSelect"].Visible = true;
                    panel.Controls["lbModelSelect"].Location = new System.Drawing.Point(39, 77);
                    panel.Controls["lbModelSelect"].Size = new System.Drawing.Size(194, 344);

                    panel.Controls["cbOption1"].Location = new System.Drawing.Point(243, 126);

                    panel.Controls["cbOption2"].Location = new System.Drawing.Point(243, 156);

                    panel.Controls["cbOption3"].Location = new System.Drawing.Point(243, 186);

                    panel.Controls["lblFactionUpgrade"].Location = new System.Drawing.Point(239, 213);

                    panel.Controls["cmbFactionUpgrade"].Location = new System.Drawing.Point(243, 236);
                    break;
                #endregion
                #region case "NL2m"
                case "NL2m":
                    panel.Controls["lblNumModels"].Visible = true;
                    panel.Controls["lblNumModels"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["nudUnitSize"].Visible = true;
                    panel.Controls["nudUnitSize"].Location = new System.Drawing.Point(243, 27);

                    panel.Controls["lbModelSelect"].Visible = true;
                    panel.Controls["lbModelSelect"].Location = new System.Drawing.Point(39, 77);
                    panel.Controls["lbModelSelect"].Size = new System.Drawing.Size(194, 344);

                    panel.Controls["lblOption1"].Location = new System.Drawing.Point(239, 127);

                    panel.Controls["cmbOption1"].Location = new System.Drawing.Point(282, 150);

                    panel.Controls["lblOption2"].Location = new System.Drawing.Point(239, 184);

                    panel.Controls["cmbOption2"].Location = new System.Drawing.Point(282, 207);

                    panel.Controls["lblFactionUpgrade"].Location = new System.Drawing.Point(239, 268);

                    panel.Controls["cmbFactionUpgrade"].Location = new System.Drawing.Point(243, 291);
                    break;
                #endregion
                #region case "NL1m1k"
                case "NL1m1k":
                    panel.Controls["lblNumModels"].Visible = true;
                    panel.Controls["lblNumModels"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["nudUnitSize"].Visible = true;
                    panel.Controls["nudUnitSize"].Location = new System.Drawing.Point(243, 27);

                    panel.Controls["lbModelSelect"].Visible = true;
                    panel.Controls["lbModelSelect"].Location = new System.Drawing.Point(39, 77);
                    panel.Controls["lbModelSelect"].Size = new System.Drawing.Size(194, 344);

                    panel.Controls["lblOption1"].Location = new System.Drawing.Point(239, 127);

                    panel.Controls["cmbOption1"].Location = new System.Drawing.Point(282, 150);

                    panel.Controls["cbOption1"].Location = new System.Drawing.Point(243, 184);

                    panel.Controls["lblFactionUpgrade"].Location = new System.Drawing.Point(239, 268);

                    panel.Controls["cmbFactionUpgrade"].Location = new System.Drawing.Point(243, 291);
                    break;
                #endregion
                #region case "NL3k1m"
                case "NL3k1m":
                    panel.Controls["lblNumModels"].Visible = true;
                    panel.Controls["lblNumModels"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["nudUnitSize"].Visible = true;
                    panel.Controls["nudUnitSize"].Location = new System.Drawing.Point(243, 27);

                    panel.Controls["lbModelSelect"].Visible = true;
                    panel.Controls["lbModelSelect"].Location = new System.Drawing.Point(39, 77);
                    panel.Controls["lbModelSelect"].Size = new System.Drawing.Size(194, 344);

                    panel.Controls["cbOption1"].Location = new System.Drawing.Point(243, 126);

                    panel.Controls["cbOption2"].Location = new System.Drawing.Point(243, 156);

                    panel.Controls["cbOption3"].Location = new System.Drawing.Point(243, 186);

                    panel.Controls["lblFactionUpgrade"].Location = new System.Drawing.Point(239, 213);

                    panel.Controls["cmbFactionUpgrade"].Location = new System.Drawing.Point(243, 236);

                    panel.Controls["lblFactionUpgrade"].Location = new System.Drawing.Point(239, 270);

                    panel.Controls["cmbFactionUpgrade"].Location = new System.Drawing.Point(243, 293);
                    break;
                #endregion
                #region case "NL2m2k"
                case "NL2m2k":
                    panel.Controls["lblNumModels"].Visible = true;
                    panel.Controls["lblNumModels"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["nudUnitSize"].Visible = true;
                    panel.Controls["nudUnitSize"].Location = new System.Drawing.Point(243, 27);

                    panel.Controls["lbModelSelect"].Visible = true;
                    panel.Controls["lbModelSelect"].Location = new System.Drawing.Point(39, 97);
                    panel.Controls["lbModelSelect"].Size = new System.Drawing.Size(194, 344);

                    panel.Controls["lblOption1"].Location = new System.Drawing.Point(239, 97);

                    panel.Controls["cmbOption1"].Location = new System.Drawing.Point(282, 120);

                    panel.Controls["lblOption2"].Location = new System.Drawing.Point(239, 151);

                    panel.Controls["cmbOption2"].Location = new System.Drawing.Point(282, 174);

                    panel.Controls["cbOption1"].Location = new System.Drawing.Point(243, 153);

                    panel.Controls["cbOption2"].Location = new System.Drawing.Point(243, 183);

                    panel.Controls["lblFactionUpgrade"].Location = new System.Drawing.Point(239, 235);

                    panel.Controls["cmbFactionUpgrade"].Location = new System.Drawing.Point(243, 258);
                    break;
                #endregion
                #region case "NL1m"
                case "NL1m":
                    panel.Controls["lblNumModels"].Visible = true;
                    panel.Controls["lblNumModels"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["nudUnitSize"].Visible = true;
                    panel.Controls["nudUnitSize"].Location = new System.Drawing.Point(243, 27);

                    panel.Controls["lbModelSelect"].Visible = true;
                    panel.Controls["lbModelSelect"].Location = new System.Drawing.Point(39, 77);
                    panel.Controls["lbModelSelect"].Size = new System.Drawing.Size(194, 344);

                    panel.Controls["lblOption1"].Location = new System.Drawing.Point(239, 127);

                    panel.Controls["cmbOption1"].Location = new System.Drawing.Point(282, 150);

                    panel.Controls["lblFactionUpgrade"].Location = new System.Drawing.Point(239, 268);

                    panel.Controls["cmbFactionUpgrade"].Location = new System.Drawing.Point(243, 291);
                    break;
                #endregion
                #region case "NL1m3k"
                case "NL1m3k":
                    panel.Controls["lblNumModels"].Visible = true;
                    panel.Controls["lblNumModels"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["nudUnitSize"].Visible = true;
                    panel.Controls["nudUnitSize"].Location = new System.Drawing.Point(243, 27);

                    panel.Controls["lbModelSelect"].Visible = true;
                    panel.Controls["lbModelSelect"].Location = new System.Drawing.Point(39, 97);
                    panel.Controls["lbModelSelect"].Size = new System.Drawing.Size(194, 344);

                    panel.Controls["lblOption1"].Location = new System.Drawing.Point(239, 97);

                    panel.Controls["cmbOption1"].Location = new System.Drawing.Point(282, 120);

                    panel.Controls["cbOption1"].Location = new System.Drawing.Point(243, 153);

                    panel.Controls["cbOption2"].Location = new System.Drawing.Point(243, 183);

                    panel.Controls["cbOption3"].Location = new System.Drawing.Point(243, 208);

                    panel.Controls["lblFactionUpgrade"].Location = new System.Drawing.Point(239, 235);

                    panel.Controls["cmbFactionUpgrade"].Location = new System.Drawing.Point(243, 258);
                    break;
                #endregion
                #region case "NL1m2k"
                case "NL1m2k":
                    panel.Controls["lblNumModels"].Visible = true;
                    panel.Controls["lblNumModels"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["nudUnitSize"].Visible = true;
                    panel.Controls["nudUnitSize"].Location = new System.Drawing.Point(243, 27);

                    panel.Controls["lbModelSelect"].Visible = true;
                    panel.Controls["lbModelSelect"].Location = new System.Drawing.Point(39, 97);
                    panel.Controls["lbModelSelect"].Size = new System.Drawing.Size(194, 344);

                    panel.Controls["lblOption1"].Location = new System.Drawing.Point(239, 97);

                    panel.Controls["cmbOption1"].Location = new System.Drawing.Point(282, 120);

                    panel.Controls["cbOption1"].Location = new System.Drawing.Point(243, 153);

                    panel.Controls["cbOption2"].Location = new System.Drawing.Point(243, 183);

                    panel.Controls["lblFactionUpgrade"].Location = new System.Drawing.Point(239, 208);

                    panel.Controls["cmbFactionUpgrade"].Location = new System.Drawing.Point(243, 231);
                    break;
                #endregion

                //Special Cases
                #region case "cultist"
                case "cultist":
                    panel.Controls["lblNumModels"].Visible = true;
                    panel.Controls["lblNumModels"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["nudUnitSize"].Visible = true;
                    panel.Controls["nudUnitSize"].Location = new System.Drawing.Point(243, 27);

                    panel.Controls["lblnud1"].Visible = true;
                    panel.Controls["lblnud1"].Location = new System.Drawing.Point(126, 61);

                    panel.Controls["nudOption1"].Visible = true;
                    panel.Controls["nudOption1"].Location = new System.Drawing.Point(283, 59);

                    panel.Controls["lblnud2"].Visible = true;
                    panel.Controls["lblnud2"].Location = new System.Drawing.Point(126, 93);

                    panel.Controls["nudOption2"].Visible = true;
                    panel.Controls["nudOption2"].Location = new System.Drawing.Point(283, 91);

                    panel.Controls["lblExtra1"].Visible = true;
                    panel.Controls["lblExtra1"].Location = new System.Drawing.Point(86, 130);

                    panel.Controls["lblnud3"].Visible = true;
                    panel.Controls["lblnud3"].Location = new System.Drawing.Point(126, 162);

                    panel.Controls["nudOption3"].Visible = true;
                    panel.Controls["nudOption3"].Location = new System.Drawing.Point(283, 162);

                    panel.Controls["lblnud4"].Visible = true;
                    panel.Controls["lblnud4"].Location = new System.Drawing.Point(126, 194);

                    panel.Controls["nudOption4"].Visible = true;
                    panel.Controls["nudOption4"].Location = new System.Drawing.Point(283, 194);

                    panel.Controls["gbUnitLeader"].Visible = true;
                    panel.Controls["gbUnitLeader"].Location = new System.Drawing.Point(90, 231);
                    panel.Controls["gbUnitLeader"].Size = new System.Drawing.Size(359, 153);

                    groupbox = panel.Controls["gbUnitLeader"] as GroupBox;

                    groupbox.Controls["gb_lblOption1"].Visible = true;
                    groupbox.Controls["gb_lblOption1"].Location = new System.Drawing.Point(6, 30);

                    groupbox.Controls["gb_cmbOption1"].Visible = true;
                    groupbox.Controls["gb_cmbOption1"].Location = new System.Drawing.Point(20, 53);

                    groupbox.Controls["gb_lblFactionupgrade"].Location = new System.Drawing.Point(16, 87);

                    groupbox.Controls["gb_cmbFactionupgrade"].Location = new System.Drawing.Point(20, 110);
                    break;
                #endregion
                #region case "corsairs"
                case "corsairs":
                    panel.Controls["lblNumModels"].Visible = true;
                    panel.Controls["lblNumModels"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["nudUnitSize"].Visible = true;
                    panel.Controls["nudUnitSize"].Location = new System.Drawing.Point(243, 27);

                    panel.Controls["lblOption1"].Visible = true;
                    panel.Controls["lblOption1"].Location = new System.Drawing.Point(88, 63);

                    panel.Controls["cmbOption1"].Visible = true;
                    panel.Controls["cmbOption1"].Location = new System.Drawing.Point(313, 59);

                    panel.Controls["lblExtra1"].Visible = true;
                    panel.Controls["lblExtra1"].Location = new System.Drawing.Point(89, 91);

                    panel.Controls["lblnud1"].Visible = true;
                    panel.Controls["lblnud1"].Location = new System.Drawing.Point(91, 119);

                    panel.Controls["nudOption1"].Visible = true;
                    panel.Controls["nudOption1"].Location = new System.Drawing.Point(307, 117);

                    panel.Controls["lblnud2"].Visible = true;
                    panel.Controls["lblnud2"].Location = new System.Drawing.Point(91, 151);

                    panel.Controls["nudOption2"].Visible = true;
                    panel.Controls["nudOption2"].Location = new System.Drawing.Point(307, 149);

                    panel.Controls["lblExtra2"].Visible = true;
                    panel.Controls["lblExtra2"].Location = new System.Drawing.Point(91, 181);

                    panel.Controls["lblOption2"].Visible = true;
                    panel.Controls["lblOption2"].Location = new System.Drawing.Point(91, 208);

                    panel.Controls["cmbOption2"].Visible = true;
                    panel.Controls["cmbOption2"].Location = new System.Drawing.Point(316, 204);

                    panel.Controls["gbUnitLeader"].Visible = true;
                    panel.Controls["gbUnitLeader"].Location = new System.Drawing.Point(90, 235);
                    panel.Controls["gbUnitLeader"].Size = new System.Drawing.Size(426, 105);

                    groupbox = panel.Controls["gbUnitLeader"] as GroupBox;
                    
                    groupbox.Controls["gb_lblOption1"].Visible = true;
                    groupbox.Controls["gb_lblOption1"].Location = new System.Drawing.Point(6, 34);

                    groupbox.Controls["gb_cmbOption1"].Visible = true;
                    groupbox.Controls["gb_cmbOption1"].Location = new System.Drawing.Point(231, 31);

                    groupbox.Controls["cbLeaderOption1"].Visible = true;
                    groupbox.Controls["cbLeaderOption1"].Location = new System.Drawing.Point(231, 65);

                    break;
                #endregion
                #region case "voidscarred"
                case "voidscarred":
                    panel.Controls["lblNumModels"].Visible = true;
                    panel.Controls["lblNumModels"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["nudUnitSize"].Visible = true;
                    panel.Controls["nudUnitSize"].Location = new System.Drawing.Point(243, 27);

                    panel.Controls["lbModelSelect"].Visible = true;
                    panel.Controls["lbModelSelect"].Location = new System.Drawing.Point(39, 97);
                    panel.Controls["lbModelSelect"].Size = new System.Drawing.Size(194, 344);

                    panel.Controls["lblOption1"].Location = new System.Drawing.Point(239, 97);

                    panel.Controls["cmbOption1"].Location = new System.Drawing.Point(282, 120);

                    panel.Controls["cbOption1"].Location = new System.Drawing.Point(243, 333);

                    panel.Controls["cbOption2"].Location = new System.Drawing.Point(243, 363);

                    panel.Controls["cbOption3"].Location = new System.Drawing.Point(243, 393);

                    panel.Controls["cbOption4"].Location = new System.Drawing.Point(248, 154);

                    panel.Controls["lblPsyker"].Location = new System.Drawing.Point(239, 97);

                    panel.Controls["clbPsyker"].Location = new System.Drawing.Point(243, 120);

                    panel.Controls["lblPsykerList"].Location = new System.Drawing.Point(244, 274);

                    panel.Controls["cmbDiscipline"].Location = new System.Drawing.Point(244, 298);
                    break;
                #endregion
                #region case "p"
                case "p":
                    panel.Controls["lblPsyker"].Visible = true;
                    panel.Controls["lblPsyker"].Location = new System.Drawing.Point(86, 29);

                    panel.Controls["clbPsyker"].Visible = true;
                    panel.Controls["clbPsyker"].Location = new System.Drawing.Point(85, 52);

                    panel.Controls["lblPsykerList"].Location = new System.Drawing.Point(83, 210);

                    panel.Controls["cmbDiscipline"].Location = new System.Drawing.Point(85, 233);

                    panel.Controls["lblFactionupgrade"].Location = new System.Drawing.Point(279, 29);

                    panel.Controls["cmbFactionupgrade"].Location = new System.Drawing.Point(283, 52);
                    break;
                    #endregion
                #region case "f" For faction upgrade only
                case "f":
                    panel.Controls["lblFactionupgrade"].Location = new System.Drawing.Point(293, 59);

                    panel.Controls["cmbFactionupgrade"].Location = new System.Drawing.Point(293, 85);
                    break;
                #endregion
            }
        }
    }
}
