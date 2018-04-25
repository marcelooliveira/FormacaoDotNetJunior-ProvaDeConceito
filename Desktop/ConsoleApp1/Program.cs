using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            IList<Livro> livros = new List<Livro>();
            using (var sr = new StreamReader("livros.txt"))
            {
                string line = null;
                int id = 1;
                while ((line = sr.ReadLine()) != null)
                {
                    var parts = line.Split('|');

                    var src = parts[0];
                    var name = parts[1];
                    var categoria = parts[2] + " / " + parts[3];

                    SaveImage(src, $"large_{id:d3}.jpg", ImageFormat.Jpeg);

                    livros.Add(new Livro(id.ToString("000"), name, categoria, src, 49.90M));

                    id++;
                }
            }

            var json = JsonConvert.SerializeObject(livros);
            File.WriteAllText("livros.json", json);
        }

        public static void SaveImage(string imageUrl, string filename, ImageFormat format)
        {
            WebClient client = new WebClient();
            Stream stream = client.OpenRead(imageUrl);
            Bitmap bitmap; bitmap = new Bitmap(stream);

            if (bitmap != null)
                bitmap.Save(filename, format);

            stream.Flush();
            stream.Close();
            client.Dispose();
        }
    }

    internal class Livro
    {
        public Livro(string codigo, string nome, string categoria, string src, decimal preco)
        {
            Codigo = codigo;
            Nome = nome;
            Categoria = categoria;
            Src = src;
            Preco = preco;
        }

        public string Codigo { get; }
        public string Nome { get; }
        public string Categoria { get; }
        public string Src { get; }
        public decimal Preco { get; }
    }
}
