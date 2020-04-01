using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BaseMoudle;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;
using Utility;

namespace ViewModule.UI.Form
{
    public partial class EnvironmentDesignForm : BaseForm
    {
        public EnvironmentDesignForm()
        { 
            InitializeComponent();
            this.Text = "环境设计";
            DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            treeListColumn.Caption = "VanPeng.Desktop";
            treeListColumn.FieldName = "VanPeng.Desktop";
            treeListColumn.Name = "treeListColumn1";
            treeListColumn.Visible = true;
            treeListColumn.VisibleIndex = 0;

            ImageList imageList = new ImageList(); 
            var img1 = base.GetImg("BlueSquare"); 
            var img2 = base.GetImg("RedSquare"); ;
            imageList.Images.Add((Image)img1); 
            imageList.Images.Add((Image)img2);
            treeList1.SelectImageList = imageList;

            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] { treeListColumn });
            this.treeList1.OptionsView.ShowCheckBoxes = false;
            this.treeList1.OptionsBehavior.AllowIndeterminateCheckState = false;
            this.treeList1.OptionsBehavior.Editable = false;
            this.treeList1.Name = "treeList1";
            this.treeList1.BeginUnboundLoad();
            int level1 = 0;
            int level2 = 1;
            int currLevelCount = 0;
            foreach (var page in GlobalStatic.PageConfigList)
            { 
                var tlPage = this.treeList1.AppendNode(new object[] { page.Text }, -1);
                Console.WriteLine($"this.treeList1.AppendNode(new object[] {{ \"{ page.Text }\"}}, -1);");
                tlPage.ImageIndex = 0;
                tlPage.SelectImageIndex = 1;
                
                foreach (var pageGroup in page.PageGroupInfoList)
                { 
                    var tlGroup = this.treeList1.AppendNode(new object[] { pageGroup.Text }, level1);
                    Console.WriteLine($"this.treeList1.AppendNode(new object[] {{ \"{ pageGroup.Text }\"}}, {level1});");
                   
                    tlGroup.SelectImageIndex = 1; 
                    foreach (var barItem in pageGroup.BarItemInfoList)
                    {
                        currLevelCount++;  
                        var tlBarItem = this.treeList1.AppendNode(new object[] { barItem.Caption }, level2);
                        Console.WriteLine($"this.treeList1.AppendNode(new object[] {{ \"{ barItem.Caption }\"}}, {level2});");
                        if(!barItem.Image.IsNullOrEmpty())
                        {
                            var imgTemp = base.GetImg(barItem.Image);
                            imageList.Images.Add((Image)imgTemp);
                            tlBarItem.ImageIndex = imageList.Images.Count - 1; 
                        }
                        tlBarItem.SelectImageIndex = 1;
                    }
                    level2 = level2 +1 + currLevelCount;//当前级别+已经写入的node个数+1
                    Console.WriteLine($"level2={level2}");
                    currLevelCount = 0;
                }
                level1 = level2;
                level2++;
                Console.WriteLine($"level1={level1}");
            }


            //this.treeList1.AppendNode(new object[] { "root" }, -1).SelectImageIndex = 1;   
            //this.treeList1.AppendNode(new object[] { "成2" }, 0).SelectImageIndex = 1;
            //this.treeList1.AppendNode(new object[] { "32" }, 1).SelectImageIndex = 1;
            //this.treeList1.AppendNode(new object[] { "3232" }, 2).SelectImageIndex = 1;
            //this.treeList1.AppendNode(new object[] { "32" }, 0).SelectImageIndex = 1;
            //this.treeList1.AppendNode(new object[] { "32" }, -1).SelectImageIndex = 1;
            this.treeList1.EndUnboundLoad();
             
            this.treeList1.TabIndex = 0;
            treeList1.ExpandAll();
            //treeList1.AfterCheckNode += treeList1_AfterCheckNode;
            //treeList1.BeforeCheckNode += treeList1_BeforeCheckNode;
            //treeList1.NodeCellStyle += treeList1_NodeCellStyle;
            //treeList1.MouseMove += treeList1_MouseMove;
            //treeList1.DoubleClick += treeList1_DoubleClick;
            treeList1.Click += TreeList1_Click;
        }

        private void TreeList1_Click(object sender, EventArgs e)
        {
            TreeListNode clickedNode = this.treeList1.FocusedNode;
            string disPlayText = clickedNode.GetDisplayText(0);
            //MessageBox.Show("You clicked " + disPlayText);
        }
        #region TreeListAbandonEvent
        private void treeList1_DoubleClick(object sender, EventArgs e)
        {
            TreeListNode clickedNode = this.treeList1.FocusedNode;
            string disPlayText = clickedNode.GetDisplayText(0);
            MessageBox.Show("You clicked " + disPlayText);
        }
       
        private void treeList1_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            SetCheckedChildNodes(e.Node, e.Node.CheckState);
            SetCheckedParentNodes(e.Node, e.Node.CheckState);
        }
        private void treeList1_BeforeCheckNode(object sender, DevExpress.XtraTreeList.CheckNodeEventArgs e)
        {
            e.State = (e.PrevState == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked);
        }
        /// <summary>
        /// 设置子节点的状态
        /// </summary>
        /// <param name="node"></param>
        /// <param name="check"></param>
        private void SetCheckedChildNodes(TreeListNode node, CheckState check)
        {
            for (int i = 0; i < node.Nodes.Count; i++)
            {
                node.Nodes[i].CheckState = check;
                SetCheckedChildNodes(node.Nodes[i], check);
            }
        }
        /// <summary>
        /// 设置父节点的状态
        /// </summary>
        /// <param name="node"></param>
        /// <param name="check"></param>
        private void SetCheckedParentNodes(TreeListNode node, CheckState check)
        {
            if (node.ParentNode != null)
            {
                bool b = false;
                CheckState state;
                for (int i = 0; i < node.ParentNode.Nodes.Count; i++)
                {
                    state = (CheckState)node.ParentNode.Nodes[i].CheckState;
                    if (!check.Equals(state))
                    {
                        b = !b;
                        break;
                    }
                }
                node.ParentNode.CheckState = b ? CheckState.Indeterminate : check;
                SetCheckedParentNodes(node.ParentNode, check);
            }
        } 
        private void treeList1_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        { 
            if (e.Node.CheckState == CheckState.Unchecked)
            {
                //e.Appearance.Font = new Font(DevExpress.Utils.AppearanceObject.DefaultFont, FontStyle.Strikeout);
                e.Appearance.ForeColor = Color.Gray;
            }
        }

       
         
        private void treeList1_MouseMove(object sender, MouseEventArgs e)
        {
            Point point = treeList1.PointToClient(Cursor.Position);
            TreeListHitInfo hitInfo = treeList1.CalcHitInfo(point);
            switch (hitInfo.HitInfoType)
            {
                case HitInfoType.Cell:
                    this.Cursor = Cursors.Hand;
                    break;
                case HitInfoType.NodeCheckBox:
                    this.Cursor = Cursors.Hand;
                    break;
                default:
                    this.Cursor = Cursors.Default;
                    break;
            }
        }
        #endregion
    }
}