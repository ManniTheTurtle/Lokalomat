﻿using DevExpress.XtraBars;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraDataLayout;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using DevExpress.XtraNavBar;
using DevExpress.XtraRichEdit;
using DevExpress.XtraTab;
using DevExpress.XtraTreeList;
using EigeneKlassen;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using TargetTool;

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

        public string AssemblyFilePath;
        // @"C:\k3tfs\Programm\bin\Debug\de\LokalisierungsTest"
        public string SerializationFilePath = @"C:\Users\Manni\Desktop\Build\de"; 

        public string DeserializationFilePath;

        public string DocumentName = "leer";

        public string AssemblyFileName = "leer";

        public Methoden NutzeMethode = new Methoden();

        public Assembly ChosenAssembly;

        public bool WhichComboBoxListIsActive;


        // Sonstige Listen:
        public List<MyUiElement> MyUiElementslist = new List<MyUiElement>();
        public List<MyXtraDocument> XtraDocumentsList = new List<MyXtraDocument>();
        public List<MyXtraDocument> DeserializedXtraDocsList = new List<MyXtraDocument>();
        public Dictionary<string, MyXtraDocument> XtraDocumentsDict = new Dictionary<string, MyXtraDocument>();
        public Dictionary<string, MyXtraDocument> DeserializedXtraDocsDict = new Dictionary<string, MyXtraDocument>();
        public Dictionary<string, MyUiElement> ActiveUIsDict = new Dictionary<string, MyUiElement>();
        public Dictionary<string, MyUiElement> SavedUIsDict = new Dictionary<string, MyUiElement>();

        // Container Listen:
        public List<Assembly> AssemblyList = new List<Assembly>();
        public List<Type> TypesList = new List<Type>();
        public List<XtraForm> ContainerListOfXtraForms = new List<XtraForm>();
        public List<XtraUserControl> ContainerListOfXtraUserControls = new List<XtraUserControl>();

        // finale Listen:
        public List<object> otherControlTypes = new List<object>();
        public List<XtraForm> xtraFormslist = new List<XtraForm>();
        public List<GroupControl> groupControlslist = new List<GroupControl>();
        public List<LayoutControl> layoutControlslist = new List<LayoutControl>();
        public List<DataLayoutControl> dataLayoutControlslist = new List<DataLayoutControl>();
        public List<LayoutGroup> layoutGroupslist = new List<LayoutGroup>();
        public List<TabbedGroup> tabbedGroupslist = new List<TabbedGroup>();
        public List<TabbedControlGroup> tabbedControlGroupslist = new List<TabbedControlGroup>();
        public List<LayoutControlGroup> layoutControlGroupslist = new List<LayoutControlGroup>();
        public List<LayoutControlItem> layoutControlItemslist = new List<LayoutControlItem>();
        public List<GridControl> gridControlslist = new List<GridControl>();
        public List<GridLookUpEdit> gridLookUpEditslist = new List<GridLookUpEdit>();
        public List<GridColumn> gridColumnslist = new List<GridColumn>();
        public List<ColumnView> gridColumnsViewlist = new List<ColumnView>();
        public List<SimpleButton> simpleButtonslist = new List<SimpleButton>();
        public List<CheckEdit> checkEditslist = new List<CheckEdit>();
        public List<XtraTabControl> xtraTabControlslist = new List<XtraTabControl>();
        public List<XtraTabPage> xtraTabPageslist = new List<XtraTabPage>();
        public List<TextEdit> textEditslist = new List<TextEdit>();
        public List<RichEditControl> richEditControlslist = new List<RichEditControl>();
        public List<ComboBoxEdit> comboBoxEditslist = new List<ComboBoxEdit>();
        public List<CheckedComboBoxEdit> checkedComboBoxEditslist = new List<CheckedComboBoxEdit>();
        public List<LabelControl> labelControlslist = new List<LabelControl>();
        public List<ImageEdit> imageEditslist = new List<ImageEdit>();
        public List<DateEdit> dateEditslist = new List<DateEdit>();
        public List<ButtonEdit> buttonEditslist = new List<ButtonEdit>();
        public List<DropDownButton> dropDownButtonslist = new List<DropDownButton>();
        public List<CheckedListBoxControl> checkedListBoxControlslist = new List<CheckedListBoxControl>();
        public List<ListBoxControl> listBoxControlslist = new List<ListBoxControl>();
        public List<TileControl> tileControlslist = new List<TileControl>();
        public List<TileGroup> tileGroupslist = new List<TileGroup>();
        public List<TileItem> tileItemslist = new List<TileItem>();
        public List<TileItemElement> tileItemElementslist = new List<TileItemElement>();
        public List<TileBar> tileBarslist = new List<TileBar>();
        public List<TileBarGroup> tileBarGroupslist = new List<TileBarGroup>();
        public List<AccordionControl> accordionControlslist = new List<AccordionControl>();
        public List<AccordionControlElement> accordionControlElementslist = new List<AccordionControlElement>();
        public List<NavBarControl> navBarControlslist = new List<NavBarControl>();
        public List<NavBarGroup> navBarGroupslist = new List<NavBarGroup>();
        public List<NavBarItem> navBarItemslist = new List<NavBarItem>();
        public List<PopupMenu> popupMenuslist = new List<PopupMenu>();
        public List<RadioGroup> radioGroupslist = new List<RadioGroup>();
        public List<BarManager> barManagerslist = new List<BarManager>();
        public List<BarButtonGroup> barButtonGroupslist = new List<BarButtonGroup>();
        public List<BarButtonItem> barButtonItemslist = new List<BarButtonItem>();
        public List<LookUpEdit> lookUpEditslist = new List<LookUpEdit>();
        public List<LookUpColumnInfo> lookUpColumnInfoslist = new List<LookUpColumnInfo>();
        public List<RepositoryItemLookUpEdit> repositoryItemLookUpEditslist = new List<RepositoryItemLookUpEdit>();     // bisher das einzige Control, das als RepositoryItem Text hat
        public List<SearchControl> searchControlslist = new List<SearchControl>();                                      // hat Nulltext
        public List<TreeList> treeListslist = new List<TreeList>();

        public List<LayoutViewColumn> layoutViewColumnslist = new List<LayoutViewColumn>();     // wie herausfinden, welcher columntyp benutzt wird?    // Caption = .LayOutviewField
        public List<TileViewColumn> tileViewColumns = new List<TileViewColumn>();

        // bisher im K3 gefundene Forms Controls:
        public List<GroupBox> groupBoxeslist = new List<GroupBox>();
        public List<CheckBox> checkBoxeslist = new List<CheckBox>();
        public List<Button> buttonslist = new List<Button>();
        public List<Label> labelslist = new List<Label>();
        public List<PictureBox> pictureBoxeslist = new List<PictureBox>();
        public List<ToolStripMenuItem> toolStripMenuItemslist = new List<ToolStripMenuItem>();
        public List<ContextMenuStrip> contextMenuStripslist = new List<ContextMenuStrip>();


        //------------------------------------------------------------------------------------|

        // --> Verzeichnis durchsuchen nach passenden Assemblies
        private void ladeAsseblies()
        {
            AssemblyList = NutzeMethode.LadeAssemblies(AssemblyFilePath);

            listBox1.Items.Add($"Assemblies Count: {AssemblyList.Count}");

            BefuelleComboBox1();

            gridView1.Columns.Clear();
            gridControl1.DataSource = null;
        }

        // --> Xtra Dokumente aus Assembly extrahieren und Controls hinzufügen
        private void SucheUndErstelleDokumente()
        {
            TypesList = NutzeMethode.LadeTypes(ChosenAssembly, TypesList);

            listBox1.Items.Add($"Types Count: {TypesList.Count}");

            (ContainerListOfXtraForms, ContainerListOfXtraUserControls) = NutzeMethode.SortiereTypes(TypesList, ContainerListOfXtraForms, ContainerListOfXtraUserControls);

            listBox1.Items.Add($"XtraForms Count: {ContainerListOfXtraForms.Count}");
            listBox1.Items.Add($"XtraUserControls Count: {ContainerListOfXtraUserControls.Count}");

            // -------------------------------->>> XtraForms
            if (ContainerListOfXtraForms != null && ContainerListOfXtraForms.Count > 0)
            {
                foreach (var item in ContainerListOfXtraForms)
                {
                    if (item != null)
                    {
                        MyXtraDocument xtra_Document = new MyXtraDocument();
                        xtra_Document.Name = item.Name;
                        xtra_Document.ObjektTyp = MyXtraDocument.Klasse.XtraForm;

                        if (ChosenAssembly != null)
                        {
                            AssemblyFileName = ChosenAssembly.Location.Split('\\').Last();
                            AssemblyFileName = AssemblyFileName.Split('.')[0];
                        }
                        else
                        {
                            AssemblyFileName = "leer";
                        }
                        xtra_Document.Assembly = AssemblyFileName;

                        xtra_Document.Projekt = item.CompanyName;

                        if (item.Controls != null && item.Controls.Count > 0)
                        {
                            SucheUndUnterscheideKindElemente(item);     // nur XtraForm besitzt TitelText, XtraUserControl nicht

                            foreach (var control in item.Controls)
                            {
                                SucheUndUnterscheideKindElemente(control);
                            }
                        }

                        DocumentName = AssemblyFileName + "_" + item.Name;

                        dictionary.Clear();
                        ListeAlleErgebnisse();

                        xtra_Document.MyUiElementsList.AddRange(MyUiElementslist);

                        xtra_Document.Filename = SerializationFilePath + "\\" + AssemblyFileName + "_" + xtra_Document.ObjektTyp + "_" + xtra_Document.Name + ".json";

                        XtraDocumentsList.Add(xtra_Document);

                        MyUiElementslist.Clear();
                    }
                }
            }

            // -------------------------------->>> XtraUserControls
            if (ContainerListOfXtraUserControls != null && ContainerListOfXtraUserControls.Count > 0)
            {
                foreach (var item in ContainerListOfXtraUserControls)
                {
                    if (item != null)
                    {
                        MyXtraDocument xtra_Document = new MyXtraDocument();
                        xtra_Document.Name = item.Name;
                        xtra_Document.ObjektTyp = MyXtraDocument.Klasse.XtraUserControl;

                        if (ChosenAssembly != null)
                        {
                            AssemblyFileName = ChosenAssembly.Location.Split('\\').Last();
                            AssemblyFileName = AssemblyFileName.Split('.')[0];
                        }
                        else
                        {
                            AssemblyFileName = "leer";
                        }
                        xtra_Document.Assembly = AssemblyFileName;

                        xtra_Document.Projekt = item.CompanyName;

                        foreach (var control in item.Controls)
                        {
                            SucheUndUnterscheideKindElemente(control);
                        }

                        DocumentName = item.Name;

                        dictionary.Clear();
                        ListeAlleErgebnisse();

                        xtra_Document.MyUiElementsList.AddRange(MyUiElementslist);

                        xtra_Document.Filename = SerializationFilePath + "\\" + AssemblyFileName + "_" + xtra_Document.ObjektTyp + "_" + xtra_Document.Name + ".json";

                        XtraDocumentsList.Add(xtra_Document);

                        MyUiElementslist.Clear();
                    }
                }
            }

            listBox1.Items.Add($"XtraDocuments Count: {XtraDocumentsList.Count}");

            WhichComboBoxListIsActive = false;
            BefuelleComboBox2(XtraDocumentsList);
            comboBoxEdit2.BackColor = Color.DeepSkyBlue;

            gridView1.Columns.Clear();
            gridControl1.DataSource = null;
            gridControl1.DataSource = XtraDocumentsList;

            TypesList.Clear();
            ContainerListOfXtraForms.Clear();
            ContainerListOfXtraUserControls.Clear();
        }

        // --> Switch Case Filter
        public void SucheUndUnterscheideKindElemente(Object item)
        {
            if (item != null)
            {
                if (!dictionary.ContainsKey(item))
                {
                    switch (item)
                    {
                        case XtraForm i when typeof(XtraForm).IsAssignableFrom(item.GetType()):
                            xtraFormslist.Add(i);
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

                            if (lc.Items.Count > 0)
                            {
                                foreach (var lc_item in lc.Items)
                                {
                                    SucheUndUnterscheideKindElemente(lc_item);
                                }
                            }
                            break;

                        case TabbedGroup lc when typeof(TabbedGroup).IsAssignableFrom(item.GetType()):
                            tabbedGroupslist.Add(lc);

                            if (lc.TabPages.Count > 0)
                            {
                                foreach (var i in lc.TabPages)
                                {
                                    SucheUndUnterscheideKindElemente(i);
                                }
                            }
                            break;

                        case TabbedControlGroup lc when typeof(TabbedControlGroup).IsAssignableFrom(item.GetType()):
                            tabbedControlGroupslist.Add(lc);

                            if (lc.TabPages.Count > 0)  // diese .TabPages sind nicht XtraTabPage sondern LayoutControlGroup!
                            {
                                foreach (var i in lc.TabPages)
                                {
                                    SucheUndUnterscheideKindElemente(i);
                                }
                            }
                            break;

                        case LayoutControlGroup lc when typeof(LayoutControlGroup).IsAssignableFrom(lc.GetType()):
                            layoutControlGroupslist.Add(lc);

                            SucheUndUnterscheideKindElemente(lc.ParentTabbedGroup);

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

                            SucheUndUnterscheideKindElemente(lc.Control);

                            break;

                        case GroupControl lc when typeof(GroupControl).IsAssignableFrom(item.GetType()):
                            groupControlslist.Add(lc);

                            if (lc.Controls.Count > 0)
                            {
                                foreach (var lc_control in lc.Controls)
                                {
                                    SucheUndUnterscheideKindElemente(lc_control);
                                }
                            }
                            break;

                        case GridControl i when typeof(GridControl).IsAssignableFrom(item.GetType()):
                            gridControlslist.Add(i);

                            GridView y = i.DefaultView as GridView;

                            if (y.Columns.Count > 0)
                            {
                                foreach (var j in y.Columns)
                                {
                                    SucheUndUnterscheideKindElemente(j);
                                }
                            }
                            break;

                        case GridLookUpEdit i when typeof(GridLookUpEdit).IsAssignableFrom(item.GetType()):
                            gridLookUpEditslist.Add(i);

                            if (i.Properties.PopupView.Columns.Count > 0)
                            {
                                foreach (var j in i.Properties.PopupView.Columns)
                                {
                                    SucheUndUnterscheideKindElemente(j);
                                }
                            }
                            break;

                        case GridColumn i when typeof(GridColumn).IsAssignableFrom(item.GetType()):
                            gridColumnslist.Add(i);
                            break;

                        case ColumnView i when typeof(ColumnView).IsAssignableFrom(item.GetType()):
                            gridColumnsViewlist.Add(i);
                            break;

                        case XtraTabControl i when typeof(XtraTabControl).IsAssignableFrom(item.GetType()):
                            xtraTabControlslist.Add(i);

                            if (i.HasChildren)
                            {
                                foreach (var i_item in i.TabPages)
                                {
                                    SucheUndUnterscheideKindElemente(i_item);
                                }
                                foreach (var i_control in i.Controls)
                                {
                                    SucheUndUnterscheideKindElemente(i_control);
                                }
                            }
                            break;

                        case XtraTabPage i when typeof(XtraTabPage).IsAssignableFrom(item.GetType()):
                            xtraTabPageslist.Add(i);
                            break;

                        case CheckEdit i when typeof(CheckEdit).IsAssignableFrom(item.GetType()):
                            checkEditslist.Add(i);
                            break;

                        case DropDownButton i when typeof(DropDownButton).IsAssignableFrom(item.GetType()):
                            dropDownButtonslist.Add(i);
                            break;

                        case DateEdit i when typeof(DateEdit).IsAssignableFrom(item.GetType()):
                            dateEditslist.Add(i);
                            break;

                        case ImageEdit i when typeof(ImageEdit).IsAssignableFrom(item.GetType()):
                            imageEditslist.Add(i);
                            break;

                        case ComboBoxEdit i when typeof(ComboBoxEdit).IsAssignableFrom(item.GetType()):
                            comboBoxEditslist.Add(i);
                            break;

                        case TextEdit i when typeof(TextEdit).IsAssignableFrom(item.GetType()):
                            textEditslist.Add(i);
                            break;

                        case CheckedListBoxControl i when typeof(CheckedListBoxControl).IsAssignableFrom(item.GetType()):
                            checkedListBoxControlslist.Add(i);
                            break;

                        case ListBoxControl i when typeof(ListBoxControl).IsAssignableFrom(item.GetType()):
                            listBoxControlslist.Add(i);
                            break;

                        case ButtonEdit i when typeof(ButtonEdit).IsAssignableFrom(item.GetType()):
                            buttonEditslist.Add(i);
                            break;

                        case SimpleButton i when typeof(SimpleButton).IsAssignableFrom(item.GetType()):
                            simpleButtonslist.Add(i);
                            break;

                        case LabelControl i when typeof(LabelControl).IsAssignableFrom(item.GetType()):
                            labelControlslist.Add(i);
                            break;

                        case RichEditControl i when typeof(RichEditControl).IsAssignableFrom(item.GetType()):
                            richEditControlslist.Add(i);
                            break;

                        case CheckedComboBoxEdit i when typeof(CheckedComboBoxEdit).IsAssignableFrom(item.GetType()):
                            checkedComboBoxEditslist.Add(i);
                            break;

                        case NavBarControl i when typeof(NavBarControl).IsAssignableFrom(item.GetType()):
                            navBarControlslist.Add(i);

                            if (i.HasChildren)
                            {
                                foreach (var j in i.Groups)
                                {
                                    SucheUndUnterscheideKindElemente(j);
                                }
                                foreach (var j in i.Controls)
                                {
                                    SucheUndUnterscheideKindElemente(j);
                                }
                                foreach (var j in i.Items)
                                {
                                    SucheUndUnterscheideKindElemente(j);
                                }
                            }
                            break;

                        case NavBarGroup i when typeof(NavBarGroup).IsAssignableFrom(item.GetType()):
                            navBarGroupslist.Add(i);

                            foreach (NavBarItemLink j in i.ItemLinks)   // NavBarItem == NavBarItemLink(Item)
                            {
                                SucheUndUnterscheideKindElemente(j.Item);
                            }
                            break;

                        case NavBarItem i when typeof(NavBarItem).IsAssignableFrom(item.GetType()):
                            navBarItemslist.Add(i);
                            break;

                        case AccordionControl i when typeof(AccordionControl).IsAssignableFrom(item.GetType()):
                            accordionControlslist.Add(i);

                            if (i.HasChildren)
                            {
                                foreach (var j in i.Controls)
                                {
                                    SucheUndUnterscheideKindElemente(j);
                                }
                                foreach (var j in i.Elements)
                                {
                                    SucheUndUnterscheideKindElemente(j);
                                }
                            }
                            break;

                        case AccordionControlElement i when typeof(AccordionControlElement).IsAssignableFrom(item.GetType()):
                            accordionControlElementslist.Add(i);

                            if (i.Elements.Count > 0)
                            {
                                foreach (var j in i.Elements)
                                {
                                    SucheUndUnterscheideKindElemente(j);
                                }
                            }
                            break;

                        case TileControl i when typeof(TileControl).IsAssignableFrom(item.GetType()):
                            tileControlslist.Add(i);

                            if (i.Groups.Count > 0)
                            {
                                foreach (var j in i.Groups)
                                {
                                    SucheUndUnterscheideKindElemente(j);
                                }
                            }
                            if (i.Controls.Count > 0)
                            {
                                foreach (var j in i.Controls)
                                {
                                    SucheUndUnterscheideKindElemente(j);
                                }
                            }
                            break;

                        case TileGroup i when typeof(TileGroup).IsAssignableFrom(item.GetType()):
                            tileGroupslist.Add(i);

                            if (i.Items.Count > 0)
                            {
                                foreach (var j in i.Items)
                                {
                                    SucheUndUnterscheideKindElemente(j);
                                }
                            }
                            break;

                        case TileBarGroup i when typeof(TileBarGroup).IsAssignableFrom(item.GetType()):
                            tileBarGroupslist.Add(i);

                            SucheUndUnterscheideKindElemente(i.Control);

                            if (i.Items.Count > 0)
                            {
                                foreach (var j in i.Items)
                                {
                                    SucheUndUnterscheideKindElemente(j);
                                }
                            }
                            break;

                        case TileBar i when typeof(TileBar).IsAssignableFrom(item.GetType()):
                            tileBarslist.Add(i);

                            if (i.HasChildren)
                            {
                                foreach (var j in i.Controls)
                                {
                                    SucheUndUnterscheideKindElemente(j);
                                }
                                foreach (var j in i.Groups)
                                {
                                    SucheUndUnterscheideKindElemente(j);
                                }
                            }
                            break;

                        case TileItem i when typeof(TileItem).IsAssignableFrom(item.GetType()):
                            tileItemslist.Add(i);

                            if (i.Elements.Count > 0)
                            {
                                foreach (var j in i.Elements)
                                {
                                    SucheUndUnterscheideKindElemente(j);
                                }
                            }
                            break;

                        case TileItemElement i when typeof(TileItemElement).IsAssignableFrom(item.GetType()):
                            tileItemElementslist.Add(i);
                            break;

                        default:
                            otherControlTypes.Add(item);
                            break;
                    }
                    dictionary.Add(item, item);
                }
            }
        }

        // --> List<MyUiElement> füllen
        public void ListeAlleErgebnisse()
        {
            MyUiElementslist.Clear();

            foreach (var item in xtraFormslist)
            {
                MyUiElementslist.Add(new MyUiElement
                {
                    ItemClassName = MyUiElement.ClassName.XtraForm,
                    Name = item.Name,
                    Text = item.Text,
                    XtraDokument = DocumentName
                });
            }
            foreach (var item in gridControlslist)
            {
                MyUiElementslist.Add(new MyUiElement
                {
                    ItemClassName = MyUiElement.ClassName.GridControl,
                    Name = item.Name,
                    Text = item.Text,
                    XtraDokument = DocumentName
                });
            }
            foreach (var item in gridColumnslist)
            {
                MyUiElementslist.Add(new MyUiElement
                {
                    ItemClassName = MyUiElement.ClassName.GridColumn,
                    Name = item.Name,
                    Caption = item.Caption,
                    XtraDokument = DocumentName
                });
            }
            foreach (var item in textEditslist)
            {
                MyUiElementslist.Add(new MyUiElement
                {
                    ItemClassName = MyUiElement.ClassName.TextEdit,
                    Name = item.Name,
                    Text = item.Text,
                    XtraDokument = DocumentName,
                    SuperTip = item.SuperTip == null ? "" : item.SuperTip.Items.Count.ToString()
                });
            }
            foreach (var item in simpleButtonslist)
            {
                MyUiElementslist.Add(new MyUiElement
                {
                    ItemClassName = MyUiElement.ClassName.SimpleButton,
                    Name = item.Name,
                    Text = item.Text,
                    XtraDokument = DocumentName,
                    SuperTip = item.SuperTip == null ? "" : item.SuperTip.Items.Count.ToString()
                });
            }
            foreach (var item in checkEditslist)
            {
                MyUiElementslist.Add(new MyUiElement
                {
                    ItemClassName = MyUiElement.ClassName.CheckEdit,
                    Name = item.Name,
                    Text = item.Text,
                    XtraDokument = DocumentName,
                    SuperTip = item.SuperTip == null ? "" : item.SuperTip.Items.Count.ToString()
                });
            }
            foreach (var item in comboBoxEditslist)
            {
                MyUiElementslist.Add(new MyUiElement
                {
                    ItemClassName = MyUiElement.ClassName.ComboBoxEdit,
                    Name = item.Name,
                    Text = item.Text,
                    XtraDokument = DocumentName,
                    SuperTip = item.SuperTip == null ? "" : item.SuperTip.Items.Count.ToString()
                });
            }
            foreach (var item in layoutControlItemslist)
            {
                MyUiElementslist.Add(new MyUiElement
                {
                    ItemClassName = MyUiElement.ClassName.LayoutControlItem,
                    Name = item.Name,
                    Text = item.Text,
                    XtraDokument = DocumentName
                });
            }
            foreach (var item in layoutControlGroupslist)
            {
                MyUiElementslist.Add(new MyUiElement
                {
                    ItemClassName = MyUiElement.ClassName.LayoutControlGroup,
                    Name = item.Name,
                    Text = item.Text,
                    XtraDokument = DocumentName
                });
            }
            foreach (var item in tabbedControlGroupslist)
            {
                MyUiElementslist.Add(new MyUiElement
                {
                    ItemClassName = MyUiElement.ClassName.TabbedControlGroup,
                    Name = item.Name,
                    Text = item.Text,
                    XtraDokument = DocumentName
                });
            }
            foreach (var item in dataLayoutControlslist)
            {
                MyUiElementslist.Add(new MyUiElement
                {
                    ItemClassName = MyUiElement.ClassName.DataLayoutControl,
                    Name = item.Name,
                    Text = item.Text,
                    XtraDokument = DocumentName
                });
            }
            foreach (var item in layoutGroupslist)
            {
                MyUiElementslist.Add(new MyUiElement
                {
                    ItemClassName = MyUiElement.ClassName.LayoutGroup,
                    Name = item.Name,
                    Text = item.Text,
                    XtraDokument = DocumentName
                });
            }
            foreach (var item in layoutControlslist)
            {
                MyUiElementslist.Add(new MyUiElement
                {
                    ItemClassName = MyUiElement.ClassName.LayoutControl,
                    Name = item.Name,
                    Text = item.Text,
                    XtraDokument = DocumentName
                });
            }
            foreach (var item in xtraTabPageslist)
            {
                MyUiElementslist.Add(new MyUiElement
                {
                    ItemClassName = MyUiElement.ClassName.XtraTabPage,
                    Name = item.Name,
                    Text = item.Text,
                    XtraDokument = DocumentName
                });
            }
            foreach (var item in buttonEditslist)
            {
                MyUiElementslist.Add(new MyUiElement
                {
                    ItemClassName = MyUiElement.ClassName.ButtonEdit,
                    Name = item.Name,
                    Text = item.Text,
                    XtraDokument = DocumentName,
                    SuperTip = item.SuperTip == null ? "" : item.SuperTip.Items.Count.ToString()
                });
            }
            foreach (var item in imageEditslist)
            {
                MyUiElementslist.Add(new MyUiElement
                {
                    ItemClassName = MyUiElement.ClassName.ImageEdit,
                    Name = item.Name,
                    Text = item.Text,
                    XtraDokument = DocumentName,
                    SuperTip = item.SuperTip == null ? "" : item.SuperTip.Items.Count.ToString()
                });
            }
            foreach (var item in groupControlslist)
            {
                MyUiElementslist.Add(new MyUiElement
                {
                    ItemClassName = MyUiElement.ClassName.GroupControl,
                    Name = item.Name,
                    Text = item.Text,
                    XtraDokument = DocumentName
                });
            }
            foreach (var item in dateEditslist)
            {
                MyUiElementslist.Add(new MyUiElement
                {
                    ItemClassName = MyUiElement.ClassName.DateEdit,
                    Name = item.Name,
                    Text = item.Text,
                    XtraDokument = DocumentName,
                    SuperTip = item.SuperTip == null ? "" : item.SuperTip.Items.Count.ToString()
                });
            }
            foreach (var item in dropDownButtonslist)
            {
                MyUiElementslist.Add(new MyUiElement
                {
                    ItemClassName = MyUiElement.ClassName.DropDownButton,
                    Name = item.Name,
                    Text = item.Text,
                    XtraDokument = DocumentName,
                    SuperTip = item.SuperTip == null ? "" : item.SuperTip.Items.Count.ToString()
                });
            }
            foreach (var item in gridLookUpEditslist)
            {
                MyUiElementslist.Add(new MyUiElement
                {
                    ItemClassName = MyUiElement.ClassName.GridLookUpEdit,
                    Name = item.Name,
                    Text = item.Text,
                    XtraDokument = DocumentName,
                    SuperTip = item.SuperTip == null ? "" : item.SuperTip.Items.Count.ToString()
                });
            }
            foreach (var item in gridColumnsViewlist)
            {
                MyUiElementslist.Add(new MyUiElement
                {
                    ItemClassName = MyUiElement.ClassName.GridColumnView,
                    Name = item.Name,
                    Other = item.ViewCaption,
                    XtraDokument = DocumentName
                });
            }
            foreach (var item in checkedListBoxControlslist)
            {
                MyUiElementslist.Add(new MyUiElement
                {
                    ItemClassName = MyUiElement.ClassName.CheckedListBoxControl,
                    Name = item.Name,
                    Text = item.Text,
                    XtraDokument = DocumentName,
                    SuperTip = item.SuperTip == null ? "" : item.SuperTip.Items.Count.ToString()
                });
            }
            foreach (var item in listBoxControlslist)
            {
                MyUiElementslist.Add(new MyUiElement
                {
                    ItemClassName = MyUiElement.ClassName.ListBoxControl,
                    Name = item.Name,
                    Text = item.Text,
                    XtraDokument = DocumentName,
                    SuperTip = item.SuperTip == null ? "" : item.SuperTip.Items.Count.ToString()
                });
            }
            foreach (var item in tileControlslist)
            {
                MyUiElementslist.Add(new MyUiElement
                {
                    ItemClassName = MyUiElement.ClassName.TileControl,
                    Name = item.Name,
                    Text = item.Text,
                    XtraDokument = DocumentName
                }); ;
            }
            foreach (var item in tileGroupslist)
            {
                MyUiElementslist.Add(new MyUiElement
                {
                    ItemClassName = MyUiElement.ClassName.TileGroup,
                    Name = item.Name,
                    Text = item.Text,
                    XtraDokument = DocumentName
                });
            }
            foreach (var item in tileItemslist)
            {
                MyUiElementslist.Add(new MyUiElement
                {
                    ItemClassName = MyUiElement.ClassName.TileItem,
                    Name = item.Name,
                    Text = item.Text,
                    XtraDokument = DocumentName,
                    SuperTip = item.SuperTip == null ? "" : item.SuperTip.Items.Count.ToString()
                });
            }
            foreach (var item in tileItemElementslist)
            {
                MyUiElementslist.Add(new MyUiElement
                {
                    ItemClassName = MyUiElement.ClassName.TileItemElement,
                    Text = item.Text,
                    XtraDokument = DocumentName
                });
            }
            foreach (var item in tileBarslist)
            {
                MyUiElementslist.Add(new MyUiElement
                {
                    ItemClassName = MyUiElement.ClassName.TileBar,
                    Name = item.Name,
                    Text = item.Text,
                    XtraDokument = DocumentName
                });
            }
            foreach (var item in tileBarGroupslist)
            {
                MyUiElementslist.Add(new MyUiElement
                {
                    ItemClassName = MyUiElement.ClassName.TileBarGroup,
                    Name = item.Name,
                    Text = item.Text,
                    XtraDokument = DocumentName
                });
            }
            foreach (var item in accordionControlslist)
            {
                MyUiElementslist.Add(new MyUiElement
                {
                    ItemClassName = MyUiElement.ClassName.AccordionControl,
                    Name = item.Name,
                    Text = item.Text,
                    XtraDokument = DocumentName,
                    SuperTip = item.SuperTip == null ? "" : item.SuperTip.Items.Count.ToString()
                });
            }
            foreach (var item in accordionControlElementslist)
            {
                MyUiElementslist.Add(new MyUiElement
                {
                    ItemClassName = MyUiElement.ClassName.AccordionControlElement,
                    Name = item.Name,
                    Text = item.Text,
                    XtraDokument = DocumentName,
                    SuperTip = item.SuperTip == null ? "" : item.SuperTip.Items.Count.ToString(),
                    Hint = item.Hint == null ? "" : item.Hint
                });
            }
            foreach (var item in labelControlslist)
            {
                MyUiElementslist.Add(new MyUiElement
                {
                    ItemClassName = MyUiElement.ClassName.LabelControl,
                    Name = item.Name,
                    Text = item.Text,
                    XtraDokument = DocumentName,
                    SuperTip = item.SuperTip == null ? "" : item.SuperTip.Items.Count.ToString()
                });
            }
            foreach (var item in navBarControlslist)
            {
                MyUiElementslist.Add(new MyUiElement
                {
                    ItemClassName = MyUiElement.ClassName.NavBarControl,
                    Name = item.Name,
                    Text = item.Text,
                    XtraDokument = DocumentName
                });
            }
            foreach (var item in navBarGroupslist)
            {
                MyUiElementslist.Add(new MyUiElement
                {
                    ItemClassName = MyUiElement.ClassName.NavBarGroup,
                    Name = item.Name,
                    Text = item.Caption,
                    XtraDokument = DocumentName,
                    SuperTip = item.SuperTip == null ? "" : item.SuperTip.Items.Count.ToString(),
                    Hint = item.Hint == null ? "" : item.Hint
                });
            }
            foreach (var item in navBarItemslist)
            {
                MyUiElementslist.Add(new MyUiElement
                {
                    ItemClassName = MyUiElement.ClassName.NavBarItem,
                    Name = item.Name,
                    Caption = item.Caption,
                    XtraDokument = DocumentName,
                    SuperTip = item.SuperTip == null ? "" : item.SuperTip.Items.Count.ToString(),
                    Hint = item.Hint == null ? "" : item.Hint
                });
            }
            foreach (var item in richEditControlslist)
            {
                MyUiElementslist.Add(new MyUiElement
                {
                    ItemClassName = MyUiElement.ClassName.RichEditControl,
                    Name = item.Name,
                    Text = item.Text,
                    XtraDokument = DocumentName
                });
            }
            foreach (var item in checkedComboBoxEditslist)
            {
                MyUiElementslist.Add(new MyUiElement
                {
                    ItemClassName = MyUiElement.ClassName.CheckedComboBoxEdit,
                    Name = item.Name,
                    Text = item.Text,
                    XtraDokument = DocumentName,
                    SuperTip = item.SuperTip == null ? "" : item.SuperTip.Items.Count.ToString()
                });
            }

            xtraFormslist.Clear();
            accordionControlElementslist.Clear();
            labelControlslist.Clear();
            navBarControlslist.Clear();
            navBarGroupslist.Clear();
            navBarItemslist.Clear();
            richEditControlslist.Clear();
            checkedComboBoxEditslist.Clear();
            accordionControlslist.Clear();
            tileBarGroupslist.Clear();
            tileControlslist.Clear();
            tileGroupslist.Clear();
            tileItemslist.Clear();
            tileItemElementslist.Clear();
            tileBarslist.Clear();
            checkedListBoxControlslist.Clear();
            listBoxControlslist.Clear();
            gridColumnsViewlist.Clear();
            dropDownButtonslist.Clear();
            gridLookUpEditslist.Clear();
            layoutControlslist.Clear();
            layoutControlItemslist.Clear();
            layoutControlGroupslist.Clear();
            gridControlslist.Clear();
            gridColumnslist.Clear();
            textEditslist.Clear();
            tabbedControlGroupslist.Clear();
            simpleButtonslist.Clear();
            checkEditslist.Clear();
            comboBoxEditslist.Clear();
            xtraTabPageslist.Clear();
            layoutGroupslist.Clear();
            dataLayoutControlslist.Clear();
            buttonEditslist.Clear();
            imageEditslist.Clear();
            dateEditslist.Clear();
            groupControlslist.Clear();
        }

        // --> Serialisiere alle neuen Elemente aus einer Assembly 
        private void serialisiereAlleneuenControls()
        {
            int count = 0;

            DeserializedXtraDocsList = NutzeMethode.DeserializeAllFilesFromActiveFolder(SerializationFilePath);

            listBox1.Items.Add("XtraDokumente deserialisiert Count: " + DeserializedXtraDocsList.Count);

            XtraDocumentsDict = XtraDocumentsList.ToDictionary(x => x.Name, x => x);
            DeserializedXtraDocsDict = DeserializedXtraDocsList.ToDictionary(x => x.Name, x => x);

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
                        SavedUIsDict.Clear();
                    }
                }
                ActiveUIsDict.Clear();
            }

            DeserializedXtraDocsList = DeserializedXtraDocsDict.Values.ToList();

            foreach (var item in DeserializedXtraDocsList)
            {
                if (!File.Exists(item.Filename))
                {
                    listBox1.Items.Add("Neue Datei erstellt: " + item.Name);
                }
                else
                {
                    listBox1.Items.Add("Datei updated: " + item.Name);
                }

                string jsonstring = JsonConvert.SerializeObject(item, Formatting.Indented);
                File.WriteAllText(item.Filename, jsonstring);
            }

            listBox1.Items.Add("Assembly Datei updated: " + AssemblyFileName);

            WhichComboBoxListIsActive = true;
            BefuelleComboBox2(DeserializedXtraDocsList);
            comboBoxEdit2.BackColor = Color.LightGreen;

            gridView1.Columns.Clear();
            gridControl1.DataSource = null;
            gridControl1.DataSource = DeserializedXtraDocsList;
        }

        // --> Deserialisiere Json Dateien
        private void deserialisiereDateien()
        {
            DeserializedXtraDocsList = NutzeMethode.DeserializeAllFilesFromActiveFolder(DeserializationFilePath);

            listBox1.Items.Add("XtraDokumente deserialisiert Count: " + DeserializedXtraDocsList.Count);

            WhichComboBoxListIsActive = true;
            BefuelleComboBox2(DeserializedXtraDocsList);
            comboBoxEdit2.BackColor = Color.LightGreen;

            gridView1.Columns.Clear();
            gridControl1.DataSource = null;
            gridControl1.DataSource = DeserializedXtraDocsList;
        }

        // --> Ersetze Texte
        private void ersetzeTexteToolStripMenuItem_Click(object sender, EventArgs e)
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

                gridView1.Columns.Clear();
                gridControl1.DataSource = null;
                gridControl1.DataSource = XtraDocumentsList;
            }
        }

        //------------------------------------------------------------------------------------|

        // Testing
        private void testeLINQToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ListOfEqualXtraDocuments = XtraDocumentsList.Where(x => DeserializedXtraDocsList.Any(y => y.Name == x.Name && y.Projekt == x.Projekt)).ToList();

            var CollectionOfEqualXtraDocuments2 = XtraDocumentsList.Select(x => x.Name).Intersect(DeserializedXtraDocsList.Select(x => x.Name));

            foreach (var item in XtraDocumentsList.Where(x => ListOfEqualXtraDocuments.Any(y => y.Name.Contains(x.Name))))
            {
            }

            //.AddRange(from x in XtraDocumentsList[0].MyUiElementsList select x);

            //.AddRange(DeserializedXtraDocsList.SelectMany(x => x.MyUiElementsList)
            //.Where(y => y.Name.Contains("")));


            // if (NewActiveUiElements.Any(x => MySavedUiElements.Any(y => y.Name == x.Name)))
            {
            }



            // olditem.MyUiElementsList.AddRange(newitem.MyUiElementsList.Where(x => olditem.MyUiElementsList.Any(y => !y.Name.Contains(x.Name))));

            if (XtraDocumentsList.Any(x => DeserializedXtraDocsList.Any(y => y.Name != x.Name)))

                DeserializedXtraDocsList.AddRange(XtraDocumentsList.Where(x => DeserializedXtraDocsList.Any(y => y.Name != x.Name)));

            gridView1.Columns.Clear();
            gridControl1.DataSource = null;
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XtraDoc1 xtraDoc1 = new XtraDoc1();
            xtraDoc1.Show();

            gridView1.Columns.Clear();
            gridControl1.DataSource = null;
        }

        //------------------------------------------------------------------------------------|

        // Verzeichnis auswählen für Assemblies
        private void verzeichnisWählenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    string[] files = Directory.GetFiles(fbd.SelectedPath);

                    AssemblyFilePath = fbd.SelectedPath;
                }
            }

            ladeAsseblies();
        }

        // Verzeichnis automatisch auswählen für Assemblies
        private void autoVerzeichnisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AssemblyFilePath = @"C:\k3tfs\Programm\bin\Debug";

            ladeAsseblies();
        }

        // Verzeichnis auswählen zum Serialisieren
        private void wähleVerzeichnisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    string[] files = Directory.GetFiles(fbd.SelectedPath);

                    SerializationFilePath = fbd.SelectedPath;
                }
            }

            serialisiereAlleneuenControls();
        }

        // Verzeichnis automatisch auswählen zum Serialisieren
        private void autoVerzeichnisToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            SerializationFilePath = @"C:\Users\Manni\Desktop\Build\de";

            serialisiereAlleneuenControls();
        }

        // Verzeichnis auswählen zum Deserialisieren
        private void wähleVerzeichnisToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    string[] files = Directory.GetFiles(fbd.SelectedPath);

                    DeserializationFilePath = fbd.SelectedPath;
                }
            }

            deserialisiereDateien();
        }

        // Verzeichnis automatisch auswählen zum Deserialisieren
        private void autoVerzeichnisToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DeserializationFilePath = @"C:\Users\Manni\Desktop\Build\de";

            deserialisiereDateien();
        }

        //------------------------------------------------------------------------------------|

        // Comboboxen
        private void BefuelleComboBox1()
        {
            comboBoxEdit1.Controls.Clear();
            ComboBoxItemCollection coll = comboBoxEdit1.Properties.Items;
            coll.BeginUpdate();
            coll.Clear();
            try
            {
                foreach (var item in AssemblyList)
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

        private void BefuelleComboBox2(List<MyXtraDocument> ChosenList)
        {
            comboBoxEdit2.Controls.Clear();
            ComboBoxItemCollection coll = comboBoxEdit2.Properties.Items;
            coll.BeginUpdate();
            coll.Clear();
            try
            {
                foreach (var item in ChosenList)
                {
                    coll.Add(item.Name);
                }
            }
            finally
            {
                coll.EndUpdate();
            }
            comboBoxEdit2.SelectedIndex = -1;
        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            XtraDocumentsList.Clear();

            if (AssemblyList != null && AssemblyList.Count() > 0)
            {
                foreach (var item in AssemblyList)
                {
                    if (comboBoxEdit1.SelectedItem != null && comboBoxEdit1.SelectedItem.ToString() == item.Location)
                    {
                        ChosenAssembly = item;

                        gridView1.Columns.Clear();
                        gridControl1.DataSource = null;

                        SucheUndErstelleDokumente();
                    }
                }
            }
        }

        private void comboBoxEdit2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (WhichComboBoxListIsActive == true)
            {
                if (DeserializedXtraDocsList != null)
                {
                    foreach (var item in DeserializedXtraDocsList)
                    {
                        if (comboBoxEdit2.SelectedItem != null && comboBoxEdit2.SelectedItem.ToString() == item.Name)
                        {
                            gridView1.Columns.Clear();
                            gridControl1.DataSource = null;
                            gridControl1.DataSource = item.MyUiElementsList;

                            listBox1.Items.Add($"Dokument Controls Count: {item.MyUiElementsList.Count}");
                        }
                    }
                }
            }
            else if (WhichComboBoxListIsActive == false)
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

        //------------------------------------------------------------------------------------|

        // Clear All
        private void speicherLeerenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            gridView1.Columns.Clear();
            gridControl1.DataSource = null;

            AssemblyFileName = "leer";
            DocumentName = "leer";

            AssemblyList.Clear();

            MyUiElementslist.Clear();
            XtraDocumentsList.Clear();
            DeserializedXtraDocsList.Clear();

            XtraDocumentsDict.Clear();
            DeserializedXtraDocsDict.Clear();
        }

        // Alle Dateien Löschen
        private void löscheAlleDateienToolStripMenuItem_Click(object sender, EventArgs e)
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

            if (di.GetFiles().Length == 0)
            {
                listBox1.Items.Add($"Alle Dateien gelöscht");
            }
        }

        // Show all unpicked Controls
        private void zeigeUnsortierteControlsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (otherControlTypes != null && otherControlTypes.Count > 0)
            {
                List<Control> showableControls = new List<Control>();

                foreach (var item in otherControlTypes)
                {
                    if (item != null && typeof(Control).IsAssignableFrom(item.GetType()))
                    {
                        showableControls.Add((Control)item);
                    }
                }

                gridView1.Columns.Clear();
                gridControl1.DataSource = null;
                gridControl1.DataSource = showableControls;
            }
        }

    }
}
