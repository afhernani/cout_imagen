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
	public struct Pixel
	{
		public static Pixel Empty;
		public int r, g, b;
		static Pixel()
		{
			Empty.r = 0;
			Empty.g = 0;
			Empty.b = 0;
		}
		public Pixel(int _r, int _g, int _b)
		{
			r = _r;
			g = _g;
			b = _b;
		}
		public void invertir(int max)
		{
			r = max - r;
			g = max - g;
			b = max - g;
		}
		
		public void posterizar(int maxi, int niveles)
		{
			r = Imagen.posterizar(r, maxi, niveles);
			g = Imagen.posterizar(g, maxi, niveles);
			b = Imagen.posterizar(b, maxi, niveles);
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
			_Pixels = new Pixel[0];
			Width = Heigh = 0;
			Maxi = 0;
		}
		public Imagen(int width, int heigh)
		{
			_Pixels = new Pixel[width * heigh];
			Width = width;
			Heigh = heigh;
			Maxi = 255;
		}
		public Imagen(Imagen I)
		{
			Width = I.Width;
			Heigh = I.Heigh;
			_Pixels = new Pixel[Width * Heigh];
			Maxi = I.Maxi;
			for (int i = 0; i < _Pixels.Length; i++) {
				_Pixels[i] = I._Pixels[i];
			}
		}
		/// <summary>
		/// vamos a cargar un fichero propio
		/// </summary>
		/// <param name="namefile"></param>
		public void FromFile(string namefile)
		{
			cout file = new cout(namefile);
			if (file.get() != "P3") {
				throw new Exception("imposible abrir el " +
				"fichero o formato no correcto");
			}
			Width = file.getInt();
			Heigh = file.getInt();
			Maxi = file.getInt();
			_Pixels = new Pixel[Width * Heigh];
			for (int i = 0; i < _Pixels.Length; i++) {
				//Debug.WriteLine("iteracion: " + i);
				_Pixels[i].r = file.getInt();
				_Pixels[i].g = file.getInt();
				_Pixels[i].b = file.getInt();
			}
			
		}
		public void Save(string filename)
		{
			using (StreamWriter stw = new StreamWriter(filename)) {
				stw.WriteLine("P3");
				stw.WriteLine("# make for hernani ");
				stw.WriteLine("{0} {1}i", Width, Heigh);
				stw.WriteLine("{0}", Maxi);
				for (int i = 0; i < _Pixels.Length; i++) {
					stw.Write("{0} {1} {2} ", _Pixels[i].r, _Pixels[i].g,
						_Pixels[i].b);
					if (i % Width == Width - 1) {
						stw.WriteLine();
					}
				}
			}
		}
		/// <summary>
		/// obtener el valor del pixel en dicho punto
		/// de la imagen
		/// </summary>
		/// <param name="i"></param>
		/// <param name="j"></param>
		/// <returns></returns>
		public Pixel Getpixel(int i, int j)
		{
			return _Pixels[j * Width + i];
		}
		/// <summary>
		/// asignación del pixel
		/// </summary>
		/// <param name="i"></param>
		/// <param name="j"></param>
		/// <param name="valor"></param>
		public void Setpixel(int i, int j, Pixel valor)
		{
			_Pixels[j * Width + i] = valor;
		}
		/// <summary>
		/// obtiene un cacho de imagen de la actual.
		/// </summary>
		/// <param name="izq"></param>
		/// <param name="arr"></param>
		/// <param name="der"></param>
		/// <param name="aba"></param>
		/// <returns></returns>
		public Imagen cut(int izq, int arr, int der, int aba)
		{
			Imagen T = new Imagen(der - izq, aba - arr);
			for (int i = izq; i < der; i++) {
				for (int j = arr; j < aba; j++) {
					if ((i >= 0 && i < Width) && (j >= 0 && j < Heigh)) {
						//estamos dentro de la imagen original
						int x = i - izq;
						int y = j - arr;
						T.Setpixel(x, y, Getpixel(i, j));
					}
				}
			}
			return T;
		}
		/// <summary>
		/// pega una imagen en la actual
		/// </summary>
		/// <param name="I"></param>
		/// <param name="izq"></param>
		/// <param name="arr"></param>
		public void paste(Imagen I, int izq, int arr)
		{
			if (Maxi < I.Maxi)
				Maxi = I.Maxi;
			//recorremos el bucle de la imagen
			for (int i = 0; i < I.Width; i++) {
				for (int j = 0; j < I.Heigh; j++) {
					int x = izq + i;
					int y = arr + j;
					if ((x >= 0 && x < Width) && (y >= 0 && y < Heigh)) {
						Setpixel(x, y, I.Getpixel(i, j));
					}
				}
			}
		}
		/// <summary>
		/// invertir los pixels de la imagen
		/// </summary>
		public void invertir()
		{
			for (int i = 0; i < _Pixels.Length; i++) {
				_Pixels[i].invertir(Maxi);
			}
		}
		/// <summary>
		/// función general para devolver un valor posterizado.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="maxi"></param>
		/// <param name="niveles"></param>
		/// <returns></returns>
		public static int posterizar(int x, int maxi, int niveles)
		{
			double valor = (double)x / (double)(maxi) * (niveles - 1);
			valor = Math.Round(valor);
			return (int)(valor * (double)(maxi) / (niveles - 1));
		}
		
		/// <summary>
		/// posterizar la imagen en varios nievles.
		/// </summary>
		/// <param name="niveles"></param>
		public void posterizar(int niveles)
		{
			for (int i = 0; i < _Pixels.Length; i++) {
				_Pixels[i].posterizar(Maxi, niveles);
			}
		}
		/// <summary>
		/// clonamos la imagen.
		/// </summary>
		/// <returns></returns>
		public Imagen Clone(){
			Imagen c = new Imagen(Width, Heigh);
			c.Maxi = Maxi;
			for (int i = 0; i < _Pixels.Length; i++) {
				c._Pixels[i] = _Pixels[i];
			}
			return c;		
		}
		
		
	}
}
