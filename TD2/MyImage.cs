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
        int largeur;
        int longueur;
        int nombreBitsParPixel;
        byte[] myfile;

        /// <summary>
        /// Constructeur de la classe MyImage qui lit un fichier ( .bmp).
        /// </summary>
        /// <param name="image">Nom de l'image</param>
        public MyImage(string image)
        {
            byte[] myfile = File.ReadAllBytes(image);
            if (myfile[0] == 6 && myfile[1] == 6 && myfile[2] == 7 && myfile[3] == 7) this.type = "BN";
            this.taille = myfile[5] * 8 + myfile[4] * 4 + myfile[6] * 2 + myfile[7] * 1;
            int tailleInfoHeader = myfile[14];
            this.largeur = myfile[15] + myfile[16] + myfile[17] + myfile[18];
            this.longueur = myfile[19] + myfile[20] + myfile[21] + myfile[22];
            this.nombreBitsParPixel = myfile[23] + myfile[24];
        }

        /// <summary>
        /// prend une instance de MyImage et la transforme en fichier binaire respectant la structure du fichier.bmp
        /// </summary>
        /// <param name="file"></param>
        public void From_Image_To_File(string file)
        {
            File.WriteAllBytes(file, this.myfile); //./Images/Sortie.bmp
        }

        /// <summary>
        /// Convertit une séquence d’octets au format little endian en entier.
        /// </summary>
        /// <param name="tab"> tableau de bits à convertir</param>
        /// <returns></returns>
        public int Convertir_Endian_To_Int(byte[] tab)
        {
            int somme = 0;
            int cpt = 0;
            for (int i = tab.Length - 1; i >= 0; i--)
            {
                somme += tab[i] * 2 ^ (cpt);
                cpt++;
            }
            return somme;
        }

        /// <summary>
        /// convertit un entier en séquence d’octets au format little endian.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public byte[] Convertir_Int_To_Endian(int val)
        {
            byte[] tab = new byte[];
            return tab;
        }
    }
}
