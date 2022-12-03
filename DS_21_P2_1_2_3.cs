using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;



namespace Proje1_2_3
{
    public class Mahalle//Mahalle Sınıfı
    {
        public string mahalle_adi;
        public List<Teslimat> teslimatlar;
        public Mahalle(string mahalle_adi, List<Teslimat> teslimatlar)
        {
            this.mahalle_adi = mahalle_adi;
            this.teslimatlar = teslimatlar;
        }
        public void yazdir()//Mahalle ve teslimat bilgilerini yazdıran method
        {
            Console.WriteLine("Mahalle = " + mahalle_adi + "    Teslimat Sayısı =" + teslimatlar.Count());
            Console.WriteLine("-----------------------------------------------------");

            // O mahalledeki teslimatları (generic listi yazdır)
            foreach (Teslimat item1 in teslimatlar)
            {
                Console.WriteLine("Sipariş edilen yemek çeşidi = {0,-20} Sipariş edilen yemek adedi = {1,5} ", item1.yemek_adi, item1.yemek_adedi);
            }
            Console.WriteLine();
        }


    }
    public class Teslimat//Teslimat Sınıfı
    {
        public string yemek_adi;
        public int yemek_adedi;

        public Teslimat(string yemek_adi, int yemek_adedi)
        {
            this.yemek_adi = yemek_adi;
            this.yemek_adedi = yemek_adedi;
        }
    }

    public class StackX<T>//Generic Stack sınıfı
    {
        readonly int maxSize;
        int top;
        T[] stackArray;
        public StackX()
        : this(100)
        { }
        public StackX(int size)//Constructor
        {
            maxSize = size; //set array size
            stackArray = new T[maxSize]; // create array
            top = -1;
        }

        public void Push(T item)
        {
            if (top >= maxSize)
                Console.WriteLine("Stack Overflow Exception");
            stackArray[++top] = item;
        }//Push method
        public T Pop()
        {
            return stackArray[top--];
        } // Pop method

        public T Peek()
        {
            return stackArray[top];
        }//Peek method

        public bool isEmpty()
        {
            return (top == -1);
        }//true is stack is empty
        public bool isFull()
        {
            return (top == maxSize - 1);
        }//true is stack is full

    } // End class StackX

    public class QueueX<T>//Queue sınıfı
    {
        readonly int maxSize;
        int front, rear;
        T[] queArray;
        int nItems;
        public QueueX()
        : this(100)
        { }
        public QueueX(int size)//Yapılandırıcı
        {
            maxSize = size; //set array size
            queArray = new T[maxSize]; // create array
            front = 0;
            rear = -1;
            nItems = 0;
        }
        public void insert(T item) // Kuyruk sonuna eleman ekler
        {
            if (rear == maxSize - 1) // başa dönme durumu
                rear = -1;
            queArray[++rear] = item; // sonu arttır ve ekle
            nItems++;
        }


        public T remove() // Kuyruğun başından bir eleman çıkarır
        {
            T temp = queArray[front++]; // Değeri alıp başı arttır
            if (front == maxSize) // başa dönme durumu
                front = 0;
            nItems--;
            return temp;
        }
        public bool isEmpty()
        {
            return (nItems == 0);
        }// true, Kuyruk boş ise

        public bool isFull()
        {
            return (nItems == maxSize);
        }// true, Kuyruk dolu ise
        public T peekFront()
        {
            return queArray[front];
        }//Peek methodu(başı silmeden döndürür)
        public int eleman_say()
        {
            return (nItems);
        }//Queue'deki eleman sayısını döndürür

    } // end class QueueX

    public class PQ<T>//Priority Queue sınıfı
    {
        private List<Mahalle> liste;
        readonly int boyut;

        public PQ()
            : this(100)
        { }
        public PQ(int s) // Yapılandırıcı
        {
            boyut = s;
            liste = new List<Mahalle>();

        }
        public void enque(Mahalle item) // Kuyruk sonuna eleman ekler
        {
            liste.Add(item); // sonu arttır ve ekle

        }
        public Mahalle remove_buyuk_tes() // Kuyruğun başından bir eleman çıkarır
        {
            //Teslimat sayısı en büyük değer 
            //******* teslimat sayısı en büyük mahalleyi bul ve çıkar.
            //En tes büyük mahalle methodu
            Mahalle buyuk_tes = buyuk_teslimat_bul();

            buyuk_tes.yazdir();
            liste.Remove(buyuk_tes);
            return buyuk_tes;
        }
        public bool isEmpty()//Liste boşsa true
        {
            return (liste.Count == 0);
        }

        public int eleman_say()//Öncelikli kuyruktaki eleman sayısını verir
        {
            return (liste.Count);
        }

        private Mahalle buyuk_teslimat_bul()//Teslimat sayısı en büyük mahalleyi bulur.
        {
            int max = -1;
            Mahalle bulunan_mah = null;

            foreach (Mahalle mahal in liste)
            {
                if (mahal.teslimatlar.Count > max)
                {
                    max = mahal.teslimatlar.Count;
                    bulunan_mah = mahal;
                }
            }
            return bulunan_mah;
        }


    } // end class PQ


    class Program
    {
        static void YıgıtOlusturYazdır(ArrayList array)//Mahalleler yığıtı oluşturur ve yığıttaki bilgileri konsola yazdırır.
        {
            StackX<Mahalle> mahalleler = new StackX<Mahalle>();

            foreach (Mahalle item in array)
            {
                mahalleler.Push(item);
            }

            Console.WriteLine("\nYığıt olarak mahalleler  ");
            Console.WriteLine("******************************");

            for (int i = 0; i < array.Count; i++)
            {
                Mahalle mah = mahalleler.Pop();
                mah.yazdir();
            }
        }

        static void KuyrukOlusturYazdır(ArrayList array)//Mahalleler kuyruğu oluşturur ve kuyruktaki bilgileri konsola yazdırır.
        {

            QueueX<Mahalle> mahalleler = new QueueX<Mahalle>();

            foreach (Mahalle item in array)
            {

                mahalleler.insert(item);
            }

            Console.WriteLine("\nKuyruk olarak mahalleler  ");
            Console.WriteLine("******************************");
            for (int i = 0; i < array.Count; i++)
            {
                Mahalle mah = mahalleler.remove();
                mah.yazdir();
            }
        }
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;

            Random random = new Random();

            string[] yemek = { "Baklava", "Pilav", "Tavuk", "Köfte", "Döner", "Beyti", "Çiğ Köfte", "Pasta", "Pizza", "Hamburger", "Pide", "Makarna", "Güveç", "Karnıyarık" };
            List<string> yemekler = new List<string>(yemek); //Yemek çeşitlerinin saklandığı liste

            string[] mahalle_adi = { "Özkanlar", "Evka 3", "Atatürk", "Erzene", "Kazımdirik", "Mevlana", "Doğanlar", "Ergene" };
            int[] teslimat_sayisi = { 5, 2, 7, 2, 7, 3, 0, 1 };

            var myArrayList = new ArrayList();//Mahallelerin saklandığı arrayList

            int sayac = 0;
            int top_teslimat_say = 0;

            Mahalle mahalle;

            Teslimat teslimat;

            List<Teslimat> genericListe;

            PQ<Mahalle> PriorityQueue = new PQ<Mahalle>();//Mahallelerin saklandığı öncelikli kuyruk

            while (sayac < mahalle_adi.Length)//Mahalleleri dolaşır
            {
                //Teslimatları tutan generic liste oluşturulur ve listeyi tutan mahalle üretilir.
                genericListe = new List<Teslimat>();
                mahalle = new Mahalle(mahalle_adi[sayac], genericListe);

                int genericListeLength = teslimat_sayisi[sayac];

                top_teslimat_say += genericListeLength;

                // Generic listede soruda istenen kadar teslimat nesnesi açılır ve bu nesneler dolaşılır
                for (int i = 0; i < genericListeLength; i++)
                {
                    // yemekler yerine random yemekler listesinden seçilir
                    // adet yerine random sayı generator
                    // Yemek adeti bu program için maximum 100 olarak verildi.
                    //Teslimat bilgileri içeren teslimat nesnesi oluşturulur.
                    teslimat = new Teslimat(yemekler[random.Next(yemekler.Count)], random.Next(100));
                    
                    //Teslimatlar listeye eklenir.
                    genericListe.Add(teslimat);
                }
                //Mahalleler arrayListe ve öncelikli kuyruğa eklenir.
                myArrayList.Add(mahalle);
                PriorityQueue.enque(mahalle);

                sayac++;
            }

            //Mahalle içeriği arraylistten alınarak yazdırılır.
            foreach (Mahalle item in myArrayList)
            {
                item.yazdir();
            }


            Console.WriteLine("\nArrayListte bulunan eleman sayısı = {0} \nToplam teslimat nesnesi sayısı = {1}", sayac, top_teslimat_say);

            //Arraylistten bilgiler alınarak yığıt ve kuyruk oluşturulup mahalle bilgileri yazdırılır.
            YıgıtOlusturYazdır(myArrayList);

            KuyrukOlusturYazdır(myArrayList);

            Console.WriteLine("Öncelikli Kuyruk ile yazdır");
            Console.WriteLine("************************************");
            int eleman = PriorityQueue.eleman_say();
            // Öncelikli  kuyruk dolaşılır ve sil methodu ile en büyük teslimat olan mahalle bulunur ve yazdırılır.
            for (int i = 0; i < eleman; i++)
            {
                PriorityQueue.remove_buyuk_tes();
            }


            Console.ReadKey();
        }
    }
}

