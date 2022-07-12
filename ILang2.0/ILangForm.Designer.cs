/*
 * Created by SharpDevelop.
 * User: alejandro.rangel
 * Date: 10/12/2019
 * Time: 10:24 a. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ILang2._
{
	partial class ILangForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ILangForm));
			this.KnlEditor = new System.Windows.Forms.GroupBox();
			this.RelationsLister = new System.Windows.Forms.ListBox();
			this.ReferenceModeCheckbox = new System.Windows.Forms.CheckBox();
			this.IlangFileTypeSelector = new System.Windows.Forms.ComboBox();
			this.SaveClipboardDataButton = new System.Windows.Forms.Button();
			this.brTst = new System.Windows.Forms.Button();
			this.TestButton = new System.Windows.Forms.Button();
			this.SaveLight = new System.Windows.Forms.Label();
			this.CopyKNLButton = new System.Windows.Forms.Button();
			this.newKnlButton = new System.Windows.Forms.Button();
			this.RemoveSection = new System.Windows.Forms.Button();
			this.RenameSection = new System.Windows.Forms.Button();
			this.NewSection = new System.Windows.Forms.Button();
			this.SaveSection = new System.Windows.Forms.Button();
			this.SectionLister = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.SaveKnlBtn = new System.Windows.Forms.Button();
			this.IlangLabel = new System.Windows.Forms.Label();
			this.FileNameTextbox = new System.Windows.Forms.TextBox();
			this.SectionsTextbox = new System.Windows.Forms.TextBox();
			this.IlangTextbox = new System.Windows.Forms.TextBox();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.funcionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.runILangToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.runSomethingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.sectionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.transpileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.insertBase64ImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.boxesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.varsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.metadataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.contextsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.createNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.KnlFileList = new System.Windows.Forms.ListBox();
			this.knlFilterText = new System.Windows.Forms.TextBox();
			this.FilteredKnlListBox = new System.Windows.Forms.ListBox();
			this.ILangWebBrowser = new System.Windows.Forms.WebBrowser();
			this.VarsBox = new System.Windows.Forms.GroupBox();
			this.NewVarButton = new System.Windows.Forms.Button();
			this.varsListBox = new System.Windows.Forms.ListBox();
			this.KnlFolderSelector = new System.Windows.Forms.ComboBox();
			this.MetadataBox = new System.Windows.Forms.GroupBox();
			this.KnlIdLabel = new System.Windows.Forms.Label();
			this.MetadataListbox = new System.Windows.Forms.ListBox();
			this.MetadataTextbox = new System.Windows.Forms.TextBox();
			this.KnlEditor.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.VarsBox.SuspendLayout();
			this.MetadataBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// KnlEditor
			// 
			this.KnlEditor.Controls.Add(this.RelationsLister);
			this.KnlEditor.Controls.Add(this.ReferenceModeCheckbox);
			this.KnlEditor.Controls.Add(this.IlangFileTypeSelector);
			this.KnlEditor.Controls.Add(this.SaveClipboardDataButton);
			this.KnlEditor.Controls.Add(this.brTst);
			this.KnlEditor.Controls.Add(this.TestButton);
			this.KnlEditor.Controls.Add(this.SaveLight);
			this.KnlEditor.Controls.Add(this.CopyKNLButton);
			this.KnlEditor.Controls.Add(this.newKnlButton);
			this.KnlEditor.Controls.Add(this.RemoveSection);
			this.KnlEditor.Controls.Add(this.RenameSection);
			this.KnlEditor.Controls.Add(this.NewSection);
			this.KnlEditor.Controls.Add(this.SaveSection);
			this.KnlEditor.Controls.Add(this.SectionLister);
			this.KnlEditor.Controls.Add(this.label1);
			this.KnlEditor.Controls.Add(this.SaveKnlBtn);
			this.KnlEditor.Controls.Add(this.IlangLabel);
			this.KnlEditor.Controls.Add(this.FileNameTextbox);
			this.KnlEditor.Controls.Add(this.SectionsTextbox);
			this.KnlEditor.Controls.Add(this.IlangTextbox);
			this.KnlEditor.Location = new System.Drawing.Point(1, 27);
			this.KnlEditor.Name = "KnlEditor";
			this.KnlEditor.Size = new System.Drawing.Size(645, 571);
			this.KnlEditor.TabIndex = 0;
			this.KnlEditor.TabStop = false;
			this.KnlEditor.Text = "Knl Editor";
			this.KnlEditor.Visible = false;
			// 
			// RelationsLister
			// 
			this.RelationsLister.FormattingEnabled = true;
			this.RelationsLister.Location = new System.Drawing.Point(317, 200);
			this.RelationsLister.Margin = new System.Windows.Forms.Padding(2);
			this.RelationsLister.Name = "RelationsLister";
			this.RelationsLister.Size = new System.Drawing.Size(323, 355);
			this.RelationsLister.TabIndex = 21;
			this.RelationsLister.TabStop = false;
			this.RelationsLister.Visible = false;
			this.RelationsLister.SelectedIndexChanged += new System.EventHandler(this.RelationsListerSelectedIndexChanged);
			this.RelationsLister.DoubleClick += new System.EventHandler(this.RelationsListerDoubleClick);
			this.RelationsLister.KeyUp += new System.Windows.Forms.KeyEventHandler(this.RelationsListerKeyUp);
			// 
			// ReferenceModeCheckbox
			// 
			this.ReferenceModeCheckbox.Location = new System.Drawing.Point(554, 44);
			this.ReferenceModeCheckbox.Margin = new System.Windows.Forms.Padding(2);
			this.ReferenceModeCheckbox.Name = "ReferenceModeCheckbox";
			this.ReferenceModeCheckbox.Size = new System.Drawing.Size(83, 18);
			this.ReferenceModeCheckbox.TabIndex = 20;
			this.ReferenceModeCheckbox.Text = "Reference";
			this.ReferenceModeCheckbox.UseVisualStyleBackColor = true;
			this.ReferenceModeCheckbox.CheckedChanged += new System.EventHandler(this.ReferenceModeCheckboxCheckedChanged);
			// 
			// IlangFileTypeSelector
			// 
			this.IlangFileTypeSelector.FormattingEnabled = true;
			this.IlangFileTypeSelector.Location = new System.Drawing.Point(411, 44);
			this.IlangFileTypeSelector.Margin = new System.Windows.Forms.Padding(2);
			this.IlangFileTypeSelector.Name = "IlangFileTypeSelector";
			this.IlangFileTypeSelector.Size = new System.Drawing.Size(136, 21);
			this.IlangFileTypeSelector.TabIndex = 19;
			this.IlangFileTypeSelector.Text = "Ilang File Type";
			this.IlangFileTypeSelector.SelectedIndexChanged += new System.EventHandler(this.IlangFileTypeSelectorSelectedIndexChanged);
			// 
			// SaveClipboardDataButton
			// 
			this.SaveClipboardDataButton.Location = new System.Drawing.Point(581, 174);
			this.SaveClipboardDataButton.Name = "SaveClipboardDataButton";
			this.SaveClipboardDataButton.Size = new System.Drawing.Size(56, 20);
			this.SaveClipboardDataButton.TabIndex = 18;
			this.SaveClipboardDataButton.Text = "Clpbrd";
			this.SaveClipboardDataButton.UseVisualStyleBackColor = true;
			this.SaveClipboardDataButton.Click += new System.EventHandler(this.SaveClipboardDataButtonClick);
			// 
			// brTst
			// 
			this.brTst.Location = new System.Drawing.Point(552, 19);
			this.brTst.Name = "brTst";
			this.brTst.Size = new System.Drawing.Size(31, 20);
			this.brTst.TabIndex = 17;
			this.brTst.Text = "button1";
			this.brTst.UseVisualStyleBackColor = true;
			this.brTst.Visible = false;
			this.brTst.Click += new System.EventHandler(this.BrTstClick);
			// 
			// TestButton
			// 
			this.TestButton.Location = new System.Drawing.Point(551, 17);
			this.TestButton.Name = "TestButton";
			this.TestButton.Size = new System.Drawing.Size(32, 19);
			this.TestButton.TabIndex = 16;
			this.TestButton.Text = "tst";
			this.TestButton.UseVisualStyleBackColor = true;
			this.TestButton.Visible = false;
			this.TestButton.Click += new System.EventHandler(this.TestButtonClick);
			// 
			// SaveLight
			// 
			this.SaveLight.BackColor = System.Drawing.Color.Lime;
			this.SaveLight.Location = new System.Drawing.Point(623, 8);
			this.SaveLight.Name = "SaveLight";
			this.SaveLight.Size = new System.Drawing.Size(16, 16);
			this.SaveLight.TabIndex = 15;
			// 
			// CopyKNLButton
			// 
			this.CopyKNLButton.Location = new System.Drawing.Point(501, 19);
			this.CopyKNLButton.Name = "CopyKNLButton";
			this.CopyKNLButton.Size = new System.Drawing.Size(44, 20);
			this.CopyKNLButton.TabIndex = 14;
			this.CopyKNLButton.Text = "Copy";
			this.CopyKNLButton.UseVisualStyleBackColor = true;
			this.CopyKNLButton.Click += new System.EventHandler(this.CopyKNLButtonClick);
			// 
			// newKnlButton
			// 
			this.newKnlButton.Location = new System.Drawing.Point(455, 19);
			this.newKnlButton.Name = "newKnlButton";
			this.newKnlButton.Size = new System.Drawing.Size(42, 20);
			this.newKnlButton.TabIndex = 13;
			this.newKnlButton.Text = "New";
			this.newKnlButton.UseVisualStyleBackColor = true;
			this.newKnlButton.Click += new System.EventHandler(this.Button1Click);
			// 
			// RemoveSection
			// 
			this.RemoveSection.Enabled = false;
			this.RemoveSection.Location = new System.Drawing.Point(581, 148);
			this.RemoveSection.Name = "RemoveSection";
			this.RemoveSection.Size = new System.Drawing.Size(56, 20);
			this.RemoveSection.TabIndex = 12;
			this.RemoveSection.Text = "Remove";
			this.RemoveSection.UseVisualStyleBackColor = true;
			this.RemoveSection.Click += new System.EventHandler(this.RemoveSectionClick);
			// 
			// RenameSection
			// 
			this.RenameSection.Enabled = false;
			this.RenameSection.Location = new System.Drawing.Point(581, 122);
			this.RenameSection.Name = "RenameSection";
			this.RenameSection.Size = new System.Drawing.Size(56, 20);
			this.RenameSection.TabIndex = 11;
			this.RenameSection.Text = "Rename";
			this.RenameSection.UseVisualStyleBackColor = true;
			this.RenameSection.Click += new System.EventHandler(this.RenameSectionClick);
			// 
			// NewSection
			// 
			this.NewSection.Location = new System.Drawing.Point(581, 70);
			this.NewSection.Name = "NewSection";
			this.NewSection.Size = new System.Drawing.Size(56, 20);
			this.NewSection.TabIndex = 10;
			this.NewSection.Text = "New";
			this.NewSection.UseVisualStyleBackColor = true;
			this.NewSection.Click += new System.EventHandler(this.NewSectionClick);
			// 
			// SaveSection
			// 
			this.SaveSection.Enabled = false;
			this.SaveSection.Location = new System.Drawing.Point(581, 96);
			this.SaveSection.Name = "SaveSection";
			this.SaveSection.Size = new System.Drawing.Size(56, 20);
			this.SaveSection.TabIndex = 9;
			this.SaveSection.Text = "Save";
			this.SaveSection.UseVisualStyleBackColor = true;
			this.SaveSection.Click += new System.EventHandler(this.SaveSectionClick);
			// 
			// SectionLister
			// 
			this.SectionLister.AllowDrop = true;
			this.SectionLister.FormattingEnabled = true;
			this.SectionLister.Location = new System.Drawing.Point(315, 70);
			this.SectionLister.Name = "SectionLister";
			this.SectionLister.Size = new System.Drawing.Size(260, 121);
			this.SectionLister.TabIndex = 8;
			this.SectionLister.SelectedIndexChanged += new System.EventHandler(this.SectionListerSelectedIndexChanged);
			this.SectionLister.DragDrop += new System.Windows.Forms.DragEventHandler(this.SectionListerDragDrop);
			this.SectionLister.DragEnter += new System.Windows.Forms.DragEventHandler(this.SectionListerDragEnter);
			this.SectionLister.DoubleClick += new System.EventHandler(this.SectionListerDoubleClick);
			this.SectionLister.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SectionListerKeyUp);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(315, 50);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(105, 13);
			this.label1.TabIndex = 7;
			this.label1.Text = "Sections";
			// 
			// SaveKnlBtn
			// 
			this.SaveKnlBtn.Enabled = false;
			this.SaveKnlBtn.Location = new System.Drawing.Point(411, 19);
			this.SaveKnlBtn.Name = "SaveKnlBtn";
			this.SaveKnlBtn.Size = new System.Drawing.Size(43, 20);
			this.SaveKnlBtn.TabIndex = 6;
			this.SaveKnlBtn.Text = "Save";
			this.SaveKnlBtn.UseVisualStyleBackColor = true;
			this.SaveKnlBtn.Click += new System.EventHandler(this.SaveKnlBtnClick);
			// 
			// IlangLabel
			// 
			this.IlangLabel.Location = new System.Drawing.Point(10, 50);
			this.IlangLabel.Name = "IlangLabel";
			this.IlangLabel.Size = new System.Drawing.Size(116, 17);
			this.IlangLabel.TabIndex = 4;
			this.IlangLabel.Text = "ILang procedures";
			// 
			// FileNameTextbox
			// 
			this.FileNameTextbox.Location = new System.Drawing.Point(11, 19);
			this.FileNameTextbox.Name = "FileNameTextbox";
			this.FileNameTextbox.Size = new System.Drawing.Size(394, 20);
			this.FileNameTextbox.TabIndex = 3;
			this.FileNameTextbox.Click += new System.EventHandler(this.FileNameTextboxClick);
			this.FileNameTextbox.TextChanged += new System.EventHandler(this.FileNameTextboxTextChanged);
			// 
			// SectionsTextbox
			// 
			this.SectionsTextbox.Location = new System.Drawing.Point(315, 200);
			this.SectionsTextbox.MaxLength = 128000;
			this.SectionsTextbox.Multiline = true;
			this.SectionsTextbox.Name = "SectionsTextbox";
			this.SectionsTextbox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.SectionsTextbox.Size = new System.Drawing.Size(324, 358);
			this.SectionsTextbox.TabIndex = 2;
			this.SectionsTextbox.TabStop = false;
			this.SectionsTextbox.WordWrap = false;
			this.SectionsTextbox.Click += new System.EventHandler(this.SectionsTextboxClick);
			this.SectionsTextbox.TextChanged += new System.EventHandler(this.SectionsTextboxTextChanged);
			this.SectionsTextbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SectionsTextboxKeyPress);
			this.SectionsTextbox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SectionsTextboxKeyUp);
			// 
			// IlangTextbox
			// 
			this.IlangTextbox.Location = new System.Drawing.Point(10, 70);
			this.IlangTextbox.MaxLength = 128000;
			this.IlangTextbox.Multiline = true;
			this.IlangTextbox.Name = "IlangTextbox";
			this.IlangTextbox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.IlangTextbox.Size = new System.Drawing.Size(290, 488);
			this.IlangTextbox.TabIndex = 0;
			this.IlangTextbox.WordWrap = false;
			this.IlangTextbox.Click += new System.EventHandler(this.IlangTextboxClick);
			this.IlangTextbox.TextChanged += new System.EventHandler(this.IlangTextboxTextChanged);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.funcionesToolStripMenuItem,
									this.sectionsToolStripMenuItem,
									this.boxesToolStripMenuItem,
									this.contextsToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(967, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.MenuStrip1ItemClicked);
			// 
			// funcionesToolStripMenuItem
			// 
			this.funcionesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.editorToolStripMenuItem,
									this.runILangToolStripMenuItem,
									this.runSomethingToolStripMenuItem});
			this.funcionesToolStripMenuItem.Name = "funcionesToolStripMenuItem";
			this.funcionesToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
			this.funcionesToolStripMenuItem.Text = "Functions";
			this.funcionesToolStripMenuItem.Click += new System.EventHandler(this.FuncionesToolStripMenuItemClick);
			// 
			// editorToolStripMenuItem
			// 
			this.editorToolStripMenuItem.Name = "editorToolStripMenuItem";
			this.editorToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
			this.editorToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
			this.editorToolStripMenuItem.Text = "Editor";
			this.editorToolStripMenuItem.Click += new System.EventHandler(this.EditorToolStripMenuItemClick);
			// 
			// runILangToolStripMenuItem
			// 
			this.runILangToolStripMenuItem.Name = "runILangToolStripMenuItem";
			this.runILangToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
			this.runILangToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
			this.runILangToolStripMenuItem.Text = "Run ILang";
			this.runILangToolStripMenuItem.Click += new System.EventHandler(this.RunILangToolStripMenuItemClick);
			// 
			// runSomethingToolStripMenuItem
			// 
			this.runSomethingToolStripMenuItem.Name = "runSomethingToolStripMenuItem";
			this.runSomethingToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
			this.runSomethingToolStripMenuItem.Text = "Run Something";
			this.runSomethingToolStripMenuItem.Click += new System.EventHandler(this.RunSomethingToolStripMenuItemClick);
			// 
			// sectionsToolStripMenuItem
			// 
			this.sectionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.renameToolStripMenuItem,
									this.transpileToolStripMenuItem,
									this.insertBase64ImageToolStripMenuItem});
			this.sectionsToolStripMenuItem.Name = "sectionsToolStripMenuItem";
			this.sectionsToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
			this.sectionsToolStripMenuItem.Text = "Sections";
			// 
			// renameToolStripMenuItem
			// 
			this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
			this.renameToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F2)));
			this.renameToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
			this.renameToolStripMenuItem.Text = "Rename";
			this.renameToolStripMenuItem.Click += new System.EventHandler(this.RenameToolStripMenuItemClick);
			// 
			// transpileToolStripMenuItem
			// 
			this.transpileToolStripMenuItem.Name = "transpileToolStripMenuItem";
			this.transpileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.T)));
			this.transpileToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
			this.transpileToolStripMenuItem.Text = "Transpile";
			this.transpileToolStripMenuItem.Click += new System.EventHandler(this.TranspileToolStripMenuItemClick);
			// 
			// insertBase64ImageToolStripMenuItem
			// 
			this.insertBase64ImageToolStripMenuItem.Name = "insertBase64ImageToolStripMenuItem";
			this.insertBase64ImageToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
			this.insertBase64ImageToolStripMenuItem.Text = "Insert base64 image";
			this.insertBase64ImageToolStripMenuItem.Click += new System.EventHandler(this.InsertBase64ImageToolStripMenuItemClick);
			// 
			// boxesToolStripMenuItem
			// 
			this.boxesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.varsToolStripMenuItem,
									this.metadataToolStripMenuItem});
			this.boxesToolStripMenuItem.Name = "boxesToolStripMenuItem";
			this.boxesToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
			this.boxesToolStripMenuItem.Text = "Boxes";
			// 
			// varsToolStripMenuItem
			// 
			this.varsToolStripMenuItem.Name = "varsToolStripMenuItem";
			this.varsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
			this.varsToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
			this.varsToolStripMenuItem.Text = "Vars";
			this.varsToolStripMenuItem.Click += new System.EventHandler(this.VarsToolStripMenuItemClick);
			// 
			// metadataToolStripMenuItem
			// 
			this.metadataToolStripMenuItem.Name = "metadataToolStripMenuItem";
			this.metadataToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
			this.metadataToolStripMenuItem.Text = "Metadata";
			this.metadataToolStripMenuItem.Click += new System.EventHandler(this.MetadataToolStripMenuItemClick);
			// 
			// contextsToolStripMenuItem
			// 
			this.contextsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.createNewToolStripMenuItem});
			this.contextsToolStripMenuItem.Name = "contextsToolStripMenuItem";
			this.contextsToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
			this.contextsToolStripMenuItem.Text = "Contexts";
			// 
			// createNewToolStripMenuItem
			// 
			this.createNewToolStripMenuItem.Name = "createNewToolStripMenuItem";
			this.createNewToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
			this.createNewToolStripMenuItem.Text = "Create new";
			this.createNewToolStripMenuItem.Click += new System.EventHandler(this.CreateNewToolStripMenuItemClick);
			// 
			// KnlFileList
			// 
			this.KnlFileList.FormattingEnabled = true;
			this.KnlFileList.Location = new System.Drawing.Point(652, 86);
			this.KnlFileList.Name = "KnlFileList";
			this.KnlFileList.Size = new System.Drawing.Size(303, 498);
			this.KnlFileList.TabIndex = 2;
			this.KnlFileList.SelectedIndexChanged += new System.EventHandler(this.ListBox1SelectedIndexChanged);
			this.KnlFileList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.KnlFileListMouseDoubleClick);
			// 
			// knlFilterText
			// 
			this.knlFilterText.Location = new System.Drawing.Point(652, 60);
			this.knlFilterText.Name = "knlFilterText";
			this.knlFilterText.Size = new System.Drawing.Size(302, 20);
			this.knlFilterText.TabIndex = 3;
			this.knlFilterText.TextChanged += new System.EventHandler(this.KnlFilterTextTextChanged);
			// 
			// FilteredKnlListBox
			// 
			this.FilteredKnlListBox.FormattingEnabled = true;
			this.FilteredKnlListBox.Location = new System.Drawing.Point(652, 86);
			this.FilteredKnlListBox.Name = "FilteredKnlListBox";
			this.FilteredKnlListBox.Size = new System.Drawing.Size(302, 498);
			this.FilteredKnlListBox.TabIndex = 4;
			this.FilteredKnlListBox.Visible = false;
			this.FilteredKnlListBox.SelectedIndexChanged += new System.EventHandler(this.FilteredKnlListBoxSelectedIndexChanged);
			this.FilteredKnlListBox.DoubleClick += new System.EventHandler(this.FilteredKnlListBoxDoubleClick);
			// 
			// ILangWebBrowser
			// 
			this.ILangWebBrowser.Location = new System.Drawing.Point(0, 27);
			this.ILangWebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
			this.ILangWebBrowser.Name = "ILangWebBrowser";
			this.ILangWebBrowser.Size = new System.Drawing.Size(646, 557);
			this.ILangWebBrowser.TabIndex = 18;
			// 
			// VarsBox
			// 
			this.VarsBox.Controls.Add(this.NewVarButton);
			this.VarsBox.Controls.Add(this.varsListBox);
			this.VarsBox.Location = new System.Drawing.Point(794, 60);
			this.VarsBox.Name = "VarsBox";
			this.VarsBox.Size = new System.Drawing.Size(161, 284);
			this.VarsBox.TabIndex = 19;
			this.VarsBox.TabStop = false;
			this.VarsBox.Text = "Vars";
			this.VarsBox.Visible = false;
			// 
			// NewVarButton
			// 
			this.NewVarButton.Location = new System.Drawing.Point(6, 16);
			this.NewVarButton.Name = "NewVarButton";
			this.NewVarButton.Size = new System.Drawing.Size(64, 23);
			this.NewVarButton.TabIndex = 1;
			this.NewVarButton.Text = "NewVar";
			this.NewVarButton.UseVisualStyleBackColor = true;
			this.NewVarButton.Click += new System.EventHandler(this.NewVarButtonClick);
			// 
			// varsListBox
			// 
			this.varsListBox.FormattingEnabled = true;
			this.varsListBox.Location = new System.Drawing.Point(6, 45);
			this.varsListBox.Name = "varsListBox";
			this.varsListBox.Size = new System.Drawing.Size(148, 225);
			this.varsListBox.TabIndex = 0;
			this.varsListBox.DoubleClick += new System.EventHandler(this.VarsListBoxDoubleClick);
			// 
			// KnlFolderSelector
			// 
			this.KnlFolderSelector.FormattingEnabled = true;
			this.KnlFolderSelector.Location = new System.Drawing.Point(652, 35);
			this.KnlFolderSelector.Name = "KnlFolderSelector";
			this.KnlFolderSelector.Size = new System.Drawing.Size(303, 21);
			this.KnlFolderSelector.TabIndex = 20;
			this.KnlFolderSelector.Text = "Knl Folder";
			this.KnlFolderSelector.SelectedIndexChanged += new System.EventHandler(this.KnlFolderSelectorSelectedIndexChanged);
			// 
			// MetadataBox
			// 
			this.MetadataBox.Controls.Add(this.KnlIdLabel);
			this.MetadataBox.Controls.Add(this.MetadataListbox);
			this.MetadataBox.Controls.Add(this.MetadataTextbox);
			this.MetadataBox.Location = new System.Drawing.Point(646, 58);
			this.MetadataBox.Name = "MetadataBox";
			this.MetadataBox.Size = new System.Drawing.Size(148, 286);
			this.MetadataBox.TabIndex = 21;
			this.MetadataBox.TabStop = false;
			this.MetadataBox.Text = "Metadata";
			this.MetadataBox.Visible = false;
			// 
			// KnlIdLabel
			// 
			this.KnlIdLabel.Location = new System.Drawing.Point(6, 22);
			this.KnlIdLabel.Name = "KnlIdLabel";
			this.KnlIdLabel.Size = new System.Drawing.Size(142, 19);
			this.KnlIdLabel.TabIndex = 2;
			this.KnlIdLabel.Text = "KnlId: ";
			// 
			// MetadataListbox
			// 
			this.MetadataListbox.FormattingEnabled = true;
			this.MetadataListbox.Location = new System.Drawing.Point(6, 73);
			this.MetadataListbox.Name = "MetadataListbox";
			this.MetadataListbox.Size = new System.Drawing.Size(142, 199);
			this.MetadataListbox.TabIndex = 1;
			// 
			// MetadataTextbox
			// 
			this.MetadataTextbox.Location = new System.Drawing.Point(6, 47);
			this.MetadataTextbox.Name = "MetadataTextbox";
			this.MetadataTextbox.Size = new System.Drawing.Size(142, 20);
			this.MetadataTextbox.TabIndex = 0;
			this.MetadataTextbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MetadataTextboxKeyPress);
			// 
			// ILangForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(967, 594);
			this.Controls.Add(this.MetadataBox);
			this.Controls.Add(this.KnlFolderSelector);
			this.Controls.Add(this.VarsBox);
			this.Controls.Add(this.FilteredKnlListBox);
			this.Controls.Add(this.knlFilterText);
			this.Controls.Add(this.KnlEditor);
			this.Controls.Add(this.menuStrip1);
			this.Controls.Add(this.KnlFileList);
			this.Controls.Add(this.ILangWebBrowser);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "ILangForm";
			this.Text = "ILangForm";
			this.Load += new System.EventHandler(this.ILangFormLoad);
			this.KnlEditor.ResumeLayout(false);
			this.KnlEditor.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.VarsBox.ResumeLayout(false);
			this.MetadataBox.ResumeLayout(false);
			this.MetadataBox.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.ToolStripMenuItem insertBase64ImageToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem transpileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem createNewToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem contextsToolStripMenuItem;
		private System.Windows.Forms.ListBox RelationsLister;
		private System.Windows.Forms.CheckBox ReferenceModeCheckbox;
		private System.Windows.Forms.ComboBox IlangFileTypeSelector;
		private System.Windows.Forms.ToolStripMenuItem metadataToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem runSomethingToolStripMenuItem;
		private System.Windows.Forms.ListBox varsListBox;
		private System.Windows.Forms.Button NewVarButton;
		private System.Windows.Forms.GroupBox VarsBox;
		private System.Windows.Forms.WebBrowser ILangWebBrowser;
		private System.Windows.Forms.Button brTst;
		private System.Windows.Forms.ToolStripMenuItem runILangToolStripMenuItem;
		private System.Windows.Forms.Button RemoveSection;
		private System.Windows.Forms.Button RenameSection;
		private System.Windows.Forms.ListBox SectionLister;
		private System.Windows.Forms.Button SaveSection;
		private System.Windows.Forms.Button NewSection;
		private System.Windows.Forms.Button SaveKnlBtn;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListBox KnlFileList;
		private System.Windows.Forms.TextBox SectionsTextbox;
		private System.Windows.Forms.TextBox FileNameTextbox;
		private System.Windows.Forms.Label IlangLabel;
		private System.Windows.Forms.ToolStripMenuItem editorToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem funcionesToolStripMenuItem;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.TextBox IlangTextbox;
		private System.Windows.Forms.GroupBox KnlEditor;
		private System.Windows.Forms.TextBox knlFilterText;
		private System.Windows.Forms.ToolStripMenuItem sectionsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
		private System.Windows.Forms.Button newKnlButton;
		private System.Windows.Forms.Button CopyKNLButton;
		private System.Windows.Forms.ListBox FilteredKnlListBox;
		private System.Windows.Forms.Label SaveLight;
		private System.Windows.Forms.Button TestButton;
		private System.Windows.Forms.ToolStripMenuItem boxesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem varsToolStripMenuItem;
		private System.Windows.Forms.ComboBox KnlFolderSelector;
		private System.Windows.Forms.Button SaveClipboardDataButton;
		private System.Windows.Forms.GroupBox MetadataBox;
		private System.Windows.Forms.Label KnlIdLabel;
		private System.Windows.Forms.ListBox MetadataListbox;
		private System.Windows.Forms.TextBox MetadataTextbox;
	}
}
