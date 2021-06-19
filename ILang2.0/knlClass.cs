/*
 * Created by SharpDevelop.
 * User: alejandro.rangel
 * Date: 06/05/2020
 * Time: 04:01 p. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace ILang2._
{
	/// <summary>
	/// Description of knlClass.
	/// </summary>
	public class knlClass
	{
		public string name;//nombre del knl
		public string path;//ruta completa del knl, archivo, etc
		public string content;//contenido
		public int type;//tipo de knl
		public knlClass(){}
		public knlClass(string name, string content, string path)
		{
			this.name=name;
			this.content=content;
			this.path=path;
		}
		
		public void setName(string name){
			this.name=name;
		}
		public void setContent(String content){
			this.content=content;
		}
		public void setType(int type){
			this.type=type;
		}
	}
}
