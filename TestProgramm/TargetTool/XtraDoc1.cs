using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using EigeneKlassen;

namespace TargetTool
{
    public partial class XtraDoc1 : DevExpress.XtraEditors.XtraForm
    {
        public XtraDoc1()
        {
            InitializeComponent();

            Control[] AlleControls;

            List<LayoutControlItem> AlleLayoutControls = new List<LayoutControlItem>();

            foreach (var item in layoutControl1.Items)
            {
                AlleLayoutControls.Add(item as LayoutControlItem);
            }
       

            Methoden neueMethode = new Methoden();

            List<MyXtraDocument> VergleichsListe = neueMethode.DeserializeAllFilesFromActiveFolder(@"C:\k3tfs\Programm\bin\Debug\de\LokalisierungsTest");

            foreach (var item in VergleichsListe)
            {
                if (item.Name.Contains("XtraForm"))
                {
                    foreach (var uiElement in item.MyUiElementsList)
                    {
                        uiElement.Text = "ichbineineFremdeSprache";

                        AlleControls = this.Controls.Find(uiElement.Name, true);

                        if (AlleControls != null && AlleControls.Length > 0)
                        {
                            AlleControls[0].Text = uiElement.Text;
                        }

                        if (AlleLayoutControls != null && AlleLayoutControls.Count > 0)
                        {
                            foreach (var lci in AlleLayoutControls)
                            {
                                if (lci != null && lci.Name == uiElement.Name)
                                {
                                    lci.Text = uiElement.Text;
                                }
                            }
                        }
                    }
                }
            }
        }

    }
}
