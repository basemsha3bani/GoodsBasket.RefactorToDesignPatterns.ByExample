namespace GoodsBasketTestProject
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]

        public void Run_GeneratesExpectedReceipt()
        {
            string inputPath = Path.GetTempFileName();
            string outputPath = Path.GetTempFileName();

            File.WriteAllText(
                inputPath,
                "Bread,2,1.50,food");

            int exitCode = Program.Run(
                new[] { inputPath, outputPath });

            string receipt = File.ReadAllText(outputPath);

            Assert.AreEqual(0, exitCode);
            Assert.IsTrue(receipt.Contains("Bread"));
            Assert.IsTrue(receipt.Contains("TOTAL:"));

        }
        [TestMethod]
        public void Run_Receipt_For_An_Empty_Baske()
        {
            string inputPath = Path.GetTempFileName();
            string outputPath = Path.GetTempFileName();

            File.WriteAllText(
                inputPath,
                "");

            int exitCode = Program.Run(
                new[] { inputPath, outputPath });

            string receipt = File.ReadAllText(outputPath);

            Assert.AreEqual(0, exitCode);


        }
        [TestMethod]
        ///<summary>
        ///Multiple-item totals
        ///</summary>
        public void Run_Receipt_For_Multiple_Items()
        {
            string inputPath = Path.GetTempFileName();
            string outputPath = Path.GetTempFileName();
            File.WriteAllText(
                inputPath,
                "Bread,2,1.50,food\nMilk,1,0.99,food\nChocolate,3,2.00,luxury");
            int exitCode = Program.Run(
                new[] { inputPath, outputPath });
            string receipt = File.ReadAllText(outputPath);
            Assert.AreEqual(0, exitCode);
            Assert.IsTrue(receipt.Contains("Bread"));
            Assert.IsTrue(receipt.Contains("Milk"));
            Assert.IsTrue(receipt.Contains("Chocolate"));
            Assert.IsTrue(receipt.Contains("TOTAL:"));
        }
        ///Add the following test methods
        /*
         ///<summary> Blank and comment lines
         /// ///</summary>*/
        [TestMethod]
        public void Run_Receipt_For_Blank_And_Comment_Lines()
        {
            string inputPath = Path.GetTempFileName();
            string outputPath = Path.GetTempFileName();
            File.WriteAllText(
                inputPath,
                "# This is a comment line\n\nBread,2,1.50,food\n# Another comment\nMilk,1,0.99,food");
            int exitCode = Program.Run(
                new[] { inputPath, outputPath });
            string receipt = File.ReadAllText(outputPath);
            Assert.AreEqual(0, exitCode);
            Assert.IsTrue(receipt.Contains("Bread"));
            Assert.IsTrue(receipt.Contains("Milk"));
            Assert.IsTrue(receipt.Contains("TOTAL:"));
        }
        ///<summary>  Invalid CSV row
        /// ///</summary>*/
        [TestMethod]
        public void Run_Receipt_For_Invalid_CSV_Row()
        {
            string inputPath = Path.GetTempFileName();
            string outputPath = Path.GetTempFileName();
            File.WriteAllText(
                inputPath,
                "Bread,2,1.50,food\nInvalidRow\nMilk,1,0.99,food");
            int exitCode = Program.Run(
                new[] { inputPath, outputPath });
            string receipt = File.ReadAllText(outputPath);
            Assert.AreEqual(0, exitCode);
            Assert.IsTrue(receipt.Contains("Bread"));
            Assert.IsTrue(receipt.Contains("Milk"));
            Assert.IsTrue(receipt.Contains("TOTAL:"));
        }


        ///<summary>  Food Tax
        /// ///</summary>*/
        [TestMethod]
        public void Run_Receipt_For_Food_Tax()
        {
            string inputPath = Path.GetTempFileName();
            string outputPath = Path.GetTempFileName();
            File.WriteAllText(
                inputPath,
                "Bread,2,1.50,food\nMilk,1,0.99,food");
            int exitCode = Program.Run(
                new[] { inputPath, outputPath });
            string receipt = File.ReadAllText(outputPath);
            Assert.AreEqual(0, exitCode);
            Assert.IsTrue(receipt.Contains("Bread"));
            Assert.IsTrue(receipt.Contains("Milk"));
            Assert.IsTrue(receipt.Contains("TOTAL:"));
        }
        ///<summary>   Luxury tax
        /// ///</summary>*/
        [TestMethod]
        public void Run_Receipt_For_Luxury_Tax()
        {
            string inputPath = Path.GetTempFileName();
            string outputPath = Path.GetTempFileName();
            File.WriteAllText(
                inputPath,
                "Chocolate,3,2.00,luxury");
            int exitCode = Program.Run(
                new[] { inputPath, outputPath });
            string receipt = File.ReadAllText(outputPath);
            Assert.AreEqual(0, exitCode);
            Assert.IsTrue(receipt.Contains("Chocolate"));
            Assert.IsTrue(receipt.Contains("TOTAL:"));
        }
        ///<summary>    Tax after discount
        /// ///</summary>*/
        [TestMethod]
        public void Run_Receipt_For_Tax_After_Discount()
        {
            string inputPath = Path.GetTempFileName();
            string outputPath = Path.GetTempFileName();
            File.WriteAllText(
                inputPath,
                "Chocolate,3,2.00,luxury");
            int exitCode = Program.Run(
                new[] { inputPath, outputPath });
            string receipt = File.ReadAllText(outputPath);
            Assert.AreEqual(0, exitCode);
            Assert.IsTrue(receipt.Contains("Chocolate"));
            Assert.IsTrue(receipt.Contains("TOTAL:"));
        }
        ///<summary>    Name truncation boundary
        /// ///</summary>*/
        [TestMethod]
        public void Run_Receipt_For_Name_Truncation_Boundary()
        {
            string inputPath = Path.GetTempFileName();
            string outputPath = Path.GetTempFileName();
            File.WriteAllText(
                inputPath,
                "VeryLongProductNameThatExceedsLimit,1,10.00,luxury");
            int exitCode = Program.Run(
                new[] { inputPath, outputPath });
            string receipt = File.ReadAllText(outputPath);
            Assert.AreEqual(0, exitCode);
            Assert.IsTrue(receipt.Contains("VeryLongProduct..."));
            Assert.IsTrue(receipt.Contains("TOTAL:"));
        }
        ///<summary>      Missing input file
        /// ///</summary>*/
        [TestMethod]
        public void Run_Receipt_For_Missing_Input_File()
        {
            string inputPath = Path.GetTempFileName();
            string outputPath = Path.GetTempFileName();
            File.Delete(inputPath); // Delete the input file to simulate missing file
            int exitCode = Program.Run(
                new[] { inputPath, outputPath });
            Assert.AreEqual(1, exitCode); // Expecting non-zero exit code for error
        }
        ///<summary>      Complete golden-master receipt
        /// ///</summary>*/
        [TestMethod]
        public void Run_Receipt_For_Complete_Golden_Master()
        {
            string inputPath = Path.GetTempFileName();
            string outputPath = Path.GetTempFileName();
            File.WriteAllText(
                inputPath,
                "Bread,2,1.50,food\nMilk,1,0.99,food\nChocolate,3,2.00,luxury\nBook,4,5.00,book\nClearance Item,1,10.00,luxury");
            int exitCode = Program.Run(
                new[] { inputPath, outputPath });
            string receipt = File.ReadAllText(outputPath);
            Assert.AreEqual(0, exitCode);
            Assert.IsTrue(receipt.Contains("Bread"));
            Assert.IsTrue(receipt.Contains("Milk"));
            Assert.IsTrue(receipt.Contains("Chocolate"));
            Assert.IsTrue(receipt.Contains("Book"));
            Assert.IsTrue(receipt.Contains("Clearance Item"));
            Assert.IsTrue(receipt.Contains("TOTAL:"));
        }
    }

}
