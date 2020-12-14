using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Context;
using Domain;

namespace Data
{
    public class ProductsCustomRepository : IProductsCustomRepository
    {
        private ShopSiteDB db;

        public ProductsCustomRepository(ShopSiteDB context)
        {
            db = context;
        }

        public IEnumerable<SelectedProductGroup> GetSelectedGroupByGroupId(int id)
        {
            return db.SelectedProductGroup.Where(g => g.GroupId == id);
        }

        public IEnumerable<ProductGroups> GetRelatedPgs(ProductGroups pg)
        {
            var list = new List<ProductGroups> { pg };

            if (pg.ProductGroups1.Any())
            {
                foreach (var sec in pg.ProductGroups1.ToList())
                {
                    list.Add(sec);

                    if (sec.ProductGroups1.Any())
                    {
                        list.AddRange(sec.ProductGroups1.ToList());
                    }
                  
                }
            }

            return list;
        }

        public IEnumerable<Products> Search(string q)
        {
            var list = new List<Products>();

            list.AddRange(db.ProductTags.Where(t => t.Tag == q).Select(p => p.Products).ToList());
            list.AddRange(db.Products.Where(p => p.ProductTitle.Contains(q) || p.ShortDescription.Contains(q) || 
                                                 p.Description.Contains(q)));
            return list.Distinct();
        }
    }
}
