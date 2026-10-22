using System;
using System.IO;

namespace file_handling
{
    class BinaryFiles
    {
        private static string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); // Desktop path
        private static string file_path = Path.Combine(path, "products.bin"); // File path

        // Performing the file writing
        private static void file_writing(string name, string color, double price, bool in_stock)
        {
            // If we do not use append File mode then we cannot update each time
            // we try to update the file is replaced.

            var w = new BinaryWriter(File.Open(file_path, FileMode.Append));
            w.Write(name);
            w.Write(color);
            w.Write(price);
            w.Write(in_stock);
            w.Close();

            Console.WriteLine("Your entry has been saved!");
        }

        // Generating the product details string 
        private static void enter_product()
        {
            Console.Write("Product name: ");
            string product_name = Console.ReadLine();

            Console.Write("Product color: ");
            string product_color = Console.ReadLine();

            Console.Write("Product price: ");
            double product_price = Convert.ToDouble(Console.ReadLine());

            Console.Write("Out of stock (true/false):");
            bool out_of_stock = Convert.ToBoolean(Console.ReadLine());

            Console.WriteLine();
            file_writing(product_name, product_color, product_price, out_of_stock);
        }

        // Reading file results
        private static void file_reading()
        {
            if (!File.Exists(file_path))
            {
                Console.WriteLine($"No products saved yet. Expected file: {file_path}\n");
                return;
            }

            using (var binary_reader = new BinaryReader(File.OpenRead(file_path)))
            {
                while (binary_reader.BaseStream.Position < binary_reader.BaseStream.Length)
                {
                    string product_name = binary_reader.ReadString();
                    string product_color = binary_reader.ReadString();

                    double product_price = binary_reader.ReadDouble();
                    bool product_stock = binary_reader.ReadBoolean();

                    Console.WriteLine($"Name: {product_name}\nColor: {product_color}\nPrice: {product_price}\nOut of stock: {product_stock}\n");
                }
            }
        }

        public static void main_menu()
        {
            /* Main menu text */
            Console.WriteLine("Welcome to our application\n\n");
            Console.WriteLine("1. Enter new product\n2. View all products\n3. Remove the product file\n");
            Console.WriteLine("Enter your option (0 to exit): ");

            char option = Console.ReadLine()[0]; // saving input option

            if (option == '1')
            {
                enter_product();
            }
            else if (option == '2')
            {
                file_reading();
            }
            else if (option == '3')
            {
                /* Deletion of an exisitng File */
                if (File.Exists(file_path))
                {
                    File.Delete(file_path);
                    Console.WriteLine($"File deleted in: {file_path}");
                }
                else
                {
                    Console.WriteLine($"File was not found in: {file_path}");
                }
            }
            else if (option == '0')
            {
                return;
            }
            else
            {
                Console.WriteLine("You entered an invalid option. Please try again \n\n");
            }

            main_menu();
        }
    }
}
