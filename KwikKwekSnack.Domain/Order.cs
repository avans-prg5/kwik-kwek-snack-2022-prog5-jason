using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KwikKwekSnack.Domain
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Drink orders")]
        public virtual ICollection<DrinkOrder> DrinkOrders { get; set; }
        
        [DisplayName("Snack orders")]
        public virtual ICollection<SnackOrder> SnackOrders { get; set; }
        [Required]
        [DisplayName("Creatietijd")]
        public DateTime CreatedDateTime { get; set; }
        [Required]
        [DisplayName("Leveringstype")]
        public OrderDeliveryType DeliveryType { get; set; }       
        public OrderStatusType Status { get; set; }       
    }
    public enum OrderDeliveryType
    {
        [Display(Name="Take-away")]
        TakeAway,
        [Display(Name="Eat-in")]
        EatIn
    }

    public enum OrderStatusType
    {
        [Display(Name ="Niet aangemaakt")]
        NotCreated,
        [Display(Name = "Order aangemaakt")]
        OrderCreated,
        [Display(Name = "In wachtlijst")]
        InQueue,
        [Display(Name = "Wordt gemaakt")]
        BeingMade,
        [Display(Name = "Klaar")]
        Ready,
        [Display(Name = "Order voltooid")]
        OrderCompleted
    }
}
