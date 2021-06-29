/*
 * Created by SharpDevelop.
 * User: alejandro.rangel
 * Date: 10/12/2019
 * Time: 10:24 a. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using System.Drawing;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Diagnostics;

using System.Threading;


/*
* 
* CAMBIOS IMPLEMENTADOS
* 	Hacer que la seleccion de categorias (carpetas de knls) sea con el combobox
* 	Hacer que la lista de archivos sea solo el nombre y no la ruta completa
* 		Se almacena la ruta de la carpeta de knls en la variable global (formulario) "rutaKnlFiles"
* 		El nombre del knl abierto se almacena en la variable global (formulario) "selectedKnl" y se guarda la ruta completa
*	Hacer que se borren los archivos temporales al iniciar el MainForm (no el ilang form)
* 	Mejorar el sistema de busqueda de knls, si busca en listas va a ser mas tardado como en el ilang 1.3
* 		La busqueda se implemento de la siguiente manera:
* 			Se toman de un solo golpe todos los valores de la lista y se convierten en arraylist, luego se filtra el arraylist y se asignan todos los valores directamente al listbox
* 	Hacer que se puedan agregar imagenes desde el clipboard al knl abierto.
* 		para eso esta el boton Clpbrd el cual pide nombre de seccion con el que sera agregado
*
* CAMBIOS A IMPLEMENTAR
*   Ver que onda con el visor de variables, no se que haga ni se para que sirva.
* 		Hasta el momento almacena datos nada mas, en forma de texto o imagenes, pero al querer recuperarlas solo se pueden los textos
 * 	Estructurar los contextos para que sean mas lucidos (las carpetas)
* 	ver si se pueden hacer relaciones a otros knls
 *   Implementar relaciones o llamadas a sus propias secciones. (necesito el lenguaje de programacion ILANG)
 * 		Que haga la ejecucion de los procedimientos indicados para manejar los datos internos.
 * 	Hacer que los knl tengan relacion a otros para que se mueva de un knl a otro para cosas como implmementacion de informacion.
*  
 * IMPORTANTES:
 * Terminar los de SQL, el sistema necesita que se cree una seccion que diga ID la cual solo va a contener el ID del knl
 * hacer que se cree al momenot de insertar el nuevo knl
 * para knls que no tiene ID aun hay que hacer que al darle click en save se cree el ID para estos.
 * hacer la parte de la metadata
 * 		El apartado de la metadata se llena sola cuando la seccion id.txt existe.
 * 			sino que busque manualmente con los datos de filename, context y path
 * 		hacer que se agregue o elimine metadata, la metadata del knl actual se almacena en el ArrayList<Object[]> knlMetadata
*/






namespace ILang2._
{
	
	/// <summary>
	/// Description of ILangForm.
	/// </summary>
	/// 
	public partial class ILangForm : Form
	{
		public String rutaKnlFiles="data\\";
		public String rootKnlData = "data\\";
		public String tempDirKnl ="data\\TempKnlsFiles\\";
		public bool isRelationViewer = false;
		
		public String knlFilename;
		public String selectedKnl;
		public String selectedFileSection;
		public ArrayList knlMetadata;
		public string knlId;
		public String tempKnlName;
		public int index;
		
		public bool enableUnsave=false;
		public bool enableNewFile=false;
		public bool isNewKnlFile=false;
		public IntPtr parentHandle;
		public bool isAtomicKNl=false;
		public Dictionary<String,object> symbTable = new Dictionary<String,object>();
		
		
		public ILangForm(int windowIndex,IntPtr parentHandle)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			DirectoryInfo directory = new DirectoryInfo(rutaKnlFiles);
			DirectoryInfo[] directories = directory.GetDirectories();
			KnlFolderSelector.Items.Add("\\");
			for (int i = 0; i < directories.Length; i++)
			{
				KnlFolderSelector.Items.Add(((DirectoryInfo)directories[i]).FullName.Substring(((DirectoryInfo)directories[i]).FullName.LastIndexOf("\\")+1));
			}
			
			this.parentHandle = parentHandle;
			tempKnlName="TempKnl"+windowIndex.ToString()+".tmp";//para si creamos multiples forms de ilang no haya problemas al querer crear varios KNL nuevos a la vez
			this.index=windowIndex;
			
			loadFilesInListbox(KnlFileList);
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		public void loadFilesInListbox(ListBox listBox){
			listBox.Items.Clear();
			string[] ficheros=Directory.GetFiles(rutaKnlFiles);
			for (int i = 0; i < ficheros.Length; i++){
				listBox.Items.Add(ficheros[i].Substring(ficheros[i].LastIndexOf("\\")+1));
			}
		}
		
		void FuncionesToolStripMenuItemClick(object sender, EventArgs e)
		{
			
		}
		
		void EditorToolStripMenuItemClick(object sender, EventArgs e)
		{
			KnlEditor.Visible=!KnlEditor.Visible;
		}
		
		void MenuStrip1ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			
		}
		
		//TODO create a util library with these methods
		public static string array2Text(ListBox.ObjectCollection theList){
			string toRet ="";
			for(int i=0; i<theList.Count; i++){
				toRet=toRet+theList[i].ToString()+"\n";
			}
			toRet.Substring(0,toRet.Length-1);
			return toRet;
		}
		
		
		void SaveSectionClick(object sender, EventArgs e)
		{
			//Guardar la seccion seleccionada en la lista de secciones
			if(SectionLister.SelectedIndex>=0){
				FileInfo zipFile = new FileInfo(selectedKnl);
				FileStream zipStream = zipFile.Open(FileMode.Open);
				ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Update);
				ZipArchiveEntry oldEntry = archive.GetEntry(selectedFileSection);
				oldEntry.Delete();
				archive.Dispose();
				zipStream.Close();
				
				FileInfo zipFileSv = new FileInfo(selectedKnl);
				FileStream zipStreamSv = zipFileSv.Open(FileMode.Open);
				ZipArchive archiveSv = new ZipArchive(zipStreamSv, ZipArchiveMode.Update);
				ZipArchiveEntry entryNew = archiveSv.CreateEntry(selectedFileSection);
				Stream zipStreamNew = entryNew.Open();
				var stream = new MemoryStream();
				var writer = new StreamWriter(stream);
				if(isRelationViewer)
					writer.Write(ILangForm.array2Text(RelationLister.Items));
				else
					writer.Write(SectionsTextbox.Text);
				writer.Flush();
				stream.Position = 0;
				stream.CopyTo(zipStreamNew);
				
				zipStreamNew.Close();
				stream.Close();
				archiveSv.Dispose();
				zipStreamSv.Close();
				if(!isNewKnlFile)
					SaveLight.BackColor = Color.Lime;
			}
		}
		
		void NewSectionClick(object sender, EventArgs e)
		{
			if(selectedKnl == null){
				MessageBox.Show("No se ha creado ni seleccionado un Knl","Knl Exception");
				return;
			}
			String sectionName = Microsoft.VisualBasic.Interaction.InputBox("Insert section name","Section Name");
			if(!String.IsNullOrEmpty(sectionName)){
				if(sectionName.LastIndexOf('.')<=0 || sectionName.Substring(sectionName.LastIndexOf('.')).Length<=1)//there is no file extension
					sectionName=sectionName+((sectionName.LastIndexOf('.')==-1)?".txt":"txt");
				if(!SectionLister.Items.Contains(sectionName)){
					FileInfo zipFile = new FileInfo(selectedKnl);
					FileStream zipStream = zipFile.Open(FileMode.Open);
					ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Update);
					ZipArchiveEntry entryNew = archive.CreateEntry(sectionName);
					Stream zipStreamNew = entryNew.Open();
					var stream = new MemoryStream();
					var writer = new StreamWriter(stream);
					writer.Write(SectionsTextbox.Text);
					writer.Flush();
					stream.Position = 0;
					stream.CopyTo(zipStreamNew);
					
					zipStreamNew.Close();
					stream.Close();
					archive.Dispose();
					zipStream.Close();
					
					SectionLister.Items.Add(sectionName);
					if(!isNewKnlFile)
						SaveLight.BackColor=Color.Lime;
				}else{
					MessageBox.Show("Section already exists","Error");
				}
			}else{
				MessageBox.Show("Invalid name","Error");
			}
		}
		void RenameSectionFunction(){
			//Abrir un inputbox para pedir el nombre nuevo de la seccion, poniendo de default el nombre actual
			//verificar que no exista la seccion ya.
			if(SectionLister.SelectedIndex>=0){
				String newName = Microsoft.VisualBasic.Interaction.InputBox("Insert new section name","Rename Section",selectedFileSection);
				if(newName!=null && newName.IndexOf('.')>0 && !SectionLister.Items.Contains(newName)){
					
					FileInfo zipFile = new FileInfo(selectedKnl);
					FileStream zipStream = zipFile.Open(FileMode.Open);
					ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Update);
					ZipArchiveEntry entry = archive.GetEntry(selectedFileSection);
					Stream stream = entry.Open();
					
					ZipArchiveEntry entryNew = archive.CreateEntry(newName);
					Stream zipStreamNew = entryNew.Open();
					stream.CopyTo(zipStreamNew);
					
					zipStreamNew.Close();
					stream.Close();
					ZipArchiveEntry oldEntry = archive.GetEntry(selectedFileSection); 
					oldEntry.Delete(); 
					
					
					archive.Dispose();
					zipStream.Close();
					
					SectionLister.Items.RemoveAt(SectionLister.SelectedIndex);
					SectionLister.Items.Add(newName);
					if(!isNewKnlFile)
						SaveLight.BackColor = Color.Lime;
				}else{
					MessageBox.Show("Error, invalid name, specify a file extension and verify that the input name doesn't exist already in the sections","Error");
				}
			}
		}
		void RenameSectionClick(object sender, EventArgs e)
		{
			RenameSectionFunction();
		}
		
		void SectionListerSelectedIndexChanged(object sender, EventArgs e)
		{
			//todas las secciones de un knl estan aqui.
			//al darle click se debe cargar en el textbox de abajo en caso de que sea texto.
			//En casi de que sea otro tipo de archivo se debe abrir con el programa por default
			//viene en modo lista porque el archivo knl se tiene planeado que sea un zip lo que quiere decir que cada seccion es un archivo
			if(SectionLister.SelectedIndex>-1){
				selectedFileSection= SectionLister.Items[SectionLister.SelectedIndex].ToString();
				String fileExt = selectedFileSection.Substring(selectedFileSection.LastIndexOf('.'));
				//TODO ponerele un switch para saber el tipo de archivo
				//switch(
				isRelationViewer=(fileExt == ".rel")?true:false;
				RelationLister.Items.Clear();
				FileInfo zipFile = new FileInfo(selectedKnl);
				FileStream zipStream = zipFile.OpenRead();
				ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Read);
				ZipArchiveEntry entry = archive.GetEntry(selectedFileSection);
				Stream stream = entry.Open();
				StreamReader reader = new StreamReader(stream);
				if(isRelationViewer){
					SectionsTextbox.Visible=false;
					RelationLister.Visible=true;
					RelationLister.Items.AddRange(reader.ReadToEnd().Split('\n'));
				}
				else{
					RelationLister.Visible=false;
					SectionsTextbox.Visible=true;
					SectionsTextbox.Text = reader.ReadToEnd();
				}
				
				stream.Close();
				archive.Dispose();
				zipStream.Close();
			}
		}
		
		void SaveKnlBtnClick(object sender, EventArgs e)
		{
			//Guardar el knl nuevo
			//en caso de que sea un simple archivo de texto plano, solo guardar un archivo de texto tal cual
			//en caso de ser varios archivos entonces hay que crear un zip con los archivos dentro.
			if(isAtomicKNl){
				StreamWriter escrito = File.CreateText(selectedKnl); // en el 
	            String contenido = IlangTextbox.Text;
	            escrito.Write(contenido.ToString());
	            escrito.Flush();
	            escrito.Close();
	            SaveLight.BackColor = Color.Lime;
	            return;
			}
			if(File.Exists(tempKnlName)){
				if(SectionLister.Items.Count==0){
					MessageBox.Show("Can not save an empty knl","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
					return;
				}
					
				String newKnlName ;
				if(String.IsNullOrEmpty(FileNameTextbox.Text)){
					newKnlName = Microsoft.VisualBasic.Interaction.InputBox("Insert Knl Name","Knl Name");
				}else{
					newKnlName = FileNameTextbox.Text.Replace(".zip","");
				}
				if(String.IsNullOrEmpty(newKnlName.Trim())){
					MessageBox.Show("Name not provided","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
					return;
				}
					
				if(KnlFileList.Items.Contains(rutaKnlFiles+ newKnlName+".zip")){
					MessageBox.Show("Knl with the name: " + newKnlName +", already exists","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
					return;
				}
				if(!String.IsNullOrEmpty(newKnlName)){
					File.Move(tempKnlName, rutaKnlFiles+newKnlName+".zip");
				}
				KnlFileList.Items.Add(newKnlName+".zip"); //update knl file list
				isNewKnlFile=false;
				SaveLight.BackColor = Color.Lime;
				selectedKnl=newKnlName+".zip";
				if(String.IsNullOrEmpty(knlId)){
					//MYSQL_IMP//MySQLReader.openDBConn();
					//MYSQL_IMP//knlId=MySQLReader.insertDB(selectedKnl, rutaKnlFiles, rutaKnlFiles+newKnlName+".zip");
					KnlIdLabel.Text="KnlId: "+knlId;
					//MYSQL_IMP//MySQLReader.closeDBConn();
				}
			}
		}
		
		void RemoveSectionClick(object sender, EventArgs e)
		{
			//Eliminar seccion, archivo, etc
			//solicitar primero confirmacion
			if(SectionLister.SelectedIndex>=0){
				if(MessageBox.Show("Delete this Section/file?","Delete",MessageBoxButtons.YesNo)==DialogResult.Yes){
					FileInfo zipFile = new FileInfo(selectedKnl);
					FileStream zipStream = zipFile.Open(FileMode.Open);
					ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Update);
					ZipArchiveEntry oldEntry = archive.GetEntry(selectedFileSection);
					SectionLister.Items.RemoveAt(SectionLister.SelectedIndex);
					oldEntry.Delete();
					archive.Dispose();
					zipStream.Close();
					if(!isNewKnlFile)
						SaveLight.BackColor=Color.Lime;
				}
			}
		}
		
		void RunILangToolStripMenuItemClick(object sender, EventArgs e)
		{
			//Run code in ILangTextBox
		}
		
		void SectionsTextboxTextChanged(object sender, EventArgs e)
		{
			//Cambiar la bandera de Seccion cambiada para si se cambia de archivo pregunte si desea guardar la seccion
			if(enableUnsave){
				SaveLight.BackColor = Color.Red;
			}
		}
		public void clearTexts(){
			IlangTextbox.Text="";
			SectionsTextbox.Text="";
		}
		void ListBox1SelectedIndexChanged(object sender, EventArgs e)
		{
			selectKnlFile(KnlFileList);
			
			//Cargar los archivos dependiendo de.
			//si es zip
			//si es txt cargarlo en dependiendo la primer linea
			//si la primer linea es un ILang -> cargarlo en ILANG
			//si no, cargarlo en template y ponerlo como Default
		}
		void SectionListerDragDrop(object sender, DragEventArgs e)
		{
			//para cuando queremos agregar archivos externos
			string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
			int existCounter=0;
			String fileNameOnly;
			foreach (string file in files){
				//agregar el archivo al zip
				fileNameOnly=file.Substring(file.LastIndexOf('\\')+1);
				if(SectionLister.Items.Contains(fileNameOnly)){
					existCounter++;
					MessageBox.Show("Section already exists","Skipping");
					if(existCounter==3){
						MessageBox.Show("Too many repeated section names, aborting","Aborting drag and drop");
						break;
					}
					continue;
				}
				FileInfo zipFile = new FileInfo(selectedKnl);
				FileStream zipStream = zipFile.Open(FileMode.Open);
				ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Update);
				FileInfo sourceFile = new FileInfo(file);
				FileStream sourceStream = sourceFile.OpenRead();
				ZipArchiveEntry newEntry = archive.CreateEntry(sourceFile.Name);
				Stream stream = newEntry.Open();
				sourceStream.CopyTo(stream);
				stream.Close();
				sourceStream.Close();
				// 4
				archive.Dispose();
				zipStream.Close();
				SectionLister.Items.Add(sourceFile.Name);
			}
		}
		void SectionListerDragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
		}
		void RenameToolStripMenuItemClick(object sender, EventArgs e)
		{
			RenameSectionFunction();
		}
		void Button1Click(object sender, EventArgs e)
		{
			isNewKnlFile=true;
			KnlFileList.SelectedIndex=-1;
			SectionLister.Items.Clear();
			selectedKnl="";
			knlFilename=null;
			FileNameTextbox.Text="";
			clearTexts();
			selectedKnl=tempKnlName;
			if(File.Exists(tempKnlName)){
				File.Delete(tempKnlName);
			}
			FileStream stream = new FileStream(selectedKnl, FileMode.Create);
			ZipArchive archive = new ZipArchive(stream, ZipArchiveMode.Create);
			archive.Dispose();
			stream.Close();
			SaveLight.BackColor=Color.Red;
		}
		void CopyKNLButtonClick(object sender, EventArgs e)
		{
			createNewKnlFromCopy();
			FileNameTextbox.Text="New " + FileNameTextbox.Text;
		}
		void createNewKnlFromCopy(){
			if(!String.IsNullOrEmpty(selectedKnl) && (KnlFileList.SelectedIndex>=0 || FilteredKnlListBox.SelectedIndex>=0)){
				if(File.Exists(tempKnlName)){
					File.Delete(tempKnlName);
				}
				KnlFileList.SelectedIndex=-1;
				FilteredKnlListBox.SelectedIndex=-1;
				File.Copy(selectedKnl,tempKnlName);
				selectedKnl=tempKnlName;
				SaveLight.BackColor=Color.Red;
			}
		}
		void ILangFormLoad(object sender, EventArgs e)
		{
	
		}
		void KnlFilterTextTextChanged(object sender, EventArgs e)
		{
			//filtrar los knl
			string textToFilter=knlFilterText.Text;
			if(String.IsNullOrEmpty(textToFilter)){
				FilteredKnlListBox.Items.Clear();
				FilteredKnlListBox.Visible =false;
				return;
			}
			FilteredKnlListBox.Visible = true;
			if(FilteredKnlListBox.Items.Count==0){
				ArrayList newCollection = new ArrayList();
				for(int i = 0;i<KnlFileList.Items.Count;i++){
					if(KnlFileList.Items[i].ToString().Contains(textToFilter)){
						newCollection.Add(KnlFileList.Items[i]);
					}
				}
				FilteredKnlListBox.Items.AddRange(newCollection.ToArray());
			}else{
				ArrayList newCollection = new ArrayList(FilteredKnlListBox.Items);
				FilteredKnlListBox.Items.Clear();
				for(int i = newCollection.Count-1; i>-1 ; i--){
					if(!newCollection[i].ToString().Contains(textToFilter)){
						newCollection.RemoveAt(i);
					}
				}
				FilteredKnlListBox.Items.AddRange(newCollection.ToArray());
			}
		}
		void FilteredKnlListBoxSelectedIndexChanged(object sender, EventArgs e)
		{
			selectKnlFile(FilteredKnlListBox);
		}
		
		void selectKnlFile(ListBox ListWithKnls){
			if(isRelationViewer){
				return;
			}
			if(File.Exists(tempKnlName)){
				File.Delete(tempKnlName);
			}
			if(ListWithKnls.SelectedIndex>=0){
				RelationLister.Items.Clear();
				SaveLight.BackColor=Color.Lime;
				enableNewFile=false;
				enableUnsave=false;
				isNewKnlFile=false;
				knlId=null;
				clearTexts();
				SectionLister.Items.Clear();
				MetadataListbox.Items.Clear();
				selectedKnl=rutaKnlFiles+ListWithKnls.Items[ListWithKnls.SelectedIndex].ToString();
				knlFilename=selectedKnl.Substring(selectedKnl.LastIndexOf('\\')+1);
				FileNameTextbox.Text=knlFilename;
				switch(selectedKnl.Substring(selectedKnl.LastIndexOf('.')+1)){
					case "knl":
						isAtomicKNl=true;
						break;
					case "zip":
						isAtomicKNl=false;
						FileInfo zipFile = new FileInfo(selectedKnl);
						FileStream zipStream = zipFile.OpenRead();
						ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Read);
						foreach (ZipArchiveEntry entry in archive.Entries)
						{
							SectionLister.Items.Add(entry.Name);
							if(entry.Name =="id.txt"){
								string idSselectedFileSection= "id.txt";
								String fileExt = idSselectedFileSection.Substring(idSselectedFileSection.LastIndexOf('.'));
								//TODO ponerele un switch para saber el tipo de archivo
								//switch(
								FileInfo idZipFile = new FileInfo(selectedKnl);
								FileStream idZipStream = idZipFile.OpenRead();
								ZipArchive idArchive = new ZipArchive(idZipStream, ZipArchiveMode.Read);
								ZipArchiveEntry idEntry = idArchive.GetEntry(idSselectedFileSection);
								Stream idStream = idEntry .Open();
								StreamReader idReader = new StreamReader(idStream);
								string id = idReader.ReadToEnd();
								if(Int32.Parse(id) > 0){
									//MYSQL_IMP//MySQLReader.openDBConn();
									//MYSQL_IMP//knlMetadata =MySQLReader.getMetadata(id);
									knlId = id;
									bulkMetadata();
									KnlIdLabel.Text="KnlId: "+id;
									//MYSQL_IMP//MySQLReader.closeDBConn();
								}
								idStream.Close();
								idArchive.Dispose();
								idZipStream.Close();
							}
						}
						if(String.IsNullOrEmpty(knlId)){
							//MYSQL_IMP//MySQLReader.openDBConn();
							//MYSQL_IMP//knlId = MySQLReader.getKnlId(knlFilename, rutaKnlFiles, selectedKnl);
							//MYSQL_IMP//knlMetadata =MySQLReader.getMetadata(knlId);
							bulkMetadata();
							KnlIdLabel.Text="KnlId: "+knlId;
							//MYSQL_IMP//MySQLReader.closeDBConn();
						}
						archive.Dispose();
						zipStream.Close();
						break;
					case "py":
					case "txt":
						isAtomicKNl=true;
						StreamReader leido = File.OpenText(selectedKnl);
						string contenido = null;
						while ((contenido = leido.ReadLine()) != null)
						{
							IlangTextbox.Text += contenido.ToString()+"\r\n";
						}
						leido.Close();
						break;
					default:
						isAtomicKNl=true;
						MessageBox.Show("Desconocido","Text");
						break;
				}
			}
		}
		void bulkMetadata(){
			if(knlMetadata == null)
				return;
			foreach(object[] element in knlMetadata){
				MetadataListbox.Items.Add(element[1].ToString());
			}
		}
		void SectionsTextboxClick(object sender, EventArgs e)
		{
			enableUnsave=true;
		}
		void FileNameTextboxTextChanged(object sender, EventArgs e)
		{
			if(enableNewFile){
				createNewKnlFromCopy();
				isNewKnlFile=true;
			}
		}
		void FileNameTextboxClick(object sender, EventArgs e)
		{
			enableNewFile=true;
		}
		void IlangTextboxTextChanged(object sender, EventArgs e)
		{
			if(enableUnsave)
				SaveLight.BackColor = Color.Red;
		}
		void IlangTextboxClick(object sender, EventArgs e)
		{
			enableUnsave=true;
		}
		void
			TestButtonClick(object sender, EventArgs e)
		{
			//MYSQL_IMP//MySQLReader.openDBConn();
			//MYSQL_IMP//MySQLReader.queryDB("select * from knldata");
			//MYSQL_IMP//MySQLReader.closeDBConn();
		}
		
		
		void BrTstClick(object sender, EventArgs e)
		{
			ILangWebBrowser.Navigate("www.google.com.mx");
		}
		
		void NewVarButtonClick(object sender, EventArgs e)
		{
			String varName = Microsoft.VisualBasic.Interaction.InputBox("Insert section name","Section Name");
			if(varName==null || varName.Trim()=="")
				return;
			if(Clipboard.ContainsText())
				symbTable.Add(varName,Clipboard.GetText());
			else if(Clipboard.ContainsImage())
				symbTable.Add(varName,Clipboard.GetImage());
			
			varsListBox.Items.Add(varName);
		}
		
		void VarsListBoxDoubleClick(object sender, EventArgs e)
		{
			if(varsListBox.SelectedIndex>=0)
				Clipboard.SetData(DataFormats.Text, symbTable[varsListBox.Items[varsListBox.SelectedIndex].ToString()]);
		}
		
		[DllImport("user32.dll", SetLastError = true)]
		private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
		
		[DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
		private static extern IntPtr FindWindow(string className, string lpWindowName);
		void RunSomethingToolStripMenuItemClick(object sender, EventArgs e)
		{
			Process myProceso = new Process();
			myProceso.StartInfo = new ProcessStartInfo("notepad");
			//Process.Start("notepad.exe");
			myProceso.Start();
            // Sleep the thread in order to let the Notepad start completely
            Thread.Sleep(100);
            SetParent(myProceso.MainWindowHandle, parentHandle);
		}
		void VarsToolStripMenuItemClick(object sender, EventArgs e)
		{
			VarsBox.Visible=!VarsBox.Visible;
			VarsBox.BringToFront();
		}
		void SectionListerDoubleClick(object sender, EventArgs e)
		{
			if(SectionLister.SelectedIndex>-1){
				selectedFileSection= SectionLister.Items[SectionLister.SelectedIndex].ToString();
				String fileExt = selectedFileSection.Substring(selectedFileSection.LastIndexOf('.'));
				//TODO ponerele un switch para saber el tipo de archivo
				//switch(
				FileInfo zipFile = new FileInfo(selectedKnl);
				FileStream zipStream = zipFile.OpenRead();
				ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Read);
				ZipArchiveEntry entry = archive.GetEntry(selectedFileSection);
				
				string outputFile = tempDirKnl+selectedFileSection;
				try{
					if(File.Exists(outputFile)){
						File.Delete(outputFile);
					}
					entry.ExtractToFile(outputFile);
					
					Process.Start(outputFile);
				}catch(Exception exept){
					MessageBox.Show(exept.Message);
				}
				
				archive.Dispose();
				zipStream.Close();
			}
		}
		void KnlFolderSelectorSelectedIndexChanged(object sender, EventArgs e)
		{
			if(KnlFolderSelector.SelectedIndex>=0){
				rutaKnlFiles = rootKnlData+KnlFolderSelector.SelectedItem.ToString()+"\\";
				loadFilesInListbox(KnlFileList);
			}
		}
		void SaveClipboardDataButtonClick(object sender, EventArgs e)
		{
			if(selectedKnl == null){
				MessageBox.Show("No se ha creado ni seleccionado un Knl");
				return;
			}
			String sectionName = Microsoft.VisualBasic.Interaction.InputBox("Insert section name","Section Name");
			if(!String.IsNullOrEmpty(sectionName)){
				if(sectionName.LastIndexOf('.')<=0 || sectionName.Substring(sectionName.LastIndexOf('.')).Length<=1)//there is no file extension
					sectionName=sectionName+((sectionName.LastIndexOf('.')==-1)?".txt":"txt");
				if(!SectionLister.Items.Contains(sectionName)){
					FileInfo zipFile = new FileInfo(selectedKnl);
					FileStream zipStream = zipFile.Open(FileMode.Open);
					ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Update);
					ZipArchiveEntry entryNew = archive.CreateEntry(sectionName);
					Stream zipStreamNew = entryNew.Open();
					var stream = new MemoryStream();
					var writer = new StreamWriter(stream);
					
					if(Clipboard.ContainsText())
						writer.Write(Clipboard.GetText());
					else if(Clipboard.ContainsImage()){
						Clipboard.GetImage().Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
					}
			
					
					writer.Flush();
					stream.Position = 0;
					stream.CopyTo(zipStreamNew);
					
					zipStreamNew.Close();
					stream.Close();
					archive.Dispose();
					zipStream.Close();
					
					SectionLister.Items.Add(sectionName);
					if(!isNewKnlFile)
						SaveLight.BackColor=Color.Lime;
				}else{
					MessageBox.Show("Section already exists","Error");
				}
			}else{
				MessageBox.Show("Invalid name","Error");
			}
		}
		void MetadataTextboxKeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)13)
		    {
		        //verificar que el texto no sea null empty
		        string metadata = MetadataTextbox.Text;
		        if(String.IsNullOrEmpty(metadata) || String.IsNullOrEmpty(knlId))
		        	return;
		        if(searchElementInListbox(MetadataListbox, metadata)){
		           	MessageBox.Show("Ya existe ese metadato","Info");
		           	return;
		           }
		           
		        //buscar el metadata en la db
		        //MYSQL_IMP//MySQLReader.openDBConn();
		        //MYSQL_IMP//string metametadataId = MySQLReader.getMetaMetadataId(metadata);
		        //sino existe agregarlo y retornar el ID
		        //MYSQL_IMP//if(String.IsNullOrEmpty(metametadataId)){
		        	//MYSQL_IMP//metametadataId = MySQLReader.insertMetaMetadataDB(metadata);
		        //MYSQL_IMP//}
		        //insertar el nuevo metadato
		        //MYSQL_IMP//if(!MySQLReader.insertMetadataDB(metametadataId, metadata, rutaKnlFiles, selectedKnl, knlId)){
		        	//MYSQL_IMP//MessageBox.Show("No fue posible insertar metadato");
		        //MYSQL_IMP//}else{
		        	//MYSQL_IMP//MetadataListbox.Items.Add(MetadataTextbox.Text);
		        //MYSQL_IMP//}
		        //MYSQL_IMP//MySQLReader.closeDBConn();
		        MetadataTextbox.Text="";
		    }
		}
		public bool searchElementInListbox(ListBox theList, string element){
			if(String.IsNullOrEmpty(element))
				return true;
			
			foreach(string curElem in theList.Items){
				if(curElem == element)
					return true;
			}
			return false;
		}
		
		void MetadataToolStripMenuItemClick(object sender, EventArgs e)
		{
			MetadataBox.Visible=!MetadataBox.Visible;
			MetadataBox.BringToFront();
		}
		void FilteredKnlListBoxDoubleClick(object sender, EventArgs e)
		{
			if(isRelationViewer){
				RelationLister.Items.Add(rutaKnlFiles+FilteredKnlListBox.Items[FilteredKnlListBox.SelectedIndex]);
				return;
			}
		}
		void KnlFileListDoubleClick(object sender, EventArgs e)
		{
			if(isRelationViewer){
				RelationLister.Items.Add(rutaKnlFiles+KnlFileList.Items[KnlFileList.SelectedIndex]);
				return;
			}
		}
	}
}
