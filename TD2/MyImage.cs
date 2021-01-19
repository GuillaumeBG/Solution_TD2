using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TD2
{
    class MyImage
    {
        string type;
        int taille;
        int offset;
        int largeur;
        int longueur;
        int nombreBitsParPixel;
        int[,][] matriceRGB;

        /// <summary>
        /// Constructeur de la classe MyImage qui lit un fichier ( .bmp).
        /// </summary>
        /// <param name="image">Nom de l'image</param>
        public MyImage(string image)
        {
            byte[] myfile = File.ReadAllBytes(image);
            if (myfile[0] == 66 && myfile[1] == 77) this.type = "BM";
            this.taille = Convertir_Endian_To_Int(myfile,2,4);
            this.offset = Convertir_Endian_To_Int(myfile, 10, 4);
            this.largeur = Convertir_Endian_To_Int(myfile, 18, 4);
            this.longueur = Convertir_Endian_To_Int(myfile, 22, 4);
            this.nombreBitsParPixel = Convertir_Endian_To_Int(myfile, 28, 2);
            this.matriceRGB = new int[this.largeur, this.longueur][];
            int index1 = 0;
            int index2 = 0;
            int cpt = 0;
            for (int i = this.offset; i < myfile.Length; i++)
            {
                index1 = cpt % this.largeur;
                index2 = cpt / this.largeur;
                this.matriceRGB[index1, index2] = new int[3];
                this.matriceRGB[index1, index2][0] = myfile[i];
                i++;
                this.matriceRGB[index1, index2][1] = myfile[i];
                i++;
                this.matriceRGB[index1, index2][2] = myfile[i];
                cpt++;
            }
        }

        /// <summary>
        /// prend une instance de MyImage et la transforme en fichier binaire respectant la structure du fichier.bmp
        /// </summary>
        /// <param name="file"></param>
        public void From_Image_To_File(string file)
        {
            //File.WriteAllBytes(file, ); //./Images/Sortie.bmp
        }

        /// <summary>
        /// Convertit une séquence d’octets au format little endian en entier.
        /// </summary>
        /// <param name="tab">tableau à convertir</param>
        /// <param name="debut">place dans le tableau,de 1 à..., du début de la sequence à decoder </param>
        /// <param name="nombre">nombre d'octect à décoder</param>
        /// <returns></returns>
        public int Convertir_Endian_To_Int(byte[] tab,int debut,int nombre)
        {
            double somme = 0;
            int cpt = 0;
            for (int i = debut; i < debut+nombre; i++)
            {
                somme += tab[i] * Math.Pow(256, cpt);
                cpt++;
            }
            return Convert.ToInt32(somme);
        }

        /// <summary>
        /// convertit un entier en séquence d’octets au format little endian.
        /// </summary>
        /// <param name="val">valeur à convertir</param>
        /// <param name="taille">nombre d'octect sur lequel transcrire la valeur</param>
        /// <returns></returns>
        public byte[] Convertir_Int_To_Endian(int val,int taille)
        {
            int test = 0;
            for(int i=1;i<=taille;i++)
            {
                test += Convert.ToInt32(Math.Pow(256, i));
            }
            if (test < val) return null;
            else
            {
                byte[] tab = new byte[taille];
                while (val / 256 != 0)
                {
                    taille--;
                    tab[taille] = Convert.ToByte(val / Convert.ToInt32(Math.Pow(256, taille)));
                    val -= val % Convert.ToInt32(Math.Pow(256, taille));
                }
                return tab;
            }
        }
    }
}
