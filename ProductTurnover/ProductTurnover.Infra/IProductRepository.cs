﻿using ProductTurnover.Infra.Record;

namespace ProductTurnover.Infra
{
    public interface IProductRepository
    {
        Product Read(int EAN);
    }
}
