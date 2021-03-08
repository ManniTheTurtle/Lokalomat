using System;
using System.Collections.Generic;

namespace LokalomatKlassen
{
    [Serializable]
    public class MyXtraDocument
    {
        public enum Klasse { XtraForm, XtraUserControl }
        public Klasse ObjektTyp { get; set; }
        public string Name { get; set; }
        public string Assembly { get; set; }
        public string Filename { get; set; }
        public string Projekt { get; set; }

        private List<MyUiElement> MyUiElementsList_private;
        public List<MyUiElement> MyUiElementsList { get { if (MyUiElementsList_private == null) MyUiElementsList = new List<MyUiElement>(); return MyUiElementsList_private; } set { MyUiElementsList_private = value; } }
    }

    [Serializable]
    public class MyUiElement
    {
        public enum ClassName
        {
            Other, XtraForm, FluentDesignForm, GridColumn, GridView, GridControl, SimpleButton, TextEdit, CheckEdit, ComboBoxEdit, TabbedControlGroup,
            LayoutControlGroup, LayoutControlItem, DataLayoutControl, LayoutGroup, XtraTabPage, LayoutControl, ButtonEdit, ImageEdit, GroupControl,
            DateEdit, DropDownButton, GridLookUpEdit, GridColumnView, ImageComboboxEdit, ListBoxControl, PictureEdit, CheckedListBoxControl,
            TileControl, TileGroup, TileBar, TileItem, TileItemElement, TileBarGroup, AccordionControl, AccordionControlElement,
            NavBarControl, NavBarItem, NavBarGroup, RichEditControl, CheckedComboBoxEdit, LabelControl, LookUpColumnInfo, LookUpEdit,
            BarButtonItem, BarButtonGroup, RadioGroup, BarItemLink, BarItem, earchControl, TreeList
        }

        public enum Language { German, English, Italian, French }

        public ClassName ItemClassName { get; set; }
        public Language ItemLanguage { get; set; }
        public string Name { get; set; }
        public bool TranslateYesOrNo { get; set; }      // z.B. für BarButtonItems, von denen die Meisten bereits von DevExpress selbst lokalisiert werden
        public string XtraDokument { get; set; }
        public string Text { get; set; }
        public string ToolTip { get; set; }

        private List<string> supertiptitleslist_private;
        public List<string> SuperTipTitlesList { get { if (supertiptitleslist_private == null) SuperTipTitlesList = new List<string>(); return supertiptitleslist_private; } set { supertiptitleslist_private = value; } }

        private List<string> supertipcontentslist_private;
        public List<string> SuperTipContentsList { get { if (supertipcontentslist_private == null) SuperTipContentsList = new List<string>(); return supertipcontentslist_private; } set { supertipcontentslist_private = value; } }

        public string Hint { get; set; }
        public string NullText { get; set; }
        public string Caption { get; set; }
        public string Other { get; set; }

        public string OriginalText { get; set; }    // was wenn sich die Texte ändern? bisher wird das nicht bemerkt
    }

}
