/*
 * Created by SharpDevelop.
 * User: alejandro.rangel
 * Date: 10/12/2019
 * Time: 10:05 a. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ILang2._
{
	partial class MainForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.mainMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.elFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.elCoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.runSOmethinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.mainMenuToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(1050, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// mainMenuToolStripMenuItem
			// 
			this.mainMenuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.elFormToolStripMenuItem,
			this.elCoreToolStripMenuItem,
			this.runSOmethinToolStripMenuItem});
			this.mainMenuToolStripMenuItem.Name = "mainMenuToolStripMenuItem";
			this.mainMenuToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
			this.mainMenuToolStripMenuItem.Text = "MainMenu";
			// 
			// elFormToolStripMenuItem
			// 
			this.elFormToolStripMenuItem.Name = "elFormToolStripMenuItem";
			this.elFormToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
			this.elFormToolStripMenuItem.Text = "ElILang";
			this.elFormToolStripMenuItem.Click += new System.EventHandler(this.ElFormToolStripMenuItemClick);
			// 
			// elCoreToolStripMenuItem
			// 
			this.elCoreToolStripMenuItem.Name = "elCoreToolStripMenuItem";
			this.elCoreToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
			this.elCoreToolStripMenuItem.Text = "ElCore";
			this.elCoreToolStripMenuItem.Click += new System.EventHandler(this.ElCoreToolStripMenuItemClick);
			// 
			// runSOmethinToolStripMenuItem
			// 
			this.runSOmethinToolStripMenuItem.Name = "runSOmethinToolStripMenuItem";
			this.runSOmethinToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
			this.runSOmethinToolStripMenuItem.Text = "RunSOmethin";
			this.runSOmethinToolStripMenuItem.Click += new System.EventHandler(this.RunSOmethinToolStripMenuItemClick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1050, 683);
			this.Controls.Add(this.menuStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.IsMdiContainer = true;
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainForm";
			this.Text = "ILang2.0";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		private System.Windows.Forms.ToolStripMenuItem runSOmethinToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem elCoreToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem elFormToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem mainMenuToolStripMenuItem;
		private System.Windows.Forms.MenuStrip menuStrip1;
	}
}
