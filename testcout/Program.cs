/*
 * Creado por SharpDevelop.
 * Usuario: hernani
 * Fecha: 20/08/2017
 * Hora: 0:59
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.IO;
using syscout;

namespace testcout
{
	class Program
	{
		public static void Main(string[] args)
		{
			string file = null;
			if (args.Length == 0) {
				System.Console.WriteLine("Please enter a namber of image file.");
				System.Console.WriteLine("Usage: extension \".ppm\"");
				file = Console.ReadLine();
			}
			if (args.Length >= 1) {
				if (File.Exists(args[0])) {
					MainImagen(args[0]);
				}
			}
			if (file != null) {
				if (File.Exists(file)) {
					MainImagen(file);
				}
			}
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
		public static void MainImagen(string args)
		{
			Console.WriteLine("leyendo imagen " + args);
			Imagen I = new Imagen();
			I.FromFile(args);
			I.Save("copia " + args);
			Console.WriteLine("copia "+ args + " realizada.");
			//posterizar la imagen
			Imagen p = new Imagen(I);
			p.posterizar(6);
			p.Save("posterizada " + args);
			Console.WriteLine("Imagen posterizada de " + args);
			Imagen t = I.cut(40, 0, 140, 120);
			t.Save("Trozo " + args); //salvamos el trozo
			Console.WriteLine("trozo " + args + " guardado.");
			t.invertir();
			t.Save("Trozo invertido " + args);
			Console.WriteLine("trozo invertido" + args + " guardado.");
		}
		
		
	}
}