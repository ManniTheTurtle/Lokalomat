using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraDataLayout;
using DevExpress.XtraTab;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using EigeneKlassen;
using DevExpress.XtraEditors.Controls;

namespace Lokalomat
{
    public partial class MainForm : DevExpress.XtraEditors.XtraForm
    {
        public MainForm()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
        }

        public XtraForm xtraForm = new XtraForm();

        public MyXtraDocument xtra_Document = new MyXtraDocument();

        public static Dictionary<Object, Object> dictionary = new Dictionary<Object, Object>();

        public string Verzeichnis = @"C:\Archiv\Gits\Tutorials\DevExpress_TestApp2\DevExpress_Controls_MusterApp\bin\Debug";

        public string filepath = @"C:\Users\Manni\Desktop\Objekte\";

        public string DocumentName = "leer";

        public string AssemblyFileName = "leer";

        public Methoden NutzeMethode = new Methoden();

        public Assembly ChosenAssembly;


        // Sonstige Listen:
        public List<MyUiElement> AllControlsAsUIelements = new List<MyUiElement>();
        public List<MyXtraDocument> XtraDocumentsList = new List<MyXtraDocument>();
        public List<MyXtraDocument> DeserializedXtraDocsList = new List<MyXtraDocument>();
        public Dictionary<string, MyXtraDocument> XtraDocumentsDict = new Dictionary<string, MyXtraDocument>();
        public Dictionary<string, MyXtraDocument> DeserializedXtraDocsDict = new Dictionary<string, MyXtraDocument>();

        // Listen als Zwischenspeicher
        List<MyXtraDocument> NewActiveDocumentsList = new List<MyXtraDocument>();
        List<MyUiElement> NewActiveUiElements = new List<MyUiElement>();
        List<MyUiElement> MySavedUiElements = new List<MyUiElement>();

        // Container Listen:
        public List<Assembly> assemblyList = new List<Assembly>();
        public List<Type> typesList = new List<Type>();
        public List<XtraForm> xtraFormslist = new List<XtraForm>();
        public List<XtraUserControl> xtraUserControlList = new List<XtraUserControl>();
        public List<Control> controlslist = new List<Control>();
        public List<LayoutControl> layoutControlslist = new List<LayoutControl>();

        // finale Listen:
        public List<Object> otherControlTypes = new List<object>();
        public List<LayoutControlItem> layoutControlItemslist = new List<LayoutControlItem>();
        public List<LayoutControlGroup> layoutControlGroupslist = new List<LayoutControlGroup>();
        public List<GridControl> gridControlslist = new List<GridControl>();
        public List<GridColumn> gridColumnslist = new List<GridColumn>();
        public List<TextEdit> textEditslist = new List<TextEdit>();
        public List<TabbedControlGroup> tabbedControlGroupslist = new List<TabbedControlGroup>();
        public List<SimpleButton> simpleButtonslist = new List<SimpleButton>();
        public List<CheckEdit> checkEditslist = new List<CheckEdit>();
        public List<ComboBoxEdit> comboBoxEditslist = new List<ComboBoxEdit>();
        public List<XtraTabPage> tabPageslist = new List<XtraTabPage>();
        public List<XtraTabControl> xtraTabControlslist = new List<XtraTabControl>();
        public List<LayoutGroup> layoutGroupslist = new List<LayoutGroup>();
        public List<DataLayoutControl> dataLayoutControlslist = new List<DataLayoutControl>();
        public List<ButtonEdit> buttonEditslist = new List<ButtonEdit>();
        public List<ImageEdit> imageEditslist = new List<ImageEdit>();
        public List<DateEdit> dateEditslist = new List<DateEdit>();
        public List<GroupControl> groupControlslist = new List<GroupControl>();


        // --> Automatisch Verzeichnis wählen
        private void autoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            assemblyList = NutzeMethode.LadeAssemblies(Verzeichnis);

            listBox1.Items.Add($"Assemblies Count: {assemblyList.Count}");

            BefuelleComboBox1();
        }

        // --> Assembly wurde gewählt
        private void SucheNachDokumenten()
        {
            typesList = NutzeMethode.LadeTypes(ChosenAssembly, typesList);

            listBox1.Items.Add($"Types Count: {typesList.Count}");

            (xtraFormslist, xtraUserControlList) = NutzeMethode.SortiereTypes(typesList, xtraFormslist, xtraUserControlList);

            listBox1.Items.Add($"XtraForms Count: {xtraFormslist.Count}");
            listBox1.Items.Add($"XtraUserControls Count: {xtraUserControlList.Count}");

            // -------------------------------->>> XtraForms
            if (xtraFormslist.Count > 0)
            {
                foreach (var item in xtraFormslist)
                {
                    MyXtraDocument xtra_Document = new MyXtraDocument();
                    xtra_Document.Name = item.Name;
                    xtra_Document.ObjektTyp = MyXtraDocument.Klasse.XtraForm;

                    if (!String.IsNullOrWhiteSpace(ChosenAssembly.FullName))
                    {
                        AssemblyFileName = ChosenAssembly.Location.Split('\\').Last();
                        AssemblyFileName = AssemblyFileName.Split('.')[0];

                        xtra_Document.Assembly = AssemblyFileName;
                    }

                    xtra_Document.Projekt = item.CompanyName;

                    controlslist.Clear();
                    controlslist = NutzeMethode.LadeControlsundLayoutControls(item);

                    foreach (var control in controlslist)
                    {
                        SucheUndUnterscheideKindElemente(control);
                    }

                    DocumentName = item.Name;

                    dictionary.Clear();
                    ListeAlleErgebnisse();

                    xtra_Document.MyUiElementsList.AddRange(AllControlsAsUIelements);

                    xtra_Document.Filename = filepath + "\\" + AssemblyFileName + "_" + xtra_Document.ObjektTyp + "_" + xtra_Document.Name + ".json";

                    XtraDocumentsList.Add(xtra_Document);

                    AllControlsAsUIelements.Clear();
                }
            }

            // -------------------------------->>> XtraUserControls
            if (xtraUserControlList.Count > 0)
            {
                foreach (var item in xtraUserControlList)
                {
                    MyXtraDocument xtra_Document = new MyXtraDocument();
                    xtra_Document.Name = item.Name;
                    xtra_Document.ObjektTyp = MyXtraDocument.Klasse.XtraUserControl;
                    
                    if (!String.IsNullOrWhiteSpace(ChosenAssembly.FullName))
                    {
                        AssemblyFileName = ChosenAssembly.Location.Split('\\').Last();
                        AssemblyFileName = AssemblyFileName.Split('.')[0];

                        xtra_Document.Assembly = AssemblyFileName;
                    }

                    xtra_Document.Projekt = item.CompanyName;

                    controlslist.Clear();
                    controlslist = NutzeMethode.LadeControlsundLayoutControls(item);

                    foreach (var control in controlslist)
                    {
                        SucheUndUnterscheideKindElemente(control);
                    }

                    DocumentName = item.Name;

                    dictionary.Clear();
                    ListeAlleErgebnisse();

                    xtra_Document.MyUiElementsList.AddRange(AllControlsAsUIelements);

                    xtra_Document.Filename = filepath + "\\" + AssemblyFileName + "_" + xtra_Document.ObjektTyp + "_" + xtra_Document.Name + ".json";

                    XtraDocumentsList.Add(xtra_Document);

                    AllControlsAsUIelements.Clear();
                }
            }

            listBox1.Items.Add($"XtraDocuments Count: {XtraDocumentsList.Count}");

            BefuelleComboBox2();

            typesList.Clear();
            xtraFormslist.Clear();
            xtraUserControlList.Clear();
            layoutControlslist.Clear();

        }

        // --> Switch Case Methode
        public void SucheUndUnterscheideKindElemente(Object item)
        {
            if (!dictionary.ContainsKey(item))
            {
                switch (item)
                {

                    case GroupControl lc when typeof(GroupControl).IsAssignableFrom(item.GetType()):
                        groupControlslist.Add(lc);
                        if (lc.HasChildren)
                        {
                            foreach (var lc_control in lc.Controls)
                            {
                                SucheUndUnterscheideKindElemente(lc_control);
                            }
                        }
                        break;

                    case DataLayoutControl lc when typeof(DataLayoutControl).IsAssignableFrom(item.GetType()):
                        dataLayoutControlslist.Add(lc);

                        if (lc.HasChildren)
                        {
                            foreach (var lc_item in lc.Items)
                            {
                                SucheUndUnterscheideKindElemente(lc_item);
                            }
                            foreach (var lc_control in lc.Controls)
                            {
                                SucheUndUnterscheideKindElemente(lc_control);
                            }
                        }
                        break;

                    case LayoutControl lc when typeof(LayoutControl).IsAssignableFrom(item.GetType()):
                        layoutControlslist.Add(lc);

                        if (lc.HasChildren)
                        {
                            foreach (var lc_item in lc.Items)
                            {
                                SucheUndUnterscheideKindElemente(lc_item);
                            }
                            foreach (var lc_control in lc.Controls)
                            {
                                SucheUndUnterscheideKindElemente(lc_control);
                            }
                        }
                        break;

                    case LayoutGroup lc when typeof(LayoutGroup).IsAssignableFrom(item.GetType()):
                        layoutGroupslist.Add(lc);
                        break;

                    case LayoutControlGroup lc when typeof(LayoutControlGroup).IsAssignableFrom(lc.GetType()):
                        layoutControlGroupslist.Add(lc);

                        if (lc.Items.Count > 0)
                        {
                            foreach (var i in lc.Items)
                            {
                                SucheUndUnterscheideKindElemente(i);
                            }
                        }
                        break;

                    case LayoutControlItem lc when typeof(LayoutControlItem).IsAssignableFrom(lc.GetType()):
                        layoutControlItemslist.Add(lc);
                        break;

                    case TabbedControlGroup lc when typeof(TabbedControlGroup).IsAssignableFrom(item.GetType()):
                        tabbedControlGroupslist.Add(lc);
                        
                        if (lc.TabPages.Count > 0)
                        {
                            foreach (var i in lc.TabPages)
                            {
                                SucheUndUnterscheideKindElemente(i);
                            }
                        }
                        break;           

                    case XtraTabControl lc when typeof(XtraTabControl).IsAssignableFrom(item.GetType()):
                        xtraTabControlslist.Add(lc);
                        if (lc.HasChildren)
                        {
                            foreach (var lc_item in lc.TabPages)
                            {
                                SucheUndUnterscheideKindElemente(lc_item);
                            }
                            foreach (var lc_control in lc.Controls)
                            {
                                SucheUndUnterscheideKindElemente(lc_control);
                            }
                        }
                        break;

                    case GridControl i when typeof(GridControl).IsAssignableFrom(item.GetType()):
                        gridControlslist.Add(i);

                        GridView y = i.MainView as GridView;
                        GridColumnCollection z = y.Columns;

                        gridColumnslist = z.Cast<GridColumn>().ToList();
                        break;

                    case XtraTabPage lc when typeof(XtraTabPage).IsAssignableFrom(item.GetType()):
                        tabPageslist.Add(lc);
                        break;

                    case CheckEdit i when typeof(CheckEdit).IsAssignableFrom(item.GetType()):
                        checkEditslist.Add(i);
                        break;

                    case SimpleButton i when typeof(SimpleButton).IsAssignableFrom(item.GetType()):
                        simpleButtonslist.Add(i);
                        break;

                    case DateEdit i when typeof(DateEdit).IsAssignableFrom(item.GetType()):
                        dateEditslist.Add(i);
                        break;

                    case ImageEdit i when typeof(ImageEdit).IsAssignableFrom(item.GetType()):
                        imageEditslist.Add(i);
                        break;

                    case ButtonEdit i when typeof(ButtonEdit).IsAssignableFrom(item.GetType()):
                        buttonEditslist.Add(i);
                        break;

                    case ComboBoxEdit i when typeof(ComboBoxEdit).IsAssignableFrom(item.GetType()):
                        comboBoxEditslist.Add(i);
                        break;

                    case TextEdit i when typeof(TextEdit).IsAssignableFrom(item.GetType()):
                        textEditslist.Add(i);
                        break;

                    default:
                        otherControlTypes.Add(item);
                        break;
                }
                dictionary.Add(item, item);
            }
        }

        // List<MyUiElement> füllen
        public void ListeAlleErgebnisse()
        {
            AllControlsAsUIelements.Clear();
            
            foreach (var item in gridControlslist)
            {
                AllControlsAsUIelements.Add(new MyUiElement { ObjektTyp = MyUiElement.Klasse.GridControl, Name = item.Name, Text = item.Text, XtraDokument = DocumentName, TopLevelControl = item.TopLevelControl == null ? "nicht verfügbar" : item.TopLevelControl.Name, Parent = item.Parent == null ? "nicht verfügbar" : item.Parent.ToString() });
            }
            foreach (var item in gridColumnslist)
            {
                AllControlsAsUIelements.Add(new MyUiElement { ObjektTyp = MyUiElement.Klasse.GridColumn, Name = item.Name, Text = item.Caption, XtraDokument = DocumentName, Other = item.FieldName, Parent = item.Container == null ? "nicht verfügbar" : item.Container.ToString() });
            }
            foreach (var item in textEditslist)
            {
                AllControlsAsUIelements.Add(new MyUiElement { ObjektTyp = MyUiElement.Klasse.TextEdit, Name = item.Name, Text = item.Text, XtraDokument = DocumentName, TopLevelControl = item.TopLevelControl == null ? "nicht verfügbar" : item.TopLevelControl.Name, Parent = item.Parent == null ? "nicht verfügbar" : item.Parent.ToString() });
            }
            foreach (var item in simpleButtonslist)
            {
                AllControlsAsUIelements.Add(new MyUiElement { ObjektTyp = MyUiElement.Klasse.SimpleButton, Name = item.Name, Text = item.Text, XtraDokument = DocumentName, TopLevelControl = item.TopLevelControl == null ? "nicht verfügbar" : item.TopLevelControl.Name, Parent = item.Parent == null ? "nicht verfügbar" : item.Parent.ToString() });
            }
            foreach (var item in checkEditslist)
            {
                AllControlsAsUIelements.Add(new MyUiElement { ObjektTyp = MyUiElement.Klasse.CheckEdit, Name = item.Name, Text = item.Text, XtraDokument = DocumentName, TopLevelControl = item.TopLevelControl == null ? "nicht verfügbar" : item.TopLevelControl.Name, Parent = item.Parent == null ? "nicht verfügbar" : item.Parent.ToString() });
            }
            foreach (var item in comboBoxEditslist)
            {
                AllControlsAsUIelements.Add(new MyUiElement { ObjektTyp = MyUiElement.Klasse.ComboBoxEdit, Name = item.Name, Text = item.Text, XtraDokument = DocumentName, TopLevelControl = item.TopLevelControl == null ? "nicht verfügbar" : item.TopLevelControl.Name, Parent = item.Parent == null ? "nicht verfügbar" : item.Parent.ToString() });
            }
            foreach (var item in layoutControlItemslist)
            {
                AllControlsAsUIelements.Add(new MyUiElement { ObjektTyp = MyUiElement.Klasse.LayoutItem, Name = item.Name, Text = item.Text, XtraDokument = DocumentName, Parent = item.Parent == null ? "nicht verfügbar" : item.Parent.ToString() });
            }
            foreach (var item in layoutControlGroupslist)
            {
                AllControlsAsUIelements.Add(new MyUiElement { ObjektTyp = MyUiElement.Klasse.LayoutControlGroup, Name = item.Name, Text = item.Text, XtraDokument = DocumentName, Parent = item.Parent == null ? "nicht verfügbar" : item.Parent.ToString() });
            }
            foreach (var item in tabbedControlGroupslist)
            {
                AllControlsAsUIelements.Add(new MyUiElement { ObjektTyp = MyUiElement.Klasse.TabbedControlGroup, Name = item.Name, Text = item.Text, XtraDokument = DocumentName, Parent = item.Parent == null ? "nicht verfügbar" : item.Parent.ToString() });
            }
            foreach (var item in dataLayoutControlslist)
            {
                AllControlsAsUIelements.Add(new MyUiElement { ObjektTyp = MyUiElement.Klasse.DataLayoutControl, Name = item.Name, XtraDokument = DocumentName, Text = item.Text, TopLevelControl = item.TopLevelControl == null ? "nicht verfügbar" : item.TopLevelControl.Name, Parent = item.Parent == null ? "nicht verfügbar" : item.Parent.ToString() });
            }
            foreach (var item in layoutGroupslist)
            {
                AllControlsAsUIelements.Add(new MyUiElement { ObjektTyp = MyUiElement.Klasse.LayoutGroup, Name = item.Name, Text = item.Text, XtraDokument = DocumentName, Parent = item.Parent == null ? "nicht verfügbar" : item.Parent.ToString() });
            }
            foreach (var item in layoutControlslist)
            {
                AllControlsAsUIelements.Add(new MyUiElement { ObjektTyp = MyUiElement.Klasse.LayoutControl, Name = item.Name, Text = item.Text, XtraDokument = DocumentName, TopLevelControl = item.TopLevelControl == null ? "nicht verfügbar" : item.TopLevelControl.Name, Parent = item.Parent == null ? "nicht verfügbar" : item.Parent.ToString() });
            }
            foreach (var item in tabPageslist)
            {
                AllControlsAsUIelements.Add(new MyUiElement { ObjektTyp = MyUiElement.Klasse.XtraTabPage, Name = item.Name, Text = item.Text, XtraDokument = DocumentName, TopLevelControl = item.TopLevelControl == null ? "nicht verfügbar" : item.TopLevelControl.Name, Parent = item.Parent == null ? "nicht verfügbar" : item.Parent.ToString() });
            }
            foreach (var item in buttonEditslist)
            {
                AllControlsAsUIelements.Add(new MyUiElement { ObjektTyp = MyUiElement.Klasse.ButtonEdit, Name = item.Name, Text = item.Text, XtraDokument = DocumentName, TopLevelControl = item.TopLevelControl == null ? "nicht verfügbar" : item.TopLevelControl.Name, Parent = item.Parent == null ? "nicht verfügbar" : item.Parent.ToString() });
            }
            foreach (var item in imageEditslist)
            {
                AllControlsAsUIelements.Add(new MyUiElement { ObjektTyp = MyUiElement.Klasse.ImageEdit, Name = item.Name, Text = item.Text, XtraDokument = DocumentName, TopLevelControl = item.TopLevelControl == null ? "nicht verfügbar" : item.TopLevelControl.Name, Parent = item.Parent == null ? "nicht verfügbar" : item.Parent.ToString() });
            }
            foreach (var item in groupControlslist)
            {
                AllControlsAsUIelements.Add(new MyUiElement { ObjektTyp = MyUiElement.Klasse.GroupControl, Name = item.Name, Text = item.Text, XtraDokument = DocumentName, TopLevelControl = item.TopLevelControl == null ? "nicht verfügbar" : item.TopLevelControl.Name, Parent = item.Parent == null ? "nicht verfügbar" : item.Parent.ToString() });
            }
            foreach (var item in dateEditslist)
            {
                AllControlsAsUIelements.Add(new MyUiElement { ObjektTyp = MyUiElement.Klasse.DateEdit, Name = item.Name, Text = item.Text, XtraDokument = DocumentName, TopLevelControl = item.TopLevelControl == null ? "nicht verfügbar" : item.TopLevelControl.Name, Parent = item.Parent == null ? "nicht verfügbar" : item.Parent.ToString() });
            }


            layoutControlItemslist.Clear();
            layoutControlGroupslist.Clear();
            gridControlslist.Clear();
            gridColumnslist.Clear();
            textEditslist.Clear();
            tabbedControlGroupslist.Clear();
            simpleButtonslist.Clear();
            checkEditslist.Clear();
            comboBoxEditslist.Clear();
            tabPageslist.Clear();
            layoutGroupslist.Clear();
            dataLayoutControlslist.Clear();
            buttonEditslist.Clear();
            imageEditslist.Clear();
            dateEditslist.Clear();
            groupControlslist.Clear();
        }

        // Serialisiere alles aus einer Assembly
        private void serializeToJsonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            if (folderDlg.ShowDialog() == DialogResult.OK)
            {
                filepath = folderDlg.SelectedPath;
            }

            AssemblyFileName = "leer";

            if (ChosenAssembly != null)
            {
                AssemblyFileName = ChosenAssembly.Location.Split('\\').Last();
                AssemblyFileName = AssemblyFileName.Split('.')[0];
            }

            foreach (var item in XtraDocumentsList)
            {
                if (File.Exists(item.Filename))
                {
                    listBox1.Items.Add("Datei updated: " + item.Name);
                }
                else
                {
                    listBox1.Items.Add("Neue Datei: " + item.Name);
                }

                string jsonstring = JsonConvert.SerializeObject(item, Formatting.Indented);
                File.WriteAllText(item.Filename, jsonstring);
            }

            listBox1.Items.Add("Assembly Datei serialized: " + AssemblyFileName);
        }

        // Serialisiere nur neue aktive Elemente aus einer Assembly 
        private void serialisiereNeueControlsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int count = 0;

            DeserializedXtraDocsList = NutzeMethode.DeserializeAllFilesFromActiveFolder(filepath);

            listBox1.Items.Add("XtraDokumente deserialisiert Count: " + DeserializedXtraDocsList.Count);

            
            XtraDocumentsDict = XtraDocumentsList.ToDictionary(x => x.Name, x => x);

            DeserializedXtraDocsDict = DeserializedXtraDocsList.ToDictionary(x => x.Name, x => x);

            Dictionary<string, MyUiElement> ActiveUIsDict = new Dictionary<string, MyUiElement>();
            Dictionary<string, MyUiElement> SavedUIsDict = new Dictionary<string, MyUiElement>();

            // Comparer 1
            foreach (var item in XtraDocumentsDict)
            {
                if (!DeserializedXtraDocsDict.ContainsKey(item.Key))
                {
                    DeserializedXtraDocsDict.Add(item.Key, item.Value);
                    listBox1.Items.Add("Neue XtraDokumente: " + item.Key);
                }

                ActiveUIsDict = item.Value.MyUiElementsList.ToDictionary(x => x.Name, x => x);

                foreach (var saveditem in DeserializedXtraDocsDict)
                {
                    if (saveditem.Value.Name == item.Value.Name)
                    {
                        SavedUIsDict = saveditem.Value.MyUiElementsList.ToDictionary(x => x.Name, x => x);

                        foreach (var ui in ActiveUIsDict)
                        {
                            if (!SavedUIsDict.ContainsKey(ui.Key))
                            {
                                SavedUIsDict.Add(ui.Key, ui.Value);
                                count++;
                            }
                        }
                        saveditem.Value.MyUiElementsList = SavedUIsDict.Values.ToList();
                        listBox1.Items.Add("Neue UiElemente Count: " + count);
                        count = 0;
                    }
                }
            }

            DeserializedXtraDocsList = DeserializedXtraDocsDict.Values.ToList();

            

            foreach (var item in DeserializedXtraDocsList)
            {
                if (!File.Exists(item.Filename))
                {
                    listBox1.Items.Add("Neue Datei erstellt: " + item.Name);
                }

                string jsonstring = JsonConvert.SerializeObject(item, Formatting.Indented);
                File.WriteAllText(item.Filename, jsonstring);
            }

            listBox1.Items.Add("Assembly Datei updated: " + AssemblyFileName);
        }

        // Deserialisiere alle Json Dateien
        private void deserializeFromJsonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeserializedXtraDocsList = NutzeMethode.DeserializeAllFilesFromActiveFolder(filepath);

            listBox1.Items.Add("XtraDokumente deserialisiert Count: " + DeserializedXtraDocsList.Count);
        }

        // Ersetze Texte
        private void compareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (XtraDocumentsList != null && DeserializedXtraDocsList != null)
            {
                listBox1.Items.Add("Aktive Dokumente Count: " + XtraDocumentsList.Count);
                listBox1.Items.Add("Dokumente aus Dateien Count: " + DeserializedXtraDocsList.Count);

                foreach (var item in DeserializedXtraDocsList)
                {
                    foreach (var uiItem in item.MyUiElementsList)
                    {
                        uiItem.Text = "neuerText!!!";
                    }
                }

                // Comparer 2
                foreach (var item in XtraDocumentsList)
                {
                    foreach (var item2 in DeserializedXtraDocsList)
                    {
                        if (item2.Name == item.Name)
                        {
                            foreach (var uiItem in item.MyUiElementsList)
                            {
                                foreach (var uiItem2 in item2.MyUiElementsList)
                                {
                                    if (uiItem2.Name == uiItem.Name)
                                    {
                                        uiItem.Text = uiItem2.Text;
                                    }
                                }
                            }
                        }
                    }
                }

                gridControl1.DataSource = XtraDocumentsList;
            }
        }

        // Alle Dateien Löschen
        private void dateienLöschenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DirectoryInfo di = new DirectoryInfo(@"C:\Users\Manni\Desktop\Objekte\");

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }
        }

        // Clear All
        private void speicherLeerenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AssemblyFileName = "leer";
            DocumentName = "leer";

            assemblyList.Clear();

            AllControlsAsUIelements.Clear();
            XtraDocumentsList.Clear();
            DeserializedXtraDocsList.Clear();

            XtraDocumentsDict.Clear();
            DeserializedXtraDocsDict.Clear();
        }

        // Verzeichnis auswählen
        private void wähleVerzeichnisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    string[] files = Directory.GetFiles(fbd.SelectedPath);

                    Verzeichnis = fbd.SelectedPath;
                }
            }
        }

        // Testing
        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ListOfEqualXtraDocuments = XtraDocumentsList.Where(x => DeserializedXtraDocsList.Any(y => y.Name == x.Name && y.Projekt == x.Projekt)).ToList();

            var CollectionOfEqualXtraDocuments2 = XtraDocumentsList.Select(x => x.Name).Intersect(DeserializedXtraDocsList.Select(x => x.Name));

            foreach (var item in XtraDocumentsList.Where(x => ListOfEqualXtraDocuments.Any(y => y.Name.Contains(x.Name))))
            {
            }

            NewActiveUiElements.AddRange(from x in XtraDocumentsList[0].MyUiElementsList select x);

            MySavedUiElements.AddRange(DeserializedXtraDocsList.SelectMany(x => x.MyUiElementsList)
                                                               .Where(y => y.Name.Contains("")));


            if (NewActiveUiElements.Any(x => MySavedUiElements.Any(y => y.Name == x.Name)))
            {
            }

            foreach (var newitem in XtraDocumentsList)
            {
                foreach (var olditem in DeserializedXtraDocsList)
                {
                    if (olditem.Name == newitem.Name)
                    {
                        foreach (var newUIitem in newitem.MyUiElementsList)
                        {
                            foreach (var oldUIitem in olditem.MyUiElementsList)
                            {
                                if (oldUIitem.Name == newUIitem.Name)
                                {
                                    if (oldUIitem.Originaltext != newUIitem.Text)
                                    {
                                        oldUIitem.Originaltext = newUIitem.Text;
                                    }
                                    MySavedUiElements.Add(oldUIitem);
                                }
                            }
                            NewActiveUiElements.Add(newUIitem);
                        }
                        int check = olditem.MyUiElementsList.Count();

                        olditem.MyUiElementsList.AddRange(newitem.MyUiElementsList.Where(x => olditem.MyUiElementsList.Any(y => !y.Name.Contains(x.Name))));

                        if (XtraDocumentsList.Any(x => DeserializedXtraDocsList.Any(y => y.Name != x.Name)))
                            listBox1.Items.Add("Neue UiElemente Count: " + (olditem.MyUiElementsList.Count() - check));
                    }
                }
            }

            DeserializedXtraDocsList.AddRange(XtraDocumentsList.Where(x => DeserializedXtraDocsList.Any(y => y.Name != x.Name)));
        }

        // Comboboxen
        private void BefuelleComboBox1()
        {
            ComboBoxItemCollection coll = comboBoxEdit1.Properties.Items;
            coll.BeginUpdate();
            coll.Clear();
            try
            {
                foreach (var item in assemblyList)
                {
                    coll.Add(item.Location);
                }
            }
            finally
            {
                coll.EndUpdate();
            }
            comboBoxEdit1.SelectedIndex = -1;
            comboBoxEdit1.BackColor = Color.DeepSkyBlue;
        }

        private void BefuelleComboBox2()
        {
            ComboBoxItemCollection coll = comboBoxEdit2.Properties.Items;
            coll.BeginUpdate();
            coll.Clear();
            try
            {
                foreach (var item in XtraDocumentsList)
                {
                    coll.Add(item.Name);
                }
            }
            finally
            {
                coll.EndUpdate();
            }
            comboBoxEdit2.SelectedIndex = -1;
            comboBoxEdit2.BackColor = Color.DeepSkyBlue;
        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            XtraDocumentsList.Clear();

            if (assemblyList.Count() > 0)
            {
                foreach (var item in assemblyList)
                {
                    if (comboBoxEdit1.SelectedItem.ToString() == item.Location)
                    {
                        ChosenAssembly = item;
                        gridControl1.DataSource = null;

                        SucheNachDokumenten();
                    }
                }
            }
        }

        private void comboBoxEdit2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (XtraDocumentsList != null)
            {
                foreach (var item in XtraDocumentsList)
                {
                    if (comboBoxEdit2.SelectedItem != null && comboBoxEdit2.SelectedItem.ToString() == item.Name)
                    {
                        gridControl1.DataSource = null;
                        gridControl1.DataSource = item.MyUiElementsList;

                        listBox1.Items.Add($"Dokument Controls Count: {item.MyUiElementsList.Count}");
                    }
                }
            }
        }


    }
}
