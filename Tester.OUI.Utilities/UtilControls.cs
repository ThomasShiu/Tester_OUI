using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Globalization;
using System.Windows.Forms.VisualStyles;
using System.Data;
using System.Collections;
using System.Windows.Forms.Design;
using System.Drawing.Design;

namespace Tester.OUI.Utilities
{
    public sealed class UtilControls
    {
        public static void InitializeFormControlsPermission(List<string> controlNames, Control.ControlCollection controls)
        {
            if (controls.Count > 0)
            {
                foreach (Control control in controls)
                {
                    if (control is Form || control is UserControl) continue;

                    string fullName = control.GetType().FullName;

                    if (fullName == typeof(MenuStrip).FullName)
                    {
                        MenuStrip menuStrip = (MenuStrip)control;

                        foreach (ToolStripItem toolStripItem in menuStrip.Items)
                        {
                            if (object.Equals(toolStripItem.Tag, UtilConstants.PERMISSION_FLAG))
                            {
                                if (controlNames.Contains(toolStripItem.Name))
                                {
                                    toolStripItem.Enabled = true;

                                    if (toolStripItem is ToolStripMenuItem)
                                    {
                                        InitializeFormToolStripItemPermission(controlNames, ((ToolStripMenuItem)toolStripItem).DropDownItems);
                                    }
                                }
                                else
                                {
                                    toolStripItem.Enabled = false;
                                }
                            }
                            else
                            {
                                if (toolStripItem is ToolStripMenuItem)
                                {
                                    InitializeFormToolStripItemPermission(controlNames, ((ToolStripMenuItem)toolStripItem).DropDownItems);
                                }
                            }
                        }
                    }
                    else if (fullName == typeof(ToolStrip).FullName)
                    {
                        ToolStrip toolStrip = (ToolStrip)control;

                        foreach (ToolStripItem toolStripItem in toolStrip.Items)
                        {
                            if (object.Equals(toolStripItem.Tag, UtilConstants.PERMISSION_FLAG))
                            {
                                if (controlNames.Contains(toolStripItem.Name))
                                {
                                    toolStripItem.Enabled = true;

                                    if (toolStripItem is ToolStripDropDownButton)
                                    {
                                        InitializeFormToolStripItemPermission(controlNames, ((ToolStripDropDownButton)toolStripItem).DropDownItems);
                                    }
                                    else if (toolStripItem is ToolStripSplitButton)
                                    {
                                        InitializeFormToolStripItemPermission(controlNames, ((ToolStripSplitButton)toolStripItem).DropDownItems);
                                    }
                                }
                                else
                                {
                                    toolStripItem.Enabled = false;
                                }
                            }
                            else
                            {
                                if (toolStripItem is ToolStripDropDownButton)
                                {
                                    InitializeFormToolStripItemPermission(controlNames, ((ToolStripDropDownButton)toolStripItem).DropDownItems);
                                }
                                else if (toolStripItem is ToolStripSplitButton)
                                {
                                    InitializeFormToolStripItemPermission(controlNames, ((ToolStripSplitButton)toolStripItem).DropDownItems);
                                }
                            }
                        }
                    }
                    else if (fullName == typeof(StatusStrip).FullName)
                    {
                        StatusStrip statusStrip = (StatusStrip)control;

                        foreach (ToolStripItem toolStripItem in statusStrip.Items)
                        {
                            if (object.Equals(toolStripItem.Tag, UtilConstants.PERMISSION_FLAG))
                            {
                                if (controlNames.Contains(toolStripItem.Name))
                                {
                                    toolStripItem.Enabled = true;

                                    if (toolStripItem is ToolStripDropDownButton)
                                    {
                                        InitializeFormToolStripItemPermission(controlNames, ((ToolStripMenuItem)toolStripItem).DropDownItems);
                                    }
                                    else if (toolStripItem is ToolStripSplitButton)
                                    {
                                        InitializeFormToolStripItemPermission(controlNames, ((ToolStripMenuItem)toolStripItem).DropDownItems);
                                    }
                                }
                                else
                                {
                                    toolStripItem.Enabled = false;
                                }
                            }
                            else
                            {
                                if (toolStripItem is ToolStripDropDownButton)
                                {
                                    InitializeFormToolStripItemPermission(controlNames, ((ToolStripMenuItem)toolStripItem).DropDownItems);
                                }
                                else if (toolStripItem is ToolStripSplitButton)
                                {
                                    InitializeFormToolStripItemPermission(controlNames, ((ToolStripMenuItem)toolStripItem).DropDownItems);
                                }
                            }
                        }
                    }
                    else if (fullName == typeof(DataGridView).FullName)
                    {
                        DataGridView dataGridView = (DataGridView)control;

                        foreach (DataGridViewColumn column in dataGridView.Columns)
                        {
                            if (object.Equals(column.Tag, UtilConstants.PERMISSION_FLAG))
                            {
                                if (controlNames.Contains(column.Name))
                                {
                                    column.ReadOnly = false;
                                }
                                else
                                {
                                    column.ReadOnly = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (object.Equals(control.Tag, UtilConstants.PERMISSION_FLAG))
                        {
                            if (controlNames.Contains(control.Name))
                            {
                                control.Enabled = true;
                            }
                            else
                            {
                                control.Enabled = false;
                            }
                        }

                        InitializeFormControlsPermission(controlNames, control.Controls);
                    }
                }
            }
        }

        public static void InitializeFormComponentsPermission(List<string> controlNames, IContainer components)
        {
            if (components.Components.Count > 0)
            {
                foreach (IComponent component in components.Components)
                {
                    string fullName = component.GetType().FullName;

                    if (fullName == typeof(ContextMenuStrip).FullName)
                    {
                        ContextMenuStrip contextMenuStrip = (ContextMenuStrip)component;

                        foreach (ToolStripItem toolStripItem in contextMenuStrip.Items)
                        {
                            if (object.Equals(toolStripItem.Tag, UtilConstants.PERMISSION_FLAG))
                            {
                                if (controlNames.Contains(toolStripItem.Name))
                                {
                                    toolStripItem.Enabled = true;

                                    if (toolStripItem is ToolStripMenuItem)
                                    {
                                        InitializeFormToolStripItemPermission(controlNames, ((ToolStripMenuItem)toolStripItem).DropDownItems);
                                    }
                                }
                                else
                                {
                                    toolStripItem.Enabled = false;
                                }
                            }
                            else
                            {
                                if (toolStripItem is ToolStripMenuItem)
                                {
                                    InitializeFormToolStripItemPermission(controlNames, ((ToolStripMenuItem)toolStripItem).DropDownItems);
                                }
                            }
                        }
                    }
                }
            }
        }

        private static void InitializeFormToolStripItemPermission(List<string> controlNames, ToolStripItemCollection toolStripItems)
        {
            if (toolStripItems.Count > 0)
            {
                foreach (ToolStripItem toolStripItem in toolStripItems)
                {
                    if (object.Equals(toolStripItem.Tag, UtilConstants.PERMISSION_FLAG))
                    {
                        if (controlNames.Contains(toolStripItem.Name))
                        {
                            toolStripItem.Enabled = true;

                            if (toolStripItem is ToolStripMenuItem)
                            {
                                InitializeFormToolStripItemPermission(controlNames, ((ToolStripMenuItem)toolStripItem).DropDownItems);
                            }
                        }
                        else
                        {
                            toolStripItem.Enabled = false;
                        }
                    }
                    else
                    {
                        if (toolStripItem is ToolStripMenuItem)
                        {
                            InitializeFormToolStripItemPermission(controlNames, ((ToolStripMenuItem)toolStripItem).DropDownItems);
                        }
                    }
                }
            }
        }

        public static void InitializeFormColorListBox(ListBox listBox)
        {
            listBox.DrawMode = DrawMode.OwnerDrawVariable;
            listBox.MeasureItem += new MeasureItemEventHandler(ListBoxMeasureItem);
            listBox.DrawItem += new DrawItemEventHandler(ListBoxDrawItem);
            listBox.KeyDown += new KeyEventHandler(ListBoxKeyDown);
        }

        public static void ProcessCheckedListBoxItemCheck(object sender, ItemCheckEventArgs e)
        {
            CheckedListBox checkedListBox = (CheckedListBox)sender;

            CheckedItem checkedItem = (CheckedItem)checkedListBox.Items[e.Index];

            if (e.NewValue == CheckState.Checked)
            {
                checkedItem.CurrentChecked = true;
                checkedItem.IsChanged = checkedItem.CurrentChecked != checkedItem.OriginalChecked;
            }
            else
            {
                checkedItem.CurrentChecked = false;
                checkedItem.IsChanged = checkedItem.CurrentChecked != checkedItem.OriginalChecked;
            }

            checkedListBox.Items[e.Index] = checkedItem;
        }

        public static void ProcessCheckBoxTreeNodeChanged(TreeNode treeNode, bool isChecked)
        {
            CheckedItem checkedItem = (CheckedItem)treeNode.Tag;

            checkedItem.CurrentChecked = isChecked;
            checkedItem.IsChanged = isChecked != checkedItem.OriginalChecked;

            treeNode.Tag = checkedItem;

            if (isChecked)
            {
                if (treeNode.Parent != null)
                {
                    treeNode.Parent.Checked = true;

                    ProcessCheckBoxTreeNodeChanged(treeNode.Parent, true);
                }
            }
            else
            {
                if (treeNode.Nodes.Count > 0)
                {
                    foreach (TreeNode childNode in treeNode.Nodes)
                    {
                        childNode.Checked = false;

                        ProcessCheckBoxTreeNodeChanged(childNode, false);
                    }
                }
            }
        }

        private static void ListBoxMeasureItem(object sender, MeasureItemEventArgs e)
        {
            if (e.Index < 0) return;

            ListBox listBox = (ListBox)sender;

            string itemText = listBox.Items[e.Index].ToString();

            SizeF itemSize = e.Graphics.MeasureString(itemText, listBox.Font);

            int itemWidth = (int)Math.Ceiling(itemSize.Width);
            int itemHeight = (int)Math.Ceiling(itemSize.Height);

            if (itemWidth > listBox.HorizontalExtent)
            {
                listBox.HorizontalExtent = itemWidth;
            }

            e.ItemWidth = itemWidth;
            e.ItemHeight = itemHeight;
        }

        private static void ListBoxDrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            ListBox listBox = (ListBox)sender;

            e.DrawBackground();
            ColorItem colorItem = (ColorItem)listBox.Items[e.Index];

            e.Graphics.DrawString(colorItem.Text, e.Font, new SolidBrush(colorItem.Color == Color.Empty ? e.ForeColor : colorItem.Color), e.Bounds.X + 2, e.Bounds.Y, StringFormat.GenericTypographic);

            e.DrawFocusRectangle();
        }

        private static void ListBoxKeyDown(object sender, KeyEventArgs e)
        {
            ListBox listBox = (ListBox)sender;

            if (e.Control && e.KeyCode == Keys.C)
            {
                if (listBox.SelectedItem != null)
                {
                    Clipboard.SetText(listBox.SelectedItem.ToString());
                }
            }
        }

        /// <summary>
        /// Collect Changed Item List From CheckedListBox Control(Include Selected and Unselected)
        /// </summary>
        /// <param name="checkedListBoxItems"></param>
        /// <param name="selectedItemList"></param>
        /// <param name="unselectedItemList"></param>
        public static void CollectChangedItemList<T>(CheckedListBox.ObjectCollection checkedListBoxItems, List<T> selectedItemList, List<T> unselectedItemList)
        {
            foreach (CheckedItem checkedItem in checkedListBoxItems)
            {
                if (checkedItem.IsChanged)
                {
                    if (checkedItem.Value != null && checkedItem.Value != DBNull.Value)
                    {
                        if (checkedItem.CurrentChecked)
                        {
                            selectedItemList.Add((T)Convert.ChangeType(checkedItem.Value, typeof(T), CultureInfo.InvariantCulture));
                        }
                        else
                        {
                            unselectedItemList.Add((T)Convert.ChangeType(checkedItem.Value, typeof(T), CultureInfo.InvariantCulture));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Collect Changed Item List From CheckedListBox Control(Include Selected and Unselected)
        /// </summary>
        /// <param name="checkedListBoxItems"></param>
        /// <param name="selectedItemList"></param>
        /// <param name="unselectedItemList"></param>
        public static void CollectChangedItemList(CheckedListBox.ObjectCollection checkedListBoxItems, List<CheckedItem> selectedItemList, List<CheckedItem> unselectedItemList)
        {
            foreach (CheckedItem checkedItem in checkedListBoxItems)
            {
                if (checkedItem.IsChanged)
                {
                    if (checkedItem.CurrentChecked)
                    {
                        selectedItemList.Add(checkedItem);
                    }
                    else
                    {
                        unselectedItemList.Add(checkedItem);
                    }
                }
            }
        }

        /// <summary>
        /// Collect Selected Item List From CheckedListBox Control
        /// </summary>
        /// <param name="checkedListBoxItems"></param>
        /// <param name="selectedItemList"></param>
        public static void CollectSelectedItemList(CheckedListBox.ObjectCollection checkedListBoxItems, List<CheckedItem> selectedItemList)
        {
            foreach (CheckedItem checkedItem in checkedListBoxItems)
            {
                if (checkedItem.Value != null && checkedItem.Value != DBNull.Value)
                {
                    if (checkedItem.CurrentChecked)
                    {
                        selectedItemList.Add(checkedItem);
                    }
                }
            }
        }

        /// <summary>
        /// Collect Selected Item Value List From CheckedListBox Control
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="checkedListBoxItems"></param>
        /// <param name="selectedItemList"></param>
        public static void CollectSelectedItemList<T>(CheckedListBox.ObjectCollection checkedListBoxItems, List<T> selectedItemList)
        {
            foreach (CheckedItem checkedItem in checkedListBoxItems)
            {
                if (checkedItem.Value != null && checkedItem.Value != DBNull.Value)
                {
                    if (checkedItem.CurrentChecked)
                    {
                        selectedItemList.Add((T)Convert.ChangeType(checkedItem.Value, typeof(T), CultureInfo.InvariantCulture));
                    }
                }
            }
        }

        /// <summary>
        /// Collect Changed Item List From CheckedTreeView Control(Include Selected and Unselected)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="treeNodes"></param>
        /// <param name="selectedItemList"></param>
        /// <param name="unselectedItemList"></param>
        public static void CollectChangedItemList<T>(TreeNodeCollection treeNodes, List<T> selectedItemList, List<T> unselectedItemList)
        {
            if (treeNodes.Count > 0)
            {
                foreach (TreeNode treeNode in treeNodes)
                {
                    CheckedItem checkedItem = (CheckedItem)treeNode.Tag;

                    if (checkedItem.IsChanged)
                    {
                        if (checkedItem.Value != null && checkedItem.Value != DBNull.Value)
                        {
                            if (checkedItem.CurrentChecked)
                            {
                                selectedItemList.Add((T)Convert.ChangeType(checkedItem.Value, typeof(T), CultureInfo.InvariantCulture));
                            }
                            else
                            {
                                unselectedItemList.Add((T)Convert.ChangeType(checkedItem.Value, typeof(T), CultureInfo.InvariantCulture));
                            }
                        }
                    }

                    CollectChangedItemList<T>(treeNode.Nodes, selectedItemList, unselectedItemList);
                }
            }
        }

        /// <summary>
        /// Collect Selected Item List From CheckedTreeView Control
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="treeNodes"></param>
        /// <param name="selectedItemList"></param>
        public static void CollectSelectedItemList<T>(TreeNodeCollection treeNodes, List<T> selectedItemList)
        {
            if (treeNodes.Count > 0)
            {
                foreach (TreeNode treeNode in treeNodes)
                {
                    CheckedItem checkedItem = (CheckedItem)treeNode.Tag;

                    if (checkedItem.Value != null && checkedItem.Value != DBNull.Value)
                    {
                        if (checkedItem.CurrentChecked)
                        {
                            selectedItemList.Add((T)Convert.ChangeType(checkedItem.Value, typeof(T), CultureInfo.InvariantCulture));
                        }
                    }

                    CollectSelectedItemList<T>(treeNode.Nodes, selectedItemList);
                }
            }
        }

        /// <summary>
        /// Convert Bytes To Image
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static Image ConvertBytesToImage(byte[] bytes)
        {
            if (bytes != null && bytes.Length > 0)
            {
                MemoryStream ms = new MemoryStream(bytes);

                return Image.FromStream(ms);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Convert Image To Bytes
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static byte[] ConvertImageToBytes(Image image)
        {
            if (image != null)
            {
                MemoryStream ms = new MemoryStream();

                image.Save(ms, image.RawFormat);

                return ms.ToArray();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Compare specified array are the same content
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool CompareArray<T>(T[] a, T[] b)
        {
            if (a == null || b == null) return false;

            if (a.Length != b.Length) return false;

            for (int index = 0; index < a.Length; index++)
            {
                if (object.Equals(a[index], b[index]) == false) return false;
            }

            return true;
        }
    }

    /// <summary>
    /// Define Items Data Struct
    /// </summary>
    public struct ItemsData
    {
        public string DisplayMember;
        public string ValueMember;
        public object DataSource;

        public ItemsData(string displayMember, string valueMember, object dataSource)
        {
            this.DisplayMember = displayMember;
            this.ValueMember = valueMember;
            this.DataSource = dataSource;
        }
    }

    /// <summary>
    /// Define Checked Item
    /// </summary>
    public struct CheckedItem
    {
        public string Text;
        public object Value;
        public object Tag;
        public bool IsChanged;
        public bool CurrentChecked;
        public bool OriginalChecked;

        public CheckedItem(string text, object value, bool currentChecked)
        {
            this.Text = text;
            this.Value = value;
            this.Tag = null;
            this.IsChanged = false;
            this.CurrentChecked = currentChecked;
            this.OriginalChecked = currentChecked;
        }

        public CheckedItem(string text, object value, object tag, bool currentChecked)
        {
            this.Text = text;
            this.Value = value;
            this.Tag = tag;
            this.IsChanged = false;
            this.CurrentChecked = currentChecked;
            this.OriginalChecked = currentChecked;
        }

        public override string ToString()
        {
            return this.Text;
        }
    }

    /// <summary>
    /// Define Color Item
    /// </summary>
    public struct ColorItem
    {
        public string Text;
        public Color Color;

        public ColorItem(string text)
        {
            this.Text = text;
            this.Color = Color.Empty;
        }

        public ColorItem(string text, Color color)
        {
            this.Text = text;
            this.Color = color;
        }

        public override string ToString()
        {
            return this.Text;
        }
    }

}
