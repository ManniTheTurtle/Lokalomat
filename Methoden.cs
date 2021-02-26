using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using System.Collections;
using static System.Windows.Forms.Control;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using EigeneKlassen;

namespace Lokalomat
{
    public class Methoden
    {

        public static Dictionary<Object, string> dictionary = new Dictionary<object, string>();

        // Assemblysuche
        public List<Assembly> LadeAssemblies(string Verzeichnis)
        {
            List<Assembly> AssemblyList = new List<Assembly>();

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

        // Types unterscheiden und von abstract zu concrete konvertieren
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
                            xtraFormList.Add((XtraForm)Activator.CreateInstance(item));
                        }
                        else if (typeof(XtraUserControl).IsAssignableFrom(item))
                        {
                            xtraUserControlList.Add((XtraUserControl)Activator.CreateInstance(item));
                        }
                    }
                    catch (Exception)
                    {
                    }
                    
                }
            }
            return (xtraFormList, xtraUserControlList);
        }

        // Controlsuche
        public List<Control> LadeControlsundLayoutControls(Object xtraeditorscontrol)
        {
            List<Control> controls = new List<Control>();

            if (typeof(XtraForm).IsAssignableFrom(xtraeditorscontrol.GetType()))
            {
                var xtraform = (XtraForm)xtraeditorscontrol;

                if (xtraform != null && xtraform.Controls.Count > 0)
                {
                    foreach (var item in xtraform.Controls)
                    {
                        controls.Add(item as Control);    // --> List<Control>
                    }
                }
            }
            else if (typeof(XtraUserControl).IsAssignableFrom(xtraeditorscontrol.GetType()))
            {
                var xtrausercontrol = (XtraUserControl)xtraeditorscontrol;

                if (xtrausercontrol != null && xtrausercontrol.Controls.Count > 0)
                {
                    foreach (var item in xtrausercontrol.Controls)
                    {
                        controls.Add(item as Control);    // --> List<Control>
                    }
                }
            }

            else
            {
            }

            return controls;
        }

        // Deserialisiere alles
        public List<MyXtraDocument> DeserializeAllFilesFromActiveFolder(string filepath)
        {
            List<MyXtraDocument> DeserializedXtraDocsList = new List<MyXtraDocument>();

            string[] filesArray = Directory.GetFiles(filepath);

            foreach (var file in filesArray)
            {
                string jsonstring = File.ReadAllText(file);

                DeserializedXtraDocsList.Add(JsonConvert.DeserializeObject<MyXtraDocument>(jsonstring));    // momentan werden alle assemblys gemeinsam deserialized in eine Liste
            }


            return DeserializedXtraDocsList;
        }

        /*
        // Rekursive Control Suche via Foreach
        public List<Control> LookForAllControls_ForeachVersion(Control control, List<Control> allcontrolslist)
        {
            if (!dictionary.ContainsValue(control.Name))
            {
                dictionary.Add(control, control.Name);

                foreach (Control item in control.Controls)
                {
                    if (!dictionary.ContainsValue(item.Name))
                    {
                        dictionary.Add(item, item.Name);

                        allcontrolslist.Add(item);

                        if (item.HasChildren)
                        {
                            if (!dictionary.ContainsKey(item))
                            {
                                dictionary.Add(item, item.Name);
                                allcontrolslist.AddRange(LookForAllControls_ForeachVersion(item, allcontrolslist));
                            }
                        }
                    }
                }
            }

            dictionary.Clear();
            return allcontrolslist;
        }

        
        // Rekursive Control Suche via LINQ
        public List<Control> LookForAllControls_LinqVersion(Control control, Type type, List<Control> control_children_List) 
        {
            if (!dictionary.ContainsKey(control))
            {
                dictionary.Add(control, control.Name);
            
            var controls = control.Controls.Cast<Control>();

            control_children_List = controls.SelectMany(ctrl => LookForAllControls_LinqVersion(ctrl, type, control_children_List))
                                            .Concat(controls)
                                            .Where(c => c.GetType() == type).ToList();
            }

            return control_children_List;
        }

        // BaseLayoutItem Suche in LayoutControl 
        public List<BaseLayoutItem> Getbaselayoutitems(Object control, List<BaseLayoutItem> allbaselayoutslist)
        {
            if (typeof(LayoutControl).IsAssignableFrom(control.GetType()))
            {
                var layoutcontrol = control as LayoutControl;

                foreach (var lc_child in layoutcontrol.Items)
                {
                    if (lc_child.GetType() == typeof(LayoutControlGroup))
                    {
                        LayoutControlGroup x = lc_child as LayoutControlGroup;

                        allbaselayoutslist.AddRange(x.Items.ToList());

                        allbaselayoutslist.Add(x);
                    }
                    else if (lc_child.GetType() == typeof(TabbedControlGroup))
                    {
                        TabbedControlGroup x = lc_child as TabbedControlGroup;

                        allbaselayoutslist.AddRange(x.TabPages.ToList());

                        allbaselayoutslist.Add(x);
                    }
                    else if (lc_child.GetType() == typeof(LayoutControlItem))
                    {
                        LayoutControlItem x = lc_child as LayoutControlItem;

                        allbaselayoutslist.Add(x);
                    }
                }
            }
            return allbaselayoutslist;
        }

        // Rekursive BaseLayoutItem Suche in Object
        public List<Object> LookForDifferentControls2(Object control, Type type, List<Object> objectlist)
        {
            List<Object> controls = new List<Object>();

            if (!dictionary.ContainsKey(control))
            {
                dictionary.Add(control, "");

                if (control.GetType() == typeof(LayoutControlGroup))
                {
                    LayoutControlGroup x = control as LayoutControlGroup;
                    var yz = x.Items.ToList();
                    controls.Add(yz);
                }
                else if (control.GetType() == typeof(TabbedControlGroup))
                {
                    TabbedControlGroup x = control as TabbedControlGroup;
                    var yz = x.TabPages.ToList();
                    controls.Add(yz);
                }
                else if (control.GetType() == typeof(LayoutControl))
                {
                    LayoutControl x = control as LayoutControl;
                    var collection = x.Controls;
                    foreach (var item in collection)
                    {
                        controls.Add(item);
                    }
                }
                objectlist = controls.SelectMany(ctrl => LookForDifferentControls2(ctrl, type, objectlist))
                                             .Concat(controls)
                                             .Where(c => c.GetType() == type).ToList();
            }
            return objectlist;
        }
        */
    }
}
