/*
 * Created by SharpDevelop.
 * User: alejandro.rangel
 * Date: 10/12/2019
 * Time: 10:05 a. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Windows.Forms;

using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Net.Sockets;


using System.Threading;
namespace ILang2._
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	
	public partial class MainForm : Form
	{
		int iLangCounter=1;
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			deleteTempFiles();
			Form form = new ILangForm(iLangCounter, this.Handle);
			form.MdiParent = this;
			//form.Dock=DockStyle.Fill; //para ajustar al 
			form.Show();
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		public void deleteTempFiles(){
			string[] ficheros=Directory.GetFiles("C:\\pyscripts\\TempKnlsFiles\\");
			int notDeletedCounter = 0;
			for (int i = 0; i < ficheros.Length; i++){
				try{
					File.Delete(ficheros[i]);
				}catch(Exception exept){
					exept.GetType(); //Para remover el warning de no usar la variable exept
					notDeletedCounter++;
				}
			}
			if(notDeletedCounter>0)
				MessageBox.Show("Numero de archivos que no fueron posible eliminar:"+notDeletedCounter);
		}
		
		void ElFormToolStripMenuItemClick(object sender, EventArgs e)
		{
			Form form = new ILangForm(iLangCounter, this.Handle);
			form.MdiParent = this;
			//form.Dock=DockStyle.Fill; //para ajustar al 
			form.Show();						
		}
		
		void ElCoreToolStripMenuItemClick(object sender, EventArgs e)
		{
			string SocketMaster = Microsoft.VisualBasic.Interaction.InputBox("Numero un puerto","Inserte dato solicitado");
			int socketMaster;
			if(string.IsNullOrEmpty(SocketMaster))
				return;
			if(!int.TryParse(SocketMaster, out socketMaster)){
				MessageBox.Show("El puerto especificado no es valido", "Error");
				return;
			}
			socketMaster = int.Parse(SocketMaster);
			using(TcpClient tcpClient = new TcpClient())
			{
			    try {
			        tcpClient.Connect("127.0.0.1", socketMaster);
			        MessageBox.Show("El puerto especificado esta en uso");
			        return;
			    } catch (Exception) {
					
			    }
			}
			
			SubjugatorCore newCore = new SubjugatorCore(socketMaster, this.Handle);
			newCore.MdiParent = this;
			newCore.Show();
		}
		
		[DllImport("user32.dll", SetLastError = true)]
		private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
		
		void RunSOmethinToolStripMenuItemClick(object sender, EventArgs e)
		{
			Process myProceso = new Process();
			myProceso.StartInfo = new ProcessStartInfo("notepad");
			//Process.Start("notepad.exe");
			myProceso.Start();
            // Sleep the thread in order to let the Notepad start completely
            Thread.Sleep(100);
            SetParent(myProceso.MainWindowHandle, this.Handle);
		}
	}
}
