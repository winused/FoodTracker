using System;
using System.Collections.Generic;

namespace DS_Proje3._1
{


    public class Mahalle//Mahalle Sınıfı
    {
        public string mahalle_adi;
        public List<List<YemekSınıfı>> Siparis_Listesi;
        public Mahalle(string mahalle_adi, List<List<YemekSınıfı>> Siparis_Listesi)
        {
            this.mahalle_adi = mahalle_adi;
            this.Siparis_Listesi = Siparis_Listesi;
        }

    }

    public class YemekSınıfı//Yemek bilgileri 
    {
        public string urun_adi;  //random yiyecek listesi
        public int urun_adedi;  //1-8 arası rasgele sayı
        public double urun_fiyati; //her urun için 1 kez oluşturulacak 

        public YemekSınıfı(string Urun_adi, int Urun_adedi, double Urun_fiyati)
        {
            this.urun_adi = Urun_adi;
            this.urun_adedi = Urun_adedi;
            this.urun_fiyati = Urun_fiyati;
        }
    }

    class TreeNode
    {
        public Mahalle mahal { get; set; }
        public TreeNode LeftNode { get; set; }
        public TreeNode RightNode { get; set; }

    }
    class BinaryTree//ağaç oluşturur
    {
        public TreeNode Root { get; set; }

        public bool Add(Mahalle mahalle)//Ağaca mahalle nesnesi ekler
        {
            TreeNode before = null;
            TreeNode current = this.Root;

            //Bu kısımdaki karşılaştırmayı  if (Mahalle.siparis listesi.Length < (parent)current.data.siparis listesi.Length)
            // şeklinde güncelle
            while (current != null)
            {
                before = current;
                if (mahalle.Siparis_Listesi.Count < current.mahal.Siparis_Listesi.Count) //Is new node in left tree? 
                    current = current.LeftNode;
                else if (mahalle.Siparis_Listesi.Count >= current.mahal.Siparis_Listesi.Count) //Is new node in right tree?
                    current = current.RightNode;
                else
                {
                    //Exist same value
                    return false;
                }
            }

            TreeNode newNode = new TreeNode();
            newNode.mahal = mahalle;

            if (this.Root == null)//Tree ise empty
                this.Root = newNode;
            else
            {
                if (mahalle.Siparis_Listesi.Count < before.mahal.Siparis_Listesi.Count)
                    before.LeftNode = newNode;
                else
                    before.RightNode = newNode;
            }

            return true;
        }

        public double UrunGuncelle(TreeNode parent,string aranan_urun,double top_urun_mik)// Ağaçta ürün arar
        {
            
            if (parent != null)
            {
                top_urun_mik += urun_bul_indirim_yap(aranan_urun, parent);
                
                top_urun_mik = UrunGuncelle( parent.LeftNode, aranan_urun, top_urun_mik);
                top_urun_mik = UrunGuncelle(parent.RightNode, aranan_urun, top_urun_mik);

            }
            return top_urun_mik;
        }

        public double urun_bul_indirim_yap(string aranan_urun, TreeNode parent)//Mahallede ürün arar ve fiyatını günceller
        {
            double top_urun_adet = 0;
            double onceki_fiyat = 0;
            double yeni_fiyat = 0;

            if (parent != null)
            {
                foreach (List<YemekSınıfı> item1 in parent.mahal.Siparis_Listesi)
                {
                    foreach (YemekSınıfı item2 in item1)
                    {
                        if (aranan_urun == item2.urun_adi)
                        {
                            top_urun_adet += item2.urun_adedi;
                            // urun fiyatına %10 indirim yapılır
                            onceki_fiyat = item2.urun_fiyati;
                            yeni_fiyat = item2.urun_fiyati * 9 / 10;
                            item2.urun_fiyati = yeni_fiyat;
                        }

                    }

                }

            }

            if (onceki_fiyat != 0 )
            {
                Console.WriteLine("Önceki fiyat ={0,-5} Güncellenmiş Fiyat={1}", onceki_fiyat, yeni_fiyat);
            }

            return top_urun_adet;
        }


        public int GetTreeDepth()
        {
            return this.GetTreeDepth(this.Root);
        }

        private int GetTreeDepth(TreeNode parent)//Ağaç derinliğini bulur.
        {
            return parent == null ? 0 : Math.Max(GetTreeDepth(parent.LeftNode), GetTreeDepth(parent.RightNode)) + 1;
        }
        private void yazdir(TreeNode parent)//Mahalle ve teslimat bilgilerini yazdıran method
        {
            Console.WriteLine("Mahalle = " + parent.mahal.mahalle_adi + "         Sipariş Listesi");
            Console.WriteLine("-----------------------------------------------------");

            // O mahalledeki teslimatları (generic listi yazdır)
            foreach (List<YemekSınıfı> item1 in parent.mahal.Siparis_Listesi)
            {
                foreach (YemekSınıfı item2 in item1)
                {
                    Console.WriteLine("Sipariş edilen yemek/içecek çeşidi = {0,-20} Sipariş edilen ürün adedi = {1}           " +
                         "Sipariş edilen ürün birim fiyatı(TL) = {2,-15} Toplam tutar(TL) = {3:0.0} ", item2.urun_adi, item2.urun_adedi, item2.urun_fiyati
                          , (item2.urun_adedi * item2.urun_fiyati));
                }

            }
            Console.WriteLine();
        }

        public void TraversePreOrder(TreeNode parent)//Preorder dolaşır
        {
            if (parent != null)
            {
                yazdir(parent);
                TraversePreOrder(parent.LeftNode);
                TraversePreOrder(parent.RightNode);
            }
        }

        public void TraverseInOrder(TreeNode parent)//Inorder dolaşır
        {
            if (parent != null)
            {
                TraverseInOrder(parent.LeftNode);
                yazdir(parent);
                TraverseInOrder(parent.RightNode);
            }
        }

        public void TraversePostOrder(TreeNode parent)//postorder
        {
            if (parent != null)
            {
                TraversePostOrder(parent.LeftNode);
                TraversePostOrder(parent.RightNode);
                yazdir(parent);
            }
        }
    }
    class Program
    {
      

        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;

            Random random = new Random();
     
            string[] urun = { "Baklava", "Pilav", "Tavuk", "Köfte", "Döner", "Beyti", "Çiğ Köfte", "Pasta", "Pizza", "Hamburger",
                "Pide", "Makarna", "Güveç", "Karnıyarık","Kola","Ayran", "Limonata","Ice Tea","Su","Fanta","Sprite","Şalgam","Çay","Kahve",
                "Poğaça","Süt","Balık","Karides","Maden Suyu","Meyve Suyu","İskender"};

            //ürün fiyatları oluşturup dizide saklar
            int[] urun_fiyati = new int[urun.Length];
            for (int sayi = 0; sayi<urun.Length; sayi++)
            {
                urun_fiyati[sayi] = random.Next(1,60);

            }

            string[] mahalle_adi = { "Özkanlar", "Evka 3", "Atatürk", "Erzene", "Kazımdirik" };
            int sayac = 0;

            BinaryTree agac = new BinaryTree();
            
            Mahalle mahalle;

            YemekSınıfı urun_bilgisi;
            double yemek_top_fiyat;
            
            List<List<YemekSınıfı>> yuksekfiyatlisiparis = new List<List<YemekSınıfı>>(); //yuksekfiyatlisipariste ne tür saklanıyo
            
 

            while (sayac < mahalle_adi.Length)//Mahalleleri dolaşır
            {
                int siparis_say = random.Next(5, 11); // sipariş bilgileri sayısı
                int siparislerlisteLength = siparis_say;

                List<List<YemekSınıfı>> siparisler_listesi = new List<List<YemekSınıfı>>(siparis_say);
               
                mahalle = new Mahalle(mahalle_adi[sayac], siparisler_listesi);   

                for (int i = 0; i < siparislerlisteLength; i++)//5-10 kez döner
                {

                    int urun_say = random.Next(3, 6); // yemek sınıfı sayısı ilk fora alınacak
                    List<YemekSınıfı> siparis_listesi = new List<YemekSınıfı>(urun_say);

                    yemek_top_fiyat = 0;

                    for (int j = 0; j < urun_say; j++)//3-5 kez döner
                    {

                        int yemek_say = random.Next(1, 9); // yemek sınıfı sayısı
                        //YemekSınıfı(urun_adı,Urun_adedi,Urun_fiyatı(max 60tl)//1 urun için 1 kez oluşturulacak
                        int secilen_urun = random.Next(urun.Length);
                        urun_bilgisi = new YemekSınıfı(urun[secilen_urun], yemek_say, urun_fiyati[secilen_urun]);
                        yemek_top_fiyat += urun_bilgisi.urun_fiyati * urun_bilgisi.urun_adedi;

                        siparis_listesi.Add(urun_bilgisi);
                    }

                    if (yemek_top_fiyat > 150)
                    {
                        yuksekfiyatlisiparis.Add(siparis_listesi);
                    }

                    siparisler_listesi.Add(siparis_listesi);

                }

                //Ağaca mahalle eklenir
                //Ağaca mahalle eklenir
                agac.Add(mahalle);
                sayac++;
            }

            //1-b
            int treedepth = agac.GetTreeDepth();
            Console.WriteLine("Ağaç derinliği=" + treedepth);

            Console.WriteLine("Inorder dolaşım = ");
            agac.TraverseInOrder(agac.Root);
            Console.WriteLine();
           
            //1-c
            Console.WriteLine("150TL'yi geçen siparişler ");
            Console.WriteLine("----------------------------");
            foreach (List<YemekSınıfı> item1 in yuksekfiyatlisiparis)
            {
                foreach (YemekSınıfı item2 in item1)
                {

                    Console.WriteLine("Sipariş edilen yemek/içecek çeşidi = {0,-20} Sipariş edilen ürün adedi = {1}           " +
                         "Sipariş edilen ürün birim fiyatı(TL) = {2,-15} Toplam tutar(TL) = {3} ", item2.urun_adi, item2.urun_adedi, item2.urun_fiyati
                          , (item2.urun_adedi * item2.urun_fiyati));

                }

                Console.WriteLine("Sipariş bilgileri sonu");

            }
            Console.WriteLine();

            //1-d
            Console.Write("Ağaçta toplam adeti bulunacak ürünü(yiyecek/içecek) giriniz:");
            string aranan_urun = Console.ReadLine();
            double top_urun_mik = 0;
            
            top_urun_mik = agac.UrunGuncelle(agac.Root, aranan_urun,top_urun_mik);

            Console.WriteLine("{0} ürünü ağaçta {1} adet bulunmaktadır.", aranan_urun, top_urun_mik);
            Console.WriteLine();

            Console.WriteLine("Güncellenmiş Fiyatlar ile ağaç");
            Console.WriteLine("--------------------------------");
            agac.TraverseInOrder(agac.Root);

        }
    }
}
