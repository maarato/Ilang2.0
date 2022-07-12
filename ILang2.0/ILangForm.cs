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
using System.Runtime.Serialization.Formatters.Binary;

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
 * Hacer que pueda invocar archivos en CORE desde ILang. 
 * 
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
		
		public String knlFilename;
		public String selectedKnl;
		public String selectedFileSection;
		public ArrayList knlMetadata;
		public string knlId;
		public String tempKnlName;
		public int index;
		public bool isEditing;
		
		public bool sectionTextChangingFromList=false;
		
		public bool referenceMode;
		public bool isReferenceOpening; //esta var es para si abrimos el archivo desde otro knl para que no se ejecute selectKnlFile dos veces al cambiar el index del knlFileList
		
		public int ilangFileType = -1;
		
		public bool enableUnsave=false;
		public bool enableNewFile=false;
		public bool isNewKnlFile=false;
		public IntPtr parentHandle;
		public bool isAtomicKNl=false;
		public Dictionary<String,object> symbTable = new Dictionary<String,object>();
		public string[] ilangFileTypeList = new string[] {"Knl","Task","Problem"};
		
		public const int ILANG_FT_KNL = 0;
		public const int ILANG_FT_TASK = 1;
		public const int ILANG_FT_PROBLEM = 2;
		
		public ILangForm(int windowIndex,IntPtr parentHandle, string ILangFileToOpen)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			loadNewKnlPaths();
			
			this.parentHandle = parentHandle;
			tempKnlName="TempKnl"+windowIndex.ToString()+".tmp";//para si creamos multiples forms de ilang no haya problemas al querer crear varios KNL nuevos a la vez
			this.index=windowIndex;
			
			loadFilesInListbox(KnlFileList);
			
			referenceMode = ReferenceModeCheckbox.Checked;
			//Cargar los tipos de archivos
			IlangFileTypeSelector.Items.AddRange(ilangFileTypeList);
			
			if(ILangFileToOpen.Length>0){
				 ILangFileToOpen=ILangFileToOpen.Replace("\r","");
				 selectKnlFile(null, rootKnlData+ILangFileToOpen);
				 KnlEditor.Visible=!KnlEditor.Visible;
				 
				 //cambiar el root del lister
				 string receivedKnlPath = ILangFileToOpen.Substring(0,ILangFileToOpen.LastIndexOf('\\')+1);
				 changeKnlFolderPath(rootKnlData+receivedKnlPath);
				 isReferenceOpening = true; //seteamos la bandera para que el selectKnlFile invocado por el cambio del index del knlFileList se aborte 
				 int indexOfSelected = KnlFileList.Items.IndexOf(knlFilename);//aqui knlFilename ya se populo con el nombre del archivo
				 KnlFileList.SelectedIndex = indexOfSelected;
			}
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			SectionsTextbox.AcceptsTab=true;
			IlangTextbox.AcceptsTab=true;
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
		
		void callSaveSelectedSection(){
			//Guardar la seccion seleccionada en la lista de secciones
			if(!SectionLister.Items.Contains("IlangRelations.rel") && referenceMode){
				string textData = referenceMode ? ILangForm.listbox2String(RelationsLister): SectionsTextbox.Text;
				createNewFileWithData("IlangRelations","rel",textData);
			}
			
			if(SectionLister.SelectedIndex>=0){ 
				string textData = referenceMode ? ILangForm.listbox2String(RelationsLister): SectionsTextbox.Text;
				saveFileWithData(selectedKnl, textData);
			}
		}
		
		void SaveSectionClick(object sender, EventArgs e)
		{
			//Guardar la seccion seleccionada en la lista de secciones
			callSaveSelectedSection();
			SectionLister.Enabled=true;
		}
		
		bool createNewSectionWithSectionText(){
			if(selectedKnl == null){
				MessageBox.Show("No se ha creado ni seleccionado un Knl","Knl Exception");
				return false;
			}
			String sectionName = Microsoft.VisualBasic.Interaction.InputBox("Insert section name","Section Name");
			if(!String.IsNullOrEmpty(sectionName)){
				createNewFileWithData(sectionName,"txt", SectionsTextbox.Text);
				return true;
			}else{
				MessageBox.Show("Invalid name","Error");
				return false;
			}
		}
		
		void NewSectionClick(object sender, EventArgs e)
		{
			createNewSectionWithSectionText();
		}
		
		void activateEditMode(bool mode){
			if(!mode){
				SaveLight.BackColor = Color.Lime;
				isEditing=false;
				SaveKnlBtn.Enabled=isEditing;
				//if(SectionLister.SelectedIndex!=-1)
				SectionLister.Enabled=!isEditing;
			}else{
				SaveLight.BackColor = Color.Red;
				isEditing=true;
				SaveKnlBtn.Enabled=isEditing;
				//if(SectionLister.SelectedIndex!=-1)
				SectionLister.Enabled=!isEditing;
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
					if(!isNewKnlFile){
						activateEditMode(true);
					}
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
			//En caso de que el archivo sea tipo: .rel entonces ocultamos el SectionsTextbox y mostramos el RelationLister
			if(SectionLister.SelectedIndex>-1){
				SectionsTextbox.Text="";
				if(!isNewKnlFile)
					activateEditMode(false);
				
				ReferenceModeCheckbox.Checked = false;
				referenceMode = false;
				selectedFileSection= SectionLister.Items[SectionLister.SelectedIndex].ToString();
				String fileExt = selectedFileSection.Substring(selectedFileSection.LastIndexOf('.'));
				
				
				//IlangRelations
				SaveSection.Enabled=true;
				RenameSection.Enabled=true;
				RemoveSection.Enabled=true;
				if(selectedFileSection=="IlangRelations.rel"){
					RenameSection.Enabled=false;
					RemoveSection.Enabled=false;
				}
				//TODO ponerele un switch para saber el tipo de archivo
				
				FileInfo zipFile = new FileInfo(selectedKnl);
				FileStream zipStream = zipFile.OpenRead();
				ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Read);
				ZipArchiveEntry entry = archive.GetEntry(selectedFileSection);
				Stream stream = entry.Open();
				StreamReader reader = new StreamReader(stream);
				switch(fileExt){
					case ".rel":
						SectionsTextbox.Text = "";
						RelationsLister.Items.Clear();
						string fileContent = reader.ReadToEnd();
						string[] ilangRelationFiles = fileContent.Length>0? fileContent.Split('\n') : null ;
						if(ilangRelationFiles != null)
							RelationsLister.Items.AddRange(ilangRelationFiles);
						ReferenceModeCheckbox.Checked = true;
						referenceMode = true;
						break;
					case ".txt":
						SectionsTextbox.Text = reader.ReadToEnd();
						break;
					default:
						break;
				}
				sectionTextChangingFromList = true;
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
	            activateEditMode(false);
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
					newKnlName = newKnlName.Trim();
				}else{
					newKnlName = FileNameTextbox.Text.Replace(".zip","").Trim();
				}
				//poner aqui la  logica de ponerle el nombre al archivo en base al selector de archivo
				switch(ilangFileType){
					case ILANG_FT_KNL:
						//No hacer nada, es el caso default
						break;
					case ILANG_FT_PROBLEM:
						newKnlName = "P_" + newKnlName;
						break;
					case ILANG_FT_TASK:
						newKnlName = "T_" + newKnlName;
						break;
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
				activateEditMode(false);
				selectedKnl=rutaKnlFiles+newKnlName+".zip";
				if(FilteredKnlListBox.Visible){
					FilteredKnlListBox.Visible=false;
					if(KnlFileList.Items.Contains(newKnlName+".zip")){
						KnlFileList.SelectedIndex = KnlFileList.Items.IndexOf(newKnlName+".zip");
					}
				}else if(KnlFileList.Items.Contains(newKnlName+".zip")){
					KnlFileList.SelectedIndex = KnlFileList.Items.IndexOf(newKnlName+".zip");
				}
				if(String.IsNullOrEmpty(knlId)){
					//MYSQL_IMP//MySQLReader.openDBConn();
					//MYSQL_IMP//knlId=MySQLReader.insertDB(selectedKnl, rutaKnlFiles, rutaKnlFiles+newKnlName+".zip");
					KnlIdLabel.Text="KnlId: "+knlId;
					//MYSQL_IMP//MySQLReader.closeDBConn();
				}
			}else{
				//En caso de que no sea un archivo nuevo y se tenga una seccion seleccionada, lo guardamos.
				if(SectionLister.SelectedIndex==-1){
					createNewSectionWithSectionText();
				}
				callSaveSelectedSection();
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
					if(!isNewKnlFile){
						activateEditMode(false);
					}
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
			//if(enableUnsave && SectionsTextbox.Text.Length>0 && !sectionTextChangingFromList){
			//	activateEditMode(true);
			//}
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
			if(isEditing){
				if(MessageBox.Show("A file is being edited, delete it and create a new one?", "Create new Ilang File", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No){
					return;
				}
			}
			//disable non new Section actions buttons
			SaveSection.Enabled=false;
			RenameSection.Enabled=false;
			RemoveSection.Enabled=false;
				
			IlangFileTypeSelector.SelectedIndex = ILANG_FT_KNL;
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
			//Crear aqui el archivo de relaciones
			createNewFileWithData("IlangRelations","rel","");
			activateEditMode(true);
			SectionLister.Enabled=true;
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
				activateEditMode(true);
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
			selectKnlFile(ListWithKnls, "");
		}
		
		void selectKnlFile(ListBox ListWithKnls, string specificFileToOpen){
			if(referenceMode || isReferenceOpening) {
				isReferenceOpening = false;
				return;
			}
			if(isEditing && MessageBox.Show("A file is being edited, ignore and open the selected file?", "Ignore editing", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No){
				return;
			}else{
				if(File.Exists(tempKnlName)){
					File.Delete(tempKnlName);
				}
				if( (ListWithKnls!= null && ListWithKnls.SelectedIndex>=0) || specificFileToOpen.Length>0){
					//disable non new Section actions buttons
					SaveSection.Enabled=false;
					RenameSection.Enabled=false;
					RemoveSection.Enabled=false;
					
					activateEditMode(false);
					enableNewFile=false;
					enableUnsave=false;
					isNewKnlFile=false;
					knlId=null;
					clearTexts();
					SectionLister.Items.Clear();
					RelationsLister.Items.Clear();
					MetadataListbox.Items.Clear();
					selectedKnl=specificFileToOpen.Length>0?specificFileToOpen:rutaKnlFiles+ListWithKnls.Items[ListWithKnls.SelectedIndex].ToString();
					knlFilename=selectedKnl.Substring(selectedKnl.LastIndexOf('\\')+1);
					FileNameTextbox.Text=knlFilename;
					switch(selectedKnl.Substring(selectedKnl.LastIndexOf('.')+1)){
						case "knl":
							isAtomicKNl=true;
							break;
						case "zip":
							isAtomicKNl=false;
							switch(selectedKnl.Substring(selectedKnl.LastIndexOf("\\")+1,2)){
								case "T_":
									IlangFileTypeSelector.SelectedIndex=ILANG_FT_TASK;	
									break;
								case "P_":
									IlangFileTypeSelector.SelectedIndex=ILANG_FT_PROBLEM;	
									break;
								default:
									IlangFileTypeSelector.SelectedIndex=ILANG_FT_KNL;
									break;
							}
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
							MessageBox.Show("Desconocido o la ruta de la referencia esta incompleta","Text");
							break;
					}
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
			/*if(enableNewFile){
				createNewKnlFromCopy();
				isNewKnlFile=true;
			}*/
		}
		void FileNameTextboxClick(object sender, EventArgs e)
		{
			enableNewFile=true;
		}
		void IlangTextboxTextChanged(object sender, EventArgs e)
		{
			if(enableUnsave){
				activateEditMode(true);
			}
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
				changeKnlFolderPath(rutaKnlFiles+KnlFolderSelector.SelectedItem.ToString()+"\\");
			}
		}
		
		public void changeKnlFolderPath (string newPath) {
			rutaKnlFiles = newPath;
			loadFilesInListbox(KnlFileList);
			loadNewKnlPaths();
		}
		
		void SaveClipboardDataButtonClick(object sender, EventArgs e)
		{
			if(selectedKnl == null){
				MessageBox.Show("No se ha creado ni seleccionado un Knl");
				return;
			}
			String sectionName = Microsoft.VisualBasic.Interaction.InputBox("Insert section name","Section Name");
			if(!String.IsNullOrEmpty(sectionName)){
				bool isText = true; //En un futuro cambiar por un INT para detectar entre imagenes, sonido, texto, etc.
				if(Clipboard.ContainsText())
					isText=true;
				else if(Clipboard.ContainsImage()){
					isText=false;
				}
				if(sectionName.LastIndexOf('.')<=0 || sectionName.Substring(sectionName.LastIndexOf('.')).Length<=1){//there is no file extension
					if(isText)
						sectionName=sectionName+((sectionName.LastIndexOf('.')==-1)?".txt":"txt");
					else
						sectionName=sectionName+((sectionName.LastIndexOf('.')==-1)?".jpg":"jpg");
				}
				if(!SectionLister.Items.Contains(sectionName)){
					FileInfo zipFile = new FileInfo(selectedKnl);
					FileStream zipStream = zipFile.Open(FileMode.Open);
					ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Update);
					ZipArchiveEntry entryNew = archive.CreateEntry(sectionName);
					Stream zipStreamNew = entryNew.Open();
					var stream = new MemoryStream();
					var writer = new StreamWriter(stream);
					
					if(isText)
						writer.Write(Clipboard.GetText());
					else{
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
					if(!isNewKnlFile){
						activateEditMode(false);
					}
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
		}
		
		void IlangFileTypeSelectorSelectedIndexChanged(object sender, EventArgs e)
		{
			ilangFileType = IlangFileTypeSelector.SelectedIndex;
		}
		
		void ReferenceModeCheckboxCheckedChanged(object sender, EventArgs e)
		{
			referenceMode = ReferenceModeCheckbox.Checked;
			RelationsLister.Visible=referenceMode;
		}
		
		void FilteredKnlListBoxDoubleClick(object sender, EventArgs e)
		{
			addReferenceFile(FilteredKnlListBox);
		}
		
		void KnlFileListMouseDoubleClick(object sender, MouseEventArgs e)
		{
			addReferenceFile(KnlFileList);
		}
		
		protected void addReferenceFile(ListBox fileListBox) {
			if (!RelationsLister.Items.Contains(fileListBox.Items[fileListBox.SelectedIndex].ToString())){
				RelationsLister.Items.Add(rutaKnlFiles.Replace("data\\","")+fileListBox.Items[fileListBox.SelectedIndex].ToString());
				activateEditMode(true);
			}
		}
		
		protected void saveFileWithData(string fileName, string textData){
			FileInfo zipFile = new FileInfo(fileName);
			FileStream zipStream = zipFile.Open(FileMode.Open);
			ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Update);
			ZipArchiveEntry oldEntry = archive.GetEntry(selectedFileSection);
			oldEntry.Delete();
			archive.Dispose();
			zipStream.Close();
			
			FileInfo zipFileSv = new FileInfo(fileName);
			FileStream zipStreamSv = zipFileSv.Open(FileMode.Open);
			ZipArchive archiveSv = new ZipArchive(zipStreamSv, ZipArchiveMode.Update);
			ZipArchiveEntry entryNew = archiveSv.CreateEntry(selectedFileSection);
			Stream zipStreamNew = entryNew.Open();
			var stream = new MemoryStream();
			var writer = new StreamWriter(stream);
			writer.Write(textData);
			writer.Flush();
			stream.Position = 0;
			stream.CopyTo(zipStreamNew);
			
			zipStreamNew.Close();
			stream.Close();
			archiveSv.Dispose();
			zipStreamSv.Close();
			if(!isNewKnlFile){
				activateEditMode(false);
			}
		}
		
		protected void createNewFileWithData(string sectionName, string extension, string textData){
			if(sectionName.LastIndexOf('.')<=0 || sectionName.Substring(sectionName.LastIndexOf('.')).Length<=1)//there is no file extension
				sectionName=sectionName+((sectionName.LastIndexOf('.')==-1)?"."+extension:extension);
			if(!SectionLister.Items.Contains(sectionName)){
				FileInfo zipFile = new FileInfo(selectedKnl);
				FileStream zipStream = zipFile.Open(FileMode.Open);
				ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Update);
				ZipArchiveEntry entryNew = archive.CreateEntry(sectionName);
				Stream zipStreamNew = entryNew.Open();
				var stream = new MemoryStream();
				var writer = new StreamWriter(stream);
				writer.Write(textData);
				writer.Flush();
				stream.Position = 0;
				stream.CopyTo(zipStreamNew);
				
				zipStreamNew.Close();
				stream.Close();
				archive.Dispose();
				zipStream.Close();
				
				SectionLister.Items.Add(sectionName);
				if(!isNewKnlFile){
					activateEditMode(false);
				}
				SectionLister.Enabled=true;
				SectionsTextbox.Text = "";
			}else{
				MessageBox.Show("Section already exists","Error");
			}
		}
		
		public static string listbox2String( ListBox theListbox ){
		
			string listedString="";
			
			foreach (var ilangRel in theListbox.Items){
				if(listedString.Length==0)
					listedString = ilangRel.ToString();
				else
					listedString = listedString + "\r\n" + ilangRel.ToString();
			}
			return listedString;
			
		}
		
		void RelationsListerDoubleClick(object sender, EventArgs e)
		{
			MainForm.mainFormSigleton.callNewIlang(RelationsLister.Items[RelationsLister.SelectedIndex].ToString());
		}
		
		void RelationsListerSelectedIndexChanged(object sender, EventArgs e)
		{
			
		}
		
		
		void RelationsListerKeyUp(object sender, KeyEventArgs e)
		{
			escapeFromEditMode(e);
			if(e.KeyCode == Keys.Delete){
				if(MessageBox.Show("Delete this relation?", "Delete relation", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes){
					RelationsLister.Items.RemoveAt(RelationsLister.SelectedIndex);
				}
			}
		}
		
		void CreateNewToolStripMenuItemClick(object sender, EventArgs e)
		{
			string newContextPath= Microsoft.VisualBasic.Interaction.InputBox("Insert context name","Context Name");;
			if (!Directory.Exists(newContextPath))
			{
			    Directory.CreateDirectory(rutaKnlFiles+newContextPath);
			    KnlFolderSelector.Items.Add(newContextPath);
			}
		}
		
		void loadNewKnlPaths(){
			KnlFolderSelector.Items.Clear();
			DirectoryInfo directory = new DirectoryInfo(rutaKnlFiles);
			DirectoryInfo[] directories = directory.GetDirectories();
			KnlFolderSelector.Items.Add("..\\");
			for (int i = 0; i < directories.Length; i++)
			{
				KnlFolderSelector.Items.Add(((DirectoryInfo)directories[i]).FullName.Substring(((DirectoryInfo)directories[i]).FullName.LastIndexOf("\\")+1));
			}
		}
		
		void escapeFromEditMode(KeyEventArgs e){
			if(e.KeyCode == Keys.Escape){
				if(isEditing){
					if(SectionLister.Enabled || MessageBox.Show("Section had some changes, ignore?", "Ignore changes", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes){
						SaveSection.Enabled=false;
						RenameSection.Enabled=false;
						RemoveSection.Enabled=false;
						if(!isNewKnlFile)
							activateEditMode(false);
						else
							SectionLister.Enabled=true; //cuando es un knl nuevo no desctivamos el edit mode porque el knl general aun no se guarda
						SectionLister.SelectedIndex=-1;
						RelationsLister.Visible=false;
					}
				}else{
					SaveSection.Enabled=false;
					RenameSection.Enabled=false;
					RemoveSection.Enabled=false;
					activateEditMode(false);
					SectionLister.SelectedIndex=-1;
					RelationsLister.Visible=false;
				}
			}
		}
		
		void SectionListerKeyUp(object sender, KeyEventArgs e)
		{
			escapeFromEditMode(e);
		}
		
		void SectionsTextboxKeyPress(object sender, KeyPressEventArgs e)
		{
			activateEditMode(true);
		}
		
		
		void SectionsTextboxKeyUp(object sender, KeyEventArgs e)
		{
			escapeFromEditMode(e);
		}
		
		void TranspileToolStripMenuItemClick(object sender, EventArgs e)
		{
			ILangHTMLTranspiler myTranspiler = new ILangHTMLTranspiler(SectionsTextbox.Text, this);
			string htmlTranspilerTemplate = System.IO.File.ReadAllText("./HTMLTranspilerTemplate.txt");
			htmlTranspilerTemplate = htmlTranspilerTemplate.Replace("<$arg1>",myTranspiler.transpileText());
			saveFile(htmlTranspilerTemplate,"./HtmlTranspiled.html");
			System.Diagnostics.Process.Start(new ProcessStartInfo
		    {
		        FileName = "HtmlTranspiled.html",
		        UseShellExecute = true
		    });
			
		}

		
		public bool saveFile(string textToSave, string path){
            StreamWriter escrito = File.CreateText(path); // en el 
            String contenido = textToSave;
            escrito.Write(contenido.ToString());
            escrito.Flush();
            escrito.Close();
            return true;
		}
		
		public string getImageFromFileBase64(string imageName){
			if(SectionLister.Items.Contains(imageName)){
				//Image imageToConvert = 
				
				FileInfo zipFile = new FileInfo(selectedKnl);
				FileStream zipStream = zipFile.OpenRead();
				ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Read);
				ZipArchiveEntry entry = archive.GetEntry(imageName);
				
				string outputFile = tempDirKnl+imageName;
				try{
					if(File.Exists(outputFile)){
						File.Delete(outputFile);
					}
					entry.ExtractToFile(outputFile);
					archive.Dispose();
					zipStream.Close();
					Image image = new Bitmap(outputFile);
					using (MemoryStream ms = new MemoryStream())
			        {
			            image.Save(ms, image.RawFormat);
			            return Convert.ToBase64String(ms.ToArray());
			        }
					
				}catch(Exception exept){
					MessageBox.Show(exept.Message);
				}
				
			}
			return "";
		}
			
		void InsertBase64ImageToolStripMenuItemClick(object sender, EventArgs e)
		{
			
		}
	}
}
