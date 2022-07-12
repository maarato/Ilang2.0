/*
 * Created by SharpDevelop.
 * User: alejandro.rangel
 * Date: 29/10/2019
 * Time: 03:03 p. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.IO;
using System.Diagnostics;

using System.Threading;
using System.Net;
using System.Net.Sockets;

using System.Runtime.InteropServices;
/*
  * Descripcion general
  * las lineas que tienen un "#" al inicio es porque debo planear en cambiar su logica.
  * 
  * El MainForm crea al iniciarse otro hilo que es el que abre el socket y esta escuchando las conexiones
  * Este hilo no crea las conexiones, solo escucha y las acepta. se bloquea el hilo hasta que alguien solicita conectarse
  * Luego el MainForm desde el listBox con los scripts de python manda llamar a cada script individual para ejecutarlo
  * Al ejecutarse se le pasa como parametros el Socket del Core y su ID de Hilo (definido por el MainForm, es el nombre del archivo sin extencion y un numero incrementable)
  * 	Cuando el Script se ejecuta el MainForm crea un hilo, el cual va a ser el que estara escuchando y enviando datos a ese Script particular
  * 		#Este hilo nuevo su primer bloqueo sea para esperar datos del Cliente (Script), no avanzara hasta no recibir datos.
  * 			Debe haber una adicion en esta logica que es, si el Cliente no envia datos de inicio (handshake) entonces el hilo se finalice
  * 		#Luego de recibir los datos enviara un "ok" y procede a esperar mas datos.
  * 			Este paso es para coordinar ambos procesos Cliente-Servidor, al responer el servidor con un "ok" el Cliente se da cuenta que puede proceder con sus procedimientos y envios de datos
  * 	Cuando el Script envia señal de terminado, que es cuando se cierra la conexion desde el Cliente (Script) entonces el hilo se termina
  * 	
  * Cuando se ejecuta un bloque y este da error por el motivo que sea, se captura el stderr del proceso y se mostrara como respuesta es decir se almacenara en las listas como:
  * 	NombreBloque.ext	texto_error_capturado
  * Crear la arquitectura de manejo de archivos flujo.
  * 	Los archivos flujos son los que indican que bloques se llamaran y el orden, el como se pasaran los datos de unos a otros.
  * 
  * */
	
 
 
 /*
   * ############CAMBIOS FUTUROS############
   * Agregar cores dinamicos, es decir, como la ejecucion la hago de la forma startProcesInfo = new ProcesInfo(ejecutable, argumentos),
   * 	entonces hacer que el ejecutable y el args sean datos dinamicos, por ejemplo el ejecutable puede ser python, o miInterprete.exe, y el argumento es el script con el codigo, o vacio en caso de que el ejecutable sea un compilado
   * 		A diferencia de como esta ahorita que los cores de los lenguajes estan definidos a nivel codigo en vez de nivel propiedades. la idea es tener un archivo donde Core lea la equivalencia de para que archivos es que interprete o ejecutable y la forma en que lo debe ejecutar 
   * Organizar los archivos de bloques en carpetas contextos y especialidades, por ejemplo, los bloques grises, amarillo, naranjas, etc.
   * 	eso implica no hardcodear el c:\pyscript
   * Crear el lenguaje ILang o CORE que se encargara de crear y manejar los flujos, por ejemplo, saber que bloque llama a que otro bloque, etc.
   * 	El lenguaje core debe tener especialidad en manejo de bloques.
   * Crear el apartado de Instintivos, que sera una carateristica donde se programaran para realizar acciones como, recibir un array de objetos de python y almacenarlo de esa manera en el core
   * 	estas interfaces internas del nucleo se encargaran de la compatibilidad de datos enstre Cores. por ejemplo que los objetos de python sean legibles por el nucleo de perl. 
   * 
   * Crear los bloques bases. Algunos serian los siguientes:
   * 	Manejo de archivos, para almacenar conocimiento y datos
   * 	Manejo de base de datos para mantener la relacion de datos y bloques.
   * 		crear los bloques en python que lean, inserten, modifiquen, eliminen datos de una base de datos mysql donde se almacenaran las relaciones entre bloques y aqui podrian incluso estar los flujos y otros bloques.
   * 			Teniendo tablas como: bloques (id int, name varchar, extension/core varchar, code blob...)
   * 			Otra tabla que seria: arguments (id int, knlID int, name varchar, ...)
   * 	Control de los medio del dispocitivo, por ejemplo, mover el mouse, abrir o cerrar ventanas, escribir o enviar comandos de teclado
   * Definir una estructura y crear un manual de uso, un manual de las estructuras para evitar caer en confusiones y caminos cerrados cuando quiero implementar mas partes
   * Definir conocimientos estructurales, que son mas que nada para el usuario (y futuramente para el mismo Core) donde indique la estructura de los bloques, variables de entrada, donde muestre visualmente un flujo. etc.
   * 
   * ===IMPORTANTES===
   * Diseñar o adaptar la forma que el programador se pueda usar tambien.
   * 
   * FACILITAR LA FORMA EN QUE SE CREAN LOS ELEMENTOS DEL KNL PARA PODER INCLUIR CONTENIDO MAS Y MAS RELEVANTE
   * al igual que con el Ilang1.3 hacer que haya configuraciones de entorno, contexto, y/o al abrir un knl. 
   * HACER QUE SE PUEDAN TENER MULTIPLES INSTANCIAS DE UN MISMO BLOQUE EJECUTANDOSE.
   * 	Por ejemplo: multiples tablas de simbolos y cada una con un ID Diferente
   *	 	por lo tanto se debe tener almacenados los IDs de cada bloque particular repetido ejecutado
   * Ver como mantener el core de python corriendo para que no se ejecute una y otra vez 
   * */


namespace ILang2._
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	/// 

		
	public partial class SubjugatorCore : Form
	{
		Thread cCon,stdErrMon, stdOutMon;
		public int socketMaster=3340;
		public ArrayList bloqsThread;
		public ArrayList bloqsKeys;
		public ArrayList tableNameS;
		public ArrayList tableValuesS;
		public ArrayList procStarted;
		public ArrayList procNamesStarted;
		Socket listener; //listener de peticiones
		
		public String ruta="";
		public String rutaPrincipal = "C:\\pyscripts\\";
		public string selectedBloq;
		long bloqCount=0;
		int isFlowRunning = 0;
		public string selectedBloqExt;
		IntPtr parentPtr;
		
		private Object _selectedMenuItem;
		private readonly ContextMenuStrip collectionRoundMenuStrip;
		private readonly ContextMenuStrip collectionRoundMenuStripBloqs;
		private readonly ContextMenuStrip collectionRoundMenuStripProcs;
		
		[DllImport("user32.dll", SetLastError = true)]
		private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
		
		
		public SubjugatorCore(int socketMaster,IntPtr parentPtr)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();	
			ruta=rutaPrincipal;
			DirectoryInfo directory = new DirectoryInfo(ruta);
			DirectoryInfo[] directories = directory.GetDirectories();
			KnlPathsCombo.Items.Add("C:\\pyscripts\\");
			for (int i = 0; i < directories.Length; i++)
			{
				KnlPathsCombo.Items.Add(((DirectoryInfo)directories[i]).FullName);
			}
			
			this.parentPtr = parentPtr;
			this.socketMaster = socketMaster;
			bloqsThread = ArrayList.Synchronized(new ArrayList());
			bloqsKeys = ArrayList.Synchronized(new ArrayList());
			tableNameS = ArrayList.Synchronized(new ArrayList());
			tableValuesS = ArrayList.Synchronized(new ArrayList());
			procStarted = ArrayList.Synchronized(new ArrayList());
			procNamesStarted = ArrayList.Synchronized(new ArrayList());
			
			//Creating the Context Menu for Main Bloq view 
			var toolStripInsertBeforeMainBloq = new ToolStripMenuItem {Text = "Edit Script"};
		    toolStripInsertBeforeMainBloq.Click += toolStripMainInsertBefore_Click;
		    var toolStripRenameScript = new ToolStripMenuItem {Text = "Rename Script"};
		    toolStripRenameScript.Click += toolStripRenameScript_Click;
		    var toolStripEditHere = new ToolStripMenuItem {Text = "Edit Here"};
		    toolStripEditHere.Click += toolStripEditHere_Click;
			var toolStripRefresh = new ToolStripMenuItem {Text = "Refresh"};
		    toolStripRefresh.Click += ReloadBloqListToolStripMenuItemClick;
		    
		    
		    collectionRoundMenuStrip = new ContextMenuStrip();
		    collectionRoundMenuStrip.Items.AddRange(new ToolStripItem[] {
		                                            	toolStripInsertBeforeMainBloq,
		                                            	toolStripRenameScript,
		                                            	toolStripEditHere,
		                                            	toolStripRefresh
		                                            		});
		    
		    //Creating the Context Menu for Main Bloq view 
			var toolStripSendMessageToBloq = new ToolStripMenuItem {Text = "Send Message"};
		    toolStripSendMessageToBloq.Click += toolStripSendMessageToBloq_Click;
		    var toolStripStopBloq = new ToolStripMenuItem {Text = "Stop Thread"};
		    toolStripStopBloq.Click += toolStripStopBloq_Click;
		    
		    collectionRoundMenuStripBloqs = new ContextMenuStrip();
		    collectionRoundMenuStripBloqs.Items.AddRange(new ToolStripItem[] {
		                                            	toolStripSendMessageToBloq,
		                                            	toolStripStopBloq
		                                            		});
		    
		      //Creating the Context Menu for Main Bloq view 
		    var toolStripStopProc = new ToolStripMenuItem {Text = "Stop Proc"};
		    toolStripStopProc.Click += toolStripStopProc_Click;
		    
		    collectionRoundMenuStripProcs = new ContextMenuStrip();
		    collectionRoundMenuStripProcs.Items.AddRange(new ToolStripItem[] {
		                                            	toolStripStopProc
		                                            		});
		    
		    
		    loadFilesInListbox(ScriptsList);
			
			cCon=new Thread(controlarConexiones);
			cCon.Start();
			cCon.IsBackground=true;
			
			addNewSymbol("SocketMaster",this.socketMaster.ToString());
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		public void loadFilesInListbox(ListBox list){
			list.Items.Clear();
			string[] ficheros=Directory.GetFiles(ruta);
			for (int i = 0; i < ficheros.Length; i++){
				list.Items.Add(ficheros[i]); 
			}
		}
		
		
		public void toolStripStopProc_Click(object sender, EventArgs e){
			if (ProcNamesListbox.SelectedIndex != -1){
				DialogResult dialogResult = MessageBox.Show("Stop the Process?","Stop Process",MessageBoxButtons.YesNo);
				if(dialogResult == DialogResult.Yes){
					Process theProc = ((Process)procStarted[ProcNamesListbox.SelectedIndex]);
					if(!theProc.HasExited){
						theProc.Kill();
					}
					procStarted.RemoveAt(ProcNamesListbox.SelectedIndex);
					procNamesStarted.RemoveAt(ProcNamesListbox.SelectedIndex);
					ProcNamesListbox.Items.RemoveAt(ProcNamesListbox.SelectedIndex);
				}
			}
		}	
			
		public void toolStripSendMessageToBloq_Click(object sender, EventArgs e){
			if (BloqKeysListbox.SelectedIndex != -1){
				String messageToSend = Microsoft.VisualBasic.Interaction.InputBox("Message To send","Send Message");
				if(string.IsNullOrEmpty(messageToSend))
				   return;
				Bloq theBloq = ((Bloq)bloqsThread[BloqKeysListbox.SelectedIndex]);
				theBloq.externalOrder = messageToSend;
			}
		}	
		
		public void toolStripStopBloq_Click(object sender, EventArgs e){
			if (BloqKeysListbox.SelectedIndex != -1){
				try{
				Bloq theBloq = ((Bloq)bloqsThread[BloqKeysListbox.SelectedIndex]);
				theBloq.myConn.Abort(null);
				bloqsThread.RemoveAt(BloqKeysListbox.SelectedIndex);
				bloqsKeys.RemoveAt(BloqKeysListbox.SelectedIndex);
				}
				catch(Exception ex){
					MessageBox.Show(ex.ToString());
				}finally{
					if(ProcListbox.Items.Count > 0)
						ProcListbox.Items.RemoveAt(BloqKeysListbox.SelectedIndex);
					BloqKeysListbox.Items.RemoveAt(BloqKeysListbox.SelectedIndex);
				}
				
			}
		}	
		
		public void toolStripMainInsertBefore_Click(object sender, EventArgs e){
			if (ScriptsList.SelectedIndex != -1){
				Process process = new Process();
				process.StartInfo = new ProcessStartInfo("\"C:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\Notepad++.lnk\"","\""+selectedBloq+"\"");
				process.Start();
				Thread.Sleep(100);
            	SetParent(process.MainWindowHandle, parentPtr);
			}
		}	
		public void toolStripEditHere_Click(object sender, EventArgs e){
			if (ScriptsList.SelectedIndex != -1){
				StreamReader leido = File.OpenText(selectedBloq);
	            string contenido = null;
	            SCCodeEditor.Text="";
	            EditorFileName.Text = selectedBloq;
	            if(selectedBloq.IndexOf(".")>=0)
	            	selectedBloqExt = selectedBloq.Substring(selectedBloq.LastIndexOf("."));
	            else 
	            	selectedBloqExt = "";
	            while ((contenido = leido.ReadLine()) != null)
	            {
	                SCCodeEditor.Text += contenido.ToString()+"\r\n";
	            }
	            leido.Close();
	            EditorBoxGroup.Visible = true;
			}
		}
			
		public void toolStripRenameScript_Click(object sender, EventArgs e){
		if (ScriptsList.SelectedIndex != -1){
				string currentPath = selectedBloq;
				string currentName = currentPath.Substring(currentPath.LastIndexOf('\\')+1);
				currentPath = currentPath.Substring(0,currentPath.LastIndexOf('\\'));
				string newName = Microsoft.VisualBasic.Interaction.InputBox("Inserte nuevo nombre","Inserte dato solicitado",currentName);
				
				if(!string.IsNullOrEmpty(newName)){
					System.IO.File.Move(currentPath+"\\"+currentName, currentPath+"\\"+newName);
					loadFilesInListbox(ScriptsList);
						
				}
					
			}
		}
		
		void ListBox1DoubleClick(object sender, EventArgs e)
		{
			runSelectedScript(ScriptsList);
		}
		
		public void runSelectedScript(ListBox selectedListBox){
			if(selectedListBox.SelectedIndex>=0){
				string fileRuta=selectedBloq;
				string[] parts=fileRuta.Split('\\');
				//string threadID=parts[parts.Length-1]+bloqCount.ToString();
				ejecutarBloq(parts[parts.Length-1],socketMaster.ToString(), false);
			}
		}
		
		public int digerirArchivo(string filename, int isFlowRunning){
			string fileText = System.IO.File.ReadAllText(filename);
			string[] fileLines = fileText.Split('\n');
			int argCount = 0, arglinesCount = 0;
			int digerir = 0;
			fileText = fileText.Replace("<$arg0>",socketMaster.ToString());
			fileText = fileText.Replace("<$arg$>",isFlowRunning>0 ? 1+"":0+"");
			foreach (string line in fileLines){
				arglinesCount++;
				if(!string.IsNullOrEmpty(line)){
					if(line[0]=='#' && line [1]=='-' && line[2]=='>'){ //procesar args
						digerir=1;
						argCount++;
						string textToShow = line.Substring(3,line.Substring(3).IndexOf("#"));
						
						string defaultVal;
						if(line.Substring(3).IndexOf("#")>=0)
							defaultVal = line.Substring(3).Substring(line.Substring(3).IndexOf("#")+1).Replace("\r","");
						else
							defaultVal="";
						
						if(isFlowRunning>0){
							if(Bloq.argsToSend.Count>0){
			            			string argToSend = Bloq.argsToSend.Dequeue().ToString();
			            			defaultVal= Bloq.getValueFromListBox(this.tableNameS,this.tableValuesS,argToSend,isFlowRunning>0);
							}else{
								defaultVal = Bloq.getValueFromListBox(this.tableNameS,this.tableValuesS,defaultVal,isFlowRunning>0);
							}
							fileText = fileText.Replace("<$arg"+argCount+">",defaultVal);
						}else{
							fileText = fileText.Replace("<$arg"+argCount+">",Bloq.getValueFromListBox(this.tableNameS,this.tableValuesS,defaultVal,isFlowRunning>0));
						}
						fileText = fileText.Replace(line,"");
					}else{
						if(digerir == 0)
							return digerir;
						break;
					}
				}
			}
			if(filename.Substring(filename.LastIndexOf('.'))==".cmd" || filename.Substring(filename.LastIndexOf('.'))==".bat"){
				if(filename.Substring(filename.LastIndexOf('.'))==".cmd"){
					filename=filename.Substring(0,filename.LastIndexOf("\\"))+"\\_c_except.cmd";
				}else{
					filename=filename.Substring(0,filename.LastIndexOf("\\"))+"\\_c_except.bat";
				}
			}
			digerir = 1;
			bool terminado=false;
			do{
				if(File.Exists(filename+digerir)){
					digerir+=1;
				}
				else{
					terminado=true;
				}
			}while(!terminado);
			saveFile(fileText,filename+digerir); //guardamos el archivo con X al final (execute)
			return digerir;
		}
		
		public Process ejecutarBloq(string fileName,string puertoMaster, bool isFlow) 
        {
			//Esta funcion retorna null si el fileName esta vacio, o si no tiene extension (No sabemos que core debe usar)
			if (fileName=="" || fileName.IndexOf('.') < 0)//necesario porque al dar dobleclick en cualquier parte del list se ejecuta este metodo 
				return null;
			string BloqID=fileName.Substring(0,fileName.IndexOf('.'));
			bloqCount++;
			fileName=ruta+fileName;
			string[] parts=fileName.Split('\\');
			string threadID=parts[parts.Length-1];//+bloqCount.ToString();//nombre del archivo
			
			clearBloqStdVariables(threadID);
			//CHG0001 - Get the StdOut y StdErr del proceso ejecutado
			Process process = new Process();
			
			try{
				if(threadID.LastIndexOf('.')==-1){
					addNewSymbol(threadID,"Error - No hay Core para este tipo de archivos");
					return null;
				}
				int numOfInstance;
				switch(threadID.Substring(threadID.LastIndexOf('.'))){
					case ".js":
						numOfInstance=digerirArchivo(fileName, isFlowRunning);
						if(numOfInstance>0){
							fileName=fileName+numOfInstance;
						}
						process.StartInfo = new ProcessStartInfo("node",fileName);
						break;
					case ".mjs": //no sirve y aun no se porque
						numOfInstance=digerirArchivo(fileName, isFlowRunning);
						if(numOfInstance>0){
							fileName=fileName+numOfInstance;
						}
						process.StartInfo = new ProcessStartInfo("\"C:\\Program Files\\MongoDB\\Server\\4.4\\bin\\mongo.exe\"",(" <"+fileName).Replace("\"", ""));
						break;
					case ".py":
						numOfInstance=digerirArchivo(fileName, isFlowRunning);
						if(numOfInstance>0){
							fileName=fileName+numOfInstance;
						}
						process.StartInfo = new ProcessStartInfo("python",fileName);
						break;
					case ".pl":
						numOfInstance=digerirArchivo(fileName, isFlowRunning);
						if(numOfInstance>0){
							fileName=fileName+numOfInstance;
						}
						process.StartInfo = new ProcessStartInfo("\"G:\\Proyectos\\Analizador Sintactico\\PerlPortable\\Perl\\bin\\perl.exe\"",fileName);
						break;
					case ".exe":
						process.StartInfo = new ProcessStartInfo(fileName);
						break;
					case ".cmd":
						numOfInstance=digerirArchivo(fileName, isFlowRunning);
						if(numOfInstance>0){
							fileName=ruta+"_c_except.cmd";
						}
						process.StartInfo = new ProcessStartInfo("cmd",fileName);
						break;
					case ".bat":
						numOfInstance=digerirArchivo(fileName, isFlowRunning);
						if(numOfInstance>0){
							fileName=ruta+"_c_except.bat";
						}
						process.StartInfo = new ProcessStartInfo(fileName);
						break;
					case ".flw":
						
						string[] flowLines = System.IO.File.ReadAllText(fileName).Split('\n');
						Thread flowProcess = new Thread(() => processFlow(threadID, flowLines, puertoMaster, this));
						flowProcess.Start();
						flowProcess.IsBackground = true;
						
						addNewSymbol(threadID,"Flujo ejecutado ["+threadID+"]");
						return null;
					default:
						addNewSymbol(threadID,"Error - No hay Core para este tipo de archivos");
						return null;
				}
				//process.StartInfo.FileName = filepath;
				process.StartInfo.UseShellExecute = false;
				process.StartInfo.RedirectStandardError = true;
				process.StartInfo.RedirectStandardOutput = true;
				process.StartInfo.CreateNoWindow = true;
				process.Start();
				Thread.Sleep(100);
				if(!process.HasExited){
            		//SetParent(process.MainWindowHandle, this.Handle);
            		procStarted.Add(process);
            		bloqsKeys.Add(BloqID);
            		procNamesStarted.Add(fileName);
				}
			}catch(Exception e){
				MessageBox.Show(e.Message);
				MessageBox.Show(e.StackTrace);
				return null;
			}
			stdErrMon = new Thread(()=>captureStandardError(process,threadID)); //uson labda expression porque de esta manera se le puede pasar parametros a una funcion dentro de thread
			stdErrMon.Start(); 	
			stdErrMon.IsBackground=true;
			
			stdOutMon = new Thread(()=>captureStandardOutput(process,threadID));
			stdOutMon.Start();
			stdOutMon.IsBackground=true;
			
			addConnection(threadID,puertoMaster);
			
			return process;
        } 
		public bool saveFile(string textToSave, string path){
            StreamWriter escrito = File.CreateText(path); // en el 
            String contenido = textToSave;
            escrito.Write(contenido.ToString());
            escrito.Flush();
            escrito.Close();
            return true;
		}
		
		
		public object[] getArgsFromLine(string line){
			
			line = line.Trim();
			ArrayList argsToRet = new ArrayList();
			bool isStringArg = false;
			StringBuilder currentArg = new StringBuilder();
			
			for(int i = 0; i<line.Length; i++){
			
				if(line[i] == ',' && isStringArg == false){ //encontramos un argumento nuevo
					argsToRet.Add(currentArg.ToString());
					currentArg=new StringBuilder();
					continue;
				}
				if(line[i] == '"' && !isStringArg){
					isStringArg = true;
				}
				else if (isStringArg && line[i] == '"'){
					isStringArg = false;
				}
				currentArg.Append(line[i]);
			}
			if(currentArg.Length>0)
				argsToRet.Add(currentArg.ToString());
			return argsToRet.ToArray();
			
		}
		
		public void processFlow(string threadID, string[] flowLines, string puertoMaster, SubjugatorCore elCore){
			isFlowRunning++;
			ArrayList tablaDeSimboloDeArchivo = new ArrayList();
			int numLine = 0;
			foreach(string line in flowLines){
				//procesar las linea
				if(string.IsNullOrEmpty(line))
					continue;
				if(line[0]=='['){
					//no hacer nada porque es la primer linea o comentario
					continue;
				}
				Bloq.argsToSend= new Queue();
				numLine++;
				
				string scriptToRun = line.Substring(0,line.IndexOf('('));
				
				//Cambiar el simple split por un manejador de argumentos
				//string[] args = line.Substring(line.IndexOf('(')+1,line.IndexOf(')')-(line.IndexOf('(')+1)).Split(',');
				object[] args = getArgsFromLine(line.Substring(line.IndexOf('(')+1,line.IndexOf(')')-(line.IndexOf('(')+1)));
				
				if(args.Length>0){
					foreach(object arg in args){
						if(!string.IsNullOrEmpty(arg.ToString()))
							Bloq.argsToSend.Enqueue(arg.ToString());
					}
				}
				string comment ="";
				if(line.IndexOf('#') !=-1)
					comment = line.Substring(line.IndexOf('#'));
				//pudeo usar los comment para ejecutar los bloques o flujos que analizan los pensamientos.
				
				Process currentBloq = ejecutarBloq(scriptToRun,puertoMaster, isFlowRunning>0);
				if(currentBloq!=null){
					currentBloq.WaitForExit();
					//buscar primero el output tipo nombreArchivo.ext
					//en caso de que no entonces buscar el output tipo stdOut_nombreArchivo.ext
					string returnValue = Bloq.getValueFromListBox(elCore.tableNameS,elCore.tableValuesS,scriptToRun.Substring(0,scriptToRun.IndexOf(".")),isFlowRunning>0);
					if(string.IsNullOrEmpty(returnValue)){
						returnValue = Bloq.getValueFromListBox(elCore.tableNameS,elCore.tableValuesS,"stdOut_"+scriptToRun,isFlowRunning>0);
					}
					if(string.IsNullOrEmpty(returnValue)){
						returnValue = Bloq.getValueFromListBox(elCore.tableNameS,elCore.tableValuesS,"stdErr_"+scriptToRun,isFlowRunning>0);
					}
					
					
					if(!string.IsNullOrEmpty(returnValue)){
						addNewSymbol("$"+numLine,returnValue);
					}
				}
				else{
					addNewSymbol(threadID,"Error - No existe o no se puede procesar el bloque: ["+scriptToRun+"]");
					//return null;//flow failed due to bloq
				}
				
			}
			isFlowRunning--;
		}
		
		public void captureStandardError(Process process, string thrID){
            StreamReader reader = process.StandardError;
            string output = "";
            do{
            	output = reader.ReadToEnd();
            	if(!string.IsNullOrEmpty(output))
            		addNewSymbol("stdErr_"+thrID,output);
            	Thread.Sleep(100);
            }while(output!=null);
           
			reader.Close();
            process.WaitForExit();
            
		}
		
		
		public void captureStandardOutput(Process process, string thrID){
            StreamReader reader = process.StandardOutput;
            string output = "";
            do{
            	output = reader.ReadToEnd();
            	if(!string.IsNullOrEmpty(output))
            		addNewSymbol("stdOut_"+thrID,output);
            	Thread.Sleep(100);
            }while(output!=null);
           
			reader.Close();
            process.WaitForExit();
		}
		
		public void addConnection(string fileID, string SocketNo){
		}
		public void removeConnection(string fileID){
		}
		
		public void clearBloqStdVariables(string thrID){
			bool stdErrRemoved = false;
			bool stdOutRemoved = false;
			for(int i =tableNameS.Count-1;i>=0;i--){
				if(tableNameS[i].ToString()=="stdErr_"+thrID){
					tableValuesS.RemoveAt(i);
					tableNameS.RemoveAt(i);
					stdErrRemoved=true;
					if(stdOutRemoved==false)
						continue;
				}
				if(tableNameS[i].ToString()=="stdOut_"+thrID){
					tableValuesS.RemoveAt(i);
					tableNameS.RemoveAt(i);
					stdOutRemoved=true;
					if(stdErrRemoved==false)
						continue;
				}
				if(stdOutRemoved && stdErrRemoved)
					return;
			}
		}
		public void controlarConexiones(){
			// Establish the local endpoint
		    // for the socket. Dns.GetHostName 
		    // returns the name of the host  
		    // running the application. 
			IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName()); 
		    IPAddress ipAddr = IPAddress.Parse("127.0.0.1");
		    IPEndPoint localEndPoint = new IPEndPoint(ipAddr, socketMaster);
		    
		  
		    // Creation TCP/IP Socket using  
		    // Socket Class Costructor 
		    
		    listener = new Socket(ipAddr.AddressFamily, 
		                 SocketType.Stream, ProtocolType.Tcp);
		   // Using Bind() method we associate a 
		        // network address to the Server Socket 
		        // All client that will connect to this  
		        // Server Socket must know this network 
		        // Address 
		        listener.Bind(localEndPoint); 
		  
		        // Using Listen() method we create  
		        // the Client list that will want 
		        // to connect to Server 
		        listener.Listen(1000); 
		        
			while(true){
		        	// Suspend while waiting for 
		            // incoming connection Using  
		            // Accept() method the server  
		            // will accept connection of client 
		            Socket clientSocket = listener.Accept(); 
		            Bloq newClient = new Bloq(listener, clientSocket, this, isFlowRunning>0, socketMaster, ruta);
		            bloqsThread.Add(newClient);
			}
		}
		
		
		
		void SymTabVTextKeyPress(object sender, KeyPressEventArgs e)
		{
			if(e.KeyChar==13){
				if(SymTabNText.Text.Trim()!="" && SymTabVText.Text.Trim()!=""){
					addNewSymbol(SymTabNText.Text.Trim(),SymTabVText.Text.Trim());
					SymTabNames.Items.Clear();
					SymTabValues.Items.Clear();
					arrayList2Listbox(SymTabNames,tableNameS);
					arrayList2Listbox(SymTabValues,tableValuesS);
				}
			}
		}
		
		public void addNewSymbol(string name, string value){
			for(int i =0;i<tableNameS.Count;i++){
				if(tableNameS[i].ToString()==name){
					tableValuesS.RemoveAt(i);
					tableValuesS.Insert(i,value);
					return;
				}
			}
			tableNameS.Add(name);
			tableValuesS.Add(value);
		}
		
		
		void SyncTablesButtonClick(object sender, EventArgs e)
		{
			SyncTables();
		}
		
		public void SyncTables(){
			SymTabNames.Items.Clear();
			SymTabValues.Items.Clear();
			ProcListbox.Items.Clear();
			BloqKeysListbox.Items.Clear();
			ProcNamesListbox.Items.Clear();
			arrayList2Listbox(ProcNamesListbox,procNamesStarted);
			arrayList2Listbox(BloqKeysListbox,bloqsKeys);
			arrayList2Listbox(ProcListbox,bloqsThread);
			arrayList2Listbox(SymTabNames,tableNameS);
			arrayList2Listbox(SymTabValues,tableValuesS);
		}
		
		public void arrayList2Listbox(ListBox lista, ArrayList aList){
			for(int i = 0 ; i<aList.Count;i++){
				if(aList[i]!=null)
					lista.Items.Add(aList[i].ToString());
			}
		}
	
		
		
		void SymTabNamesDoubleClick(object sender, EventArgs e)
		{
			if(SymTabNames.SelectedIndex>=0){
				string newValue = Microsoft.VisualBasic.Interaction.InputBox("Inserte nuevo valor ","Inserte dato solicitado",SymTabValues.Items[SymTabNames.SelectedIndex].ToString());
				if(newValue!=null && newValue!="")
					addNewSymbol(SymTabNames.Items[SymTabNames.SelectedIndex].ToString(), newValue);
			}
		}
		
		void SymTabValuesDoubleClick(object sender, EventArgs e)
		{
			if(SymTabValues.SelectedIndex>=0){
				Clipboard.SetText(tableValuesS[SymTabValues.SelectedIndex].ToString());
			}
		}
		
		void VisorVariablesToolStripMenuItemClick(object sender, EventArgs e)
		{
			VarVisorText.Visible =!VarVisorText.Visible;
			if(VarVisorText.Visible && (SymTabValues.SelectedIndex>=0 || SymTabNames.SelectedIndex>=0)){
				int index;
				if(SymTabValues.SelectedIndex>=0)
					index = SymTabValues.SelectedIndex;
				else
					index = SymTabNames.SelectedIndex;
				VarVisorText.Text = tableValuesS[index].ToString();
			}
		}
		
		void SymTabValuesSelectedIndexChanged(object sender, EventArgs e)
		{
			if(VarVisorText.Visible && SymTabValues.SelectedIndex>=0){
				VarVisorText.Text = tableValuesS[SymTabValues.SelectedIndex].ToString();
			}
		}
		
		void SymTabNamesSelectedIndexChanged(object sender, EventArgs e)
		{
			if(VarVisorText.Visible && SymTabNames.SelectedIndex>=0){
				VarVisorText.Text = tableValuesS[SymTabNames.SelectedIndex].ToString();
			}
		}
		
		
		void ListBox1MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right){
			    var index = ScriptsList.IndexFromPoint(e.Location);
			    ScriptsList.SelectedIndex=index;
			    if (index != ListBox.NoMatches)
			    {
			    	_selectedMenuItem = ScriptsList.Items[index];
			        collectionRoundMenuStrip.Show(Cursor.Position);
			        collectionRoundMenuStrip.Visible = true;
			    }
			    else
			    {
			        collectionRoundMenuStrip.Show(Cursor.Position);
			        collectionRoundMenuStrip.Visible = true;
			    }
			    return;
			}
		}
		
		void EditorCodigoToolStripMenuItemClick(object sender, EventArgs e)
		{
			EditorBoxGroup.Visible = !EditorBoxGroup.Visible;
		}
		
		void ScriptsListSelectedIndexChanged(object sender, EventArgs e)
		{
			if(ScriptsList.SelectedIndex>=0)
				selectedBloq = ScriptsList.Items[ScriptsList.SelectedIndex].ToString();
		}
		
		void RunEditorCodeToolStripMenuItemClick(object sender, EventArgs e)
		{
			if(EditorFileName.Text.IndexOf(".")>=0)
				selectedBloqExt = EditorFileName.Text.Substring(EditorFileName.Text.LastIndexOf("."));
			else
				selectedBloqExt = "";
            StreamWriter escrito = File.CreateText(ruta+"__tempBloq"+selectedBloqExt); // en el 
            String contenido = SCCodeEditor.Text;
            escrito.Write(contenido.ToString());
            escrito.Flush();
            escrito.Close();
            ejecutarBloq("__tempBloq"+selectedBloqExt,socketMaster.ToString(), false);
		}
		
		void SaveEditorCodeToolStripMenuItemClick(object sender, EventArgs e)
		{
			string fileName;
			fileName = Microsoft.VisualBasic.Interaction.InputBox("insert bloq name", "Save editor code","TempFile."+EditorFileName.Text);
			if(string.IsNullOrEmpty(fileName))
				return;
			StreamWriter escrito = File.CreateText(ruta+fileName); // en el 
            String contenido = SCCodeEditor.Text;
            escrito.Write(contenido.ToString());
            escrito.Flush();
            escrito.Close();
            loadFilesInListbox(ScriptsList);
		}
		
		
		void RunBloqToolStripMenuItemClick(object sender, EventArgs e)
		{
			runSelectedScript(ScriptsList);
		}
		
		void FilteredScriptListSelectedIndexChanged(object sender, EventArgs e)
		{
			if(FilteredScriptList.SelectedIndex>=0)
				selectedBloq = FilteredScriptList.Items[FilteredScriptList.SelectedIndex].ToString();
		}
		
		void FilterScriptTextTextChanged(object sender, EventArgs e)
		{
			//filtrar bloques
			if(string.IsNullOrEmpty(FilterScriptText.Text))
				FilteredScriptList.Visible = false;
			if(ScriptsList.Items.Count<1)
				return;
			FilteredScriptList.Items.Clear();
			FilteredScriptList.Visible = true;
			for (int i =0; i<ScriptsList.Items.Count; i++){
				if(ScriptsList.Items[i].ToString().Contains(FilterScriptText.Text))
					FilteredScriptList.Items.Add(ScriptsList.Items[i]);
			}
			if (FilteredScriptList.Items.Count==0){
				FilteredScriptList.Visible = false;
			}
		}
		
		void FilteredScriptListDoubleClick(object sender, EventArgs e)
		{
			runSelectedScript(FilteredScriptList);
		}
		
		void EditorBoxGroupEnter(object sender, EventArgs e)
		{
			
		}
		
		void ReloadBloqListToolStripMenuItemClick(object sender, EventArgs e)
		{
			loadFilesInListbox(ScriptsList);
		}
		
		void SubjugatorCoreFormClosing(object sender, FormClosingEventArgs e)
		{
			cCon.Suspend();
			listener.Close();
		}
		
		void SubjugatorCoreLoad(object sender, EventArgs e)
		{
			
		}
		
		void BloqKeysListboxMouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right){
			    var index = BloqKeysListbox.IndexFromPoint(e.Location);
			    BloqKeysListbox.SelectedIndex=index;
			    if (index != ListBox.NoMatches)
			    {
			    	_selectedMenuItem = ScriptsList.Items[index];
			        collectionRoundMenuStripBloqs.Show(Cursor.Position);
			        collectionRoundMenuStripBloqs.Visible = true;
			    }
			    else
			    {
			        collectionRoundMenuStripBloqs.Show(Cursor.Position);
			        collectionRoundMenuStripBloqs.Visible = true;
			    }
			    return;
			}
		}
		
		void ProcNamesListboxMouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right){
			    var index = ProcNamesListbox.IndexFromPoint(e.Location);
			    ProcNamesListbox.SelectedIndex=index;
			    if (index != ListBox.NoMatches)
			    {
			    	_selectedMenuItem = ScriptsList.Items[index];
			        collectionRoundMenuStripProcs.Show(Cursor.Position);
			        collectionRoundMenuStripProcs.Visible = true;
			    }
			    else
			    {
			        collectionRoundMenuStripProcs.Show(Cursor.Position);
			        collectionRoundMenuStripProcs.Visible = true;
			    }
			    return;
			}
		}
		void SaveClipboardToolStripMenuItemClick(object sender, EventArgs e)
		{
			string data = Clipboard.GetText();
			string variableName = Microsoft.VisualBasic.Interaction.InputBox("Insert Variable Name", "Variable Name", "Clipboard_data");
			if(!String.IsNullOrEmpty(variableName) && !String.IsNullOrEmpty(data)){
				addNewSymbol(variableName, data);
				SyncTables();
			}
		}
		void KnlPathsComboSelectedIndexChanged(object sender, EventArgs e)
		{
			ruta=KnlPathsCombo.SelectedItem.ToString();
			loadFilesInListbox(ScriptsList);
		}
	}
	
	class Bloq {  
		Socket listener;
		public Socket clientSocket;
		public bool isAlive; 
		public string scriptFilename;
		public static Queue argsToSend= new Queue();
		SubjugatorCore elCore=null;
		public string myID;
		public Thread myConn;
		public string externalOrder;
		public bool stillWaiting  = false;
		public string filePath;
		
		public Bloq(Socket listener, Socket clientSocket, SubjugatorCore elCore, bool isFlowRunning, int socketMaster, string filePath){
			this.elCore=elCore;
			this.listener=listener;
			this.clientSocket=clientSocket;
			isAlive=true;
			this.filePath = filePath;
			myConn=new Thread(() => ExecuteServer(isFlowRunning, socketMaster));
			myConn.Start();
		}
		public void ExecuteServer(bool isFlowRunning, int socketMaster) 
		{ 
		    try { 		  
		        while (isAlive) { 
		  			
		            // Data buffer 
		            int BUFFSIZE=1024;
		            byte[] bytes = new Byte[BUFFSIZE]; 
		            string data = null; 
		  			int numByte=0;
		  			bool stopThread = false;
		  			
		  			//esto es el handshake, el cliente envia su ID y el servidor (Core) envia un "ok"
		  			myID=Encoding.ASCII.GetString(bytes, 0, clientSocket.Receive(bytes));
		  			scriptFilename = myID;
		  			myID = myID.Substring(0,myID.IndexOf('.'));
		  			//MessageBox.Show(myID);
		  			clientSocket.Send(Encoding.ASCII.GetBytes("ok"));
		  			
		  			//luego el cliente debe enviar datos al servidor, esto porque el cliente debe solicitar al servidor los datos para trabajar. por ejemplo.
					//supongamos que tenemos un cliente llamado sumar dos numeros. luego del handshake, el cliente debe preguntar al servidor por los valores.
					//lo hara enviando al servidor un comando en modo texto tipo: "requestValue:NombreValor"
					//el servidor buscara en su tabla de simbolos y lo va a enviar.					
		            while (true) { 
		  				//cuando se recibe mas datos de los que el buffer puede contener el buffer
		  				//en el siguiente ciclo va a leer los bytes siguientes o faltantes
		  				//con el siguiente codigo, mientras siga habiendo que leer entonces continuar leyendo y almacenarlo en una variable
			  				
		  				if(externalOrder!=null){
		  					data=externalOrder;
		  					externalOrder=null;
		  					byte[] message = Encoding.ASCII.GetBytes(data);
				            clientSocket.Send(message);
						}
		  				
		  					
		  				
		  					data=null;
			  				numByte=0;
			  				do{
				  				numByte = clientSocket.Receive(bytes);
				                data += Encoding.ASCII.GetString(bytes, 0, numByte); 
				                if(numByte==0){
				                	stopThread=true;
				                }
			  				}while(numByte>=BUFFSIZE);
		  				
			  				
		  				//aqui se ve el ultimo mensaje, el de resultado
		  				
		                
		                // Send a message to Client  
			            // using Send() method 
			            // Crear la data que se le enviara al cliente
			            byte[] messagex ;
			            if(data.Length>12 && data.Substring(0,12)=="requestValue"){//peticion de valor
			            	data=data.Substring(13);
			            	string dataToSend="";
			            	
			            	if(isFlowRunning){
			            		if(argsToSend.Count>0){
			            			string argToSend = argsToSend.Dequeue().ToString();
			            			dataToSend= getValueFromListBox(elCore.tableNameS,elCore.tableValuesS,argToSend,isFlowRunning);
			            		}
			            		messagex = Encoding.ASCII.GetBytes(dataToSend);
			            	}else{
				            	dataToSend = getValueFromListBox(elCore.tableNameS,elCore.tableValuesS,data,isFlowRunning);
				            	if(dataToSend!=""){
				            		messagex = Encoding.ASCII.GetBytes(dataToSend);
				            	}else{
					            	String datosAEnviar;
					            	do{
					            		datosAEnviar = Microsoft.VisualBasic.Interaction.InputBox("Dato solicitado no existente: "+data,"Inserte dato solicitado");
					            	}while(datosAEnviar=="");
					            	//agregar dato solicitado
					            	addNewSymbol(data,datosAEnviar);
									messagex = Encoding.ASCII.GetBytes(datosAEnviar);
			            		}
			            	}
			            	clientSocket.Send(messagex);
			            }else if(data.Length>11 && data.Substring(0,11)=="requestProc"){
			            	data=data.Substring(12);
			            	Process ranSuccess = elCore.ejecutarBloq(data,socketMaster.ToString(), isFlowRunning);//aun no probado pero creo que dara error porque un hilo manda llamar a otro hilo y no espera a que termine el proceso
			            	if(ranSuccess!=null){
			            		ranSuccess.WaitForExit();
			            		string dataToSend;
				            	do{
				            	dataToSend = getValueFromListBox(elCore.tableNameS,elCore.tableValuesS,data,isFlowRunning);
				            	Thread.Sleep(100);
				            	}while(dataToSend=="");
				            	messagex = Encoding.ASCII.GetBytes(dataToSend);
				            	clientSocket.Send(messagex);
			            	}else{
			            		if(!data.ToLower().EndsWith(".flw")) 
			            			MessageBox.Show("Error running the requiredScript, Maybe it doesn't exist");
			            		messagex = Encoding.ASCII.GetBytes("null");
				            	clientSocket.Send(messagex);
			            	}
							//Monitor.Wait(elCore.tableValuesS);//CHG00001
			            }else if(data.Length>10 && data.Substring(0,10)=="requestVar"){
			            	data=data.Substring(11);
			            	string dataToSend = getValueFromListBox(elCore.tableNameS,elCore.tableValuesS,data,false);
				            messagex = Encoding.ASCII.GetBytes(dataToSend);
			            	clientSocket.Send(messagex);
			            }else if(data.Length>13 && data.Substring(0,13)=="requestPrcVar"){
			            	data=data.Substring(14);
			            	string[] args = data.Split(',');
			            	if(args.Length==2){
			            		Bloq askedBloq = getBloqFromCore(elCore.bloqsKeys,elCore.bloqsThread,args[0],isFlowRunning);
			            		if(askedBloq!=null){
			            			askedBloq.externalOrder = args[1];
			            			Thread.Sleep(100);
			            			string fromExternalVal = getValueFromListBox(elCore.tableNameS,elCore.tableValuesS,args[1],isFlowRunning);
			            			if(fromExternalVal==""){
				            			int iterations = 0;
				            			while(iterations < 10){
				            				fromExternalVal = getValueFromListBox(elCore.tableNameS,elCore.tableValuesS,args[1],isFlowRunning);
					            			iterations++;
					            			if(fromExternalVal=="")
					            				Thread.Sleep(100);
					            			else
					            				break;
				            			}
				            			if(fromExternalVal=="")
				            				fromExternalVal="null";
										messagex = Encoding.ASCII.GetBytes(fromExternalVal);			            			
					            		clientSocket.Send(messagex);
			            			}else{
			            				messagex = Encoding.ASCII.GetBytes(fromExternalVal);			            			
					            		clientSocket.Send(messagex);
			            			}
				            		
			            		}
			            		else{
			            			messagex = Encoding.ASCII.GetBytes("null");
				            		clientSocket.Send(messagex);
			            		}
			            	}else{
				            	messagex = Encoding.ASCII.GetBytes("null");
				            	clientSocket.Send(messagex);
			            	}
			            	
			            }else if(data.Length>10 && data.Substring(0,10)=="requestArg"){
			            	data=data.Substring(11);
			            	String datosAEnviar="";
			            	
			            	if(isFlowRunning){
			            		if(argsToSend.Count>0){
			            			string argToSend = argsToSend.Dequeue().ToString();
			            			datosAEnviar= getValueFromListBox(elCore.tableNameS,elCore.tableValuesS,argToSend, isFlowRunning);
			            		}
			            		messagex = Encoding.ASCII.GetBytes(datosAEnviar);
			            	}else{
				            	do{
				            		datosAEnviar = Microsoft.VisualBasic.Interaction.InputBox("Inserte dato manual solicitado: "+data,"Inserte dato solicitado");
				            	}while(datosAEnviar=="");
			            	}
				            	//agregar dato solicitado
							messagex = Encoding.ASCII.GetBytes(datosAEnviar);
							clientSocket.Send(messagex);
			            }else if(data.Length>9 && data.Substring(0,9)=="saveValue"){
			            	data=data.Substring(10);
			            	string varName=data.Split(',')[0];
			            	addNewSymbol(varName,data.Substring(varName.Length+1));
			            	String datosAEnviar = "ok";
				            	//agregar dato solicitado
							messagex = Encoding.ASCII.GetBytes(datosAEnviar);
							clientSocket.Send(messagex);
							if(stillWaiting)
								data="wait";
			            }else if(data.Length>=4 && data.Substring(0,4)=="wait"){
			            	while(externalOrder==null){
			            		Thread.Sleep(100);
			            		if(!string.IsNullOrEmpty(externalOrder)){
			            			messagex = Encoding.ASCII.GetBytes(externalOrder);
									clientSocket.Send(messagex);
									stillWaiting=true;
									if(externalOrder=="stopme"){
										stillWaiting=false;
										break;
									}
			            		}
			            	}
			            	externalOrder= null;
			            }else{
			            	if(data!=""){
			            		addNewSymbol(myID,data);
			            		
			            	}
			                if (stopThread){
			                    break; 
			                }
			            }
			            
			            //MessageBox.Show("enviando mensaje");
		            } 
		  			
		            //MessageBox.Show("Stopping Thread");
		            //Monitor.Pulse(elCore.tableValuesS);//CHG00001
		            
		            // Send a message to Client  
		            // using Send() method 
		  
		            // Close client Socket using the 
		            // Close() method. After closing, 
		            // we can use the closed Socket  
		            // for a new Client Connection 
		            clientSocket.Shutdown(SocketShutdown.Both); 
		            clientSocket.Close();
		            isAlive=false;
		            //eliminar archivo myID
		            File.Delete(filePath+scriptFilename);
		        } 
		    } 
		      
		    catch (Exception e) { 
				addNewSymbol("CoreExcept_"+myID,e.ToString());
				
		    }
			finally	{
				//remover aqui el hilo de las listas
				removeListBloq(myID,elCore.bloqsKeys,elCore.bloqsThread);
				elCore.removeConnection(myID);
			}
		}
		public void removeListBloq(string name, ArrayList names, ArrayList values){
			for(int i =0;i<names.Count;i++){
				if(names[i].ToString()==name){
					try{
						values.RemoveAt(i);
						names.RemoveAt(i);
					}catch(Exception e){
						e.ToString();
					}
					return;
				}
			}
		}
		
		public void addNewSymbol(string name, string value){
			for(int i =0;i<elCore.tableNameS.Count;i++){
				if(elCore.tableNameS[i].ToString()==name){
					elCore.tableValuesS.RemoveAt(i);
					elCore.tableValuesS.Insert(i,value);
					return;
				}
			}
			elCore.tableNameS.Add(name);
			elCore.tableValuesS.Add(value);
		}
		public static Bloq getBloqFromCore(ArrayList elListNames, ArrayList elListVals, string nameToSearch,bool isFlowRunning){
			if(isFlowRunning && nameToSearch[0]=='&'){
				return null;
			}
			for(int i = 0 ;i<elListNames.Count;i++){
				if(elListNames[i].ToString()==nameToSearch){
					return (Bloq)elListVals[i];
				}
			}
			return null;
		}
		
		public static string getValueFromListBox(ArrayList elListNames, ArrayList elListVals, string nameToSearch,bool isFlowRunning){
			if(isFlowRunning && nameToSearch[0]=='"'){
				return nameToSearch.Substring(1,nameToSearch.Length-2);
			}
			if(nameToSearch[0]=='"')
				return nameToSearch.Substring(1,nameToSearch.Length-2);
			for(int i = 0 ;i<elListNames.Count;i++){ //buscar la variable que esta solicitando
				if(elListNames[i].ToString()==nameToSearch){
					return elListVals[i].ToString();
				}
			}
			return "";
		}
	}
}
