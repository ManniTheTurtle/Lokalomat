using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using EigeneKlassen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TargetTool
{
    public partial class XtraDoc1 : DevExpress.XtraEditors.XtraForm
    {

        public List<MyXtraDocument> ListOutOfDeserializedLanguageFiles = new List<MyXtraDocument>();

        public Dictionary<string, MyUiElement> DictOfUiElementsToCompare = new Dictionary<string, MyUiElement>();

        public Dictionary<string, Object> dictionary = new Dictionary<string, Object>();

        public Methoden neueMethode = new Methoden();

        public MyUiElement ControlValue = new MyUiElement();

        public XtraDoc1()
        {
            InitializeComponent();

            ListOutOfDeserializedLanguageFiles = neueMethode.DeserializeAllFilesFromActiveFolder(@"C:\k3tfs\Programm\bin\Debug\de\LokalisierungsTest");

            foreach (var item in ListOutOfDeserializedLanguageFiles)
            {
                if (item.Name.Contains(this.Name))
                {
                    DictOfUiElementsToCompare = item.MyUiElementsList.Distinct().ToDictionary(x => string.Format(x.Name), x => x);
                }
            }

            foreach (var item in DictOfUiElementsToCompare)
            {
                item.Value.Text = "!NewLanguage!";
                item.Value.ToolTip = "!NewLanguage!";
                item.Value.SuperTip = "!NewLanguage!";
                item.Value.Hint = "!NewLanguage!";
                item.Value.NullText = "!NewLanguage!";
                item.Value.Caption = "!NewLanguage!";
                item.Value.Other = "!NewLanguage!";
            }
        }

        private void XtraDoc1_Load(object sender, EventArgs e)
        {
            foreach (Control item in this.Controls)
            {
                if (item != null)
                {
                    SwitchLabeling(item);
                }
            }
        }

        // Texte Verändern Methode:
        private void SwitchLabeling(Control Item)
        {
            if (Item != null && !string.IsNullOrEmpty(Item.Name) && !dictionary.ContainsKey(Item.Name))
            {
                dictionary.Add(Item.Name, Item);

                if (Item.GetType() == typeof(LayoutControl))
                {
                    var thisDocsLayoutControl = Item as LayoutControl;

                    foreach (BaseLayoutItem bli in thisDocsLayoutControl.Items)
                    {
                        if (bli is LayoutControlItem)
                        {
                            var lci = bli as LayoutControlItem;

                            if (DictOfUiElementsToCompare.TryGetValue(Item.Name, out var ControlValue))
                            {
                                if (!string.IsNullOrEmpty(ControlValue.Text))
                                {
                                    lci.Text = ControlValue.Text;
                                }
                                if (!string.IsNullOrEmpty(ControlValue.ToolTip))
                                {
                                    lci.OptionsToolTip.ToolTip = ControlValue.ToolTip;
                                }

                            }
                        }
                        else if (bli is LayoutControlGroup)
                        {
                            var lcg = bli as LayoutControlGroup;

                            if (DictOfUiElementsToCompare.TryGetValue(Item.Name, out ControlValue))
                            {
                                if (!string.IsNullOrEmpty(ControlValue.Text))
                                {
                                    lcg.Text = ControlValue.Text;
                                }
                            }
                        }
                        else if (bli is TabbedControlGroup)
                        {
                            //.TabPages = LayoutControlGroups
                        }
                    }

                    foreach (Control con in thisDocsLayoutControl.Controls)
                    {
                        SwitchLabeling(con);
                    }
                }

                if (Item.Controls != null && Item.Controls.Count > 0)
                {
                    foreach (Control con in Item.Controls)
                    {
                        SwitchLabeling(con);
                    }
                }



                if (Item.GetType() == typeof(GridControl))
                {
                    var gc = Item as GridControl;

                    GridView gv = gc.DefaultView as GridView;

                    if (gv.Columns != null && gv.Columns.Count > 0)
                    {
                        foreach (GridColumn column in gv.Columns)
                        {
                            if (DictOfUiElementsToCompare.TryGetValue(column.Name, out ControlValue))
                            {
                                if (!string.IsNullOrEmpty(ControlValue.ToolTip))
                                {
                                    column.ToolTip = ControlValue.ToolTip;
                                }
                                if (!string.IsNullOrEmpty(ControlValue.Caption))
                                {
                                    column.Caption = ControlValue.Caption;
                                }
                            }
                        }
                    }
                }

                if (DictOfUiElementsToCompare.TryGetValue(Item.Name, out ControlValue))
                {
                    if (!string.IsNullOrEmpty(ControlValue.Text))
                    {
                        Item.Text = ControlValue.Text;
                    }
                    if (!string.IsNullOrEmpty(ControlValue.ToolTip))
                    {
                        // Item. = ControlValue.ToolTip;
                    }
                    if (!string.IsNullOrEmpty(ControlValue.SuperTip))
                    {
                        // Item.SuperTip = ControlValue.SuperTip;
                    }
                    if (!string.IsNullOrEmpty(ControlValue.Hint))
                    {
                        // Item.Hint = ControlValue.Hint;
                    }
                    if (!string.IsNullOrEmpty(ControlValue.NullText))
                    {
                        // Item.NullText = ControlValue.NullText;
                    }
                    if (!string.IsNullOrEmpty(ControlValue.Caption))
                    {
                        // Item.Caption = ControlValue.Caption;
                    }
                }
            }
        }


        private void XtraDoc1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
