/*
 * Created by SharpDevelop.
 * User: alejandro.rangel
 * Date: 29/10/2019
 * Time: 03:03 p. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ILang2._
{
	partial class SubjugatorCore
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SubjugatorCore));
			this.ScriptsList = new System.Windows.Forms.ListBox();
			this.SymTabNText = new System.Windows.Forms.TextBox();
			this.SymTabVText = new System.Windows.Forms.TextBox();
			this.SyncTablesButton = new System.Windows.Forms.Button();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveEditorCodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.visorVariablesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editorCodigoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.runEditorCodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.runBloqToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.reloadBloqListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.EditorBoxGroup = new System.Windows.Forms.GroupBox();
			this.EditorFileName = new System.Windows.Forms.TextBox();
			this.SCCodeEditor = new System.Windows.Forms.TextBox();
			this.FilterScriptText = new System.Windows.Forms.TextBox();
			this.FilteredScriptList = new System.Windows.Forms.ListBox();
			this.TabListCtrl = new System.Windows.Forms.TabControl();
			this.TabVars = new System.Windows.Forms.TabPage();
			this.VarVisorText = new System.Windows.Forms.TextBox();
			this.SymTabValues = new System.Windows.Forms.ListBox();
			this.SymTabNames = new System.Windows.Forms.ListBox();
			this.TabThreads = new System.Windows.Forms.TabPage();
			this.BloqKeysListbox = new System.Windows.Forms.ListBox();
			this.ProcListbox = new System.Windows.Forms.ListBox();
			this.TabProcess = new System.Windows.Forms.TabPage();
			this.ProcNamesListbox = new System.Windows.Forms.ListBox();
			this.KnlPathsCombo = new System.Windows.Forms.ComboBox();
			this.menuStrip1.SuspendLayout();
			this.EditorBoxGroup.SuspendLayout();
			this.TabListCtrl.SuspendLayout();
			this.TabVars.SuspendLayout();
			this.TabThreads.SuspendLayout();
			this.TabProcess.SuspendLayout();
			this.SuspendLayout();
			// 
			// ScriptsList
			// 
			this.ScriptsList.FormattingEnabled = true;
			this.ScriptsList.Location = new System.Drawing.Point(13, 76);
			this.ScriptsList.Name = "ScriptsList";
			this.ScriptsList.Size = new System.Drawing.Size(312, 381);
			this.ScriptsList.TabIndex = 0;
			this.ScriptsList.SelectedIndexChanged += new System.EventHandler(this.ScriptsListSelectedIndexChanged);
			this.ScriptsList.DoubleClick += new System.EventHandler(this.ListBox1DoubleClick);
			this.ScriptsList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ListBox1MouseDown);
			// 
			// SymTabNText
			// 
			this.SymTabNText.Location = new System.Drawing.Point(330, 26);
			this.SymTabNText.Name = "SymTabNText";
			this.SymTabNText.Size = new System.Drawing.Size(128, 20);
			this.SymTabNText.TabIndex = 5;
			// 
			// SymTabVText
			// 
			this.SymTabVText.Location = new System.Drawing.Point(469, 26);
			this.SymTabVText.Name = "SymTabVText";
			this.SymTabVText.Size = new System.Drawing.Size(134, 20);
			this.SymTabVText.TabIndex = 6;
			this.SymTabVText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SymTabVTextKeyPress);
			// 
			// SyncTablesButton
			// 
			this.SyncTablesButton.Location = new System.Drawing.Point(609, 26);
			this.SyncTablesButton.Name = "SyncTablesButton";
			this.SyncTablesButton.Size = new System.Drawing.Size(40, 20);
			this.SyncTablesButton.TabIndex = 7;
			this.SyncTablesButton.Text = "Sync";
			this.SyncTablesButton.UseVisualStyleBackColor = true;
			this.SyncTablesButton.Click += new System.EventHandler(this.SyncTablesButtonClick);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.archivoToolStripMenuItem,
			this.editorToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(659, 24);
			this.menuStrip1.TabIndex = 9;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// archivoToolStripMenuItem
			// 
			this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.saveEditorCodeToolStripMenuItem});
			this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
			this.archivoToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
			this.archivoToolStripMenuItem.Text = "Archivo";
			// 
			// saveEditorCodeToolStripMenuItem
			// 
			this.saveEditorCodeToolStripMenuItem.Name = "saveEditorCodeToolStripMenuItem";
			this.saveEditorCodeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.saveEditorCodeToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
			this.saveEditorCodeToolStripMenuItem.Text = "Save Editor Code";
			this.saveEditorCodeToolStripMenuItem.Click += new System.EventHandler(this.SaveEditorCodeToolStripMenuItemClick);
			// 
			// editorToolStripMenuItem
			// 
			this.editorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.visorVariablesToolStripMenuItem,
			this.editorCodigoToolStripMenuItem,
			this.runEditorCodeToolStripMenuItem,
			this.runBloqToolStripMenuItem,
			this.reloadBloqListToolStripMenuItem,
			this.saveClipboardToolStripMenuItem});
			this.editorToolStripMenuItem.Name = "editorToolStripMenuItem";
			this.editorToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
			this.editorToolStripMenuItem.Text = "Editor";
			// 
			// visorVariablesToolStripMenuItem
			// 
			this.visorVariablesToolStripMenuItem.Name = "visorVariablesToolStripMenuItem";
			this.visorVariablesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
			this.visorVariablesToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
			this.visorVariablesToolStripMenuItem.Text = "Visor valores";
			this.visorVariablesToolStripMenuItem.Click += new System.EventHandler(this.VisorVariablesToolStripMenuItemClick);
			// 
			// editorCodigoToolStripMenuItem
			// 
			this.editorCodigoToolStripMenuItem.Name = "editorCodigoToolStripMenuItem";
			this.editorCodigoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
			this.editorCodigoToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
			this.editorCodigoToolStripMenuItem.Text = "Editor codigo";
			this.editorCodigoToolStripMenuItem.Click += new System.EventHandler(this.EditorCodigoToolStripMenuItemClick);
			// 
			// runEditorCodeToolStripMenuItem
			// 
			this.runEditorCodeToolStripMenuItem.Name = "runEditorCodeToolStripMenuItem";
			this.runEditorCodeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
			| System.Windows.Forms.Keys.R)));
			this.runEditorCodeToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
			this.runEditorCodeToolStripMenuItem.Text = "Run Editor Code";
			this.runEditorCodeToolStripMenuItem.Click += new System.EventHandler(this.RunEditorCodeToolStripMenuItemClick);
			// 
			// runBloqToolStripMenuItem
			// 
			this.runBloqToolStripMenuItem.Name = "runBloqToolStripMenuItem";
			this.runBloqToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
			this.runBloqToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
			this.runBloqToolStripMenuItem.Text = "Run Bloq";
			this.runBloqToolStripMenuItem.Click += new System.EventHandler(this.RunBloqToolStripMenuItemClick);
			// 
			// reloadBloqListToolStripMenuItem
			// 
			this.reloadBloqListToolStripMenuItem.Name = "reloadBloqListToolStripMenuItem";
			this.reloadBloqListToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
			this.reloadBloqListToolStripMenuItem.Text = "Reload Bloq List";
			this.reloadBloqListToolStripMenuItem.Click += new System.EventHandler(this.ReloadBloqListToolStripMenuItemClick);
			// 
			// saveClipboardToolStripMenuItem
			// 
			this.saveClipboardToolStripMenuItem.Name = "saveClipboardToolStripMenuItem";
			this.saveClipboardToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
			| System.Windows.Forms.Keys.V)));
			this.saveClipboardToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
			this.saveClipboardToolStripMenuItem.Text = "Save Clipboard";
			this.saveClipboardToolStripMenuItem.Click += new System.EventHandler(this.SaveClipboardToolStripMenuItemClick);
			// 
			// EditorBoxGroup
			// 
			this.EditorBoxGroup.Controls.Add(this.EditorFileName);
			this.EditorBoxGroup.Controls.Add(this.SCCodeEditor);
			this.EditorBoxGroup.Location = new System.Drawing.Point(12, 26);
			this.EditorBoxGroup.Name = "EditorBoxGroup";
			this.EditorBoxGroup.Size = new System.Drawing.Size(374, 435);
			this.EditorBoxGroup.TabIndex = 12;
			this.EditorBoxGroup.TabStop = false;
			this.EditorBoxGroup.Text = "Editor";
			this.EditorBoxGroup.Visible = false;
			this.EditorBoxGroup.Enter += new System.EventHandler(this.EditorBoxGroupEnter);
			// 
			// EditorFileName
			// 
			this.EditorFileName.Location = new System.Drawing.Point(6, 16);
			this.EditorFileName.Name = "EditorFileName";
			this.EditorFileName.Size = new System.Drawing.Size(362, 20);
			this.EditorFileName.TabIndex = 12;
			// 
			// SCCodeEditor
			// 
			this.SCCodeEditor.Location = new System.Drawing.Point(6, 42);
			this.SCCodeEditor.Multiline = true;
			this.SCCodeEditor.Name = "SCCodeEditor";
			this.SCCodeEditor.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.SCCodeEditor.Size = new System.Drawing.Size(362, 385);
			this.SCCodeEditor.TabIndex = 11;
			this.SCCodeEditor.WordWrap = false;
			// 
			// FilterScriptText
			// 
			this.FilterScriptText.Location = new System.Drawing.Point(13, 51);
			this.FilterScriptText.Name = "FilterScriptText";
			this.FilterScriptText.Size = new System.Drawing.Size(311, 20);
			this.FilterScriptText.TabIndex = 13;
			this.FilterScriptText.TextChanged += new System.EventHandler(this.FilterScriptTextTextChanged);
			// 
			// FilteredScriptList
			// 
			this.FilteredScriptList.FormattingEnabled = true;
			this.FilteredScriptList.Location = new System.Drawing.Point(14, 76);
			this.FilteredScriptList.Name = "FilteredScriptList";
			this.FilteredScriptList.Size = new System.Drawing.Size(311, 381);
			this.FilteredScriptList.TabIndex = 14;
			this.FilteredScriptList.Visible = false;
			this.FilteredScriptList.SelectedIndexChanged += new System.EventHandler(this.FilteredScriptListSelectedIndexChanged);
			this.FilteredScriptList.DoubleClick += new System.EventHandler(this.FilteredScriptListDoubleClick);
			this.FilteredScriptList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ListBox1MouseDown);
			// 
			// TabListCtrl
			// 
			this.TabListCtrl.Controls.Add(this.TabVars);
			this.TabListCtrl.Controls.Add(this.TabThreads);
			this.TabListCtrl.Controls.Add(this.TabProcess);
			this.TabListCtrl.Location = new System.Drawing.Point(330, 51);
			this.TabListCtrl.Name = "TabListCtrl";
			this.TabListCtrl.SelectedIndex = 0;
			this.TabListCtrl.Size = new System.Drawing.Size(329, 409);
			this.TabListCtrl.TabIndex = 15;
			// 
			// TabVars
			// 
			this.TabVars.Controls.Add(this.VarVisorText);
			this.TabVars.Controls.Add(this.SymTabValues);
			this.TabVars.Controls.Add(this.SymTabNames);
			this.TabVars.Location = new System.Drawing.Point(4, 22);
			this.TabVars.Name = "TabVars";
			this.TabVars.Padding = new System.Windows.Forms.Padding(3);
			this.TabVars.Size = new System.Drawing.Size(321, 383);
			this.TabVars.TabIndex = 0;
			this.TabVars.Text = "Variables";
			this.TabVars.UseVisualStyleBackColor = true;
			// 
			// VarVisorText
			// 
			this.VarVisorText.Location = new System.Drawing.Point(0, 156);
			this.VarVisorText.Multiline = true;
			this.VarVisorText.Name = "VarVisorText";
			this.VarVisorText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.VarVisorText.Size = new System.Drawing.Size(321, 229);
			this.VarVisorText.TabIndex = 17;
			this.VarVisorText.Visible = false;
			// 
			// SymTabValues
			// 
			this.SymTabValues.FormattingEnabled = true;
			this.SymTabValues.Location = new System.Drawing.Point(143, 6);
			this.SymTabValues.Name = "SymTabValues";
			this.SymTabValues.Size = new System.Drawing.Size(175, 368);
			this.SymTabValues.TabIndex = 5;
			this.SymTabValues.SelectedIndexChanged += new System.EventHandler(this.SymTabValuesSelectedIndexChanged);
			// 
			// SymTabNames
			// 
			this.SymTabNames.FormattingEnabled = true;
			this.SymTabNames.Location = new System.Drawing.Point(6, 6);
			this.SymTabNames.Name = "SymTabNames";
			this.SymTabNames.Size = new System.Drawing.Size(131, 368);
			this.SymTabNames.TabIndex = 4;
			this.SymTabNames.SelectedIndexChanged += new System.EventHandler(this.SymTabNamesSelectedIndexChanged);
			// 
			// TabThreads
			// 
			this.TabThreads.Controls.Add(this.BloqKeysListbox);
			this.TabThreads.Controls.Add(this.ProcListbox);
			this.TabThreads.Location = new System.Drawing.Point(4, 22);
			this.TabThreads.Name = "TabThreads";
			this.TabThreads.Padding = new System.Windows.Forms.Padding(3);
			this.TabThreads.Size = new System.Drawing.Size(321, 383);
			this.TabThreads.TabIndex = 1;
			this.TabThreads.Text = "Threads";
			this.TabThreads.UseVisualStyleBackColor = true;
			// 
			// BloqKeysListbox
			// 
			this.BloqKeysListbox.FormattingEnabled = true;
			this.BloqKeysListbox.Location = new System.Drawing.Point(6, 3);
			this.BloqKeysListbox.Name = "BloqKeysListbox";
			this.BloqKeysListbox.Size = new System.Drawing.Size(148, 368);
			this.BloqKeysListbox.TabIndex = 1;
			this.BloqKeysListbox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BloqKeysListboxMouseDown);
			// 
			// ProcListbox
			// 
			this.ProcListbox.FormattingEnabled = true;
			this.ProcListbox.Location = new System.Drawing.Point(156, 3);
			this.ProcListbox.Name = "ProcListbox";
			this.ProcListbox.Size = new System.Drawing.Size(159, 368);
			this.ProcListbox.TabIndex = 0;
			// 
			// TabProcess
			// 
			this.TabProcess.Controls.Add(this.ProcNamesListbox);
			this.TabProcess.Location = new System.Drawing.Point(4, 22);
			this.TabProcess.Name = "TabProcess";
			this.TabProcess.Padding = new System.Windows.Forms.Padding(3);
			this.TabProcess.Size = new System.Drawing.Size(321, 383);
			this.TabProcess.TabIndex = 2;
			this.TabProcess.Text = "Process";
			this.TabProcess.UseVisualStyleBackColor = true;
			// 
			// ProcNamesListbox
			// 
			this.ProcNamesListbox.FormattingEnabled = true;
			this.ProcNamesListbox.Location = new System.Drawing.Point(93, 6);
			this.ProcNamesListbox.Name = "ProcNamesListbox";
			this.ProcNamesListbox.Size = new System.Drawing.Size(166, 368);
			this.ProcNamesListbox.TabIndex = 0;
			this.ProcNamesListbox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ProcNamesListboxMouseDown);
			// 
			// KnlPathsCombo
			// 
			this.KnlPathsCombo.FormattingEnabled = true;
			this.KnlPathsCombo.Location = new System.Drawing.Point(14, 28);
			this.KnlPathsCombo.Name = "KnlPathsCombo";
			this.KnlPathsCombo.Size = new System.Drawing.Size(310, 21);
			this.KnlPathsCombo.TabIndex = 16;
			this.KnlPathsCombo.Text = "C:\\pyscripts";
			this.KnlPathsCombo.SelectedIndexChanged += new System.EventHandler(this.KnlPathsComboSelectedIndexChanged);
			// 
			// SubjugatorCore
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(659, 469);
			this.Controls.Add(this.EditorBoxGroup);
			this.Controls.Add(this.SyncTablesButton);
			this.Controls.Add(this.SymTabVText);
			this.Controls.Add(this.SymTabNText);
			this.Controls.Add(this.menuStrip1);
			this.Controls.Add(this.FilterScriptText);
			this.Controls.Add(this.FilteredScriptList);
			this.Controls.Add(this.ScriptsList);
			this.Controls.Add(this.TabListCtrl);
			this.Controls.Add(this.KnlPathsCombo);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "SubjugatorCore";
			this.Text = "Core";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SubjugatorCoreFormClosing);
			this.Load += new System.EventHandler(this.SubjugatorCoreLoad);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.EditorBoxGroup.ResumeLayout(false);
			this.EditorBoxGroup.PerformLayout();
			this.TabListCtrl.ResumeLayout(false);
			this.TabVars.ResumeLayout(false);
			this.TabVars.PerformLayout();
			this.TabThreads.ResumeLayout(false);
			this.TabProcess.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		private System.Windows.Forms.ListBox ProcNamesListbox;
		private System.Windows.Forms.TabPage TabProcess;
		private System.Windows.Forms.ListBox BloqKeysListbox;
		private System.Windows.Forms.ListBox ProcListbox;
		private System.Windows.Forms.TabPage TabThreads;
		private System.Windows.Forms.TabPage TabVars;
		private System.Windows.Forms.TabControl TabListCtrl;
		private System.Windows.Forms.ToolStripMenuItem reloadBloqListToolStripMenuItem;
		private System.Windows.Forms.ListBox FilteredScriptList;
		private System.Windows.Forms.TextBox FilterScriptText;
		private System.Windows.Forms.ToolStripMenuItem runBloqToolStripMenuItem;
		private System.Windows.Forms.GroupBox EditorBoxGroup;
		private System.Windows.Forms.TextBox EditorFileName;
		private System.Windows.Forms.ToolStripMenuItem saveEditorCodeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem runEditorCodeToolStripMenuItem;
		private System.Windows.Forms.TextBox SCCodeEditor;
		private System.Windows.Forms.ToolStripMenuItem editorCodigoToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem visorVariablesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editorToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.TextBox VarVisorText;
		private System.Windows.Forms.Button SyncTablesButton;
		private System.Windows.Forms.TextBox SymTabVText;
		private System.Windows.Forms.TextBox SymTabNText;
		public System.Windows.Forms.ListBox SymTabValues;
		public System.Windows.Forms.ListBox SymTabNames;
		private System.Windows.Forms.ListBox ScriptsList;
		private System.Windows.Forms.ToolStripMenuItem saveClipboardToolStripMenuItem;
		private System.Windows.Forms.ComboBox KnlPathsCombo;
	}
}
