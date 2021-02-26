using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EigeneKlassen
{
    [Serializable]
    public class MyXtraDocument
    {
        public enum Klasse { XtraForm, XtraUserControl }
        public Klasse ObjektTyp { get; set; }
        public string Name { get; set; }
        public string Assembly { get; set; }
        public string Projekt { get; set; }
        public string Filename { get; set; }

        private List<MyUiElement> MyUiElementsList_private;
        public List<MyUiElement> MyUiElementsList { get { if (MyUiElementsList_private == null) MyUiElementsList = new List<MyUiElement>(); return MyUiElementsList_private; } set { MyUiElementsList_private = value; } }
    }

    [Serializable]
    public class MyUiElement
    {
        public enum Klasse { GridColumn, GridView, GridControl, SimpleButton, TextEdit, CheckEdit, ComboBoxEdit, TabbedControlGroup, LayoutControlGroup, LayoutItem, DataLayoutControl, LayoutGroup, XtraTabPage, LayoutControl, ButtonEdit, ImageEdit, GroupControl, DateEdit }
        public Klasse ObjektTyp { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string Originaltext { get; set; }
        public string XtraDokument { get; set; }
        public string Assembly { get; set; }
        public string Parent { get; set; }
        public string Other { get; set; }
        public string TopLevelControl { get; set; }
        public string FullName { get; set; }
    }
}
