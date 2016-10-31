using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Storage.Models
{
    public class DataSortModel
    {
        public SelectList ListAttributes { get; set; }

        public DataSortModel()
        {
            ListAttributes = new SelectList(
                new List<SelectListItem>
                {
                    new SelectListItem { Text = "Title", Value = "Title"},
                    new SelectListItem { Text = "CreationAuthor", Value = "CreationAuthor"},
                    new SelectListItem { Text = "CreationDate", Value = "CreationDate"},
                }, "Value", "Text");

        }


    }
}