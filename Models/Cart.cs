using System.ComponentModel.DataAnnotations.Schema;

public class Cart
{
    public int CartId { get; set; }
    public string UserId { get; set; }

    [ForeignKey("Product")]
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public Product Product { get; set; }
}
