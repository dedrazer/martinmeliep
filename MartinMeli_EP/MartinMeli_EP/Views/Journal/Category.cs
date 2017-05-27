using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MartinMeli_EP.Views.Journal
{
    public class Category {
        public int categoryId { get; set; }
        public string name { get; set; }

        public Category()
        { }

        public static IEnumerable<Category> Categories = new List<Category> {
            new Category {
                categoryId = 1,
                name = "National"
            },
            new Category {
                categoryId = 2,
                name = "Overseas"
            },
            new Category {
                categoryId = 3,
                name = "Sports"
            },
            new Category {
                categoryId = 4,
                name = "Odd"
            },
            new Category {
                categoryId = 5,
                name = "Travel"
            },
            new Category {
                categoryId = 6,
                name = "Opinion"
            }
        };
    }
}