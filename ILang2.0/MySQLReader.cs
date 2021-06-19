/*
 * Created by SharpDevelop.
 * User: alejandro.rangel
 * Date: 20/05/2020
 * Time: 11:31 a. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;

using MySql.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace ILang2._
{
	/// <summary>
	/// Description of MySQLReader.
	/// </summary>
	public class MySQLReader
	{
		
		public static string server= "localhost";
		public static string user = "root";
		public static string password = "root";
		public static string database = "ilangstorer";
		public static string port = "3306";
		
		public static string connStr = "server="+server+";user="+user+";database="+database+";port="+port+";password="+password;
		
	    public static MySqlConnection conn = new MySqlConnection(connStr);
	    public static bool isDBConnected=false;
	    
		public MySQLReader()
		{
		}
		
		
		public static void openDBConn(){
			if(isDBConnected)
				return;
			try
		        {
		            conn.Open();
		            isDBConnected = true;
		        }
		        catch (Exception ex)
		        {
		            Console.WriteLine(ex.ToString());
		        }
		}
		
		public static ArrayList getQueryFieldNames(MySqlDataReader reader){
			if(reader==null)
				return null;
			ArrayList listToReturn = new ArrayList();
			for(int i=0;i<reader.FieldCount;i++){
				listToReturn.Add(reader.GetName(i));
	        }
			return listToReturn;
		}
		
		public static ArrayList getFieldsData(MySqlDataReader reader){
			if(reader==null)
				return null;
			ArrayList listToReturn = new ArrayList();
			while(reader.Read()){
				ArrayList currentRow = new ArrayList();
				for(int i=0;i<reader.FieldCount;i++){
					currentRow.Add(reader.GetString(i));
				}
				listToReturn.Add(currentRow);
			}
			return listToReturn;
		}
		
		public static ArrayList getQueryNamesAndData(MySqlDataReader reader){
			if(reader==null)
				return null;
			ArrayList listToReturn = getFieldsData(reader);
			listToReturn.Insert(0,getQueryFieldNames(reader));
			return listToReturn;
		}
		
		public static string queryDataToString(ArrayList queryList){
			if(queryList==null)
				return "";
			return "";
			//for(int i=0;i<queryList.Count; i++){
			//	if(queryList[i]
			//	for(m=0;m<
			//}
			
		}
		public static bool insertMetadataDB(string metadataId, string name, string type, string path, string knlId){
			if(!isDBConnected){
				MessageBox.Show("Need to connec to DB first");
				return false;
			}
			int rowAffected=-1;
			string theID = null;
			try
	        {
	           	var sql = "INSERT INTO metadata(METADATA_ID, NAME, TYPE, PATH, KNL_ID) VALUES(@metadataid, @name, @type, @path, @knlid)";
	            MySqlCommand cmd = new MySqlCommand(sql, conn);
	
	            cmd.Parameters.AddWithValue("@metadataid", metadataId);
	            cmd.Parameters.AddWithValue("@name", name);
	            cmd.Parameters.AddWithValue("@type", type);
	            cmd.Parameters.AddWithValue("@path", path);
	            cmd.Parameters.AddWithValue("@knlid", knlId);
	            cmd.Prepare();

	            rowAffected = cmd.ExecuteNonQuery();
	        }
	        catch (Exception ex)
	        {
	        	MessageBox.Show(ex.ToString());
	        	return (rowAffected>0);
	        }
	        return (rowAffected>0);
		}
		public static string insertMetaMetadataDB(string name){
			if(!isDBConnected){
				MessageBox.Show("Need to connec to DB first");
				return null;
			}
			string theID = null;
			try
	        {
	           	var sql = "INSERT INTO metametadata(NAME) VALUES(@name)";
	            MySqlCommand cmd = new MySqlCommand(sql, conn);
	
	            cmd.Parameters.AddWithValue("@name", name);
	            cmd.Prepare();

	            cmd.ExecuteNonQuery();
	            theID = getMetaMetadataId(name);
	        }
	        catch (Exception ex)
	        {
	        	MessageBox.Show(ex.ToString());
	        	return null;
	        }
	        return theID;
		}
		public static string getMetaMetadataId(string name){
			if(!isDBConnected){
				MessageBox.Show("Need to connec to DB first");
				return null;
			}
			string theID = null;
			try
	        {
	           	var sql = "SELECT ID FROM metametadata WHERE NAME='"+name+"'";
	            MySqlCommand cmd = new MySqlCommand(sql, conn);

	            cmd = new MySqlCommand(sql, conn);
	            MySqlDataReader r = cmd.ExecuteReader();
	            if(r.Read()){
	            	theID=r.GetInt32(0).ToString();
	            }
	            r.Close();
	        }
	        catch (Exception ex)
	        {
	        	MessageBox.Show(ex.ToString());
	        	return null;
	        }
	        return theID;
		}
		public static string getKnlId(string filename, string category, string path){
			if(!isDBConnected){
				MessageBox.Show("Need to connec to DB first");
				return null;
			}
			string theID = null;
			try
	        {
	           	var sql = "SELECT ID FROM knldata WHERE FILENAME='"+filename+"' and CATEGORY='"+category.Replace("\\","\\\\")+"' and PATH='"+path.Replace("\\","\\\\")+"'";
	            MySqlCommand cmd = new MySqlCommand(sql, conn);

	            cmd = new MySqlCommand(sql, conn);
	            MySqlDataReader r = cmd.ExecuteReader();
	            if(r.Read()){
	            	theID=r.GetInt32(0).ToString();
	            }
	            r.Close();
	        }
	        catch (Exception ex)
	        {
	        	MessageBox.Show(ex.ToString());
	        	return null;
	        }
	        return theID;
		}
		public static string insertDB(string filename, string category, string path){
			if(!isDBConnected){
				MessageBox.Show("Need to connec to DB first");
				return null;
			}
			string theID = null;
			try
	        {
	           	var sql = "INSERT INTO knldata(FILENAME, CATEGORY, PATH) VALUES(@filename, @category, @path)";
	            MySqlCommand cmd = new MySqlCommand(sql, conn);
	
	            cmd.Parameters.AddWithValue("@filename", filename);
	            cmd.Parameters.AddWithValue("@category", category);
	            cmd.Parameters.AddWithValue("@path", path);
	            cmd.Prepare();

	            cmd.ExecuteNonQuery();
	            theID= getKnlId(filename, category, path);
	        }
	        catch (Exception ex)
	        {
	        	MessageBox.Show(ex.ToString());
	        	return null;
	        }
	        return theID;
		}
		
		public static ArrayList getMetadata(string id){
			if(!isDBConnected){
				MessageBox.Show("Need to connec to DB first");
				return null;
			}
			MySqlDataReader r;
			ArrayList arrToRet;
			try
	        {
				arrToRet = new ArrayList();
	            MySqlCommand cmd = new MySqlCommand("SELECT * FROM METADATA WHERE KNL_ID = "+id, conn);
	            r = cmd.ExecuteReader();
	             while (r.Read())
		            {
	             	string data= r.GetInt32(0).ToString()+r.GetString(1)+r.GetString(2)+r.GetString(3);
	             	arrToRet.Add(new object[]{r.GetInt32(0).ToString(),r.GetString(1),r.GetString(2),r.GetString(3)});
		            }
	             r.Close();
	        }
	        catch (Exception ex)
	        {
	        	MessageBox.Show(ex.ToString());
	        	return null;
	        }
	        return arrToRet;
		}
		
		public static ArrayList queryDB(string query){
			if(!isDBConnected){
				MessageBox.Show("Need to connec to DB first");
				return null;
			}
			MySqlDataReader r;
			ArrayList arrToRet;
			try
	        {
				arrToRet = new ArrayList();
	            MySqlCommand cmd = new MySqlCommand(query, conn);
	            r = cmd.ExecuteReader();
	             while (r.Read())
		            {
	             	string data= r.GetInt32(0).ToString()+r.GetString(1)+r.GetString(2)+r.GetString(3);
	             	arrToRet.Add(new object[]{r.GetInt32(0).ToString(),r.GetString(1),r.GetString(2),r.GetString(3)});
		            }
	             r.Close();	            
	        }
	        catch (Exception ex)
	        {
	        	MessageBox.Show(ex.ToString());
	        	return null;
	        }
	        return arrToRet;
		}
		
		public static void closeDBConn(){
			if(!isDBConnected)
				return;
			conn.Close();
			isDBConnected=false;
		}
	}
}
