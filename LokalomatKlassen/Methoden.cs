using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.FluentDesignSystem;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraDataLayout;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Card;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Layout;
using DevExpress.XtraGrid.Views.Tile;
using DevExpress.XtraLayout;
using DevExpress.XtraNavBar;
using DevExpress.XtraRichEdit;
using DevExpress.XtraTab;
using DevExpress.XtraTreeList;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace LokalomatKlassen
{
    public class Methoden
    {
        public MyUiElement ControlValue;

        // Assemblysuche
        public List<Assembly> LadeAssemblies(string Verzeichnis)
        {
            List<Assembly> AssemblyList = new List<Assembly>();

            if (Verzeichnis != null)
            {
                foreach (string Datei in Directory.GetFiles(Verzeichnis).Where(s => !s.Contains("DevExpress.") && s.EndsWith(".dll") || s.EndsWith(".exe")))
                {
                    try
                    {
                        AssemblyList.Add(Assembly.LoadFile(Datei));
                    }
                    catch
                    {
                    }
                }
            }

            return AssemblyList;
        }

        // Typesuche
        public List<Type> LadeTypes(Assembly assembly, List<Type> TypesList)
        {
            if (assembly != null)
            {
                try
                {
                    TypesList.AddRange((from t in assembly.GetTypes()
                    .Where(t => typeof(XtraForm).IsAssignableFrom(t) || typeof(Form).IsAssignableFrom(t)
                    || typeof(XtraUserControl).IsAssignableFrom(t) || typeof(UserControl).IsAssignableFrom(t))
                                        select t).ToList());
                }
                catch (Exception)
                {
                }
            }
            return TypesList;
        }

        // Types unterscheiden und Xtra Forms von abstract zu concrete konvertieren
        public (List<XtraForm>, List<XtraUserControl>) SortiereTypes(List<Type> TypesList, List<XtraForm> xtraFormList, List<XtraUserControl> xtraUserControlList)
        {
            foreach (var item in TypesList)      // --> Types sortieren
            {
                if (item != null)
                {
                    try
                    {
                        if (typeof(XtraForm).IsAssignableFrom(item))
                        {
                            // xtraFormList.Add((XtraForm)Activator.CreateInstance(item));

                            List<object> alldeclaredcontrolmemebers = new List<object>();

                            TypeInfo ti = item.GetTypeInfo();
                            IEnumerable<MemberInfo>  dm = ti.DeclaredMembers;
                            foreach (var member in dm)
                            {
                                if (member.MemberType == MemberTypes.Field)
                                {
                                    object control = member as object;
                                    alldeclaredcontrolmemebers.Add(control);
                                }
                            }
                            var x = alldeclaredcontrolmemebers;
                        }
                        else if (typeof(XtraUserControl).IsAssignableFrom(item))
                        {
                            xtraUserControlList.Add((XtraUserControl)Activator.CreateInstance(item));
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Fehler in Dokument Name: "+item.Name);
                    }
                }
            }
            return (xtraFormList, xtraUserControlList);
        }

        // Deserialisiere alles aus einem Verzeichnis
        public List<MyXtraDocument> DeserializeAllFilesFromFolder(string chosenfilepath)
        {
            List<MyXtraDocument> DeserializedXtraDocsList = new List<MyXtraDocument>();

            string[] filesArray = Directory.GetFiles(chosenfilepath);

            foreach (var file in filesArray)
            {
                string jsonstring = File.ReadAllText(file);

                if (file.EndsWith(".json"))
                {
                    var MyXtraDocNullTest = JsonConvert.DeserializeObject<MyXtraDocument>(jsonstring);

                    if (MyXtraDocNullTest != null)
                    {
                        DeserializedXtraDocsList.Add(MyXtraDocNullTest);    // momentan werden alle assemblys gemeinsam deserialized in eine Liste
                    }
                }
            }

            return DeserializedXtraDocsList;
        }

        // Finde passende Json Datei und fülle Dictionary<MyUiElement>
        public void FillDictionaryToCompare(string thisactivedocumentsname)
        {
            var filepath = Lager.FilePathForDeserialization;

            Lager.DictOfUiElementsToCompare = new Dictionary<string, MyUiElement>();

            Lager.ListOutOfDeserializedLanguageFiles = DeserializeAllFilesFromFolder(filepath);

            foreach (var item in Lager.ListOutOfDeserializedLanguageFiles)
            {
                if (item.Name.Contains(thisactivedocumentsname))
                {
                    Lager.DictOfUiElementsToCompare = item.MyUiElementsList.Distinct().ToDictionary(x => string.Format(x.Name), x => x);
                }
            }
        }

        // Ersetze Texte
        public void ChangeLanguage(object item)
        {
            if (item != null && Lager.DictOfUiElementsToCompare != null)
            {
                if (!Lager.ObjectsDictionary.ContainsKey(item))
                {
                    switch (item)
                    {
                        case FluentDesignForm i when typeof(FluentDesignForm).IsAssignableFrom(item.GetType()):

                            if (Lager.DictOfUiElementsToCompare.TryGetValue(i.Name, out ControlValue))
                            {
                                if (!string.IsNullOrEmpty(ControlValue.Text))
                                {
                                    i.Text = ControlValue.Text;
                                }
                            }
                            break;

                        case XtraForm i when typeof(XtraForm).IsAssignableFrom(item.GetType()):

                            if (Lager.DictOfUiElementsToCompare.TryGetValue(i.Name, out ControlValue))
                            {
                                if (!string.IsNullOrEmpty(ControlValue.Text))
                                {
                                    i.Text = ControlValue.Text;
                                }
                            }
                            break;

                        case DataLayoutControl lc when typeof(DataLayoutControl).IsAssignableFrom(item.GetType()):

                            if (Lager.DictOfUiElementsToCompare.TryGetValue(lc.Name, out ControlValue))
                            {
                                if (!string.IsNullOrEmpty(ControlValue.Text))
                                {
                                    lc.Text = ControlValue.Text;
                                }
                            }

                            if (lc.HasChildren)
                            {
                                foreach (var lc_item in lc.Items)
                                {
                                    ChangeLanguage(lc_item);
                                }
                                foreach (var lc_control in lc.Controls)
                                {
                                    ChangeLanguage(lc_control);
                                }
                            }
                            break;

                        case LayoutControl lc when typeof(LayoutControl).IsAssignableFrom(item.GetType()):

                            if (Lager.DictOfUiElementsToCompare.TryGetValue(lc.Name, out ControlValue))
                            {
                                if (!string.IsNullOrEmpty(ControlValue.Text))
                                {
                                    lc.Text = ControlValue.Text;
                                }
                            }

                            if (lc.HasChildren)
                            {
                                foreach (var lc_item in lc.Items)
                                {
                                    ChangeLanguage(lc_item);
                                }
                                foreach (var lc_control in lc.Controls)
                                {
                                    ChangeLanguage(lc_control);
                                }
                            }
                            break;

                        case LayoutGroup lc when typeof(LayoutGroup).IsAssignableFrom(item.GetType()):

                            if (Lager.DictOfUiElementsToCompare.TryGetValue(lc.Name, out ControlValue))
                            {
                                if (!string.IsNullOrEmpty(ControlValue.Text))
                                {
                                    lc.Text = ControlValue.Text;
                                }
                            }

                            if (lc.Items.Count > 0)
                            {
                                foreach (var lc_item in lc.Items)
                                {
                                    ChangeLanguage(lc_item);
                                }
                            }
                            break;

                        case TabbedGroup lc when typeof(TabbedGroup).IsAssignableFrom(item.GetType()):

                            if (Lager.DictOfUiElementsToCompare.TryGetValue(lc.Name, out ControlValue))
                            {
                                if (!string.IsNullOrEmpty(ControlValue.Text))
                                {
                                    lc.Text = ControlValue.Text;
                                }
                            }

                            if (lc.TabPages.Count > 0)
                            {
                                foreach (var i in lc.TabPages)
                                {
                                    ChangeLanguage(i);
                                }
                            }
                            break;

                        case TabbedControlGroup lc when typeof(TabbedControlGroup).IsAssignableFrom(item.GetType()):

                            if (Lager.DictOfUiElementsToCompare.TryGetValue(lc.Name, out ControlValue))
                            {
                                if (!string.IsNullOrEmpty(ControlValue.Text))
                                {
                                    lc.Text = ControlValue.Text;
                                }
                            }

                            if (lc.TabPages.Count > 0)  // diese .TabPages sind nicht XtraTabPage sondern LayoutControlGroup!
                            {
                                foreach (var i in lc.TabPages)
                                {
                                    ChangeLanguage(i);
                                }
                            }
                            break;

                        case LayoutControlGroup lc when typeof(LayoutControlGroup).IsAssignableFrom(lc.GetType()):

                            if (Lager.DictOfUiElementsToCompare.TryGetValue(lc.Name, out ControlValue))
                            {
                                if (!string.IsNullOrEmpty(ControlValue.Text))
                                {
                                    lc.Text = ControlValue.Text;
                                }
                            }

                            ChangeLanguage(lc.ParentTabbedGroup);

                            if (lc.Items.Count > 0)
                            {
                                foreach (var i in lc.Items)
                                {
                                    ChangeLanguage(i);
                                }
                            }
                            break;

                        case LayoutControlItem lc when typeof(LayoutControlItem).IsAssignableFrom(lc.GetType()):

                            if (Lager.DictOfUiElementsToCompare.TryGetValue(lc.Name, out ControlValue))
                            {
                                if (!string.IsNullOrEmpty(ControlValue.Text))
                                {
                                    lc.Text = ControlValue.Text;
                                }
                            }

                            ChangeLanguage(lc.Control);

                            break;

                        case GroupControl lc when typeof(GroupControl).IsAssignableFrom(item.GetType()):

                            if (Lager.DictOfUiElementsToCompare.TryGetValue(lc.Name, out ControlValue))
                            {
                                if (!string.IsNullOrEmpty(ControlValue.Text))
                                {
                                    lc.Text = ControlValue.Text;
                                }
                            }

                            if (lc.Controls.Count > 0)
                            {
                                foreach (var lc_control in lc.Controls)
                                {
                                    ChangeLanguage(lc_control);
                                }
                            }
                            break;

                        case GridControl i when typeof(GridControl).IsAssignableFrom(item.GetType()):

                            if (Lager.DictOfUiElementsToCompare.TryGetValue(i.Name, out ControlValue))
                            {
                                if (!string.IsNullOrEmpty(ControlValue.Text))
                                {
                                    i.Text = ControlValue.Text;
                                }
                            }

                            BaseView bv = i.MainView;           // BaseView kann auch eine ViewCaption haben

                            ChangeLanguage(bv);

                            GridLevelNodeCollection nodes = i.LevelTree.Nodes;

                            foreach (GridLevelNode node in nodes)
                            {
                                ChangeLanguage(node.LevelTemplate);       // BaseView kann auch eine ViewCaption haben
                            }
                            break;

                        case BandedGridView i when typeof(BandedGridView).IsAssignableFrom(item.GetType()):     //BaseView kann nicht nach .Columns durchsucht werden, darum Unterteilung nötig

                            foreach (var j in i.Columns)
                            {
                                ChangeLanguage(j);
                            }
                            break;

                        case GridView i when typeof(GridView).IsAssignableFrom(item.GetType()):

                            foreach (var j in i.Columns)
                            {
                                ChangeLanguage(j);
                            }
                            break;

                        case TileView i when typeof(TileView).IsAssignableFrom(item.GetType()):
                            foreach (var j in i.Columns)
                            {
                                ChangeLanguage(j);
                            }
                            break;

                        case LayoutView i when typeof(LayoutView).IsAssignableFrom(item.GetType()):
                            foreach (var j in i.Columns)
                            {
                                ChangeLanguage(j);
                            }
                            break;

                        case CardView i when typeof(CardView).IsAssignableFrom(item.GetType()):
                            foreach (var j in i.Columns)
                            {
                                ChangeLanguage(j);
                            }
                            break;

                        case GridLookUpEdit i when typeof(GridLookUpEdit).IsAssignableFrom(item.GetType()):     // hat nur ein Level und die GridColumns können ohne cast aus dem BaseView gelesen werden
                           
                            if (Lager.DictOfUiElementsToCompare.TryGetValue(i.Name, out ControlValue))
                            {
                                if (!string.IsNullOrEmpty(ControlValue.Text))
                                {
                                    i.Text = ControlValue.Text;
                                }
                                if (!string.IsNullOrEmpty(ControlValue.ToolTip))
                                {
                                    i.ToolTip = ControlValue.ToolTip;
                                }
                                if (!string.IsNullOrEmpty(ControlValue.NullText))
                                {
                                    i.Properties.NullText = ControlValue.NullText;
                                }
                                if (!string.IsNullOrEmpty(ControlValue.NullValuePrompt))
                                {
                                    i.Properties.NullValuePrompt = ControlValue.NullValuePrompt;
                                }
                                if (ControlValue.SuperTipTitlesList.Count() > 0 || ControlValue.SuperTipContentsList.Count() > 0)
                                {
                                    int titles = 0;
                                    int contents = 0;

                                    foreach (var supertip in i.SuperTip.Items)
                                    {
                                        if (supertip is ToolTipTitleItem)
                                        {
                                            var casteditem = supertip as ToolTipTitleItem;
                                            casteditem.Text = ControlValue.SuperTipTitlesList[titles];
                                            titles++;
                                        }
                                        else if (supertip is ToolTipItem)
                                        {
                                            var casteditem = supertip as ToolTipItem;
                                            casteditem.Text = ControlValue.SuperTipContentsList[contents];
                                            contents++;
                                        }
                                    }
                                }
                            }

                            if (i.Properties.View.Columns.Count > 0)
                            {
                                GridColumnCollection viewcolumns = i.Properties.View.Columns;
                                foreach (GridColumn j in viewcolumns)
                                {
                                    ChangeLanguage(j);
                                }
                            }
                            break;

                        case GridColumn i when typeof(GridColumn).IsAssignableFrom(item.GetType()):

                            if (Lager.DictOfUiElementsToCompare.TryGetValue(i.Name, out ControlValue))
                            {
                                if (!string.IsNullOrEmpty(ControlValue.ToolTip))
                                {
                                    i.ToolTip = ControlValue.ToolTip;
                                }
                                if (!string.IsNullOrEmpty(ControlValue.Caption))
                                {
                                    i.Caption = ControlValue.Caption;
                                }
                            }

                            if (i.ColumnEdit is RepositoryItemLookUpEdit)
                            {
                                ChangeLanguage(i);
                            }
                            break;

                        case XtraTabControl i when typeof(XtraTabControl).IsAssignableFrom(item.GetType()):

                            if (Lager.DictOfUiElementsToCompare.TryGetValue(i.Name, out ControlValue))
                            {
                                if (!string.IsNullOrEmpty(ControlValue.Text))
                                {
                                    i.Text = ControlValue.Text;
                                }
                            }

                            if (i.HasChildren)
                            {
                                foreach (var i_item in i.TabPages)
                                {
                                    ChangeLanguage(i_item);
                                }
                                foreach (var i_control in i.Controls)
                                {
                                    ChangeLanguage(i_control);
                                }
                            }
                            break;

                        case XtraTabPage i when typeof(XtraTabPage).IsAssignableFrom(item.GetType()):

                            if (Lager.DictOfUiElementsToCompare.TryGetValue(i.Name, out ControlValue))
                            {
                                if (!string.IsNullOrEmpty(ControlValue.Text))
                                {
                                    i.Text = ControlValue.Text;
                                }
                                if (ControlValue.SuperTipTitlesList.Count() > 0 || ControlValue.SuperTipContentsList.Count() > 0)
                                {
                                    int titles = 0;
                                    int contents = 0;

                                    foreach (var supertip in i.SuperTip.Items)
                                    {
                                        if (supertip is ToolTipTitleItem)
                                        {
                                            var casteditem = supertip as ToolTipTitleItem;
                                            casteditem.Text = ControlValue.SuperTipTitlesList[titles];
                                            titles++;
                                        }
                                        else if (supertip is ToolTipItem)
                                        {
                                            var casteditem = supertip as ToolTipItem;
                                            casteditem.Text = ControlValue.SuperTipContentsList[contents];
                                            contents++;
                                        }
                                    }
                                }
                            }
                            break;

                        case RepositoryItemLookUpEdit i when typeof(RepositoryItemLookUpEdit).IsAssignableFrom(item.GetType()):

                            LookUpColumnInfoCollection lookupcolumns = i.Columns;
                            foreach (LookUpColumnInfo j in lookupcolumns)
                            {
                                ChangeLanguage(j);
                            }
                            break;

                        case LookUpEdit i when typeof(LookUpEdit).IsAssignableFrom(item.GetType()):

                            if (Lager.DictOfUiElementsToCompare.TryGetValue(i.Name, out ControlValue))
                            {
                                if (!string.IsNullOrEmpty(ControlValue.Text))
                                {
                                    i.Text = ControlValue.Text;
                                }
                                if (!string.IsNullOrEmpty(ControlValue.ToolTip))
                                {
                                    i.ToolTip = ControlValue.ToolTip;
                                }
                                if (!string.IsNullOrEmpty(ControlValue.NullText))
                                {
                                    i.Properties.NullText = ControlValue.NullText;
                                }
                                if (!string.IsNullOrEmpty(ControlValue.NullValuePrompt))
                                {
                                    i.Properties.NullValuePrompt = ControlValue.NullValuePrompt;
                                }
                                if (ControlValue.SuperTipTitlesList.Count() > 0 || ControlValue.SuperTipContentsList.Count() > 0)
                                {
                                    int titles = 0;
                                    int contents = 0;

                                    foreach (var supertip in i.SuperTip.Items)
                                    {
                                        if (supertip is ToolTipTitleItem)
                                        {
                                            var casteditem = supertip as ToolTipTitleItem;
                                            casteditem.Text = ControlValue.SuperTipTitlesList[titles];
                                            titles++;
                                        }
                                        else if (supertip is ToolTipItem)
                                        {
                                            var casteditem = supertip as ToolTipItem;
                                            casteditem.Text = ControlValue.SuperTipContentsList[contents];
                                            contents++;
                                        }
                                    }
                                }
                            }

                            lookupcolumns = i.Properties.Columns;
                            foreach (LookUpColumnInfo j in lookupcolumns)
                            {
                                ChangeLanguage(j);
                            }
                            break;

                        case LookUpColumnInfo i when typeof(LookUpColumnInfo).IsAssignableFrom(item.GetType()):
                            
                            if (!string.IsNullOrEmpty(ControlValue.Caption))
                            {
                                i.Caption = ControlValue.Caption;
                            }
                            break;

                        case PopupMenu i when typeof(PopupMenu).IsAssignableFrom(item.GetType()):

                            foreach (BarItemLink j in i.ItemLinks)
                            {
                                ChangeLanguage(j);
                            }
                            break;

                        case BarItemLink i when typeof(BarItemLink).IsAssignableFrom(item.GetType()):

                            BarItem bi = i.Item;
                            ChangeLanguage(i);
                            break;

                        case BarItem i when typeof(BarItem).IsAssignableFrom(item.GetType()):

                            if (Lager.DictOfUiElementsToCompare.TryGetValue(i.Name, out ControlValue))
                            {
                                if (!string.IsNullOrEmpty(ControlValue.Text))
                                {
                                    i.Caption = ControlValue.Caption;
                                }
                                if (!string.IsNullOrEmpty(ControlValue.Hint))
                                {
                                    i.Hint = ControlValue.Hint;
                                }
                                if (ControlValue.SuperTipTitlesList.Count() > 0 || ControlValue.SuperTipContentsList.Count() > 0)
                                {
                                    int titles = 0;
                                    int contents = 0;

                                    foreach (var supertip in i.SuperTip.Items)
                                    {
                                        if (supertip is ToolTipTitleItem)
                                        {
                                            var casteditem = supertip as ToolTipTitleItem;
                                            casteditem.Text = ControlValue.SuperTipTitlesList[titles];
                                            titles++;
                                        }
                                        else if (supertip is ToolTipItem)
                                        {
                                            var casteditem = supertip as ToolTipItem;
                                            casteditem.Text = ControlValue.SuperTipContentsList[contents];
                                            contents++;
                                        }
                                    }
                                }
                            }
                            break;

                        case SearchControl i when typeof(SearchControl).IsAssignableFrom(item.GetType()):

                            if (Lager.DictOfUiElementsToCompare.TryGetValue(i.Name, out ControlValue))
                            {
                                if (!string.IsNullOrEmpty(ControlValue.Text))
                                {
                                    i.Text = ControlValue.Text;
                                }
                                if (!string.IsNullOrEmpty(ControlValue.ToolTip))
                                {
                                    i.ToolTip = ControlValue.ToolTip;
                                }
                                if (!string.IsNullOrEmpty(ControlValue.NullText))
                                {
                                    i.Properties.NullText = ControlValue.NullText;
                                }
                                if (!string.IsNullOrEmpty(ControlValue.NullValuePrompt))
                                {
                                    i.Properties.NullValuePrompt = ControlValue.NullValuePrompt;
                                }
                                if (ControlValue.SuperTipTitlesList.Count() > 0 || ControlValue.SuperTipContentsList.Count() > 0)
                                {
                                    int titles = 0;
                                    int contents = 0;

                                    foreach (var supertip in i.SuperTip.Items)
                                    {
                                        if (supertip is ToolTipTitleItem)
                                        {
                                            var casteditem = supertip as ToolTipTitleItem;
                                            casteditem.Text = ControlValue.SuperTipTitlesList[titles];
                                            titles++;
                                        }
                                        else if (supertip is ToolTipItem)
                                        {
                                            var casteditem = supertip as ToolTipItem;
                                            casteditem.Text = ControlValue.SuperTipContentsList[contents];
                                            contents++;
                                        }
                                    }
                                }
                            }
                            break;

                        case TreeList i when typeof(TreeList).IsAssignableFrom(item.GetType()):

                            if (Lager.DictOfUiElementsToCompare.TryGetValue(i.Name, out ControlValue))
                            {
                                if (!string.IsNullOrEmpty(ControlValue.Text))
                                {
                                    i.Text = ControlValue.Text;
                                }
                                if (!string.IsNullOrEmpty(ControlValue.Caption))
                                {
                                    i.Caption = ControlValue.Caption;
                                }
                            }
                            break;

                        case RadioGroup i when typeof(RadioGroup).IsAssignableFrom(item.GetType()):

                            if (Lager.DictOfUiElementsToCompare.TryGetValue(i.Name, out ControlValue))
                            {
                                if (!string.IsNullOrEmpty(ControlValue.Text))
                                {
                                    i.Text = ControlValue.Text;
                                }
                                if (!string.IsNullOrEmpty(ControlValue.ToolTip))
                                {
                                    i.ToolTip = ControlValue.ToolTip;
                                }
                                if (!string.IsNullOrEmpty(ControlValue.NullText))
                                {
                                    i.Properties.NullText = ControlValue.NullText;
                                }
                                if (ControlValue.SuperTipTitlesList.Count() > 0 || ControlValue.SuperTipContentsList.Count() > 0)
                                {
                                    int titles = 0;
                                    int contents = 0;

                                    foreach (var supertip in i.SuperTip.Items)
                                    {
                                        if (supertip is ToolTipTitleItem)
                                        {
                                            var casteditem = supertip as ToolTipTitleItem;
                                            casteditem.Text = ControlValue.SuperTipTitlesList[titles];
                                            titles++;
                                        }
                                        else if (supertip is ToolTipItem)
                                        {
                                            var casteditem = supertip as ToolTipItem;
                                            casteditem.Text = ControlValue.SuperTipContentsList[contents];
                                            contents++;
                                        }
                                    }
                                }
                            }
                            break;

                        case BarManager i when typeof(BarManager).IsAssignableFrom(item.GetType()):

                            break;

                        case BarButtonGroup i when typeof(BarButtonGroup).IsAssignableFrom(item.GetType()):
                            if (Lager.DictOfUiElementsToCompare.TryGetValue(i.Name, out ControlValue))
                            {
                                if (!string.IsNullOrEmpty(ControlValue.Text))
                                {
                                    i.Caption = ControlValue.Caption;
                                }
                                if (!string.IsNullOrEmpty(ControlValue.Hint))
                                {
                                    i.Hint = ControlValue.Hint;
                                }
                                if (ControlValue.SuperTipTitlesList.Count() > 0 || ControlValue.SuperTipContentsList.Count() > 0)
                                {
                                    int titles = 0;
                                    int contents = 0;

                                    foreach (var supertip in i.SuperTip.Items)
                                    {
                                        if (supertip is ToolTipTitleItem)
                                        {
                                            var casteditem = supertip as ToolTipTitleItem;
                                            casteditem.Text = ControlValue.SuperTipTitlesList[titles];
                                            titles++;
                                        }
                                        else if (supertip is ToolTipItem)
                                        {
                                            var casteditem = supertip as ToolTipItem;
                                            casteditem.Text = ControlValue.SuperTipContentsList[contents];
                                            contents++;
                                        }
                                    }
                                }
                            }
                            break;

                        case BarButtonItem i when typeof(BarButtonItem).IsAssignableFrom(item.GetType()):
                            if (Lager.DictOfUiElementsToCompare.TryGetValue(i.Name, out ControlValue))
                            {
                                if (!string.IsNullOrEmpty(ControlValue.Text))
                                {
                                    i.Caption = ControlValue.Caption;
                                }
                                if (!string.IsNullOrEmpty(ControlValue.Hint))
                                {
                                    i.Hint = ControlValue.Hint;
                                }
                                if (ControlValue.SuperTipTitlesList.Count() > 0 || ControlValue.SuperTipContentsList.Count() > 0)
                                {
                                    int titles = 0;
                                    int contents = 0;

                                    foreach (var supertip in i.SuperTip.Items)
                                    {
                                        if (supertip is ToolTipTitleItem)
                                        {
                                            var casteditem = supertip as ToolTipTitleItem;
                                            casteditem.Text = ControlValue.SuperTipTitlesList[titles];
                                            titles++;
                                        }
                                        else if (supertip is ToolTipItem)
                                        {
                                            var casteditem = supertip as ToolTipItem;
                                            casteditem.Text = ControlValue.SuperTipContentsList[contents];
                                            contents++;
                                        }
                                    }
                                }
                            }
                            break;

                        case CheckEdit i when typeof(CheckEdit).IsAssignableFrom(item.GetType()):
                            if (Lager.DictOfUiElementsToCompare.TryGetValue(i.Name, out ControlValue))
                            {
                                if (!string.IsNullOrEmpty(ControlValue.Text))
                                {
                                    i.Text = ControlValue.Text;
                                }
                                if (!string.IsNullOrEmpty(ControlValue.ToolTip))
                                {
                                    i.ToolTip = ControlValue.ToolTip;
                                }
                                if (!string.IsNullOrEmpty(ControlValue.NullText))
                                {
                                    i.Properties.NullText = ControlValue.NullText;
                                }
                                if (ControlValue.SuperTipTitlesList.Count() > 0 || ControlValue.SuperTipContentsList.Count() > 0)
                                {
                                    int titles = 0;
                                    int contents = 0;

                                    foreach (var supertip in i.SuperTip.Items)
                                    {
                                        if (supertip is ToolTipTitleItem)
                                        {
                                            var casteditem = supertip as ToolTipTitleItem;
                                            casteditem.Text = ControlValue.SuperTipTitlesList[titles];
                                            titles++;
                                        }
                                        else if (supertip is ToolTipItem)
                                        {
                                            var casteditem = supertip as ToolTipItem;
                                            casteditem.Text = ControlValue.SuperTipContentsList[contents];
                                            contents++;
                                        }
                                    }
                                }
                            }
                            break;

                        case DropDownButton i when typeof(DropDownButton).IsAssignableFrom(item.GetType()):
                            if (Lager.DictOfUiElementsToCompare.TryGetValue(i.Name, out ControlValue))
                            {
                                if (!string.IsNullOrEmpty(ControlValue.Text))
                                {
                                    i.Text = ControlValue.Text;
                                }
                                if (!string.IsNullOrEmpty(ControlValue.ToolTip))
                                {
                                    i.ToolTip = ControlValue.ToolTip;
                                }
                                if (ControlValue.SuperTipTitlesList.Count() > 0 || ControlValue.SuperTipContentsList.Count() > 0)
                                {
                                    int titles = 0;
                                    int contents = 0;

                                    foreach (var supertip in i.SuperTip.Items)
                                    {
                                        if (supertip is ToolTipTitleItem)
                                        {
                                            var casteditem = supertip as ToolTipTitleItem;
                                            casteditem.Text = ControlValue.SuperTipTitlesList[titles];
                                            titles++;
                                        }
                                        else if (supertip is ToolTipItem)
                                        {
                                            var casteditem = supertip as ToolTipItem;
                                            casteditem.Text = ControlValue.SuperTipContentsList[contents];
                                            contents++;
                                        }
                                    }
                                }
                            }
                            break;

                        case DateEdit i when typeof(DateEdit).IsAssignableFrom(item.GetType()):
                            if (Lager.DictOfUiElementsToCompare.TryGetValue(i.Name, out ControlValue))
                            {
                                if (!string.IsNullOrEmpty(ControlValue.Text))
                                {
                                    i.Text = ControlValue.Text;
                                }
                                if (!string.IsNullOrEmpty(ControlValue.ToolTip))
                                {
                                    i.ToolTip = ControlValue.ToolTip;
                                }
                                if (!string.IsNullOrEmpty(ControlValue.NullText))
                                {
                                    i.Properties.NullText = ControlValue.NullText;
                                }
                                if (!string.IsNullOrEmpty(ControlValue.NullValuePrompt))
                                {
                                    i.Properties.NullValuePrompt = ControlValue.NullValuePrompt;
                                }
                                if (ControlValue.SuperTipTitlesList.Count() > 0 || ControlValue.SuperTipContentsList.Count() > 0)
                                {
                                    int titles = 0;
                                    int contents = 0;

                                    foreach (var supertip in i.SuperTip.Items)
                                    {
                                        if (supertip is ToolTipTitleItem)
                                        {
                                            var casteditem = supertip as ToolTipTitleItem;
                                            casteditem.Text = ControlValue.SuperTipTitlesList[titles];
                                            titles++;
                                        }
                                        else if (supertip is ToolTipItem)
                                        {
                                            var casteditem = supertip as ToolTipItem;
                                            casteditem.Text = ControlValue.SuperTipContentsList[contents];
                                            contents++;
                                        }
                                    }
                                }
                            }
                            break;

                        case ImageEdit i when typeof(ImageEdit).IsAssignableFrom(item.GetType()):
                            if (Lager.DictOfUiElementsToCompare.TryGetValue(i.Name, out ControlValue))
                            {
                                if (!string.IsNullOrEmpty(ControlValue.Text))
                                {
                                    i.Text = ControlValue.Text;
                                }
                                if (!string.IsNullOrEmpty(ControlValue.ToolTip))
                                {
                                    i.ToolTip = ControlValue.ToolTip;
                                }
                                if (!string.IsNullOrEmpty(ControlValue.NullText))
                                {
                                    i.Properties.NullText = ControlValue.NullText;
                                }
                                if (!string.IsNullOrEmpty(ControlValue.NullValuePrompt))
                                {
                                    i.Properties.NullValuePrompt = ControlValue.NullValuePrompt;
                                }
                                if (ControlValue.SuperTipTitlesList.Count() > 0 || ControlValue.SuperTipContentsList.Count() > 0)
                                {
                                    int titles = 0;
                                    int contents = 0;

                                    foreach (var supertip in i.SuperTip.Items)
                                    {
                                        if (supertip is ToolTipTitleItem)
                                        {
                                            var casteditem = supertip as ToolTipTitleItem;
                                            casteditem.Text = ControlValue.SuperTipTitlesList[titles];
                                            titles++;
                                        }
                                        else if (supertip is ToolTipItem)
                                        {
                                            var casteditem = supertip as ToolTipItem;
                                            casteditem.Text = ControlValue.SuperTipContentsList[contents];
                                            contents++;
                                        }
                                    }
                                }
                            }
                            break;

                        case ComboBoxEdit i when typeof(ComboBoxEdit).IsAssignableFrom(item.GetType()):
                            if (Lager.DictOfUiElementsToCompare.TryGetValue(i.Name, out ControlValue))
                            {
                                if (!string.IsNullOrEmpty(ControlValue.Text))
                                {
                                    i.Text = ControlValue.Text;
                                }
                                if (!string.IsNullOrEmpty(ControlValue.ToolTip))
                                {
                                    i.ToolTip = ControlValue.ToolTip;
                                }
                                if (!string.IsNullOrEmpty(ControlValue.NullText))
                                {
                                    i.Properties.NullText = ControlValue.NullText;
                                }
                                if (!string.IsNullOrEmpty(ControlValue.NullValuePrompt))
                                {
                                    i.Properties.NullValuePrompt = ControlValue.NullValuePrompt;
                                }
                                if (ControlValue.SuperTipTitlesList.Count() > 0 || ControlValue.SuperTipContentsList.Count() > 0)
                                {
                                    int titles = 0;
                                    int contents = 0;

                                    foreach (var supertip in i.SuperTip.Items)
                                    {
                                        if (supertip is ToolTipTitleItem)
                                        {
                                            var casteditem = supertip as ToolTipTitleItem;
                                            casteditem.Text = ControlValue.SuperTipTitlesList[titles];
                                            titles++;
                                        }
                                        else if (supertip is ToolTipItem)
                                        {
                                            var casteditem = supertip as ToolTipItem;
                                            casteditem.Text = ControlValue.SuperTipContentsList[contents];
                                            contents++;
                                        }
                                    }
                                }
                            }
                            break;

                        case TextEdit i when typeof(TextEdit).IsAssignableFrom(item.GetType()):
                            if (Lager.DictOfUiElementsToCompare.TryGetValue(i.Name, out ControlValue))
                            {
                                if (!string.IsNullOrEmpty(ControlValue.Text))
                                {
                                    i.Text = ControlValue.Text;
                                }
                                if (!string.IsNullOrEmpty(ControlValue.ToolTip))
                                {
                                    i.ToolTip = ControlValue.ToolTip;
                                }
                                if (!string.IsNullOrEmpty(ControlValue.NullText))
                                {
                                    i.Properties.NullText = ControlValue.NullText;
                                }
                                if (!string.IsNullOrEmpty(ControlValue.NullValuePrompt))
                                {
                                    i.Properties.NullValuePrompt = ControlValue.NullValuePrompt;
                                }
                                if (!string.IsNullOrEmpty(ControlValue.AdvancedModeOptionsLabel))
                                {
                                    i.Properties.AdvancedModeOptions.Label = ControlValue.AdvancedModeOptionsLabel;
                                }
                                if (ControlValue.SuperTipTitlesList.Count() > 0 || ControlValue.SuperTipContentsList.Count() > 0)
                                {
                                    int titles = 0;
                                    int contents = 0;

                                    foreach (var supertip in i.SuperTip.Items)
                                    {
                                        if (supertip is ToolTipTitleItem)
                                        {
                                            var casteditem = supertip as ToolTipTitleItem;
                                            casteditem.Text = ControlValue.SuperTipTitlesList[titles];
                                            titles++;
                                        }
                                        else if (supertip is ToolTipItem)
                                        {
                                            var casteditem = supertip as ToolTipItem;
                                            casteditem.Text = ControlValue.SuperTipContentsList[contents];
                                            contents++;
                                        }
                                    }
                                }
                            }
                            break;

                        case CheckedListBoxControl i when typeof(CheckedListBoxControl).IsAssignableFrom(item.GetType()):

                            if (Lager.DictOfUiElementsToCompare.TryGetValue(i.Name, out ControlValue))
                            {
                                if (!string.IsNullOrEmpty(ControlValue.Text))
                                {
                                    i.Text = ControlValue.Text;
                                }
                                if (!string.IsNullOrEmpty(ControlValue.ToolTip))
                                {
                                    i.ToolTip = ControlValue.ToolTip;
                                }
                                if (ControlValue.SuperTipTitlesList.Count() > 0 || ControlValue.SuperTipContentsList.Count() > 0)
                                {
                                    int titles = 0;
                                    int contents = 0;

                                    foreach (var supertip in i.SuperTip.Items)
                                    {
                                        if (supertip is ToolTipTitleItem)
                                        {
                                            var casteditem = supertip as ToolTipTitleItem;
                                            casteditem.Text = ControlValue.SuperTipTitlesList[titles];
                                            titles++;
                                        }
                                        else if (supertip is ToolTipItem)
                                        {
                                            var casteditem = supertip as ToolTipItem;
                                            casteditem.Text = ControlValue.SuperTipContentsList[contents];
                                            contents++;
                                        }
                                    }
                                }
                            }
                            break;

                        case ListBoxControl i when typeof(ListBoxControl).IsAssignableFrom(item.GetType()):

                            if (Lager.DictOfUiElementsToCompare.TryGetValue(i.Name, out ControlValue))
                            {
                                if (!string.IsNullOrEmpty(ControlValue.Text))
                                {
                                    i.Text = ControlValue.Text;
                                }
                                if (!string.IsNullOrEmpty(ControlValue.ToolTip))
                                {
                                    i.ToolTip = ControlValue.ToolTip;
                                }
                                if (ControlValue.SuperTipTitlesList.Count() > 0 || ControlValue.SuperTipContentsList.Count() > 0)
                                {
                                    int titles = 0;
                                    int contents = 0;

                                    foreach (var supertip in i.SuperTip.Items)
                                    {
                                        if (supertip is ToolTipTitleItem)
                                        {
                                            var casteditem = supertip as ToolTipTitleItem;
                                            casteditem.Text = ControlValue.SuperTipTitlesList[titles];
                                            titles++;
                                        }
                                        else if (supertip is ToolTipItem)
                                        {
                                            var casteditem = supertip as ToolTipItem;
                                            casteditem.Text = ControlValue.SuperTipContentsList[contents];
                                            contents++;
                                        }
                                    }
                                }
                            }
                            break;

                        case ButtonEdit i when typeof(ButtonEdit).IsAssignableFrom(item.GetType()):

                            if (Lager.DictOfUiElementsToCompare.TryGetValue(i.Name, out ControlValue))
                            {
                                if (!string.IsNullOrEmpty(ControlValue.Text))
                                {
                                    i.Text = ControlValue.Text;
                                }
                                if (!string.IsNullOrEmpty(ControlValue.ToolTip))
                                {
                                    i.ToolTip = ControlValue.ToolTip;
                                }
                                if (!string.IsNullOrEmpty(ControlValue.NullText))
                                {
                                    i.Properties.NullText = ControlValue.NullText;
                                }
                                if (!string.IsNullOrEmpty(ControlValue.NullValuePrompt))
                                {
                                    i.Properties.NullValuePrompt = ControlValue.NullValuePrompt;
                                }
                                if (ControlValue.SuperTipTitlesList.Count() > 0 || ControlValue.SuperTipContentsList.Count() > 0)
                                {
                                    int titles = 0;
                                    int contents = 0;

                                    foreach (var supertip in i.SuperTip.Items)
                                    {
                                        if (supertip is ToolTipTitleItem)
                                        {
                                            var casteditem = supertip as ToolTipTitleItem;
                                            casteditem.Text = ControlValue.SuperTipTitlesList[titles];
                                            titles++;
                                        }
                                        else if (supertip is ToolTipItem)
                                        {
                                            var casteditem = supertip as ToolTipItem;
                                            casteditem.Text = ControlValue.SuperTipContentsList[contents];
                                            contents++;
                                        }
                                    }
                                }
                            }
                            break;

                        case SimpleButton i when typeof(SimpleButton).IsAssignableFrom(item.GetType()):

                            if (Lager.DictOfUiElementsToCompare.TryGetValue(i.Name, out ControlValue))
                            {
                                if (!string.IsNullOrEmpty(ControlValue.Text))
                                {
                                    i.Text = ControlValue.Text;
                                }
                                if (!string.IsNullOrEmpty(ControlValue.ToolTip))
                                {
                                    i.ToolTip = ControlValue.ToolTip;
                                }
                                if (ControlValue.SuperTipTitlesList.Count() > 0 || ControlValue.SuperTipContentsList.Count() > 0)
                                {
                                    int titles = 0;
                                    int contents = 0;

                                    foreach (var supertip in i.SuperTip.Items)
                                    {
                                        if (supertip is ToolTipTitleItem)
                                        {
                                            var casteditem = supertip as ToolTipTitleItem;
                                            casteditem.Text = ControlValue.SuperTipTitlesList[titles];
                                            titles++;
                                        }
                                        else if (supertip is ToolTipItem)
                                        {
                                            var casteditem = supertip as ToolTipItem;
                                            casteditem.Text = ControlValue.SuperTipContentsList[contents];
                                            contents++;
                                        }
                                    }
                                }
                            }
                            break;

                        case LabelControl i when typeof(LabelControl).IsAssignableFrom(item.GetType()):

                            if (Lager.DictOfUiElementsToCompare.TryGetValue(i.Name, out ControlValue))
                            {
                                if (!string.IsNullOrEmpty(ControlValue.Text))
                                {
                                    i.Text = ControlValue.Text;
                                }
                                if (!string.IsNullOrEmpty(ControlValue.ToolTip))
                                {
                                    i.ToolTip = ControlValue.ToolTip;
                                }
                                if (ControlValue.SuperTipTitlesList.Count() > 0 || ControlValue.SuperTipContentsList.Count() > 0)
                                {
                                    int titles = 0;
                                    int contents = 0;

                                    foreach (var supertip in i.SuperTip.Items)
                                    {
                                        if (supertip is ToolTipTitleItem)
                                        {
                                            var casteditem = supertip as ToolTipTitleItem;
                                            casteditem.Text = ControlValue.SuperTipTitlesList[titles];
                                            titles++;
                                        }
                                        else if (supertip is ToolTipItem)
                                        {
                                            var casteditem = supertip as ToolTipItem;
                                            casteditem.Text = ControlValue.SuperTipContentsList[contents];
                                            contents++;
                                        }
                                    }
                                }
                            }
                            break;

                        case RichEditControl i when typeof(RichEditControl).IsAssignableFrom(item.GetType()):

                            if (Lager.DictOfUiElementsToCompare.TryGetValue(i.Name, out ControlValue))
                            {
                                if (!string.IsNullOrEmpty(ControlValue.Text))
                                {
                                    i.Text = ControlValue.Text;
                                }
                            }
                            break;

                        case CheckedComboBoxEdit i when typeof(CheckedComboBoxEdit).IsAssignableFrom(item.GetType()):

                            if (Lager.DictOfUiElementsToCompare.TryGetValue(i.Name, out ControlValue))
                            {
                                if (!string.IsNullOrEmpty(ControlValue.Text))
                                {
                                    i.Text = ControlValue.Text;
                                }
                                if (!string.IsNullOrEmpty(ControlValue.ToolTip))
                                {
                                    i.ToolTip = ControlValue.ToolTip;
                                }
                                if (!string.IsNullOrEmpty(ControlValue.NullText))
                                {
                                    i.Properties.NullText = ControlValue.NullText;
                                }
                                if (!string.IsNullOrEmpty(ControlValue.NullValuePrompt))
                                {
                                    i.Properties.NullValuePrompt = ControlValue.NullValuePrompt;
                                }
                                if (ControlValue.SuperTipTitlesList.Count() > 0 || ControlValue.SuperTipContentsList.Count() > 0)
                                {
                                    int titles = 0;
                                    int contents = 0;

                                    foreach (var supertip in i.SuperTip.Items)
                                    {
                                        if (supertip is ToolTipTitleItem)
                                        {
                                            var casteditem = supertip as ToolTipTitleItem;
                                            casteditem.Text = ControlValue.SuperTipTitlesList[titles];
                                            titles++;
                                        }
                                        else if (supertip is ToolTipItem)
                                        {
                                            var casteditem = supertip as ToolTipItem;
                                            casteditem.Text = ControlValue.SuperTipContentsList[contents];
                                            contents++;
                                        }
                                    }
                                }
                            }
                            break;

                        case NavBarControl i when typeof(NavBarControl).IsAssignableFrom(item.GetType()):

                            if (Lager.DictOfUiElementsToCompare.TryGetValue(i.Name, out ControlValue))
                            {
                                if (!string.IsNullOrEmpty(ControlValue.Text))
                                {
                                    i.Text = ControlValue.Text;
                                }
                            }

                            if (i.HasChildren)
                            {
                                foreach (var j in i.Groups)
                                {
                                    ChangeLanguage(j);
                                }
                                foreach (var j in i.Controls)
                                {
                                    ChangeLanguage(j);
                                }
                                foreach (var j in i.Items)
                                {
                                    ChangeLanguage(j);
                                }
                            }
                            break;

                        case NavBarGroup i when typeof(NavBarGroup).IsAssignableFrom(item.GetType()):

                            if (Lager.DictOfUiElementsToCompare.TryGetValue(i.Name, out ControlValue))
                            {
                                if (!string.IsNullOrEmpty(ControlValue.Text))
                                {
                                    i.Caption = ControlValue.Caption;
                                }
                                if (!string.IsNullOrEmpty(ControlValue.Hint))
                                {
                                    i.Hint = ControlValue.Hint;
                                }
                                if (ControlValue.SuperTipTitlesList.Count() > 0 || ControlValue.SuperTipContentsList.Count() > 0)
                                {
                                    int titles = 0;
                                    int contents = 0;

                                    foreach (var supertip in i.SuperTip.Items)
                                    {
                                        if (supertip is ToolTipTitleItem)
                                        {
                                            var casteditem = supertip as ToolTipTitleItem;
                                            casteditem.Text = ControlValue.SuperTipTitlesList[titles];
                                            titles++;
                                        }
                                        else if (supertip is ToolTipItem)
                                        {
                                            var casteditem = supertip as ToolTipItem;
                                            casteditem.Text = ControlValue.SuperTipContentsList[contents];
                                            contents++;
                                        }
                                    }
                                }
                            }

                            foreach (NavBarItemLink j in i.ItemLinks)   // NavBarItem == NavBarItemLink(Item)
                            {
                                ChangeLanguage(j.Item);
                            }
                            break;

                        case NavBarItem i when typeof(NavBarItem).IsAssignableFrom(item.GetType()):
                            if (Lager.DictOfUiElementsToCompare.TryGetValue(i.Name, out ControlValue))
                            {
                                if (!string.IsNullOrEmpty(ControlValue.Text))
                                {
                                    i.Caption = ControlValue.Caption;
                                }
                                if (!string.IsNullOrEmpty(ControlValue.Hint))
                                {
                                    i.Hint = ControlValue.Hint;
                                }
                                if (ControlValue.SuperTipTitlesList.Count() > 0 || ControlValue.SuperTipContentsList.Count() > 0)
                                {
                                    int titles = 0;
                                    int contents = 0;

                                    foreach (var supertip in i.SuperTip.Items)
                                    {
                                        if (supertip is ToolTipTitleItem)
                                        {
                                            var casteditem = supertip as ToolTipTitleItem;
                                            casteditem.Text = ControlValue.SuperTipTitlesList[titles];
                                            titles++;
                                        }
                                        else if (supertip is ToolTipItem)
                                        {
                                            var casteditem = supertip as ToolTipItem;
                                            casteditem.Text = ControlValue.SuperTipContentsList[contents];
                                            contents++;
                                        }
                                    }
                                }
                            }
                            break;

                        case AccordionControl i when typeof(AccordionControl).IsAssignableFrom(item.GetType()):

                            if (Lager.DictOfUiElementsToCompare.TryGetValue(i.Name, out ControlValue))
                            {
                                if (!string.IsNullOrEmpty(ControlValue.Text))
                                {
                                    i.Text = ControlValue.Text;
                                }
                                if (!string.IsNullOrEmpty(ControlValue.ToolTip))
                                {
                                    i.ToolTip = ControlValue.ToolTip;
                                }
                                if (ControlValue.SuperTipTitlesList.Count() > 0 || ControlValue.SuperTipContentsList.Count() > 0)
                                {
                                    int titles = 0;
                                    int contents = 0;

                                    foreach (var supertip in i.SuperTip.Items)
                                    {
                                        if (supertip is ToolTipTitleItem)
                                        {
                                            var casteditem = supertip as ToolTipTitleItem;
                                            casteditem.Text = ControlValue.SuperTipTitlesList[titles];
                                            titles++;
                                        }
                                        else if (supertip is ToolTipItem)
                                        {
                                            var casteditem = supertip as ToolTipItem;
                                            casteditem.Text = ControlValue.SuperTipContentsList[contents];
                                            contents++;
                                        }
                                    }
                                }
                            }

                            if (i.HasChildren)
                            {
                                foreach (var j in i.Controls)
                                {
                                    ChangeLanguage(j);
                                }
                                foreach (var j in i.Elements)
                                {
                                    ChangeLanguage(j);
                                }
                            }
                            break;

                        case AccordionControlElement i when typeof(AccordionControlElement).IsAssignableFrom(item.GetType()):

                            if (Lager.DictOfUiElementsToCompare.TryGetValue(i.Name, out ControlValue))
                            {
                                if (!string.IsNullOrEmpty(ControlValue.Text))
                                {
                                    i.Text = ControlValue.Text;
                                }
                                if (!string.IsNullOrEmpty(ControlValue.Hint))
                                {
                                    i.Hint = ControlValue.Hint;
                                }
                                if (ControlValue.SuperTipTitlesList.Count() > 0 || ControlValue.SuperTipContentsList.Count() > 0)
                                {
                                    int titles = 0;
                                    int contents = 0;

                                    foreach (var supertip in i.SuperTip.Items)
                                    {
                                        if (supertip is ToolTipTitleItem)
                                        {
                                            var casteditem = supertip as ToolTipTitleItem;
                                            casteditem.Text = ControlValue.SuperTipTitlesList[titles];
                                            titles++;
                                        }
                                        else if (supertip is ToolTipItem)
                                        {
                                            var casteditem = supertip as ToolTipItem;
                                            casteditem.Text = ControlValue.SuperTipContentsList[contents];
                                            contents++;
                                        }
                                    }
                                }
                            }

                            if (i.Elements.Count > 0)
                            {
                                foreach (var j in i.Elements)
                                {
                                    ChangeLanguage(j);
                                }
                            }
                            break;

                        case TileControl i when typeof(TileControl).IsAssignableFrom(item.GetType()):

                            if (Lager.DictOfUiElementsToCompare.TryGetValue(i.Name, out ControlValue))
                            {
                                if (!string.IsNullOrEmpty(ControlValue.Text))
                                {
                                    i.Text = ControlValue.Text;
                                }
                            }

                            if (i.Groups.Count > 0)
                            {
                                foreach (var j in i.Groups)
                                {
                                    ChangeLanguage(j);
                                }
                            }
                            if (i.Controls.Count > 0)
                            {
                                foreach (var j in i.Controls)
                                {
                                    ChangeLanguage(j);
                                }
                            }
                            break;

                        case TileGroup i when typeof(TileGroup).IsAssignableFrom(item.GetType()):

                            if (Lager.DictOfUiElementsToCompare.TryGetValue(i.Name, out ControlValue))
                            {
                                if (!string.IsNullOrEmpty(ControlValue.Text))
                                {
                                    i.Text = ControlValue.Text;
                                }
                            }

                            if (i.Items.Count > 0)
                            {
                                foreach (var j in i.Items)
                                {
                                    ChangeLanguage(j);
                                }
                            }
                            break;

                        case TileBarGroup i when typeof(TileBarGroup).IsAssignableFrom(item.GetType()):

                            if (Lager.DictOfUiElementsToCompare.TryGetValue(i.Name, out ControlValue))
                            {
                                if (!string.IsNullOrEmpty(ControlValue.Text))
                                {
                                    i.Text = ControlValue.Text;
                                }
                            }

                            ChangeLanguage(i.Control);

                            if (i.Items.Count > 0)
                            {
                                foreach (var j in i.Items)
                                {
                                    ChangeLanguage(j);
                                }
                            }
                            break;

                        case TileBar i when typeof(TileBar).IsAssignableFrom(item.GetType()):

                            if (Lager.DictOfUiElementsToCompare.TryGetValue(i.Name, out ControlValue))
                            {
                                if (!string.IsNullOrEmpty(ControlValue.Text))
                                {
                                    i.Text = ControlValue.Text;
                                }
                            }

                            if (i.HasChildren)
                            {
                                foreach (var j in i.Controls)
                                {
                                    ChangeLanguage(j);
                                }
                                foreach (var j in i.Groups)
                                {
                                    ChangeLanguage(j);
                                }
                            }
                            break;

                        case TileItem i when typeof(TileItem).IsAssignableFrom(item.GetType()):

                            if (Lager.DictOfUiElementsToCompare.TryGetValue(i.Name, out ControlValue))
                            {
                                if (!string.IsNullOrEmpty(ControlValue.Text))
                                {
                                    i.Text = ControlValue.Text;
                                }
                                if (ControlValue.SuperTipTitlesList.Count() > 0 || ControlValue.SuperTipContentsList.Count() > 0)
                                {
                                    int titles = 0;
                                    int contents = 0;

                                    foreach (var supertip in i.SuperTip.Items)
                                    {
                                        if (supertip is ToolTipTitleItem)
                                        {
                                            var casteditem = supertip as ToolTipTitleItem;
                                            casteditem.Text = ControlValue.SuperTipTitlesList[titles];
                                            titles++;
                                        }
                                        else if (supertip is ToolTipItem)
                                        {
                                            var casteditem = supertip as ToolTipItem;
                                            casteditem.Text = ControlValue.SuperTipContentsList[contents];
                                            contents++;
                                        }
                                    }
                                }
                            }

                            if (i.Elements.Count > 0)
                            {
                                foreach (var j in i.Elements)
                                {
                                    ChangeLanguage(j);
                                }
                            }
                            break;

                        case TileItemElement i when typeof(TileItemElement).IsAssignableFrom(item.GetType()):
                            break;

                        default:
                            Lager.UnknownObjects.Add(item);
                            break;
                    }
                    Lager.ObjectsDictionary.Add(item, item);
                }
            }
        }

    }
}
