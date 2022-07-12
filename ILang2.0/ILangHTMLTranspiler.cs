/*
 * Created by SharpDevelop.
 * User: T430
 * Date: 05/09/2021
 * Time: 02:12 p.m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Text;

namespace ILang2._
{
	/// <summary>
	/// Description of ILangHTMLTranspiler.
	/// </summary>
	public class ILangHTMLTranspiler
	{
		
		private string metaSourceText;
		private ILangForm ilangForm;
		public ILangHTMLTranspiler() {}
		public ILangHTMLTranspiler(string metaSourceText, ILangForm rootIlangForm) {
		
			this.metaSourceText = metaSourceText;
			ilangForm = rootIlangForm;
		}
		public string transpileText(){
			
			/*Este es el metodo principal, aqui transpilaremos a codigo.
			Especificaciones:
				En la mayoria solo habra un metacomando por linea.
				Tendra una etiqueta por defecto en caso de que no haya un metacaracter //No tendremos etiqueta por defecto, asi podemos implementar los codigos multilinea de forma directa.
					La etiqueta por defecto sera el <p>
					//Ignorar -> Si el metacaracter esta lejos del inicio (5 caracteres), entonces usamos la etuqueta por defecto
					Si no se identifica una etiqueta para el metacaracter usado entonces usamos la etiqueta por defecto
				En caso de que haya un metacaracter de multiples lineas usaremos /# para cerrar el bloque.
					esto es para code# porque es de multiples lineas. 
				Usaermos una maquina de estados para hacer la seleccion de metacaracter
				
			Pasos para el proceso:
			//1. Separar en lineas la cadena usando \r\n
			//2. Linea a linea iterar cada accion
				//2.1 transpilarla 
				//2.2 concatenarla en la cadena final.
			//3. Si hay un metacaracter de multiples lineas lo iteramos hasta encontrar el /# y agrupamos todas las lineas a la cadena final
			*/
			
			//1. Separar en lineas la cadena usando \r\n
			string[] textToTranspile = metaSourceText.Split("\r\n".ToCharArray());
			StringBuilder htmlTranspiled = new StringBuilder();
			
			//2. linea a linea iterar cada accion
			int metacharIndex;
			string stringMetaSelector;
			string stringTextContent;
			string htmlContentLine;
			foreach(string line in textToTranspile){
				if(line.Length<1)
					continue;
				metacharIndex = line.IndexOf('#');
				if(metacharIndex < 1){ //En caso de no tener metacaracter o de que sea el primer caracter lo pasamos tal cual
					htmlTranspiled.Append(line.ToString()+"\r\n");
					continue;
				}else{
					stringMetaSelector = line.Substring(0,metacharIndex);
					stringTextContent = line.Substring(metacharIndex+1).Trim();
				}
				htmlContentLine = transpileLine(stringMetaSelector, stringTextContent);
				htmlTranspiled.Append(htmlContentLine.ToString()+"\r\n");
			}
			return htmlTranspiled.ToString();
		}
		
		public string transpileLine(string mainMetachars, string textContent){
			string selector;
			string classes="";
			string properties="";
			textContent=textContent.Trim();
			if(mainMetachars.IndexOf(',') > 0){
				selector=mainMetachars.Substring(0,mainMetachars.IndexOf(','));
				mainMetachars = mainMetachars.Substring(mainMetachars.IndexOf(',')+1);
				//En caso de que haya una coma despues de la primer coma, es decir haya atributos
				if(mainMetachars.IndexOf(',') > -1){
					classes=" class=\""+mainMetachars.Substring(0,mainMetachars.IndexOf(','))+"\" ";
					properties =" "+ mainMetachars.Substring(mainMetachars.IndexOf(',')+1)+ " ";
				}else{
					classes=" class=\"" + mainMetachars+"\" ";
				}
			}else{
				selector = mainMetachars;
			}
			switch(selector){
				case "h1":
					return "<h1"+classes+properties+">"+textContent+"</h1>";
				case "h2":
					return "<h2"+classes+properties+">"+textContent+"</h2>";
				case "h3":
					return "<h3"+classes+properties+">"+textContent+"</h3>";
				case "h4":
					return "<h4"+classes+properties+">"+textContent+"</h4>";
				case "h5":
					return "<h5"+classes+properties+">"+textContent+"</h5>";
				case "h6":
					return "<h6"+classes+properties+">"+textContent+"</h6>";
				case "li":
					return "<li"+classes+properties+">"+textContent+"</li>";
				case "hr":
					return "<hr"+classes+properties+"/>";
				case "p":
					return "<p"+classes+properties+">"+textContent+"</p>";
				case "a":
					return "<a"+classes+properties+">"+textContent+"</a>";
				case "img":
					return "<img "+classes+" src=\"data:image/jpeg;base64,"+ ilangForm.getImageFromFileBase64(properties.Trim()) + "\" />";
				default:
					return "<"+selector+classes+properties+">";
			}
		}
	}
}
