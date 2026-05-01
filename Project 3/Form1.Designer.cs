namespace CS3502_P3_FileSystem_JacksonDeal
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            mainLayout = new TableLayoutPanel();
            topPanel = new Panel();
            btnBack = new Button();
            btnGo = new Button();
            txtPath = new TextBox();
            lblPath = new Label();
            contentLayout = new TableLayoutPanel();
            grpContents = new GroupBox();
            lstContents = new ListBox();
            grpFileContent = new GroupBox();
            txtFileContent = new TextBox();
            grpActions = new GroupBox();
            actionButtonLayout = new TableLayoutPanel();
            btnCreate = new Button();
            btnRead = new Button();
            btnUpdate = new Button();
            btnRename = new Button();
            btnDelete = new Button();
            statusStrip = new StatusStrip();
            statusLabel = new ToolStripStatusLabel();
            statusSpring = new ToolStripStatusLabel();
            statusItems = new ToolStripStatusLabel();
            mainLayout.SuspendLayout();
            topPanel.SuspendLayout();
            contentLayout.SuspendLayout();
            grpContents.SuspendLayout();
            grpFileContent.SuspendLayout();
            grpActions.SuspendLayout();
            actionButtonLayout.SuspendLayout();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // mainLayout
            // 
            mainLayout.ColumnCount = 1;
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainLayout.Controls.Add(topPanel, 0, 0);
            mainLayout.Controls.Add(contentLayout, 0, 1);
            mainLayout.Controls.Add(grpActions, 0, 2);
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.Location = new Point(0, 0);
            mainLayout.Name = "mainLayout";
            mainLayout.RowCount = 3;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
            mainLayout.Size = new Size(800, 578);
            mainLayout.TabIndex = 0;
            // 
            // topPanel
            // 
            topPanel.Controls.Add(btnBack);
            topPanel.Controls.Add(btnGo);
            topPanel.Controls.Add(txtPath);
            topPanel.Controls.Add(lblPath);
            topPanel.Dock = DockStyle.Fill;
            topPanel.Location = new Point(3, 3);
            topPanel.Name = "topPanel";
            topPanel.Size = new Size(794, 54);
            topPanel.TabIndex = 0;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(12, 15);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(40, 28);
            btnBack.TabIndex = 3;
            btnBack.Text = "←";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // btnGo
            // 
            btnGo.Anchor = AnchorStyles.Right;
            btnGo.Location = new Point(707, 15);
            btnGo.Name = "btnGo";
            btnGo.Size = new Size(75, 28);
            btnGo.TabIndex = 0;
            btnGo.Text = "Go";
            // 
            // txtPath
            // 
            txtPath.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtPath.Location = new Point(145, 18);
            txtPath.Name = "txtPath";
            txtPath.Size = new Size(545, 23);
            txtPath.TabIndex = 1;
            // 
            // lblPath
            // 
            lblPath.AutoSize = true;
            lblPath.Location = new Point(58, 21);
            lblPath.Name = "lblPath";
            lblPath.Size = new Size(77, 15);
            lblPath.TabIndex = 2;
            lblPath.Text = "Current Path:";
            // 
            // contentLayout
            // 
            contentLayout.ColumnCount = 2;
            contentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            contentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            contentLayout.Controls.Add(grpContents, 0, 0);
            contentLayout.Controls.Add(grpFileContent, 1, 0);
            contentLayout.Dock = DockStyle.Fill;
            contentLayout.Location = new Point(3, 63);
            contentLayout.Name = "contentLayout";
            contentLayout.RowCount = 1;
            contentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            contentLayout.Size = new Size(794, 412);
            contentLayout.TabIndex = 1;
            // 
            // grpContents
            // 
            grpContents.Controls.Add(lstContents);
            grpContents.Dock = DockStyle.Fill;
            grpContents.Location = new Point(3, 3);
            grpContents.Name = "grpContents";
            grpContents.Size = new Size(311, 406);
            grpContents.TabIndex = 0;
            grpContents.TabStop = false;
            grpContents.Text = "Contents";
            // 
            // lstContents
            // 
            lstContents.Dock = DockStyle.Fill;
            lstContents.ItemHeight = 15;
            lstContents.Location = new Point(3, 19);
            lstContents.Name = "lstContents";
            lstContents.Size = new Size(305, 384);
            lstContents.TabIndex = 0;
            lstContents.MouseDoubleClick += lstContents_MouseDoubleClick;
            // 
            // grpFileContent
            // 
            grpFileContent.Controls.Add(txtFileContent);
            grpFileContent.Dock = DockStyle.Fill;
            grpFileContent.Location = new Point(320, 3);
            grpFileContent.Name = "grpFileContent";
            grpFileContent.Size = new Size(471, 406);
            grpFileContent.TabIndex = 1;
            grpFileContent.TabStop = false;
            grpFileContent.Text = "File Content Area";
            // 
            // txtFileContent
            // 
            txtFileContent.Dock = DockStyle.Fill;
            txtFileContent.Location = new Point(3, 19);
            txtFileContent.Multiline = true;
            txtFileContent.Name = "txtFileContent";
            txtFileContent.ScrollBars = ScrollBars.Vertical;
            txtFileContent.Size = new Size(465, 384);
            txtFileContent.TabIndex = 0;
            // 
            // grpActions
            // 
            grpActions.Controls.Add(actionButtonLayout);
            grpActions.Dock = DockStyle.Fill;
            grpActions.Location = new Point(3, 481);
            grpActions.Name = "grpActions";
            grpActions.Size = new Size(794, 94);
            grpActions.TabIndex = 2;
            grpActions.TabStop = false;
            grpActions.Text = "Action Buttons";
            // 
            // actionButtonLayout
            // 
            actionButtonLayout.ColumnCount = 5;
            actionButtonLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            actionButtonLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            actionButtonLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            actionButtonLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            actionButtonLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            actionButtonLayout.Controls.Add(btnCreate, 0, 0);
            actionButtonLayout.Controls.Add(btnRead, 1, 0);
            actionButtonLayout.Controls.Add(btnUpdate, 2, 0);
            actionButtonLayout.Controls.Add(btnRename, 3, 0);
            actionButtonLayout.Controls.Add(btnDelete, 4, 0);
            actionButtonLayout.Dock = DockStyle.Fill;
            actionButtonLayout.Location = new Point(3, 19);
            actionButtonLayout.Name = "actionButtonLayout";
            actionButtonLayout.RowCount = 1;
            actionButtonLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            actionButtonLayout.Size = new Size(788, 72);
            actionButtonLayout.TabIndex = 0;
            // 
            // btnCreate
            // 
            btnCreate.Dock = DockStyle.Fill;
            btnCreate.Location = new Point(3, 3);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(151, 66);
            btnCreate.TabIndex = 0;
            btnCreate.Text = "Create File/Folder";
            btnCreate.Click += btnCreate_Click;
            // 
            // btnRead
            // 
            btnRead.Dock = DockStyle.Fill;
            btnRead.Location = new Point(160, 3);
            btnRead.Name = "btnRead";
            btnRead.Size = new Size(151, 66);
            btnRead.TabIndex = 1;
            btnRead.Text = "Read";
            btnRead.Click += btnRead_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Dock = DockStyle.Fill;
            btnUpdate.Location = new Point(317, 3);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(151, 66);
            btnUpdate.TabIndex = 2;
            btnUpdate.Text = "Update/Save";
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnRename
            // 
            btnRename.Dock = DockStyle.Fill;
            btnRename.Location = new Point(474, 3);
            btnRename.Name = "btnRename";
            btnRename.Size = new Size(151, 66);
            btnRename.TabIndex = 3;
            btnRename.Text = "Rename";
            btnRename.Click += btnRename_Click;
            // 
            // btnDelete
            // 
            btnDelete.Dock = DockStyle.Fill;
            btnDelete.Location = new Point(631, 3);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(154, 66);
            btnDelete.TabIndex = 4;
            btnDelete.Text = "Delete";
            btnDelete.Click += btnDelete_Click;
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { statusLabel, statusSpring, statusItems });
            statusStrip.Location = new Point(0, 578);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(800, 22);
            statusStrip.TabIndex = 1;
            // 
            // statusLabel
            // 
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(130, 17);
            statusLabel.Text = "● Current Status: Ready";
            // 
            // statusSpring
            // 
            statusSpring.Name = "statusSpring";
            statusSpring.Size = new Size(610, 17);
            statusSpring.Spring = true;
            statusSpring.Text = "Status Bar";
            // 
            // statusItems
            // 
            statusItems.Name = "statusItems";
            statusItems.Size = new Size(45, 17);
            statusItems.Text = "6 Items";
            // 
            // Form1
            // 
            ClientSize = new Size(800, 600);
            Controls.Add(mainLayout);
            Controls.Add(statusStrip);
            Name = "Form1";
            Text = "CS 3502: File System Implementation-1";
            mainLayout.ResumeLayout(false);
            topPanel.ResumeLayout(false);
            topPanel.PerformLayout();
            contentLayout.ResumeLayout(false);
            grpContents.ResumeLayout(false);
            grpFileContent.ResumeLayout(false);
            grpFileContent.PerformLayout();
            grpActions.ResumeLayout(false);
            actionButtonLayout.ResumeLayout(false);
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainLayout;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.TableLayoutPanel contentLayout;
        private System.Windows.Forms.GroupBox grpContents;
        private System.Windows.Forms.ListBox lstContents;
        private System.Windows.Forms.GroupBox grpFileContent;
        private System.Windows.Forms.TextBox txtFileContent;
        private System.Windows.Forms.GroupBox grpActions;
        private System.Windows.Forms.TableLayoutPanel actionButtonLayout;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnRename;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripStatusLabel statusSpring;
        private System.Windows.Forms.ToolStripStatusLabel statusItems;
    }
}