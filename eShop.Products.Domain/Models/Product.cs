﻿using eShop.Products.Domain.Interfaces;

namespace eShop.Products.Domain.Models;

public class Product : IDeletable
{
    public Guid ProductId { get; set; }
    public Guid ProductTypeId { get; set; }

    public bool IsDeleted { get; set; }

    public Guid MakerId { get; set; }
    public Maker Maker { get; set; }

    public string Number { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }


    /// <summary>
    /// Цена
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// Наличие в магазинах
    /// </summary>
    public int Availability { get; set; }
    /// <summary>
    /// Средняя оценка в отзывах
    /// </summary>
    public double Stars { get; set; }


    public HashSet<ProductParamValue> ParamValues { get; set; }
}
