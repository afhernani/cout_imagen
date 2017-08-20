/*
 * Creado por Hernani.
 * Usuario: hernani
 * Fecha: 20/08/2017
 * Hora: 1:04
 * 
 *
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics;

namespace syscout
{
	/// <summary>
	/// Description of cout.
	/// </summary>
	public class cout
	{
		readonly StreamReader _str;
		bool Open{ get; set; }
		public cout()
		{
			Open = false;
		}
		public cout(StreamReader st)
			: this()
		{
			_str = st;
		}
		public cout(string font)
			: this()
		{
			_str = new StreamReader(font);
		}
		/// <summary>
		/// el destructor de la clase
		/// </summary>
		~cout(){
			if (Open)
				_str.Close();
			_str.Dispose();
			
		}
		
		public int getInt()
		{
			string cad;
			cad = get();
			//Debug.WriteLine(cad);
			return (int)Convert.ToDouble(cad);
		}
		/// <summary>
		/// recorre hasta conseguir la cadena siguiente
		/// en el streamReader.
		/// </summary>
		/// <returns></returns>
		public string get()
		{
			if (_str == null)
				return "Null";
			if (_str.EndOfStream)
				return "eof";
			char ch = Convert.ToChar(_str.Read());
			//Debug.Write(ch);
			switch (ch) {
				case '#':
					while (!_str.EndOfStream && ch != '\n') {
						ch = Convert.ToChar(_str.Read());
					}
					return get();
				case '\n':
					return get();
				case ' ':
					return get();
				case '.':
				case '0':
				case '1':
				case '2':
				case '3':
				case '4':
				case '5':
				case '6':
				case '7':
				case '8':
				case '9':
					{
						StringBuilder strb = new StringBuilder();
						strb.Append(ch);
						
						while (!_str.EndOfStream) {
							if (TextUtils.IsNumeric(ch = Convert.ToChar(_str.Peek())) || Convert.ToChar(_str.Peek()) == '.') {
								ch = Convert.ToChar(_str.Read());
								strb.Append(ch);
							} else {
								break;
							}
						}
						
						//Debug.WriteLine(strb.ToString());
						return Convert.ToDouble(strb.ToString(), System.Globalization.CultureInfo.InvariantCulture).ToString();
					}
				default:
					if (TextUtils.IsAlphabetic(ch)) {
						StringBuilder name = new StringBuilder();
						
						name.Append(ch);
						
						while (!_str.EndOfStream) {
							ch = Convert.ToChar(_str.Peek());
							if (ch !=' ' && ch !='\n') {
								ch = Convert.ToChar(_str.Read());
								name.Append(ch);
							} else {
								break;
							}
						}
						
						//Debug.WriteLine(name.ToString());
						return name.ToString();
				
					} else {
						throw new Exception("exception bad get Token");
					}
			//break;					
			}
					
		}
		//
	}
}
	
