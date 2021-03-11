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

        private List<MyUiElement> myuielementslist;
        public List<MyUiElement> MyUiElementsList { get { if (myuielementslist == null) myuielementslist = new List<MyUiElement>(); return myuielementslist; } set { myuielementslist = value; } }
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
        public string Name { get; set; }
        public ClassName ItemClassName { get; set; }
        public Language ItemLanguage { get; set; }
        public bool TranslateYesOrNo { get; set; }
        public string XtraDokument { get; set; }
        public string Text { get; set; }
        public string ToolTip { get; set; }

        private List<string> supertiptitleslist;
        public List<string> SuperTipTitlesList { get { if (supertiptitleslist == null) supertiptitleslist = new List<string>(); return supertiptitleslist; } set { supertiptitleslist = value; } }

        private List<string> supertipcontentslist;
        public List<string> SuperTipContentsList { get { if (supertipcontentslist == null) supertipcontentslist = new List<string>(); return supertipcontentslist; } set { supertipcontentslist = value; } }

        public string Hint { get; set; }
        public string NullText { get; set; }
        public string NullValuePrompt { get; set; }
        public string AdvancedModeOptionsLabel { get; set; }
        public string Caption { get; set; }
        public string Other { get; set; }
    }

}
