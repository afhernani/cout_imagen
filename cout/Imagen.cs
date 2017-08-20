/*
 * Creado por SharpDevelop.
 * Usuario: hernani
 * Fecha: 20/08/2017
 * Hora: 16:24
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Collections;
using System.Diagnostics;
using System.IO;

namespace syscout
{
	public struct Pixel{
		public static Pixel Empty;
		public int r, g, b;
		static Pixel(){
			Empty.r = 0;
			Empty.g = 0;
			Empty.b = 0;
		}
		public Pixel(int _r, int _g, int _b){
			r = _r;
			g = _g;
			b = _b;
		}
	}
	/// <summary>
	/// Description of Imagen.
	/// </summary>
	public class Imagen
	{
		public Pixel[] _Pixels;
		public int Width{ get; set; }
		public int Heigh{ get; set; }
		public int Maxi{ get; set; }
		public Imagen()
		{
			_Pixels=new Pixel[0];
			Width = Heigh = 0;
			Maxi = 0;
		}
		public Imagen(int width, int heigh){
			_Pixels = new Pixel[width * heigh];
			Width = width;
			Heigh = heigh;
			Maxi = 255;
		}
		public Imagen(Imagen i){
			_Pixels = i._Pixels;
			Width = i.Width;
			Heigh = i.Heigh;
			Maxi = i.Maxi;
		}
		/// <summary>
		/// vamos a cargar un fichero propio
		/// </summary>
		/// <param name="namefile"></param>
		public void FromFile(string namefile){
			cout file = new cout(namefile);
			if(file.get()!="P3"){
				throw new Exception("imposible abrir el " +
				                    "fichero o formato no correcto");
			}
			Width = file.getInt();
			Heigh = file.getInt();
			Maxi = file.getInt();
			_Pixels = new Pixel[Width * Heigh];
			for(int i =0; i< _Pixels.Length; i++){
				//Debug.WriteLine("iteracion: " + i);
				_Pixels[i].r = file.getInt();
				_Pixels[i].g = file.getInt();
				_Pixels[i].b = file.getInt();
			}
			
		}
		public void Save(string filename){
			using (StreamWriter stw = new StreamWriter(filename)){
				stw.WriteLine("P3");
				stw.WriteLine("# make for hernani ");
				stw.WriteLine("{0} {1}i", Width, Heigh);
				stw.WriteLine("{0}", Maxi);
				for (int i = 0; i <_Pixels.Length; i++) {
					stw.Write("{0} {1} {2} ", _Pixels[i].r, _Pixels[i].g,
						_Pixels[i].b);
					if (i % Width == Width-1) {
						stw.WriteLine();
					}
				}
			}
		}
	}
}
