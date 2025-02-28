﻿using Domain.Entitys;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrostracture.Repositories
{
    public class RepositProduct : IRepositProduct
    {
        private List<Product> _products;
        public RepositProduct()
        {
            _products = new List<Product>();
        }

        private Product Find(int id)
        {
            foreach (var v in _products)
                if (v.Id == id)
                    return v;
            return null;
        }
        public Task Create(Product element)
        {
            _products.Add(element);
            return Task.CompletedTask;
        }
        public Task<bool> Delete(int id)
        {
            var find = Find(id);
            if (find != null)
            {
                _products.Remove(find);
                return Task.FromResult(true);
            }
            else
                return Task.FromResult(false);
        }
        public Task<List<Product>> ReadAll()
        {
            return Task.FromResult(_products);
        }
        public Task<Product> ReadById(int id)
        {
            var find = Find(id);
            return Task.FromResult(find);
        }
        public Task<bool> Update(Product element)
        {
            var find = Find(element.Id);
            if (find != null)
            {
                find.Length = element.Length;
                find.Height = element.Height;
                find.Width = element.Width;
                find.Priсe = element.Priсe;
                find.Discount = element.Discount;
                find.Path_picture = element.Path_picture;
                find.Id_section = element.Id_section;
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}
