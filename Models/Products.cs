public class Product {
    public int ProductId { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
    public string? Description { get; set; }

    public List<Cart> Cart { get; set; }

    public Product() {
        Cart = new List<Cart>();
    }
}
