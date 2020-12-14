﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Data
{
    public interface IProductsCustomRepository
    {
        IEnumerable<SelectedProductGroup> GetSelectedGroupByGroupId(int id);
        IEnumerable<ProductGroups> GetRelatedPgs(ProductGroups pg);
        IEnumerable<Products> Search(string q);

    }
}
