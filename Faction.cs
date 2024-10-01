using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "$faction")]
    #region JSON Factions
    #endregion
    public abstract class Faction
    {
        public string subFactionName { get; set; }
        public string currentSubFaction { get; set; }
        public string factionUpgradeName { get; set; }
        public List<string> StratagemList { get; set; }
        public int[] StratagemCount { get; set; }
        public int[] StratagemLimit { get; set; }
        public string[] customSubFactionTraits { get; set; }
        public bool antiLoop { get; set; }
        public List<string> restrictedItems { get; set; }
        public Form1 baseForm { get; set; }
        public ListBox lbUnits { get; set; }

        public Faction() 
        { 
            StratagemList = new List<string>(); 
            restrictedItems = new List<string>();
        }

        public virtual void SetUpForm(Form form)
        {
            lbUnits = form.Controls["lbUnits"] as ListBox;
        }

        public abstract List<string> GetPsykerPowers(string keywords);
        public abstract List<string> GetFactionUpgrades(List<string> keywords);
        public abstract List<string> GetSubFactions();
        public abstract List<Datasheets> GetDatasheets();
        public abstract List<string> GetWarlordTraits(string keyword);
        public abstract int GetFactionUpgradePoints(string upgrade);
        public abstract List<string> GetRelics(List<string> keywords);
        public abstract List<string> GetCustomSubfactionList1();
        public abstract List<string> GetCustomSubfactionList2();
        public abstract void SetPoints(int points);
        public abstract bool GetIfEnabled(int index);
        public abstract void SetSubFactionPanel(Panel panel);
        public abstract void SaveSubFaction(int code, Panel panel); 

        public void DrawItemWithRestrictions(List<int> restrictedIndexes, ComboBox control)
        {
            control.DrawMode = DrawMode.OwnerDrawFixed;
            control.DrawItem += new DrawItemEventHandler(TestDraw);

            void TestDraw(object sender, DrawItemEventArgs e)
            {
                if (e.Index < 0)
                {
                    return;
                }

                // Draw the background of the ListBox control for each item.
                Brush brush = new SolidBrush(Color.LightSlateGray);
                Brush defbrush = new SolidBrush(Color.White);

                if (restrictedIndexes.Contains(e.Index))
                {
                    e.Graphics.FillRectangle(brush, e.Bounds);
                }
                else
                {
                    e.Graphics.FillRectangle(defbrush, e.Bounds);
                }

                brush.Dispose();
                defbrush.Dispose();
                // Define the default color of the brush as black.
                Brush myBrush = Brushes.Black;

                // Draw the current item text based on the current Font 
                // and the custom brush settings.
                e.Graphics.DrawString(control.Items[e.Index].ToString(),
                    e.Font, myBrush, e.Bounds, StringFormat.GenericDefault);
                // If the ListBox has focus, draw a focus rectangle around the selected item.
                e.DrawFocusRectangle();
            }
        }
    }
}
