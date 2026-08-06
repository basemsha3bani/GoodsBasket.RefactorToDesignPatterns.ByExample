namespace ReceiptGenerator.Domain.Product
{
    public class Product
    {
        public string Name { get; set; }
        
        public string Category { get; set; }
        public Product(string name,string category)
        {
            Name = name;
           
            Category = category;
        }
    }
    
    

}
