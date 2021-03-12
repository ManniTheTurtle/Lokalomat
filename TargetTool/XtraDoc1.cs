using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using LokalomatKlassen;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Tile;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTab;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Layout;
using DevExpress.Utils;
using System.Reflection;
using DevExpress.XtraBars.FluentDesignSystem;

namespace TargetTool
{
    public partial class XtraDoc1 : DevExpress.XtraEditors.XtraForm
    {
        public Methoden neueMethode = new Methoden();

        public MyUiElement ControlValue = new MyUiElement();

        public LayoutControl thisDocsLayoutControl = new LayoutControl();

        public XtraDoc1()
        {
            InitializeComponent();
        }

        // Test K3 Zeugs
        private void k3versuche()
        { }

        // Test object PropertyInfo
        private void findeNameEigenschaft()
        {
            var sdfds = this as Control;
            foreach (object item in this.Controls)
            {
                Type z = item.GetType();
                var xy = z.GetRuntimeProperties();
                foreach (PropertyInfo prop in xy)
                {
                    if (prop.Name == "Name")
                    {
                        var a = prop.Attributes;
                        var proptype = prop.PropertyType;
                    }
                    if (prop.PropertyType.Name == "Name")
                    {
                        var a = prop.Attributes;
                    }
                }
            }
        }

        // Testbutton
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            // Texte verändern:
            neueMethode.FillDictionaryToCompare(this.Name);

            if (Lager.ObjectsDictionary == null)
            {
                Lager.ObjectsDictionary = new Dictionary<object, object>();
            }
            Lager.ObjectsDictionary.Clear();


            neueMethode.ChangeLanguage(this);
            foreach (Control item in this.Controls)
            {
                if (item != null)
                {
                    neueMethode.ChangeLanguage(item);
                }
            }
        }
    }
}
