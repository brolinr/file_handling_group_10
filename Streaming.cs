using System;
using System.IO;

namespace file_handling
{
    // This streaming example uses a txt filw which lists a product in line after line
    class Streaming
    {
        private static string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); // Desktop path
        private static string file_path = Path.Combine(path, "products_stream.txt"); // File path

        // Performing the file writing
        private static void file_writing(string product_details)
        {
            var writer = new StreamWriter(file_path, append: true);
            writer.WriteLine(product_details);
            
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
            file_writing($"{product_name},{product_color},{product_price},{out_of_stock}");
        }

        private static void file_reading()
        {
            var reader = new StreamReader(file_path);
            string? line;

            // So long as the line being printed is not null and has a value we print it
            while ((line = reader.ReadLine()) != null)
            {
                Console.WriteLine(line);
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
                } else
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
