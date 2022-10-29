﻿using System;
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
        [DisplayName("Creation time")]
        public DateTime CreatedDateTime { get; set; }
        [Required]
        [DisplayName("Delivery type")]
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
        [Display(Name ="Not created")]
        NotCreated,
        [Display(Name = "Order created")]
        OrderCreated,
        [Display(Name = "In queue")]
        InQueue,
        [Display(Name = "Being made")]
        BeingMade,
        [Display(Name = "Ready")]
        Ready,
        [Display(Name = "Order completed")]
        OrderCompleted
    }
}
